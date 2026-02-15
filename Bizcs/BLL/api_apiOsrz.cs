using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class api_apiOsrz
    {
        private readonly Bizcs.DAL.api_apiOsrz dal = new Bizcs.DAL.api_apiOsrz();
        public api_apiOsrz()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.api_apiOsrz model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.api_apiOsrz model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int osrzID)
        {

            return dal.Delete(osrzID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.api_apiOsrz GetModel(int osrzID)
        {

            return dal.GetModel(osrzID);
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
        public List<Bizcs.Model.api_apiOsrz> GetModelList(string strWhere, params SqlParameter[] parms)
        {
            DataSet ds = dal.GetList(strWhere, parms);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.api_apiOsrz> DataTableToList(DataTable dt)
        {
            List<Bizcs.Model.api_apiOsrz> modelList = new List<Bizcs.Model.api_apiOsrz>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Bizcs.Model.api_apiOsrz model;
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
        public Bizcs.Model.api_apiOsrz GetModelByID(int appID, int apiID)
        {
            return dal.GetModelByID(appID, apiID);
        }
        public Bizcs.Model.api_apiOsrz GetModelByCode(string apiOode, string appSID)
        {
            return dal.GetModelByCode(apiOode, appSID);
        }
        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            return dal.GetSimpleListByPage(strWhere.Trim(), orderby, startIndex, endIndex, parms);
        }
        #endregion  ExtensionMethod
    }
}
