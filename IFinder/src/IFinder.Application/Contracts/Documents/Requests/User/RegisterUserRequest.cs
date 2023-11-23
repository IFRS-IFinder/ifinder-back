namespace IFinder.Application.Contracts.Documents.Requests.User
{
    public class RegisterUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public string Hobbies { get; set; }
    }
}
