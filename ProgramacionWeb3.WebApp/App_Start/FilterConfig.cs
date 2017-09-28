using System.Web;
using System.Web.Mvc;
using ProgramacionWeb3.WebApp.Filters;

namespace ProgramacionWeb3.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LayoutFilter());
        }
    }
}
