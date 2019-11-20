using System.Web.Mvc;
using System.Web.Mvc.Filters;
using WebApplication3.Models;
using System.Web.Security;

namespace WebApplication3.Filters
{
    public class UserAuthenticationFilter :  FilterAttribute, IAuthenticationFilter
    {
        private IUserRepository repository;
        public UserAuthenticationFilter() => repository = StaticRepositories.UserRepository;

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (!repository.IsAuthorized) FormsAuthentication.SignOut();

            if (user == null || !user.Identity.IsAuthenticated || !repository.IsAuthorized)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var user = filterContext.HttpContext.User;

            if (user == null || !user.Identity.IsAuthenticated || !repository.IsAuthorized)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                    { "controller", "User" }, { "action", "Login" }
                   });
            }
        }
    }
}