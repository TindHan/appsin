jQuery(function ($) {
    getAppDetail();
    bindEvent();
})
function getAppDetail() {
    var aid = request("i");
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query app";
    obj.reqData = [aid];
    var url = "/core/api/Apps/getAppDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#parentID").val(result.resData[0].appID);
            $("#appName").val(result.resData[0].appName);

            if (request("t") == "u") { getMenuDetail(); }
        }
        else {
            Swal.fire("Error", "Query application failed,please retry or contact administrator!");
        }
    })
}

function getMenuDetail() {
    var mid = request("m");
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query menu";
    obj.reqData = [mid];
    var url = "/core/api/Users/getMenuDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#menuID").val(result.resData[0].menuID);
            $("#menuName").val(result.resData[0].menuName);
            $("#menuDesc").val(result.resData[0].menuDescription);
            $("#menuIcon").val(result.resData[0].menuIcon);
            $("#menuLink").val(result.resData[0].menuLink);
            $("#displayOrder").val(result.resData[0].displayOrder);
            if (result.resData[0].menuStatus == 1) { $("#menuStatus").attr("checked", "true"); }
            else { $("#menuStatus").attr("checked", "false") };
        }
        else {
            Swal.fire("Error", "Query menu failed,please retry or contact administrator!");
        }
    })
}

function bindEvent() {
    $("#toSave").on("click", function () {
        var subObj = formArrToObj($("#moduleInfo").serializeArray());
        if (subObj.menuName.trim() == "" || subObj.menuDesc.trim() == "") {
            Swal.fire("Error!", "Module name and description is required!");
        }
        else {
            if (!("menuStatus" in subObj)) {
                subObj.menuStatus = "off";
            }
            subObj.menuType = "group";
            var obj = new Object();
            obj.uToken = localStorage.getItem("uToken");
            obj.action = "query orglist";
            obj.reqData = [subObj];

            if (request("t") == "a") {
                var url = "/core/api/Apps/moduleAdd";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        window.parent.closeModal(); window.parent.getModule();
                    }
                })
            }
            else {
                var url = "/core/api/Apps/moduleUpdate";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        window.parent.closeModal(); window.parent.getModule();
                    }
                })
            }
        }
    })
    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}