using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TestNinja.Housekeeping
{
    public interface IHouseKeeperRepository
    {
        IQueryable<Housekeeper> GetHousekeepers();
    }

    public class HouseKeeperRepository : IHouseKeeperRepository
    {
        public IQueryable<Housekeeper> GetHousekeepers()
        {
            using (var context = new HousekeeperContext())
            {
                return context.Housekeepers;
            }
        }
    }

    class HousekeeperContext : DbContext
    {
        public DbSet<Housekeeper> Housekeepers { get; set; }
    }
}
