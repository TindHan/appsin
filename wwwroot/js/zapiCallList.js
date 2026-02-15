jQuery(function ($) {
    getApp();
    bindEvent();
});
function getApp() {
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
            $("#oid").html(optionHtml);

            getApi();
        }
        else {
            Swal.fire("Error", "Query application list failed,please retry or contact administrator!");
        }
    })
}

function getApi() {
    var aid = request("i");
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query app";
    obj.reqData = [""];
    var url = "/core/api/Interface/getAllApi";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var optionHtml = "<option value='all'>All</option>";
            for (var i = 0; i < result.resData.length; i++) {
                optionHtml += "<option value='" + result.resData[i].apiID + "'>" + result.resData[i].apiName + "</option>";
            }
            $("#type").html(optionHtml);

            getLog(1);
        }
        else {
            Swal.fire("Error", "Query application list failed,please retry or contact administrator!");
        }
    })
}
function queryList(pageIndex) {
    getLog(pageIndex);
}
function getLog(pageIndex) {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getApiUseLog";

    var subData = new Object();
    subData.oid = $("#oid").val();
    subData.kw = "";
    subData.ty = $("#type").val();
    subData.ons = "";
    subData.pageIndex = pageIndex;
    subData.pageListNum = 20;

    obj.reqData = [subData];
    var url = "/core/api/Interface/getApiUseLog";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Application</th>';
        listHtml += '            <th>Api</th>';
        listHtml += '            <th>Invoke Time</th>';
        listHtml += '            <th>Is Success</th>';
        listHtml += '            <th>Domain</th>';
        listHtml += '            <th class="width-50 text-center">Operation</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            for (var i = 0; i < result.resData.length; i++) {
                listHtml += '<tr>';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + (result.resData[i]["appName"]) + '</td>';
                listHtml += '    <td>' + (result.resData[i]["apiName"]) + '</td>';
                listHtml += '    <td>' + result.resData[i]["createTime"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["isS"] + '</td>';
                listHtml += '    <td>' + (result.resData[i]["domain"] == undefined ? "" : result.resData[i]["domain"]) + '</td>';
                listHtml += '    <td class="text-center">';
                listHtml += '        <button type="button" class="btn btn-soft-info waves-effect waves-light btn-sm" name="toDetail" data-id="' + result.resData[i]["logID"] + '" title="detail"><i class="fas fa-align-left"></i></button>';
                listHtml += '    </td>';
                listHtml += '</tr>';
                No++;
            }
            listHtml += "</tbody></table>";
            $("#tblArea").html(listHtml);
            var pagination = getPageNumHtml(result.number, subData.pageListNum, pageIndex);
            $("#paginationArea").html(pagination);
            bindListEvent();
        }
        else {
            listHtml += "<tr><td colspan='7' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblArea").html(listHtml);
        }

    })
}

function bindListEvent() {
    $("button[name='toDetail']").on("click", function () {
        var oid = $(this).data("id");
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "getApiUseLogDetail";

        obj.reqData = [oid.toString()];
        var url = "/core/api/Interface/getApiUseLogDetail";

        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                console.log(result);
                let t = result.resData[0];
                $("#appName").html(t.appName);
                $("#apiName").html(t.apiName);
                $("#createTime").html(t.createTime);
                $("#isS").html(t.isS);
                $("#apiDomain").html(t.apiDomain);
                $("#logMemo").html(t.logMemo == null ? "" : t.logMemo);
                $("#inPara").html(t.inPara);
                $("#outPara").html(t.outPara);
            }
        })

        $("#myModal").modal("show");
    })
}
function bindEvent() {
    $("#btnSearch").on("click", function () {
        getLog(1);
    })

    $("#toClose").on("click", function () {
        $("#myModal").modal("hide");
    })
}
