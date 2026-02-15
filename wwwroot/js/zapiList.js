jQuery(function ($) {
    getApi(1);
    bindEvent();
});

function queryList(pageIndex) {
    getApi(pageIndex);
}
function getApi(pageIndex) {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getApi";

    var subData = new Object();
    subData.oid = "";
    subData.kw = $("#kw").val();
    subData.ons = $("#ons").val();
    subData.ty = $("#type").val();
    subData.pageIndex = pageIndex;
    subData.pageListNum = 20;

    obj.reqData = [subData];
    var url = "/core/api/Interface/getApiList";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Api Name</th>';
        listHtml += '            <th>Api Type</th>';
        listHtml += '            <th>Api Description</th>';
        listHtml += '            <th>Api Address</th>';
        listHtml += '            <th>Api Code</th>';
        listHtml += '            <th>Status</th>';
        listHtml += '            <th class="width-50 text-center">Operation</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            for (var i = 0; i < result.resData.length; i++) {
                listHtml += '<tr>';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + (result.resData[i]["apiName"]) + '</td>';
                listHtml += '    <td>' + (result.resData[i]["apiType"]) + '</td>';
                listHtml += '    <td>' + result.resData[i]["apiDescription"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["apiAddress"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["apiCode"] + '</td>';
                listHtml += '    <td>' + (result.resData[i]["apiStatus"] == "1" ? '<span class="badge rounded-pill bg-success">Enabled</span>' : '<span class="badge rounded-pill bg-danger">Disabled</span>') + '</td>';
                listHtml += '    <td class="width-120">';
                listHtml += '        <button type="button" class="btn btn-soft-info waves-effect waves-light btn-sm" name="toDetail" data-id="' + result.resData[i]["apiID"] + '" title="detail"><i class="fas fa-align-left"></i></button>';
                listHtml += '        <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toEdit" data-id="' + result.resData[i]["apiID"] + '" title="edit"><i class="fas fa-pencil-alt"></i></button>';
                listHtml += '        <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toDel" data-id="' + result.resData[i]["apiID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button>';
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
            listHtml += "<tr><td colspan='9' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblArea").html(listHtml);
        }
        
    })
}

function bindTblEvent() {
    $("button[name='toDetail']").on("click", function () {
        var aid = $(this).attr("data-id");
        $("#myModalFrame").attr("src", "apiDetail.html?t=u&i=" + aid).attr("width", "950px").attr("height", "410px");
        $("#mtt").html("Api Information");
        $("#myModal").modal("show");
    })
    $("button[name='toEdit']").on("click", function () {
        var aid = $(this).attr("data-id");
        $("#myModalFrame").attr("src", "apiEdit.html?t=u&i=" + aid).attr("width", "950px").attr("height", "410px");
        $("#mtt").html("Edit Api");
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
                obj.action = "query infDel";
                obj.reqData = [aid];
                var url = "/core/api/Interface/apiDel";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success!", "This interface already be deleted!").then(function () { getApp(1) });
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
        $("#myModalFrame").attr("src", "apiEdit.html?t=a").attr("width", "950px").attr("height", "410px");
        $("#mtt").html("New Api");
        $("#myModal").modal("show");
    })

    $("#btnSearch").on("click", function () {
        getApi(1);
    })
}
function closeModal() {
    $("#myModal").modal("hide");
}