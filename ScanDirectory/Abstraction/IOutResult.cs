namespace ScanDirectory.Abstraction
{
    internal interface IOutResult
    {
        public void Out(List<IScanInfo> scanInfos);
    }
}
