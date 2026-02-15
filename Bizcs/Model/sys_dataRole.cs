namespace appsin.Bizcs.Model
{
    public class sys_dataRole
    {

        public sys_dataRole()
        { }
        #region Model
        private int _roleid;
        private string _rolepk;
        private string _rolename;
        private string _roletype;
        private string _rolememo1;
        private string _rolememo2;
        private string _rolememo3;
        private string _rolememo4;
        private string _rolememo5;
        private int? _displayorder;
        private DateTime? _createtime;
        private int? _createuser;
        private int? _rolestatus;
        /// <summary>
        /// 
        /// </summary>
        public int roleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string rolePK
        {
            set { _rolepk = value; }
            get { return _rolepk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string roleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string roleType
        {
            set { _roletype = value; }
            get { return _roletype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string roleMemo1
        {
            set { _rolememo1 = value; }
            get { return _rolememo1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string roleMemo2
        {
            set { _rolememo2 = value; }
            get { return _rolememo2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string roleMemo3
        {
            set { _rolememo3 = value; }
            get { return _rolememo3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string roleMemo4
        {
            set { _rolememo4 = value; }
            get { return _rolememo4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string roleMemo5
        {
            set { _rolememo5 = value; }
            get { return _rolememo5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? displayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
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
        public int? roleStatus
        {
            set { _rolestatus = value; }
            get { return _rolestatus; }
        }
        #endregion Model
    }
}
