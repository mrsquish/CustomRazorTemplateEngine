using System;
using System.Linq;
using Custom.TemplateEngine.Model;
using Dapper;
using Microsoft.Extensions.Primitives;

namespace Custom.TemplateEngine
{
    public class TemplateChangeDetection : IChangeToken
    {
        private string _connection;
        private readonly SqlConnectionFactory _connectionFactory;
        private readonly string _viewPath;

        public TemplateChangeDetection(SqlConnectionFactory connectionFactory, string viewPath)
        {            
            _connectionFactory = connectionFactory;
            _viewPath = viewPath;
        }

        public bool ActiveChangeCallbacks => false;

        public bool HasChanged
        {
            get
            {

                var query = "SELECT LastRequested, LastModified FROM [dbo].[Templates] WHERE Name = @Name;";
                try
                {
                    using (var conn = _connectionFactory.GetNamedConnection("Client"))
                    {
                        var template = conn.Query<StoredTemplate>(query, new {Name = _viewPath}).FirstOrDefault();
                        if (template == null)
                            return false;
                        return template.LastModified > template.LastRequested;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public IDisposable RegisterChangeCallback(Action<object> callback, object state) => EmptyDisposable.Instance;
    }

    internal class EmptyDisposable : IDisposable
    {
        public static EmptyDisposable Instance { get; } = new EmptyDisposable();
        private EmptyDisposable() { }
        public void Dispose() { }
    }
}

