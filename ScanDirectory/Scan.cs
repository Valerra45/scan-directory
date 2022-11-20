using ScanDirectory.Abstraction;

namespace ScanDirectory
{
    internal class Scan
    {
        private readonly IScanService _scanService;

        public Scan(IScanService scanService)
        {
            _scanService = scanService;
        }

        public void Do()
        {
            _scanService.Run();
        }
    }
}
