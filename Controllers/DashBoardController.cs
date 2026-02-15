using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using appsin.Common;
using appsin.Models;
using appsin.ApiModels;
using Azure.Identity;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        [HttpPost(Name = "getNumbers")]
        public IActionResult getNumbers([FromBody] RequestSet<string> reqModel)
        {
            object res = null;
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res = new { status = -1, uToken = "", message = "uToken is illegal!" };
            }
            else
            {
                string uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                DataSet ds = new Bizcs.BLL.app_common().GetDashboardData(VerifyHelper.getPsnID(reqModel.uToken));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    int number1 = ds.Tables[0].Rows[0]["workdays"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["workdays"]);
                    int number2 = ds.Tables[0].Rows[0]["loginNum"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["loginNum"]);
                    int number3 = ds.Tables[0].Rows[0]["taskAll"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["taskAll"]);
                    int number4 = ds.Tables[0].Rows[0]["taskDone"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["taskDone"]);

                    res = new { status = 1, uToken, message = "uToken is illegal!", number1, number2, number3, number4 };
                }
                else
                {
                    res = new { status = 0, uToken, message = "No valid data!" };
                }
            }
            return Ok(res);
        }

        [HttpPost(Name = "getMyTasks")]
        public ResponseSet<ApiModels.taskList> getMyTasks([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<ApiModels.taskList> res = new ResponseSet<ApiModels.taskList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else
            {
                DataSet ds = new Bizcs.BLL.app_tasks().GetList(6, "taskStatus =1 and taskProgress<100 and psnID=" + VerifyHelper.getPsnID(reqModel.uToken), " taskDeadline");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<ApiModels.taskList> taskList = listHelper.ConvertDtToList<ApiModels.taskList>(ds.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = taskList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getMyTaskList")]
        public ResponseSet<ApiModels.taskList> getMyTaskList(RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<taskList> res = new ResponseSet<taskList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].kw) || !VerifyHelper.isSafe(reqModel.reqData[0].oid) || !VerifyHelper.isSafe(reqModel.reqData[0].ty) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                int sIndex = (pageIndex - 1) * pageListNum + 1;
                int eIndex = pageIndex * pageListNum;

                string strwhere1 = "psnID=" + VerifyHelper.getPsnID(reqModel.uToken) + " and taskStatus=" + reqModel.reqData[0].ons;
                string strwhere2 = reqModel.reqData[0].kw == "" ? "" : "and taskTitle like '%" + reqModel.reqData[0].kw + "%'";

                Bizcs.BLL.app_tasks taskBll = new Bizcs.BLL.app_tasks();
                DataSet dsTask = taskBll.GetSimpleListByPage(strwhere1 + strwhere2, sIndex, eIndex);
                DataSet dsTaskNumber = taskBll.GetList(strwhere1 + strwhere2);
                if (dsTask.Tables[0] != null && dsTask.Tables[0].Rows.Count > 0)
                {
                    List<taskList> taskList = listHelper.ConvertDtToList<taskList>(dsTask.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsTaskNumber.Tables[0].Rows.Count;
                    res.resData = taskList.ToList();
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getTaskDetail")]
        public ResponseSet<ApiModels.taskList> getTaskDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<ApiModels.taskList> res = new ResponseSet<ApiModels.taskList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -1;
                res.uToken = "";
                res.message = "TaskID is not correct!";
                res.resData = null;
            }
            else
            {
                DataSet ds = new Bizcs.BLL.app_tasks().GetList(" taskID=" + int.Parse(reqModel.reqData[0]));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<ApiModels.taskList> taskList = listHelper.ConvertDtToList<ApiModels.taskList>(ds.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = taskList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "addTask")]
        public ResponseSet<string> addTask([FromBody] RequestSet<ApiModels.taksModel> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].taskTitle) && !VerifyHelper.isSafe(reqModel.reqData[0].taskContent) && !VerifyHelper.isSafe(reqModel.reqData[0].taskDeadline.ToString()))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else if (reqModel.reqData == null || reqModel.reqData.Count <= 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "No valid task data!";
                res.resData = null;
            }
            else
            {
                Bizcs.Model.app_tasks taskModel = new Bizcs.Model.app_tasks();
                taskModel.psnID = VerifyHelper.getPsnID(reqModel.uToken);
                taskModel.assignPsnID = VerifyHelper.getPsnID(reqModel.uToken);
                taskModel.taskTitle = reqModel.reqData[0].taskTitle;
                taskModel.taskContent = reqModel.reqData[0].taskContent;
                taskModel.taskDeadline = reqModel.reqData[0].taskDeadline;
                taskModel.taskProgress = 0;
                taskModel.taskCreateTime = DateTime.Now;
                taskModel.taskStatus = 1;

                int taskID = new Bizcs.BLL.app_tasks().Add(taskModel);
                if (taskID > 0)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = null;
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request fail!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "setTask")]
        public ResponseSet<string> setTask([FromBody] RequestSet<ApiModels.taksModel> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].taskTitle) || !VerifyHelper.isSafe(reqModel.reqData[0].taskContent) || !VerifyHelper.isSafe(reqModel.reqData[0].taskDeadline.ToString()))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.Model.app_tasks taskModel = new Bizcs.BLL.app_tasks().GetModel(reqModel.reqData[0].taskID);
                taskModel.taskTitle = reqModel.reqData[0].taskTitle;
                taskModel.taskContent = reqModel.reqData[0].taskContent;
                taskModel.taskDeadline = reqModel.reqData[0].taskDeadline;
                taskModel.taskStatus = reqModel.reqData[0].taskStatus;

                bool iss = new Bizcs.BLL.app_tasks().Update(taskModel);
                if (iss)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = null;
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request fail!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "closeTask")]
        public ResponseSet<string> closeTask([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "The progressID is not correct!";
                res.resData = null;
            }
            else
            {
                Bizcs.Model.app_tasks taskModel = new Bizcs.BLL.app_tasks().GetModel(int.Parse(reqModel.reqData[0]));
                taskModel.taskStatus = -1;

                bool iss = new Bizcs.BLL.app_tasks().Update(taskModel);
                if (iss)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = null;
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request fail!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getProgress")]
        public ResponseSet<progressList> getProgress([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<ApiModels.progressList> res = new ResponseSet<ApiModels.progressList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "TaskID is not correct!";
                res.resData = null;
            }
            else
            {
                DataSet ds = new Bizcs.BLL.app_taskProgress().GetList(1000, " taskID=" + reqModel.reqData[0], " createTime desc");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<ApiModels.progressList> progressList = listHelper.ConvertDtToList<ApiModels.progressList>(ds.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = progressList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "addProgress")]
        public ResponseSet<string> addProgress([FromBody] RequestSet<progressModel> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].progressContent) || !VerifyHelper.isSafe(reqModel.reqData[0].taskID.ToString()) || !VerifyHelper.isSafe(reqModel.reqData[0].progressValue.ToString()))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.Model.app_taskProgress progressModel = new Bizcs.Model.app_taskProgress();
                progressModel.psnID = VerifyHelper.getPsnID(reqModel.uToken);
                progressModel.taskID = reqModel.reqData[0].taskID;
                progressModel.progressValue = reqModel.reqData[0].progressValue;
                progressModel.progressContent = reqModel.reqData[0].progressContent;
                progressModel.progressStatus = 1;
                progressModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                progressModel.createTime = DateTime.Now;

                int taskID = new Bizcs.BLL.app_taskProgress().Add(progressModel);
                if (taskID > 0)
                {
                    Bizcs.BLL.app_tasks taskBll = new Bizcs.BLL.app_tasks();
                    Bizcs.Model.app_tasks taskModel = taskBll.GetModel(reqModel.reqData[0].taskID);
                    taskModel.taskProgress = progressModel.progressValue;
                    taskBll.Update(taskModel);

                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = null;
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request fail!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "setProgress")]
        public ResponseSet<string> setProgress([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "The progressID is not correct!";
                res.resData = null;
            }
            else
            {
                Bizcs.Model.app_taskProgress progressModel = new Bizcs.BLL.app_taskProgress().GetModel(int.Parse(reqModel.reqData[0]));
                progressModel.progressStatus = -1;

                bool iss = new Bizcs.BLL.app_taskProgress().Update(progressModel);
                if (iss)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = null;
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request fail!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getMyNotices")]
        public ResponseSet<noticeList> getMyNotices([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<ApiModels.noticeList> res = new ResponseSet<ApiModels.noticeList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].kw) || !VerifyHelper.isSafe(reqModel.reqData[0].oid) || !VerifyHelper.isSafe(reqModel.reqData[0].ty) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 5);
                int sIndex = (pageIndex - 1) * pageListNum + 1;
                int eIndex = pageIndex * pageListNum;

                int myPsnId = VerifyHelper.getPsnID(reqModel.uToken);
                string strwhere = "noticeStatus=1 and startTime<GETDATE() and DATEADD(dd,1,endTime)>GETDATE() and  ("
                               + "(objType='psn' and objID=" + myPsnId + ") or "
                               + "(objType='post' and objID =(select postID from psn_psnMain where psnID=" + myPsnId + ")) or "
                               + "(objType='dept' and objID =(select deptID from psn_psnMain where psnID=" + myPsnId + ")) or "
                               + "(objType='unit' and objID =(select unitID from psn_psnMain where psnID=" + myPsnId + ")) or "
                               + "objType='all' )";

                if (reqModel.reqData[0].kw.Trim() != "")
                {
                    strwhere += " and noticeTitle like '%" + reqModel.reqData[0].kw.Trim() + "%' ";
                }

                Bizcs.BLL.app_notices noticeBll = new Bizcs.BLL.app_notices();
                DataSet dsNotice = noticeBll.GetSimpleListByPage(strwhere, " createTime desc", myPsnId, sIndex, eIndex);
                DataSet dsNoticeNumber = noticeBll.GetList(strwhere);
                if (dsNotice.Tables[0] != null && dsNotice.Tables[0].Rows.Count > 0)
                {
                    List<noticeList> noticeList = listHelper.ConvertDtToList<noticeList>(dsNotice.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsNoticeNumber.Tables[0].Rows.Count;
                    res.resData = noticeList.ToList();
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getNoticeDetail")]
        public ResponseSet<ApiModels.noticeList> getNoticeDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<ApiModels.noticeList> res = new ResponseSet<ApiModels.noticeList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -1;
                res.uToken = "";
                res.message = "NoticeID is not correct!";
                res.resData = null;
            }
            else
            {
                DataSet ds = new Bizcs.BLL.app_notices().GetSimpleList(" noticeID=" + int.Parse(reqModel.reqData[0]), VerifyHelper.getPsnID(reqModel.uToken));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<ApiModels.noticeList> noticeList = listHelper.ConvertDtToList<ApiModels.noticeList>(ds.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = noticeList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getNotices")]
        public ResponseSet<noticeList> getNotices([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<ApiModels.noticeList> res = new ResponseSet<ApiModels.noticeList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].kw) || !VerifyHelper.isSafe(reqModel.reqData[0].oid) || !VerifyHelper.isSafe(reqModel.reqData[0].ty) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                int sIndex = (pageIndex - 1) * pageListNum + 1;
                int eIndex = pageIndex * pageListNum;

                string strwhere1 = "noticeStatus=" + reqModel.reqData[0].ons;
                string strwhere2 = reqModel.reqData[0].kw == "" ? "" : "and noticeTitle like '%" + reqModel.reqData[0].kw + "%'";

                Bizcs.BLL.app_notices noticeBll = new Bizcs.BLL.app_notices();
                DataSet dsNotice = noticeBll.GetAllListByPage(strwhere1 + strwhere2, " createTime desc ", sIndex, eIndex);
                DataSet dsNoticeNumber = noticeBll.GetList(strwhere1 + strwhere2);
                if (dsNotice.Tables[0] != null && dsNotice.Tables[0].Rows.Count > 0)
                {
                    List<noticeList> noticeList = listHelper.ConvertDtToList<noticeList>(dsNotice.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsNoticeNumber.Tables[0].Rows.Count;
                    res.resData = noticeList.ToList();
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "addNotice")]
        public ResponseSet<string> addNotice([FromBody] RequestSet<noticeModel> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].noticeTitle) || !VerifyHelper.isSafe(reqModel.reqData[0].noticeContent) || !VerifyHelper.isSafe(reqModel.reqData[0].noticeFile))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.Model.app_notices noticeModel = new Bizcs.Model.app_notices();
                noticeModel.appID = 10001;
                noticeModel.noticeTitle = reqModel.reqData[0].noticeTitle;
                noticeModel.noticeContent = reqModel.reqData[0].noticeContent;
                noticeModel.noticeFile = reqModel.reqData[0].noticeFile;
                noticeModel.storeFile = reqModel.reqData[0].storeFile;
                noticeModel.objType = "unit";
                noticeModel.objID = 10001;
                noticeModel.startTime = reqModel.reqData[0].startTime;
                noticeModel.endTime = reqModel.reqData[0].endTime;
                noticeModel.noticeStatus = 1;
                noticeModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                noticeModel.createTime = DateTime.Now;

                int taskID = new Bizcs.BLL.app_notices().Add(noticeModel);
                if (taskID > 0)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = null;
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request fail!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "setNotice")]
        public ResponseSet<string> setNotice([FromBody] RequestSet<noticeModel> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].noticeTitle) || !VerifyHelper.isSafe(reqModel.reqData[0].noticeContent) || !VerifyHelper.isSafe(reqModel.reqData[0].noticeFile))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.Model.app_notices noticeModel = new Bizcs.BLL.app_notices().GetModel(reqModel.reqData[0].noticeID);
                noticeModel.noticeTitle = reqModel.reqData[0].noticeTitle;
                noticeModel.noticeContent = reqModel.reqData[0].noticeContent;
                noticeModel.noticeFile = reqModel.reqData[0].noticeFile;
                noticeModel.storeFile = reqModel.reqData[0].storeFile;
                noticeModel.startTime = reqModel.reqData[0].startTime;
                noticeModel.endTime = reqModel.reqData[0].endTime;
                noticeModel.noticeStatus = reqModel.reqData[0].noticeStatus;

                bool iss = new Bizcs.BLL.app_notices().Update(noticeModel);
                if (iss)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = null;
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request fail!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "disableNotice")]
        public ResponseSet<string> disableNotice([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -1;
                res.uToken = "";
                res.message = "Wrong noticeID!";
                res.resData = null;
            }
            else
            {
                Bizcs.Model.app_notices noticeModel = new Bizcs.BLL.app_notices().GetModel(int.Parse(reqModel.reqData[0]));
                noticeModel.noticeStatus = -1;
                bool iss = new Bizcs.BLL.app_notices().Update(noticeModel);
                if (iss)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = null;
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request fail!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getMyMessages")]
        public ResponseSet<msgList> getMyMessages([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<ApiModels.msgList> res = new ResponseSet<ApiModels.msgList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].kw) || !VerifyHelper.isSafe(reqModel.reqData[0].oid) || !VerifyHelper.isSafe(reqModel.reqData[0].ty) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 5);
                int sIndex = (pageIndex - 1) * pageListNum + 1;
                int eIndex = pageIndex * pageListNum;

                int myPsnId = VerifyHelper.getPsnID(reqModel.uToken);
                string strwhere = "msgStatus=1 and  ("
                               + "(objType='psn' and objID=" + myPsnId + ") or "
                               + "(objType='post' and objID =(select postID from psn_psnMain where psnID=" + myPsnId + ")) or "
                               + "(objType='dept' and objID =(select deptID from psn_psnMain where psnID=" + myPsnId + ")) or "
                               + "(objType='unit' and objID =(select unitID from psn_psnMain where psnID=" + myPsnId + ")) or "
                               + "objType='all' )";
                if (reqModel.reqData[0].kw.Trim() != "")
                {
                    strwhere += " and msgTitle like '%" + reqModel.reqData[0].kw.Trim() + "%' ";
                }

                Bizcs.BLL.app_messages msgBll = new Bizcs.BLL.app_messages();
                DataSet dsMsg = msgBll.GetSimpleListByPage(strwhere, myPsnId, " createTime desc", sIndex, eIndex);
                DataSet dsMsgNumber = msgBll.GetList(strwhere);
                if (dsMsg.Tables[0] != null && dsMsg.Tables[0].Rows.Count > 0)
                {
                    List<msgList> msgList = listHelper.ConvertDtToList<msgList>(dsMsg.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsMsgNumber.Tables[0].Rows.Count;
                    res.resData = msgList.ToList();
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getMessageDetail")]
        public ResponseSet<ApiModels.msgList> getMessageDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<ApiModels.msgList> res = new ResponseSet<ApiModels.msgList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -1;
                res.uToken = "";
                res.message = "MsgID is not correct!";
                res.resData = null;
            }
            else
            {
                DataSet ds = new Bizcs.BLL.app_messages().GetSimpleList(" msgID=" + int.Parse(reqModel.reqData[0]), VerifyHelper.getPsnID(reqModel.uToken));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<ApiModels.msgList> msgList = listHelper.ConvertDtToList<ApiModels.msgList>(ds.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = msgList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "readCount")]
        public ResponseSet<string> readCount([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]) || !VerifyHelper.isSafe(reqModel.reqData[1]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]) || (reqModel.reqData[1] != "notice" && reqModel.reqData[1] != "message"))
            {
                res.status = -1;
                res.uToken = "";
                res.message = "Wrong arguments";
                res.resData = null;
            }
            else
            {
                int readID = 0;
                if (reqModel.reqData[1] == "notice")
                {
                    Bizcs.Model.app_noticeRead readModel = new Bizcs.Model.app_noticeRead();
                    readModel.psnID = VerifyHelper.getPsnID(reqModel.uToken);
                    readModel.readTime = DateTime.Now;
                    readModel.noticeID = int.Parse(reqModel.reqData[0]);
                    readModel.readStatus = 1;

                    readID = new Bizcs.BLL.app_noticeRead().Add(readModel);
                }
                if (reqModel.reqData[1] == "message")
                {
                    Bizcs.Model.app_messageRead readModel = new Bizcs.Model.app_messageRead();
                    readModel.psnID = VerifyHelper.getPsnID(reqModel.uToken);
                    readModel.readTime = DateTime.Now;
                    readModel.MsgID = int.Parse(reqModel.reqData[0]);
                    readModel.readStatus = 1;

                    readID = new Bizcs.BLL.app_messageRead().Add(readModel);
                }

                if (readID > 0)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = null;
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request fail!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "filePath")]
        public ResponseSet<string> filePath([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]) || !VerifyHelper.isSafe(reqModel.reqData[1]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -1;
                res.uToken = "";
                res.message = "NoticeID is wrong!";
                res.resData = null;
            }
            else
            {
                if (reqModel.reqData[1] == "notice")
                {
                    Bizcs.Model.app_notices ntsModel = new Bizcs.BLL.app_notices().GetModel(int.Parse(reqModel.reqData[0]));

                    string filePath = "/files/Upload/" + ntsModel.storeFile;
                    string fileName = ntsModel.noticeFile;
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success!";
                    res.resData = [filePath, fileName];
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Args is not correct!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getSearchFunc")]
        public ResponseSet<funcSearch> getSearchFunc([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<ApiModels.funcSearch> res = new ResponseSet<ApiModels.funcSearch>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {

                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                DataSet dsMenu = menuBll.GetSearchList(reqModel.reqData[0]);
                if (dsMenu.Tables[0] != null && dsMenu.Tables[0].Rows.Count > 0)
                {
                    List<funcSearch> funcList = listHelper.ConvertDtToList<funcSearch>(dsMenu.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = funcList.ToList();
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getSearchMenu")]
        public ResponseSet<funcSearch> getSearchMenu([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<ApiModels.funcSearch> res = new ResponseSet<ApiModels.funcSearch>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {

                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                DataSet dsMenu = menuBll.GetSearchAdmin(reqModel.reqData[0]);
                if (dsMenu.Tables[0] != null && dsMenu.Tables[0].Rows.Count > 0)
                {
                    List<funcSearch> funcList = listHelper.ConvertDtToList<funcSearch>(dsMenu.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = funcList.ToList();
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getMsgUrl")]
        public ResponseSet<string> getMsgUrl([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]) && VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.Model.app_messages msgModel = new Bizcs.BLL.app_messages().GetModel(int.Parse(reqModel.reqData[0]));
                if (msgModel != null)
                {
                    Bizcs.Model.app_appMain appModel = new Bizcs.BLL.app_appMain().GetModel(Convert.ToInt32(msgModel.appID));

                    string url = appModel.appID == 10001 ? msgModel.msgUrl : (appModel.appDomain1 + appModel.appDomain2 + msgModel.msgUrl);

                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = [url,msgModel.bizType,msgModel.bizID.ToString()];
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }
    }
}
