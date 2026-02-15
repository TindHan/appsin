using System.Security.Cryptography;

namespace appsin.ApiModels
{
    public class nextApprover
    {
        public int msgID { get; set; }
        public string msgTitle { get; set; }
        public DateTime createTime { get; set; }
        public string msgContent { get; set; }
        public string bizType { get; set; }
        public int bizID { get; set; }
        public string objType { get; set; }
        public int objID { get; set; }
        public string psnName { get; set; }
        public string postName { get; set; }
        public string deptName { get; set; }
        public string unitName { get; set; }
        public DateTime readTime { get; set; }
        public int readCount { get; set; }
    }
    public class approveLogList
    {
        public int nodeID { get; set; }
        public string approverName { get; set; }
        public string approverUnit { get; set; }
        public string approverDept { get; set; }
        public string approverPost { get; set; }
        public int isAgree { get; set; }
        public string isNote { get; set; }
        public DateTime approveTime { get; set; }
    }
    public class instanceList
    {
        public int instanceID { get; set; }
        public string instancePK { get; set; }
        public string instanceName { get; set; }
        public string instanceDesc { get; set; }
        public string doneNodePK { get; set; }
        public string contentUrl { get; set; }
        public string domain { get; set; }
        public int isEnd { get; set; }
        public int isPass { get; set; }
        public int isError { get; set; }
        public string errorDesc { get; set; }
        public string psnPK { get; set; }
        public string psnName { get; set; }
        public string createPsnPK { get; set; }
        public string createPsnName { get; set; }
        public string appName { get; set; }
        public string templateName { get; set; }
        public string doneNodeName { get; set; }
        public DateTime createTime { get; set; }

    }
    public class instanceDetail
    {
        public int instanceID { get; set; }
        public string instancePK { get; set; }
        public int appID { get; set; }
        public string instanceName { get; set; }
        public string instanceDesc { get; set; }
        public string doneNodePK { get; set; }
        public string contentUrl { get; set; }
        public string domain { get; set; }
        public int isEnd { get; set; }
        public int isPass { get; set; }
        public string psnPK { get; set; }
        public string createPsnName { get; set; }
        public string createPsn { get; set; }
        public DateTime createTime { get; set; }
        public int instanceStatus { get; set; }
    }

    public class instanceApi<Tnodes, Tlogs>
    {
        public string instancePK { get; set; }
        public string instanceName { get; set; }
        public string instanceDesc { get; set; }
        public string doneNodePK { get; set; }
        public string contentUrl { get; set; }
        public int isEnd { get; set; }
        public int isPass { get; set; }
        public int isError { get; set; }
        public string errorDesc { get; set; }
        public string psnPK { get; set; }
        public string createPsnPK { get; set; }
        public DateTime createTime { get; set; }
        public List<Tnodes> apprNodes { get; set; }
        public List<Tlogs> apprLogs { get; set; }
    }

    public class LogsApi
    {
        public string nodePK { get; set; }
        public string approverName { get; set; }
        public string approverUnit { get; set; }
        public string approverDept { get; set; }
        public string approverPost { get; set; }
        public int isAgree { get; set; }
        public string isNote { get; set; }
        public DateTime approveTime { get; set; }
    }
    public class nodesApi
    {
        public string nodePk { get; set; }
        public string nodeName { get; set; }
        public string prevNodePK { get; set; }
        public int isEnd { get; set; }
        public string approve { get; set; }
        public string condition { get; set; }
    }
    public class templateApi
    {
        public string templatePK { get; set; }
        public string templateName { get; set; }
        public string templateDesc { get; set; }
        public string str1Title { get; set; }
        public string int1Title { get; set; }
        public string date1Title { get; set; }
    }

    public class templateList
    {
        public int templateID { get; set; }
        public string templatePK { get; set; }
        public string templateName { get; set; }
        public string templateDesc { get; set; }
        public int createUser { get; set; }
        public string psnName { get; set; }
        public DateTime createTime { get; set; }
        public int isReady { get; set; }
        public int displayOrder { get; set; }
        public int templateStatus { get; set; }
    }

    public class templateEdit
    {
        public string templateID { get; set; }
        public string templateName { get; set; }
        public string templateDesc { get; set; }
        public string str1Title { get; set; }
        public string str2Title { get; set; }
        public string str3Title { get; set; }
        public string int1Title { get; set; }
        public string int2Title { get; set; }
        public string int3Title { get; set; }
        public string date1Title { get; set; }
        public string date2Title { get; set; }
        public string date3Title { get; set; }
        public string displayOrder { get; set; }
        public string templateStatus { get; set; }
    }

    public class templateDetail
    {
        public int templateID { get; set; }
        public string templateName { get; set; }
        public string templateDesc { get; set; }
        public string str1Title { get; set; }
        public string str2Title { get; set; }
        public string str3Title { get; set; }
        public string int1Title { get; set; }
        public string int2Title { get; set; }
        public string int3Title { get; set; }
        public string date1Title { get; set; }
        public string date2Title { get; set; }
        public string date3Title { get; set; }
        public int displayOrder { get; set; }
        public int templateStatus { get; set; }
    }

    public class nodeEdit
    {
        public int templateID { get; set; }
        public string nodePK { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public decimal left { get; set; }
        public decimal top { get; set; }
        public string approve { get; set; }
        public string condition { get; set; }
        public string prevID { get; set; }
        public int isEnd { get; set; }
    }
}
