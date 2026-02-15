namespace appsin.Bizcs.Model
{
    public class sys_fieldItem
    {
        public sys_fieldItem()
        { }
        #region Model
        private int _itemid;
        private string _itempk;
        private int? _setid;
        private string _setpk;
        private string _itemname;
        private int? _itemlevel;
        private int? _parentid;
        private string _parentpk;
        private string _itemdescription;
        private string _itemmemo1;
        private string _itemmemo2;
        private string _itemmemo3;
        private string _itemmemo4;
        private string _itemmemo5;
        private int? _displayorder;
        private int? _createuser;
        private DateTime? _createtime;
        private int? _itemstatus;
        /// <summary>
        /// 
        /// </summary>
        public int itemID
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemPK
        {
            set { _itempk = value; }
            get { return _itempk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? setID
        {
            set { _setid = value; }
            get { return _setid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string setPK
        {
            set { _setpk = value; }
            get { return _setpk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemName
        {
            set { _itemname = value; }
            get { return _itemname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? itemLevel
        {
            set { _itemlevel = value; }
            get { return _itemlevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? parentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string parentPK
        {
            set { _parentpk = value; }
            get { return _parentpk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemDescription
        {
            set { _itemdescription = value; }
            get { return _itemdescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemMemo1
        {
            set { _itemmemo1 = value; }
            get { return _itemmemo1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemMemo2
        {
            set { _itemmemo2 = value; }
            get { return _itemmemo2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemMemo3
        {
            set { _itemmemo3 = value; }
            get { return _itemmemo3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemMemo4
        {
            set { _itemmemo4 = value; }
            get { return _itemmemo4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string itemMemo5
        {
            set { _itemmemo5 = value; }
            get { return _itemmemo5; }
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
        public int? itemStatus
        {
            set { _itemstatus = value; }
            get { return _itemstatus; }
        }
        #endregion Model
    }
}
