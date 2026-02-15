namespace appsin.Bizcs.Model
{
    public class app_notices
    {
        public app_notices()
        { }
        #region Model
        private int _noticeid;
        private int? _appid;
        private string _objtype;
        private int? _objid;
        private string _noticetitle;
        private string _noticecontent;
        private string _noticefile;
        private string _storefile;
        private DateTime? _createtime;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private int? _createuser;
        private int? _noticestatus;
        /// <summary>
        /// 
        /// </summary>
        public int noticeID
        {
            set { _noticeid = value; }
            get { return _noticeid; }
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
        public string objType
        {
            set { _objtype = value; }
            get { return _objtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? objID
        {
            set { _objid = value; }
            get { return _objid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string noticeTitle
        {
            set { _noticetitle = value; }
            get { return _noticetitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string noticeContent
        {
            set { _noticecontent = value; }
            get { return _noticecontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string noticeFile
        {
            set { _noticefile = value; }
            get { return _noticefile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string storeFile
        {
            set { _storefile = value; }
            get { return _storefile; }
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
        public DateTime? startTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? endTime
        {
            set { _endtime = value; }
            get { return _endtime; }
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
        public int? noticeStatus
        {
            set { _noticestatus = value; }
            get { return _noticestatus; }
        }
        #endregion Model
    }
}
