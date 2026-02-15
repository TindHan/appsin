using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class app_messageRead
    {
        public app_messageRead()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.app_messageRead model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into app_messageRead(");
            strSql.Append("MsgID,psnID,readTime,readDesc,readStatus)");
            strSql.Append(" values (");
            strSql.Append("@MsgID,@psnID,@readTime,@readDesc,@readStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@MsgID", SqlDbType.Int,4),
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@readTime", SqlDbType.DateTime),
                    new SqlParameter("@readDesc", SqlDbType.VarChar,50),
                    new SqlParameter("@readStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.MsgID;
            parameters[1].Value = model.psnID;
            parameters[2].Value = model.readTime;
            parameters[3].Value = model.readDesc;
            parameters[4].Value = model.readStatus;

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
        public bool Update(appsin.Bizcs.Model.app_messageRead model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update app_messageRead set ");
            strSql.Append("MsgID=@MsgID,");
            strSql.Append("psnID=@psnID,");
            strSql.Append("readTime=@readTime,");
            strSql.Append("readDesc=@readDesc,");
            strSql.Append("readStatus=@readStatus");
            strSql.Append(" where readID=@readID");
            SqlParameter[] parameters = {
                    new SqlParameter("@MsgID", SqlDbType.Int,4),
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@readTime", SqlDbType.DateTime),
                    new SqlParameter("@readDesc", SqlDbType.VarChar,50),
                    new SqlParameter("@readStatus", SqlDbType.Int,4),
                    new SqlParameter("@readID", SqlDbType.Int,4)};
            parameters[0].Value = model.MsgID;
            parameters[1].Value = model.psnID;
            parameters[2].Value = model.readTime;
            parameters[3].Value = model.readDesc;
            parameters[4].Value = model.readStatus;
            parameters[5].Value = model.readID;

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
        public bool Delete(int readID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from app_messageRead ");
            strSql.Append(" where readID=@readID");
            SqlParameter[] parameters = {
                    new SqlParameter("@readID", SqlDbType.Int,4)
            };
            parameters[0].Value = readID;

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
        public appsin.Bizcs.Model.app_messageRead GetModel(int readID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 readID,MsgID,psnID,readTime,readDesc,readStatus from app_messageRead ");
            strSql.Append(" where readID=@readID");
            SqlParameter[] parameters = {
                    new SqlParameter("@readID", SqlDbType.Int,4)
            };
            parameters[0].Value = readID;

            appsin.Bizcs.Model.app_messageRead model = new appsin.Bizcs.Model.app_messageRead();
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
        public appsin.Bizcs.Model.app_messageRead DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.app_messageRead model = new appsin.Bizcs.Model.app_messageRead();
            if (row != null)
            {
                if (row["readID"] != null && row["readID"].ToString() != "")
                {
                    model.readID = int.Parse(row["readID"].ToString());
                }
                if (row["MsgID"] != null && row["MsgID"].ToString() != "")
                {
                    model.MsgID = int.Parse(row["MsgID"].ToString());
                }
                if (row["psnID"] != null && row["psnID"].ToString() != "")
                {
                    model.psnID = int.Parse(row["psnID"].ToString());
                }
                if (row["readTime"] != null && row["readTime"].ToString() != "")
                {
                    model.readTime = DateTime.Parse(row["readTime"].ToString());
                }
                if (row["readDesc"] != null)
                {
                    model.readDesc = row["readDesc"].ToString();
                }
                if (row["readStatus"] != null && row["readStatus"].ToString() != "")
                {
                    model.readStatus = int.Parse(row["readStatus"].ToString());
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
            strSql.Append("select readID,MsgID,psnID,readTime,readDesc,readStatus ");
            strSql.Append(" FROM app_messageRead ");
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
                strSql.Append("order by T.readID desc");
            }
            strSql.Append(")AS Row, T.*  from app_messageRead T ");
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

        #endregion  ExtensionMethod
    }
}
