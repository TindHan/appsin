namespace appsin.Models
{
    public class ResponseSet<T>
    {
        public int status {  get; set; }
        public int number { get; set; }
        public string message { get; set; }
        public string uToken {  get; set; }
        public List<T> resData { get; set; }

    }
}
