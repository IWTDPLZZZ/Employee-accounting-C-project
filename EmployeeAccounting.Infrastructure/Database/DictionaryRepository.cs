using EmployeeAccounting.Application.Interfaces;
using EmployeeAccounting.Domain.Entities;
using Npgsql;

namespace EmployeeAccounting.Infrastructure.Database;

public class DictionaryRepository : IDictionaryRepository
{
    private readonly string _connectionString;

    public DictionaryRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Department> GetDepartments()
    {
        var departments = new List<Department>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT id, name FROM departments ORDER BY id", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            departments.Add(new Department(id, name));
        }
        return departments;
    }
    public List<Post> GetPosts()
    {
        var posts = new List<Post>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT id, name, description FROM posts ORDER BY id", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string description = reader.GetString(2);
            posts.Add(new Post(id, name, description));
        }
        return posts;
    }
    public List<City> GetCities()
    {
        var cities = new List<City>();
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand("SELECT id, name FROM cities ORDER BY id", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string region = reader.GetString(2);
            cities.Add(new City(id, name, region));
        }
        return cities;
    }
}
