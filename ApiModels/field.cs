namespace appsin.Models
{
    public class iFielditemList
    {
        public int itemID { get; set; }
        public int setID { get; set; }
        public string itemName { get; set; }
        public int itemLevel { get; set; }
        public int parentID { get; set; }
        public string itemDescription { get; set; }
        public int displayOrder { get; set; }
        public string createUser { get; set; }
        public DateTime createTime { get; set; }
        public int itemStatus { get; set; }
    }
    public class iFielditemEdit
    {
        public string itemID { get; set; }
        public string setID { get; set; }
        public string itemName { get; set; }
        public string parentID { get; set; }
        public string itemDesc { get; set; }
        public string displayOrder { get; set; }
        public string itemStatus { get; set; }
    }

    public class iFieldsetList
    {
        public int setID { get; set; }
        public string setPK { get; set; }
        public string setName { get; set; }
        public string setDescription { get; set; }
        public string setType { get; set; }
        public int setLevel { get; set; }
        public string setCode { get; set; }
        public int displayOrder { get; set; }
        public DateTime createTime { get; set; }
        public string createUser { get; set; }
        public int setStatus { get; set; }
    }

    public class iFieldsetEdit
    {
        public string setID { get; set; }
        public string setName { get; set; }
        public string setDesc { get; set; }
        public string setType { get; set; }
        public string setLevel { get; set; }
        public string setCode { get; set; }
        public string displayOrder { get; set; }
        public string setStatus { get; set; }
    }
}
