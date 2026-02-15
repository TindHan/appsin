jQuery(function ($) {
    getNumbers();
    getMyTasks();
    getMyNotices();
    getMyMessages();
    bindEvents();
});

function getNumbers() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMyTasks";
    obj.reqData = [""];
    var url = "/core/api/DashBoard/getNumbers";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#number1").attr("data-target", result.number1);
            $("#number2").attr("data-target", result.number2);
            $("#number3").attr("data-target", result.number3);
            $("#number4").attr("data-target", result.number4);
            $("#number1").html(result.number1 == null ? "No valid data" : result.number1);
            $("#number2").html(result.number2);
            $("#number3").html(result.number3);
            $("#number4").html(result.number4);
        }
    })
}
function getMyNotices() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMyTasks";
    var reqData = new Object();
    reqData.oid = "";
    reqData.kw = "";
    reqData.ons = "";
    reqData.ty = "";
    reqData.pageIndex = 1;
    reqData.pageListNum = 5;
    obj.reqData = [reqData];

    var url = "/core/api/DashBoard/getMyNotices";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var notices = result.resData;
            if (notices != null && notices.length > 0) {
                $("#noticeArea").html("");
                notices.forEach((t,i) => {
                    $("#noticeArea").append(`<li class="hover-bg-color">
                                                <div class="row ${i == 4 ? "" : "border-bottom"}  ">
                                                    <div class="col-9 pt-3 pb-2_5">
                                                        <h6 class="text-truncate font-size-14">${t.noticeTitle}</h6>
                                                        <span>${t.createTime.substr(0, 10)}</span>
                                                    </div>
                                                    <div class="col-3 p-4 d-flex justify-content-end">`
                        + (t.readCount == 0 ? `<span class="badge bg-warning rounded-pill pt-1">unread</span>
                                                       &nbsp;&nbsp;`: ``)
                        + `<a href="#" onclick="getNoticeDetail(${t.noticeID})">Detail</a>
                                                                                </div>
                                                                            </div>
                                                                        </li>`);

                })
            }
        }
    })
}
function getMyMessages() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMyMessages";
    var reqData = new Object();
    reqData.oid = "";
    reqData.kw = "";
    reqData.ons = "";
    reqData.ty = "";
    reqData.pageIndex = 1;
    reqData.pageListNum = 5;
    obj.reqData = [reqData];

    var url = "/core/api/DashBoard/getMyMessages";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var msgs = result.resData;
            if (msgs != null && msgs.length > 0) {
                $("#msgArea").html("");
                msgs.forEach((t, i) => {
                    $("#msgArea").append(`
                                     <li>
                                        <div class="d-flex align-items-start p-3 ${i == 4 ? "" : "border-bottom"}  hover-bg-color">
                                            <div class="flex-shrink-0 user-img online align-self-center me-3">
                                                <div class="avatar-sm align-self-center">
                                                    <span class="avatar-title rounded-circle  bg-primary-subtle text-primary">
                                                        ${t.appName.substr(0, 1)}
                                                    </span>
                                                </div>
                                                <span class="user-status"></span>
                                            </div>

                                            <div class="flex-grow-1 overflow-hidden ml-5">
                                                <h5 class="text-truncate font-size-14 mb-1">${t.msgTitle}</h5>
                                                <p class="text-truncate mb-0">${t.appName} &nbsp; ${t.createTime.substr(0, 16).replace('T', ' ')}</p>
                                            </div>
                                            <div class="flex-shrink-0">
                                                <div class="d-flex justify-content-end pt-2">`
                        + (t.readCount == 0 ? `     <span class="badge bg-warning rounded-pill">unread</span>&nbsp;&nbsp;` : ``)
                        + `                          <a href="#" onclick="getMsgDetail(${t.msgID})">Detail</a>
                                                </div>
                                            </div>
                                        </div>
                                    </li>
                    `);
                })
            }
        }
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
            $("#objUrl").css("display", "none");
            $("#msgntsModel").modal("show");
            readCount(noticeID, "notice");
        }

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
            $("#objFile").css("display", "none");
            $("#msgntsModel").modal("show");
            readCount(msgID, "message");
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
        if (result.status == 1 && type == "notice") {
            getMyNotices();
        }
        if (result.status == 1 && type == "message") {
            getMyMessages();
        }
    })
}
function getMyTasks() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMyTasks";
    obj.reqData = [""];
    var url = "/core/api/DashBoard/getMyTasks";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var tasks = result.resData;
            if (tasks == null) {
                $("#taskList").html(`<div class="list-group-item d-flex justify-content-between align-items-center border-bottom">
                                          <div class="w-100">
                                          <p class="text-center">No tasks</p>
                                          </div>
                                      </div>
                `);
            }
            else {
                $("#taskList").html("");
                tasks.forEach(t => {
                    const badgeCls = { pending: 'warning', completed: 'success', closed: 'secondary' }[t.taskStatus];
                    const badgeText = { pending: 'Pending', completed: 'Completed', closed: 'Closed' }[t.taskStatus];
                    $("#taskList").append(`
                                          <div class="list-group-item d-flex justify-content-between align-items-center border-bottom hover-bg-color">
                                            <div class="w-100">
                                              <div class="fw-bold"><i class="fas fa-tasks"></i>  ${t.taskTitle}</div>
                                              <div class="d-flex justify-content-between align-items-center mt-1">
                                                <small class="text-muted">Deadline: ${t.taskDeadline.substr(0, 10) || 'No Deadline'}</small>
                                                <div class="d-flex align-items-center gap-2" style="width:50%;">
                                                  <div class="progress list-progress w-100">
                                                    <div class="progress-bar" style="width:${t.taskProgress}%">${t.taskProgress}%</div>
                                                  </div>
                                                  <span class="badge bg-${badgeCls} rounded-pill">${badgeText}</span>
                                                </div>
                                              </div>
                                            </div>
                                            <button class="btn btn-sm btn-outline-primary btnDetail ms-2" data-id="${t.taskID}">Detail</button>
                                          </div>
                                        `);
                });
            }

        }
        else {
            Swal.fire("Error!", "There is something wrong occured,please retry or contact administrator!");
        }
        bindListEvents();
    })
}
function bindListEvents() {
    $(".btnDetail").on('click', function () {
        var taskID = Number($(this).data('id'));

        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "getTaskDetail";
        obj.reqData = [taskID.toString()];
        var url = "/core/api/DashBoard/getTaskDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {

                $("#progressNote").val("");

                var t = result.resData[0];
                $('#detailTitle').text(t.taskTitle);
                $('#detailDesc').text(t.taskContent || '-');
                $('#detailStatus').text({ pending: 'Pending', completed: 'Completed', closed: 'Closed' }[t.taskStatus]);
                $('#detailProgressBar').css('width', t.taskProgress + '%').text(t.taskProgress + '%');
                $('#progressValue').val(t.taskProgress);
                $('#detailDeadline').html(t.taskDeadline.substr(0, 10) || '');

                $('#btnSaveDetail').toggle(t.status !== 'completed' && t.status !== 'closed');
                $('#btnSaveDetail').attr("data-id", taskID);
                $("#btnAddProgress").attr("data-id", taskID);
                $("#taskID").val(taskID);
                $("#taskPro").val(t.taskProgress);
                $('#btnCloseTaskText').toggle(t.status !== 'completed' && t.status !== 'closed');


                $('#detailModal').modal("show");

                getProgress(taskID);
            }
        });
    });
}
function getProgress(taskID) {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getProgress";
    obj.reqData = [taskID.toString()];
    var url = "/core/api/DashBoard/getProgress";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var t = result.resData;
            $("#progressHistory").html("");
            if (t != null && t.length > 0) {
                let count = 0;
                t.forEach(t => {
                    $("#progressHistory").append(`<li class="list-group-item d-flex justify-content-between align-items-center">`
                        + `<span ` + (t.progressStatus == -1 ? `style="text-decoration: line-through"` : ``) + ` >${t.progressValue}% (${t.createTime.substr(0, 10)})<br /> ${t.progressContent} </span>`
                        + ((count == 0 || t.progressStatus == -1) ? `` : `<a href="#" onclick="cancelProgress(${t.progressID},${taskID})" data-index="${t.progressID}"><span class="font-size-12">Cancel</p></a>`)
                        + `</li>`);
                    count++;
                });
            }

        }
    });

}
function cancelProgress(progressID, taskID) {

    Swal.fire({
        type: 'warning', // 弹框类型
        title: 'Cancel', //标题
        text: "You are going to cancel this progress, are you sure?", //显示内容            
        confirmButtonText: 'Confirm',// 确定按钮的 文字
        showCancelButton: true, // 是否显示取消按钮
    }).then((isConfirm) => {
        if (isConfirm.value) {
            var obj = new Object();
            obj.uToken = localStorage.getItem("uToken");
            obj.action = "cancelProgress";
            obj.reqData = [progressID.toString()];
            var url = "/core/api/DashBoard/setProgress";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    getProgress(taskID);
                }
                else {
                    Swal.fire("Erorr", "There is something wrong occured, please retry or contact the administrator!");
                }
            })
        }
    });
}
function addProgress(taskID, progressValue, progressNote) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "addProgress";
    var reqData = new Object();
    reqData.taskID = taskID;
    reqData.progressID = 0;
    reqData.progressValue = Number(progressValue);
    reqData.progressContent = progressNote;
    reqData.progressStatus = 1;
    obj.reqData = [reqData]
    var url = "/core/api/DashBoard/addProgress";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $('#detailProgressBar').css('width', progressValue + '%').text(progressValue + '%');
            $("#taskPro").val(progressValue);
            getProgress(taskID);
            getMyTasks();
            getNumbers();
        }
        else {
            Swal.fire("Erorr!", result.message)
        }
    })
}
function bindEvents() {
    $('#btnOpenAddModal').on('click', function () {
        $('#addTaskModal').modal('show');
    });

    $('#btnCloseTaskText').on('click', () => {
        Swal.fire({
            type: 'warning', // 弹框类型
            title: 'Close', //标题
            text: "You are going to close this task, are you sure?", //显示内容            
            confirmButtonText: 'Confirm',// 确定按钮的 文字
            showCancelButton: true, // 是否显示取消按钮

        }).then((isConfirm) => {
            if (isConfirm.value) {

                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "cancelProgress";
                obj.reqData = [$("#taskID").val()];
                var url = "/core/api/DashBoard/closeTask";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success", "The task has been closed!")
                            .then(() => {
                                getMyTasks();
                                $("#detailModal").modal("hide")
                            });
                    }
                    else {
                        Swal.fire("Erorr", "There is something wrong occured, please retry or contact the administrator!");
                    }
                })
            }
        });
    });

    $("#btnAddProgress").on("click", function () {
        var taskID = Number($(this).data('id'));

        var progressNote = $("#progressNote").val();
        var progressValue = $("#progressValue").val();

        if (progressNote.trim() == "" || progressValue.trim() == "") {
            Swal.fire("Error", "the progress note and progress value cannot be null");
        }
        else {
            if (progressValue == 0) {
                Swal.fire({
                    type: 'warning', // 弹框类型
                    title: 'Close', //标题
                    text: "The task progress is 0%, are you sure to submit?", //显示内容            
                    confirmButtonText: 'Confirm',// 确定按钮的 文字
                    showCancelButton: true, // 是否显示取消按钮

                }).then((isConfirm) => {
                    if (isConfirm.value) {
                        addProgress(taskID, progressValue, progressNote);
                    }
                });
            }
            else {
                addProgress(taskID, progressValue, progressNote);
            }
        }

    })

    $("#btnSaveTask").on("click", function () {
        var taskTitle = $("#taskTitle").val();
        var taskContent = $("#taskContent").val();
        var taskDeadline = $("#taskDeadline").val();
        if (taskTitle.trim() == "" || taskTitle == null || taskContent.trim() == "" || taskContent == null || taskDeadline.trim() == "" || taskDeadline == null) {
            Swal.fire("Error", "The task title and task content and task deadline cannot be null!");
        }
        else {
            var obj = new Object();
            obj.uToken = localStorage.getItem("uToken");
            obj.action = "add task";
            var reqData = new Object();
            reqData.taskID = 0;
            reqData.taskTitle = taskTitle;
            reqData.taskContent = taskContent;
            reqData.taskDeadline = taskDeadline;
            reqData.taskStatus = 1;
            obj.reqData = [reqData]
            var url = "/core/api/DashBoard/addTask";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    localStorage.setItem("uToken", result.uToken);
                    getMyTasks();
                    $('#addTaskModal').modal('hide');

                    $("#taskTitle").val("");
                    $("#taskContent").val("");
                    $("#taskDeadline").val("");
                }
                else {
                    Swal.fire("Error!", "There is something wrong occured,please retry or contact administrator!");
                }
            })
        }

    })

    $("#closeDetail").on("click", function () {
        getMyTasks();
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
function gotoTask(msgID) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMsgUrl";
    obj.reqData = [msgID.toString()];
    var url = "/core/api/DashBoard/getMsgUrl";
    httpPost(url, obj, function (result) {
        if (result.status == 1 && result.resData.length > 0) {
            if (result.resData[1] == "FlowNode") {
                window.location.href = "myApprove.html?nid=" + result.resData[2];
            }
            else {
                window.location.href = result.resData[0];
            }
        }
        else {
            Swal.fire("Alert!","There is something wrong occured, please retry or contact administrator!","error")
        }

    })
}