namespace appsin.Bizcs.Model
{
    public class psn_captcha
    {
        public psn_captcha()
        { }
        #region Model
        private int _capid;
        private int? _psnid;
        private string _captchastr;
        private DateTime? _createtime;
        private DateTime? _verifytime;
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
        public int? psnID
        {
            set { _psnid = value; }
            get { return _psnid; }
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
        #endregion Model
    }
}
