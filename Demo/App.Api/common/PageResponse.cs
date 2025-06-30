namespace App.Api.common;

public class PageResponse<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public class PageData<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalItems { get; set; }
}
