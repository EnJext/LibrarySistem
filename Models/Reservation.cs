using System;
using Newtonsoft.Json;

namespace WebApplication3.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        [JsonIgnore]
        public bool isValid => FinishReservation.Date > DateTime.Now.Date;
        public DateTime StartReservation { get; set; }
        public DateTime FinishReservation { get; set; }

       
    }
}