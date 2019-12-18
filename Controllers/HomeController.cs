using System.Linq;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Filters;
using System.Collections.Generic;
using System.Web;
using System;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository Repository;
        public HomeController()=>Repository = StaticRepositories.UserRepository;

        public ViewResult Index()=>View(Repository.Books.Take(7));

        [UserAuthenticationFilter]
        public ViewResult History() => View(Repository.User.Reservations.Where(r => !r.isValid));

        public ActionResult Search(SearchModel model)
        {
            IEnumerable<Book> ResultBooks = Repository.Books
            .Where(b => (model.Name == null) ? true : (b.Name == model.Name))
            .Where(b => (model.Author == null) ? true : (b.Author == model.Author))
            .Where(b => (model.Genre == null) ? true : (b.Genres.Any(genre=>genre.Name == model.Genre)))
            .Where(b => ((b.Date.Date >= model.FromDate) && (model.UntilDate.Date == DateTime.MinValue.Date ? true : b.Date.Date <= model.UntilDate.Date)))
            .Take(7);
            return View("Index", ResultBooks);
        }

        public ActionResult ChangeCulture(string lang, string returnUrl=null)
        {
            List<string> cultures = new List<string> { "uk", "en" };

            if(!cultures.Contains(lang))
            {
                lang = "uk";
            }

            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;   
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }

        public ViewResult Error() => View("Error");
    }
}