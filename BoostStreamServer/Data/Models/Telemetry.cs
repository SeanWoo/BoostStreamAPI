using System;

namespace BoostStreamServer.Data.Models
{
    public class Telemetry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string IP { get; set; }
        public string Login { get; set; }
        public string Message { get; set; }
    }
}