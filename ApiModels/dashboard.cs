namespace appsin.ApiModels
{
    public class funcSearch
    {
        public string menuPK {  get; set; }
        public string appName { get; set; }
        public string moduleName { get; set; }
        public string menuName { get; set; }
        public string menuDescription { get; set; }
    }
    public class taskList
    {
        public int taskID { get; set; }
        public int psnID { get; set; }
        public string? taskTitle { get; set; }
        public string? taskContent { get; set; }
        public DateTime? taskDeadline { get; set; }
        public int? taskProgress { get; set; }
        public DateTime? taskCreatetime { get; set; }
        public int? taskStatus { get; set; }
    }
    public class taksModel
    {
        public int taskID { get; set; }
        public string taskTitle { get; set; }
        public string taskContent { get; set; }
        public DateTime taskDeadline { get; set; }
        public int taskStatus { get; set; }
    }
    public class progressList
    {
        public int progressID { get; set; }
        public int? taskID { get; set; }
        public int? progressValue { get; set; }
        public string progressContent { get; set; }
        public DateTime? createTime { get; set; }
        public int? progressStatus { get; set; }
    }
    public class progressModel
    {
        public int progressID { get; set; }
        public int taskID { get; set; }
        public int progressValue { get; set; }
        public string progressContent { get; set; }
        public int progressStatus { get; set; }
    }
    public class noticeList
    {
        public int noticeID { get; set; }
        public int? appID { get; set; }
        public string appName { get; set; }
        public string objType { get; set; }
        public int? objID { get; set; }
        public string noticeTitle { get; set; }
        public string noticeContent { get; set; }
        public string noticeFile { get; set; }
        public DateTime? createTime { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int? createUser { get; set; }
        public string createUserName { get; set; }
        public int? noticeStatus { get; set; }

        public int? readCount { get; set; }
    }
    public class noticeModel
    {
        public int noticeID { get; set; }
        public string objType { get; set; }
        public int? objID { get; set; }
        public string noticeTitle { get; set; }
        public string noticeContent { get; set; }
        public string noticeFile { get; set; }
        public string storeFile { get; set; }
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public int? noticeStatus { get; set; }
    }
    public class msgList
    {
        public int msgID { get; set; }
        public int? appID { get; set; }
        public string appName { get; set; }
        public string objType { get; set; }
        public int? objID { get; set; }
        public string bizType { get; set; }
        public int bizID { get; set; }
        public string msgType { get; set; }
        public string msgTitle { get; set; }
        public string msgContent { get; set; }
        public string msgUrl { get; set; }
        public DateTime? createTime { get; set; }
        public DateTime? expireTime { get; set; }
        public string msgDesc { get; set; }
        public int? msgStatus { get; set; }
        public int readCount { get; set; }
    }
    public class msgModel
    {
        public int msgID { get; set; }
        public int? appID { get; set; }
        public string objType { get; set; }
        public int? objID { get; set; }
        public string msgType { get; set; }
        public string msgTitle { get; set; }
        public string msgContent { get; set; }
        public string msgUrl { get; set; }
        public DateTime? createTime { get; set; }
        public DateTime? expireTime { get; set; }
        public string msgDesc { get; set; }
        public int? msgStatus { get; set; }
    }
    public class msgApi<T>
    {
        public string aukey { get; set; }
        public string args { get; set; }
        public List<T> msgData { get; set; }
    }
    public class msgData
    {
        public string objType { get; set; }
        public int objID { get; set; }
        public string msgTitle { get; set; }
        public string msgContent { get; set; }
        public string msgUrl { get; set; }
        public DateTime createTime { get; set; }
        public DateTime expireTime { get; set; }
        public string msgDesc { get; set; }
    }
    public class consoleData
    {
        public List<consoleNum> data1 {get;set;}
        public List<consoleNum> data2 { get; set; }
        public List<consoleNum> data3 { get; set; }
        public List<consoleNum> data4 { get; set; }
    }
    public class consoleNum
    {
        public string objName { get; set; }
        public int thisMonth { get; set; }
        public int pre1Month { get; set; }
        public int pre2Month { get; set; }
        public int pre3Month { get; set; }
        public int pre4Month { get; set; }
        public int pre5Month { get; set; }
    }
}
