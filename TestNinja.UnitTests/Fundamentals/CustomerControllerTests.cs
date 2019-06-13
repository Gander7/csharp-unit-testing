using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var controller = new CustomerController();

            var actual = controller.GetCustomer(0);

            // Not Found
            Assert.That(actual, Is.TypeOf<NotFound>());
            // or
            // NotFound or one of it's derivatives
            //Assert.That(actual, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            var controller = new CustomerController();

            var actual = controller.GetCustomer(1);

            Assert.That(actual, Is.TypeOf<Ok>());
        }
    }
}
