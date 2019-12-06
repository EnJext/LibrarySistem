using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;

namespace WebApplication3.Models
{
    public class UserRepository : IUserRepository
    {
        private AppContext context;
        private User User;
        public bool IsAuthorized => User != null;

        public UserRepository() => context = new StandardKernel().Get<AppContext>();
        public UserRepository(AppContext context)=>this.context = context;
        public IEnumerable<Book> Books => context.Books.Include(book=>book.Genres);//перенести в другой репозиторий 
        public void Logout() => User = null; //удалить
        public User Login(string Name, string password)
        {
            User = context.Users.FirstOrDefault(u => u.Name == Name && u.Password == password);
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
                Reservation updateReservarion  = User?.Reservations?.FirstOrDefault(res=> res.BookId == reservation.BookId);

                if (updateReservarion != null)
                {
                    updateReservarion.FinishReservation = reservation.FinishReservation;
                    updateReservarion.StartReservation = reservation.StartReservation;
                }
                else {
                    reservation.UserId = User.Id;
                    context.Reservations.Add(reservation);
                }
                context.SaveChanges();
            }
        }

        public Reservation CancelReservation(int ReservationId)
        {
            Reservation toCancel = 
                User?.Reservations?.FirstOrDefault(resv => resv.Id == ReservationId);

            if(toCancel != null)
            {
                toCancel.FinishReservation = DateTime.Now;
                context.SaveChanges();
            }
            return toCancel;
        }
        public User GetAuthorizedUser(IPrincipal user)
        {
            User = context.Users.FirstOrDefault(u => u.Name == user.Identity.Name);
            if(IsAuthorized)context.Entry(User).Collection(s => s.Reservations).Load();
            return User;
        }
    }
}