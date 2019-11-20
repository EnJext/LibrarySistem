using System;

namespace WebApplication3.Models
{
    public class SearchModel
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set;}
        public DateTime FromDate { get; set;}
        public DateTime UntilDate { get; set;}
    }
}