jQuery(function ($) {
    loadOrgList();
    bindEvent();
});

function loadOrgList() {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array("");
    var url = "/core/api/Orgs/getOrgTree";
    httpPost(url, obj, function (result) {
        var orgList = result.resData;
        var unitDeptList = $.grep(orgList, function (item) {
            return item.type != "post";
        })
        var deptList = $.grep(orgList, function (item) {
            return item.type == "dept";
        })
        var postList = $.grep(orgList, function (item) {
            return item.type == "post";
        })

        var unitOption = "<option value=''>---Please Choose---</option>";
        for (var i = 0; i < unitDeptList.length; i++) {
            unitOption += "<option value='" + unitDeptList[i]["id"] + "'>" + unitDeptList[i]["name"] + "</option>";
        }
        $("#puID").html(unitOption);

        var postOption = "<option value=''>---Please Choose---</option>";
        for (var i = 0; i < postList.length; i++) {
            postOption += "<option value='" + postList[i]["id"] + "'>" + postList[i]["pName"] + " -- " + postList[i]["name"] + "</option>";
        }
        $("#adminPost").html(postOption);

        loadPsnList();
    })


}

function loadPsnList() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query psnlist";
    obj.reqData = new Array("on");
    var url = "/core/api/Psns/getPsn";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var psnOption = "<option value=''>---Please Choose---</option>";
            for (var i = 0; i < result.resData.length; i++) {
                psnOption += "<option value='" + result.resData[i]["psnID"] + "'>" + result.resData[i]["deptName"] + " -- " + result.resData[i]["psnName"] + "</option>";
            }
            $("#adminPsn").html(psnOption);
        }
        loadOrgInfo();
    });
}

function orgTypeConverter(typeStr) {
    switch (typeStr) {
        case "unit": return "Organization"; break;
        case "dept": return "Department"; break;
        case "post": return "Position"; break;
    }
}

function loadOrgInfo() {

    var orgID = request("i");
    if (request("t")=="e") {
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query orglist";
        obj.reqData = Array.of(orgID);
        var url = "/core/api/Orgs/getOrgDetail";
        httpPost(url, obj, function (result) {
            if (result.resData[0].orgID == 10000) {
                $("#parentList").addClass("div-disabled");
                $("#typeList").addClass("div-disabled");
            }
            else {
                $("#parentList").removeClass("div-disabled");
                $("#typeList").removeClass("div-disabled");
            }
            if (result.resData[0].orgType == "post") {
                $("#chargePsn").addClass("div-disabled");
                $("#chargePst").addClass("div-disabled");
            }
            else {
                $("#chargePsn").removeClass("div-disabled");
                $("#chargePst").removeClass("div-disabled");
            }

            $("#uID").val(result.resData[0].orgID)
            $("#uName").val(result.resData[0].orgName);
            $("#uType").val(result.resData[0].orgType);
            $("#uCode").val(result.resData[0].orgCode);
            $("#startDate").val(result.resData[0].validStartDate.substr(0, 10));
            $("#endDate").val(result.resData[0].validEndDate.substr(0, 10));

            $("#puID").val(result.resData[0].parentID);
            $("#adminPost").val(result.resData[0].chargePost);
            $("#adminPsn").val(result.resData[0].chargeUser);

            $("#puID").select2();
            $("#adminPost").select2();
            $("#adminPsn").select2();
        });
    }
    else {
        $("#puID").val(orgID);
        $("#puID").select2();
        $("#adminPost").select2();
        $("#adminPsn").select2();
    }
}
function bindEvent() {

    $("#uType").on("change", function () {
        var uTypeValue = this.value;
        if (uTypeValue == "post") {
            $("#chargePsn").addClass("div-disabled");
            $("#chargePst").addClass("div-disabled");
        }
        else {
            $("#chargePsn").removeClass("div-disabled");
            $("#chargePst").removeClass("div-disabled");
        }
    })

    $("#toSave").on("click", function () {
        $("#toSave").attr("disabled", true);
        var orgInfo = formArrToObj($("#unitInfo").serializeArray());

        if ((orgInfo.puID == "0" || orgInfo.puID == "") && orgInfo.uID!="10000") {
            Swal.fire({
                title: "PROPMT",
                text:  "Parent organization  must be inputed！",
                icon: "error",
                confirmButtonColor: "#5156be"
            });
            $("#toSave").attr("disabled", false);
        }
        else if (orgInfo.uName == "" || orgInfo.uCode == "" || orgInfo.uName == "0" || orgInfo.uCode == "0") {
            Swal.fire({
                title:  "PROPMT",
                text:  "Organization name and code must be inputed！",
                icon: "error",
                confirmButtonColor: "#5156be"
            });
            $("#toSave").attr("disabled", false);
        }
        else if (orgInfo.startDate == "" || orgInfo.endDate == "") {
            Swal.fire({
                title:  "PROPMT",
                text:  "Organization startDate and endDate must be inputed！",
                icon: "error",
                confirmButtonColor: "#5156be"
            });
            $("#toSave").attr("disabled", false);
        }
        else if (orgInfo.startDate != "" && orgInfo.endDate != "" && orgInfo.startDate >= orgInfo.endDate) {
            Swal.fire({
                title:  "PROPMT",
                text:  "Organization startDate must be before endDate！",
                icon: "error",
                confirmButtonColor: "#5156be"
            });
            $("#toSave").attr("disabled", false);
        }
        else {
            //add
            if (request("t") == "a") {
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "request orgAdd";
                obj.reqData = [orgInfo];
                var url = "/core/api/Orgs/orgAdd";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        window.parent.bindOrg();
                        window.parent.closeModal();
                    }
                    else if (result.status==110) {
                        Swal.fire("Error!", "The text you input including high risk characters,please remove the high risk character!", "error");
                    }
                    else {
                        Swal.fire("Error","Add Organization failed,please retry or contact administrator！", "error");
                    }
                    $("#toSave").attr("disabled", false);
                })
            }
            //update
            else {
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "request orgUpdate";
                obj.reqData = [orgInfo];
                var url = "/core/api/Orgs/orgUpdate";
                httpPost(url, obj, function (result) {
                    console.log(result);
                    if (result.status == 1) {
                        window.parent.bindOrg();
                        window.parent.closeModal();
                    }
                    else if (result.status == 110) {
                        Swal.fire("Error!", "The text you input including high risk characters,please remove the high risk character!", "error");
                    }
                    else {
                        Swal.fire("Error", "Add Organization failed,please retry or contact administrator！", "error");
                    }
                    $("#toSave").attr("disabled", false);
                })
            }
        }
    })

    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}

