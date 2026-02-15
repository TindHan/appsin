namespace appsin.Bizcs.Model
{
    public class api_useLog
    {
        public api_useLog()
        { }
        #region Model
        private int _logid;
        private int? _appid;
        private int? _apiid;
        private int? _osrzid;
        private DateTime? _createtime;
        private string _appdomain;
        private string _iss;
        private string _logmemo;
        private string _inpara;
        private string _outpara;
        /// <summary>
        /// 
        /// </summary>
        public int logID
        {
            set { _logid = value; }
            get { return _logid; }
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
        public int? apiID
        {
            set { _apiid = value; }
            get { return _apiid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? osrzID
        {
            set { _osrzid = value; }
            get { return _osrzid; }
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
        public string appDomain
        {
            set { _appdomain = value; }
            get { return _appdomain; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isS
        {
            set { _iss = value; }
            get { return _iss; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string logMemo
        {
            set { _logmemo = value; }
            get { return _logmemo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string inPara
        {
            set { _inpara = value; }
            get { return _inpara; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string outPara
        {
            set { _outpara = value; }
            get { return _outpara; }
        }
        #endregion Model
    }
}
