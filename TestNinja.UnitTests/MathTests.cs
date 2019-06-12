using NUnit.Framework;
using TestNinja;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            var math = new Math();

            var actual = math.Add(1, 2);

            Assert.That(actual, Is.EqualTo(3));
        }

        [Test]
        public void Max_FirstArgIsGreater_ReturnTheFirstArgument()
        {
            var math = new Math();

            var actual = math.Max(2, 1);

            Assert.That(actual, Is.EqualTo(2));
        }

        [Test]
        public void Max_SecondArgIsGreater_ReturnTheSecondArgument()
        {
            var math = new Math();

            var actual = math.Max(1, 2);

            Assert.That(actual, Is.EqualTo(2));
        }

        [Test]
        public void Max_ArgsAreEqual_ReturntheSameArgument()
        {
            var math = new Math();

            var actual = math.Max(2, );

            Assert.That(actual, Is.EqualTo(2));
        }
    }
}
