namespace appsin.Bizcs.Model
{
    public class sys_menu
    {
        public sys_menu()
        { }
        #region Model
        private int _menuid;
        private string _menupk;
        private int? _parentid;
        private string _parentpk;
        private string _menuname;
        private int? _menuappid;
        private int? _menulevel;
        private string _menutype;
        private string _menulink;
        private string _menuicon;
        private string _menudescription;
        private string _menumemo1;
        private string _menumemo2;
        private string _menumemo3;
        private string _menumemo4;
        private string _menumemo5;
        private int? _displayorder;
        private int? _createuser;
        private DateTime? _createtime;
        private int? _menustatus;
        /// <summary>
        /// 
        /// </summary>
        public int menuID
        {
            set { _menuid = value; }
            get { return _menuid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuPK
        {
            set { _menupk = value; }
            get { return _menupk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? parentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string parentPK
        {
            set { _parentpk = value; }
            get { return _parentpk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuName
        {
            set { _menuname = value; }
            get { return _menuname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? menuAppID
        {
            set { _menuappid = value; }
            get { return _menuappid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? menuLevel
        {
            set { _menulevel = value; }
            get { return _menulevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuType
        {
            set { _menutype = value; }
            get { return _menutype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuLink
        {
            set { _menulink = value; }
            get { return _menulink; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuIcon
        {
            set { _menuicon = value; }
            get { return _menuicon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuDescription
        {
            set { _menudescription = value; }
            get { return _menudescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuMemo1
        {
            set { _menumemo1 = value; }
            get { return _menumemo1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuMemo2
        {
            set { _menumemo2 = value; }
            get { return _menumemo2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuMemo3
        {
            set { _menumemo3 = value; }
            get { return _menumemo3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuMemo4
        {
            set { _menumemo4 = value; }
            get { return _menumemo4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string menuMemo5
        {
            set { _menumemo5 = value; }
            get { return _menumemo5; }
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
        public int? createUser
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
        public int? menuStatus
        {
            set { _menustatus = value; }
            get { return _menustatus; }
        }
        #endregion Model
    }
}
