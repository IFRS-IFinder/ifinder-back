namespace IFinder.Application.Contracts.Documents.Dtos
{
    public class GetCompleteUserDto
    {
        public string? Email { get; set; }
        public string? Sex { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }
        public string? Hobbies { get; set; }
        public bool isAuthor { get; set; }
    }
}

