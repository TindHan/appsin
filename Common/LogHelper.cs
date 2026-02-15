namespace appsin.Common
{
    public class LogHelper
    {
        public static void logRecord(int psnID, string action, string isS, string para, string result, string memo)
        {
            Bizcs.BLL.psn_actLog logBll = new Bizcs.BLL.psn_actLog();
            Bizcs.Model.psn_actLog logModel = new Bizcs.Model.psn_actLog();
            logModel.logTime = DateTime.Now;
            logModel.psnID = psnID;
            logModel.logAction = action;
            logModel.actPara = para;
            logModel.isSuccess = isS;
            logModel.actResult = result;
            logModel.actMemo = memo;
            logBll.Add(logModel);
        }
    }
}
