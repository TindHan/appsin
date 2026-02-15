using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class flow_template
    {
        private readonly appsin.Bizcs.DAL.flow_template dal = new appsin.Bizcs.DAL.flow_template();
        public flow_template()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.flow_template model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(appsin.Bizcs.Model.flow_template model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int templateID)
        {

            return dal.Delete(templateID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public appsin.Bizcs.Model.flow_template GetModel(int templateID)
        {

            return dal.GetModel(templateID);
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
        public List<appsin.Bizcs.Model.flow_template> GetModelList(string strWhere, params SqlParameter[] parms)
        {
            DataSet ds = dal.GetList(strWhere, parms);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<appsin.Bizcs.Model.flow_template> DataTableToList(DataTable dt)
        {
            List<appsin.Bizcs.Model.flow_template> modelList = new List<appsin.Bizcs.Model.flow_template>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                appsin.Bizcs.Model.flow_template model;
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
        public appsin.Bizcs.Model.flow_template GetModelByPK(string templatePK)
        {
            return dal.GetModelByPK(templatePK);
        }
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms)
        {
            return dal.GetSimpleList(strWhere, parms);
        }
        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            return dal.GetSimpleListByPage(strWhere.Trim(), orderby, startIndex, endIndex, parms);
        }
        #endregion  ExtensionMethod
    }
}
