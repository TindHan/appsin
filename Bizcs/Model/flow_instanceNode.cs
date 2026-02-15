namespace appsin.Bizcs.Model
{
    public class flow_instanceNode
    {
        public flow_instanceNode()
        { }
        #region Model
        private int _nodeid;
        private int _instanceid;
        private string _instancepk;
        private string _nodepk;
        private string _nodename;
        private string _prevnodepk;
        private int? _isend;
        private string _approve;
        private string _condition;
        private decimal? _left;
        private decimal? _top;
        private string _type;
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
        public int? nodeStatus
        {
            set { _nodestatus = value; }
            get { return _nodestatus; }
        }
        #endregion Model
    }
}
