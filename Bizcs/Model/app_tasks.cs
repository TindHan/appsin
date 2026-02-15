namespace appsin.Bizcs.Model
{
    public class app_tasks
    {
        public app_tasks()
        { }
        #region Model
        private int _taskid;
        private int? _parenttaskid;
        private int? _psnid;
        private int? _assignpsnid;
        private string _tasktitle;
        private string _taskcontent;
        private DateTime? _taskdeadline;
        private int? _taskprogress;
        private int? _taskassessresult;
        private DateTime? _taskassesstime;
        private int? _tasksupervisor;
        private DateTime? _taskcreatetime;
        private int? _taskstatus;
        /// <summary>
        /// 
        /// </summary>
        public int taskID
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? parentTaskID
        {
            set { _parenttaskid = value; }
            get { return _parenttaskid; }
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
        public int? assignPsnID
        {
            set { _assignpsnid = value; }
            get { return _assignpsnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string taskTitle
        {
            set { _tasktitle = value; }
            get { return _tasktitle; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string taskContent
        {
            set { _taskcontent = value; }
            get { return _taskcontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? taskDeadline
        {
            set { _taskdeadline = value; }
            get { return _taskdeadline; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? taskProgress
        {
            set { _taskprogress = value; }
            get { return _taskprogress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? taskAssessResult
        {
            set { _taskassessresult = value; }
            get { return _taskassessresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? taskAssessTime
        {
            set { _taskassesstime = value; }
            get { return _taskassesstime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? taskSupervisor
        {
            set { _tasksupervisor = value; }
            get { return _tasksupervisor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? taskCreateTime
        {
            set { _taskcreatetime = value; }
            get { return _taskcreatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? taskStatus
        {
            set { _taskstatus = value; }
            get { return _taskstatus; }
        }
        #endregion Model
    }
}
