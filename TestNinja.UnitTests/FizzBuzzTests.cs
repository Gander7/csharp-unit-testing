using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class FizzBuzzTests
    {
        [Test]
        public void GetOutput_DivisibleBy5And3_ReturnsFizzBuzz()
        {
            var actual = FizzBuzz.GetOutput(15);

            Assert.That(actual, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_DivisibleBy3_ReturnsFizz()
        {
            var actual = FizzBuzz.GetOutput(9);

            Assert.That(actual, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_DivisibleBy5_ReturnsBuzz()
        {
            var actual = FizzBuzz.GetOutput(10);

            Assert.That(actual, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_NotDivisibleBy3Or5_ReturnsNumberArg()
        {
            var actual = FizzBuzz.GetOutput(7);

            Assert.That(actual, Is.EqualTo(7.ToString()));
        }
    }
}
