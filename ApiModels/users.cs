namespace appsin.Models
{
    public class reqSimple
    {
        public string args { get; set; }
    }
    public class reqLogin
    {
        public string userName { get; set; }
        public string userPwd { get; set; }
        public string vCode { get; set; }
        public string wkey { get; set; }
        public string bfp {  get; set; }
    }
    public class resLogin
    {
        public string uToken { get; set; }
        public string uInfo { get; set; }
    }

    public class reqAnalysis
    {
        public string uToken { get; set; }
    }
    public class resAnalysis
    {
        public int analysis1 { get; set; }
        public int analysis11 { get; set; }
        public int analysis2 { get; set; }
        public int analysis22 { get; set; }
        public int analysis3 { get; set; }
        public int analysis33 { get; set; }
        public int analysis4 { get; set; }
        public int analysis44 { get; set; }

    }
}
