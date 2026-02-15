jQuery(function ($) {
    getBind();
    bindEvent();
});

function getBind() {
    var pid = request("i");
    console.log(pid);
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array(pid.toString());
    var url = "/core/api/Users/getPsnRole";
    httpPost(url, obj, function (result) {

        if (result.status == 1 && result.resData!=null) {
            localStorage.setItem("uToken", result.uToken);
            var No = 1;
            var tableHtml = "";
            tableHtml += ' <table id = "datatable" class="table table-bordered dt-responsive table-striped w-100" >';
            tableHtml += '    <thead class="table-secondary table-header-fixed">';
            tableHtml += '        <tr>';
            tableHtml += '            <th>No.</th>';
            tableHtml += '            <th>bindType</th>';
            tableHtml += '            <th>roleName</th>';
            tableHtml += '            <th>roleMemo1</th>';
            tableHtml += '            <th>createTime</th>';
            tableHtml += '            <th>createUser</th>';
            tableHtml += '        </tr>';
            tableHtml += '    </thead>';
            tableHtml += '    <tbody>';
            for (var i = 0; i < result.resData.length; i++) {
                tableHtml += '<tr>';
                tableHtml += '    <td>' + No + '</td>';
                tableHtml += '    <td>' + result.resData[i]["roleObj"] + '</td>';
                tableHtml += '    <td>' + result.resData[i]["roleName"] + '</td>';
                tableHtml += '    <td>' + result.resData[i]["roleMemo1"] + '</td>';
                tableHtml += '    <td>' + result.resData[i]["createTime"].substr(0,16) + '</td>';
                tableHtml += '    <td>' + result.resData[i]["createUser"] + '</td>';
                tableHtml += '</tr>';
                No++;
            }
            tableHtml += '     </tbody>';
            tableHtml += '</table>';
            $("#divInfo").html(tableHtml);
        }
        else {
            localStorage.setItem("uToken", result.uToken);
            var No = 1;
            var tableHtml = "";
            tableHtml += ' <table id = "datatable" class="table table-bordered dt-responsive table-striped w-100" >';
            tableHtml += '    <thead class="table-secondary table-header-fixed">';
            tableHtml += '        <tr>';
            tableHtml += '            <th>No.</th>';
            tableHtml += '            <th>bindType</th>';
            tableHtml += '            <th>roleName</th>';
            tableHtml += '            <th>roleMemo1</th>';
            tableHtml += '            <th>createTime</th>';
            tableHtml += '            <th>createUser</th>';
            tableHtml += '        </tr>';
            tableHtml += '    </thead>';
            tableHtml += '    <tbody>';
                tableHtml += '<tr>';
                tableHtml += '    <td colspan="6" class="text-center">No Data</td>';
                tableHtml += '</tr>';
            tableHtml += '     </tbody>';
            tableHtml += '</table>';
            $("#divInfo").html(tableHtml);
        }
    })
}


function bindEvent() {
    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}