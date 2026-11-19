namespace EmployeeAccounting.Domain.Entities;

public class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;        // Имя
    public string Surname { get; set; } = string.Empty;     // Фамилия
    public string MiddleName { get; set; } = string.Empty;  // Отчество
    public DateTime BirthDate { get; set; }                 // Дата рождения

    public int BirthPlaceId { get; set; }
    public City? BirthPlace { get; set; }

    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CurrentAddress { get; set; } = string.Empty;

    public int PostId { get; set; }
    public Post? Post { get; set; }

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public decimal Salary { get; set; }
    public string? EtcInformation { get; set; }

    public byte[]? Photo { get; set; }
    public bool IsDismissed { get; set; }

    public Employee() { }

    public Employee(
        string name,
        string surname,
        string middleName,
        DateTime birthDate,
        int birthPlaceId,
        string phone,
        string email,
        string currentAddress,
        int postId,
        int departmentId,
        decimal salary,
        string? etcInformation = null,
        byte[]? photo = null,
        bool isDismissed = false)
    {
        Name = name;
        Surname = surname;
        MiddleName = middleName;
        BirthDate = birthDate;
        BirthPlaceId = birthPlaceId;
        Phone = phone;
        Email = email;
        CurrentAddress = currentAddress;
        PostId = postId;
        DepartmentId = departmentId;
        Salary = salary;
        EtcInformation = etcInformation;
        Photo = photo;
        IsDismissed = isDismissed;
    }
}
