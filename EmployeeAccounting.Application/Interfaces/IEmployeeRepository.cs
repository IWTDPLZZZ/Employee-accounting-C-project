using EmployeeAccounting.Domain.Entities;

namespace EmployeeAccounting.Application.Interfaces;

public interface IEmployeeRepository
{
    List<Employee> GetFiltered(Employee filter);
    Employee? GetById(int id);
    int Create(Employee employee);
    void Update(Employee employee);
    void Delete(int id);
    void Dismiss(int id);
}
