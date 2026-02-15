using appsin.ApiModels;
using appsin.Bizcs.BLL;
using appsin.Common;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml.Linq;

namespace appsin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppServController : ControllerBase
    {
        [HttpPost(Name = "createFlow")]
        public servResArgs<string> createFlow(servFlowArgs flowArgus)
        {
            const string thisApiCode = "30101";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;
            servReqArgs reqArgs = new servReqArgs();
            reqArgs.aukey = flowArgus.aukey;
            reqArgs.args = flowArgus.templatePK + flowArgus.flowName;

            servResArgs<string> res = new servResArgs<string>();

            if (!VerifyHelper.isSafe(flowArgus.flowName) || !VerifyHelper.isSafe(flowArgus.flowDesc) || !VerifyHelper.isSafe(flowArgus.contentUrl)
                || !VerifyHelper.isSafe(flowArgus.str1Value) || !VerifyHelper.isSafe(flowArgus.aukey) || !VerifyHelper.isSafe(flowArgus.templatePK))
            {
                res.status = "FAIL";
                res.message = "Args including high risk character！！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }
            else if (VerifyHelper.isUrl(flowArgus.contentUrl))
            {
                res.status = "FAIL";
                res.message = "Wrong contentUrl,you don't need to use full url,just the part which following the domain and port, the syestem will attach domain and port automatically！";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                Bizcs.Model.flow_template templateModel = new Bizcs.BLL.flow_template().GetModelByPK(flowArgus.templatePK);
                Bizcs.Model.psn_psnMain psnModel = psnBll.GetModelByPsnPK(flowArgus.psnPK);
                Bizcs.Model.psn_psnMain createPsnModel = psnBll.GetModelByPsnPK(flowArgus.createPsnPK);
                if (templateModel == null || psnModel == null)
                {
                    res.status = "FAIL";
                    res.message = "Wrong templatePK or psnPK, cannot find the right flow or person！";
                    res.resData = null;
                }
                else
                {

                    Bizcs.BLL.flow_tempNode tempNodeBll = new Bizcs.BLL.flow_tempNode();
                    Bizcs.BLL.flow_instance instanceBll = new Bizcs.BLL.flow_instance();
                    Bizcs.Model.flow_instance instanceModel = new Bizcs.Model.flow_instance();
                    Bizcs.BLL.flow_instanceNode instNodeBll = new Bizcs.BLL.flow_instanceNode();
                    Bizcs.Model.flow_instanceNode instNodeModel = new Bizcs.Model.flow_instanceNode();

                    List<Bizcs.Model.flow_tempNode> nodeList = tempNodeBll.GetModelList("templateID=" + templateModel.templateID);

                    instanceModel.instancePK = Guid.NewGuid().ToString();
                    instanceModel.psnID = psnModel.psnID;
                    instanceModel.psnPK = psnModel.psnPK;
                    instanceModel.createPsnID = createPsnModel.psnID;
                    instanceModel.createPsnPK = createPsnModel.psnPK;
                    instanceModel.instanceName = flowArgus.flowName;
                    instanceModel.instanceDesc = flowArgus.flowDesc;
                    instanceModel.contentUrl = flowArgus.contentUrl;
                    instanceModel.templateID = templateModel.templateID;
                    instanceModel.templatePK = templateModel.templatePK;
                    instanceModel.appID = VerifyHelper.getAppID(reqArgs.aukey);
                    instanceModel.str1Value = flowArgus.str1Value;
                    instanceModel.int1Value = (int)flowArgus.int1Value;
                    instanceModel.date1Value = flowArgus.date1Value;
                    instanceModel.createTime = DateTime.Now;
                    instanceModel.isEnd = 0;
                    instanceModel.isPass = 0;
                    instanceModel.isError = 0;
                    instanceModel.errorDesc = "";
                    instanceModel.instanceStatus = 1;
                    instanceModel.doneNodePK = "node0";

                    int instanceID = instanceBll.Add(instanceModel);

                    if (instanceID <= 0)
                    {
                        res.status = "FAIL";
                        res.message = "Create flow fail, please retry or contact administrator！";
                        res.resData = null;

                    }
                    else if (nodeList == null || nodeList.Count == 0)
                    {
                        res.status = "FAIL";
                        res.message = "There is no approve nodes for this flow！";
                        res.resData = null;

                    }
                    else
                    {
                        for (int i = 0; i < nodeList.Count; i++)
                        {
                            instNodeModel.instanceID = instanceID;
                            instNodeModel.instancePK = instanceModel.instancePK;
                            instNodeModel.nodePk = nodeList[i].nodePk;
                            instNodeModel.nodeName = nodeList[i].nodeName;
                            instNodeModel.approve = nodeList[i].approve;
                            instNodeModel.condition = nodeList[i].condition;
                            instNodeModel.prevNodePK = nodeList[i].prevNodePK;
                            instNodeModel.isEnd = nodeList[i].isEnd;
                            instNodeModel.left = nodeList[i].left;
                            instNodeModel.top = nodeList[i].top;
                            instNodeModel.type = nodeList[i].type;
                            instNodeModel.nodeStatus = 1;

                            instNodeBll.Add(instNodeModel);
                            instNodeBll.UpdatePrevNodeID(instanceID);
                        }

                        //Create new thread to send message to next approver.
                        Task task = Task.Run(() =>
                        {
                            Bizcs.Model.flow_instanceNode nodeModel = new Bizcs.BLL.flow_instanceNode().GetModelByNodePK(instanceID, "node0");
                            FlowHelper.goToNextNode(instanceID, psnModel.psnID, nodeModel.nodeID);
                        });

                        res.status = "SUCCESS";
                        res.message = "The flow has been created successfully, the flow instancePK is in the resData！";
                        res.resData = [instanceModel.instancePK];
                    }
                }
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getFlowTemplateList")]
        public servResArgs<templateApi> getFlowTemplateList(servReqArgs reqArgs)
        {
            const string thisApiCode = "30102";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<templateApi> res = new servResArgs<templateApi>();
            if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args including high risk character！！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }

            else
            {
                Bizcs.BLL.flow_template tempBll = new Bizcs.BLL.flow_template();
                DataSet dsFlow = tempBll.GetList("templateStatus = 1 and isReady = 1");
                if (dsFlow.Tables[0] != null && dsFlow.Tables[0].Rows.Count > 0)
                {
                    List<templateApi> listFlow = listHelper.ConvertDtToList<templateApi>(dsFlow.Tables[0]);
                    res.status = "SUCCESS";
                    res.message = "Query success,the data is in the resData！";
                    res.resData = listFlow;
                }
                else
                {
                    res.status = "SUCCESS";
                    res.message = "Query success,but no data！";
                    res.resData = null;
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getFlowTemplateNodes")]
        public servResArgs<nodesApi> getFlowTemplateNodes(servReqArgs reqArgs)
        {
            const string thisApiCode = "30103";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<nodesApi> res = new servResArgs<nodesApi>();
            if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args including high risk character！！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }

            else
            {
                Bizcs.Model.flow_template templateModel = new Bizcs.BLL.flow_template().GetModelByPK(reqArgs.args);
                if (templateModel == null)
                {
                    res.status = "FAIL";
                    res.message = "Wrong templatePK, cannot find flow template！";
                    res.resData = null;
                }
                else
                {
                    StringBuilder strWhere = new StringBuilder();
                    List<SqlParameter> parms = new List<SqlParameter>();
                    strWhere.Append("nodeStatus=1 and templateID=@templateID");
                    parms.Add(new SqlParameter("@templateID", templateModel.templateID));

                    DataSet dsNodes = new Bizcs.BLL.flow_tempNode().GetList(parms.ToString(),parms.ToArray());
                    if (dsNodes == null || dsNodes.Tables[0].Rows.Count == 0)
                    {
                        res.status = "SUCCESS";
                        res.message = "Query success,but no data！";
                        res.resData = null;
                    }
                    else
                    {
                        List<nodesApi> resData = listHelper.ConvertDtToList<nodesApi>(dsNodes.Tables[0]);
                        res.status = "SUCCESS";
                        res.message = "Query success, the data is in the resData！";
                        res.resData = resData;
                    }
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getFlowInstance")]
        public servResArgs<instanceApi<nodesApi, LogsApi>> getFlowInstance(servReqArgs reqArgs)
        {
            const string thisApiCode = "30104";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<instanceApi<nodesApi, LogsApi>> res = new servResArgs<instanceApi<nodesApi, LogsApi>>();
            if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args including high risk character！！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }

            else
            {
                StringBuilder strWhere = new StringBuilder();
                List<SqlParameter> parms = new List<SqlParameter>();
                strWhere.Append("instancePK=@instancePK");
                parms.Add(new SqlParameter("@instancePK", reqArgs.args));

                List<Bizcs.Model.flow_instance> listInstance = new Bizcs.BLL.flow_instance().GetModelList(strWhere.ToString(),parms.ToArray());
                if (listInstance == null || listInstance.Count == 0)
                {
                    res.status = "FAIL";
                    res.message = "Wrong instancePK, cannot find flow instance！";
                    res.resData = null;
                }
                else
                {
                    StringBuilder strWhere1 = new StringBuilder();
                    List<SqlParameter> parms1 = new List<SqlParameter>();
                    strWhere1.Append("nodeStatus=1 and instanceID=@instanceID");
                    parms1.Add(new SqlParameter("@instanceID", listInstance[0].instanceID));

                    DataSet dsNodes = new Bizcs.BLL.flow_instanceNode().GetList(strWhere1.ToString(),parms1.ToArray());

                    StringBuilder strWhere2 = new StringBuilder();
                    List<SqlParameter> parms2 = new List<SqlParameter>();
                    strWhere2.Append("instanceID=@instanceID");
                    parms2.Add(new SqlParameter("@instanceID", listInstance[0].instanceID));

                    DataSet dsApproveLog = new Bizcs.BLL.flow_approveLog().GetLogList(strWhere2.ToString(),parms2.ToArray());
                    List<nodesApi> nodeList = listHelper.ConvertDtToList<nodesApi>(dsNodes.Tables[0]);
                    List<LogsApi> logLists = listHelper.ConvertDtToList<LogsApi>(dsApproveLog.Tables[0]);

                    var apiData = new instanceApi<nodesApi, LogsApi>
                    {
                        instancePK = listInstance[0].instancePK,
                        instanceName = listInstance[0].instanceName,
                        instanceDesc = listInstance[0].instanceDesc,
                        doneNodePK = listInstance[0].doneNodePK,
                        contentUrl = listInstance[0].contentUrl,
                        isPass = listInstance[0].isPass,
                        isEnd = listInstance[0].isEnd,
                        isError = listInstance[0].isError,
                        errorDesc = listInstance[0].errorDesc,
                        psnPK = listInstance[0].psnPK,
                        createPsnPK = listInstance[0].createPsnPK,
                        createTime = listInstance[0].createTime,
                        apprNodes = nodeList,
                        apprLogs = logLists
                    };

                    res.status = "SUCCESS";
                    res.message = "All data is in the resData！";
                    res.resData = new List<instanceApi<nodesApi, LogsApi>>();
                    res.resData.Add(apiData);
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getPsnSearch")]
        public servResArgs<servPsnInfoSimple> getPsnSearch(servReqArgs reqArgs)
        {
            const string thisApiCode = "11103";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<servPsnInfoSimple> res = new servResArgs<servPsnInfoSimple>();

            if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args including high risk character！！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }
            else
            {
                string keyWord = reqArgs.args;
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();

                StringBuilder strWhere = new StringBuilder();
                List<SqlParameter> parms = new List<SqlParameter>();
                strWhere.Append(" (psnName like @psnName or psnCode like @psnCode) and psnStatus=1 ");
                parms.Add(new SqlParameter("@psnName", $"%{keyWord}%"));
                parms.Add(new SqlParameter("@psnCode", $"%{keyWord}%"));

                DataSet dsObj = psnBll.GetSimpleList(strWhere.ToString(),parms.ToArray());
                if (dsObj.Tables[0] != null && dsObj.Tables[0].Rows.Count > 0)
                {
                    List<servPsnInfoSimple> resData = listHelper.ConvertDtToList<servPsnInfoSimple>(dsObj.Tables[0]);
                    res.status = "SUCCESS";
                    res.message = "Query success！";
                    res.resData = resData;
                }
                else
                {
                    res.status = "SUCCESS";
                    res.message = "Query success,but no data！";
                    res.resData = null;
                }

                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getPsnInfo")]
        public servResArgs<servPsnInfoSimple> getPsnInfo(servReqArgs reqArgs)
        {
            const string thisApiCode = "11101";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<servPsnInfoSimple> res = new servResArgs<servPsnInfoSimple>();
            if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args including high risk character！！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }

            else
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                DataSet dspsn = psnBll.GetListByPsnPK(reqArgs.args);
                if (dspsn.Tables[0] != null && dspsn.Tables[0].Rows.Count > 0)
                {
                    List<servPsnInfoSimple> psnInfoSimple = listHelper.ConvertDtToList<servPsnInfoSimple>(dspsn.Tables[0]);
                    res.status = "SUCCESS";
                    res.message = "Query success,the data in resData！";
                    res.resData = psnInfoSimple;
                }
                else
                {
                    res.status = "SUCCESS";
                    res.message = "Query success,but no data！";
                    res.resData = null;
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getPsnInfoFull")]
        public servResArgs<servPsnInfoFull> getPsnInfoFull(servReqArgs reqArgs)
        {
            const string thisApiCode = "11102";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<servPsnInfoFull> res = new servResArgs<servPsnInfoFull>();
            if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args including high risk character！！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                DataSet dspsn = psnBll.GetListByPsnPK(reqArgs.args);
                if (dspsn.Tables[0] != null && dspsn.Tables[0].Rows.Count > 0)
                {
                    List<servPsnInfoFull> psnInfoFull = listHelper.ConvertDtToList<servPsnInfoFull>(dspsn.Tables[0]);
                    res.status = "SUCCESS";
                    res.message = "Query success,the data in resData！";
                    res.resData = psnInfoFull;
                }
                else
                {
                    res.status = "SUCCESS";
                    res.message = "Query success,but no data！";
                    res.resData = null;
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getAllOrgList")]
        public servResArgs<servOrgList> getAllOrgList(servReqArgs reqArgs)
        {
            const string thisApiCode = "12101";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<servOrgList> res = new servResArgs<servOrgList>();
            if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                DataSet ds = orgBll.GetOrgList("");
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<servOrgList> orgList = listHelper.ConvertDtToList<servOrgList>(ds.Tables[0]);
                    res.status = "SUCCESS";
                    res.message = "Query success,the data in resData！";
                    res.resData = orgList;
                }
                else
                {
                    res.status = "SUCCESS";
                    res.message = "No data";
                    res.resData = null;
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getOrgInfo")]
        public servResArgs<servOrgList> getOrgInfo(servReqArgs reqArgs)
        {
            const string thisApiCode = "12102";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<servOrgList> res = new servResArgs<servOrgList>();

            if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args including high risk character！！";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                DataSet ds = orgBll.GetOrgList(reqArgs.args);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<servOrgList> orgList = listHelper.ConvertDtToList<servOrgList>(ds.Tables[0]);
                    res.status = "SUCCESS";
                    res.message = "";
                    res.resData = orgList;
                }
                else
                {
                    res.status = "SUCCESS";
                    res.message = "No data";
                    res.resData = null;
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getDataOsrz")]
        public servResArgs<servDataOsrz> getDataOsrz(servReqArgs reqArgs)
        {
            const string thisApiCode = "20102";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<servDataOsrz> res = new servResArgs<servDataOsrz>();

            if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api accessment！";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args including high risk character！！";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                Bizcs.Model.psn_psnMain psnModel = psnBll.GetModelByPsnPK(reqArgs.args);
                if (psnModel != null)
                {
                    Bizcs.BLL.sys_dataBind bindBll = new Bizcs.BLL.sys_dataBind();
                    DataSet ds = bindBll.GetOsrzOrg(psnModel.psnID);
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        List<servDataOsrz> osrzList = listHelper.ConvertDtToList<servDataOsrz>(ds.Tables[0]);
                        res.status = "SUCCESS";
                        res.message = "";
                        res.resData = osrzList;
                    }
                    else
                    {
                        res.status = "SUCCESS";
                        res.message = "No data";
                        res.resData = null;
                    }
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getFieldSet")]
        public servResArgs<servFieldset> getFieldSet(servReqArgs reqArgs)
        {
            const string thisApiCode = "20103";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<servFieldset> res = new servResArgs<servFieldset>();
            if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api authorization！";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args1 including high risk characters！";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_fieldset setBll = new Bizcs.BLL.sys_fieldset();
                DataSet dsset = setBll.GetSimpleList();
                if (dsset.Tables[0] != null && dsset.Tables[0].Rows.Count > 0)
                {
                    List<servFieldset> setList = listHelper.ConvertDtToList<servFieldset>(dsset.Tables[0]);
                    res.status = "SUCCESS";
                    res.message = "";
                    res.resData = setList;

                }
                else
                {
                    res.status = "SUCCESS";
                    res.message = "No data";
                    res.resData = null;
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "getFieldItem")]
        public servResArgs<servFielditem> getFieldItem(servReqArgs reqArgs)
        {
            const string thisApiCode = "20104";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<servFielditem> res = new servResArgs<servFielditem>();
            if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api authorization！";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args1 including high risk characters！";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                DataSet dsset = itemBll.GetServItem(reqArgs.args);
                if (dsset.Tables[0] != null && dsset.Tables[0].Rows.Count > 0)
                {
                    List<servFielditem> itemInfo = listHelper.ConvertDtToList<servFielditem>(dsset.Tables[0]);
                    res.status = "SUCCESS";
                    res.message = "";
                    res.resData = itemInfo;

                }
                else
                {
                    res.status = "SUCCESS";
                    res.message = "No data";
                    res.resData = null;
                }
                appID = VerifyHelper.getAppID(reqArgs.aukey);
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "addMsg")]
        public servResArgs<servFielditem> addMsg(ApiModels.msgApi<ApiModels.msgData> msgInfo)
        {
            const string thisApiCode = "20105";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<servFielditem> res = new servResArgs<servFielditem>();

            servReqArgs reqArgs = new servReqArgs();
            reqArgs.aukey = msgInfo.aukey;
            reqArgs.args = msgInfo.args;

            if (!VerifyHelper.isSafe(msgInfo.args))
            {
                res.status = "FAIL";
                res.message = "Args1 including high risk characters！";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(msgInfo.aukey))
            {
                res.status = "FAIL";
                res.message = "Aukey including high risk characters！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(msgInfo.aukey))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                res.status = "FAIL";
                res.message = "No api authorization！";
                res.resData = null;
            }
            else if (msgInfo.msgData.Count > 100)
            {
                res.status = "FAIL";
                res.message = "The maximum number of message data is 100！";
                res.resData = null;
            }
            else
            {
                int riskCount = 0; int wrongObjCount = 0; int isUrlCount = 0;
                for (int i = 0; i < msgInfo.msgData.Count; i++)
                {
                    if (VerifyHelper.isSafe(msgInfo.msgData[i].msgTitle) || VerifyHelper.isSafe(msgInfo.msgData[i].msgContent) || VerifyHelper.isSafe(msgInfo.msgData[i].msgDesc) || VerifyHelper.isSafe(msgInfo.msgData[i].msgUrl))
                    {
                        riskCount++;
                    }
                    if (msgInfo.msgData[i].objType != "psn" && msgInfo.msgData[i].objType != "post" && msgInfo.msgData[i].objType != "dept" && msgInfo.msgData[i].objType != "unit")
                    {
                        wrongObjCount++;
                    }
                    if (VerifyHelper.isUrl(msgInfo.msgData[i].msgUrl))
                    {
                        isUrlCount++;
                    }
                }
                if (riskCount > 0)
                {
                    res.status = "FAIL";
                    res.message = "The message data include " + riskCount + " records have high risk characters!";
                    res.resData = null;
                }
                else if (wrongObjCount > 0)
                {
                    res.status = "FAIL";
                    res.message = "The message data include " + wrongObjCount + " records have wrong objType!";
                    res.resData = null;
                }
                else if (isUrlCount > 0)
                {
                    res.status = "FAIL";
                    res.message = "The message data include " + isUrlCount + " records have full url in msgUrl, Only the relative paths which following the domain name and port are supported !";
                    res.resData = null;
                }
                else
                {
                    Bizcs.BLL.app_messages msgBll = new Bizcs.BLL.app_messages();
                    int addCount = 0;
                    appID = VerifyHelper.getAppID(reqArgs.aukey);

                    for (int j = 0; j < msgInfo.msgData.Count; j++)
                    {
                        Bizcs.Model.app_messages msgModel = new Bizcs.Model.app_messages();
                        msgModel.objType = msgInfo.msgData[j].objType;
                        msgModel.objID = msgInfo.msgData[j].objID;
                        msgModel.msgTitle = msgInfo.msgData[j].msgTitle;
                        msgModel.msgContent = msgInfo.msgData[j].msgContent;
                        msgModel.msgUrl = msgInfo.msgData[j].msgUrl;
                        msgModel.msgType = appID.ToString();
                        msgModel.msgDesc = msgInfo.msgData[j].msgDesc;
                        msgModel.expireTime = msgInfo.msgData[j].expireTime;
                        msgModel.msgStatus = 1;
                        msgModel.appID = appID;
                        int msgID = msgBll.Add(msgModel);
                        if (msgID > 0)
                        {
                            addCount++;
                        }
                    }

                    res.status = "SUCCESS";
                    res.message = "The message data include " + msgInfo.msgData.Count + " records and " + addCount + " records have successfully added!";
                    res.resData = null;
                }
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "resetToken")]
        public servResArgs<DateTime> resetToken(servReqArgs reqArgs)
        {
            const string thisApiCode = "10101";
            string domain = Request.Host.Value.ToString();
            int apiID = VerifyHelper.getApiID(thisApiCode);
            int appID = 0; int osrzID = 0;

            servResArgs<DateTime> res = new servResArgs<DateTime>();
            if (!VerifyHelper.isSafe(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Aukey including high risk characters！";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "Args1 including high risk characters！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorization key！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkApiAccess(thisApiCode, reqArgs))
            {
                appID = VerifyHelper.getAppID(reqArgs.aukey);

                res.status = "FAIL";
                res.message = "No api authorization！";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_tokenMain tokenBll = new Bizcs.BLL.sys_tokenMain();
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                Bizcs.Model.psn_psnMain psnModel = psnBll.GetModelByPsnPK(reqArgs.args);

                if (psnModel != null)
                {
                    Bizcs.Model.sys_tokenMain tokenModel = tokenBll.GetModelLatest(psnModel.psnID);
                    if (tokenModel.expireTime > DateTime.Now)
                    {
                        appID = VerifyHelper.getAppID(reqArgs.aukey);
                        tokenModel.expireTime = DateTime.Now.AddMinutes(60);
                        tokenModel.appID = appID;
                        tokenBll.Update(tokenModel);
                        Bizcs.Model.sys_tokenVerify verifyModel = new Bizcs.Model.sys_tokenVerify();
                        verifyModel.tokenID = tokenModel.tokenID;
                        verifyModel.appID = appID;
                        verifyModel.verifyTime = DateTime.Now;
                        verifyModel.verifyResult = "SUCCESS";
                        new Bizcs.BLL.sys_tokenVerify().Add(verifyModel);

                        res.status = "SUCCESS";
                        res.message = "Query success,the latest expire time you can check resData！";
                        res.resData = [Convert.ToDateTime(tokenModel.expireTime)];
                    }
                    else
                    {
                        res.status = "FAIL";
                        res.message = "Token has expired！";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = "FAIL";
                    res.message = "No person,please chech psnPK！";
                    res.resData = null;
                }
                osrzID = VerifyHelper.getOsrzID(apiID, appID);
            }
            new Bizcs.BLL.api_useLog().AddApiUseLog(apiID, appID, osrzID, domain, res.status, res.message, reqArgs.aukey, "");
            return res;
        }

        [HttpPost(Name = "handShake")]
        public servResArgs<servPsnInfoSimple> handShake(servReqArgs reqArgs)
        {
            string domain = Request.Host.Value.ToString();
            servResArgs<servPsnInfoSimple> res = new servResArgs<servPsnInfoSimple>();

            if (!VerifyHelper.isSafe(reqArgs.args))
            {
                res.status = "FAIL";
                res.message = "High risk charcters！";
                res.resData = null;
            }
            else if (!VerifyHelper.checkAuKey(reqArgs.aukey))
            {
                res.status = "FAIL";
                res.message = "Wrong authorized key！";
                res.resData = null;
            }
            else
            {
                string appSID = reqArgs.aukey.Substring(0, 8);
                string goStr = reqArgs.args;
                Bizcs.BLL.app_goToLog goBll = new Bizcs.BLL.app_goToLog();
                Bizcs.Model.app_goToLog goModel = goBll.GetListByGostr(goStr, appSID);
                if (goModel != null)
                {
                    Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                    DataSet dsPsn = psnBll.GetServList(Convert.ToInt32(goModel.psnID));
                    if (dsPsn.Tables[0] != null && dsPsn.Tables[0].Rows.Count > 0)
                    {
                        goModel.callBackRes = "success";
                        goModel.callBackTime = DateTime.Now;
                        goBll.Update(goModel);

                        List<servPsnInfoSimple> servPsnInfoSimple = listHelper.ConvertDtToList<servPsnInfoSimple>(dsPsn.Tables[0]);
                        res.status = "SUCCESS";
                        res.message = "Person info in resdata";
                        res.resData = servPsnInfoSimple;
                    }
                    else
                    {
                        goModel.callBackRes = "fail due to no person info";
                        goModel.callBackTime = DateTime.Now;
                        goBll.Update(goModel);

                        res.status = "FAIL";
                        res.message = "No person info！";
                        res.resData = null;
                    }
                    new Bizcs.BLL.api_useLog().AddApiUseLog(0, VerifyHelper.getAppID(reqArgs.aukey), 0, domain, "HandShake: " + res.status, res.message, reqArgs.aukey, "");
                }
                else
                {
                    res.status = "FAIL";
                    res.message = "No goto record！";
                    res.resData = null;
                }
            }

            return res;
        }

        //This function just for test, you can use request tools such as Apipost to get Aukey and test the api above. Please note that you need to change the arguments.
        [HttpPost(Name = "genAukey")]
        public servResArgs<string> genAukey(servReqArgs reqArgs)
        {
            servResArgs<string> res = new servResArgs<string>();
            string appSID = "52021156";
            string appSecret = "wGmt9o5ucmzLpszm";
            long timestamp = GetCurrentTimestampSeconds();
            string pubkey = "MEgCQQDOGE80bY9nK8Akxw+CXmsHqF7y/kPMbAim/M9hm3w0TN/h1cFONdo7OaWF3hBRAaTYwbdTTiTQjvgi7j31ExbdAgMBAAE=";
            string aukey = appSID + CryptRSA.RsaHelper.Encrypt(pubkey, appSID + ";" + appSecret + ";" + timestamp);

            res.status = "SUCCESS";
            res.message = "";
            res.resData = [aukey];
            return res;
        }

        public static long GetCurrentTimestampSeconds()
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeSpan = currentTime - startTime;
            return (long)timeSpan.TotalSeconds;
        }

    }
}
