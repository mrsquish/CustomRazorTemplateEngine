using System;
using System.IO;
using System.Threading.Tasks;
using Custom.TemplateEngine.Derived;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace Custom.TemplateEngine
{
    public class TemplateEngine
    {
        private readonly IRazorViewEngine _engine;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly TemplateRazorEngine _myEngine;


        public TemplateEngine(
            IRazorViewEngine engine,
            TemplateRazorEngine myEngine,
            IRazorPageFactoryProvider factoryProvider,
            IRazorPageActivator razorPageActivator,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _engine = engine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _myEngine = myEngine;
        }

        public async Task<string> RenderViewToString<TModel>(string name, TModel model)
        {
            var actionContext = GetActionContext();

            var viewEngineResult2 = _engine.FindView(actionContext, name, false);
            var viewEngineResult = _myEngine.FindView(actionContext, name, false);

            if (!viewEngineResult.Success) throw new InvalidOperationException($"Couldn't find view '{name}'");

            var view = viewEngineResult.View;

            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    new ViewDataDictionary<TModel>(
                        new EmptyModelMetadataProvider(),
                        new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(
                        actionContext.HttpContext,
                        _tempDataProvider),
                    output,
                    new HtmlHelperOptions());

                await view.RenderAsync(viewContext);

                return output.ToString();
            }
        }

        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };

            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
    }
}