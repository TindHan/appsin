jQuery(function ($) {
    getSet(1);
    bindEvent();
});

function queryList(pageIndex) {
    getSet(pageIndex);
}
function getSet(pageIndex) {

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
    var url = "/core/api/Fieldcode/setList";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Set Name</th>';
        listHtml += '            <th>Set Type</th>';
        listHtml += '            <th>Set Code</th>';
        listHtml += '            <th>Set Level</th>';
        listHtml += '            <th>Set Description</th>';
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
                listHtml += '    <td>' + result.resData[i]["setName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["setType"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["setCode"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["setLevel"] + '</td>';
                listHtml += '    <td>' + (result.resData[i]["setDescription"] == null ? "" : result.resData[i]["setDescription"]) + '</td>';
                listHtml += '    <td>' + result.resData[i]["createUser"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["createTime"].substr(0, 10) + '</td>';
                listHtml += '    <td>' + (result.resData[i]["setStatus"] == "1" ? '<span class="badge rounded-pill bg-success">Enabled</span>' : '<span class="badge rounded-pill bg-danger">Disabled</span>') + '</td>';
                listHtml += '    <td class="width-120">';
                listHtml += '        <button type="button" class="btn btn-soft-info waves-effect waves-light btn-sm" name="toItem" data-id="' + result.resData[i]["setID"] + '" title="detail"><i class="fas fa-align-left"></i></button>';
                listHtml += '        <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toEdit" data-id="' + result.resData[i]["setID"] + '" title="edit"><i class="fas fa-pencil-alt"></i></button>';
                listHtml += '        <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toDel" data-id="' + result.resData[i]["setID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button>';
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
    $("button[name='toItem']").on("click", function () {
        var aid = $(this).attr("data-id");
        window.location.href = "itemList.html?i=" + aid;
    })
    $("button[name='toEdit']").on("click", function () {
        var aid = $(this).attr("data-id");
        $("#myModalFrame").attr("src", "setEdit.html?t=u&i=" + aid).attr("width", "950px").attr("height", "410px");
        $("#mtt").html("Edit Field Set");
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
                var url = "/core/api/Fieldcode/setDel";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success!", "This interface already be deleted!").then(function () { getSet(1) });
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
        $("#myModalFrame").attr("src", "setEdit.html?t=a").attr("width", "950px").attr("height", "410px");
        $("#mtt").html("New Field Set");
        $("#myModal").modal("show");
    })

    $("#btnSearch").on("click", function () {
        getSet(1);
    })
}
function closeModal() {
    $("#myModal").modal("hide");
}