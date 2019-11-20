using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApplication3.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public List<Reservation> Reservations { get; set; }
    }
}