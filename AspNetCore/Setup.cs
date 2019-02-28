using Custom.TemplateEngine.Derived;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Custom.TemplateEngine.AspNetCore
{
    public static class Setup
    {
        public static void AddCustomTemplateEngine(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<TemplateEngine>();
            services.AddScoped<TemplateRazorEngine>();
            services.AddScoped<TemplateFactoryProvider>();
            services.AddScoped<TemplateCompilerProvider>();
            services.AddScoped<TemplateFileProviderAccessor>();
            services.AddScoped<TemplateRepository>();
            services.AddScoped<OptionsManager<TemplateOptions>>();
            services.AddTransient<SqlConnectionFactory>();
            services.Configure<TemplateOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new TemplateNameExpander());
                options.ViewLocationFormats.Add("{0}");
                options.AreaViewLocationFormats.Add("{0}");
                options.FileProviders.Clear();
                options.FileProviders.Add(new TemplateRepository(new SqlConnectionFactory(configuration)));
            });

            services.Configure<RazorViewEngineOptions>(
                options =>
                {
                    options.FileProviders.Add(new TemplateRepository(new SqlConnectionFactory(configuration)));
                });

        }
    }
}
 