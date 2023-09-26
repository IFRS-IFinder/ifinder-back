namespace IFinder.Application.Contracts.Documents.Requests.User
{
    public class LoginUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}