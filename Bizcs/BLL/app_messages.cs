using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class app_messages
    {
        private readonly appsin.Bizcs.DAL.app_messages dal = new appsin.Bizcs.DAL.app_messages();
        public app_messages()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.app_messages model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(appsin.Bizcs.Model.app_messages model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int msgID)
        {

            return dal.Delete(msgID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public appsin.Bizcs.Model.app_messages GetModel(int msgID)
        {

            return dal.GetModel(msgID);
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
        public List<appsin.Bizcs.Model.app_messages> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<appsin.Bizcs.Model.app_messages> DataTableToList(DataTable dt)
        {
            List<appsin.Bizcs.Model.app_messages> modelList = new List<appsin.Bizcs.Model.app_messages>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                appsin.Bizcs.Model.app_messages model;
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

        public DataSet GetSimpleList(string strWhere, int psnID, params SqlParameter[] parms)
        {
            return dal.GetSimpleList(strWhere, psnID, parms);
        }

        public DataSet GetSimpleListByPage(string strWhere, int psnID, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            return dal.GetSimpleListByPage(strWhere,psnID,orderby, startIndex, endIndex, parms);
        }
        #endregion  ExtensionMethod
    }
}
