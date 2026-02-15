jQuery(function ($) {
    getTemplate();
    bindEvent();
});

function getTemplate() {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getTemplate";

    var subData = new Object();
    subData.oid = "";
    subData.kw = "";
    subData.ons = "1";
    subData.ty = "";
    subData.pageIndex = 1;
    subData.pageListNum = 999999;

    obj.reqData = [subData];
    var url = "/core/api/Flows/tempList";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            let listHtml = '<option value="">All</option>';
            result.resData.forEach((t) => {
                listHtml += '<option value="' + t.templateID + '">' + t.templateName + '</option>';
            })
            $("#template").html(listHtml);
        }
        getInstance(1);
    })
}

function queryList(pageIndex) {
    getInstance(pageIndex);
}
function getInstance(pageIndex) {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getMonitorList";

    var subData = new Object();
    subData.oid = "";
    subData.kw = "";
    subData.ons = $("#status").val();
    subData.ty = $("#template").val();
    subData.pageIndex = pageIndex;
    subData.pageListNum = 20;

    obj.reqData = [subData];
    var url = "/core/api/Flows/getMonitorList";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Application Name</th>';
        listHtml += '            <th>Instance Name</th>';
        listHtml += '            <th>Template Name</th>';
        listHtml += '            <th>Applicant</th>';
        listHtml += '            <th>Status</th>';
        listHtml += '            <th>Curret Done Node</th>';
        listHtml += '            <th>Error</th>';
        listHtml += '            <th>Create Time</th>';
        listHtml += '            <th>Operation</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            result.resData.forEach((t) => {
                listHtml += '<tr>';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + t["appName"] + '</td>';
                listHtml += '    <td>' + t["instanceName"] + '</td>';
                listHtml += '    <td>' + t["templateName"] + '</td>';
                listHtml += '    <td>' + t["psnName"] + '</td>';
                listHtml += '    <td>' + (t["isEnd"] == 1 ? (t["isPass"] == 1 ? bandge(1) : bandge(-1)) : bandge(0)) + '</td>';
                listHtml += '    <td>' + t["doneNodeName"] + '</td>';
                listHtml += '    <td>' + (t.isError == 1 ? '<span style="color:red">Yes <i title="' + t.errorDesc + '" class="fas fa-info-circle"></i></span>' : 'No Error') + '</td>';
                listHtml += '    <td>' + t["createTime"].substr(0, 10) + '</td>';
                listHtml += '    <td>';
                listHtml += '        <div class="text-center">';
                listHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toDetail" data-id="' + t["instanceID"] + '" title="Detail"><i class="fas fa-align-justify"></i></button>';
                listHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toView" data-id="' + t["instanceID"] + '" title="ViewFlow"><i class="fas fa-random"></i></button>';
                listHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toDisable" data-id="' + t["instanceID"] + '" title="Disable"><i class="fas fa-trash-alt"></i></button>';
                listHtml += '        </div>';
                listHtml += '    </td>';
                listHtml += '</tr>';
                No++;
            })
            listHtml += "</tbody></table>";
            $("#tblArea").html(listHtml);
            var pagination = getPageNumHtml(result.number, subData.pageListNum, pageIndex);
            $("#paginationArea").html(pagination);
            bindTableEvent();
        }
        else {
            listHtml += "<tr><td colspan='9' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblArea").html(listHtml);
        }

    })
}

function bindEvent() {
    $("#btnSearch").on("click", function () {
        getInstance(1);
    })

    $("#toClose").on("click", function () {
        $("#myModal").modal("hide");
    })
}

function bindTableEvent() {
    $('button[name="toDetail"]').on("click", function () {
        let tid = Number(this.dataset.id);
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "instanceDetail";
        obj.reqData = [tid.toString()];
        var url = "/core/api/Flows/instDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                var t = result.resData[0];
                $("#instanceName").text(t.instanceName);
                $("#instancePK").text(t.instancePK);
                $("#instanceDesc").text(t.instanceDesc);
                $("#appName").text(t.appName);
                $("#domain").text(t.domain);
                $("#contentUrl").text(t.contentUrl);
                $("#templateName").text(t.templateName);
                $("#createPsnName").text(t.createPsnName);
                $("#createPsnPK").text(t.createPsnPK);
                $("#psnName").text(t.psnName);
                $("#psnPK").text(t.psnPK);
                $("#createTime").text(t.createTime.substr(0, 16).replace('T', ' '));
                $("#doneNodeName").text(t.doneNodeName);
                $("#isEnd").text(t.isEnd == 1 ? "Yes" : "No");
                $("#isPass").text(t.isPass == 1 ? "Yes" : "No");

                getApproveLog(tid);
                getNextApprover(tid);

                $("#myModal").modal("show");
            }
            else {
                Swal.fire("Error", "Query field set failed,please retry or contact administrator!");
            }
        })
        $("#myModal").modal("show");
    })

    $('button[name="toDisable"]').on("click", function () {

        let iid = Number(this.dataset.id);

        Swal.fire({
            title: "Are you sure?",
            text: "You are going to delete this item,you won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, delete it!"
        }).then(function (e) {
            if (e.isConfirmed == true) {
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "setInstance";
                obj.reqData = [iid.toString()];
                var url = "/core/api/Flows/setInstance";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        getInstance(1);
                    }
                    else {
                        Swal.fire("Error", "Failed,please retry or contact administrator!");
                    }
                })
            }
        })
    })

    $('button[name="toView"]').on("click", function () {
        let instanceID = Number(this.dataset.id);
        $("#fview").attr("src", "../users/flowView.html?iid=" + instanceID);
        $("#viewModel").modal("show");
    })
}

function getApproveLog(instanceID) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getApproveLog";
    obj.reqData = [instanceID.toString()];
    var url = "/core/api/Flows/getApproveLog";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            let apprHtml = "";
            if (result.resData != null && result.resData.length > 0) {
                result.resData.forEach((item) => {
                    if (item.approverName !== null && item.approverName != "") {
                        apprHtml += `<li class='mb-2'>${item.approverName} from the ${item.approverDept} ${item.isAgree == 1 ? 'agreed' : 'disagreed'} this application on ${item.approveTime.substr(0, 10)}.
                             His/Her opinon: ${item.isNote}</li>`;
                    }
                    else {
                        apprHtml += `<li class='mb-2'>${item.isNote} on ${item.approveTime.substr(0, 10)}</li>`;
                    }
                })
                $("#apprHistory").html(apprHtml);
            }
            else {
                $("#apprHistory").html("<li class='mb-2'>There is no approve history yet.</li>");
            }
        }
        else {
            Swal.fire("Error!", result.message);
        }
    })
}

function getNextApprover(instanceID) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getNextApprover";
    obj.reqData = [instanceID.toString()];
    var url = "/core/api/Flows/getNextApprover";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            let apprHtml = "";
            if (result.resData != null && result.resData.length > 0) {
                result.resData.forEach((item) => {
                    apprHtml += `<li class='mb-2'>${item.psnName} from the ${item.deptName} recieved the message at ${item.createTime.substr(0, 16).replace('T', ' ')}, ${item.readCount > 0 ? 'He/She has already read the message.' : 'He/She is not read the message yet.'} </li>`;
                })
                $("#nextApprover").html(apprHtml);
            }
            else {
                $("#nextApprover").html("<li class='mb-2'>There is no next approver.</li>");
            }
        }
        else {
            Swal.fire("Error!", result.message);
        }
    })
}
function bandge(status) {
    switch (status) {
        case 1:
            return '<span class="badge bg-success">Approved</span>';
            break;
        case 0:
            return '<span class="badge bg-info">Ongoing</span>';
            break;
        case -1:
            return '<span class="badge bg-danger">Rejected</span>';
            break;
    }
}