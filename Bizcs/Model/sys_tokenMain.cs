namespace appsin.Bizcs.Model
{
    public class sys_tokenMain
    {
        public sys_tokenMain()
        { }
        #region Model
        private int _tokenid;
        private int? _psnid;
        private int? _appid;
        private DateTime? _createtime;
        private DateTime? _expiretime;
        private string _tokenstr;
        private int? _tokenstatus;
        private DateTime? _canceltime;
        /// <summary>
        /// 
        /// </summary>
        public int tokenID
        {
            set { _tokenid = value; }
            get { return _tokenid; }
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
        public int? appID
        {
            set { _appid = value; }
            get { return _appid; }
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
        public DateTime? expireTime
        {
            set { _expiretime = value; }
            get { return _expiretime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string tokenStr
        {
            set { _tokenstr = value; }
            get { return _tokenstr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? tokenStatus
        {
            set { _tokenstatus = value; }
            get { return _tokenstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? cancelTime
        {
            set { _canceltime = value; }
            get { return _canceltime; }
        }
        #endregion Model

    }
}
