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
    subData.pageIndex = pageIndex;
    subData.pageListNum = 20;

    obj.reqData = [subData];
    var url = "/core/api/Users/getGotoLog";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Person Name</th>';
        listHtml += '            <th>Person ID</th>';
        listHtml += '            <th>App Name</th>';
        listHtml += '            <th>App ID</th>';
        listHtml += '            <th>Menu Name</th>';
        listHtml += '            <th>Menu ID</th>';
        listHtml += '            <th>Time</th>';
        listHtml += '            <th>Authorized String</th>';
        listHtml += '            <th>Callback Time</th>';
        listHtml += '            <th>Callback Result</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            for (var i = 0; i < result.resData.length; i++) {
                listHtml += '<tr>';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + result.resData[i]["psnName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["psnID"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["appName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["appID"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["menuName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["menuID"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["goTime"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["goStr"] + '</td>';
                listHtml += '    <td>' + (result.resData[i]["callBackTime"].substr(0,4)=="0001" ? "" : result.resData[i]["callBackTime"]) + '</td>';
                listHtml += '    <td>' + (result.resData[i]["callBackRes"] == null ? "" : result.resData[i]["callBackRes"]) + '</td>';

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
        var url = "/core/api/Users/ExportGotoLog";
        httpPost(url, obj, function (result) {

            const a = document.createElement('a');
            a.href = result.resData[1];
            a.download = result.resData[0];
            a.target = "_blank";
            a.click();

        })
    })
}
