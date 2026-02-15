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
        var url = "/core/api/Interface/getApiDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                $("#apiID").val(result.resData[0].apiID);
                $("#apiName").val(result.resData[0].apiName);
                $("#apiType").val(result.resData[0].apiType);
                $("#apiDesc").val(result.resData[0].apiDescription);
                $("#apiCode").val(result.resData[0].apiCode);
                $("#apiAddress").val(result.resData[0].apiAddress);
                $("#apiReqPara").val(result.resData[0].apiReqPara);
                $("#apiResPara").val(result.resData[0].apiResPara);
                $("#apiKeyNote").val(result.resData[0].apiKeyNote);
                $("#apiExample").val(result.resData[0].apiExample);
               // $("#apiStatus").val(result.resData[0].apiStatus);
                result.resData[0].apiStatus == 1 ? $("#apiStatus").attr("checked", true) : $("#apiStatus").attr("checked", false);
            }
            else {
                Swal.fire("Error", "Query interface failed,please retry or contact administrator!");
            }
        })
    }
}

function bindEvent() {

    $("#toSave").on("click", function () {
        $("#toSave").attr("disabled", true);
        var reqdata = formArrToObj($("#objInfo").serializeArray());
        if (reqdata.apiName.trim() == "" || reqdata.apiDesc.trim() == "" ) {
            Swal.fire("Error!", "Api name and description is required!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if ( reqdata.apiCode.trim() == "" || reqdata.apiAddress.trim() == "") {
            Swal.fire("Error!", "Api code and description and address is required!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (reqdata.apiReqPara.trim() == "" || reqdata.apiResPara.trim() == "") {
            Swal.fire("Error!", "Interface request para and response para is required!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (!("apiStatus" in reqdata)) {
            roleObj.infStatus = "off";
        }
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query apiAdd";
        obj.reqData = [reqdata];
        
        if (request("t") == "a" || request("t") == "a#") {
            var url = "/core/api/Interface/apiAdd";
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
            var url = "/core/api/Interface/apiUpdate";
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

    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}