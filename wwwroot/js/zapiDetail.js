jQuery(function ($) {
    getApp();
    bindEvent();
});

function getApp() {
    var aid = request("i");
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query app";
    obj.reqData = [aid];
    var url = "/core/api/Interface/getapiDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#apiName").html(result.resData[0].apiName);
            $("#apiType").html(result.resData[0].apiType);
            $("#apiDesc").html(result.resData[0].apiDescription);
            $("#apiCode").html(result.resData[0].apiCode);
            $("#apiAddress").html(result.resData[0].apiAddress);
            $("#apiReqPara").html(result.resData[0].apiReqPara);
            $("#apiResPara").html(result.resData[0].apiResPara);
            $("#apiKeyNote").html(result.resData[0].apiKeyNote);
            $("#apiExample").html(result.resData[0].apiExample);
            $("#apiStatus").html(result.resData[0].apiStatus);
        }
        else {
            Swal.fire("Error", "Query interface failed,please retry or contact administrator!");
        }
    })

}

function bindEvent() {
    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}