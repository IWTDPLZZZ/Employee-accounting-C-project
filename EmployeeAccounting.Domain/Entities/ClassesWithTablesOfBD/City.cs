namespace EmployeeAccounting.Domain.Entities;
public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Region {get ; set;}

    City(string name, int id, string region)
    {
        Name = name;
        Id = id;
        Region = region;
    }
}
