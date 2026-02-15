jQuery(function ($) {
    getApp();
    getApiOsrz(1);
    bindEvent();
});

function getApp() {

    var aid = request("i");
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query app";
    obj.reqData = [""];
    var url = "/core/api/Apps/getAllApp";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var optionHtml = "<option value='all'>All</option>";
            for (var i = 0; i < result.resData.length; i++) {
                optionHtml += "<option value='" + result.resData[i].appID + "'>" + result.resData[i].appName + "</option>";
            }
            $("#appID").html(optionHtml);
        }
        else {
            Swal.fire("Error", "Query application list failed,please retry or contact administrator!");
        }
    })
}

function queryList(pageIndex) {
    getApiOsrz(pageIndex);
}
function getApiOsrz(pageIndex) {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getApp";

    var subData = new Object();
    subData.oid = $("#appID").val();
    subData.ons = $("#osrzStatus").val();
    subData.kw = ""; subData.ty = "";
    subData.pageIndex = pageIndex;
    subData.pageListNum = 20;

    obj.reqData = [subData];
    var url = "/core/api/Interface/osrzList";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Application Name</th>';
        listHtml += '            <th>Api Name</th>';
        listHtml += '            <th>Api Code</th>';
        listHtml += '            <th>Description</th>';
        listHtml += '            <th>Start</th>';
        listHtml += '            <th>End</th>';
        listHtml += '            <th>Create User</th>';
        listHtml += '            <th>Create Time</th>';
        listHtml += '            <th>Status</th>';
        listHtml += '            <th class="width-50 text-center">Operation</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            for (var i = 0; i < result.resData.length; i++) {
                listHtml += '<tr>';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + result.resData[i]["appName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["apiName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["apiCode"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["osrzDescription"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["validStartTime"].substr(0, 10) + '</td>';
                listHtml += '    <td>' + result.resData[i]["validEndTime"].substr(0, 10) + '</td>';
                listHtml += '    <td>' + result.resData[i]["createUser"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["createTime"].substr(0, 10) + '</td>';
                listHtml += '    <td>' + (result.resData[i]["osrzStatus"] == "1" ? '<span class="badge rounded-pill bg-success">Enabled</span>' : '<span class="badge rounded-pill bg-danger">Disabled</span>') + '</td>';
                listHtml += '    <td class="width-50 text-center">';
                listHtml += '        <button type="button" class="btn btn-soft-info waves-effect waves-light btn-sm" name="toEdit" data-id="' + result.resData[i]["osrzID"] + '" title="disable"><i class="fas fa-pencil-alt"></i></button>';
                listHtml += '        <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toDel" data-id="' + result.resData[i]["osrzID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button>';
                listHtml += '    </td>';
                listHtml += '</tr>';
                No++;
            }
            listHtml += "</tbody></table>";
            $("#tblArea").html(listHtml);
            var pagination = getPageNumHtml(result.number, subData.pageListNum, pageIndex);
            $("#paginationArea").html(pagination);
            bindTblEvent();
        }
        else {
            listHtml += "<tr><td colspan='11' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblArea").html(listHtml);
        }
        
    })
}

function bindTblEvent() {

    $("button[name='toEdit']").on("click", function () {
        var aid = $(this).attr("data-id");
        $("#myModalFrame").attr("src", "apiOsrzEdit.html?t=u&i=" + aid).attr("width", "950px").attr("height", "410px");
        $("#mtt").html("Edit Api Call");
        $("#myModal").modal("show");

    })
    $("button[name='toDel']").on("click", function () {
        var aid = $(this).attr("data-id");
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
                obj.action = "query apiDel";
                obj.reqData = [aid];
                var url = "/core/api/Interface/osrzDel";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success!", "This Api already be deleted!").then(function () { getApiOsrz(1) });
                    }
                    else {
                        Swal.fire("Error!", result.message);
                    }
                })
            }
        })
    })
}
function bindEvent() {
    $("#btnNew").on("click", function () {
        $("#myModalFrame").attr("src", "apiOsrzEdit.html?t=a").attr("width", "950px").attr("height", "410px");
        $("#mtt").html("New Api Call");
        $("#myModal").modal("show");
    })

    $("#btnSearch").on("click", function () {
        getApiOsrz(1);
    })
}
function closeModal() {
    $("#myModal").modal("hide");
}