using NUnit.Framework;
using System;
using System.Linq;
using TestNinja;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        private Math _math;

        [SetUp]
        // Setup, triggers before each test
        public void SetUp()
        {
            _math = new Math();
        }

        // Teardown
        // triggers after each test. Often used with integration tests to keep data clean.


        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            var actual = _math.Add(1, 2);

            Assert.That(actual, Is.EqualTo(3));
        }

        [Test]
        [TestCase(1, 2, 2)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 2)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expected)
        {
            var actual = _math.Max(a, b);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [Ignore("TODO, Broken")]
        public void BrokenTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetOddNumbers_LimitIsPositive_ReturnOddNumbersUpToLimit()
        {
            var actual = _math.GetOddNumbers(5);

            //Assert.That(actual, Is.Not.Empty);
            // or
            //Assert.That(actual.Count(), Is.EqualTo(3));
            // or
            //Assert.That(actual, Does.Contain(1));
            //Assert.That(actual, Does.Contain(3));
            //Assert.That(actual, Does.Contain(5));
            // or
            Assert.That(actual, Is.EquivalentTo(new[] { 1, 3, 5 })); // does not care about order

            // Other, Useful but not needed for this test
            Assert.That(actual, Is.Ordered);
            Assert.That(actual, Is.Unique);
        }
    }
}
