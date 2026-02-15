namespace appsin.Models
{
    public class osrzDisable
    {
        public string id { get; set; }
        public string type { get; set; }
    }
    public class objList
    {
        public int objID { get; set; }
        public string objName { get; set; }
        public string objType { get; set; }
        public string objDesc { get; set; }
        public string objCode { get; set; }
        public string objParent { get; set; }
    }
    
    public class objRoleBindList
    {
        public int osrzID { get; set; }
        public string osrzWay { get; set; }
        public string osrzObjType { get; set; }
        public string objName { get; set; }
        public string roleName { get; set; }
        public string createUserName { get; set; }
        public DateTime createTime { get; set; }
        public int osrzStatus { get; set; }
    }

    public class objRoleBindEdit
    {
        public string roleID { get; set; }
        public string objType { get; set; }
        public string objID { get; set; }
    }

    public class dataItemList
    {
        public int bindID { get; set; }
        public string bindType { get; set; }
        public string dynamicOrg { get; set; }
        public string staticOrgName { get; set; }
        public string subOrgIn { get; set; }
        public DateTime createTime { get; set; }
        public string createUserName { get; set; }
    }

    public class dataItemEdit
    {
        public string roleID { get; set; }
        public string bindType { set; get; }
        public string subOrgIn {  set; get; }
        public string dynamicOrg { get; set; }
        public string staticOrgID { get; set; }
    }

    public class osrzItemList
    {
        public int bindID { get; set; }
        public string menuName { get; set; }
        public string moduleName { get; set; }
        public string appName { get; set; }
        public string createUserName { get; set; }
        public DateTime createTime { get; set; }
        public int bindStatus { get; set; }
        
    }

    public class osrzItemEdit
    {
        public string roleID { get; set; }
        public string menuID { get; set; }
    }
    public class osrzRoleList
    {
        public int roleID { get; set; }
        public string roleName { get; set; }
        public string roleMemo { get; set; }
        public string roleEnable { get; set; }
        public int roleStatus { get; set; }
        public DateTime createTime { get; set; }
        public string createUserName { get; set;}
    }
    public class osrzRoleEdit
    {
        public string roleID { get; set; }
        public string roleName { get; set; }
        public string roleMemo { get; set; }
        public string roleStatus { get; set; }
    }
}
