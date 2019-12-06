using System.Web.Security;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Filters;
using System.Linq;
using NLog;


namespace WebApplication3.Controllers
{
    public class UserController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private IUserRepository userRepository;

        public UserController(IUserRepository repository) => userRepository = repository;

        public ViewResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = userRepository.Login(model.Name, model.Password);

                if (user != null)
                {           
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    logger.Info($"Користувач {userRepository.GetAuthorizedUser(User)?.Name}: Увійшов в систему");                
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
                User user = userRepository.Register(model.Name, model.Password);

                if (user != null)
                {
                    userRepository.Login(user.Name, user.Password);
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    logger.Info($"Користувач {userRepository.GetAuthorizedUser(User)?.Name}: Зареєструвався");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Name), "Користувач з таким іменем уже існує");
                }
            }

            return View(model);
        }

        [UserAuthenticationFilter]
        public ActionResult Logoff()
        {
            logger.Info($"Користувач {userRepository.GetAuthorizedUser(User)?.Name}: Вийшов з системи");
            userRepository.Logout();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public PartialViewResult Menu() => PartialView(new UserMenuModel
        {
            CountOfReservedBooks = userRepository.GetAuthorizedUser(User)?.Reservations?.Count(resv => resv.isValid),
            IsAuthorized = userRepository.IsAuthorized,
            UserName = userRepository.GetAuthorizedUser(User)?.Name
        });

    }
}