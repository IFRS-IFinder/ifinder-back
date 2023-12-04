namespace IFinder.Application.Contracts.Documents.Dtos.User
{
    public class GetSimpleUserDto
    {
        public string Name { get; set; }
        public string? Sex { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }
        public string? Hobbies { get; set; }
        public bool isAuthor { get; set; }
    }
}

