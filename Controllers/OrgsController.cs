using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using appsin.Bizcs.Model;
using appsin.Common;
using appsin.Models;
using System.Diagnostics.CodeAnalysis;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class OrgsController : ControllerBase
    {
        [HttpPost(Name = "orgCancel")]
        public ResponseSet<string> orgCancel([FromBody] RequestSet<string> reqModel)
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
                res.message = "Request Success,but no data input,so no data is changed";
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
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Invalid nodeID!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                Bizcs.Model.org_orgMain orgModel = new org_orgMain();
                orgModel = orgBll.GetModel(int.Parse(reqModel.reqData[0]));
                string strWhere = "unitID in (SELECT [orgID] FROM [org_orgMain] where orgLevel like '" + orgModel.orgLevel + "%')" +
                    " or deptID in (SELECT [orgID] FROM [appsin].[dbo].[org_orgMain] where orgLevel like '" + orgModel.orgLevel + "%')" +
                    " or postID in (SELECT [orgID] FROM [appsin].[dbo].[org_orgMain] where orgLevel like '" + orgModel.orgLevel + "%')";
                List<psn_psnMain> listPsn = new Bizcs.BLL.psn_psnMain().GetModelList(strWhere);
                if (listPsn.Count > 0)
                {
                    res.status = -3;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There are some people still in this and sub organization,can't be canceled!";
                    res.resData = null;
                }
                else
                {
                    int execCount = orgBll.cancelOrg(orgModel.orgLevel);
                    if (execCount > 0)
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
                        res.message = "Request Success,but there is some wrong occured,no data is changed!";
                        res.resData = null;
                    }
                }
            }
            return res;
        }

        [HttpPost(Name = "orgDel")]
        public ResponseSet<string> orgDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                if (reqModel.reqData.Count > 0)
                {
                    DataSet dsSub = orgBll.GetList(" parentID=" + reqModel.reqData[0]);
                    DataSet dsPsn = psnBll.GetList(" unitID=" + reqModel.reqData[0] + " or deptID=" + reqModel.reqData[0] + " or postID=" + reqModel.reqData[0]);
                    if (dsSub.Tables[0] != null && dsSub.Tables[0].Rows.Count > 0)
                    {
                        res.status = -2;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Request Success,but there is sub-organization exist,can't be deleted!";
                        res.resData = null;
                    }
                    else if (dsPsn.Tables[0] != null && dsPsn.Tables[0].Rows.Count > 0)
                    {
                        res.status = -3;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Request Success,but there is somebody exist in this unit,can't be deleted!";
                        res.resData = null;
                    }
                    else
                    {
                        bool iss = orgBll.Delete(int.Parse(reqModel.reqData[0]));
                        if (iss)
                        {
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Request Success,unit already be deleted!";
                            res.resData = null;
                        }
                        else
                        {
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Request Success,but unit not be deleted for some reason!";
                            res.resData = null;
                        }
                    }
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "orgUpdate")]
        public ResponseSet<string> orgUpdate([FromBody] RequestSet<iOrgInfo> reqModel)
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
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].puID) || !VerifyHelper.isSafe(reqModel.reqData[0].uName)
                || !VerifyHelper.isSafe(reqModel.reqData[0].uCode) || !VerifyHelper.isSafe(reqModel.reqData[0].adminPsn)
                || !VerifyHelper.isSafe(reqModel.reqData[0].adminPost))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                var orgInfo = reqModel.reqData[0];
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                Bizcs.Model.org_orgMain orgModel = orgBll.GetModel(int.Parse(orgInfo.uID));
                orgModel.orgID = int.Parse(orgInfo.uID);
                orgModel.parentID = orgInfo.uID == "10000" ? null : int.Parse(orgInfo.puID);
                orgModel.orgName = orgInfo.uName;
                orgModel.orgCode = orgInfo.uCode;
                orgModel.validStartDate = orgInfo.startDate;
                orgModel.validEndDate = orgInfo.endDate;
                if (!orgInfo.adminPsn.Equals(""))
                { orgModel.chargeUser = int.Parse(orgInfo.adminPsn); }
                if (!orgInfo.adminPost.Equals(""))
                { orgModel.chargePost = int.Parse(orgInfo.adminPost); }
                orgModel.orgType = orgInfo.uID == "10000" ? orgModel.orgType : orgInfo.uType;
                orgModel.orgStatus = 1;
                bool iss = orgBll.Update(orgModel);

                res.status = 1;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Request Success!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "orgAdd")]
        public ResponseSet<iOrgInfo> orgAdd([FromBody] RequestSet<iOrgInfo> reqModel)
        {
            ResponseSet<iOrgInfo> res = new ResponseSet<iOrgInfo>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                Bizcs.Model.org_orgMain orgModel = new org_orgMain();
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].puID) && VerifyHelper.isSafe(reqModel.reqData[0].uName) && VerifyHelper.isSafe(reqModel.reqData[0].uCode) && VerifyHelper.isSafe(reqModel.reqData[0].adminPsn) && VerifyHelper.isSafe(reqModel.reqData[0].adminPost))
                    {
                        //calculate orgLevel
                        string orgLevel = default;
                        Bizcs.Model.org_orgMain parentOrgModel = orgBll.GetModel(int.Parse(reqModel.reqData[0].puID));
                        DataSet dsIsSub = orgBll.GetList(" parentID=" + reqModel.reqData[0].puID + " order by orgLevel desc");
                        if (dsIsSub.Tables[0] != null && dsIsSub.Tables[0].Rows.Count > 0)
                        { orgLevel = (Convert.ToInt64(dsIsSub.Tables[0].Rows[0]["orgLevel"]) + 1).ToString(); }
                        else { orgLevel = parentOrgModel.orgLevel.ToString() + "001"; }

                        orgModel.orgPK = Guid.NewGuid().ToString().ToUpper();
                        orgModel.orgLevel = orgLevel;
                        orgModel.parentID = int.Parse(reqModel.reqData[0].puID);
                        orgModel.parentPK = parentOrgModel.orgPK;
                        orgModel.orgName = reqModel.reqData[0].uName;
                        orgModel.orgCode = reqModel.reqData[0].uCode;
                        orgModel.validStartDate = reqModel.reqData[0].startDate;
                        orgModel.validEndDate = reqModel.reqData[0].endDate;
                        if (!reqModel.reqData[0].adminPsn.Equals("")) { orgModel.chargeUser = int.Parse(reqModel.reqData[0].adminPsn); }
                        ;
                        if (!reqModel.reqData[0].adminPost.Equals("")) { orgModel.chargePost = int.Parse(reqModel.reqData[0].adminPost); }
                        ;
                        orgModel.orgType = reqModel.reqData[0].uType;
                        orgModel.orgStatus = 1;
                        int orgID = orgBll.Add(orgModel);
                        reqModel.reqData[0].uID = orgID.ToString();
                        Models.iOrgInfo[] iOrgInfo = { reqModel.reqData[0] };

                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Request Success!";
                        res.resData = iOrgInfo.ToList();
                    }
                    else
                    {
                        res.status = 110;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "High risk characters!";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "getOrgTree")]
        public ResponseSet<iOrgsTree> getOrgTree([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iOrgsTree> res = new ResponseSet<iOrgsTree>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                List<string> orglist; string unitlist; string deptlist; string postlist;

                //get auhorized organization
                OsrzHelper.getOsrzOrgID(VerifyHelper.getPsnID(reqModel.uToken), out orglist, out unitlist, out deptlist, out postlist);
                List<string> orgAllList = OsrzHelper.getOsrzOrglist(orglist);
                string authrizedOrg = "";
                for (int i = 0; i < orgAllList.Count; i++)
                { authrizedOrg += "'" + orgAllList[i] + "',"; }
                authrizedOrg = authrizedOrg.Length > 0 ? " and orgLevel in (" + authrizedOrg.Substring(0, authrizedOrg.Length - 1) + ")" : "and orgLevel in ('000')";

                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                string strWhere = "";
                if (reqModel.reqData[0] == "all") { strWhere = " orgStatus=1 or orgStatus=-1"; } else { strWhere = " orgStatus=1"; }
                DataSet dsOrg = orgBll.GetTreeList(strWhere + authrizedOrg);
                if (dsOrg.Tables[0] != null && dsOrg.Tables[0].Rows.Count > 0)
                {
                    List<iOrgsTree> orgList = Common.listHelper.ConvertDtToList<iOrgsTree>(dsOrg.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = orgList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "getOrgAll")]
        public ResponseSet<iOrgsTree> getOrgAll([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iOrgsTree> res = new ResponseSet<iOrgsTree>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                string strWhere = " orgStatus=1";
                DataSet dsOrg = orgBll.GetTreeList(strWhere);
                if (dsOrg.Tables[0] != null && dsOrg.Tables[0].Rows.Count > 0)
                {
                    List<iOrgsTree> orgList = Common.listHelper.ConvertDtToList<iOrgsTree>(dsOrg.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = orgList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "getOrgList")]
        public ResponseSet<iOrgsList> getOrgList([FromBody] RequestSet<commonItem> reqModel)
        {
            ResponseSet<iOrgsList> res = new ResponseSet<iOrgsList>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                Bizcs.Model.org_orgMain orgModel = orgBll.GetModel(int.Parse(reqModel.reqData[0].id));
                string strWhere = "";
                if (reqModel.reqData[0].type == "unit") { strWhere = " orgStatus=1 and orgType='unit'"; }
                if (reqModel.reqData[0].type == "dept") { strWhere = " orgStatus=1 and orgType='dept' and orgLevel like '" + (orgModel == null ? "0" : orgModel.orgLevel) + "%'"; }
                if (reqModel.reqData[0].type == "post") { strWhere = " orgStatus=1 and orgType='post' and orgLevel like '" + (orgModel == null ? "0" : orgModel.orgLevel) + "%'"; }
                DataSet dsOrg = orgBll.GetOrgsList(strWhere);
                if (dsOrg.Tables[0] != null && dsOrg.Tables[0].Rows.Count > 0)
                {
                    List<iOrgsList> orgList = Common.listHelper.ConvertDtToList<iOrgsList>(dsOrg.Tables[0]);

                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = orgList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "getOrgDoc")]
        public ResponseSet<iOrgsList> getOrgDoc([FromBody] RequestSet<commonItem> reqModel)
        {
            ResponseSet<iOrgsList> res = new ResponseSet<iOrgsList>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                string strWhere = "";
                if (reqModel.reqData[0].type == "unit") { strWhere = " orgStatus=1 and orgType='unit'"; }
                if (reqModel.reqData[0].type == "dept") { strWhere = " orgStatus=1 and orgType='dept'"; }
                if (reqModel.reqData[0].type == "post") { strWhere = " orgStatus=1 and orgType='post' order by parentID"; }
                DataSet dsOrg = orgBll.GetOrgsList(strWhere);
                if (dsOrg.Tables[0] != null && dsOrg.Tables[0].Rows.Count > 0)
                {
                    List<iOrgsList> orgList = Common.listHelper.ConvertDtToList<iOrgsList>(dsOrg.Tables[0]);

                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = orgList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "getOrgDetail")]
        public ResponseSet<org_orgMain> getOrgDetail([FromBody] RequestSet<int> reqModel)
        {
            ResponseSet<org_orgMain> res = new ResponseSet<org_orgMain>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                Bizcs.Model.org_orgMain orgModel = new org_orgMain();
                orgModel = orgBll.GetFullModel(reqModel.reqData[0]);
                if (orgModel != null)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    org_orgMain[] orgList = { orgModel };
                    res.resData = orgList.ToList();
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }
    }
}
