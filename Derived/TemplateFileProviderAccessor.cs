using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.Options;

namespace Custom.TemplateEngine.Derived
{
    public class TemplateFileProviderAccessor : DefaultRazorViewEngineFileProviderAccessor
    {
        public TemplateFileProviderAccessor(OptionsManager<TemplateOptions> options) : base(options)
        {
            Debug.Print("TemplateFileProviderAccessor Constructed");
        }
    }
}
