using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        // DI testing via Method Parameter
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnsErrMsg()
        {
            var service = new VideoService();

            var result = service.ReadVideoTitle(new FakeFileReader());

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        // DI testing via Class property
        [Test]
        public void ReadVideoTitle2_EmptyFile_ReturnsErrMsg()
        {
            var service = new VideoService();
            service.FileReader2 = new FakeFileReader();

            var result = service.ReadVideoTitle2();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        // DI Testing via Constructor
        [Test]
        public void ReadVideoTitle3_EmptyFile_ReturnsErrMsg()
        {
            var service = new VideoService(new FakeFileReader());

            var result = service.ReadVideoTitle3();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        // Mocking
        private VideoService _videoService;
        private Mock<IFileReader> _fileReader;
        [SetUp]
        public void Setup()
        {
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReader.Object);
        }
        [Test]
        public void ReaderVideoTitle3WithMock_EmptyFile_ReturnsErrMsg()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            
            var result = _videoService.ReadVideoTitle3();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}
