namespace ScanDirectory.Abstraction
{
    internal interface IScanInfo
    {
        public bool IsDirectory { get; set; }

        public int Level { get; set; }

        public string? Name { get; set; }

        public long Size { get; set; }

        public string? MimeType { get; set; }
    }
}
