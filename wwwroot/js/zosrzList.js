jQuery(function ($) {
    getBind(1);
    getObjList();
    bindEventOnload();

})

function queryList(pageIndex) {
    getBind(pageIndex);
}
function getBind(pageIndex) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getField";

    var subData = new Object();
    subData.oid = "";
    subData.kw = $("#kw").val();
    subData.ons = $("#ons").val();
    subData.ty = $("#type").val();
    subData.pageIndex = pageIndex;
    subData.pageListNum = 20;

    obj.reqData = [subData];
    var url = "/core/api/Osrzs/getAllBind";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Authorize Type</th>';
        listHtml += '            <th>Object Type</th>';
        listHtml += '            <th>Object Name</th>';
        listHtml += '            <th>Authorize Role</th>';
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
                listHtml += '    <td>' + markConvert(result.resData[i]["osrzWay"]) + '</td>';
                listHtml += '    <td>' + markConvert(result.resData[i]["osrzObjType"]) + '</td>';
                listHtml += '    <td>' + result.resData[i]["objName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["roleName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["createTime"].substr(0, 10) + '</td>';
                listHtml += '    <td>' + result.resData[i]["createUserName"] + '</td>';
                listHtml += '    <td>' + (result.resData[i]["osrzStatus"] == "1" ? '<span class="badge rounded-pill bg-success">Enabled</span>' : '<span class="badge rounded-pill bg-danger">Disabled</span>') + '</td>';
                listHtml += '    <td class="text-center">';
                listHtml += '        <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toDel" data-type="' + result.resData[i]["osrzWay"] +'" data-id="' + result.resData[i]["osrzID"] + '" title="disable"><i class="fas fa-trash-alt"></i></button>';
                listHtml += '    </td>';
                listHtml += '</tr>';
                No++;
            }
            listHtml += "</tbody></table>";
            $("#tblRole").html(listHtml);
            var pagination = getPageNumHtml(result.number, subData.pageListNum, pageIndex);
            $("#paginationArea").html(pagination);
        }
        else {
            listHtml += "<tr><td colspan='9' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblRole").html(listHtml);
        }
        bindEvent();
    })
}

function markConvert(str) {
    switch (str) {
        case "data":
            return "Data Authorization";
            break;
        case "func":
            return "Menu Authorization";
            break;
        case "unit":
            return "Organization";
            break;
        case "dept":
            return "Department";
            break;
        case "post":
            return "Postion";
            break;
        case "psn":
            return "Person";
            break;
    }
}

function getObjList() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query objList";
    obj.reqData = [""];
    var url = "/core/api/Osrzs/getObjList";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var optionHtml = "<option value='0'>---Please Choose--</option>";
            for (var i = 0; i < result.resData.length; i++) {
                optionHtml += "<option value='" + result.resData[i]["objType"] + ":" + result.resData[i]["objID"] + "'>" + result.resData[i]["objName"] + "</option>"
            }
            $("#kw").html(optionHtml);
            $("#kw").select2({ width: '200px' });
        }
    })
}


function bindEventOnload() {
    $("#btnNewFunc").on("click", function () {
        $("#showModalFrame").attr("src", "osrzEdit.html?t=f").attr("width", "950px").attr("height", "360px");
        $("#mtt").html("New Func Role Bind");
        $("#showModal").modal("show");
    })

    $("#btnNewData").on("click", function () {
        $("#showModalFrame").attr("src", "osrzEdit.html?t=d").attr("width", "950px").attr("height", "360px");
        $("#mtt").html("New Data Role Bind");
        $("#showModal").modal("show");
    })

    $("#btnSearch").on("click", function () {
        getBind(1);
    })

}

function bindEvent() {

    $("button[name='toDel']").on("click", function () {
        var that = this;
        var bid = $(that).attr("data-id");
        var btype = $(that).attr("data-type");
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
                obj.action = "osrzDelete";
                var subData = new Object();
                subData.id = bid;
                subData.type = btype;
                obj.reqData = [subData];
                var url = "/core/api/Osrzs/osrzDelete";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success!", "This authorization has been deleted!").then(function () { getBind(1) });
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