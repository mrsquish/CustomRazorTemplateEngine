using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace Custom.TemplateEngine.Derived
{
    public class TemplatePathContext : List<TemplateContext>, IDirectoryContents
    {
        public new bool Exists { get; set; }

        public new IEnumerator<IFileInfo> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
