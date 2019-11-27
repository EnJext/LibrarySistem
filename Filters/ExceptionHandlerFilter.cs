using System.Web.Mvc;
using System.Web.Mvc.Filters;
using WebApplication3.Models;
using NLog;

namespace WebApplication3.Filters
{
    
    public class ExceptionHandlerFilter :FilterAttribute, IExceptionFilter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext filterContext)
        {
            logger.Error(filterContext.Exception,
                $"Виникла виняткова ситуація: {filterContext.Exception.Message} по запиту {filterContext.RequestContext.RouteData.ToString()}");

           filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                    { "controller", "Home" }, { "action", "Error" }, {"message", "Виникла помилка"}
                   });
            filterContext.ExceptionHandled = true;
        }
    }
}