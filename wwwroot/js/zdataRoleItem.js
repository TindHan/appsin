jQuery(function ($) {
    loadOrgList();
    bindEvent();
})

function loadOrgList() {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array("");
    var url = "/core/api/Orgs/getOrgAll";
    httpPost(url, obj, function (result) {
        var orgList = result.resData;
        var unitDeptList = $.grep(orgList, function (item) {
            return item.type != "post";
        })

        var unitOption = "<option value='0'>---Please Choose---</option>";
        for (var i = 0; i < unitDeptList.length; i++) {
            var iconUnicode = ""
            if (unitDeptList[i]["type"] == "unit") {
                iconUnicode = "&#xf1ad;&#x00A0;";
            }
            if ((unitDeptList[i]["type"] == "dept")) {
                iconUnicode = "&#x00A0;&#x00A0;&#x00A0;&#x00A0;&#xf0e8;&#x00A0;";
            }
            unitOption += "<option value='" + unitDeptList[i]["id"] + "'>" + iconUnicode + unitDeptList[i]["name"] + "</option>";
        }
        $("#staticOrgID").html(unitOption);
        $("#roleID").val(request("i"));
    })


}


function bindEvent() {

    $("#bindType").on("change", function () {
        var t = $("#bindType").val();
        if ($("#bindType").val() == "static") {
            $("#orgList").css("display", "block");
            $("#orgType").css("display", "none");
        }
        else if ($("#bindType").val() == "dynamic") {
            $("#orgList").css("display", "none");
            $("#orgType").css("display", "block");
        }
        else {
            $("#orgList").css("display", "none");
            $("#orgType").css("display", "none");
        }
    })

    $("#toSave").on("click", function () {
        var subObj = formArrToObj($("#itemInfo").serializeArray());

        if ($("#bindType").val() == "0") {
            Swal.fire("Error!", "You are not choose any option about type!");
            return;
        }
        if ($("#bindType").val() == "static" && $("#staticOrgID").val() == "0") {
            Swal.fire("Error!", "You are not choose any option about organization!");
            return;
        }
        if ($("#bindType").val() == "dynamic" && $("#dynamicOrg").val() == "0") {
            Swal.fire("Error!", "You are not choose any option about dynamic Organization!");
            return;
        } 
        if ( $("#subOrgIn").val() == "0") {
            Swal.fire("Error!", "You are not choose  option about include or exclude sub_organization!");
            return;
        }
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "save bind";
        obj.reqData = [subObj];
        var url = "/core/api/Osrzs/dataBindAdd";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                window.parent.getRoleBind(); window.parent.closeModal();
            }

        })

    })


    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}