namespace API.Models.Requests
{
    public class AuthorizeCard
    {
        public string Number { get; set; }
        public int PIN { get; set; }
    }
}
