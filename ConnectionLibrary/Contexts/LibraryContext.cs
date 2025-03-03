using ConnectionLibrary.Objects.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConnectionLibrary.Contexts
{
    public class LibraryContext : DbContext
    {
        public IConfiguration Configuration { get; }

        [LastModified("06-11-2024")]
        public LibraryContext()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        #region [Configurations]

        [LastModified("06-11-2024")]
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, options =>
                {
                    options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            }
        }

        #endregion

    }
}
