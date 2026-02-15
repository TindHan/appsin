namespace appsin.Bizcs.Model
{
    public class app_goToLog
    {
        public app_goToLog()
        { }
        #region Model
        private int _logid;
        private int? _psnid;
        private int? _appid;
        private int? _menuid;
        private string _gostr;
        private DateTime? _gotime;
        private DateTime? _callbacktime;
        private string _callbackres;
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
        public int? menuID
        {
            set { _menuid = value; }
            get { return _menuid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string goStr
        {
            set { _gostr = value; }
            get { return _gostr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? goTime
        {
            set { _gotime = value; }
            get { return _gotime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? callBackTime
        {
            set { _callbacktime = value; }
            get { return _callbacktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string callBackRes
        {
            set { _callbackres = value; }
            get { return _callbackres; }
        }
        #endregion Model
    }
}
