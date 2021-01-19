using System;

namespace BoostStreamServer.Data.Models
{
    public class Token
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public string Access { get; set; }
    }
}