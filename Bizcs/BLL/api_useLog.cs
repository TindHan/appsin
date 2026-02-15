using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class api_useLog
    {
        private readonly Bizcs.DAL.api_useLog dal = new Bizcs.DAL.api_useLog();
        public api_useLog()
        { }
        #region  BasicMethod
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.api_useLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.api_useLog model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int logID)
        {

            return dal.Delete(logID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.api_useLog GetModel(int logID)
        {

            return dal.GetModel(logID);
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
        public List<Bizcs.Model.api_useLog> GetModelList(string strWhere, params SqlParameter[] parms)
        {
            DataSet ds = dal.GetList(strWhere, parms);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.api_useLog> DataTableToList(DataTable dt)
        {
            List<Bizcs.Model.api_useLog> modelList = new List<Bizcs.Model.api_useLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Bizcs.Model.api_useLog model;
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

        public DataSet GetSimpleList(int logID)
        {
            return dal.GetSimpleList(logID);
        }
        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            return dal.GetSimpleListByPage(strWhere.Trim(), orderby, startIndex, endIndex, parms);
        }
        public int AddApiUseLog(int apiID, int appID, int osrzID, string appDomain, string isS, string logMemo, string inPara, string outPara)
        {
            Bizcs.Model.api_useLog useModel = new Model.api_useLog();
            useModel.apiID = apiID;
            useModel.appID = appID;
            useModel.appDomain = appDomain;
            useModel.isS = isS;
            useModel.logMemo = logMemo;
            useModel.inPara = inPara;
            useModel.outPara = outPara;
            useModel.createTime = DateTime.Now;
            return dal.Add(useModel);
        }
        #endregion  ExtensionMethod
    }
}
