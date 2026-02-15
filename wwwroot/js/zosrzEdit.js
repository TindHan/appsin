jQuery(function ($) {
    bindEvent();
    getRole();
})

function getRole() {
    $("#objType").select2();
    if (request("t") == "d") {
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query getField";

        var subData = new Object();
        subData.oid = "";
        subData.kw = "";
        subData.ons = "1";
        subData.ty = "";
        subData.pageIndex = 1;
        subData.pageListNum = 99999;

        obj.reqData = [subData];
        var url = "/core/api/Osrzs/getDataRoleList";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                var optionHtml = "<option value='0'>---Please Choose--</option>";
                for (var i = 0; i < result.resData.length; i++) {
                    optionHtml += "<option value='" + result.resData[i]["roleID"] + "'>" + result.resData[i]["roleName"] + "</option>";
                }
                $("#roleID").html(optionHtml);
                $("#roleID").select2();
            }
        })
    }
    else {
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query getField";

        var subData = new Object();
        subData.oid = "";
        subData.kw = "";
        subData.ons = "1";
        subData.ty = "";
        subData.pageIndex = 1;
        subData.pageListNum = 99999;

        obj.reqData = [subData];
        var url = "/core/api/Osrzs/getFuncRoleList";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                var optionHtml = "<option value='0'>---Please Choose--</option>";
                for (var i = 0; i < result.resData.length; i++) {
                    optionHtml += "<option value='" + result.resData[i]["roleID"] + "'>" + result.resData[i]["roleName"] + "--" + result.resData[i]["roleMemo"] + "</option>";
                }
                $("#roleID").html(optionHtml);
                $("#roleID").select2();
            }
        })
    }
}

function getObjList(type) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query objList";
    obj.reqData = [""];
    var url = "/core/api/Osrzs/getObjList";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var orgList = result.resData;
            var objList;
            switch (type) {
                case "unit":
                    objList = $.grep(orgList, function (item) { return item.objDesc == "unit"; })
                    break;
                case "dept":
                    objList = $.grep(orgList, function (item) { return item.objDesc == "dept"; })
                    break;
                case "post":
                    objList = $.grep(orgList, function (item) { return item.objDesc == "post"; })
                    break;
                case "psn":
                    objList = $.grep(orgList, function (item) { return item.objDesc == "psn"; })
                    break;
            }
        }
        console.log(objList);
        var optionHtml = "<option value='0'>---Please Choose--</option>";
        for (var i = 0; i < objList.length; i++) {
            optionHtml += "<option value='" +  objList[i]["objID"] + "'>" + objList[i]["objName"] + (objList[i]["objParent"] == null ? "" : "---" + (objList[i]["objParent"])) + "</option>"
        }
        $("#objID").html(optionHtml);
        $("#objID").select2();

    })
}

function bindEvent() {

    $("#objType").on("change", function () {

        var objType = $("#objType").val();
        getObjList(objType);
    })

    $("#toSave").on("click", function () {
        $("#toSave").attr("disabled", true);
        var itemInfo = formArrToObj($("#itemInfo").serializeArray());
        if (itemInfo.roleID == 0) { Swal.fire("Error!", "Role must be choosed!"); $("#toSave").attr("disabled", false); return; }
        if (itemInfo.objType == 0) { Swal.fire("Error!", "Object type must be choosed!"); $("#toSave").attr("disabled", false); return; }
        if (itemInfo.objID == 0) { Swal.fire("Error!", "Authorize object must choosed!"); $("#toSave").attr("disabled", false); return; }
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query objList";
        obj.reqData = [itemInfo];
        var url = request("t") == "d"?"/core/api/Osrzs/addDataOsrz": "/core/api/Osrzs/addFuncOsrz";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                window.parent.getBind(1);
                window.parent.closeModal();
            }
            else {
                Swal.fire("Error!", result.message);
            }
            $("#toSave").attr("disabled", false);
        })
    })

    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}