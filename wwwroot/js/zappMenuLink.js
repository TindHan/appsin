jQuery(function ($) {
    getParentMenu();
    bindEvent();
})

function getParentMenu() {
    var mid = request("i");
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query menu";
    obj.reqData = [mid];
    var url = "/core/api/Users/getMenuDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#menuID").val(result.resData[0].menuID);
            $("#moduleName").val(result.resData[0].menuName);

            if (request("t") == "u") { getMenuDetail(); }
        }
        else {
            Swal.fire("Error", "Query module failed,please retry or contact administrator!");
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
            $("#menuType").val(result.resData[0].menuType);
            $("#menuDesc").val(result.resData[0].menuDescription);
            $("#menuIcon").val(result.resData[0].menuIcon);
            $("#menuLink").val(result.resData[0].menuLink);
            $("#displayOrder").val(result.resData[0].displayOrder);
            if (result.resData[0].menuStatus == 1) { $("#menuStatus").attr("checked", "true"); }
            else { $("#menuStatus").attr("checked", "false") };
            if (result.resData[0].menuAppID == 10001) {
                $("#menuLink").attr("disabled", "true");
                $("#lblLink").append("---Core apps menu link can't be altered!")
            }
        }
        else {
            Swal.fire("Error", "Query module failed,please retry or contact administrator!");
        }
    })
}

function bindEvent() {
    $("#toSave").on("click", function () {
        var subObj = formArrToObj($("#menuInfo").serializeArray());
        if (subObj.menuName.trim() == "" || subObj.menuDesc.trim() == "") {
            Swal.fire("Error!", "Menu name and description is required!");
        }
        else if (subObj.menuLink.trim() == "") {
            Swal.fire("Error!", "Menu link address is required!");
        }
        else if (subObj.menuType.trim() == "") {
            Swal.fire("Error!", "Menu Type is required!");
        }
        else {
            if (!("menuStatus" in subObj)) {
                subObj.menuStatus = "off";
            }
            var obj = new Object();
            obj.uToken = localStorage.getItem("uToken");
            obj.action = "exec menuAdd";
            obj.reqData = [subObj];

            if (request("t") == "a") {
                var url = "/core/api/Apps/menuAdd";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        window.parent.closeModal(); window.parent.getMenu();
                    }
                    else {
                        Swal.fire("Error!", result.message);
                    }
                })
            }
            else {
                var url = "/core/api/Apps/menuUpdate";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        window.parent.closeModal(); window.parent.getMenu();
                    }
                    else {
                        Swal.fire("Error!", result.message);
                    }
                })
            }
        }
    })

    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}