using ScanDirectory;
using ScanDirectory.Services;

var path = Directory.GetCurrentDirectory();

var scanResult = new ScanResult(path, "scan_result.html");

var scan = new Scan(new ScanService(path, scanResult));

scan.Do();