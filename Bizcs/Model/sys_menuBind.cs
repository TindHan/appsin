namespace appsin.Bizcs.Model
{
    public class sys_menuBind
    {
        public sys_menuBind()
        { }
        #region Model
        private int _bindid;
        private int? _roleid;
        private int? _menuid;
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
        public int? menuID
        {
            set { _menuid = value; }
            get { return _menuid; }
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
