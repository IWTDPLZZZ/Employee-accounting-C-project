namespace EmployeeAccounting.Domain.Entities;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;

    public City()
    {
    }

    public City(int id, string name, string region)
    {
        Id = id;
        Name = name;
        Region = region;
    }
}
