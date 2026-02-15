jQuery(function ($) {
    getApp();
    bindEvent();
});

function getOsrz() {
    if (request("t") == "u") {
        var oid = request("i");
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query app";
        obj.reqData = [oid];
        var url = "/core/api/Interface/osrzDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                $("#appID").val(result.resData[0].appID);
                $("#apiID").val(result.resData[0].apiID);
                $("#osrzDesc").val(result.resData[0].osrzDescription);
                $("#validStartTime").val(result.resData[0].validStartTime.substr(0, 10));
                $("#validEndTime").val(result.resData[0].validEndTime.substr(0, 10));
                $("#displayOrder").val(result.resData[0].displayOrder);
                result.resData[0].osrzStatus == 1 ? $("#osrzStatus").attr("checked", true) : $("#osrzStatus").attr("checked", false);
                $("#osrzID").val(result.resData[0].osrzID);
                $("#appID").attr("readonly", "true");
                $("#apiID").attr("readonly", "true");
            }

        })
    }
}

function getApp() {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query app";
    obj.reqData = [""];
    var url = "/core/api/Apps/getAllApp";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var optionHtml = "<option value='0'>Please Choose</option>";
            for (var i = 0; i < result.resData.length; i++) {
                optionHtml += "<option value='" + result.resData[i].appID + "'>" + result.resData[i].appName + "</option>";
            }
            $("#appID").html(optionHtml);
            getApi();
        }
        else {
            Swal.fire("Error", "Query application list failed,please retry or contact administrator!");
        }
    })
}

function getApi() {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getApi";
    obj.reqData = [""];
    var url = "/core/api/Interface/getAllApi";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var optionHtml = "<option value='0'>Please Choose</option>";
            for (var i = 0; i < result.resData.length; i++) {
                optionHtml += "<option value='" + result.resData[i].apiID + "'>" + result.resData[i].apiName + "</option>";
            }
            $("#apiID").html(optionHtml);
            getOsrz();
        }
        else {
            Swal.fire("Error", "Query application list failed,please retry or contact administrator!");
        }
    })
}

function bindEvent() {

    $("#toSave").on("click", function () {
        var reqdata = formArrToObj($("#objInfo").serializeArray());
        if (reqdata.appID.trim() == "0" || reqdata.apiID.trim() == "0") {
            Swal.fire("Error!", "Application and Api is required!");
            return;
        }
        if (reqdata.validStartTime.trim() == "" || reqdata.validEndTime.trim() == "") {
            Swal.fire("Error!", "Valid time and Invalid time is required!");
            return;
        }
        if (!("osrzStatus" in reqdata)) {
            reqdata.osrzStatus = "off";
        }
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query osrzAdd";
        obj.reqData = [reqdata];

        if (request("t") == "a") {
            var url = "/core/api/Interface/osrzAdd";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.queryList(1);
                    window.parent.closeModal();
                }
                else {
                    Swal.fire("Error!", "There is something wrong occured,please retry or contact administrator!");
                }
            })
        }
        else {
            var url = "/core/api/Interface/osrzUpdate";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.queryList(1);
                    window.parent.closeModal();
                }
                else {
                    Swal.fire("Error!", "There is something wrong occured,please retry or contact administrator!");
                }
            })
        }
    })

    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}