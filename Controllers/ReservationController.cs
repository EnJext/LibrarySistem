using System.Linq;
using System.Web.Mvc;
using WebApplication3.Models;
using System;
using WebApplication3.Filters;
using NLog;

namespace WebApplication3.Controllers
{
    [UserAuthenticationFilter]
    public class ReservationController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IUserRepository userRepository;
        public ReservationController(IUserRepository repository) => userRepository = repository;

        public ViewResult Index() => View(userRepository.GetAuthorizedUser(User).Reservations.Where(r=>r.isValid));

        public ActionResult Reservation(int? BookId)
        {
            Book book = userRepository.Books.FirstOrDefault(b => b.Id == BookId);
            if (book != null)
            {
                return View(new Reservation { Book = book});
            }
            return HttpNotFound("Книга відсутня");
        }

        [HttpPost] 
        public ActionResult Reservation(Reservation reservation)
        {
            reservation.Book = userRepository.Books.FirstOrDefault(b => b.Id == reservation.BookId);

            if (reservation.StartReservation >= DateTime.Now.Date && reservation.isValid
                &&reservation.Book != null)
            {
                userRepository.AddReservation(reservation);

                logger.Info($"Користувач {userRepository.GetAuthorizedUser(User)?.Name}: Зарезервував книгу \"{reservation.Book.Name}\"");

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
            Reservation reservation = userRepository.CancelReservation(ReservationId);

            if(reservation != null)
            {
                logger.Info($"Користувач {userRepository.GetAuthorizedUser(User)?.Name}: Відмінив резервування книги \"{reservation.Book.Name}\"");
                TempData["ReservationMessage"] = "Резервування відмінено";
            }

            return RedirectToAction("Index");
        }
    }
}