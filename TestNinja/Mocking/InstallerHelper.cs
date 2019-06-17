using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private IFileDownloader _fileDownloader;

        public InstallerHelper(IFileDownloader fileDownloader)
        {
            _fileDownloader = fileDownloader ?? new FileDownloader();
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            const string baseURL = "http://example.com";
            try
            {
                _fileDownloader.DownloadFile(
                    string.Format("{0}/{1}/{2}",
                        baseURL,
                        customerName,
                        installerName),
                   _setupDestinationFile);

                return true;
            }
            // Only catch possible expected exceptions
            // Other exceptions should propogate into a global exception tracker
            catch (WebException)
            {
                // Log stuff
                return false;
            }
        }
    }
}
