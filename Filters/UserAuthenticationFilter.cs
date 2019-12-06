using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace WebApplication3.Filters
{
    public class UserAuthenticationFilter :  FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if(user == null || !user.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary {
                        { "controller", "User" }, { "action", "Login" }
                });
            }
        }
    }
}