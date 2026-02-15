namespace appsin.Bizcs.Model
{
    public class flow_approveLog
    {
        public flow_approveLog()
        { }
        #region Model
        private int _approveid;
        private string _approvepk;
        private int _instanceid;
        private string _instancepk;
        private int _nodeid;
        private string _nodepk;
        private int _approverid;
        private string _approverpk;
        private string _approverunit;
        private string _approverdept;
        private string _approverpost;
        private int _isagree;
        private string _isnote;
        private int? _istimes;
        private DateTime _approvetime;
        /// <summary>
        /// 
        /// </summary>
        public int approveID
        {
            set { _approveid = value; }
            get { return _approveid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string approvePK
        {
            set { _approvepk = value; }
            get { return _approvepk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int instanceID
        {
            set { _instanceid = value; }
            get { return _instanceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string instancePK
        {
            set { _instancepk = value; }
            get { return _instancepk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int nodeID
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nodePK
        {
            set { _nodepk = value; }
            get { return _nodepk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int approverID
        {
            set { _approverid = value; }
            get { return _approverid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string approverPK
        {
            set { _approverpk = value; }
            get { return _approverpk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string approverUnit
        {
            set { _approverunit = value; }
            get { return _approverunit; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string approverDept
        {
            set { _approverdept = value; }
            get { return _approverdept; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string approverPost
        {
            set { _approverpost = value; }
            get { return _approverpost; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isAgree
        {
            set { _isagree = value; }
            get { return _isagree; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isNote
        {
            set { _isnote = value; }
            get { return _isnote; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isTimes
        {
            set { _istimes = value; }
            get { return _istimes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime approveTime
        {
            set { _approvetime = value; }
            get { return _approvetime; }
        }
        #endregion Model
    }
}
