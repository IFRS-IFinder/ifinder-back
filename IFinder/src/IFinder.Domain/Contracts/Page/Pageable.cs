namespace IFinder.Domain.Contracts.Page;

public class Pageable
{
    public int Page { get; set; } = 1;
    public int Take { get; set; } = 20;
    
    public int Offset() => (Page - 1) * Take;
}