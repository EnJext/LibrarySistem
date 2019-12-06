using System;
using System.Collections.Generic;


namespace WebApplication3.Models
{
    public class Book
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public ICollection<Genre> Genres { get; set;}
        public string Author { get; set;}
        public DateTime Date { get; set;}   
    }

    public class Genre
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public ICollection<Book> Books { get; set;}
        public override string ToString() => Name;
    }
}


