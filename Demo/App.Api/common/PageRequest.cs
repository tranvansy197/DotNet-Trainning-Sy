using System.ComponentModel.DataAnnotations;

namespace App.Api.common;

public class PageRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}