using System;
using System.Linq;

namespace TestNinja.BookingHelp
{
    public class BookingHelper
    {
        public static IBookingStorage _storage { get; set; }

        static BookingHelper()
        {
            _storage = new BookingStorage();
        }

        public static string OverlappingBookingsExist(Booking booking)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = _storage.GetActiveBookings(booking.Id);

            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate <= b.DepartureDate
                        && b.ArrivalDate <= booking.DepartureDate);

            return overlappingBooking == null
                ? string.Empty
                : overlappingBooking.Reference;
        }
    }

    // Using integers as dates for simplicity.
    // Should be using DateTimes.
    public class Booking
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}
