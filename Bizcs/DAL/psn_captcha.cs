using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class psn_captcha
    {
        public psn_captcha()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.psn_captcha model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into psn_captcha(");
            strSql.Append("psnID,captchaStr,createTime,verifyTime)");
            strSql.Append(" values (");
            strSql.Append("@psnID,@captchaStr,@createTime,@verifyTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@captchaStr", SqlDbType.VarChar,50),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@verifyTime", SqlDbType.DateTime)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.captchaStr;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.verifyTime;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.psn_captcha model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update psn_captcha set ");
            strSql.Append("psnID=@psnID,");
            strSql.Append("captchaStr=@captchaStr,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("verifyTime=@verifyTime");
            strSql.Append(" where capID=@capID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@captchaStr", SqlDbType.VarChar,50),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@verifyTime", SqlDbType.DateTime),
                    new SqlParameter("@capID", SqlDbType.Int,4)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.captchaStr;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.verifyTime;
            parameters[4].Value = model.capID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int capID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from psn_captcha ");
            strSql.Append(" where capID=@capID");
            SqlParameter[] parameters = {
                    new SqlParameter("@capID", SqlDbType.Int,4)
            };
            parameters[0].Value = capID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.psn_captcha GetModel(int capID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 capID,psnID,captchaStr,createTime,verifyTime from psn_captcha ");
            strSql.Append(" where capID=@capID");
            SqlParameter[] parameters = {
                    new SqlParameter("@capID", SqlDbType.Int,4)
            };
            parameters[0].Value = capID;

            Bizcs.Model.psn_captcha model = new Bizcs.Model.psn_captcha();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.psn_captcha DataRowToModel(DataRow row)
        {
            Bizcs.Model.psn_captcha model = new Bizcs.Model.psn_captcha();
            if (row != null)
            {
                if (row["capID"] != null && row["capID"].ToString() != "")
                {
                    model.capID = int.Parse(row["capID"].ToString());
                }
                if (row["psnID"] != null && row["psnID"].ToString() != "")
                {
                    model.psnID = int.Parse(row["psnID"].ToString());
                }
                if (row["captchaStr"] != null)
                {
                    model.captchaStr = row["captchaStr"].ToString();
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["verifyTime"] != null && row["verifyTime"].ToString() != "")
                {
                    model.verifyTime = DateTime.Parse(row["verifyTime"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select capID,psnID,captchaStr,createTime,verifyTime ");
            strSql.Append(" FROM psn_captcha ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),parms);
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.capID desc");
            }
            strSql.Append(")AS Row, T.*  from psn_captcha T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }

        #endregion  BasicMethod
        #region  ExtensionMethod
        public Bizcs.Model.psn_captcha GetIsCaptcha(int psnID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 capID,psnID,captchaStr,createTime,verifyTime from psn_captcha ");
            strSql.Append(" where psnID=@psnID and DATEADD(mi,5,createTime)>getdate() order by capID desc");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;

            Bizcs.Model.psn_captcha model = new Bizcs.Model.psn_captcha();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        #endregion  ExtensionMethod
    }
}
