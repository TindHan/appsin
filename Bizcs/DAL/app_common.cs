using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.NetworkInformation;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class app_common
    {
        public app_common()
        { }

        public DataSet GetDashboardData(int psnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("(SELECT DATEDIFF(DAY, (select psnJobday from psn_psnMain where psnID = " + psnID + "), GETDATE())) AS workdays, ");
            strSql.Append("(select count(1) from psn_actLog where psnID = " + psnID + ") as loginNum, ");
            strSql.Append("(select count(1) from app_tasks where psnID = " + psnID + " and taskStatus = 1) as taskAll, ");
            strSql.Append("(select count(1) from app_tasks where psnID = " + psnID + " and taskStatus = 1 and taskProgress = 100) as taskDone ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet getConsoleLoginCount()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 'loginCount' as objName,");
            strSql.Append("(select count(1) from psn_actLog where logAction = 'login' and psnID > 0 and isSuccess = 'success' and YEAR(logTime) = YEAR(GETDATE()) and MONTH(logTime) = MONTH(GETDATE())) as thisMonth, ");
            strSql.Append("(select count(1) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -1, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -1, GETDATE()))) as pre1Month, ");
            strSql.Append("(select count(1) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -2, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -2, GETDATE()))) as pre2Month, ");
            strSql.Append("(select count(1) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -3, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -3, GETDATE()))) as pre3Month, ");
            strSql.Append("(select count(1) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -4, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -4, GETDATE()))) as pre4Month, ");
            strSql.Append("(select count(1) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -5, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -5, GETDATE()))) as pre5Month ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet getConsoleLoginUser()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 'loginUser' as objName,");
            strSql.Append("(select count(distinct psnID) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(GETDATE()) and MONTH(logTime)= MONTH(GETDATE())) as thisMonth, ");
            strSql.Append("(select count(distinct psnID) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -1, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -1, GETDATE()))) as pre1Month, ");
            strSql.Append("(select count(distinct psnID) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -2, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -2, GETDATE()))) as pre2Month, ");
            strSql.Append("(select count(distinct psnID) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -3, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -3, GETDATE()))) as pre3Month, ");
            strSql.Append("(select count(distinct psnID) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -4, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -4, GETDATE()))) as pre4Month, ");
            strSql.Append("(select count(distinct psnID) from psn_actLog where logAction = 'login' and psnID> 0 and isSuccess = 'success' and YEAR(logTime)= YEAR(DATEADD(MONTH, -5, GETDATE())) and MONTH(logTime)= MONTH(DATEADD(MONTH, -5, GETDATE()))) as pre5Month ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet getConsoleAppUse()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select appName as objName,");
            strSql.Append("(select count(1) from app_goToLog golog where golog.appID = app.appID and YEAR(goTime) = YEAR(GETDATE()) and MONTH(goTime) = MONTH(GETDATE())) as thisMonth, ");
            strSql.Append("(select count(1) from app_goToLog golog where golog.appID = app.appID and YEAR(goTime)= YEAR(DATEADD(MONTH, -1, GETDATE())) and MONTH(goTime)= MONTH(DATEADD(MONTH, -1, GETDATE()))) as pre1Month,  ");
            strSql.Append("(select count(1) from app_goToLog golog where golog.appID = app.appID and YEAR(goTime)= YEAR(DATEADD(MONTH, -2, GETDATE())) and MONTH(goTime)= MONTH(DATEADD(MONTH, -2, GETDATE()))) as pre2Month, ");
            strSql.Append("(select count(1) from app_goToLog golog where golog.appID = app.appID and YEAR(goTime)= YEAR(DATEADD(MONTH, -3, GETDATE())) and MONTH(goTime)= MONTH(DATEADD(MONTH, -3, GETDATE()))) as pre3Month, ");
            strSql.Append("(select count(1) from app_goToLog golog where golog.appID = app.appID and YEAR(goTime)= YEAR(DATEADD(MONTH, -4, GETDATE())) and MONTH(goTime)= MONTH(DATEADD(MONTH, -4, GETDATE()))) as pre4Month, ");
            strSql.Append("(select count(1) from app_goToLog golog where golog.appID = app.appID and YEAR(goTime)= YEAR(DATEADD(MONTH, -5, GETDATE())) and MONTH(goTime)= MONTH(DATEADD(MONTH, -5, GETDATE()))) as pre5Month ");
            strSql.Append("from app_appMain app where appStatus = 1 and appID<>10001");
            return DbHelperSQL.Query(strSql.ToString());
        }
        public DataSet getConsoleApiUse()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select appName as objName,");
            strSql.Append("(select count(1) from api_useLog ulog where ulog.appID = app.appID and YEAR(createTime)= YEAR(GETDATE()) and MONTH(createTime)= MONTH(GETDATE())) as thisMonth, ");
            strSql.Append("(select count(1) from api_useLog ulog where ulog.appID = app.appID and YEAR(createTime)= YEAR(DATEADD(MONTH, -1, GETDATE())) and MONTH(createTime)= MONTH(DATEADD(MONTH, -1, GETDATE()))) as pre1Month,  ");
            strSql.Append("(select count(1) from api_useLog ulog where ulog.appID = app.appID and YEAR(createTime)= YEAR(DATEADD(MONTH, -2, GETDATE())) and MONTH(createTime)= MONTH(DATEADD(MONTH, -2, GETDATE()))) as pre2Month, ");
            strSql.Append("(select count(1) from api_useLog ulog where ulog.appID = app.appID and YEAR(createTime)= YEAR(DATEADD(MONTH, -3, GETDATE())) and MONTH(createTime)= MONTH(DATEADD(MONTH, -3, GETDATE()))) as pre3Month, ");
            strSql.Append("(select count(1) from api_useLog ulog where ulog.appID = app.appID and YEAR(createTime)= YEAR(DATEADD(MONTH, -4, GETDATE())) and MONTH(createTime)= MONTH(DATEADD(MONTH, -4, GETDATE()))) as pre4Month, ");
            strSql.Append("(select count(1) from api_useLog ulog where ulog.appID = app.appID and YEAR(createTime)= YEAR(DATEADD(MONTH, -5, GETDATE())) and MONTH(createTime)= MONTH(DATEADD(MONTH, -5, GETDATE()))) as pre5Month ");
            strSql.Append("from app_appMain app where appStatus = 1 and appID<>10001");
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
