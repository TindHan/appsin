namespace appsin.Bizcs.Model
{
    public class api_apiOsrz
    {
        public api_apiOsrz()
        { }
        #region Model
        private int _osrzid;
        private int? _appid;
        private int? _apiid;
        private DateTime? _validstarttime;
        private DateTime? _validendtime;
        private string _osrzdescription;
        private string _osrzmemo1;
        private string _osrzmemo2;
        private string _osrzmemo3;
        private string _osrzmemo4;
        private string _osrzmemo5;
        private int? _displayorder;
        private int? _createuser;
        private DateTime? _createtime;
        private int? _osrzstatus;
        /// <summary>
        /// 
        /// </summary>
        public int osrzID
        {
            set { _osrzid = value; }
            get { return _osrzid; }
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
        public DateTime? validStartTime
        {
            set { _validstarttime = value; }
            get { return _validstarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? validEndTime
        {
            set { _validendtime = value; }
            get { return _validendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string osrzDescription
        {
            set { _osrzdescription = value; }
            get { return _osrzdescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string osrzMemo1
        {
            set { _osrzmemo1 = value; }
            get { return _osrzmemo1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string osrzMemo2
        {
            set { _osrzmemo2 = value; }
            get { return _osrzmemo2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string osrzMemo3
        {
            set { _osrzmemo3 = value; }
            get { return _osrzmemo3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string osrzMemo4
        {
            set { _osrzmemo4 = value; }
            get { return _osrzmemo4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string osrzMemo5
        {
            set { _osrzmemo5 = value; }
            get { return _osrzmemo5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? displayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
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
        public DateTime? createTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? osrzStatus
        {
            set { _osrzstatus = value; }
            get { return _osrzstatus; }
        }
        #endregion Model
    }
}
