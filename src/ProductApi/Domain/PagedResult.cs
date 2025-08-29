namespace ProductApi.Domain;

public class PagedResult<T> 
        where T : class
{
    public List<T> Items { get; set; } = new List<T>();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int PageCount { get; set; }
    public int TotalCount { get; set; }
}