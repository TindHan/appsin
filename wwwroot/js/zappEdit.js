jQuery(function ($) {
    getApp();
    bindEvent();
});

function getApp() {
    if (request("t") == "u") {
        var aid = request("i");
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query app";
        obj.reqData = [aid];
        var url = "/core/api/Apps/getAppDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                $("#appID").val(result.resData[0].appID);
                $("#appName").val(result.resData[0].appName);
                $("#appType").val(result.resData[0].appType);
                $("#appSID").val(result.resData[0].appSID);
                $("#appSecret").val(result.resData[0].appSecret);
                $("#startDate").val(result.resData[0].validStartTime.substr(0,10));
                $("#endDate").val(result.resData[0].validEndTime.substr(0, 10));
                $("#appDesc").val(result.resData[0].appDescription);
                $("#Domain1").val(result.resData[0].appDomain1);
                $("#Domain2").val(result.resData[0].appDomain2);
                $("#Domain3").val(result.resData[0].appDomain3);
                $("#appStatus").val(result.resData[0].appStatus);
            }
            else {
                Swal.fire("Error", "Query application failed,please retry or contact administrator!");
            }
        })
    }
}

function bindEvent() {
    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })

    $("#genSecret").on("click", function () {
        var rdstr = genRandomStr(16);
        $("#appSecret").val(rdstr);
    })

    $("#genSID").on("click", function () {
        var rdNum = genRandomNum(8);
        $("#appSID").val(rdNum);
    })

    $("#toSave").on("click", function () {
        $("#toSave").attr("disabled", true);
        var reqdata = formArrToObj($("#appInfo").serializeArray());
        if (reqdata.appName.trim() == "" || reqdata.appSID.trim() == "" || reqdata.appSecret.trim() == "" || reqdata.appType.trim() == "") {
            Swal.fire("Error!", "Application name and type and SID and secret is required!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (reqdata.startDate.trim() == "" || reqdata.endDate.trim() == "") {
            Swal.fire("Error!", "Application start time and end time is required!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (reqdata.Domain1.trim() == "") {
            Swal.fire("Error!", "Application domain1 is required!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (!("appStatus" in reqdata)) {
            roleObj.appStatus = "off";
        }
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query orglist";
        obj.reqData = [reqdata];
        
        if (request("t") == "a" || request("t") == "a#") {
            var url = "/core/api/Apps/appAdd";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.queryList(1);
                    window.parent.closeModal();
                }
                else {
                    Swal.fire("Error!", "There is something wrong occured,please retry or contact administrator!");
                }
                $("#toSave").attr("disabled", false);
            })
        }
        else {
            var url = "/core/api/Apps/appUpdate";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.queryList(1);
                    window.parent.closeModal();
                }
                else {
                    Swal.fire("Error!", "There is something wrong occured,please retry or contact administrator!");
                }
                $("#toSave").attr("disabled", false);
            })
        }

    })
}