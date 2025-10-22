using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hotel_3.EntityFramework;

internal class HotelDbContextFactory : IDesignTimeDbContextFactory<HotelDbContext>
{
	public HotelDbContext CreateDbContext(string[]? args = null)
	{
		var connectionString = GetConnectionString();
			
		if(connectionString == null)
		{
			throw new InvalidOperationException("DB_CONNECTION_STRING not found in .env");
		}
			
		var options = new DbContextOptionsBuilder<HotelDbContext>();
		options.UseSqlServer(connectionString)
			.UseLazyLoadingProxies();

		return new HotelDbContext(options.Options);
	}

	private string? GetConnectionString()
	{
		var envPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\.env"));

		if (File.Exists(envPath))
			Env.Load(envPath);
		else
			throw new InvalidOperationException("File .env not found");

		return Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
	}
}