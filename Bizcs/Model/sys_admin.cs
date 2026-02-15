namespace appsin.Bizcs.Model
{
    public class sys_admin
    {
        public sys_admin()
        { }
        #region Model
        private int _adminid;
        private string _adminname;
        private string _adminpwd;
        private string _createuser;
        private DateTime? _createtime;
        private DateTime? _pwdsettime;
        /// <summary>
        /// 
        /// </summary>
        public int adminID
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string adminName
        {
            set { _adminname = value; }
            get { return _adminname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string adminPwd
        {
            set { _adminpwd = value; }
            get { return _adminpwd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createUser
        {
            set { _createuser = value; }
            get { return _createuser; }
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
        public DateTime? pwdSetTime
        {
            set { _pwdsettime = value; }
            get { return _pwdsettime; }
        }
        #endregion Model
    }
}
