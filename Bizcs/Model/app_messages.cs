namespace appsin.Bizcs.Model
{
    public class app_messages
    {
        public app_messages()
        { }
        #region Model
        private int _msgid;
        private int _appid;
        private string _objtype;
        private int _objid;
        private string _biztype;
        private int _bizid;
        private string _msgtype;
        private string _msgtitle;
        private string _msgcontent;
        private string _msgurl;
        private DateTime? _createtime;
        private DateTime? _expiretime;
        private string _msgdesc;
        private int _msgstatus;
        /// <summary>
        /// 
        /// </summary>
        public int msgID
        {
            set { _msgid = value; }
            get { return _msgid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int appID
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
        public int objID
        {
            set { _objid = value; }
            get { return _objid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string bizType
        {
            set { _biztype = value; }
            get { return _biztype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int bizID
        {
            set { _bizid = value; }
            get { return _bizid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string msgType
        {
            set { _msgtype = value; }
            get { return _msgtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string msgTitle
        {
            set { _msgtitle = value; }
            get { return _msgtitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string msgContent
        {
            set { _msgcontent = value; }
            get { return _msgcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string msgUrl
        {
            set { _msgurl = value; }
            get { return _msgurl; }
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
        public string msgDesc
        {
            set { _msgdesc = value; }
            get { return _msgdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int msgStatus
        {
            set { _msgstatus = value; }
            get { return _msgstatus; }
        }
        #endregion Model
    }
}
