using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TestNinja.BookingHelp
{
    public interface IBookingStorage
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }

    public class BookingStorage : IBookingStorage
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            // In real world, should not have updated this function without covering
            // it with integration tests first.
            using (var _context = new BookingContext())
            {
                var bookings = _context.Bookings.Where(b => b.Status != "Cancelled");

                if (excludedBookingId.HasValue)
                    bookings = bookings.Where(b => b.Id != excludedBookingId);

                return bookings;
            }
        }
    }

    public class BookingContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
    }
}
