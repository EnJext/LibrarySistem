using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

namespace WebApplication3.Models
{
    public class JsonContext
    {
        [JsonIgnore]
        private JsonSerializer serializer;

        [JsonIgnore]
        private string StorageName;

        public JsonContext() { }
        public JsonContext(string JsonFileName)
        {
            StorageName = JsonFileName;
            serializer = new JsonSerializer();

            using (StreamReader sr = new StreamReader(StorageName))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonContext tempRepository = serializer.Deserialize<JsonContext>(reader);

                this.Books = tempRepository.Books ?? new List<Book>();
                this.Reservations = tempRepository.Reservations ?? new List<Reservation>();
                this.Users = tempRepository.Users ?? new List<User>();
            }
        }

        public void SaveChanges()
        {
            using(StreamWriter sw = new StreamWriter(StorageName))
            using(JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }

        public List<Book> Books { get; set;}
        public List<Reservation> Reservations { get; set;}
        public List<User> Users { get; set;}

    }
}