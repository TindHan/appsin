jQuery(function ($) {
    getItem();
    bindEvent();
})

function getItem() {
    var rid = request("i");

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query applist";
    obj.reqData = new Array("");
    var url = "/core/api/Users/getApp";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            if (result.resData.length > 0) {
                var optionHtml = '<option value="0">---Please Choose---</option>';
                for (var i = 0; i < result.resData.length; i++) {
                    optionHtml += '<option value="' + result.resData[i]["appID"] + '"> ' + result.resData[i]["appName"] + ' </option >'
                }
                $("#appID").html(optionHtml);
            }
        }
    })

}

function bindEvent() {

    $("#appID").on("change", function () {
        var aid = $("#appID").val();
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query menulist";
        var subObj = new Object();
        subObj.oid = aid;
        subObj.ty = "1";
        subObj.kw = "";
        subObj.ons = "1";
        subObj.pageIndex = 0;
        subObj.pageListNum = 0;
        obj.reqData = [subObj];
        var url = "/core/api/Users/getAppMenu";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                localStorage.setItem("uToken", result.uToken);
                var optionHtml = '<option value="0">---Please Choose---</option>';
                for (var j = 0; j < result.resData.length > 0; j++) {
                    if (result.resData[j]["menuLevel"] == 1) {
                        optionHtml += '<option value="' + result.resData[j]["menuID"] + '"> ' + result.resData[j]["menuName"] + ' </option >'
                    }
                }
                $("#moduleID").html(optionHtml);
            }
        })
    })

    $("#moduleID").on("change", function () {
        var aid = $("#moduleID").val();
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query menulist";
        var subObj = new Object();
        subObj.oid = aid;
        subObj.ty = "2";
        subObj.kw = "";
        subObj.ons = "1";
        subObj.pageIndex = 0;
        subObj.pageListNum = 0;
        obj.reqData = [subObj];
        var url = "/core/api/Users/getAppMenu";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                localStorage.setItem("uToken", result.uToken);
                var optionHtml = '<option value="0">---Please Choose---</option>';
                for (var j = 0; j < result.resData.length > 0; j++) {
                    if (result.resData[j]["parentID"] == $("#moduleID").val()) {
                        optionHtml += '<option value="' + result.resData[j]["menuID"] + '"> ' + result.resData[j]["menuName"] + ' </option >'
                    }
                }
                $("#menuID").html(optionHtml);
                $("#menuID").select2({ maximumSelectionLength: 15 });
            }
        })
    })

    $("#toSave").on("click", function () {
        if ($("#menuID").val() == "0") {
            Swal.fire("Error!", "You are not select any function!");
            return;
        }
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "save bind";
        var mList = $("#menuID").val();
        var mstr = "";
        for (var i = 0; i < mList.length; i++) {
            mstr += mList[i]+","
        }
        if (mstr == "") {
            Swal.fire("Success!", "Please choose at least one menu!"); return
        }
        var subObj = new Object();
        subObj.roleID = request("i");

        subObj.menuID = mstr;
        obj.reqData = [subObj];
        var url = "/core/api/Osrzs/funcBindAdd";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                Swal.fire("Success!", "Function already authorized to this role!").then(function () { window.parent.getRoleBind(); window.parent.closeModal(); })
            }
            else {
                Swal.fire("Warning!", result.message)
            }
        })

    })


    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}