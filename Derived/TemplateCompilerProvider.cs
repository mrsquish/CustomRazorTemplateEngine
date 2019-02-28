using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Custom.TemplateEngine.Derived
{
    public class TemplateCompilerProvider : RazorViewCompilerProvider
    {
        public TemplateCompilerProvider(
            ApplicationPartManager applicationPartManager,
            RazorProjectEngine razorProjectEngine,
            TemplateFileProviderAccessor fileProviderAccessor,
            CSharpCompiler csharpCompiler,
            OptionsManager<TemplateOptions> options,
            IViewCompilationMemoryCacheProvider compilationMemoryCacheProvider,
            ILoggerFactory loggerFactory) :
            base(applicationPartManager,
                razorProjectEngine,
                fileProviderAccessor,
                csharpCompiler,
                options,
                compilationMemoryCacheProvider,
                loggerFactory
            )
        {
            Debug.Print("TemplateCompilerProvider Constructed");
        }
    }
}
