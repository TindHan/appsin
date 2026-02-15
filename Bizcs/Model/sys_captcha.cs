namespace appsin.Bizcs.Model
{
    public class sys_captcha
    {
        public sys_captcha()
        { }
        #region Model
        private int _capid;
        private int? _adminid;
        private string _captchastr;
        private DateTime? _createtime;
        private DateTime? _verifytime;
        private string _captchadesc;
        /// <summary>
        /// 
        /// </summary>
        public int capID
        {
            set { _capid = value; }
            get { return _capid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? adminID
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string captchaStr
        {
            set { _captchastr = value; }
            get { return _captchastr; }
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
        public DateTime? verifyTime
        {
            set { _verifytime = value; }
            get { return _verifytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string captchaDesc
        {
            set { _captchadesc = value; }
            get { return _captchadesc; }
        }
        #endregion Model
    }
}
