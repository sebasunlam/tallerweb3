using System.Web.Mvc;

namespace ProgramacionWeb3.WebApp.Filters
{
    public class LayoutFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!(filterContext.Result is ViewResult viewResult)) return;

            viewResult.MasterName = "_Layout";

            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                viewResult.MasterName = filterContext.HttpContext.User.IsInRole("Administrador") ? "_LayoutAdmin" : "_LayoutUser";
            }
        }
    }
}