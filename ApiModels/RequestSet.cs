namespace appsin.Models
{
    public class RequestSet<T>
    {
        public string uToken {  get; set; }
        public string action { get; set; }
        public List<T> reqData { get; set; }
    }

    public class commonItem
    {
        public string id { get; set; }
        public string type { get; set; }
        public string order { get; set; }
    }
}
