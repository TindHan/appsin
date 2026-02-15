jQuery(function ($) {
    bindOrg();
    bindEvent();
});

function bindOrg() {
    var zTree;
    var setting = {
        view: {
            dblClickExpand: false,
            showLine: true,
            selectedMulti: false,
            showIcon: true
        },
        data: {
            simpleData: {
                enable: true,
                idKey: "id",
                pIdKey: "pId",
                rootPId: ""
            }
        },
        callback: {
            onClick: function (event, treeId, treeNode) {
                getOrgInfo();
            }
        }
    };

    var isChecked = "";
    if ($("#chkCancel").is(':checked')) { isChecked = "all"; }

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array(isChecked);
    var url = "/core/api/Orgs/getOrgTree";
    httpPost(url, obj, function (result) {
        var orgList = result.resData;
        var t = $("#orgTree");
        t = $.fn.zTree.init(t, setting, orgList);

        //left org tree
        var zTree = $.fn.zTree.getZTreeObj("orgTree");
        zTree.selectNode(zTree.getNodeByParam("id", 10000));

        getOrgInfo();
    });
}

function getOrgInfo() {
    var zTree = $.fn.zTree.getZTreeObj("orgTree");
    var sOrgList = zTree.getSelectedNodes()
    var sOrgID = sOrgList[0].id;

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = Array.of(sOrgID);
    var url = "/core/api/Orgs/getOrgDetail";
    httpPost(url, obj, function (result) {
        $("#orgNameShow").html(result.resData[0].orgName);
        $("#orgTypeShow").html(orgTypeConverter(result.resData[0].orgType));
        $("#orgCodeShow").html(result.resData[0].orgCode);
        $("#parentOrgShow").html(result.resData[0].orgMemo5);
        $("#startTimeShow").html(result.resData[0].validStartDate.substr(0, 10));
        $("#endTimeShow").html(result.resData[0].validEndDate.substr(0, 10));
        $("#adminPostShow").html(result.resData[0].orgMemo4);
        $("#adminPsnShow").html(result.resData[0].orgMemo3);
    });
}


function orgTypeConverter(typeStr) {
    switch (typeStr) {
        case "unit": return "Organization"; break;
        case "dept": return "Department"; break;
        case "post": return "Position"; break;
    }
}

function bindEvent() {

    $("#chkCancel").on("click", function () {
        bindOrg();
    })
    $("#toEdit").on("click", function () {
        var zTree = $.fn.zTree.getZTreeObj("orgTree");
        var sOrgList = zTree.getSelectedNodes()
        var sOrgID = sOrgList[0].id;
        $("#orgEditFrame").attr("src", "orgEdit.html?t=e&i=" + sOrgID).attr("width", "950px").attr("height", "430px");
        $("#mtt").html("Edit Organization");
        $("#orgEdit").modal("show");

    })

    $("#toAdd").on("click", function () {
        var zTree = $.fn.zTree.getZTreeObj("orgTree");
        var sOrgList = zTree.getSelectedNodes()
        var sOrgID = sOrgList[0].id;
        var sOrgType = sOrgList[0].type;
        if (sOrgType.trim() == "post") {
            Swal.fire("Warning!", "Postion can't be upper organization,please choose unit or dept on organization chart", "warning");
        }
        else {
            $("#orgEditFrame").attr("src", "orgEdit.html?t=a&i=" + sOrgID).attr("width", "950px").attr("height", "430px");
            $("#mtt").html("New Organization");
            $("#orgEdit").modal("show");
        }

    })

    $("#toCancel").on("click", function () {
        Swal.fire({
            title: "Are you sure?",
            text: "Cancel organization will cancel all sub-organization at the same time, you won't be able to revert this,but you can search it!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, cancel it!"
        }).then(function (e) {
            if (e.isConfirmed == true) {
                var zTree = $.fn.zTree.getZTreeObj("orgTree");
                var sOrgList = zTree.getSelectedNodes()
                var sOrgID = sOrgList[0].id;
                if (sOrgID != "" || sOrgID != 0) {
                    var obj = new Object();
                    obj.uToken = localStorage.getItem("uToken");
                    obj.action = "request orgCancel";
                    obj.reqData = [sOrgID.toString()];
                    var url = "/core/api/Orgs/orgCancel";
                    httpPost(url, obj, function (result) {
                        if (result.status == 1) {
                            Swal.fire("Canceled!", "Organization has been canceled.", "success");
                            localStorage.setItem("uToken", result.uToken);
                            bindOrg();
                        }
                        else if (result.status == -2) {
                            Swal.fire("Failed!", "Organization has sub-org,can't be canceled,but you can cancel this organization", "warning");
                            localStorage.setItem("uToken", result.uToken);
                        }
                        else if (result.status == -3) {
                            Swal.fire("Failed!", "There are still some people in this organization,can't be canceled,but you can cancel this organization", "warning");
                            localStorage.setItem("uToken", result.uToken);
                        }
                        else if (result.status == -1) {
                            Swal.fire("Log Out!", "You haven't clicked on the system for a while,you need to reLogin now.", "warning");
                        }
                        else { }
                    })
                }
            }
        })
    })

    $("#toDel").on("click", function () {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, delete it!"
        }).then(function (e) {
            if (e.isConfirmed == true) {
                var zTree = $.fn.zTree.getZTreeObj("orgTree");
                var sOrgList = zTree.getSelectedNodes()
                var sOrgID = sOrgList[0].id;
                if (sOrgID != "" || sOrgID != 0) {
                    var obj = new Object();
                    obj.uToken = localStorage.getItem("uToken");
                    obj.action = "request orgDel";
                    obj.reqData = [sOrgID.toString()];
                    var url = "/core/api/Orgs/orgDel";
                    httpPost(url, obj, function (result) {
                        if (result.status == 1) {
                            Swal.fire("Deleted!", "Organization has been deleted.", "success");
                            localStorage.setItem("uToken", result.uToken);
                            bindOrg();
                        }
                        else if (result.status == -2) {
                            Swal.fire("Failed!", "Organization has sub-org,can't be deleted,but you can cancel this organization", "warning");
                            localStorage.setItem("uToken", result.uToken);
                        }
                        else if (result.status == -3) {
                            Swal.fire("Failed!", "There is somebody in this organization,can't be deleted,but you can cancel this organization", "warning");
                            localStorage.setItem("uToken", result.uToken);
                        }
                        else if (result.status == -1) {
                            Swal.fire("Log Out!", "You have been no click for a while,you need reLogin now.", "warning");
                        }
                        else { }
                    })
                }
            }
        })
    })

}

function closeModal() {
    $("#orgEdit").modal("hide");
}