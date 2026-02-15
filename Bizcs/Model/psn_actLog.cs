namespace appsin.Bizcs.Model
{
    public class psn_actLog
    {
        public psn_actLog()
        { }
        #region Model
        private int _logid;
        private int? _psnid;
        private DateTime? _logtime;
        private string _logaction;
        private string _issuccess;
        private string _actdvsip;
        private string _actdvscode;
        private string _actpara;
        private string _actresult;
        private string _actmemo;
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
        public DateTime? logTime
        {
            set { _logtime = value; }
            get { return _logtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string logAction
        {
            set { _logaction = value; }
            get { return _logaction; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string isSuccess
        {
            set { _issuccess = value; }
            get { return _issuccess; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string actDvsIP
        {
            set { _actdvsip = value; }
            get { return _actdvsip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string actDvsCode
        {
            set { _actdvscode = value; }
            get { return _actdvscode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string actPara
        {
            set { _actpara = value; }
            get { return _actpara; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string actResult
        {
            set { _actresult = value; }
            get { return _actresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string actMemo
        {
            set { _actmemo = value; }
            get { return _actmemo; }
        }
        #endregion Model
    }
}
