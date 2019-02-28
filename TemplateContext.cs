using System;
using System.IO;
using System.Linq;
using System.Text;
using Custom.TemplateEngine.Model;
using Dapper;
using Microsoft.Extensions.FileProviders;

namespace Custom.TemplateEngine
{    
    public class TemplateContext : IFileInfo
    {
        private readonly SqlConnectionFactory _connectionFactory;
        private StoredTemplate _template;
        private byte[] _viewContent;

        public TemplateContext()
        {
        }

        public TemplateContext(SqlConnectionFactory connectionFactory, string viewPath)
        {
            _connectionFactory = connectionFactory;
            GetView(viewPath);
        }

        public bool Exists => _template != null;

        public bool IsDirectory => false;

        public DateTimeOffset LastModified => _template?.LastModified ?? DateTime.MinValue;

        public long Length
        {
            get
            {
                using (var stream = new MemoryStream(_viewContent))
                {
                    return stream.Length;
                }
            }
        }

        public string Name => _template?.Name;

        public string PhysicalPath => null;

        public Stream CreateReadStream()
        {
            return new MemoryStream(_viewContent);
        }

        private void GetView(string viewPath)
        {
            var query = @"SELECT Content, LastModified FROM [dbo].[Templates] WHERE Name = @Name;
                          UPDATE [dbo].[Templates] SET LastRequested = GetUtcDate() WHERE Name = @Name";
            using (var conn = _connectionFactory.GetNamedConnection("Client"))
            {
                _template = conn.Query<StoredTemplate>(query, new {Name = viewPath}).FirstOrDefault();
                _viewContent = Encoding.UTF8.GetBytes(_template.Content);
            }
        }
    }
}
