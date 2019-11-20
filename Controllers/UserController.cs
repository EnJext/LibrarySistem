using System.Web.Security;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Filters;

namespace WebApplication3.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository Repository;

        public UserController()
        {
            Repository = StaticRepositories.UserRepository;
        }

        public ViewResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = Repository.Login(model.Name, model.Password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Невірно введений пароль або логін");
                }
            }
 
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = Repository.Register(model.Name, model.Password);

                if (user != null)
                {
                    Repository.Login(user.Name, user.Password);
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Користувач з таким іменем уже існує");
                }
            }

            return View(model);
        }

        [UserAuthenticationFilter]
        public ActionResult Logoff()
        {
            Repository.Logout();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}