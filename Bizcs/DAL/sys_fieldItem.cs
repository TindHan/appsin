using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_fieldItem
    {
        public sys_fieldItem()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_fieldItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_fieldItem(");
            strSql.Append("itemPK,setID,setPK,itemName,itemLevel,parentID,parentPK,itemDescription,itemMemo1,itemMemo2,itemMemo3,itemMemo4,itemMemo5,displayOrder,createUser,createTime,itemStatus)");
            strSql.Append(" values (");
            strSql.Append("@itemPK,@setID,@setPK,@itemName,@itemLevel,@parentID,@parentPK,@itemDescription,@itemMemo1,@itemMemo2,@itemMemo3,@itemMemo4,@itemMemo5,@displayOrder,@createUser,@createTime,@itemStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@itemPK", SqlDbType.VarChar,100),
                    new SqlParameter("@setID", SqlDbType.Int,4),
                    new SqlParameter("@setPK", SqlDbType.VarChar,100),
                    new SqlParameter("@itemName", SqlDbType.VarChar,100),
                    new SqlParameter("@itemLevel", SqlDbType.Int,4),
                    new SqlParameter("@parentID", SqlDbType.Int,4),
                    new SqlParameter("@parentPK", SqlDbType.VarChar,100),
                    new SqlParameter("@itemDescription", SqlDbType.VarChar,5000),
                    new SqlParameter("@itemMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@itemMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@itemMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@itemMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@itemMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@itemStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.itemPK;
            parameters[1].Value = model.setID;
            parameters[2].Value = model.setPK;
            parameters[3].Value = model.itemName;
            parameters[4].Value = model.itemLevel;
            parameters[5].Value = model.parentID;
            parameters[6].Value = model.parentPK;
            parameters[7].Value = model.itemDescription;
            parameters[8].Value = model.itemMemo1;
            parameters[9].Value = model.itemMemo2;
            parameters[10].Value = model.itemMemo3;
            parameters[11].Value = model.itemMemo4;
            parameters[12].Value = model.itemMemo5;
            parameters[13].Value = model.displayOrder;
            parameters[14].Value = model.createUser;
            parameters[15].Value = model.createTime;
            parameters[16].Value = model.itemStatus;

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
        public bool Update(Bizcs.Model.sys_fieldItem model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_fieldItem set ");
            strSql.Append("itemPK=@itemPK,");
            strSql.Append("setID=@setID,");
            strSql.Append("setPK=@setPK,");
            strSql.Append("itemName=@itemName,");
            strSql.Append("itemLevel=@itemLevel,");
            strSql.Append("parentID=@parentID,");
            strSql.Append("parentPK=@parentPK,");
            strSql.Append("itemDescription=@itemDescription,");
            strSql.Append("itemMemo1=@itemMemo1,");
            strSql.Append("itemMemo2=@itemMemo2,");
            strSql.Append("itemMemo3=@itemMemo3,");
            strSql.Append("itemMemo4=@itemMemo4,");
            strSql.Append("itemMemo5=@itemMemo5,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("itemStatus=@itemStatus");
            strSql.Append(" where itemID=@itemID");
            SqlParameter[] parameters = {
                    new SqlParameter("@itemPK", SqlDbType.VarChar,100),
                    new SqlParameter("@setID", SqlDbType.Int,4),
                    new SqlParameter("@setPK", SqlDbType.VarChar,100),
                    new SqlParameter("@itemName", SqlDbType.VarChar,100),
                    new SqlParameter("@itemLevel", SqlDbType.Int,4),
                    new SqlParameter("@parentID", SqlDbType.Int,4),
                    new SqlParameter("@parentPK", SqlDbType.VarChar,100),
                    new SqlParameter("@itemDescription", SqlDbType.VarChar,5000),
                    new SqlParameter("@itemMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@itemMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@itemMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@itemMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@itemMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@itemStatus", SqlDbType.Int,4),
                    new SqlParameter("@itemID", SqlDbType.Int,4)};
            parameters[0].Value = model.itemPK;
            parameters[1].Value = model.setID;
            parameters[2].Value = model.setPK;
            parameters[3].Value = model.itemName;
            parameters[4].Value = model.itemLevel;
            parameters[5].Value = model.parentID;
            parameters[6].Value = model.parentPK;
            parameters[7].Value = model.itemDescription;
            parameters[8].Value = model.itemMemo1;
            parameters[9].Value = model.itemMemo2;
            parameters[10].Value = model.itemMemo3;
            parameters[11].Value = model.itemMemo4;
            parameters[12].Value = model.itemMemo5;
            parameters[13].Value = model.displayOrder;
            parameters[14].Value = model.createUser;
            parameters[15].Value = model.createTime;
            parameters[16].Value = model.itemStatus;
            parameters[17].Value = model.itemID;

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
        public bool Delete(int itemID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_fieldItem ");
            strSql.Append(" where itemID=@itemID");
            SqlParameter[] parameters = {
                    new SqlParameter("@itemID", SqlDbType.Int,4)
            };
            parameters[0].Value = itemID;

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
        public Bizcs.Model.sys_fieldItem GetModel(int itemID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 itemID,itemPK,setID,setPK,itemName,itemLevel,parentID,parentPK,itemDescription,itemMemo1,itemMemo2,itemMemo3,itemMemo4,itemMemo5,displayOrder,createUser,createTime,itemStatus from sys_fieldItem ");
            strSql.Append(" where itemID=@itemID");
            SqlParameter[] parameters = {
                    new SqlParameter("@itemID", SqlDbType.Int,4)
            };
            parameters[0].Value = itemID;

            Bizcs.Model.sys_fieldItem model = new Bizcs.Model.sys_fieldItem();
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
        public Bizcs.Model.sys_fieldItem DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_fieldItem model = new Bizcs.Model.sys_fieldItem();
            if (row != null)
            {
                if (row["itemID"] != null && row["itemID"].ToString() != "")
                {
                    model.itemID = int.Parse(row["itemID"].ToString());
                }
                if (row["itemPK"] != null)
                {
                    model.itemPK = row["itemPK"].ToString();
                }
                if (row["setID"] != null && row["setID"].ToString() != "")
                {
                    model.setID = int.Parse(row["setID"].ToString());
                }
                if (row["setPK"] != null)
                {
                    model.setPK = row["setPK"].ToString();
                }
                if (row["itemName"] != null)
                {
                    model.itemName = row["itemName"].ToString();
                }
                if (row["itemLevel"] != null && row["itemLevel"].ToString() != "")
                {
                    model.itemLevel = int.Parse(row["itemLevel"].ToString());
                }
                if (row["parentID"] != null && row["parentID"].ToString() != "")
                {
                    model.parentID = int.Parse(row["parentID"].ToString());
                }
                if (row["parentPK"] != null)
                {
                    model.parentPK = row["parentPK"].ToString();
                }
                if (row["itemDescription"] != null)
                {
                    model.itemDescription = row["itemDescription"].ToString();
                }
                if (row["itemMemo1"] != null)
                {
                    model.itemMemo1 = row["itemMemo1"].ToString();
                }
                if (row["itemMemo2"] != null)
                {
                    model.itemMemo2 = row["itemMemo2"].ToString();
                }
                if (row["itemMemo3"] != null)
                {
                    model.itemMemo3 = row["itemMemo3"].ToString();
                }
                if (row["itemMemo4"] != null)
                {
                    model.itemMemo4 = row["itemMemo4"].ToString();
                }
                if (row["itemMemo5"] != null)
                {
                    model.itemMemo5 = row["itemMemo5"].ToString();
                }
                if (row["displayOrder"] != null && row["displayOrder"].ToString() != "")
                {
                    model.displayOrder = int.Parse(row["displayOrder"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["itemStatus"] != null && row["itemStatus"].ToString() != "")
                {
                    model.itemStatus = int.Parse(row["itemStatus"].ToString());
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
            strSql.Append("select itemID,itemPK,setID,setPK,itemName,itemLevel,parentID,parentPK,itemDescription,itemMemo1,itemMemo2,itemMemo3,itemMemo4,itemMemo5,displayOrder,createUser,createTime,itemStatus ");
            strSql.Append(" FROM sys_fieldItem ");
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
                strSql.Append("order by T.itemID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_fieldItem T ");
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
        public DataSet GetServItem(string itemPK)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select itemID,itemPK,setID,setPK,itemName,itemLevel,parentID,parentPK,itemDescription,itemMemo1,itemMemo2,itemMemo3,itemMemo4,itemMemo5,displayOrder,createUser,createTime,itemStatus ");
            strSql.Append(" FROM sys_fieldItem where itemPK=@itemPK");
            SqlParameter[] parameters = {
                    new SqlParameter("@itemPK", SqlDbType.VarChar,100)
            };
            parameters[0].Value = itemPK;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds;
        }
        public int GetTypeIDByStr(string typeStr)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 itemID from sys_fieldItem ");
            strSql.Append(" where ( itemName=@itemName or itemID=@itemID ) and setID=10001 and itemStatus=1");
            SqlParameter[] parameters = {
                    new SqlParameter("@itemName", SqlDbType.VarChar,100),
                    new SqlParameter("@itemID", SqlDbType.Int,4)
            };
            parameters[0].Value = typeStr;
            parameters[1].Value = (int.TryParse(typeStr, out int result) ? int.Parse(typeStr) : 0);

            Bizcs.Model.sys_fieldItem model = new Bizcs.Model.sys_fieldItem();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        public int GetItemIDByStr(string itemStr)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 itemID from sys_fieldItem ");
            strSql.Append(" where ( itemName=@itemName or itemID=@itemID ) and setID=10002 and itemStatus=1");
            SqlParameter[] parameters = {
                    new SqlParameter("@itemName", SqlDbType.VarChar,100),
                    new SqlParameter("@itemID", SqlDbType.Int,4)
            };
            parameters[0].Value = itemStr;
            parameters[1].Value = (int.TryParse(itemStr, out int result) ? int.Parse(itemStr) : 0);

            Bizcs.Model.sys_fieldItem model = new Bizcs.Model.sys_fieldItem();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select itemID,itemName,itemLevel,itemDescription, parentID as itemPID,displayOrder,createTime,(select psnName from psn_psnMain where psnID=createUser) as createUser,itemStatus   ");
            strSql.Append(" FROM sys_fieldItem ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }
        #endregion  ExtensionMethod
    }
}
