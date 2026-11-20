using EmployeeAccounting.Application.Interfaces;
using EmployeeAccounting.Domain.Entities;

namespace EmployeeAccounting.Application.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employees;
    private readonly EmployeeValidator _validator;

    public EmployeeService(IEmployeeRepository employees, EmployeeValidator validator)
    {
        _employees = employees;
        _validator = validator;
    }

    public IReadOnlyList<string> Create(Employee employee)
    {
        var errors = _validator.Validate(employee);
        if (errors.Count > 0)
        {
            return errors;
        }

        _employees.Create(employee);
        return Array.Empty<string>();
    }
}
