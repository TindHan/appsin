using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class sys_tokenVerify
    {
        private readonly Bizcs.DAL.sys_tokenVerify dal = new Bizcs.DAL.sys_tokenVerify();
        public sys_tokenVerify()
        { }
        #region  BasicMethod}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_tokenVerify model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.sys_tokenVerify model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int verifyID)
        {

            return dal.Delete(verifyID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.sys_tokenVerify GetModel(int verifyID)
        {

            return dal.GetModel(verifyID);
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
        public List<Bizcs.Model.sys_tokenVerify> GetModelList(string strWhere, params SqlParameter[] parms)
        {
            DataSet ds = dal.GetList(strWhere, parms);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.sys_tokenVerify> DataTableToList(DataTable dt)
        {
            List<Bizcs.Model.sys_tokenVerify> modelList = new List<Bizcs.Model.sys_tokenVerify>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Bizcs.Model.sys_tokenVerify model;
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

        #endregion  ExtensionMethod
    }
}
