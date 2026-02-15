namespace appsin.Bizcs.Model
{
    public class sys_dataBind
    {
        public sys_dataBind()
        { }
        #region Model
        private int _bindid;
        private int? _roleid;
        private string _bindtype;
        private string _dynamicorg;
        private int? _staticorgid;
        private int? _suborgin;
        private DateTime? _createtime;
        private int? _createuser;
        private int? _bindstatus;
        /// <summary>
        /// 
        /// </summary>
        public int bindID
        {
            set { _bindid = value; }
            get { return _bindid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? roleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bindType
        {
            set { _bindtype = value; }
            get { return _bindtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dynamicOrg
        {
            set { _dynamicorg = value; }
            get { return _dynamicorg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? staticOrgID
        {
            set { _staticorgid = value; }
            get { return _staticorgid; }
        }
        /// <summary>
        /// 是否包含下级组织
        /// </summary>
        public int? subOrgIn
        {
            set { _suborgin = value; }
            get { return _suborgin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? createUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? bindStatus
        {
            set { _bindstatus = value; }
            get { return _bindstatus; }
        }
        #endregion Model
    }
}
