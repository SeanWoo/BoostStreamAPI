using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouTube_Stream_API.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Access { get; set; }

        public User() { }
        public User(string name, string password, string access)
        {
            Name = name;
            Password = password;
            Access = access;
        }
    }
}
