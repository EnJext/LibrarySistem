using System.Linq;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Filters;
using System.Collections.Generic;
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

        public ViewResult Error() => View("Error");
    }
}