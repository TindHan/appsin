using Microsoft.Data.SqlClient;
using System.Data;

namespace appsin.Bizcs.BLL
{
    public class psn_captcha
    {
        private readonly Bizcs.DAL.psn_captcha dal = new Bizcs.DAL.psn_captcha();
        public psn_captcha()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.psn_captcha model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.psn_captcha model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int capID)
        {

            return dal.Delete(capID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.psn_captcha GetModel(int capID)
        {

            return dal.GetModel(capID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, params SqlParameter[] parms)
        {
            return dal.GetList(strWhere,parms);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.psn_captcha> GetModelList(string strWhere, params SqlParameter[] parms)
        {
            DataSet ds = dal.GetList(strWhere,parms);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Bizcs.Model.psn_captcha> DataTableToList(DataTable dt)
        {
            List<Bizcs.Model.psn_captcha> modelList = new List<Bizcs.Model.psn_captcha>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Bizcs.Model.psn_captcha model;
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
        public Bizcs.Model.psn_captcha GetIsCaptcha(int psnID)
        {
            return dal.GetIsCaptcha(psnID);
        }
        #endregion  ExtensionMethod
    }
}
