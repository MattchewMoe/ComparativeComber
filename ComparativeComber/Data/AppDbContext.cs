using System.Diagnostics;
using ComparativeComber.Data;
using ComparativeComber.Entities;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.InkML;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using ComparativeComber.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ComparativeComber.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {

        public DbSet<ComparableSale> ComparableSales { get; set; }

        public DbSet<Organization> Organizations { get; set; }



        // New DbSet for ComparableSaleSearchable
        public async Task<List<ComparableSale>> GetComparableSalesWithinDistanceAsync(double longitude, double latitude, double distanceMiles, CancellationToken cancellationToken)
        {
            // Prepare parameters for the stored procedure
            var longitudeParam = new SqlParameter("@Longitude", longitude);
            var latitudeParam = new SqlParameter("@Latitude", latitude);
            var distanceMilesParam = new SqlParameter("@DistanceMiles", distanceMiles);

            // Call the stored procedure
            return await ComparableSales.FromSqlRaw(
                "EXEC dbo.GetComparableSalesWithinDistance @Longitude, @Latitude, @DistanceMiles",
                longitudeParam, latitudeParam, distanceMilesParam
            ).ToListAsync(cancellationToken);
        }







        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>().HasData(
                 new Organization { OrganizationId = 1, Name = "Example Organization", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
               );

            // Conversion for ImageUrls in ComparableSale
            modelBuilder.Entity<ComparableSale>()
                .Property(e => e.ImageUrls)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            // Conversion for RenovationYears in ComparableSaleSearchable
            modelBuilder.Entity<ComparableSale>()
                .Property(e => e.RenovationYears)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                           .Select(int.Parse).ToList()
                );

            // Value conversion for the List<string> in ComparableSale
            modelBuilder.Entity<ComparableSale>()
                .Property(e => e.ImageUrls)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                );

            modelBuilder
             .Entity<ComparableSale>()
             .Property(e => e.ImageUrls)
             .HasConversion(
                  v => string.Join(',', v),
                  v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
            // User -> Organization
       
           






        }
    }
}