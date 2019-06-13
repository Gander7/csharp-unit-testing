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
    }
}
