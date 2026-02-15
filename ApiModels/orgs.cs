namespace appsin.Models
{
    public class iOrgsList
    {
        public int orgID { get; set; }
        public string orgName { get; set; }
        public string orgCode { get; set; }
        public string orgType { get; set; }
        public string orgLevel { get; set; }
        public int parentID { get; set; }
        public string parentName { get; set; }
    }

    public class iOrgsTree
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int pId {  get; set; }
        public string pName { get; set; }
        public string icon { get; set; }
        public string open {  get; set; }

        
    }

    public class iOrgInfo
    {
        public string uID { get; set; }
        public string puID { get; set; }
        public string uName { get; set; }
        public string uCode { get; set; }
        public string uType { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string adminPost { get; set; }
        public string adminPsn { get; set; }
    }
}
