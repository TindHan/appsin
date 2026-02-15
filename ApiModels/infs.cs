using System;

namespace appsin.Models
{
    public class iApiUseLogList
    {
        public int logID { get; set; }
        public int? appID { get; set; }
        public string appName { get; set; }
        public int? apiID { get; set; }
        public string apiName { get; set; }
        public int? osrzID { get; set; }
        public DateTime? createTime { get; set; }
        public string appDomain { get; set; }
        public string isS { get; set; }
        public string logMemo { get; set; }
        public string inPara { get; set; }
        public string outPara { get; set; }
    }
    public class iApiOsrzList
    {
        public int osrzID { get; set; }
        public int appID { get; set; }
        public string appName { get; set; }
        public int apiID { get; set; }
        public string apiName { get; set; }
        public string apiCode { get; set; }
        public DateTime validStartTime { get; set; }
        public DateTime validEndTime { get; set; }
        public string osrzDescription { get; set; }
        public string createUser { get; set; }
        public DateTime createTime { get; set; }
        public int displayOrder { get; set; }
        public int osrzStatus { get; set; }
    }
    public class iApiOsrzEdit
    {
        public string osrzID { get; set; }
        public string appID { get; set; }
        public string apiID { get; set; }
        public DateTime validStartTime { get; set; }
        public DateTime validEndTime { get; set; }
        public string displayOrder { get; set; }
        public string osrzDesc { get; set; }
        public string osrzStatus { get; set; }
    }


    public class iApiList
    {
        public int apiID { get; set; }
        public string apiName { get; set; }
        public string apiDescription { get; set; }
        public string apiCode { get; set; }
        public string apiType { get; set; }
        public string apiAddress { get; set; }
        public string apiReqPara { get; set; }
        public string apiResPara { get; set; }
        public string apiKeyNote { get; set; }
        public int isIdentify { get; set; }
        public string apiExample { get; set; }
        public string createUser { get; set; }
        public DateTime createTime { get; set; }
        public int displayOrder { get; set; }
        public int apiStatus { get; set; }
    }
    public class iApiEdit
    {
        public string apiID { get; set; }
        public string apiName { get; set; }
        public string apiDesc { get; set; }
        public string apiCode { get; set; }
        public string apiType { get; set; }
        public string apiAddress { get; set; }
        public string apiReqPara { get; set; }
        public string apiResPara { get; set; }
        public string apiKeyNote { get; set; }
        public string apiExample { get; set; }
        public string apiStatus { get; set; }
    }
}