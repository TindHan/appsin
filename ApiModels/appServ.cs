namespace appsin.ApiModels
{
    public class servFlowArgs
    {
        public string aukey { get; set; }
        public string psnPK { get; set; }
        public string createPsnPK { get; set; }
        public string templatePK { get; set; }
        public string flowName { get; set; }
        public string flowDesc { get; set; }
        public string contentUrl { get; set; }
        public int? int1Value {  get; set; }
        public string? str1Value { get; set; }
        public DateTime? date1Value { get; set; }
    }
    public class servFielditem
    {
        public string itemPK { set; get; }
        public string setPK { set; get; }
        public string itemName { set; get; }
        public int itemLevel { set; get; }
        public string parentPK { set; get; }
        public string itemDescription { set; get; }
        public string itemMemo1 { set; get; }
        public string itemMemo2 { set; get; }
        public string itemMemo3 { set; get; }
        public string itemMemo4 { set; get; }
        public string itemMemo5 { set; get; }
        public int displayOrder { set; get; }
    }

    public class servFieldset
    {
        public string setPK { set; get; }
        public string setName { set; get; }
        public string setDescription { set; get; }
        public string setType { set; get; }
        public string setCode { set; get; }
        public int setLevel { set; get; }
        public string setMemo1 { set; get; }
        public string setMemo2 { set; get; }
        public string setMemo3 { set; get; }
        public string setMemo4 { set; get; }
        public string setMemo5 { set; get; }
    }

    public class servDataOsrz
    {
        public string bindType { get; set; }
        public string dynamicOrg { get; set; }
        public string staticOrgPK { get; set; }
        public string subOrgIn { get; set; }
    }
    public class servResArgs<T>
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<T> resData { get; set; }

    }
    public class servReqArgs
    {
        public string aukey { get; set; }
        public string args { get; set; }
    }

    public class servOrgList
    {
        public string orgPK { get; set; }
        public string orgCode { get; set; }
        public string parentPK { get; set; }
        public string orgLevel { get; set; }
        public string orgType { get; set; }
        public string orgName { get; set; }
        public string chargePsn { get; set; }
        public string chargePost { get; set; }
        public string parentName { get; set; }
        public DateTime validStartDate { get; set; }
        public DateTime validEndDate { get; set; }
        public string displayOrder { get; set; }
        public string orgMemo1 { get; set; }
        public string orgMemo2 { get; set; }
        public string orgMemo3 { get; set; }
        public string orgMemo4 { get; set; }
        public string orgMemo5 { get; set; }
        public string createTime { get; set; }
    }

    public class servPsnInfoFull
    {
        public int psnPK { get; set; }
        public int unitPK { get; set; }
        public int deptPK { get; set; }
        public int postPK { get; set; }
        public string unitName { get; set; }
        public string deptName { get; set; }
        public string postName { get; set; }
        public string psnName { get; set; }
        public string aliaName { get; set; }
        public string psnCode { get; set; }
        public string idTypeName { get; set; }
        public string idNo { get; set; }
        public int loginStatus { get; set; }
        public string onStatusName { get; set; }
        public string onTypeName { get; set; }
        public string psnSex { get; set; }
        public string psnNational { get; set; }
        public string psnPicture { get; set; }
        public string psnEmail { get; set; }
        public string psnCellphone { get; set; }
        public string psnUsername { get; set; }
        public string psnIM { get; set; }
        public string psnBirthday { get; set; }
        public string psnJoinday { get; set; }
        public string psnJobday { get; set; }

    }

    public class servPsnInfoSimple
    {
        public string psnPK { get; set; }
        public string unitPK { get; set; }
        public string deptPK { get; set; }
        public string postPK { get; set; }
        public string unitName { get; set; }
        public string deptName { get; set; }
        public string postName { get; set; }
        public string psnName { get; set; }
        public string aliaName { get; set; }
        public string psnCode { get; set; }
        public string onStatusName { get; set; }
        public string onTypeName { get; set; }
        public string psnSex { get; set; }
        public string psnNational { get; set; }
        public string psnUsername { get; set; }

    }

    public class keyValueList
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
