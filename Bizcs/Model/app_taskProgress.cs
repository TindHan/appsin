namespace appsin.Bizcs.Model
{
    public class app_taskProgress
    {
        public app_taskProgress()
        { }
        #region Model
        private int _progressid;
        private int? _taskid;
        private int? _psnid;
        private int? _progressvalue;
        private string _progresscontent;
        private DateTime? _createtime;
        private int? _createuser;
        private int? _progressstatus;
        /// <summary>
        /// 
        /// </summary>
        public int progressID
        {
            set { _progressid = value; }
            get { return _progressid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? taskID
        {
            set { _taskid = value; }
            get { return _taskid; }
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
        public int? progressValue
        {
            set { _progressvalue = value; }
            get { return _progressvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string progressContent
        {
            set { _progresscontent = value; }
            get { return _progresscontent; }
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
        public int? progressStatus
        {
            set { _progressstatus = value; }
            get { return _progressstatus; }
        }
        #endregion Model
    }
}
