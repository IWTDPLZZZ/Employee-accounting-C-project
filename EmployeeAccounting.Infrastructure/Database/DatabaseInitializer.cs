using System.Text;
using EmployeeAccounting.Application.Interfaces;
using Npgsql;

namespace EmployeeAccounting.Infrastructure.Database;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly string _serverConnectionString;
    private readonly string _defaultConnectionString;
    private readonly string _databaseName;

    public DatabaseInitializer(string serverConnectionString, string defaultConnectionString)
    {
        _serverConnectionString = serverConnectionString;
        _defaultConnectionString = defaultConnectionString;
        _databaseName = new NpgsqlConnectionStringBuilder(defaultConnectionString).Database
            ?? "employee_accounting";
    }

    public void Initialize()
    {
        EnsureDatabaseExists();
        EnsureTablesExist();
        SeedAllDictionaries();
    }

    private void EnsureDatabaseExists()
    {
        using var connection = new NpgsqlConnection(_serverConnectionString);
        connection.Open();

        using (var check = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @dbName", connection))
        {
            check.Parameters.AddWithValue("dbName", _databaseName);
            if (check.ExecuteScalar() != null)
            {
                return;
            }
        }

        using var create = new NpgsqlCommand($"CREATE DATABASE \"{_databaseName}\"", connection);
        create.ExecuteNonQuery();
    }

    private void EnsureTablesExist()
    {
        using var connection = new NpgsqlConnection(_defaultConnectionString);
        connection.Open();

        using var cmd = new NpgsqlCommand(
            """
            CREATE TABLE IF NOT EXISTS departments (
                id SERIAL PRIMARY KEY,
                name TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS posts (
                id SERIAL PRIMARY KEY,
                name TEXT NOT NULL,
                description TEXT
            );

            CREATE TABLE IF NOT EXISTS cities (
                id SERIAL PRIMARY KEY,
                name TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS employees (
                id SERIAL PRIMARY KEY,
                name TEXT NOT NULL,
                surname TEXT NOT NULL,
                middle_name TEXT NOT NULL,
                birth_date DATE NOT NULL,
                birth_place_id INTEGER NOT NULL REFERENCES cities(id),
                phone TEXT NOT NULL,
                email TEXT NOT NULL,
                current_address TEXT NOT NULL,
                post_id INTEGER NOT NULL REFERENCES posts(id),
                department_id INTEGER NOT NULL REFERENCES departments(id),
                salary NUMERIC NOT NULL,
                etc_information TEXT,
                photo BYTEA,
                is_dismissed BOOLEAN NOT NULL DEFAULT FALSE
            );
            """,
            connection);
        cmd.ExecuteNonQuery();
    }

    private void SeedAllDictionaries()
    {
        using var connection = new NpgsqlConnection(_defaultConnectionString);
        connection.Open();

        SeedDepartments(connection);
        SeedPosts(connection);
        SeedCities(connection);
    }

    private static void SeedDepartments(NpgsqlConnection connection)
    {
        if (!IsTableEmpty(connection, "departments"))
        {
            return;
        }

        foreach (var name in ReadNonEmptyLines("department.csv"))
        {
            using var cmd = new NpgsqlCommand("INSERT INTO departments (name) VALUES (@name)", connection);
            cmd.Parameters.AddWithValue("name", name);
            cmd.ExecuteNonQuery();
        }
    }

    private static void SeedPosts(NpgsqlConnection connection)
    {
        if (!IsTableEmpty(connection, "posts"))
        {
            return;
        }

        foreach (var line in ReadNonEmptyLines("post.csv"))
        {
            var parts = line.Split(';', 2);
            var name = parts[0].Trim();
            var description = parts.Length > 1 ? parts[1].Trim() : string.Empty;

            using var cmd = new NpgsqlCommand(
                "INSERT INTO posts (name, description) VALUES (@name, @description)",
                connection);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("description", description);
            cmd.ExecuteNonQuery();
        }
    }

    private static void SeedCities(NpgsqlConnection connection)
    {
        if (!IsTableEmpty(connection, "cities"))
        {
            return;
        }

        var cities = File.ReadAllText(ResolveCsvPath("city.csv"), Encoding.UTF8)
            .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (var name in cities)
        {
            using var cmd = new NpgsqlCommand("INSERT INTO cities (name) VALUES (@name)", connection);
            cmd.Parameters.AddWithValue("name", name);
            cmd.ExecuteNonQuery();
        }
    }

    private static bool IsTableEmpty(NpgsqlConnection connection, string tableName)
    {
        using var cmd = new NpgsqlCommand($"SELECT COUNT(*) FROM {tableName}", connection);
        return Convert.ToInt64(cmd.ExecuteScalar()) == 0;
    }

    private static IEnumerable<string> ReadNonEmptyLines(string fileName)
    {
        return File.ReadAllLines(ResolveCsvPath(fileName), Encoding.UTF8)
            .Select(line => line.Trim())
            .Where(line => line.Length > 0);
    }

    private static string ResolveCsvPath(string fileName)
    {
        var path = Path.Combine(AppContext.BaseDirectory, "csv", fileName);
        if (File.Exists(path))
        {
            return path;
        }

        throw new FileNotFoundException($"Не найден файл справочника: {fileName}", path);
    }
}
