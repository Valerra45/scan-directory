using ScanDirectory.Abstraction;
using System.Text;

namespace ScanDirectory.Models
{
    internal class ScanInfo : IScanInfo
    {
        public bool IsDirectory { get; set; }

        public int Level { get; set; }

        public string? Name { get; set; }

        public long Size { get; set; }

        public string? MimeType { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(new string('\t', Level - 1));

            sb.Append(IsDirectory ? "|" : " ");

            sb.Append(Name);

            sb.Append($"\t{Size.ToString("N0")}b\t");

            sb.Append(MimeType);

            return sb.ToString();
        }
    }
}
