using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class sys_menu
    {
        private readonly Bizcs.DAL.sys_menu dal = new Bizcs.DAL.sys_menu();
        public sys_menu()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_menu model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.sys_menu model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int menuID)
        {

            return dal.Delete(menuID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.sys_menu GetModel(int menuID)
        {

            return dal.GetModel(menuID);
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
        public List<Bizcs.Model.sys_menu> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.sys_menu> DataTableToList(DataTable dt)
        {
            List<Bizcs.Model.sys_menu> modelList = new List<Bizcs.Model.sys_menu>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Bizcs.Model.sys_menu model;
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
        public DataSet GetPsnRole(int psnID)
        {
            return dal.GetPsnRole(psnID);
        }
        public DataSet GetAdminMenu()
        {
            return dal.GetAdminMenu();
        }
        public DataSet GetOsrzMenu(int psnID)
        {
            return dal.GetOsrzMenu(psnID);
        }

        public DataSet GetOsrzMobMenu(int psnID)
        {
            return dal.GetOsrzMobMenu(psnID);
        }
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms)
        {
            return dal.GetSimpleList(strWhere,parms);
        }

        public DataSet GetSearchList(string kw)
        {
            return dal.GetSearchList(kw);
        }

        public DataSet GetSearchAdmin(string kw)
        {
            return dal.GetSearchAdmin(kw);
        }
        #endregion  ExtensionMethod
    }
}
