jQuery(function ($) {
    getNoticeList(1);
    bindEvent();
})

function queryList(pageIndex) {
    getNoticeList(pageIndex);
}
function getNoticeList(pageIndex) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMyTasks";
    var reqData = new Object();
    reqData.oid = "";
    reqData.kw = $("#kw").val();
    reqData.ons = "";
    reqData.ty = "";
    reqData.pageIndex = pageIndex;
    reqData.pageListNum = 20;
    obj.reqData = [reqData];

    var url = "/core/api/DashBoard/getMyNotices";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
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
                listHtml += '<tr data-id="' + result.resData[i].noticeID + '">';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + result.resData[i].noticeTitle + '</td>';
                listHtml += '    <td class="width-40percent">' + subStrText(result.resData[i].noticeContent, 60) + '</td>';
                listHtml += '    <td>' + result.resData[i]["createTime"].substr(0, 16).replace('T', '&nbsp;') + '</td>';
                listHtml += '    <td id="value' + result.resData[i].noticeID + '">' + (result.resData[i].readCount == 0 ? '<span class="badge bg-warning rounded-pill pt-1">unread</span>&nbsp;&nbsp;' : '<span class="badge bg-info rounded-pill pt-1">readed</span>&nbsp;&nbsp;') + '</td>';
                listHtml += '    <td class="text-center">';
                listHtml += '        <button type="button" class="btn btn-soft-info waves-effect waves-light btn-sm" name="toDetail" data-id="' + result.resData[i].noticeID + '" title="disable"><i class="fas fa-align-justify"></i></button>';
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
            listHtml += "<tr><td colspan='6' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblRole").html(listHtml);
        }
        bindListEvent();
    })
}
function getNoticeDetail(noticeID) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getNoticeDetail";
    obj.reqData = [noticeID.toString()];
    var url = "/core/api/DashBoard/getNoticeDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#objTitle").html(result.resData[0].noticeTitle);
            $("#objContent").html(result.resData[0].noticeContent);
            $("#objSender").html("Sender: " + result.resData[0].createUserName);
            $("#objTime").html("Time: " + result.resData[0].createTime.substr(0, 10));
            if (result.resData[0].noticeFile != "" && result.resData[0].noticeFile != null) {
                $("#objFile").html('Attachment: <a href="#" onclick="downNoticeFile(' + noticeID + ')">' + result.resData[0].noticeFile + '</a>');
                $("#objFile").css("display", "block");
            }
            else {
                $("#objFile").html('');
                $("#objFile").css("display", "none");
            }

            $("#noticeDetailModel").modal("show");

            readCount(noticeID, "notice");
            if (result.resData[0].readCount == 0) {
                $("#value" + noticeID).html('<span class="badge bg-info rounded-pill pt-1">readed</span>&nbsp;&nbsp;');
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
        getNoticeList(1);
    })

}

function bindListEvent() {
    $("button[name='toDetail']").on("click", function () {
        let noticeID = Number($(this).data("id"));
        getNoticeDetail(noticeID);
        $("#noticeDetailModel").modal("show");
    })
}


function downNoticeFile(noticeID) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getFile";
    obj.reqData = [noticeID.toString(), "notice"];
    var url = "/core/api/DashBoard/filePath";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            const a = document.createElement('a');
            a.href = result.resData[0];
            a.download = result.resData[1];
            a.target = "_blank";
            a.click();
        }
    })
}