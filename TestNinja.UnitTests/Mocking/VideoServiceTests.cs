using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        // DI testing via Method Parameter
        [Test]
        public void ReadVideoTitleWithMethodProperty_EmptyFile_ReturnsErrMsg()
        {
            var service = new VideoService();

            var result = service.ReadVideoTitleWithMethodProperty(new FakeFileReader());

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        // DI testing via Class property, just a refactored version of the above
        [Test]
        public void ReadVideoTitleWithClassProperty_EmptyFile_ReturnsErrMsg()
        {
            var service = new VideoService();
            service.ClassFileReader = new FakeFileReader();

            var result = service.ReadVideoTitleWithClassProperty();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        // DI Testing via Constructor
        // Just a refactored example of the above two examples
        [Test]
        public void ReadVideoTitleWithContructor_EmptyFile_ReturnsErrMsg()
        {
            var service = new VideoService(new FakeFileReader());

            var result = service.ReadVideoTitleWithContructor();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        // Mocking
        private VideoService _videoService;
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }
        [Test]
        public void ReaderVideoTitle3WithMock_EmptyFile_ReturnsErrMsg()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            
            var result = _videoService.ReadVideoTitleWithContructor();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AllProcessedVideos_ReturnsEmptyString()
        {
            _repository.Setup(r => r.GetUnProcessedVideos()).Returns(new List<Video>());

            var actual = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(actual, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_SomeProcessedVideos_ReturnsStringWithIdsOfUnprocessed()
        {
            _repository.Setup(r => r.GetUnProcessedVideos()).Returns(new List<Video>
            {
                new Video { Id = 1 },
                new Video { Id = 3 },
                new Video { Id = 4 },
            });

            var actual = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(actual, Is.EqualTo("1,3,4"));
        }
    }
}
