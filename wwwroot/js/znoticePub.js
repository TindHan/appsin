jQuery(function ($) {
    getNoticeList(1);
    bindEvent();
})
const editor = new AppsinRichTextEditor('nContent');
function queryList(pageIndex) {
    getNoticeList(pageIndex);
}
function getNoticeList(pageIndex) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getNotices";
    var reqData = new Object();
    reqData.oid = "";
    reqData.kw = $("#kw").val();
    reqData.ons = $("#ons").val();
    reqData.ty = "";
    reqData.pageIndex = pageIndex;
    reqData.pageListNum = 20;
    obj.reqData = [reqData];

    var url = "/core/api/DashBoard/getNotices";
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
        listHtml += '            <th>Read Person(s)</th>';
        listHtml += '            <th>Operation</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            for (var i = 0; i < result.resData.length; i++) {
                listHtml += '<tr data-id="' + result.resData[i].noticeID + '">';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + result.resData[i].noticeTitle + '</td>';
                listHtml += '    <td class="width-40percent">' + subStrText(result.resData[i].noticeContent, 60) + '</td>';
                listHtml += '    <td class="width-200 text-center">' + result.resData[i]["createTime"].substr(0,16).replace('T', '&nbsp;') + '</td>';
                listHtml += '    <td class="width-150 text-center">' + (result.resData[i].readCount == null ? "0" : result.resData[i].readCount) + '</td>';
                listHtml += '    <td class="width-150 text-center">';
                listHtml += '        <button type="button" class="btn btn-soft-info waves-effect waves-light btn-sm" name="toDetail" data-id="' + result.resData[i].noticeID + '" title="disable"><i class="fas fa-align-justify"></i></button>';
                listHtml += '        <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toEdit" data-id="' + result.resData[i].noticeID + '" title="edit" ' + (result.resData[i].noticeStatus == -1 ? 'disabled' : '') + '><i class="fas fa-pencil-alt"></i></button>';
                listHtml += '        <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toDel" data-id="' + result.resData[i].noticeID + '" title="delete" ' + (result.resData[i].noticeStatus == -1 ? 'disabled' : '') + '><i class="fas fa-trash-alt"></i></button>';
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
            $("#startTime").html("Start: " + result.resData[0].startTime.substr(0, 10));
            $("#endTime").html("End: " + result.resData[0].endTime.substr(0, 10));
            $("#noticeFileName").val(result.resData[0].noticeFile);
            $("#storeFileName").val(result.resData[0].storeFile);
            if (result.resData[0].noticeFile != "" && result.resData[0].noticeFile != null) {
                $("#objFile").html('Attachment: <a href="#" onclick="downNoticeFile(' + noticeID + ')">' + result.resData[0].noticeFile + '</a>');
                $("#objFile").css("display", "block");
            }
            else {
                $("#objFile").html('');
                $("#objFile").css("display", "none");
            }

            $("#noticeDetailModel").modal("show");

            if (result.resData[0].readCount == 0) {
                $("#value" + noticeID).html('<span class="badge bg-info rounded-pill pt-1">readed</span>&nbsp;&nbsp;');
            }
        }

    })
}

function bindEvent() {
    $("#btnSearch").on("click", function () {
        getNoticeList(1);
    })
    $("#btnNoticeNew").on("click", function () {
        $("#noticeID").val(0);
        $("#nTitle").val("");
        $("#nStart").val("");
        $("#nEnd").val("");
        editor.setContent("");
        $("#nFile").html('');
        $("#noticeFileName").val("");
        $("#storeFileName").val("");
        $("#btnSaveNotice").text("Create");
        $("#noticeEdit").modal("show");
    })

    $("#btnSaveNotice").on("click", function () {

        $("#btnSaveNotice").attr("disable", "true");
        let noticeID = $("#noticeID").val();
        let noticeTitle = $("#nTitle").val();
        let noticeContent = editor.getContent();
        let noticeFile = $("#noticeFileName").val();
        let storeFile = $("#storeFileName").val();
        let startTime = $("#nStart").val();
        let endTime = $("#nEnd").val();

        if (noticeTitle.trim() == "" || noticeContent.trim() == "") {
            Swal.fire("Alert", "Title and content cannot be null!");
            return;
        }
        if (startTime.trim() == "" || endTime.trim() == "") {
            Swal.fire("Alert", "Start time and expire time cannot be null!");
            return;
        }

        let obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "getNoticeDetail";
        let reqData = new Object();
        reqData.noticeID = Number(noticeID);
        reqData.objType = "";
        reqData.objID = 0
        reqData.noticeTitle = noticeTitle;
        reqData.noticeContent = noticeContent;
        reqData.noticeFile = noticeFile;
        reqData.storeFile = storeFile;
        reqData.startTime = startTime;
        reqData.endTime = endTime;
        reqData.noticeStatus = 1;
        obj.reqData = [reqData];

        var url = noticeID > 0 ? "/core/api/DashBoard/setNotice" : "/core/api/DashBoard/addNotice";

        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                $("#btnSaveNotice").removeAttr("disable");
                queryList(1);
                $("#noticeEdit").modal("hide");
            }
            else {
                Swal.fire("Erorr!", "There is something wrong occured, please retry or contact the administrator!");
            }
        })

    })

    $("#ntsFile").change(function () {
        upLoad();
    })
}

function bindListEvent() {

    $("button[name='toDetail']").on("click", function () {
        let noticeID = Number($(this).data("id"));
        getNoticeDetail(noticeID);
        $("#noticeDetailModel").modal("show");
    })

    $("button[name='toEdit']").on("click", function () {
        let noticeID = Number($(this).data("id"));
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "getNoticeDetail";
        obj.reqData = [noticeID.toString()];
        var url = "/core/api/DashBoard/getNoticeDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                $("#noticeID").val(result.resData[0].noticeID);
                $("#nTitle").val(result.resData[0].noticeTitle);
                $("#nStart").val(result.resData[0].startTime == null ? "" : result.resData[0].startTime.substr(0, 10));
                $("#nEnd").val(result.resData[0].endTime == null ? "" : result.resData[0].endTime.substr(0, 10));
                editor.setContent(result.resData[0].noticeContent);
                if (result.resData[0].noticeFile != "" && result.resData[0].noticeFile != null) {
                    $("#nFile").html('Attachment: <a href="#" onclick="downNoticeFile(' + noticeID + ')">' + result.resData[0].noticeFile + '</a>');
                }
                else {
                    $("#nFile").html('');
                }
                $("#btnSaveNotice").text("Save");
            }
            $("#noticeEdit").modal("show");
        })

    })

    $("button[name='toDel']").on("click", function () {
        let noticeID = Number($(this).data("id"));
        Swal.fire({
            type: 'warning', // 弹框类型
            title: 'Close', //标题
            text: "Are you sure to disable this notice?", //显示内容            
            confirmButtonText: 'Confirm',// 确定按钮的 文字
            showCancelButton: true, // 是否显示取消按钮
        }).then((isConfirm) => {
            if (isConfirm.value) {

                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "disableNotice";
                obj.reqData = [noticeID.toString()];
                var url = "/core/api/DashBoard/disableNotice";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        getNoticeList(1);
                    }
                })
            }
        });
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

function upLoad() {
    var formData = new FormData();
    var file = $('#ntsFile').get(0).files[0];
    if (file == undefined || file == "") {
        Swal.fire("Error!", "No file be choosed");
        return;
    }

    formData.append('file', file);
    let uToken = localStorage.getItem("uToken");

    $.ajax({
        url: "/upload/FileSave",
        type: 'POST',
        cache: false,
        data: formData,
        processData: false,
        contentType: false,
        headers: { uToken },
        dataType: "json",
        success: function (res) {
            if (res.status == 1) {
                var imn = res.namelist[0];
                $("#storeFileName").val(imn);
                $("#noticeFileName").val(file.name);
                $("#nFile").html('Attachment: ' + file.name);
            }
            else {
                Swal.fire("Error!", result.message)
            }
        },
        error: function (XmlHttpRequest, textStatus, errorThrown) { },
        complete: function () { }
    });
}