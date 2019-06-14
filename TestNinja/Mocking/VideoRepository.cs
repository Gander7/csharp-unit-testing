using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnProcessedVideos();
    }

    public class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetUnProcessedVideos()
        {
            using (var _context = new VideoContext())
            {
                var videos =
                    (from video in _context.Videos
                     where !video.IsProcessed
                     select video).ToList();

                return videos;
            }
        }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}
