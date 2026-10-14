using Microsoft.Extensions.Configuration;
using Npgsql;
using PeopleResourceManagement.Application.Interfaces;

namespace PeopleResourceManagement.Infrastructure.Services;

public class PostgresDistributedLockService : IDistributedLockService, IAsyncDisposable
{
    private readonly string _connectionString;
    private NpgsqlConnection? _connection;

    public PostgresDistributedLockService(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "DefaultConnection connection string was not found.");
    }

    public async Task<bool> TryAcquireAsync(
        long lockKey,
        CancellationToken cancellationToken = default)
    {
        _connection = new NpgsqlConnection(_connectionString);

        await _connection.OpenAsync(cancellationToken);

        await using var command = new NpgsqlCommand(
            "SELECT pg_try_advisory_lock(@lockKey);",
            _connection);

        command.Parameters.AddWithValue("lockKey", lockKey);

        var result = await command.ExecuteScalarAsync(cancellationToken);

        return result is true;
    }

    public async Task ReleaseAsync(
        long lockKey,
        CancellationToken cancellationToken = default)
    {
        if (_connection is null)
        {
            return;
        }

        await using var command = new NpgsqlCommand(
            "SELECT pg_advisory_unlock(@lockKey);",
            _connection);

        command.Parameters.AddWithValue("lockKey", lockKey);

        await command.ExecuteScalarAsync(cancellationToken);

        await _connection.CloseAsync();

        await _connection.DisposeAsync();

        _connection = null;
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection is not null)
        {
            await _connection.DisposeAsync();
        }
    }
}