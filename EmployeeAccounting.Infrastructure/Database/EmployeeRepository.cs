using Npgsql;
namespace EmployeeAccounting.Infrastructure.Database;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly string _connectionString;

    public EmployeeRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Employee> GetFiltered(Employee filter)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();

        var sqlCommandText = @"
    SELECT
        e.id,
        e.surname || ' ' || e.name || ' ' || e.middle_name AS full_name,
        e.birth_date,
        c.name AS birth_place,
        p.name AS post_name,
        d.name AS department_name,
        e.salary,
        e.is_dismissed
    FROM employees e
    JOIN cities c ON e.birth_place_id = c.id
    JOIN posts p ON e.post_id = p.id
    JOIN departments d ON e.department_id = d.id
    WHERE 1=1" ;

    }


}
