using System.Diagnostics;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Custom.TemplateEngine.Derived
{
    public class TemplateRazorEngine : RazorViewEngine
    {
        public TemplateRazorEngine(
            TemplateFactoryProvider pageFactory,
            IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder,
            OptionsManager<TemplateOptions> optionsAccessor,
            RazorProjectFileSystem razorFileSystem,
            ILoggerFactory loggerFactory,
            DiagnosticSource diagnosticSource)
            : base(
                pageFactory, pageActivator, htmlEncoder,
                optionsAccessor, razorFileSystem,
                loggerFactory,
                diagnosticSource)
        {

        }
    }
}
