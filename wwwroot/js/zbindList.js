jQuery(function ($) {
    bindType();
});
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

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getField";

    var subData = new Object();
    subData.oid = "10000";
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
                    tableHtml += '    <td>' + result.resData[i]["psnName"] + '</td>';
                    tableHtml += '    <td>' + result.resData[i]["psnSex"] + '</td>';
                    tableHtml += '    <td>' + result.resData[i]["psnCode"] + '</td>';
                    tableHtml += '    <td>' + result.resData[i]["idNo"] + '</td>';
                    tableHtml += '    <td>';
                    tableHtml += '        <div class="text-center">';
                    tableHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toTransfer" data-id="' + result.resData[i]["psnID"] + '" title="transfer"><i class="fas fa-align-justify"></i></button>';
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

    $("#btnSearch").on("click", function () {
        getPsnList(1);
    })

    $('button[name="toTransfer"]').on('click', function (e) {
        var pid = $(this).attr("data-id");
        $("#showModalFrame").attr("src", "bindDetail.html?i=" + pid).attr("width", "950px").attr("height", "500px");
        $("#mtt").html("Person Bind Role");
        $("#showModal").modal("show");
    });

}

function closeModal() {
    $("#showModal").modal("hide");
}