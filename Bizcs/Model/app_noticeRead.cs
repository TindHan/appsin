namespace appsin.Bizcs.Model
{
    public class app_noticeRead
    {
        public app_noticeRead()
        { }
        #region Model
        private int _readid;
        private int? _noticeid;
        private int? _psnid;
        private DateTime? _readtime;
        private string _readdesc;
        private int? _readstatus;
        /// <summary>
        /// 
        /// </summary>
        public int readID
        {
            set { _readid = value; }
            get { return _readid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? noticeID
        {
            set { _noticeid = value; }
            get { return _noticeid; }
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
        public DateTime? readTime
        {
            set { _readtime = value; }
            get { return _readtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string readDesc
        {
            set { _readdesc = value; }
            get { return _readdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? readStatus
        {
            set { _readstatus = value; }
            get { return _readstatus; }
        }
        #endregion Model
    }
}
