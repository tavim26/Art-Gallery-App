using SaleService.Domain;
using SaleService.Domain.Contracts;
using SaleService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace SaleService.Infrastructure
{
    public class SaleDAO : DbContext, ISaleDAO
    {
        private DbSet<SaleEntity> _salesSet { get; set; }

        public SaleDAO(DbContextOptions<SaleDAO> options)
            : base(options) { }

        public bool InsertSale(Sale sale)
        {
            if (sale == null)
                return false;
            try
            {
                _salesSet.Add(new SaleEntity(sale));
                return SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }


        public double GetTotalSalesAmount()
        {
            try
            {
                if (_salesSet == null || !_salesSet.Any())
                    return 0.0;

                return _salesSet.Sum(s => s.Price);
            }
            catch
            {
                return 0.0;
            }
        }
    }
}
