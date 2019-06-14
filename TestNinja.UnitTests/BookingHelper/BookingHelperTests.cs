using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.BookingHelp;

namespace TestNinja.UnitTests.BookingTests
{
    [TestFixture]
    class BookingHelperTests
    {
        private Mock<IBookingStorage> _storage;
        private Booking _existingBooking;

        [SetUp]
        public void Setup()
        {
            _existingBooking = new Booking {
                Id = 2,
                ArrivalDate = ArriveOn(2019, 1, 10),
                DepartureDate = DepartOn(2019, 01, 20),
                Reference = "ABC"
            };
            _storage = new Mock<IBookingStorage>();
            BookingHelper._storage = _storage.Object;
            _storage.Setup(s => s.GetActiveBookings(1)).Returns(new List<Booking> { _existingBooking }.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_BookingIsCancelled_ReturnsEmptyString()
        {
            var actual = BookingHelper.OverlappingBookingsExist(new Booking {
                Id = 2,
                Status = "Cancelled"
            });

            Assert.That(actual, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndEndsBeforeExisting_ReturnsExistingReference()
        {
            var actual = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            });

            Assert.That(actual, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndEndsAfterExisting_ReturnsExistingReference()
        {
            var actual = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2)
            });

            Assert.That(actual, Is.EqualTo(string.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartBeforesAndEndsDuringExisting_ReturnsExistingReference()
        {
            var actual = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate)
            });

            Assert.That(actual, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsDuringAndEndsAfterExisting_ReturnsExistingReference()
        {
            var actual = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.DepartureDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            });

            Assert.That(actual, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndEndsAfterExisting_ReturnsExistingReference()
        {
            var actual = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            });

            Assert.That(actual, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndEndsDuringExisting_ReturnsExistingReference()
        {
            var actual = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate),
                DepartureDate = Before(_existingBooking.DepartureDate)
            });

            Assert.That(actual, Is.EqualTo(_existingBooking.Reference));
        }

        private DateTime Before(DateTime datetime, int days = 1)
        {
            return datetime.AddDays(-days);
        }

        private DateTime After(DateTime datetime, int days = 1)
        {
            return datetime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

    }
}
