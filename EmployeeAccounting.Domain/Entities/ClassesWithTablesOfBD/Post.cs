namespace EmployeeAccounting.Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    Post(string name, int id, string description)
    {
        Name = name;
        Id = id;
        Description = description;
    }
}
