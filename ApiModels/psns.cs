using System;

namespace appsin.Models
{
    public class iPsnRole
    {
        public int roleID { get; set; }
        public string roleName { get; set; }
        public string roleMemo1 { get; set; }
        public string roleObj { get; set; }
        public string createUser { get; set;}
        public DateTime createTime { get; set;}
    }

    public class iGotoLog
    {
        public int logID { get; set; }
        public int psnID { get; set; }
        public string psnName { get; set; }
        public int appID { get; set; }
        public string appName { get; set; }
        public int menuID { get; set; }
        public string menuName { get; set; }
        public string goStr { get; set; }
        public DateTime goTime { get; set; }
        public DateTime callBackTime { get; set; }
        public string callBackRes { get; set; }
    }
    public class iActLog
    {
        public int psnID { get; set; }
        public string psnName { get; set; }
        public DateTime logTime { get; set; }
        public string logAction { get; set; }
        public string isSuccess { get; set; }
        public string actDvsIP { get; set; }
        public string actDvsCode { get; set; }
        public string actPara { get; set; }
        public string actResult { get; set; }
        public string actMemo { get; set; }
    }
    public class iTransEdit
    {
        public string psnID { get; set; }
        public string transType { get; set; }
        public DateTime transDate { get; set; }
        public string unitID { get; set; }
        public string deptID { get; set; }
        public string postID { get; set; }
        public string onType { get; set; }
        public string onStatus { get; set; }
    }

    public class iTransList
    {
        public int psnID { get; set; }
        public string transType { get; set; }
        public DateTime transDate { get; set; }
        public string preUnitName { get; set; }
        public string preDeptName { get; set; }
        public string prePostName { get; set; }
        public string preOnTypeName { get; set; }
        public string preOnStatusName { get; set; }
        public string nextUnitName { get; set; }
        public string nextDeptName { get; set; }
        public string nextPostName { get; set; }
        public string nextOnTypeName { get; set; }
        public string nextOnStatusName { get; set; }
    }

    public class iFieldItem
    {
        public int itemID { get; set; }
        public string itemName { get; set; }
        public int itemLevel { get; set; }
        public int itemPID { get; set; }
        public int showOrder { get; set; }
    }
    public class iPsnInfo
    {
        public int psnID { get; set; }
        public int unitID { get; set; }
        public int deptID { get; set; }
        public int postID { get; set; }
        public string unitName { get; set; }
        public string deptName { get; set; }
        public string postName { get; set; }
        public string psnName { get; set; }
        public string aliaName { get; set; }
        public string psnCode { get; set; }
        public string idType { get; set; }
        public string idTypeName { get; set; }
        public string idNo { get; set; }
        public int onStatus { get; set; }
        public int loginStatus { get; set; }
        public string onStatusName { get; set; }
        public int onType { get; set; }
        public string onTypeName { get; set; }
        public string psnSex { get; set; }
        public string psnNational { get; set; }
        public string psnPicture { get; set; }
        public string psnEmail { get; set; }
        public string psnCellphone { get; set; }
        public string psnUsername { get; set; }
        public string psnIM { get; set; }
        public DateTime psnBirthday { get; set; }
        public DateTime psnJoinday { get; set; }
        public DateTime psnJobday { get; set; }
        public string psnMemo1 { get; set; }
        public string psnMemo2 { get; set; }
        public string psnMemo3 { get; set; }
        public string psnMemo4 { get; set; }
        public string psnMemo5 { get; set; }
        public DateTime createTime { get; set; }

    }

    public class iPsnEdit
    {
        public string psnID { get; set; }
        public string unitID { get; set; }
        public string deptID { get; set; }
        public string postID { get; set; }
        public string psnName { get; set; }
        public string aliaName { get; set; }
        public string psnCode { get; set; }
        public string idType { get; set; }
        public string idNo { get; set; }
        public string loginStatus { get; set; }
        public string onType { get; set; }
        public string psnSex { get; set; }
        public string psnNational { get; set; }
        public string psnPicture { get; set; }
        public string psnEmail { get; set; }
        public string psnCellphone { get; set; }
        public string psnIM { get; set; }
        public string psnBirthday { get; set; }
        public string psnJoinday { get; set; }
        public string psnJobday { get; set; }
        public string psnUsername { get; set; }
        public string psnPassword { get; set; }
        public string psnMemo1 { get; set; }
        public string psnMemo2 { get; set; }
        public string psnMemo3 { get; set; }
        public string psnMemo4 { get; set; }
        public string psnMemo5 { get; set; }

    }

    public class iReqConditon
    {
        public string oid { get; set; }
        public string kw { get; set; }
        public string ons { get; set; }
        public string ty { get; set; }
        public int pageIndex { get; set; }
        public int pageListNum { get; set; }
    }
}
