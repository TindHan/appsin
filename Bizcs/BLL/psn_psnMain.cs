using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class psn_psnMain
    {
        private readonly appsin.Bizcs.DAL.psn_psnMain dal = new appsin.Bizcs.DAL.psn_psnMain();
        public psn_psnMain()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.psn_psnMain model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(appsin.Bizcs.Model.psn_psnMain model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int psnID)
        {

            return dal.Delete(psnID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public appsin.Bizcs.Model.psn_psnMain GetModel(int psnID)
        {

            return dal.GetModel(psnID);
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
        public List<appsin.Bizcs.Model.psn_psnMain> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<appsin.Bizcs.Model.psn_psnMain> DataTableToList(DataTable dt)
        {
            List<appsin.Bizcs.Model.psn_psnMain> modelList = new List<appsin.Bizcs.Model.psn_psnMain>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                appsin.Bizcs.Model.psn_psnMain model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex,parms);
        }

        #endregion  BasicMethod
        #region  ExtensionMethod
        public Bizcs.Model.psn_psnMain GetModelByPsnUserName(string psnUserName)
        {
            return dal.GetModelByPsnUserName(psnUserName);
        }
        public Bizcs.Model.psn_psnMain GetModelByPsnPK(string psnPK)
        {
            return dal.GetModelByPsnPK(psnPK);
        }
        public DataSet GetListByPsnPK(string psnPK)
        {
            return dal.GetListByPsnPK(psnPK);
        }
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms) {
            return dal.GetSimpleList(strWhere,parms);
        }

        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            return dal.GetSimpleListByPage(strWhere.Trim(), orderby, startIndex, endIndex,parms); 
        }
        public Bizcs.Model.psn_psnMain GetSimpleModel(int psnID)
        {
            return dal.GetSimpleModel(psnID);
        }

        public DataSet GetServList(int psnID)
        {
            return dal.GetServList(psnID);
        }
        #endregion  ExtensionMethod
    }
}
