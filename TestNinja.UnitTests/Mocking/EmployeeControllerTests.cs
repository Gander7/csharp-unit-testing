using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _storage;
        private EmployeeController _controller;

        [SetUp]
        public void Setup()
        {
            _storage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_storage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_InvokesDeleteEmployee()
        {
            _controller.DeleteEmployee(1);
            _storage.Verify(s => s.deleteEmployee(1));
        }

        [Test]
        public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
        {
            var actual = _controller.DeleteEmployee(1);

            Assert.That(actual, Is.TypeOf<RedirectResult>());
        }
    }
}
