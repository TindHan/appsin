using Microsoft.AspNetCore.Mvc;
using System.Data;
using appsin.Common;
using appsin.Models;
using appsin.ApiModels;
using appsin.Bizcs.Model;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class FlowsController : ControllerBase
    {
        [HttpPost(Name = "genKey")]
        public ResponseSet<string> genKey(RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0 )
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
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Wrong argument!";
                res.resData = null;
            }
            else
            {
                int appID = int.Parse(reqModel.reqData[0]);

                string goStr = Guid.NewGuid().ToString().ToUpper();
                Bizcs.Model.app_goToLog gotoModel = new Bizcs.Model.app_goToLog();
                gotoModel.menuID = 0;
                gotoModel.psnID = VerifyHelper.getPsnID(reqModel.uToken);
                gotoModel.appID = appID;
                gotoModel.goStr = goStr;
                gotoModel.goTime = DateTime.Now;
                int logID = new Bizcs.BLL.app_goToLog().Add(gotoModel);

                if (logID > 0)
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "ok!";
                    res.resData = [goStr];
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is something wrong occured,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "tempSet")]
        public ResponseSet<string> tempSet(RequestSet<templateEdit> reqModel)
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

            else if (!VerifyHelper.isSafe(reqModel.reqData[0].templateName) || !VerifyHelper.isSafe(reqModel.reqData[0].templateDesc)
                || !VerifyHelper.isSafe(reqModel.reqData[0].str1Title) || !VerifyHelper.isSafe(reqModel.reqData[0].str2Title)
                || !VerifyHelper.isSafe(reqModel.reqData[0].str3Title) || !VerifyHelper.isSafe(reqModel.reqData[0].templateID)
                || !VerifyHelper.isSafe(reqModel.reqData[0].templateStatus) || !VerifyHelper.isSafe(reqModel.reqData[0].displayOrder))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                if (int.Parse(reqModel.reqData[0].templateID) > 0)//update
                {
                    Bizcs.BLL.flow_template tempBll = new Bizcs.BLL.flow_template();
                    Bizcs.Model.flow_template tempModel = tempBll.GetModel(int.Parse(reqModel.reqData[0].templateID));

                    tempModel.templateID = int.Parse(reqModel.reqData[0].templateID);
                    tempModel.templateName = reqModel.reqData[0].templateName;
                    tempModel.templateDesc = reqModel.reqData[0].templateDesc;
                    tempModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder);
                    tempModel.str1Title = reqModel.reqData[0].str1Title;
                    tempModel.str2Title = reqModel.reqData[0].str2Title;
                    tempModel.str3Title = reqModel.reqData[0].str3Title;
                    tempModel.int1Title = reqModel.reqData[0].int1Title;
                    tempModel.int2Title = reqModel.reqData[0].int2Title;
                    tempModel.int3Title = reqModel.reqData[0].int3Title;
                    tempModel.date1Title = reqModel.reqData[0].date1Title;
                    tempModel.date2Title = reqModel.reqData[0].date2Title;
                    tempModel.date3Title = reqModel.reqData[0].date3Title;
                    tempModel.templateStatus = (reqModel.reqData[0].templateStatus == "on" ? 1 : 0);

                    bool iss = tempBll.Update(tempModel);
                    if (iss)
                    {
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "ok!";
                        res.resData = null;
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is something wrong occured,please retry or contact administrator!";
                        res.resData = null;
                    }
                }
                else//add
                {
                    Bizcs.BLL.flow_template tempBll = new Bizcs.BLL.flow_template();
                    Bizcs.Model.flow_template tempModel = new Bizcs.Model.flow_template();

                    tempModel.templateID = int.Parse(reqModel.reqData[0].templateID);
                    tempModel.templateName = reqModel.reqData[0].templateName;
                    tempModel.templatePK = Guid.NewGuid().ToString();
                    tempModel.templateDesc = reqModel.reqData[0].templateDesc;
                    tempModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder);
                    tempModel.str1Title = reqModel.reqData[0].str1Title;
                    tempModel.str2Title = reqModel.reqData[0].str2Title;
                    tempModel.str3Title = reqModel.reqData[0].str3Title;
                    tempModel.int1Title = reqModel.reqData[0].int1Title;
                    tempModel.int2Title = reqModel.reqData[0].int2Title;
                    tempModel.int3Title = reqModel.reqData[0].int3Title;
                    tempModel.date1Title = reqModel.reqData[0].date1Title;
                    tempModel.date2Title = reqModel.reqData[0].date2Title;
                    tempModel.date3Title = reqModel.reqData[0].date3Title;
                    tempModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                    tempModel.createTime = DateTime.Now;
                    tempModel.isReady = 0;
                    tempModel.templateStatus = (reqModel.reqData[0].templateStatus == "on" ? 1 : 0);

                    int tempID = tempBll.Add(tempModel);
                    if (tempID > 0)
                    {



                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "ok!";
                        res.resData = null;
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is something wrong occured,please retry or contact administrator!";
                        res.resData = null;
                    }
                }
            }
            return res;
        }

        [HttpPost(Name = "tempDel")]
        public ResponseSet<string> tempDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0 || !VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Query failed,no valid arguments!";
                res.resData = null;
            }
            else
            {
                int tempID = int.Parse(reqModel.reqData[0]);
                Bizcs.BLL.flow_template tempBll = new Bizcs.BLL.flow_template();
                Bizcs.Model.flow_template menuModel = tempBll.GetModel(tempID);
                DataSet ds = new Bizcs.BLL.flow_tempNode().GetList(" templateID=" + tempID);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    res.status = -10;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "This template has the flow node (including disabled nodes), please remove all nodes first!";
                    res.resData = null;
                }
                else
                {
                    bool iss = tempBll.Delete(tempID);
                    if (iss)
                    {
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.resData = null;
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is something wrong occured,please retry or contact administrator!";
                        res.resData = null;
                    }
                }
            }
            return res;
        }

        [HttpPost(Name = "tempList")]
        public ResponseSet<templateList> tempList([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<templateList> res = new ResponseSet<templateList>();
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

                string strwhere1 = " templateStatus=" + reqModel.reqData[0].ons;
                string strwhere2 = reqModel.reqData[0].kw == "" ? "" : "and templateName like '%" + reqModel.reqData[0].kw + "%'";

                Bizcs.BLL.flow_template tempBll = new Bizcs.BLL.flow_template();
                DataSet dsApp = tempBll.GetSimpleListByPage(strwhere1 + strwhere2, " displayOrder ", sIndex, eIndex);
                DataSet dsNumber = tempBll.GetList(strwhere1 + strwhere2);
                if (dsApp.Tables[0] != null && dsApp.Tables[0].Rows.Count > 0)
                {
                    List<templateList> tempList = listHelper.ConvertDtToList<templateList>(dsApp.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsNumber.Tables[0].Rows.Count;
                    res.resData = tempList.ToList();
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

        [HttpPost(Name = "tempDetail")]
        public ResponseSet<templateDetail> tempDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<templateDetail> res = new ResponseSet<templateDetail>();
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
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Invalid templateID!";
                res.resData = null;
            }
            else
            {
                string strwhere1 = " templateID=" + reqModel.reqData[0];

                Bizcs.BLL.flow_template tempBll = new Bizcs.BLL.flow_template();
                DataSet dsApp = tempBll.GetSimpleList(strwhere1);
                if (dsApp.Tables[0] != null && dsApp.Tables[0].Rows.Count > 0)
                {
                    List<templateDetail> tempList = listHelper.ConvertDtToList<templateDetail>(dsApp.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = tempList.ToList();
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

        [HttpPost(Name = "nodeDetail")]
        public ResponseSet<string> nodeDetail([FromBody] RequestSet<string> reqModel)
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
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]) || !VerifyHelper.isSafe(reqModel.reqData[1]))
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
                res.message = "Invalid templateID!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.flow_instanceNode nodeBll = new Bizcs.BLL.flow_instanceNode();
                Bizcs.Model.flow_instanceNode nodeModel = nodeBll.GetModelByNodePK(int.Parse(reqModel.reqData[0]), reqModel.reqData[1]);
                Bizcs.Model.flow_instance instModel = new Bizcs.BLL.flow_instance().GetModel(nodeModel.instanceID);
                if (nodeModel != null)
                {
                    string nodeName = nodeModel.nodeName;
                    string approver = "";
                    string[] approve = nodeModel.approve.Split('|');
                    if (approve.Length == 2)
                    {
                        switch (approve[0])
                        {
                            case "role":
                                string[] roleType = approve[1].Split(':');
                                if (roleType[0] == "data")
                                {
                                    approver = "Role: " + new Bizcs.BLL.sys_dataRole().GetModel(int.Parse(roleType[1])).roleName;
                                }
                                if (roleType[0] == "menu")
                                {
                                    approver = "Role: " + new Bizcs.BLL.sys_dataRole().GetModel(int.Parse(roleType[1])).roleName;
                                }
                                break;
                            case "person":
                                approver = "Person: " + new Bizcs.BLL.psn_psnMain().GetModel(int.Parse(approve[1])).psnName;
                                break;
                            case "position":
                                approver = "Position: " + new Bizcs.BLL.org_orgMain().GetModel(int.Parse(approve[1])).orgName;
                                break;
                            case "reportLine":
                                approver = "Report Line: " + (approve[1] == "dept1" ? "The Superior of department" : (approve[1] == "dept2" ? "The Superior of higer Department" : (approve[1] == "unit1" ? "The head of Organization" : (approve[1] == "unit2" ? "The head of higer Organization" : "Oneself"))));
                                break;
                        }
                    }

                    string condition = "";
                    if (nodeModel.condition != "" && nodeModel.condition != "||" && nodeModel.condition.Substring(0, 2) != "||")
                    {
                        string[] cond = nodeModel.condition.Split('|');
                        Bizcs.Model.flow_template tempModel = new Bizcs.BLL.flow_template().GetModel(instModel.templateID);
                        if (!tempModel.int1Title.IsNullOrEmpty())
                        {
                            condition = tempModel.int1Title + FlowHelper.changeSymbol(cond[1]) + instModel.int1Value;
                        }
                        if (!tempModel.str1Title.IsNullOrEmpty())
                        {
                            condition = tempModel.str1Title + FlowHelper.changeSymbol(cond[1]) + instModel.str1Value;
                        }
                        if (!tempModel.date1Title.IsNullOrEmpty())
                        {
                            condition = tempModel.date1Title + FlowHelper.changeSymbol(cond[1]) + instModel.date1Value;
                        }
                    }

                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = [nodeModel.nodeName, approver, condition];
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

        [HttpPost(Name = "nodeList")]
        public ResponseSet<nodeEdit> nodeList([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<nodeEdit> res = new ResponseSet<nodeEdit>();
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
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Invalid templateID!";
                res.resData = null;
            }
            else
            {
                string strwhere1 = " templateID=" + reqModel.reqData[0];

                Bizcs.BLL.flow_tempNode nodeBll = new Bizcs.BLL.flow_tempNode();
                DataSet dsNode = nodeBll.GetNodeList(strwhere1);
                if (dsNode.Tables[0] != null && dsNode.Tables[0].Rows.Count > 0)
                {
                    List<nodeEdit> nodeList = listHelper.ConvertDtToList<nodeEdit>(dsNode.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = nodeList.ToList();
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

        [HttpPost(Name = "nodeSet")]
        public ResponseSet<string> nodeSet(RequestSet<nodeEdit> reqModel)
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

            else if (!VerifyHelper.isSafe(reqModel.reqData[0].nodePK) || !VerifyHelper.isSafe(reqModel.reqData[0].name)
                || !VerifyHelper.isSafe(reqModel.reqData[0].prevID) || !VerifyHelper.isSafe(reqModel.reqData[0].type)
                || !VerifyHelper.isSafe(reqModel.reqData[0].approve) || !VerifyHelper.isSafe(reqModel.reqData[0].condition))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.flow_tempNode nodeBll = new Bizcs.BLL.flow_tempNode();
                nodeBll.DeleteBeforeSave(reqModel.reqData[0].templateID);

                int addcount = 0;
                for (int i = 0; i < reqModel.reqData.Count; i++)
                {
                    Bizcs.Model.flow_tempNode nodeModel = new Bizcs.Model.flow_tempNode();
                    nodeModel.templateID = reqModel.reqData[i].templateID;
                    nodeModel.nodePk = reqModel.reqData[i].nodePK;
                    nodeModel.nodeName = reqModel.reqData[i].name;
                    nodeModel.prevNodePK = reqModel.reqData[i].prevID;
                    nodeModel.type = reqModel.reqData[i].type;
                    nodeModel.isEnd = reqModel.reqData[i].isEnd;
                    nodeModel.condition = reqModel.reqData[i].condition;
                    nodeModel.approve = reqModel.reqData[i].approve;
                    nodeModel.left = reqModel.reqData[i].left;
                    nodeModel.top = reqModel.reqData[i].top;
                    nodeModel.nodeStatus = 1;
                    nodeModel.createTime = DateTime.Now;
                    nodeModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);

                    addcount += nodeBll.Add(nodeModel) > 0 ? 1 : 0;
                }

                if (addcount == reqModel.reqData.Count)
                {
                    Bizcs.BLL.flow_template tempBll = new Bizcs.BLL.flow_template();
                    Bizcs.Model.flow_template tempModel = tempBll.GetModel(reqModel.reqData[0].templateID);
                    tempModel.isReady = 1;
                    tempBll.Update(tempModel);

                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "ok!";
                    res.resData = null;
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is something wrong occured,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getInstance")]
        public ResponseSet<instanceDetail> getInstance([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<instanceDetail> res = new ResponseSet<instanceDetail>();
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
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Invalid templateID!";
                res.resData = null;
            }
            else
            {
                string strwhere1 = " nodeID=" + reqModel.reqData[0];

                Bizcs.BLL.flow_instanceNode msgBll = new Bizcs.BLL.flow_instanceNode();
                Bizcs.Model.flow_instanceNode nodeModel = msgBll.GetModel(int.Parse(reqModel.reqData[0]));
                if (nodeModel != null)
                {
                    DataSet dsInstantce = new Bizcs.BLL.flow_instance().GetShowList(nodeModel.instanceID);
                    List<instanceDetail> instlist = listHelper.ConvertDtToList<instanceDetail>(dsInstantce.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = instlist;
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

        [HttpPost(Name = "setInstance")]
        public ResponseSet<string> setInstance([FromBody] RequestSet<string> reqModel)
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
                res.message = "Invalid templateID!";
                res.resData = null;
            }
            else
            {

                Bizcs.BLL.flow_instance instBll = new Bizcs.BLL.flow_instance();
                Bizcs.Model.flow_instance nodeModel = instBll.GetModel(int.Parse(reqModel.reqData[0]));
                if (nodeModel != null)
                {
                    nodeModel.instanceStatus = -1;
                    instBll.Update(nodeModel);

                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = null;
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

        [HttpPost(Name = "instNodeList")]
        public ResponseSet<nodeEdit> instNodeList([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<nodeEdit> res = new ResponseSet<nodeEdit>();
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
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Invalid instanceID!";
                res.resData = null;
            }
            else
            {
                string strwhere1 = " instanceID=" + reqModel.reqData[0];

                Bizcs.BLL.flow_instanceNode nodeBll = new Bizcs.BLL.flow_instanceNode();
                DataSet dsNode = nodeBll.GetNodeList(strwhere1);
                if (dsNode.Tables[0] != null && dsNode.Tables[0].Rows.Count > 0)
                {
                    List<nodeEdit> nodeList = listHelper.ConvertDtToList<nodeEdit>(dsNode.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = nodeList.ToList();
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

        [HttpPost(Name = "instDetail")]
        public ResponseSet<instanceList> instDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<instanceList> res = new ResponseSet<instanceList>();
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
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Invalid instanceID!";
                res.resData = null;
            }
            else
            {
                string strwhere1 = " instanceID=" + reqModel.reqData[0];

                Bizcs.BLL.flow_instance instBll = new Bizcs.BLL.flow_instance();
                DataSet dsinst = instBll.GetSimpleList(strwhere1);
                if (dsinst.Tables[0] != null && dsinst.Tables[0].Rows.Count > 0)
                {
                    List<instanceList> instList = listHelper.ConvertDtToList<instanceList>(dsinst.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = instList.ToList();
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

        [HttpPost(Name = "submitApprove")]
        public ResponseSet<instanceDetail> submitApprove([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<instanceDetail> res = new ResponseSet<instanceDetail>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count < 3 || reqModel.reqData.Count > 3)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Wrong arguments number!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]) || !VerifyHelper.isSafe(reqModel.reqData[1]) || !VerifyHelper.isSafe(reqModel.reqData[2]))
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
                int nodeID = int.Parse(reqModel.reqData[0]);
                int opinion = reqModel.reqData[1] == "agree" ? 1 : 0;
                string reason = reqModel.reqData[2];

                Bizcs.BLL.flow_instance instBll = new Bizcs.BLL.flow_instance();
                Bizcs.BLL.flow_instanceNode nodeBll = new Bizcs.BLL.flow_instanceNode();
                Bizcs.Model.flow_instanceNode nodeModel = nodeBll.GetModel(nodeID);
                Bizcs.Model.flow_instance instModel = instBll.GetModel(nodeModel.instanceID);
                Bizcs.Model.flow_approveLog logModel = new Bizcs.Model.flow_approveLog();
                Bizcs.BLL.flow_approveLog logBll = new Bizcs.BLL.flow_approveLog();

                List<Bizcs.Model.flow_approveLog> dsLog = logBll.GetModelList("nodeID=" + nodeID);
                if (dsLog != null && dsLog.Count > 0)
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "This node has approved by other people!";
                    res.resData = null;
                }
                else if (instModel.instanceStatus != 1)
                {
                    res.status = -12;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "This flow instance has been closed by administrator, cannot to process!";
                    res.resData = null;
                }
                else
                {

                    Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                    DataSet dsPsn = psnBll.GetServList(VerifyHelper.getPsnID(reqModel.uToken));
                    List<servPsnInfoSimple> approver = listHelper.ConvertDtToList<servPsnInfoSimple>(dsPsn.Tables[0]);

                    if (reqModel.reqData[1] == "agree")
                    {
                        instModel.doneNodePK = nodeModel.nodePk;
                        instModel.isEnd = nodeModel.isEnd == 1 ? 1 : 0;
                        instModel.isPass = nodeModel.isEnd == 1 ? 1 : 0;
                        instBll.Update(instModel);

                        logModel.approvePK = Guid.NewGuid().ToString();
                        logModel.approverID = VerifyHelper.getPsnID(reqModel.uToken);
                        logModel.approverPK = approver[0].psnPK;
                        logModel.approverUnit = approver[0].unitName;
                        logModel.approverDept = approver[0].deptName;
                        logModel.approverPost = approver[0].postName;
                        logModel.instanceID = instModel.instanceID;
                        logModel.instancePK = instModel.instancePK;
                        logModel.nodeID = nodeModel.nodeID;
                        logModel.nodePK = nodeModel.nodePk;
                        logModel.isAgree = 1;
                        logModel.isNote = reason;
                        logModel.approveTime = DateTime.Now;

                        logBll.Add(logModel);

                        if (nodeModel.isEnd != 1)
                        {
                            FlowHelper.goToNextNode(instModel.instanceID, instModel.psnID, nodeModel.nodeID);
                        }
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Agree opinion submitted successfully!!";
                        res.resData = null;
                    }
                    else if (reqModel.reqData[1] == "disagree")
                    {
                        instModel.doneNodePK = nodeModel.nodePk;
                        instModel.isEnd = 1;
                        instModel.isPass = -1;
                        instBll.Update(instModel);

                        logModel.approverID = VerifyHelper.getPsnID(reqModel.uToken);
                        logModel.approverUnit = approver[0].unitName;
                        logModel.approverDept = approver[0].deptName;
                        logModel.approverPost = approver[0].postName;
                        logModel.instanceID = instModel.instanceID;
                        logModel.nodeID = nodeModel.nodeID;
                        logModel.isAgree = -1;
                        logModel.isNote = reason;
                        logModel.approveTime = DateTime.Now;

                        logBll.Add(logModel);

                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Disagree opinion submitted successfully!!";
                        res.resData = null;
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Invalid opinon argument!";
                        res.resData = null;
                    }
                }

            }
            return res;
        }

        [HttpPost(Name = "getApproveLog")]
        public ResponseSet<approveLogList> getApproveLog([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<approveLogList> res = new ResponseSet<approveLogList>();
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
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Invalid instanceID!";
                res.resData = null;
            }
            else
            {
                string strwhere1 = " instanceID=" + reqModel.reqData[0];

                Bizcs.BLL.flow_approveLog logBll = new Bizcs.BLL.flow_approveLog();
                DataSet dsNode = logBll.GetLogList(strwhere1);
                if (dsNode.Tables[0] != null && dsNode.Tables[0].Rows.Count > 0)
                {
                    List<approveLogList> nodeList = listHelper.ConvertDtToList<approveLogList>(dsNode.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = nodeList.ToList();
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getNextApprover")]
        public ResponseSet<nextApprover> getNextApprover([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<nextApprover> res = new ResponseSet<nextApprover>();
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
            else if (!VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Invalid instanceID!";
                res.resData = null;
            }
            else
            {
                DataSet dsNextApprver = new Bizcs.BLL.flow_instance().getNextApprover(int.Parse(reqModel.reqData[0]));
                if (dsNextApprver.Tables[0] != null && dsNextApprver.Tables[0].Rows.Count > 0)
                {
                    List<nextApprover> approverList = listHelper.ConvertDtToList<nextApprover>(dsNextApprver.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = approverList.ToList();
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getMonitorList")]
        public ResponseSet<instanceList> getMonitorList([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<instanceList> res = new ResponseSet<instanceList>();
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
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].ty) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
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

                string strwhere1 = " instanceStatus =" + reqModel.reqData[0].ons;
                string strwhere2 = reqModel.reqData[0].ty == "" ? "" : " and templateID =" + reqModel.reqData[0].ty;

                Bizcs.BLL.flow_instance instBll = new Bizcs.BLL.flow_instance();
                DataSet dsInst = instBll.GetSimpleListByPage(strwhere1 + strwhere2, "createTime desc", sIndex, eIndex);
                DataSet dsInstNumber = instBll.GetList(strwhere1 + strwhere2);
                if (dsInst != null && dsInst.Tables[0].Rows.Count > 0)
                {
                    List<instanceList> instlist = listHelper.ConvertDtToList<instanceList>(dsInst.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsInstNumber.Tables[0].Rows.Count;
                    res.resData = instlist;
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
