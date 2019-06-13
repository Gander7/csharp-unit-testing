using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        // DI via Method Property //////////////////////////////////////
        public string ReadVideoTitle(IFileReader fileReader)
        {
            var str = fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video";
            return video.Title;
        }

        // DI via Class Property //////////////////////////////////////
        public IFileReader FileReader2 { get; set; }
        public VideoService()
        {
            FileReader2 = new FileReader();
        }
        public string ReadVideoTitle2()
        {
            var str = FileReader2.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video";
            return video.Title;
        }

        // DI via Constrcuctor //////////////////////////////////////
        private IFileReader FileReader3 { get; set; }
        public VideoService(IFileReader fileReader = null)
        {
            FileReader3 = fileReader ?? new FileReader();
        }
        public string ReadVideoTitle3()
        {
            var str = FileReader3.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            using (var context = new VideoContext())
            {
                var videos =
                    (from video in context.Videos
                     where !video.IsProcessed
                     select video).ToList();

                foreach (var v in videos)
                    videoIds.Add(v.Id);

                return String.Join(",", videoIds);
            }
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}
