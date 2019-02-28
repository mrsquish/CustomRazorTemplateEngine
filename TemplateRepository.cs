using System.Linq;
using Custom.TemplateEngine.Derived;
using Dapper;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Custom.TemplateEngine
{
    public class TemplateRepository : IFileProvider
    {
        private readonly SqlConnectionFactory _connectionFactory;
        
        public TemplateRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;            
        }
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            using (var connection = _connectionFactory.GetNamedConnection("Client"))
            {
                var path = new TemplatePathContext() {Exists = true};

                var items = connection.Query<TemplateContext>("SELECT * FROM [dbo].[Templates] WHERE Name LIKE @Path",
                    new {Path = subpath + "%"}).ToList();
                if (items.Any())
                    path.AddRange(items);
                return path;
            }
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var result = new TemplateContext(_connectionFactory, subpath);
            return result.Exists ? result as IFileInfo : new NotFoundFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return new TemplateChangeDetection(_connectionFactory, filter);
        }
    }
}
