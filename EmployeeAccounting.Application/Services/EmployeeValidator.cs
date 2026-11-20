using System.Text.RegularExpressions;
using EmployeeAccounting.Domain.Entities;

namespace EmployeeAccounting.Application.Services;

public class EmployeeValidator
{
    private const int MaxPhotoSizeBytes = 10 * 1024 * 1024;

    public List<string> Validate(Employee employee)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(employee.Name))
        {
            errors.Add("Name is required");
        }

        if (string.IsNullOrWhiteSpace(employee.Surname))
        {
            errors.Add("Surname is required");
        }

        if (string.IsNullOrWhiteSpace(employee.MiddleName))
        {
            errors.Add("MiddleName is required");
        }

        if (employee.BirthPlaceId == 0)
        {
            errors.Add("BirthPlaceId is required");
        }

        if (string.IsNullOrWhiteSpace(employee.Phone))
        {
            errors.Add("Phone is required");
        }

        if (string.IsNullOrWhiteSpace(employee.Email))
        {
            errors.Add("Email is required");
        }

        if (string.IsNullOrWhiteSpace(employee.CurrentAddress))
        {
            errors.Add("Current address is required");
        }

        if (!string.IsNullOrWhiteSpace(employee.Phone) &&
            !Regex.IsMatch(employee.Phone, @"^\+?[0-9\s\-\(\)]{7,20}$"))
        {
            errors.Add("Некорректный формат телефонного номера.");
        }

        if (!string.IsNullOrWhiteSpace(employee.Email) &&
            !Regex.IsMatch(employee.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            errors.Add("Некорректный формат Email.");
        }

        if (employee.Photo != null && employee.Photo.Length > MaxPhotoSizeBytes)
        {
            errors.Add("Размер фотографии превышает 10 МБ.");
        }

        return errors;
    }
}
