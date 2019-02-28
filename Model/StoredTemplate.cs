using System;

namespace Custom.TemplateEngine.Model
{
    public class StoredTemplate
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime LastRequested { get; set; }
    }
}
