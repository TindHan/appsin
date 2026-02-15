jQuery(function ($) {
    getPsnInfo();
    getOrgList();
    getOnType();
    getOnStatus();
    bindEvent();
})

function getPsnInfo() {
    var pid = request("i");

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array(pid.toString());
    var url = "/core/api/Psns/getPsnDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            $("#uName").html(result.resData[0].psnName);
            $("#uAlian").html(result.resData[0].aliaName);
            $("#uUnit").html(result.resData[0].unitName);
            $("#uDept").html(result.resData[0].deptName);
            $("#uPost").html(result.resData[0].postName);
            $("#uCode").html(result.resData[0].psnCode);
            $("#uCert").html(result.resData[0].idTypeName);
            $("#uID").html(result.resData[0].idNo);
            $("#uSex").html(result.resData[0].psnSex);
            $("#uOnType").html(result.resData[0].onTypeName);
            $("#uOnStatus").html(result.resData[0].onStatusName);
            if (result.resData[0].psnPicture != "") { $("#imgAvatar").attr("src", result.resData[0].psnPicture) };
            $("#psnID").val(result.resData[0].psnID);
        }
    });
}

function getOnType() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array("type");
    var url = "/core/api/Psns/getField";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var itemList = result.resData;
            var itemHtml = "<option value='0'>---Please choose---</option>";
            for (var i = 0; i < itemList.length; i++) {
                itemHtml += "<option value='" + itemList[i]["itemID"] + "'>" + itemList[i]["itemName"] + "</option>";
            }
            $("#onType").html(itemHtml);
        }
    })
}

function getOnStatus() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array("status");
    var url = "/core/api/Psns/getField";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var itemList = result.resData;
            var itemHtml = "<option value='0'>---Please choose---</option>";
            for (var i = 0; i < itemList.length; i++) {
                itemHtml += "<option value='" + itemList[i]["itemID"] + "'>" + itemList[i]["itemName"] + "</option>";
            }
            $("#onStatus").html(itemHtml);
        }
    })
}

function getOrgList() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    var reqData = new Object();
    reqData.id = "10001";// $("#unitID").val();
    reqData.type = "unit";
    reqData.order = "";
    obj.reqData = [reqData];
    var url = "/core/api/Orgs/getOrgList";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            var unitList = result.resData;
            var orgHtml = "<option value='0'>---Please choose---</option>";
            for (var i = 0; i < unitList.length; i++) {
                orgHtml += "<option value='" + unitList[i]["orgID"] + "'>" + unitList[i]["orgName"] + "</option>";
            }
            $("#unitID").html(orgHtml)
        }
    })
}

function bindEvent() {
    $("#unitID").on("change", function () {
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query orglist";
        var reqData = new Object();
        reqData.id = $("#unitID").val();
        reqData.type = "dept";
        reqData.order = "";
        obj.reqData = [reqData];
        var url = "/core/api/Orgs/getOrgList";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                localStorage.setItem("uToken", result.uToken);
                var deptList = result.resData;
                var deptHtml = "<option value='0'>---Please choose---</option>";
                if (deptList != null) {
                    for (var i = 0; i < deptList.length; i++) {
                        deptHtml += "<option value='" + deptList[i]["orgID"] + "'>" + deptList[i]["orgName"] + "</option>";
                    }
                }

                $("#deptID").html(deptHtml)

                var postHtml = "<option value='0'>---Please choose department first---</option>";
                $("#postID").html(postHtml)
            }
        })

    });


    $("#deptID").on("change", function () {
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query orglist";
        var reqData = new Object();
        reqData.id = $("#deptID").val();
        reqData.type = "post";
        reqData.order = "";
        obj.reqData = [reqData];
        var url = "/core/api/Orgs/getOrgList";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                localStorage.setItem("uToken", result.uToken);
                var postList = result.resData;
                var postHtml = "<option value='0'>---Please choose---</option>";
                for (var i = 0; i < postList.length; i++) {
                    postHtml += "<option value='" + postList[i]["orgID"] + "'>" + postList[i]["orgName"] + "</option>";
                }
                $("#postID").html(postHtml)
            }
        })

    })
    $("#transType").on("change", function () {
        var tt = this.value;
        switch (tt) {
            case "1":
                $("tr[name='orgArea']").removeClass("element-hide");
                $("tr[name='staffArea']").addClass("element-hide");
                $("tr[name='onjobArea']").addClass("element-hide");
                break;
            case "2":
                $("tr[name='orgArea']").addClass("element-hide");
                $("tr[name='staffArea']").removeClass("element-hide");
                $("tr[name='onjobArea']").addClass("element-hide");
                break;
            case "3":
                $("tr[name='orgArea']").addClass("element-hide");
                $("tr[name='staffArea']").addClass("element-hide");
                $("tr[name='onjobArea']").removeClass("element-hide");
                break;
            default:
                $("tr[name='orgArea']").addClass("element-hide");
                $("tr[name='staffArea']").addClass("element-hide");
                $("tr[name='onjobArea']").addClass("element-hide");

        }
    })

    $("#toSave").on("click", function () {
        var transInfo = formArrToObj($("#transferForm").serializeArray());
        console.log(transInfo);
        if (transInfo.transDate == "") { Swal.fire("Error!", "Transfer date must be input,please check again!"); return; }
        if (transInfo.transType == "1" && (transInfo.unitID == "0" || transInfo.deptID == "0")) { Swal.fire("Error!", "Organiztion and department must be input,please check again!"); return; }
        if (transInfo.transType == "2" && transInfo.onType == "0") { Swal.fire("Error!", "Staff type must be input,please check again!"); return; }
        if (transInfo.transType == "3" && transInfo.onStatus == "0") { Swal.fire("Error!", "Onjob status must be input,please check again!"); return; }
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query orglist";
        obj.reqData = [transInfo];
        var url = "/core/api/Psns/transEdit";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                window.parent.getPsnList();
                Swal.fire("Success!", "Tranfer is already done!", "success").then(function () { window.parent.closeModal() });
                
            }
        })
    })

    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}