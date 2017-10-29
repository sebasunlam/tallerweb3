using System.Text;
using System.Web;
using System.Web.Mvc;
using ProgramacionWeb3.WebApp.Models;

namespace ProgramacionWeb3.WebApp.Extensions
{
    public static class PagingHelper
    {
        /// <summary>
        /// El helper utiliza bootsrap para dibujarse, ocupa un div de col-7
        /// </summary>
        /// <param name="html">Objeto helper</param>
        /// <param name="model">Modelo que implemente IPagedModel</param>
        /// <param name="actionName">Action al que debe ser redirigido</param>
        /// <param name="controllerName">Controller al que se debe redirigir</param>
        /// <returns></returns>
        public static IHtmlString Pager<T>(this HtmlHelper html, IPagedModel<T> model, string actionName, string controllerName)
        {
            var sb = new StringBuilder();

            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            sb.Append(@"<div class='col-7'><nav aria-label='Pager'><ul class='pagination'>");

            if (model.CurrentPage != 1)
            {
                sb.Append(
                    $"<li class='page-item'><a class='page-link' href='{urlHelper.Action(actionName, controllerName, new { page = model.CurrentPage - 1, pageSize = model.PageSize })}'>Anterior</a></li>");
            }
            if (model.CurrentPage - 5 > 1)
                sb.Append(
                    $"<li class='page-item'><a class='page-link href='{urlHelper.Action(actionName, controllerName, new { page = model.CurrentPage - 5, pageSize = model.PageSize })}'>-5</a></li>");
            if (model.TotalPages > 5)
            {
                for (var i = model.CurrentPage; i <= model.CurrentPage + 5; i++)
                {
                    if (model.CurrentPage == i)
                    {
                        sb.Append(
                            $"<li class='page-item active'><a href='{urlHelper.Action(actionName, controllerName, new { page = i, pageSize = model.PageSize })}' class='page-link'>{i}</a></li>");
                    }
                    else
                    {
                        sb.Append(
                            $"<li class='page-item'><a href='{urlHelper.Action(actionName, controllerName, new { page = i, pageSize = model.PageSize })}' class='page-link'>{i}</a></li>");
                    }
                }
            }
            else
            {
                for (var i = 1; i <= model.TotalPages; i++)
                {
                    if (model.CurrentPage == i)
                    {
                        sb.Append(
                            $"<li class='page-item active'><a href='{urlHelper.Action(actionName, controllerName, new { page = i, pageSize = model.PageSize })}' class='page-link'>{i}</a></li>");
                    }
                    else
                    {
                        sb.Append(
                            $"<li class='page-item'><a href='{urlHelper.Action(actionName, controllerName, new { page = i, pageSize = model.PageSize })}' class='page-link'>{i}</a></li>");
                    }
                }
            }

            if (model.CurrentPage + 5 < model.TotalPages)
                sb.Append($"<li class='page-item'><a class='page-link' href='{urlHelper.Action(actionName, controllerName, new { page = model.CurrentPage + 5, pageSize = model.PageSize })}'>+5</a></li>");
            if (model.CurrentPage != model.TotalPages)
                sb.Append(
                    $"<li class='page-item'><a class='page-link' href='{urlHelper.Action(actionName, controllerName, new { page = model.CurrentPage + 1, pageSize = model.PageSize })}'>Siguiente</a></li>");


            sb.Append(
                "</ul></nav></div>");

           
            return new MvcHtmlString(sb.ToString());
        }
    }
}