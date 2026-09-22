using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Text.Json;
using WebArMa.ArMaMelk.Persistence.Contexts;
using WebArMa.ArMaMelk.Persistence.Seeds.DTOs;

namespace WebArMa.ArMaMelk.Persistence.Seeds
{
    public sealed class CitySeeder(IConfiguration configuration, DatabaseContext databaseContext)
    {
        public string Path = configuration["Seeds:City"]!;

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            if (!File.Exists(Path))
            {
                return;
            }

            if (await databaseContext.Cities.AnyAsync(cancellationToken))
            {
                return;
            }

            var json = await File.ReadAllTextAsync(Path, cancellationToken);

            var items = JsonSerializer.Deserialize<List<CitySeedDTO>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (items is null || items.Count == 0)
            {
                return;
            }

            await using var connection = new NpgsqlConnection(databaseContext.Database.GetConnectionString());

            await connection.OpenAsync(cancellationToken);

            await using var importer = connection.BeginBinaryImport("""COPY "Cities" ("Id", "Name", "ProvinceId")FROM STDIN (FORMAT BINARY)""");

            foreach (var item in items)
            {
                await importer.StartRowAsync(cancellationToken);
                await importer.WriteAsync(int.Parse(item.CityId), NpgsqlTypes.NpgsqlDbType.Integer, cancellationToken);
                await importer.WriteAsync(item.CityName, NpgsqlTypes.NpgsqlDbType.Text, cancellationToken);
                await importer.WriteAsync(int.Parse(item.ProvinceId), NpgsqlTypes.NpgsqlDbType.Integer, cancellationToken);
            }

            await importer.CompleteAsync(cancellationToken);
        }
    }
}
