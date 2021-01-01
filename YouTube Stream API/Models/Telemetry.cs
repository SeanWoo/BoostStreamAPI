using System;

namespace YouTube_Stream_API.Models
{
    public class Telemetry
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string IP { get; set; }
        public string Login { get; set; }
        public string Message { get; set; }

        public Telemetry() { }
        public Telemetry(DateTime date, string ip, string login, string message)
        {
            Date = date;
            IP = ip;
            Login = login;
            Message = message;
        }
    }
}