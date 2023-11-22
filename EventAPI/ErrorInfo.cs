namespace EventAPI
{
    public class ErrorInfo
    {
        public Dictionary<string, string> Info { get; set; }=new Dictionary<string, string>();
        public void AddInfo(string key, string value)
        { Info.Add(key, value); }
    }
}
