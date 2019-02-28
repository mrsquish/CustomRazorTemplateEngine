using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Custom.TemplateEngine.Derived
{
    public class TemplateNameExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {            
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return viewLocations;
        }
    }
}
