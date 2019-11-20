using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication3.Models
{
    public static class StaticRepositories
    {
        public static IUserRepository UserRepository { get;}
        static StaticRepositories()
        {
            UserRepository = new UserRepository(AppDomain.CurrentDomain.BaseDirectory + "App_Data/JsonStorage.json");
        }
    }


    public class UserRepository : IUserRepository
    {
        private JsonContext jsonContext;
        public User User { get; set; }
        public bool IsAuthorized => User != null;

        public UserRepository(string JsonFileName)
        {
            jsonContext = new JsonContext(JsonFileName);
        }
        public IEnumerable<Book> Books => jsonContext.Books;
        public void Logout() => User = null;
        public User Login(string Name, string password)
        {
            User = jsonContext.Users.FirstOrDefault(u => u.Name == Name && u.Password == password);

            if(User != null)
            {
                User.Reservations = jsonContext.Reservations.Where(resv => resv.UserId == User.Id).ToList();
                User.Reservations.ForEach(resv => resv.Book = Books.FirstOrDefault(b => b.Id == resv.BookId)); 
            }
            return User;
        }

        public User Register(string Name, string Password)
        {
           if (!jsonContext.Users.Any(usr=>usr.Name==Name))
            {
                int newId = jsonContext.Users.Count() + 1;
                while (jsonContext.Users.Any(usr => usr.Id == newId)) newId++;
                User user = new User
                {
                    Id = newId,
                    Name = Name,
                    Password = Password
                };

                jsonContext.Users.Add(user);
                jsonContext.SaveChanges();
                return user;
            }
            return null;
        }

        public void AddReservation(Reservation reservation)
        {
            if(reservation.Id == 0)
            {
                int newId = jsonContext.Reservations.Count()+1;
                while (jsonContext.Reservations.Any(resv => resv.Id == newId)) newId++;
                reservation.Id = newId;
                reservation.UserId = User.Id;

                jsonContext.Reservations.Add(reservation);
                User.Reservations.Add(reservation);

                jsonContext.SaveChanges();
            }
        }

        public Reservation CancelReservation(int ReservationId)
        {
            Reservation toCancel =
                jsonContext.Reservations.FirstOrDefault(resv => resv.Id == ReservationId);

            if(toCancel != null)
            {
                toCancel.FinishReservation = DateTime.Now;
            }

            return toCancel;
        }
    }
}