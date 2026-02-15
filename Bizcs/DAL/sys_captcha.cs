using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_captcha
    {
        public sys_captcha()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.sys_captcha model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_captcha(");
            strSql.Append("adminID,captchaStr,createTime,verifyTime,captchaDesc)");
            strSql.Append(" values (");
            strSql.Append("@adminID,@captchaStr,@createTime,@verifyTime,@captchaDesc)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@adminID", SqlDbType.Int,4),
                    new SqlParameter("@captchaStr", SqlDbType.VarChar,50),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@verifyTime", SqlDbType.DateTime),
                    new SqlParameter("@captchaDesc", SqlDbType.VarChar,150)};
            parameters[0].Value = model.adminID;
            parameters[1].Value = model.captchaStr;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.verifyTime;
            parameters[4].Value = model.captchaDesc;

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
        public bool Update(appsin.Bizcs.Model.sys_captcha model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_captcha set ");
            strSql.Append("adminID=@adminID,");
            strSql.Append("captchaStr=@captchaStr,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("verifyTime=@verifyTime,");
            strSql.Append("captchaDesc=@captchaDesc");
            strSql.Append(" where capID=@capID");
            SqlParameter[] parameters = {
                    new SqlParameter("@adminID", SqlDbType.Int,4),
                    new SqlParameter("@captchaStr", SqlDbType.VarChar,50),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@verifyTime", SqlDbType.DateTime),
                    new SqlParameter("@captchaDesc", SqlDbType.VarChar,150),
                    new SqlParameter("@capID", SqlDbType.Int,4)};
            parameters[0].Value = model.adminID;
            parameters[1].Value = model.captchaStr;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.verifyTime;
            parameters[4].Value = model.captchaDesc;
            parameters[5].Value = model.capID;

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
            strSql.Append("delete from sys_captcha ");
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
        public appsin.Bizcs.Model.sys_captcha GetModel(int capID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 capID,adminID,captchaStr,createTime,verifyTime,captchaDesc from sys_captcha ");
            strSql.Append(" where capID=@capID");
            SqlParameter[] parameters = {
                    new SqlParameter("@capID", SqlDbType.Int,4)
            };
            parameters[0].Value = capID;

            appsin.Bizcs.Model.sys_captcha model = new appsin.Bizcs.Model.sys_captcha();
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
        public appsin.Bizcs.Model.sys_captcha DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.sys_captcha model = new appsin.Bizcs.Model.sys_captcha();
            if (row != null)
            {
                if (row["capID"] != null && row["capID"].ToString() != "")
                {
                    model.capID = int.Parse(row["capID"].ToString());
                }
                if (row["adminID"] != null && row["adminID"].ToString() != "")
                {
                    model.adminID = int.Parse(row["adminID"].ToString());
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
                if (row["captchaDesc"] != null)
                {
                    model.captchaDesc = row["captchaDesc"].ToString();
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
            strSql.Append("select capID,adminID,captchaStr,createTime,verifyTime,captchaDesc ");
            strSql.Append(" FROM sys_captcha ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
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
            strSql.Append(")AS Row, T.*  from sys_captcha T ");
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
        
        public appsin.Bizcs.Model.sys_captcha GetValidModel(string captchaStr)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 capID,adminID,captchaStr,createTime,verifyTime,captchaDesc from sys_captcha ");
            strSql.Append(" where captchaStr=@captchaStr and createTime >= DATEADD(MINUTE, -2, GETDATE()) AND createTime <= GETDATE()");
            SqlParameter[] parameters = {
                    new SqlParameter("@captchaStr", SqlDbType.VarChar,50)
            };
            parameters[0].Value = captchaStr;

            appsin.Bizcs.Model.sys_captcha model = new appsin.Bizcs.Model.sys_captcha();
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
