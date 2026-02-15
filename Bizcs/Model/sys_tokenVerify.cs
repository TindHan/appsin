namespace appsin.Bizcs.Model
{
    public class sys_tokenVerify
    {
        public sys_tokenVerify()
        { }
        #region Model
        private int _verifyid;
        private int? _tokenid;
        private int? _appid;
        private DateTime? _verifytime;
        private string _verifyresult;
        /// <summary>
        /// 
        /// </summary>
        public int verifyID
        {
            set { _verifyid = value; }
            get { return _verifyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? tokenID
        {
            set { _tokenid = value; }
            get { return _tokenid; }
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
        public DateTime? verifyTime
        {
            set { _verifytime = value; }
            get { return _verifytime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string verifyResult
        {
            set { _verifyresult = value; }
            get { return _verifyresult; }
        }
        #endregion Model

    }
}
