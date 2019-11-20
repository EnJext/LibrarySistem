using System.Linq;
using System.Web.Mvc;
using WebApplication3.Models;
using System;
using WebApplication3.Filters;

namespace WebApplication3.Controllers
{
    [UserAuthenticationFilter]
    public class ReservationController : Controller
    {
        private IUserRepository Repository;
        public ReservationController() => Repository = StaticRepositories.UserRepository;

        public ViewResult Index() => View(Repository.User.Reservations.Where(r=>r.isValid));

        public ActionResult Reservation(int? BookId)
        {
            Book book = Repository.Books.FirstOrDefault(b => b.Id == BookId);
            if (book != null)
            {
                return View(new Reservation { Book = book});
            }
            return HttpNotFound("Книга відсутня");
        }

        [HttpPost] 
        public ActionResult Reservation(Reservation reservation)
        {
            reservation.Book = Repository.Books.FirstOrDefault(b => b.Id == reservation.BookId);

            if (reservation.StartReservation >= DateTime.Now.Date && reservation.isValid
                &&reservation.Book != null)
            {
                Repository.AddReservation(reservation);
                TempData["ReservationMessage"] = $"\"{reservation.Book.Name}\" - зарезервовано";
            }
            else
            {
                TempData["ReservationMessage"] = "Помилка: невірно вказані дані";
            }

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult CancelReservation(int ReservationId)
        {
            Repository.CancelReservation(ReservationId);
            TempData["ReservationMessage"] = "Резервування відмінено";

            return RedirectToAction("Index");
        }
    }
}