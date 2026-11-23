using EmployeeAccounting.Domain.Entities;

namespace EmployeeAccounting.Application.Interfaces;

public interface IDictionaryRepository
{
    List<Department> GetDepartments();
    List<Post> GetPosts();
    List<City> GetCities();
}
