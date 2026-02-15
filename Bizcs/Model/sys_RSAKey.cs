namespace appsin.Bizcs.Model
{
    public class sys_RSAKey
    {
        #region Model
        private int _keyid;
        private string _wkey;
        private string _nkey;
        private DateTime? _createtime;
        private int? _createuser;
        private string _createfor;
        /// <summary>
        /// 
        /// </summary>
        public int KeyID
        {
            set { _keyid = value; }
            get { return _keyid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string wkey
        {
            set { _wkey = value; }
            get { return _wkey; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nkey
        {
            set { _nkey = value; }
            get { return _nkey; }
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
        public int? createUser
        {
            set { _createuser = value; }
            get { return _createuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createFor
        {
            set { _createfor = value; }
            get { return _createfor; }
        }
        #endregion Model
    }
}
