namespace appsin.Bizcs.Model
{
    public class flow_template
    {
        public flow_template()
        { }
        #region Model
        private int _templateid;
        private string _templatepk;
        private string _templatename;
        private string _templatedesc;
        private string _str1title;
        private string _str2title;
        private string _str3title;
        private string _int1title;
        private string _int2title;
        private string _int3title;
        private string _date1title;
        private string _date2title;
        private string _date3title;
        private int? _createuser;
        private DateTime? _createtime;
        private int? _isready;
        private int? _displayorder;
        private int? _templatestatus;
        /// <summary>
        /// 
        /// </summary>
        public int templateID
        {
            set { _templateid = value; }
            get { return _templateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string templatePK
        {
            set { _templatepk = value; }
            get { return _templatepk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string templateName
        {
            set { _templatename = value; }
            get { return _templatename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string templateDesc
        {
            set { _templatedesc = value; }
            get { return _templatedesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string str1Title
        {
            set { _str1title = value; }
            get { return _str1title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string str2Title
        {
            set { _str2title = value; }
            get { return _str2title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string str3Title
        {
            set { _str3title = value; }
            get { return _str3title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string int1Title
        {
            set { _int1title = value; }
            get { return _int1title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string int2Title
        {
            set { _int2title = value; }
            get { return _int2title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string int3Title
        {
            set { _int3title = value; }
            get { return _int3title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string date1Title
        {
            set { _date1title = value; }
            get { return _date1title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string date2Title
        {
            set { _date2title = value; }
            get { return _date2title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string date3Title
        {
            set { _date3title = value; }
            get { return _date3title; }
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
        public int? isReady
        {
            set { _isready = value; }
            get { return _isready; }
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
        public int? templateStatus
        {
            set { _templatestatus = value; }
            get { return _templatestatus; }
        }
        #endregion Model
    }
}
