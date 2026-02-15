jQuery(function ($) {
    getTaskList(1);
    bindEvent();
})
function queryList(pageIndex) {
    getTaskList(pageIndex);
}
function getTaskList(pageIndex) {

    if ($("#ons").val() == -1) {
        $("#btnNewProgress").attr("disabled", "true");
    }
    else {
        $("#btnNewProgress").removeAttr("disabled");
    }

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getField";

    var subData = new Object();
    subData.oid = "";
    subData.kw = $("#kw").val();
    subData.ons = $("#ons").val();
    subData.ty = "";
    subData.pageIndex = pageIndex;
    subData.pageListNum = 10;

    obj.reqData = [subData];
    var url = "/core/api/DashBoard/getMyTaskList";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Task Title</th>';
        listHtml += '            <th>Task Content</th>';
        listHtml += '            <th>DeadLine</th>';
        listHtml += '            <th>Progress</th>';
        listHtml += '            <th>Status</th>';
        listHtml += '            <th class="width-50 text-center">Operation</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            var t = result.resData;
            for (var i = 0; i < t.length; i++) {
                listHtml += '<tr data-id="' + result.resData[i]["taskID"] + '">';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + t[i]["taskTitle"] + '</td>';
                listHtml += '    <td>' + t[i]["taskContent"] + '</td>';
                listHtml += '    <td>' + t[i]["taskDeadline"].substr(0, 10) + '</td>';
                listHtml += '    <td id="' + 'value' + t[i]["taskID"] + '">' + t[i]["taskProgress"] + '%</td>';
                listHtml += '    <td>' + (t[i]["taskStatus"] == "1" ? '<span class="badge rounded-pill bg-success">Enabled</span>' : '<span class="badge rounded-pill bg-danger">Disabled</span>') + '</td>';
                listHtml += '    <td>';
                listHtml += '        <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm btnEditTask" title="Edit" ' + (t[i]["taskStatus"] == "1" ? "" : " disabled ") + ' data-id="' + t[i]["taskID"] + '"><i class="fas fa-pencil-alt"></i></button>';
                listHtml += '        <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm btnCloseTask" title="Close" ' + (t[i]["taskStatus"] == "1" ? "" : " disabled ") + ' data-id="' + t[i]["taskID"] + '"><i class="fas fa-trash-alt"></i></button>';
                listHtml += '    </td>';
                listHtml += '</tr>';
                No++;
            }
            listHtml += "</tbody></table>";
            $("#tblRole").html(listHtml);
            var pagination = getPageNumHtml(result.number, subData.pageListNum, pageIndex);
            $("#paginationArea").html(pagination);
            bindTaskListEvent();
            highLightTr();
        }
        else {
            listHtml += "<tr><td colspan='7' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblRole").html(listHtml);
            $("#tblBind").html(`<table id="datatable" class="table table-bordered dt-responsive table-striped w-100">    
            <thead class="table-secondary">
            <tr><th>No.</th><th>Progress</th><th>Content</th><th>Create Time</th><th>Status</th><th class="width-50">Operation</th></tr>
            </thead>
            <tbody><tr><td colspan="8" class="text-center">No Result</td></tr></tbody></table>`);
        }
    })
}
function getProgress() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query getProgress";
    obj.reqData = [$("#tid").val()];
    var url = "/core/api/DashBoard/getProgress";
    httpPost(url, obj, function (result) {
        var tblHtml = "";
        tblHtml += '<table id="datatable" class="table table-bordered dt-responsive table-striped w-100">';
        tblHtml += '    <thead class="table-secondary table-header-fixed">';
        tblHtml += '        <tr>';
        tblHtml += '            <th>No.</th>';
        tblHtml += '            <th>Progress</th>';
        tblHtml += '            <th>Content</th>';
        tblHtml += '            <th>Create Time</th>';
        tblHtml += '            <th>Status</th>';
        tblHtml += '            <th class="width-50">Operation</th>';
        tblHtml += '        </tr>';
        tblHtml += '    </thead>';
        tblHtml += '    <tbody>';
        if (result.status == 1 && result.resData != null) {
            var No = 1;
            for (var i = 0; i < result.resData.length; i++) {
                var dynamicText = "";
                if (result.resData[i]["dynamicOrg"] == "unit") { dynamicText = "The unit which user work in"; }
                if (result.resData[i]["dynamicOrg"] == "dept") { dynamicText = "The department which user work in"; }

                tblHtml += '<tr>';
                tblHtml += '    <td>' + No + '</td>';
                tblHtml += '    <td' + (result.resData[i]["progressStatus"] == "1" ? "" : " style='text-decoration: line-through'") + '>' + result.resData[i]["progressValue"] + '%</td>';
                tblHtml += '    <td' + (result.resData[i]["progressStatus"] == "1" ? "" : " style='text-decoration: line-through'") + '>' + result.resData[i]["progressContent"] + '</td>';
                tblHtml += '    <td>' + result.resData[i]["createTime"].substr(0, 10) + '</td>';
                tblHtml += '    <td>' + (result.resData[i]["progressStatus"] == "1" ? '<span class="badge rounded-pill bg-success">Enabled</span>' : '<span class="badge rounded-pill bg-danger">Disabled</span>') + '</td>';
                tblHtml += '    <td><button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm closeProgress" data-id="' + result.resData[i]["progressID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button> </td>';
                tblHtml += '</tr>';
                No++;
            }
            tblHtml += '</tbody></table>';
            $("#tblBind").html(tblHtml);


        }
        else {
            tblHtml += "<tr><td colspan='8' class='text-center'>No Result</td></tr></tbody></table>";
            $("#tblBind").html(tblHtml);
        }
        bindPregressListEvent();
    })
}
function highLightTr() {
    $('#dt1 tr').click(function () {
        var tid = $(this).attr("data-id");
        $('#dt1 tr').removeClass('highlight');
        $(this).addClass('highlight');
        $("#tid").val(tid);
        if (tid != "" && tid != undefined) { getProgress(); }
    });
    $("#dt1 tr:first-child").click();
}
function bindEvent() {

    $("#btnNewTask").on("click", function () {
        $("#btnSaveTask").text("Create");
        $("#addTaskModal").modal("show");
    })
    $('#btnOpenAddModal').on('click', function () {
        $('#addTaskModal').modal('show');
    });
    $("#btnSearch").on("click", function () {
        getTaskList(1);
    })

    $("#btnSaveTask").on("click", function () {
        var taskTitle = $("#taskTitle").val();
        var taskContent = $("#taskContent").val();
        var taskDeadline = $("#taskDeadline").val();
        if (taskTitle.trim() == "" || taskTitle == null || taskContent.trim() == "" || taskContent == null || taskDeadline.trim() == "" || taskDeadline == null) {
            Swal.fire("Error", "The task title and task content and task deadline cannot be null!");
        }
        else {
            if ($("#btnSaveTask").text() == "Create") {
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
                        getTaskList(1);
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
            if ($("#btnSaveTask").text() == "Save") {
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "add task";
                var reqData = new Object();
                reqData.taskID = $("#tid").val();
                reqData.taskTitle = taskTitle;
                reqData.taskContent = taskContent;
                reqData.taskDeadline = taskDeadline;
                reqData.taskStatus = 1;
                obj.reqData = [reqData]
                var url = "/core/api/DashBoard/setTask";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        localStorage.setItem("uToken", result.uToken);
                        getTaskList(1);
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

        }

    })
    $("#btnNewProgress").on("click", function () {
        $("#addProgressModal").modal("show");
    })
    $("#btnAddProgress").on("click", function () {

        var taskID = $("#tid").val();
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
            $("#addProgressModal").modal("hide");
            $("#value" + taskID).html(progressValue + "%");
            $("#progressNote").val("");
            $("#progressValue").val(0);
            getProgress();
        }
        else {
            Swal.fire("Erorr!", result.message)
        }
    })
}
function bindTaskListEvent() {
    $('.btnCloseTask').on('click', function () {
        var taskID = $(this).attr("data-id");
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
                obj.reqData = [taskID.toString()];
                var url = "/core/api/DashBoard/closeTask";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success", "The task has been closed!")
                            .then(() => {
                                getTaskList(1);
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

    $(".btnEditTask").on("click", function () {
        let taskID = Number($(this).data("id"));
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "getTaskDetail";
        obj.reqData = [taskID.toString()];
        var url = "/core/api/DashBoard/getTaskDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {

                $("#progressNote").val("");
                var t = result.resData[0];
                $('#taskTitle').val(t.taskTitle);
                $('#taskContent').val(t.taskContent || '-');
                $('#taskDeadline').val(t.taskDeadline.substr(0, 10) || '');
                $("#btnSaveTask").text("Save");
                $("#addTaskModal").modal("show");
            }
        });
    })
}
function bindPregressListEvent() {
    $(".closeProgress").on("click", function () {
        let progressID = Number($(this).data("id"));
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
                        getProgress();
                    }
                    else {
                        Swal.fire("Erorr", "There is something wrong occured, please retry or contact the administrator!");
                    }
                })
            }
        });
    })
}