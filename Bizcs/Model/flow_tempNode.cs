namespace appsin.Bizcs.Model
{
    public class flow_tempNode
    {
        public flow_tempNode()
        { }
        #region Model
        private int _nodeid;
        private int? _templateid;
        private string _nodepk;
        private string _nodename;
        private string _prevnodepk;
        private int? _isend;
        private string _approve;
        private string _condition;
        private decimal? _left;
        private decimal? _top;
        private string _type;
        private int? _createuser;
        private DateTime? _createtime;
        private int? _nodestatus;
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
        public int? templateID
        {
            set { _templateid = value; }
            get { return _templateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nodePk
        {
            set { _nodepk = value; }
            get { return _nodepk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nodeName
        {
            set { _nodename = value; }
            get { return _nodename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string prevNodePK
        {
            set { _prevnodepk = value; }
            get { return _prevnodepk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? isEnd
        {
            set { _isend = value; }
            get { return _isend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string approve
        {
            set { _approve = value; }
            get { return _approve; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string condition
        {
            set { _condition = value; }
            get { return _condition; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? left
        {
            set { _left = value; }
            get { return _left; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? top
        {
            set { _top = value; }
            get { return _top; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
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
        public int? nodeStatus
        {
            set { _nodestatus = value; }
            get { return _nodestatus; }
        }
        #endregion Model
    }
}
