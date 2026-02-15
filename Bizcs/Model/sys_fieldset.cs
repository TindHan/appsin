namespace appsin.Bizcs.Model
{
    public class sys_fieldset
    {
        public sys_fieldset()
        { }
        #region Model
        private int _setid;
        private string _setpk;
        private string _setname;
        private string _setdescription;
        private string _settype;
        private string _setcode;
        private int? _setlevel;
        private string _setmemo1;
        private string _setmemo2;
        private string _setmemo3;
        private string _setmemo4;
        private string _setmemo5;
        private int? _displayorder;
        private DateTime? _createtime;
        private int? _createuser;
        private int? _setstatus;
        /// <summary>
        /// 
        /// </summary>
        public int setID
        {
            set { _setid = value; }
            get { return _setid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setPK
        {
            set { _setpk = value; }
            get { return _setpk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setName
        {
            set { _setname = value; }
            get { return _setname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setDescription
        {
            set { _setdescription = value; }
            get { return _setdescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setType
        {
            set { _settype = value; }
            get { return _settype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setCode
        {
            set { _setcode = value; }
            get { return _setcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? setLevel
        {
            set { _setlevel = value; }
            get { return _setlevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setMemo1
        {
            set { _setmemo1 = value; }
            get { return _setmemo1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setMemo2
        {
            set { _setmemo2 = value; }
            get { return _setmemo2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setMemo3
        {
            set { _setmemo3 = value; }
            get { return _setmemo3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setMemo4
        {
            set { _setmemo4 = value; }
            get { return _setmemo4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setMemo5
        {
            set { _setmemo5 = value; }
            get { return _setmemo5; }
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
        public int? setStatus
        {
            set { _setstatus = value; }
            get { return _setstatus; }
        }
        #endregion Model
    }
}
