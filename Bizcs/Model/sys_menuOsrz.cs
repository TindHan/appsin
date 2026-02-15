namespace appsin.Bizcs.Model
{
    public class sys_menuOsrz
    {
        public sys_menuOsrz()
        { }
        #region Model
        private int _osrzid;
        private string _osrzobjtype;
        private int? _osrzobjid;
        private int? _osrzroleid;
        private DateTime? _createtime;
        private int? _createuser;
        private int? _osrzstatus;
        /// <summary>
        /// 
        /// </summary>
        public int osrzID
        {
            set { _osrzid = value; }
            get { return _osrzid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string osrzObjType
        {
            set { _osrzobjtype = value; }
            get { return _osrzobjtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? osrzObjID
        {
            set { _osrzobjid = value; }
            get { return _osrzobjid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? osrzRoleID
        {
            set { _osrzroleid = value; }
            get { return _osrzroleid; }
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
        public int? osrzStatus
        {
            set { _osrzstatus = value; }
            get { return _osrzstatus; }
        }
        #endregion Model

    }
}
