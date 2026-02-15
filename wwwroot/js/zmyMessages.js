jQuery(function ($) {
    getMsgList(1);
    bindEvent();
})

function queryList(pageIndex) {
    getMsgList(pageIndex);
}
function getMsgList(pageIndex) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMyMessages";
    var reqData = new Object();
    reqData.oid = "";
    reqData.kw = $("#kw").val().trim();
    reqData.ons = "";
    reqData.ty = "";
    reqData.pageIndex = pageIndex;
    reqData.pageListNum = 20;
    obj.reqData = [reqData];

    var url = "/core/api/DashBoard/getMyMessages";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary  table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Title</th>';
        listHtml += '            <th>Content</th>';
        listHtml += '            <th>Create Time</th>';
        listHtml += '            <th>Is Read</th>';
        listHtml += '            <th class="width-50 text-center">Operation</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            for (var i = 0; i < result.resData.length; i++) {
                listHtml += '<tr data-id="' + result.resData[i]["msnID"] + '">';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + result.resData[i]["msgTitle"] + '</td>';
                listHtml += '    <td>' + subStrText(result.resData[i]["msgContent"], 60) + '</td>';
                listHtml += '    <td>' + result.resData[i]["createTime"].substr(0, 16).replace('T', '&nbsp;') + '</td>';
                listHtml += '    <td id="value' + result.resData[i].msgID + '">' + (result.resData[i].readCount == 0 ? '<span class="badge bg-warning rounded-pill pt-1">unread</span>&nbsp;&nbsp;' : '<span class="badge bg-info rounded-pill pt-1">readed</span>&nbsp;&nbsp;') + '</td>';
                listHtml += '    <td class="text-center">';
                listHtml += '        <button type="button" class="btn btn-soft-info waves-effect waves-light btn-sm" name="toDetail" data-id="' + result.resData[i]["msgID"] + '" title="disable"><i class="fas fa-align-justify"></i></button>';
                listHtml += '    </td>';
                listHtml += '</tr>';
                No++;
            }
            listHtml += "</tbody></table>";
            $("#tblRole").html(listHtml);
            var pagination = getPageNumHtml(result.number, reqData.pageListNum, pageIndex);
            $("#paginationArea").html(pagination);
        }
        else {
            listHtml += "<tr><td colspan='5' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblRole").html(listHtml);
        }
        bindListEvent();
    })
}
function getMsgDetail(msgID) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMessageDetail";
    obj.reqData = [msgID.toString()];
    var url = "/core/api/DashBoard/getMessageDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#objTitle").html(result.resData[0].msgTitle);
            $("#objContent").html(result.resData[0].msgContent);
            $("#objSender").html("Sender: " + result.resData[0].appName);
            $("#objTime").html("Time: " + result.resData[0].createTime.substr(0, 10));
            if (result.resData[0].msgUrl != "" && result.resData[0].msgUrl != null) {
                $("#objUrl").html(' <a href="#" onclick="gotoTask(' + msgID + ')">Click here to process the task</a>');
                $("#objUrl").css("display", "block");
            }
            else {
                $("#objUrl").html('');
                $("#objUrl").css("display", "none");
            }

            $("#msgntsModel").modal("show");
            readCount(msgID, "message");
            if (result.resData[0].readCount == 0) {
                $("#value" + msgID).html('<span class="badge bg-info rounded-pill pt-1">readed</span>&nbsp;&nbsp;');
            }
        }
    })
}

function readCount(oid, type) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMessageDetail";
    obj.reqData = [oid.toString(), type];
    var url = "/core/api/DashBoard/readCount";
    httpPost(url, obj, function (result) {
        if (result.status == 1 && type == "message") {
        }
    })
}

function bindEvent() {
    $("#btnSearch").on("click", function () {
        getMsgList(1);
    })

}

function bindListEvent() {
    $("button[name='toDetail']").on("click", function () {
        let msgID = Number($(this).data("id"));
        getMsgDetail(msgID);
        $("#msgDetailModel").modal("show");
    })
}

function gotoTask(msgID) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMsgUrl";
    obj.reqData = [msgID.toString()];
    var url = "/core/api/DashBoard/getMsgUrl";
    httpPost(url, obj, function (result) {
        if (result.status == 1 && result.resData.length > 0) {
            if (result.resData[1] == "FlowNode") {
                window.location.href = "myApprove.html?Nid=" + result.resData[2];
            }
            else {
                window.location.href = result.resData[0];
            }
        }
        else {
            Swal.fire("Alert!", "There is something wrong occured, please retry or contact administrator!", "error")
        }

    })
}