using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public interface IUserRepository
    {
        User User { get;}
        bool IsAuthorized { get; }
        IEnumerable<Book> Books { get; }
        void Logout();
        User Login(string Name, string password);
        User Register(string Name, string Password);
        void AddReservation(Reservation reservation);
        Reservation CancelReservation(int ReservationId);
    }
}
