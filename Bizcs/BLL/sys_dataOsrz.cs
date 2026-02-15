using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class sys_dataOsrz
    {
        private readonly Bizcs.DAL.sys_dataOsrz dal = new Bizcs.DAL.sys_dataOsrz();
        public sys_dataOsrz()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_dataOsrz model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.sys_dataOsrz model)
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
        public Bizcs.Model.sys_dataOsrz GetModel(int osrzID)
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
        public List<Bizcs.Model.sys_dataOsrz> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.sys_dataOsrz> DataTableToList(DataTable dt)
        {
            List<Bizcs.Model.sys_dataOsrz> modelList = new List<Bizcs.Model.sys_dataOsrz>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Bizcs.Model.sys_dataOsrz model;
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
        public DataSet GetObjecList()
        {
            return dal.GetObjecList();
        }

        public DataSet getAllBind(int osrzStatus)
        {
            return dal.getAllBind(osrzStatus);
        }
        public DataSet GetAllBindByPage(string osrzStatus, string orderby, string strWhere, int startIndex, int endIndex)
        {
            return dal.GetAllBindByPage(osrzStatus, orderby, strWhere, startIndex, endIndex);
        }
        #endregion  ExtensionMethod
    }
}
