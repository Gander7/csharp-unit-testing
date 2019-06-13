using NUnit.Framework;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            // Arrange
            var reservation = new Reservation();

            // Act
            var result = reservation.CanBeCancelledBy(new User { isAdmin = true });

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_SameUser_ReturnsTrue()
        {
            var madeBy = new User();
            var reservation = new Reservation { MadeBy = madeBy };

            var actual = reservation.CanBeCancelledBy(madeBy);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void CanBeCancelledBy_DifferentNonAdminUser_ReturnsFalse()
        {
            var reservation = new Reservation { MadeBy = new User() };

            var actual = reservation.CanBeCancelledBy(new User());

            Assert.That(actual, Is.False);
        }
    }
}
