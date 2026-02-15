using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.BLL
{
    public class sys_fieldItem
    {
        private readonly Bizcs.DAL.sys_fieldItem dal = new Bizcs.DAL.sys_fieldItem();
        public sys_fieldItem()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_fieldItem model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.sys_fieldItem model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int itemID)
        {

            return dal.Delete(itemID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.sys_fieldItem GetModel(int itemID)
        {
            return dal.GetModel(itemID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, params SqlParameter[] parms)
        {
            return dal.GetList(strWhere, parms);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.sys_fieldItem> GetModelList(string strWhere, params SqlParameter[] parms)
        {
            DataSet ds = dal.GetList(strWhere, parms);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.sys_fieldItem> DataTableToList(DataTable dt)
        {
            List<Bizcs.Model.sys_fieldItem> modelList = new List<Bizcs.Model.sys_fieldItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Bizcs.Model.sys_fieldItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex, parms);
        }

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataSet GetServItem(string itemPK)
        {
            return dal.GetServItem(itemPK);
        }
        public int GetTypeIDByStr(string typeStr)
        {
            return dal.GetTypeIDByStr(typeStr);
        }
        public int GetItemIDByStr(string itemStr)
        {
            return dal.GetItemIDByStr(itemStr);
        }
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms)
        { return dal.GetSimpleList(strWhere, parms); }
        #endregion  ExtensionMethod
    }
}
