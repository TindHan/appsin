using appsin.ApiModels;
using appsin.Bizcs.BLL;
using appsin.Bizcs.Model;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.Xml;
namespace appsin.Common
{
    public class FlowHelper
    {
        public static void goToNextNode(int instanceID, int psnID, int doneNodeID)
        {

            Bizcs.BLL.flow_instanceNode nodeBll = new Bizcs.BLL.flow_instanceNode();
            Bizcs.Model.flow_instanceNode doneNodeModel = nodeBll.GetModel(doneNodeID);
            Bizcs.BLL.flow_instance instBll = new Bizcs.BLL.flow_instance();
            Bizcs.Model.flow_instance instModel = instBll.GetModel(instanceID);

            if (doneNodeModel.isEnd == 1)//If doneNode is the end node, the flow finished.
            {
                instModel.isEnd = 1;
                instModel.isPass = 1;
                instBll.Update(instModel);
            }
            else
            {
                //To find next Nodes first, might be multiply nodes due to node's condition.
                List<int> nextNodes = Common.FlowHelper.findNextNodes(instanceID, doneNodeID);

                if (nextNodes.Count <= 0 || (nextNodes.Count == 1 && nextNodes[0] == 0))//Cannot find next node
                {
                    //Recourd error
                    instModel.isError = 1;
                    instModel.errorDesc = "There is no next node!";
                    instBll.Update(instModel);
                }
                else
                {
                    int allSendCount = 0;
                    foreach (var nodeID in nextNodes)
                    {
                        int sendCount = findNextApprover(nodeID, psnID);
                        //If someone don't have higer Department or higer organization, the skip this node.
                        if (sendCount == -11 || sendCount == -12)
                        {
                            Bizcs.Model.flow_approveLog logModel = new Bizcs.Model.flow_approveLog();
                            Bizcs.Model.flow_instanceNode nodeModel = nodeBll.GetModel(nodeID);
                            logModel.approvePK = Guid.NewGuid().ToString();
                            logModel.approverID = 0;
                            logModel.approverPK = "0";
                            logModel.approverUnit = "";
                            logModel.approverDept = "";
                            logModel.approverPost = "";
                            logModel.instanceID = instModel.instanceID;
                            logModel.instancePK = instModel.instancePK;
                            logModel.nodeID = nodeID;
                            logModel.nodePK = nodeModel.nodePk;
                            logModel.isAgree = 1;
                            logModel.isNote = "Skipped node " + nodeModel.nodeName + " due to no corresponding higher organization";
                            logModel.approveTime = DateTime.Now;

                            new Bizcs.BLL.flow_approveLog().Add(logModel);

                            //Even though this node is skipped, but it is also a done node, so edit the doneNodePK of flow instance.
                            instModel.doneNodePK= nodeModel.nodePk;
                            instBll.Update(instModel);

                            //Skip this node and find next node.
                            goToNextNode(instanceID, psnID, nodeID);
                            allSendCount = 1;
                            continue;
                        }
                        else
                        {
                            allSendCount += sendCount;
                        }
                    }
                    if (allSendCount == 0)//if next nodes cannot find exact person, then error occured.
                    {
                        Bizcs.Model.flow_instanceNode nodeModel = new Bizcs.Model.flow_instanceNode();
                        string noApproverNodeName = "";
                        foreach (var item in nextNodes)
                        {
                            nodeModel = new Bizcs.BLL.flow_instanceNode().GetModel(item);
                            noApproverNodeName += nodeModel.nodeName + ",";
                        }
                        //Recourd error
                        instModel.isError = 1;
                        instModel.errorDesc = "Node: " + noApproverNodeName.Substring(0, noApproverNodeName.Length - 1) + " did not find approver!";
                        new Bizcs.BLL.flow_instance().Update(instModel);
                    }
                }
            }
        }

        public static List<int> findNextNodes(int instanceID, int doneNodeID)
        {
            DataSet dsNext = new Bizcs.BLL.flow_instanceNode().GetList("instanceID=" + instanceID + " and prevNodeID='" + doneNodeID + "'");
            if (dsNext == null || dsNext.Tables[0].Rows.Count == 0)
            {
                return [0];
            }
            else if (dsNext.Tables[0].Rows.Count == 1)
            {
                return [Convert.ToInt32(dsNext.Tables[0].Rows[0]["nodeID"])];
            }
            else
            {
                DataTable dt = dsNext.Tables[0];
                List<int> res = []; string condition = "";
                Bizcs.Model.flow_instance instModel = new Bizcs.BLL.flow_instance().GetModel(instanceID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    condition = dt.Rows[i]["condition"].ToString();
                    if (condition.Trim() == "||" || condition.Trim() == "" || condition.Substring(0, 2) == "||")
                    {
                        res.Add(Convert.ToInt32(dt.Rows[0]["nodeID"]));
                    }
                    else
                    {
                        string[] conList = condition.Split('|');
                        switch (conList[1])
                        {
                            case "bb":
                                if (conList[0] == "int1Value" && instModel.int1Value > int.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "str1Value" && String.CompareOrdinal(instModel.str1Value, conList[2]) < 0)
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "date1Value" && instModel.date1Value > DateTime.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }
                                break;
                            case "be":
                                if (conList[0] == "int1Value" && instModel.int1Value >= int.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "str1Value" && String.CompareOrdinal(instModel.str1Value, conList[2]) <= 0)
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "date1Value" && instModel.date1Value >= DateTime.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }
                                break;
                            case "ss":
                                if (conList[0] == "int1Value" && instModel.int1Value < int.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "str1Value" && String.CompareOrdinal(instModel.str1Value, conList[2]) > 0)
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "date1Value" && instModel.date1Value < DateTime.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }
                                break;
                            case "se":
                                if (conList[0] == "int1Value" && instModel.int1Value <= int.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "str1Value" && String.CompareOrdinal(instModel.str1Value, conList[2]) >= 0)
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "date1Value" && instModel.date1Value <= DateTime.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }
                                break;
                            case "ee":
                                if (conList[0] == "int1Value" && instModel.int1Value == int.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "str1Value" && instModel.str1Value == conList[2])
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "date1Value" && instModel.date1Value == DateTime.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }
                                break;
                            case "nn":
                                if (conList[0] == "int1Value" && instModel.int1Value != int.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "str1Value" && instModel.str1Value != conList[2])
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }

                                if (conList[0] == "date1Value" && instModel.date1Value != DateTime.Parse(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }
                                break;
                            case "in":
                                if (conList[0] == "str1Value" && instModel.str1Value.Contains(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }
                                break;
                            case "out":
                                if (conList[0] == "str1Value" && !instModel.str1Value.Contains(conList[2]))
                                { res.Add(Convert.ToInt32(dt.Rows[i]["nodeID"])); }
                                break;
                        }
                    }
                }
                return res;
            }
        }

        public static int findNextApprover(int nodeID, int psnID)
        {
            Bizcs.Model.flow_instanceNode nodesModel = new Bizcs.BLL.flow_instanceNode().GetModel(nodeID);
            Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
            Bizcs.Model.psn_psnMain psnModel = psnBll.GetModel(psnID);

            string[] apprList = nodesModel.approve.Split('|');
            DataSet ds = new DataSet();
            int sendCount = 0;
            switch (apprList[0])
            {
                case "person":
                    if (psnBll.GetModel(int.Parse(apprList[1])) != null)
                    {
                        sendMessage(int.Parse(apprList[1]), nodesModel.instanceID, nodeID);
                        sendCount = 1;
                    }
                    break;
                case "role":
                    DataSet dsPsn = new DataSet();
                    string[] roles = apprList[1].Split(':');
                    if (roles.Length == 2)
                    {
                        ds = roles[0] == "data" ?
                           (new Bizcs.BLL.sys_dataOsrz().GetList("osrzRoleID=" + roles[1])) :
                           (new Bizcs.BLL.sys_menuOsrz().GetList("osrzRoleID=" + roles[1]));
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                switch (dt.Rows[i]["osrzObjType"].ToString())
                                {
                                    case "unit":
                                        dsPsn = psnBll.GetList("unitID=" + apprList[1]);
                                        if (dsPsn != null && dsPsn.Tables[0].Rows.Count > 0)
                                        {
                                            for (int x = 0; x < dsPsn.Tables[0].Rows.Count; x++)
                                            {
                                                sendMessage(Convert.ToInt32(dsPsn.Tables[0].Rows[x]["psnID"]), nodesModel.instanceID, nodeID);
                                            }
                                            sendCount = ds.Tables[0].Rows.Count;
                                        }
                                        break;
                                    case "dept":
                                        dsPsn = psnBll.GetList("deptID=" + apprList[1]);
                                        if (dsPsn != null && dsPsn.Tables[0].Rows.Count > 0)
                                        {
                                            for (int x = 0; x < dsPsn.Tables[0].Rows.Count; x++)
                                            {
                                                sendMessage(Convert.ToInt32(dsPsn.Tables[0].Rows[x]["psnID"]), nodesModel.instanceID, nodeID);
                                            }
                                            sendCount = ds.Tables[0].Rows.Count;
                                        }
                                        break;
                                    case "post":
                                        dsPsn = psnBll.GetList("postID=" + apprList[1]);
                                        if (dsPsn != null && dsPsn.Tables[0].Rows.Count > 0)
                                        {
                                            for (int x = 0; x < dsPsn.Tables[0].Rows.Count; x++)
                                            {
                                                sendMessage(Convert.ToInt32(dsPsn.Tables[0].Rows[x]["psnID"]), nodesModel.instanceID, nodeID);
                                            }
                                            sendCount = ds.Tables[0].Rows.Count;
                                        }
                                        break;
                                    case "psn":
                                        if (psnBll.GetModel(Convert.ToInt32(dt.Rows[i]["osrzObjID"])) != null)
                                        {
                                            sendMessage(Convert.ToInt32(dt.Rows[i]["osrzObjID"]), nodesModel.instanceID, nodeID);
                                            sendCount = 1;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    break;
                case "position":
                    ds = psnBll.GetList("postID=" + apprList[1]);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            sendMessage(Convert.ToInt32(ds.Tables[0].Rows[i]["psnID"]), nodesModel.instanceID, nodeID);
                        }
                        sendCount = ds.Tables[0].Rows.Count;
                    }
                    break;
                case "reportLine":
                    Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                    Bizcs.Model.org_orgMain orgModel = new Bizcs.Model.org_orgMain();
                    if (apprList[1] == "dept1")
                    {
                        orgModel = orgBll.GetModel(Convert.ToInt32(psnModel.deptID));
                        if (orgModel != null && orgModel.chargeUser >= 0)
                        {
                            if (psnBll.GetModel(Convert.ToInt32(orgModel.chargeUser)) != null)
                            {
                                sendMessage(Convert.ToInt32(orgModel.chargeUser), nodesModel.instanceID, nodeID);
                                sendCount = 1;
                            }
                        }
                        if (orgModel != null && orgModel.chargePost >= 0)
                        {
                            dsPsn = psnBll.GetList("postID=" + orgModel.chargePost);
                            if (dsPsn != null && dsPsn.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsPsn.Tables[0].Rows.Count; i++)
                                {
                                    sendMessage(Convert.ToInt32(dsPsn.Tables[0].Rows[i]["psnID"]), nodesModel.instanceID, nodeID);
                                }
                                sendCount = dsPsn.Tables[0].Rows.Count;
                            }
                        }
                    }
                    if (apprList[1] == "dept2")
                    {
                        //get the parent orgModel
                        orgModel = orgBll.GetModel(Convert.ToInt32(orgBll.GetModel(Convert.ToInt32(psnModel.deptID)).parentID));
                        if (orgModel != null || orgModel.orgType != "dept")
                        {
                            sendCount = -11;
                        }
                        else
                        {
                            if (orgModel != null && orgModel.chargeUser >= 0)
                            {
                                if (psnBll.GetModel(Convert.ToInt32(orgModel.chargeUser)) != null)
                                {
                                    sendMessage(Convert.ToInt32(orgModel.chargeUser), nodesModel.instanceID, nodeID);
                                    sendCount = 1;
                                }
                            }
                            if (orgModel != null && orgModel.chargePost >= 0)
                            {
                                dsPsn = psnBll.GetList("postID=" + orgModel.chargePost);
                                if (dsPsn != null && dsPsn.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dsPsn.Tables[0].Rows.Count; i++)
                                    {
                                        sendMessage(Convert.ToInt32(dsPsn.Tables[0].Rows[i]["psnID"]), nodesModel.instanceID, nodeID);
                                    }
                                    sendCount = dsPsn.Tables[0].Rows.Count;
                                }
                            }
                        }
                    }
                    if (apprList[1] == "unit1")
                    {
                        orgModel = orgBll.GetModel(Convert.ToInt32(psnModel.unitID));
                        if (orgModel != null && orgModel.chargeUser >= 0)
                        {
                            sendMessage(Convert.ToInt32(orgModel.chargeUser), nodesModel.instanceID, nodeID);
                            sendCount = 1;
                        }
                        if (orgModel != null && orgModel.chargePost >= 0)
                        {
                            dsPsn = psnBll.GetList("postID=" + orgModel.chargePost);
                            if (dsPsn != null && dsPsn.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsPsn.Tables[0].Rows.Count; i++)
                                {
                                    sendMessage(Convert.ToInt32(dsPsn.Tables[0].Rows[i]["psnID"]), nodesModel.instanceID, nodeID);
                                }
                                sendCount = dsPsn.Tables[0].Rows.Count;
                            }

                        }
                    }
                    if (apprList[1] == "unit2")
                    {
                        //get the parent orgModel
                        orgModel = orgBll.GetModel(Convert.ToInt32(orgBll.GetModel(Convert.ToInt32(psnModel.deptID)).parentID));
                        if (orgModel != null | orgModel.orgType != "unit")
                        {
                            sendCount = -12;
                        }
                        else
                        {
                            if (orgModel != null && orgModel.chargeUser >= 0)
                            {
                                if (psnBll.GetModel(Convert.ToInt32(orgModel.chargeUser)) != null)
                                {
                                    sendMessage(Convert.ToInt32(orgModel.chargeUser), nodesModel.instanceID, nodeID);
                                    sendCount = 1;
                                }
                            }
                            else
                            {
                                dsPsn = psnBll.GetList("postID=" + orgModel.chargePost);
                                if (dsPsn != null && dsPsn.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dsPsn.Tables[0].Rows.Count; i++)
                                    {
                                        sendMessage(Convert.ToInt32(dsPsn.Tables[0].Rows[i]["psnID"]), nodesModel.instanceID, nodeID);
                                    }
                                    sendCount = dsPsn.Tables[0].Rows.Count;
                                }
                            }
                        }
                    }
                    if (apprList[1] == "oneself")
                    {
                        Bizcs.Model.flow_instance instModel=new Bizcs.BLL.flow_instance().GetModel(nodesModel.instanceID);
                        if(instModel != null) 
                        { sendMessage(instModel.psnID, nodesModel.instanceID, nodeID); sendCount = 1; }
                    }
                    break;
            }
            return sendCount;

        }

        public static void sendMessage(int psnID, int instanceID, int nodeID)
        {
            Bizcs.Model.flow_instance instModel = new Bizcs.BLL.flow_instance().GetModel(instanceID);
            Bizcs.BLL.app_messages msgBll = new Bizcs.BLL.app_messages();
            Bizcs.Model.app_messages msgModel = new Bizcs.Model.app_messages();

            msgModel.appID = instModel.appID;
            msgModel.objType = "psn";
            msgModel.objID = psnID;
            msgModel.msgType = "Flow";
            msgModel.bizType = "FlowNode";
            msgModel.bizID = nodeID;
            msgModel.msgTitle = instModel.instanceName;
            msgModel.msgContent = instModel.instanceDesc;
            msgModel.msgUrl = instModel.contentUrl;
            msgModel.createTime = DateTime.Now;
            msgModel.msgStatus = 1;

            msgBll.Add(msgModel);
        }

        public static string changeSymbol(string letter)
        {
            switch (letter)
            {
                case "bb":
                    return ">";
                    break;
                case "be":
                    return ">=";
                    break;
                case "ss":
                    return "<";
                    break;
                case "se":
                    return "<=";
                    break;
                case "ee":
                    return "==";
                    break;
                case "nn":
                    return "<>";
                    break;
                case "in":
                    return "contains";
                    break;
                case "out":
                    return "excludes";
                    break;
                default:
                    return "Error";
                    break;
            }
        }

    }
}
