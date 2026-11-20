namespace EmployeeAccounting.Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public Post()
    {
    }

    public Post(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
