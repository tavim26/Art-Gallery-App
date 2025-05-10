using SaleService.Domain;
using SaleService.Domain.Contracts;
using SaleService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace SaleService.Infrastructure
{
    public class SaleDAO : DbContext, ISaleDAO
    {
        public DbSet<SaleEntity> Sales { get; set; }

        public SaleDAO(DbContextOptions<SaleDAO> options)
            : base(options) { }


        public List<Sale> GetAllSales()
        {
            try
            {
                return Sales.Select(s => s.ToSale()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Sale>();
            }
        }

        public bool InsertSale(Sale sale)
        {
            if (sale == null)
                return false;
            try
            {
                Sales.Add(new SaleEntity(sale));
                return SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la inserare vanzare: {ex.Message}");
                return false;
            }
        }

        public double GetTotalSalesAmount()
        {
            try
            {
                if (Sales == null || !Sales.Any())
                    return 0.0;

                return Sales.Sum(s => s.Price);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la calcul total: {ex.Message}");
                return 0.0;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SaleEntity>().ToTable("SALE");
        }
    }
}