namespace appsin.Bizcs.Model
{
    public class psn_transfer
    {
        public psn_transfer()
        { }
        #region Model
        private int _transferid;
        private int? _psnid;
        private string _transfertype;
        private string _preunitname;
        private string _predeptname;
        private string _prepostname;
        private string _preontype;
        private string _preonstatus;
        private string _nextunitname;
        private string _nextdeptname;
        private string _nextpostname;
        private string _nextontype;
        private string _nextonstatus;
        private DateTime? _transfertime;
        private string _transfermemo1;
        private string _transfermemo2;
        private string _transfermemo3;
        private string _transfermemo4;
        private string _transfermemo5;
        private DateTime? _createtime;
        private int? _createuser;
        private int _transferstatus;
        /// <summary>
        /// 
        /// </summary>
        public int transferID
        {
            set { _transferid = value; }
            get { return _transferid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? psnID
        {
            set { _psnid = value; }
            get { return _psnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string transferType
        {
            set { _transfertype = value; }
            get { return _transfertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string preUnitName
        {
            set { _preunitname = value; }
            get { return _preunitname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string preDeptName
        {
            set { _predeptname = value; }
            get { return _predeptname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string prePostName
        {
            set { _prepostname = value; }
            get { return _prepostname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string preOnType
        {
            set { _preontype = value; }
            get { return _preontype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string preOnStatus
        {
            set { _preonstatus = value; }
            get { return _preonstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nextUnitName
        {
            set { _nextunitname = value; }
            get { return _nextunitname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nextDeptName
        {
            set { _nextdeptname = value; }
            get { return _nextdeptname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nextPostName
        {
            set { _nextpostname = value; }
            get { return _nextpostname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nextOnType
        {
            set { _nextontype = value; }
            get { return _nextontype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nextOnStatus
        {
            set { _nextonstatus = value; }
            get { return _nextonstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? transferTime
        {
            set { _transfertime = value; }
            get { return _transfertime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string transferMemo1
        {
            set { _transfermemo1 = value; }
            get { return _transfermemo1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string transferMemo2
        {
            set { _transfermemo2 = value; }
            get { return _transfermemo2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string transferMemo3
        {
            set { _transfermemo3 = value; }
            get { return _transfermemo3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string transferMemo4
        {
            set { _transfermemo4 = value; }
            get { return _transfermemo4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string transferMemo5
        {
            set { _transfermemo5 = value; }
            get { return _transfermemo5; }
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
        public int transferStatus
        {
            set { _transferstatus = value; }
            get { return _transferstatus; }
        }
        #endregion Model
    }
}
