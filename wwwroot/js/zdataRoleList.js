jQuery(function ($) {
    getRoleList(1);
    $("#btnNew").on("click", function () {
        $("#showModalFrame").attr("src", "dataRoleEdit.html?t=a").attr("width", "950px").attr("height", "260px");
        $("#mtt").html("New Data Role");
        $("#showModal").modal("show");
    })
})

function queryList(pageIndex) {
    getRoleList(pageIndex);
}
function getRoleList(pageIndex) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getField";

    var subData = new Object();
    subData.oid = "";
    subData.kw = $("#kw").val();
    subData.ons = $("#ons").val();
    subData.ty = "";
    subData.pageIndex = pageIndex;
    subData.pageListNum = 10;

    obj.reqData = [subData];
    var url = "/core/api/Osrzs/getDataRoleList";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Role Name</th>';
        listHtml += '            <th>Memo</th>';
        listHtml += '            <th>Create Time</th>';
        listHtml += '            <th class="width-120">Create User</th>';
        listHtml += '            <th>Status</th>';
        listHtml += '            <th class="width-50 text-center">Operation</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            for (var i = 0; i < result.resData.length; i++) {
                listHtml += '<tr data-id="' + result.resData[i]["roleID"] + '">';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + result.resData[i]["roleName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["roleMemo"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["createTime"].substr(0, 10) + '</td>';
                listHtml += '    <td>' + result.resData[i]["createUserName"] + '</td>';
                listHtml += '    <td>' + (result.resData[i]["roleStatus"] == "1" ? '<span class="badge rounded-pill bg-success">Enabled</span>' :'<span class="badge rounded-pill bg-danger">Disabled</span>') + '</td>';
                listHtml += '    <td>';
                listHtml += '        <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toEdit" data-id="' + result.resData[i]["roleID"] + '" title="edit"><i class="fas fa-pencil-alt"></i></button>';
                listHtml += '        <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toDel" data-id="' + result.resData[i]["roleID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button>';
                listHtml += '    </td>';
                listHtml += '</tr>';
                No++;
            }
            listHtml += "</tbody></table>";
            $("#tblRole").html(listHtml);
            var pagination = getPageNumHtml(result.number, subData.pageListNum, pageIndex);
            $("#paginationArea").html(pagination);
            highLightTr();
        }
        else {
            listHtml += "<tr><td colspan='7' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblRole").html(listHtml);
            nullRoleBind();
        }
    })
}


function getRoleBind() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getRoleBind";
    obj.reqData = [$("#rid").val()];
    var url = "/core/api/Osrzs/getDataRoleBind";
    httpPost(url, obj, function (result) {
        var tblHtml = "";
        tblHtml += '<table id="datatable" class="table table-bordered dt-responsive table-striped w-100">';
        tblHtml += '    <thead class="table-secondary table-header-fixed">';
        tblHtml += '        <tr>';
        tblHtml += '            <th>No.</th>';
        tblHtml += '            <th>Authorize Type</th>';
        tblHtml += '            <th>Dynamic Organization</th>';
        tblHtml += '            <th>Static Organization</th>';
        tblHtml += '            <th>Include or exclude Sub_organization</th>';
        tblHtml += '            <th class="width-120">Create Time</th>';
        tblHtml += '            <th>Create User</th>';
        tblHtml += '            <th class="width-50">Operation</th>';
        tblHtml += '        </tr>';
        tblHtml += '    </thead>';
        tblHtml += '    <tbody>';
        if (result.status == 1) {
            var No = 1;
            for (var i = 0; i < result.resData.length; i++) {
                var dynamicText = "";
                if (result.resData[i]["dynamicOrg"] == "unit") { dynamicText = "The unit which user work in"; }
                if (result.resData[i]["dynamicOrg"] == "dept") { dynamicText = "The department which user work in"; }

                tblHtml += '<tr>';
                tblHtml += '    <td>' + No + '</td>';
                tblHtml += '    <td>' + result.resData[i]["bindType"] + '</td>';
                tblHtml += '    <td>' + dynamicText + '</td>';
                tblHtml += '    <td>' + ((result.resData[i]["staticOrgName"] == null || result.resData[i]["staticOrgName"] == "0") ? "" : result.resData[i]["staticOrgName"]) + '</td>';
                tblHtml += '    <td>' + result.resData[i]["subOrgIn"] + '</td>';
                tblHtml += '    <td>' + result.resData[i]["createTime"].substr(0, 10) + '</td>';
                tblHtml += '    <td>' + result.resData[i]["createUserName"] + '</td>';
                tblHtml += '    <td class="text-center"><button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toDelBind" data-id="' + result.resData[i]["bindID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button> </td>';
                tblHtml += '</tr>';
                No++;
            }
            tblHtml += '</tbody></table>';
            $("#tblBind").html(tblHtml);


        }
        else {
            tblHtml += "<tr><td colspan='8' class='text-center'>No Result</td></tr></tbody></table>";
            $("#tblBind").html(tblHtml);
        }
        bindEvent();
    })
}

function nullRoleBind() {
    var tblHtml = "";
    tblHtml += '<table id="datatable" class="table table-bordered dt-responsive table-striped w-100">';
    tblHtml += '    <thead class="table-secondary table-header-fixed">';
    tblHtml += '        <tr>';
    tblHtml += '            <th>No.</th>';
    tblHtml += '            <th>Authorize Type</th>';
    tblHtml += '            <th>Dynamic Organization</th>';
    tblHtml += '            <th>Static Organization</th>';
    tblHtml += '            <th>Include or exclude Sub_organization</th>';
    tblHtml += '            <th class="width-120">Create Time</th>';
    tblHtml += '            <th>Create User</th>';
    tblHtml += '            <th class="width-50">Operation</th>';
    tblHtml += '        </tr>';
    tblHtml += '    </thead>';
    tblHtml += '    <tbody>';
    tblHtml += "<tr><td colspan='8' class='text-center'>No Result</td></tr></tbody></table>";
    $("#tblBind").html(tblHtml);
}

function highLightTr() {
    $('#dt1 tr').click(function () {
        var rid = $(this).attr("data-id");
        $('#dt1 tr').removeClass('highlight');
        $(this).addClass('highlight');
        $("#rid").val(rid);
        if (rid != "" && rid != undefined) { getRoleBind(); }
    });
    $("#dt1 tr:first-child").click();
}


function bindEvent() {
    $("#btnSearch").on("click", function () {
        getRoleList(1);
    })

    $("button[name='toEdit']").on("click", function () {
        var rid = $(this).attr("data-id");
        $("#showModalFrame").attr("src", "dataRoleEdit.html?t=u&i=" + rid).attr("width", "950px").attr("height", "260px");
        $("#mtt").html("Edit Data Role");
        $("#showModal").modal("show");
    })

    $("#toItem").on("click", function () {
        var rid = $("#rid").val();
        $("#showModalFrame").attr("src", "dataRoleItem.html?i=" + rid).attr("width", "950px").attr("height", "260px");
        $("#mtt").html("New Data Scope");
        $("#showModal").modal("show");
    })

    $("button[name='toDel']").on("click", function () {
        var rid = $(this).attr("data-id");
        Swal.fire({
            title: "Are you sure?",
            text: "You are going to delete this item,you won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, delete it!"
        }).then(function (e) {
            if (e.isConfirmed == true) {
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "query roleDel";
                obj.reqData = [rid];
                var url = "/core/api/Osrzs/dataRoleDel";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success!", "This role has been deleted!").then(function () { getRoleList(1) });
                    }
                    else {
                        Swal.fire("Error!", result.message);
                    }
                })
            }
        })
    })

    $("button[name='toDelBind']").on("click", function () {
        var bid = $(this).attr("data-id");
        Swal.fire({
            title: "Are you sure?",
            text: "You are going to delete this item,you won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, delete it!"
        }).then(function (e) {
            if (e.isConfirmed == true) {
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "query bindDel";
                obj.reqData = [bid];
                var url = "/core/api/Osrzs/dataBindDel";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success!", "This function has been deleted!").then(function () { getRoleBind() });
                    }
                    else {
                        Swal.fire("Error!", result.message);
                    }
                })
            }


        })
    })
}
function closeModal() {
    $("#showModal").modal("hide");
}