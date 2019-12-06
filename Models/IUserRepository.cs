using System.Collections.Generic;
using System.Security.Principal;

namespace WebApplication3.Models
{
    public interface IUserRepository
    {
        User GetAuthorizedUser(IPrincipal user);
        bool IsAuthorized { get; }
        IEnumerable<Book> Books { get; }
        void Logout();
        User Login(string Name, string password);
        User Register(string Name, string Password);
        void AddReservation(Reservation reservation);
        Reservation CancelReservation(int ReservationId);
    }
}
