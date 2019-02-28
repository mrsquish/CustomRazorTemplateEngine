using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Custom.TemplateEngine.Derived
{
    public class TemplateFactoryProvider : DefaultRazorPageFactoryProvider
    {
        public TemplateFactoryProvider(TemplateCompilerProvider viewCompilerProvider) : base(viewCompilerProvider)
        {        
        }
    }
}
