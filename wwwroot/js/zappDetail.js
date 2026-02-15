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
    var url = "/core/api/Apps/getAppDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#appPK").html(result.resData[0].appPK);
            $("#appName").html(result.resData[0].appName);
            $("#appSID").html(result.resData[0].appSID);
            $("#appSecret").html(result.resData[0].appSecret);
            $("#appSkey").html(result.resData[0].appSkey);
            $("#Domain1").html(result.resData[0].appDomain1);
            $("#Domain2").html(result.resData[0].appDomain2);
            $("#Domain3").html(result.resData[0].appDomain3);
        }
        else {
            Swal.fire("Error", "Query application failed,please retry or contact administrator!");
        }
    })

}

function bindEvent() {
    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}