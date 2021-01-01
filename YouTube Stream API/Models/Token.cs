namespace YouTube_Stream_API.Models
{
    public class Token
    {
        public int ID { get; set; }
        public string Chars { get; set; }
        public bool Active { get; set; }
        public string Access { get; set; }

        public Token() { }
        public Token(string token, string access, bool active = true)
        {
            Chars = token;
            Access = access;
            Active = active;
        }
    }
}