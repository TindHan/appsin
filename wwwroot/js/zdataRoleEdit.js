jQuery(function ($) {
    getRoleInfo();
    bindEvent();
})

function getRoleInfo() {
    if (request("t") == "u") {
        var rid = request("i");
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query orglist";
        obj.reqData = [rid];
        var url = "/core/api/Osrzs/getDataRoleDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                $("#roleID").val(result.resData[0]["roleID"]);
                $("#roleName").val(result.resData[0]["roleName"]);
                $("#roleMemo").val(result.resData[0]["roleMemo"]);
                if (result.resData[0]["roleStatus"] == 1) { $("#roleStatus").attr("checked", true) }
                else { $("#roleStatus").attr("checked", false) }
            }
        })
    }
}


function bindEvent() {

    $("#toSave").on("click", function () {
        $("#toSave").attr("disabled", false);
        var roleObj = formArrToObj($("#roleInfo").serializeArray());
        if (roleObj.roleName.trim() == "" || roleObj.roleMemo.trim() == "") {
            Swal.fire("Error!", "Role name and role memo must be inputed!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (roleObj.roleName.includes('|') || roleObj.roleMemo.includes(':')) {
            Swal.fire("Error!", "Role name cannot contains '|' or ':'!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (!("roleStatus" in roleObj)) {
            roleObj.roleStatus = "off";
        }
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query orglist";
        obj.reqData = [roleObj];

        if (request("t") == "a") {
            var url = "/core/api/Osrzs/dataRoleAdd";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.closeModal(); window.parent.getRoleList(1);
                }
                else {
                    Swal.fire("Error!", "There is something wrong occured,please retry or contact administrator!");
                }
                $("#toSave").attr("disabled", false);
            })
        }
        else {
            var url = "/core/api/Osrzs/dataRoleEdit";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.closeModal(); window.parent.getRoleList(1);
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