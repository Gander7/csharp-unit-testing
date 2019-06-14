using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        ///////////////////////////////////////////
        /// Example of DI via Method Property 
        ///////////////////////////////////////////
        public string ReadVideoTitleWithMethodProperty(IFileReader fileReader)
        {
            var str = fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video";
            return video.Title;
        }


        ///////////////////////////////////////////
        /// Example of DI via Class Property 
        ///////////////////////////////////////////
        public IFileReader ClassFileReader { get; set; }
        public VideoService()
        {
            ClassFileReader = new FileReader();
        }
        public string ReadVideoTitleWithClassProperty()
        {
            var str = ClassFileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video";
            return video.Title;
        }


        ///////////////////////////////////////////
        /// Example of DI via Constructor
        ///////////////////////////////////////////
        private IFileReader ConstructorFileReader { get; set; }
        public VideoService(IFileReader fileReader = null)
        {
            ConstructorFileReader = fileReader ?? new FileReader();
        }
        public string ReadVideoTitleWithContructor()
        {
            var str = ConstructorFileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video";
            return video.Title;
        }


        /////////////////////////////////////////////////////////////////
        ///// Rest of Class that wasn't duplicated for example purposes
        /////////////////////////////////////////////////////////////////
        private IVideoRepository _repository { get; set; }

        public VideoService(IVideoRepository repository = null)
        {
            ConstructorFileReader = new FileReader();
            _repository = repository ?? new VideoRepository();
        }
        public VideoService(IFileReader fileReader = null, IVideoRepository repository = null)
        {
            ConstructorFileReader = fileReader ?? new FileReader();
            _repository = repository ?? new VideoRepository();
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _repository.GetUnProcessedVideos();

            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }
}
