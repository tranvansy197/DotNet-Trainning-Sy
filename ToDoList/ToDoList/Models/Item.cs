namespace ToDoList.Models;

public class Item
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
}