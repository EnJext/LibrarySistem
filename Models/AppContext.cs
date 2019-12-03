using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;

namespace WebApplication3.Models
{
    public class AppContext : DbContext
    {

        public AppContext() : base("LibraryContext") { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}