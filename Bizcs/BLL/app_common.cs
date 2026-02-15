using System.Data;
namespace appsin.Bizcs.BLL
{
    public class app_common
    {
        private readonly Bizcs.DAL.app_common dal = new Bizcs.DAL.app_common();
        public app_common()
        { }

        public DataSet GetDashboardData(int psnID)
        {
            return dal.GetDashboardData(psnID);
        }

        public DataSet getConsoleLoginCount()
        {
            return dal.getConsoleLoginCount();
        }

        public DataSet getConsoleLoginUser()
        {
            return dal.getConsoleLoginUser();
        }

        public DataSet getConsoleAppUse()
        {
            return dal.getConsoleAppUse();
        }

        public DataSet getConsoleApiUse()
        {
            return dal.getConsoleApiUse();
        }
    }
}
