using Npgsql;

namespace EmployeeAccounting.Infrastructure.Database;

public static class Postgres
{
    public static async Task<bool> CanConnectAsync(string connectionString, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection.State == System.Data.ConnectionState.Open;
    }
}
