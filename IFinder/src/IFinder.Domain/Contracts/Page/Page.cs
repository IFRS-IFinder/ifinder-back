namespace IFinder.Domain.Contracts.Page;

public class Page<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalPages { get; private set; }
    public int TotalRegisters { get; private set; }
    public bool LastPage { get; private set; }

    public Page(IEnumerable<T> data, int totalPage, int totalRegisters, bool lastPage)
    {
        Data = data;
        TotalPages = totalPage;
        TotalRegisters = totalRegisters;
        LastPage = lastPage;
    }

    public Page<U> Map<U>(Func<T, U> func)
    {
         var mappedData = Data.Select(func).ToList();
         return new Page<U>(mappedData, TotalPages, TotalRegisters, LastPage);
    }
}