using Microsoft.AspNetCore.StaticFiles;
using ScanDirectory.Abstraction;
using ScanDirectory.Models;

namespace ScanDirectory.Services
{
    internal class ScanService : IScanService
    {
        private readonly IOutResult _outResult;

        public FileExtensionContentTypeProvider Provider { get; set; } = new();

        public List<IScanInfo> scanInfos = new();

        public string? DirectoryPath { get; set; }

        public ScanService(string? path, IOutResult outResult)
        {
            DirectoryPath = path;

            _outResult = outResult;
        }

        public void Run()
        {
            GetDirectoryFiles(DirectoryPath!, 0);

            ScanResult();
        }

        private void GetDirectoryFiles(string path, int level)
        {
            level++;

            var directoryInfo = new DirectoryInfo(path);

            var folders = directoryInfo.GetDirectories();

            foreach (var folder in folders)
            {
                scanInfos.Add(new ScanInfo()
                {
                    IsDirectory = true,
                    Level = level,
                    Name = folder.Name,
                    Size = GetDirectorySize(folder.FullName),
                    MimeType = "",
                });

                GetDirectoryFiles(folder.FullName, level);
            }

            var files = directoryInfo.GetFiles();

            foreach (var file in files)
            {
                scanInfos.Add(new ScanInfo()
                {
                    IsDirectory = false,
                    Level = level,
                    Name = file.Name,
                    Size = file.Length,
                    MimeType = GetMimeType(file.FullName),
                });
            }
        }

        private string GetMimeType(string path)
        {
            string type;

            if (!Provider.TryGetContentType(path, out type))
            {
                type = "application/octet-stream";
            };

            return type;
        }

        private long GetDirectorySize(string path)
        {
            var directoryInfo = new DirectoryInfo(path);

            return directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories)
                .Sum(x => x.Length);
        }

        private void ScanResult()
        {
            _outResult.Out(scanInfos);
        }
    }
}
