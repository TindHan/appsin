

jQuery(function ($) {
    bindOrg();
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
                getPsnList(1);
            }
        }
    };

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array("");
    var url = "/core/api/Orgs/getOrgTree";
    httpPost(url, obj, function (result) {
        var orgList = result.resData;
        var t = $("#orgTree");
        t = $.fn.zTree.init(t, setting, orgList);

        //left org tree
        var zTree = $.fn.zTree.getZTreeObj("orgTree");
        zTree.selectNode(zTree.getNodeByParam("id", 10000));
        bindType();
    });
}
function bindType() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getField";
    obj.reqData = new Array("type");
    var url = "/core/api/Psns/getField";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var optionHtml = "<option value='all'>All</option>";
            for (var i = 0; i < result.resData.length; i++) {
                optionHtml += "<option value='" + result.resData[i]["itemID"] + "'>" + result.resData[i]["itemName"] + "</option>";
            }
            $("#chkType").html(optionHtml);

            bindStatus();
        }
    })
}
function bindStatus() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getField";
    obj.reqData = new Array("status");
    var url = "/core/api/Psns/getField";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var optionHtml = "";
            for (var i = 0; i < result.resData.length; i++) {
                optionHtml += "<option value='" + result.resData[i]["itemID"] + "'>" + result.resData[i]["itemName"] + "</option>";
            }
            $("#chkStatus").html(optionHtml);

            getPsnList(1);
        }
    })
}



function queryList(pageIndex) {
    getPsnList(pageIndex);
}
function getPsnList(pageIndex) {
    var pageListNum = 20;

    var zTree = $.fn.zTree.getZTreeObj("orgTree");
    var sOrgList = zTree.getSelectedNodes()

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getField";

    var subData = new Object();
    subData.oid = sOrgList[0].id.toString();
    subData.kw = $("#txtKw").val();
    subData.ons = $("#chkStatus").val();
    subData.ty = $("#chkType").val();
    subData.pageIndex = pageIndex;
    subData.pageListNum = pageListNum;
    obj.reqData = [subData];
    var url = "/core/api/Psns/getPsnList";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            if (result.resData != null) {
                var No = 1;
                var tableHtml = "";
                tableHtml += ' <table id = "datatable" class="table table-bordered dt-responsive table-striped w-100" >';
                tableHtml += '    <thead class="table-secondary table-header-fixed">';
                tableHtml += '        <tr>';
                tableHtml += '            <th>No.</th>';
                tableHtml += '            <th>Organization</th>';
                tableHtml += '            <th>Department</th>';
                tableHtml += '            <th>Postion</th>';
                tableHtml += '            <th class="width-120">Name</th>';
                tableHtml += '            <th>Sex</th>';
                tableHtml += '            <th>Code</th>';
                tableHtml += '            <th>ID</th>';
                tableHtml += '            <th class="width-120">Operation</th>';
                tableHtml += '        </tr>';
                tableHtml += '    </thead>';
                tableHtml += '    <tbody>';
                for (var i = 0; i < result.resData.length; i++) {
                    tableHtml += '<tr>';
                    tableHtml += '    <td>' + No + '</td>';
                    tableHtml += '    <td>' + result.resData[i]["unitName"] + '</td>';
                    tableHtml += '    <td>' + result.resData[i]["deptName"] + '</td>';
                    tableHtml += '    <td>' + (result.resData[i]["postName"] == null ? "" : result.resData[i]["postName"]) + '</td>';
                    tableHtml += '    <td><a href="#" name="toDetail" data-id="' + result.resData[i]["psnID"] + '">' + result.resData[i]["psnName"] + '</td>';
                    tableHtml += '    <td>' + result.resData[i]["psnSex"] + '</td>';
                    tableHtml += '    <td>' + result.resData[i]["psnCode"] + '</td>';
                    tableHtml += '    <td>' + result.resData[i]["idNo"] + '</td>';
                    tableHtml += '    <td>';
                    tableHtml += '        <div class="flex-row-between">';
                    tableHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toTransfer" data-id="' + result.resData[i]["psnID"] + '" title="transfer"><i class=" fas fa-arrows-alt"></i></button>';
                    tableHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toEdit" data-id="' + result.resData[i]["psnID"] + '" title="edit"><i class="fas fa-pencil-alt"></i></button>';
                    tableHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toPwd" data-id="' + result.resData[i]["psnID"] + '" title="password"><i class="fas fa-key"></i></button>';
                    tableHtml += '            <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toDel" data-id="' + result.resData[i]["psnID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button>';
                    tableHtml += '        </div>';
                    tableHtml += '    </td>';
                    tableHtml += '</tr>';
                    No++;
                }
                tableHtml += '     </tbody>';
                tableHtml += '</table>';
                $("#tblPsn").html(tableHtml);
            }
            else {
                var tableHtml = "";
                tableHtml += ' <table id = "datatable" class="table table-bordered dt-responsive table-striped w-100" >';
                tableHtml += '    <thead class="table-secondary table-header-fixed">';
                tableHtml += '        <tr>';
                tableHtml += '            <th>No.</th>';
                tableHtml += '            <th>Organization</th>';
                tableHtml += '            <th>Department</th>';
                tableHtml += '            <th>Postion</th>';
                tableHtml += '            <th>Name</th>';
                tableHtml += '            <th>Sex</th>';
                tableHtml += '            <th>Code</th>';
                tableHtml += '            <th>ID</th>';
                tableHtml += '            <th class="width-120">Operation</th>';
                tableHtml += '        </tr>';
                tableHtml += '    </thead>';
                tableHtml += '    <tbody>';
                tableHtml += '<tr>';
                tableHtml += '    <td class="text-center" colspan="9">No data!</td>';
                tableHtml += '</tr>';
                tableHtml += '     </tbody>';
                tableHtml += '</table>';
                $("#tblPsn").html(tableHtml);
            }

            var paginationHtml = getPageNumHtml(result.number, pageListNum, pageIndex);
            $("#paginationArea").html(paginationHtml);


            bindEvent();
        }
    })
}


function bindEvent() {
    $("#btnNew").on("click", function () {
        $("#showModalFrame").attr("src", "psnEdit.html?t=a").attr("width", "950px").attr("height", "410px");
        $("#mtt").html("New Person");
        $("#showModal").modal("show");
    })
    $("#btnSearch").on("click", function () {
        getPsnList(1);
    })

    //$("#chkStatus").on("click", function () {
    //    getPsnList();
    //})

    //$("#chkType").on("click", function () {
    //    getPsnList();
    //})

    $("#btnBatch").on("click", function () {
        $("#showModalFrame").attr("src", "psnImport.html?t=d").attr("width", "950px").attr("height", "320px");
        $("#mtt").html("Batch Import Person");
        $("#showModal").modal("show");
    })

    $('button[name="toTransfer"]').on('click', function (e) {
        var pid = $(this).attr("data-id");
        $("#showModalFrame").attr("src", "psnTransfer.html?i=" + pid).attr("width", "950px").attr("height", "500px");
        $("#mtt").html("Transfer Person");
        $("#showModal").modal("show");
    });

    $('button[name="toEdit"]').on('click', function (e) {
        var pid = $(this).attr("data-id");
        $("#showModalFrame").attr("src", "psnEdit.html?t=e&i=" + pid).attr("width", "950px").attr("height", "410px");
        $("#mtt").html("Edit Person");
        $("#showModal").modal("show");
    });

    $('button[name="toPwd"]').on('click', function (e) {
        var pid = $(this).attr("data-id");
        $("#showModalFrame").attr("src", "psnPwd.html?t=e&i=" + pid).attr("width", "950px").attr("height", "260px");
        $("#mtt").html("Change Password");
        $("#showModal").modal("show");
    });

    $('a[name="toDetail"]').on('click', function (e) {
        var pid = $(this).attr("data-id");
        $("#showModalFrame").attr("src", "psnDetail.html?pid=" + pid).attr("width", "950px").attr("height", "500px");
        $("#mtt").html("Person Information");
        $("#showModal").modal("show");
    });


    $('button[name="toDel"]').on('click', function (e) {
        var pid = $(this).attr("data-id");
        Swal.fire({
            title: "Are you sure?",
            text: "You are going to delete this item,you won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, delete it!"
        }).then(function (e) {
            if (e.isConfirmed == true) {

                console.log(pid);
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "query psnDel";
                obj.reqData = new Array(pid.toString());
                var url = "/core/api/Psns/psnDel";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        getPsnList();
                        Swal.fire("Success!", "Your opration is already done!");
                    }
                    else {
                        Swal.fire("Failed", result.message, "error");
                    }
                });
            }
        })
    });
}

function closeModal() {
    $("#showModal").modal("hide");
}