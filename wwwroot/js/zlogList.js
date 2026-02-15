jQuery(function ($) {
    getLog(1);
    bindEvent();
});

function queryList(pageIndex) {
    getLog(pageIndex);
}
function getLog(pageIndex) {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getApi";

    var subData = new Object();
    subData.oid = "";
    subData.kw = $("#kw").val();
    if (($("#start").val().trim() == "" && $("#end").val().trim() != "") || ($("#start").val().trim() != "" && $("#end").val().trim() == "")) {
        Swal.fire("Error","Start date and end date cannot be null!");
        return;
    }
    else if (new Date($("#start").val()) > new Date($("#end").val())) {
        Swal.fire("Error", "Start date must before end date!");
        return;
    }
    else if ($("#start").val().trim() == "" && $("#end").val().trim() == "") {
        subData.ons = "";
    }
    else {
        subData.ons = $("#start").val() + ";" + $("#end").val();
    }

    subData.ty = "";
    subData.pageIndex = pageIndex;
    subData.pageListNum = 20;

    obj.reqData = [subData];
    var url = "/core/api/Users/getActLog";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Person Name</th>';
        listHtml += '            <th>User ID</th>';
        listHtml += '            <th>Time</th>';
        listHtml += '            <th>Action</th>';
        listHtml += '            <th>Is Success</th>';
        listHtml += '            <th>Result</th>';
        listHtml += '            <th>Arguments</th>';
        listHtml += '            <th>Memo</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            for (var i = 0; i < result.resData.length; i++) {
                listHtml += '<tr>';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + (result.resData[i]["psnName"] == null ? "" : result.resData[i]["psnName"]) + '</td>';
                listHtml += '    <td>' + (result.resData[i]["psnID"]) + '</td>';
                listHtml += '    <td>' + result.resData[i]["logTime"].substr(0, 10) + '</td>';
                listHtml += '    <td>' + result.resData[i]["logAction"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["isSuccess"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["actResult"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["actPara"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["actMemo"] + '</td>';
                listHtml += '</tr>';
                No++;
            }
            listHtml += "</tbody></table>";
            $("#tblArea").html(listHtml);
            var pagination = getPageNumHtml(result.number, subData.pageListNum, pageIndex);
            $("#paginationArea").html(pagination);
        }
        else {
            listHtml += "<tr><td colspan='9' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblArea").html(listHtml);
        }
        
    })
}

function bindEvent() {
    $("#btnSearch").on("click", function () {
        getLog(1);
    })

    $("#btnExport").on("click", function () {

        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query getApi";

        var subData = new Object();
        subData.oid = "";
        subData.kw = $("#kw").val();
        if (($("#start").val().trim() == "" && $("#end").val().trim() != "") || ($("#start").val().trim() != "" && $("#end").val().trim() == "")) {
            Swal.fire("Start date and end date must be input together!");
            return;
        }
        else if ($("#start").val().trim() == "" && $("#end").val().trim() == "") {
            subData.ons = "";
        }
        else {
            subData.ons = $("#start").val() + ";" + $("#end").val();
        }

        subData.ty = "";
        subData.pageIndex = 1;
        subData.pageListNum = 999999;

        obj.reqData = [subData];
        var url = "/core/api/Users/exportActLog";
        httpPost(url, obj, function (result) {
            const a = document.createElement('a');
            a.href = result.resData[1];
            a.download = result.resData[0];
            a.target = "_blank";
            a.click();
        })

    })
}
