using System;

namespace appsin.Models
{
    public class iMenuList
    {
        public int menuID { get; set; }
        public int parentID { get; set; }
        public string menuName { get; set; }
        public int menuAppID { get; set; }
        public int menuLevel { get; set; }
        public string menuType { get; set; }
        public string menuLink { get; set; }
        public string menuIcon { get; set; }
        public string menuDescription { get; set; }
        public int displayOrder { get; set; }
        public string createUser { get; set; }
        public DateTime createTime { get; set; }
        public int menuStatus { get; set; }
    }
    public class iMenuEdit
    {
        public string menuID { get; set; }
        public string parentID { get; set; }
        public string appName { get; set; }
        public string menuName { get; set; }
        public string menuType { get; set; }
        public string menuDesc { get; set; }
        public string menuIcon { get; set; }
        public string menuLink { get; set; }
        public string menuStatus { get; set; }
        public string displayOrder { get; set; }
    }
    public class iAppsList
    {
        public int appID { get; set; }
        public string appPK { get; set; }
        public string appName { get; set; }
        public string appSID { get; set; }
        public string appSecret { get; set; }
        public string appSkey { get; set; }
        public string appType { get; set; }
        public string appDomain1 { get; set; }
        public string appDomain2 { get; set; }
        public string appDomain3 { get; set; }
        public string appDescription { get; set; }
        public DateTime validStartTime { get; set; }
        public DateTime validEndTime { get; set; }
        public string createUser { get; set; }
        public DateTime createTime { get; set; }
        public int appStatus { get; set; }
    }
    public class iAppsEdit
    {
        public string appID {  get; set; }
        public string appName { get; set; }
        public string appType { get; set; }
        public string appSID {  get; set; }
        public string appSecret { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string appDesc { get; set; }
        public string Domain1 { get; set; }
        public string Domain2 { get; set; }
        public string Domain3 { get; set; }
        public string appStatus { get; set; }
    }
}
