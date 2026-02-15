using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class flow_tempNode
    {
        private readonly appsin.Bizcs.DAL.flow_tempNode dal = new appsin.Bizcs.DAL.flow_tempNode();
        public flow_tempNode()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.flow_tempNode model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(appsin.Bizcs.Model.flow_tempNode model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int nodeID)
        {

            return dal.Delete(nodeID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public appsin.Bizcs.Model.flow_tempNode GetModel(int nodeID)
        {

            return dal.GetModel(nodeID);
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
        public List<appsin.Bizcs.Model.flow_tempNode> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<appsin.Bizcs.Model.flow_tempNode> DataTableToList(DataTable dt)
        {
            List<appsin.Bizcs.Model.flow_tempNode> modelList = new List<appsin.Bizcs.Model.flow_tempNode>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                appsin.Bizcs.Model.flow_tempNode model;
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
        public DataSet GetNodeList(string strWhere, params SqlParameter[] parms)
        {
            return dal.GetNodeList(strWhere, parms);
        }
        public bool DeleteBeforeSave(int templateID)
        {
            return dal.DeleteBeforeSave(templateID);
        }
        #endregion  ExtensionMethod
    }
}
