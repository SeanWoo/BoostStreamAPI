using Microsoft.AspNetCore.Identity;
using System;

namespace BoostStreamServer.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Token Token { get; set; }
        public DateTime RegistrationAt { get; set; }
        public bool Banned { get; set; }
    }
}
