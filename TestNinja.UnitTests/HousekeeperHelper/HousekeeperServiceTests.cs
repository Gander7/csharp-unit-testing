using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Housekeeping;

namespace TestNinja.UnitTests.HousekeeperHelperTests
{
    [TestFixture]
    public class HousekeeperServiceTests
    {
        private HousekeeperService _service;
        private Mock<IStatementReportGenerator> _generator;
        private Mock<IEmailHelper> _emailHelper;
        private Mock<IXtraMessageBox> _messageBox;

        private DateTime _statementDate = new DateTime(2019, 1, 1);
        private string _statementFileName = "filename";
        private Housekeeper _housekeeper;

        [SetUp]
        public void Setup()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

            var storage = new Mock<IHouseKeeperRepository>();
            storage.Setup(s => s.GetHousekeepers()).Returns(new List<Housekeeper>
            {
                _housekeeper
            }.AsQueryable());

            _statementFileName = "filename";
            _generator = new Mock<IStatementReportGenerator>();
            _generator
                .Setup(g => g.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() => _statementFileName);

            _emailHelper = new Mock<IEmailHelper>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(
                storage.Object, 
                _generator.Object, 
                _emailHelper.Object, 
                _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _generator.Verify(g => 
                g.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [Test]
        [TestCase(null)]
        [TestCase("               ")]
        [TestCase("")]
        public void SendStatementEmails_HouseKeeperEmailIsNullOrWhitespace_ShouldNotGenerateStatements(string email)
        {
            _housekeeper.Email = email;

            _service.SendStatementEmails(_statementDate);

            _generator.Verify(g =>
                g.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _emailHelper.Verify(eh => eh.EmailFile(
                _housekeeper.Email, 
                _housekeeper.StatementEmailBody, 
                _statementFileName,
                It.IsAny<string>()));
        }

        [Test]
        [TestCase(null)]
        [TestCase("     ")]
        [TestCase("")]
        public void SendStatementEmails_FilenameIsNullOrWhitespace_ShouldNotEmailStatements(string statementFileName)
        {
            // Lambda expression of generator Returns allows on demand assignment of filename.
            _statementFileName = statementFileName;

            _service.SendStatementEmails(_statementDate);

            _emailHelper.Verify(eh =>
                eh.EmailFile(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()), 
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenEmailFails_MessageBoxShouldBeShown()
        {
            _emailHelper.Setup(eh => eh.EmailFile(
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<string>()
            )).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            _messageBox.Verify(mb => mb.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<MessageBoxButtons>()));
        }
    }
}
