using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class org_orgMain
    {
        private readonly Bizcs.DAL.org_orgMain dal = new Bizcs.DAL.org_orgMain();
        public org_orgMain()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.org_orgMain model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.org_orgMain model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int orgID)
        {

            return dal.Delete(orgID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.org_orgMain GetModel(int orgID)
        {

            return dal.GetModel(orgID);
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
        public List<Bizcs.Model.org_orgMain> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.org_orgMain> DataTableToList(DataTable dt)
        {
            List<Bizcs.Model.org_orgMain> modelList = new List<Bizcs.Model.org_orgMain>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Bizcs.Model.org_orgMain model;
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
        public int GetOrgIDByStr(string orgStr, string orgType)
        {
            return dal.GetOrgIDByStr(orgStr, orgType);
        }
        public DataSet GetOrgList(string orgPK, params SqlParameter[] parms)
        {
            return dal.GetOrgList(orgPK, parms);
        }
        public DataSet GetTreeList(string strWhere, params SqlParameter[] parms)
        {
            return dal.GetTreeList(strWhere, parms);
        }
        public DataSet GetOrgsList(string strWhere, params SqlParameter[] parms)
        {
            return dal.GetOrgsList(strWhere, parms);
        }
        public int cancelOrg(string orgLevel)
        {
            return dal.cancelOrg(orgLevel);
        }
        public Bizcs.Model.org_orgMain GetFullModel(int orgID)
        {
            return dal.GetFullModel(orgID);
        }
        #endregion  ExtensionMethod
    }
}
