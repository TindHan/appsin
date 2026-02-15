using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using appsin.Models;

namespace appsin.Bizcs.DAL
{
    public class sys_RSAKey
    {
        public sys_RSAKey()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_RSAKey model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_RSAKey(");
            strSql.Append("wkey,nkey,createTime,createUser,createFor)");
            strSql.Append(" values (");
            strSql.Append("@wkey,@nkey,@createTime,@createUser,@createFor)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@wkey", SqlDbType.VarChar,4000),
                    new SqlParameter("@nkey", SqlDbType.VarChar,4000),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createFor", SqlDbType.VarChar,100)};
            parameters[0].Value = model.wkey;
            parameters[1].Value = model.nkey;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.createUser;
            parameters[4].Value = model.createFor;

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
        public bool Update(Bizcs.Model.sys_RSAKey model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_RSAKey set ");
            strSql.Append("wkey=@wkey,");
            strSql.Append("nkey=@nkey,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("createFor=@createFor");
            strSql.Append(" where KeyID=@KeyID");
            SqlParameter[] parameters = {
                    new SqlParameter("@wkey", SqlDbType.VarChar,4000),
                    new SqlParameter("@nkey", SqlDbType.VarChar,4000),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createFor", SqlDbType.VarChar,100),
                    new SqlParameter("@KeyID", SqlDbType.Int,4)};
            parameters[0].Value = model.wkey;
            parameters[1].Value = model.nkey;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.createUser;
            parameters[4].Value = model.createFor;
            parameters[5].Value = model.KeyID;

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
        public bool Delete(int KeyID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_RSAKey ");
            strSql.Append(" where KeyID=@KeyID");
            SqlParameter[] parameters = {
                    new SqlParameter("@KeyID", SqlDbType.Int,4)
            };
            parameters[0].Value = KeyID;

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
        public Bizcs.Model.sys_RSAKey GetModel(int KeyID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 KeyID,wkey,nkey,createTime,createUser,createFor from sys_RSAKey ");
            strSql.Append(" where KeyID=@KeyID");
            SqlParameter[] parameters = {
                    new SqlParameter("@KeyID", SqlDbType.Int,4)
            };
            parameters[0].Value = KeyID;

            Bizcs.Model.sys_RSAKey model = new Bizcs.Model.sys_RSAKey();
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
        public Bizcs.Model.sys_RSAKey DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_RSAKey model = new Bizcs.Model.sys_RSAKey();
            if (row != null)
            {
                if (row["KeyID"] != null && row["KeyID"].ToString() != "")
                {
                    model.KeyID = int.Parse(row["KeyID"].ToString());
                }
                if (row["wkey"] != null)
                {
                    model.wkey = row["wkey"].ToString();
                }
                if (row["nkey"] != null)
                {
                    model.nkey = row["nkey"].ToString();
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["createFor"] != null)
                {
                    model.createFor = row["createFor"].ToString();
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
            strSql.Append("select KeyID,wkey,nkey,createTime,createUser,createFor ");
            strSql.Append(" FROM sys_RSAKey ");
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
                strSql.Append("order by T.KeyID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_RSAKey T ");
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
        public Bizcs.Model.sys_RSAKey GetModelByPubkey(string pubkey)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 KeyID,wkey,nkey,createTime,createUser,createFor from sys_RSAKey ");
            strSql.Append(" where wkey=@wkey");
            SqlParameter[] parameters = {
                    new SqlParameter("@wkey", SqlDbType.VarChar,1000)
            };
            parameters[0].Value = pubkey;

            Bizcs.Model.sys_RSAKey model = new Bizcs.Model.sys_RSAKey();
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
