using ScanDirectory.Abstraction;

namespace ScanDirectory.Services
{
    internal class ScanResult : IOutResult
    {
        public string DirectoryPath { get; set; }

        public string FileName { get; set; }

        public ScanResult(string path, string fileName)
        {
            DirectoryPath = path;

            FileName = fileName;
        }

        public void Out(List<IScanInfo> scanInfos)
        {
            if (scanInfos.Count == 0)
            {
                return;
            }

            var htmlFilePath = Path.Combine(DirectoryPath, FileName);

            List<string> page = new();

            page.Add("<!DOCTYPE html>");
            page.Add("<html>");

            page.Add("<head>");
            page.Add("<meta charset=\"utf-8\">");
            page.Add("</head>");

            page.Add("<body>");

            page.Add($"<h2>Результат сканирования директории {DirectoryPath}:</h2>");

            page.Add("<div>");

            foreach (var info in scanInfos)
            {
                page.Add($"<pre>{info}</pre>");
            }

            page.Add("</div>");

            var typeInfos = scanInfos.FindAll(x => !x.IsDirectory)
                .GroupBy(x => x.MimeType)
                .Select(x => new
                {
                    MimeType = x.Key,
                    Size = x.Sum(y => y.Size),
                    Count = x.Count(),
                });

            var allSize = typeInfos.Sum(x => x.Size);
            var allCount = typeInfos.Sum(x => x.Count);

            page.Add("<h2>Дополнительная информация:</h2>");

            page.Add("<table border=\"1\">");
            page.Add("<caption>Группировка по MimeType</caption>");

            page.Add("<tr>");
            page.Add("<th>MimeType</th>");
            page.Add("<th>Файлов</th>");
            page.Add("<th>Файлы %</th>");
            page.Add("<th>Средний размер</th>");
            page.Add("</tr>");

            foreach (var type in typeInfos)
            {
                page.Add("<tr>");
                page.Add($"<td>{type.MimeType}</td>");
                page.Add($"<td>{type.Count}</td>");
                page.Add($"<td>{((double)type.Count / allCount).ToString("P02")}</td>");
                page.Add($"<td>{(type.Size / type.Count).ToString("N0")}</td>");
                page.Add("</tr>");
            }

            page.Add("</table>");

            page.Add("</body>");
            page.Add("</html>");

            File.WriteAllLines(htmlFilePath, page);
        }
    }
}
