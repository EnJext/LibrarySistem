using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace WebApplication3.Models
{
    public static class StaticRepositories
    {
        public static IUserRepository UserRepository { get;}
        static StaticRepositories()
        {
            UserRepository = new UserRepository();
        }
    }

    public class UserRepository : IUserRepository
    {
        private AppContext context;
        public User User { get; set; }
        public bool IsAuthorized => User != null;

        public UserRepository()
        {
            context = new AppContext();
        }
        public IEnumerable<Book> Books => context.Books.Include(book=>book.Genres);
        public void Logout() => User = null;
        public User Login(string Name, string password)
        {
            User = context.Users.FirstOrDefault(u => u.Name == Name && u.Password == password);

            if(User != null)
            {
                context.Entry(User).Collection(s => s.Reservations).Load();
            }
            return User;
        }

        public User Register(string Name, string Password)
        {
           if (!context.Users.Any(usr=>usr.Name==Name))
            {
                User user = new User { Name = Name, Password = Password };
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
            return null;
        }

        public void AddReservation(Reservation reservation)
        {
            if(reservation.Id == 0)
            {
                Reservation updateReservarion  = context.Reservations.FirstOrDefault(res=> res.BookId == reservation.BookId);

                if (updateReservarion != null)
                {
                    updateReservarion.FinishReservation = reservation.FinishReservation;
                    updateReservarion.StartReservation = reservation.StartReservation;
                }
                else {
                    reservation.UserId = User.Id;
                    context.Reservations.Add(reservation);
                    User.Reservations.Add(reservation);
                }
                context.SaveChanges();
            }
        }

        public Reservation CancelReservation(int ReservationId)
        {
            Reservation toCancel =
                context.Reservations.FirstOrDefault(resv => resv.Id == ReservationId);

            if(toCancel != null)
            {
                toCancel.FinishReservation = DateTime.Now;
                context.SaveChanges();
            }
            return toCancel;
        }
    }
}