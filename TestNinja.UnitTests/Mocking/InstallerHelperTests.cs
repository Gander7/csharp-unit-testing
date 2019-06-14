using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class InstallerHelperTests
    {
        private InstallerHelper _helper;
        private Mock<IFileDownloader> _downloader;

        [SetUp]
        public void Setup()
        {
            _downloader = new Mock<IFileDownloader>();
            _helper = new InstallerHelper(_downloader.Object);
        }

        [Test]
        public void DownloadInstaller_SuccessfulDownload_ReturnsTrue()
        {
            var actual = _helper.DownloadInstaller("customerName", "installerName");

            Assert.That(actual, Is.True);
        }

        [Test]
        public void DownloadInstaller_DownloadBreaks_ReturnsFalse()
        {
            // When throwing exceptions, 
            // by default, moq will only throw if params of method here match ones used to invoke
            // Moq has It.IsAny and other flags to bypass this functionality
            _downloader.Setup(dlr => dlr.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            var actual = _helper.DownloadInstaller("customerName", "installerName");

            Assert.That(actual, Is.False);
        }
    }
}
