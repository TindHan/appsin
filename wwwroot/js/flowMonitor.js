jQuery(function ($) {
    getTemplate();
    bindEvent();
    bindApproverModal();
    I18n.init();
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
                    apprHtml += `<li class='mt-3'>${item.psnName} from the ${item.deptName} recieved the message at ${item.createTime.substr(0, 16).replace('T', ' ')}, ${item.readCount > 0 ? 'He/She has already read the message.' : 'He/She is not read the message yet.  '} </li>`;
                    apprHtml += `<a data-id="` + item.msgID + `" id="changeBtn" class="aClickable">Change</a>&nbsp;&nbsp;&nbsp;<a id="removeBtn" data-id="` + item.msgID + `" class="aClickable">Remove</a>`
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
            return '<span class="badge bg-info">Ongoing</span';
            break;
        case -1:
            return '<span class="badge bg-danger">Rejected</span>';
            break;
    }
}


function showSearch() {
    // ensure modal element has stack class
    const modalEl = document.getElementById('editApproverModal');
    if (!modalEl) return;
    modalEl.classList.add('add-approver-stack');

    // prefer Bootstrap 5 Modal API if available
    try {
        if (typeof bootstrap !== 'undefined' && bootstrap.Modal) {
            const bsModal = new bootstrap.Modal(modalEl, { backdrop: 'static', keyboard: false });
            bsModal.show();
            // adjust backdrop z-index and modal z-index to ensure modal sits above backdrop and underlying modal
            setTimeout(() => {
                const backs = document.querySelectorAll('.modal-backdrop');
                if (backs.length) {
                    const lastBack = backs[backs.length - 1];
                    lastBack.classList.add('add-approver-backdrop');
                    lastBack.style.zIndex = '1060';
                }
                // ensure modal z-index higher than its backdrop and enable pointer events
                modalEl.style.zIndex = '1065';
                const dialog = modalEl.querySelector('.modal-dialog');
                if (dialog) dialog.style.pointerEvents = 'auto';
            }, 50);
            return;
        }
    } catch (ex) {
        console.warn('bootstrap.Modal show failed, fallback to jQuery modal', ex);
    }

    // fallback to jQuery plugin
    $('#editApproverModal').modal({ backdrop: 'static', keyboard: false, show: true });
    setTimeout(function () {
        const last = $('.modal-backdrop').last();
        last.addClass('add-approver-backdrop');
        last.css('z-index', '1060');
        $('#editApproverModal').css('z-index', '1065');
        $('#editApproverModal .modal-dialog').css('pointer-events', 'auto');
    }, 50);
}

// New: bindApproverModal to update selected area when selecting radio and confirm
function bindApproverModal() {
    // show addApproverModal on click without hiding underlying modal
    $(document).on('click', '#btnAddApprover', function (e) {
        e.preventDefault();
        $("#opType").val("add");
        showSearch();
    });

    $(document).on('click', '#changeBtn', function (e) {
        e.preventDefault();
        $("#opType").val("change");
        $("#msgID").val(e.currentTarget.dataset.id);
        showSearch();
    });

    $(document).on('click', '#removeBtn', function (e) {
        e.preventDefault();
        let msgID = Number($(this).data('id'));
        Swal.fire({
            title: "Are you sure?",
            text: "You are going to remove this approver,you won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, remove it!"
        }).then(function (e) {
            if (e.isConfirmed == true) {
                var obj = new Object();
                obj.uToken = localStorage.getItem('uToken');
                obj.action = 'remove';
                obj.reqData = [msgID.toString()];
                var url = '/core/api/Flows/removeApprover';
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success", "The approver has been removed!").then(function () {
                            $("#editApproverModal").modal("hide");
                            $("#myModal").modal("hide");
                        }); 
                    }
                    else {
                        Swal.fire("Error", result.message); 
                    }
                })
            }
        })
        
    });

    // Search button: call backend PsnsController.getSearch
    $(document).on('click', '#approverSearchBtn', function (e) {
        e.preventDefault();
        const kw = $('#approverSearchInput').val() ? $('#approverSearchInput').val().trim() : '';
        if (!kw) {
            Swal.fire('warning', 'Please input a keyword (name or employee code)');
            return;
        }
        var obj = new Object();
        obj.uToken = localStorage.getItem('uToken');
        obj.action = 'getSearch';
        obj.reqData = [kw];
        var url = '/core/api/Psns/getSearch';
        httpPost(url, obj, function (result) {
            let tbodyHtml = '';
            if (result.status == 1 && result.resData && result.resData.length > 0) {
                result.resData.forEach(function (p) {
                    tbodyHtml += `<tr>` +
                        `<td><input type="radio" name="approverSelect" value="${p.psnID}"></td>` +
                        `<td>${p.psnName}</td>` +
                        `<td>${p.psnCode || ''}</td>` +
                        `<td>${p.unitName || ''}</td>` +
                        `<td>${p.deptName || ''}</td>` +
                        `<td>${p.psnSex || ''}</td>` +
                        `</tr>`;
                });
            } else {
                tbodyHtml = `<tr><td colspan="4" class="text-center">No results</td></tr>`;
            }
            // replace tbody in the modal table
            $('#editApproverModal').find('table tbody').html(tbodyHtml);
        });
    });

    // ensure custom backdrop class removed when modal hidden by other means
    $(document).on('hidden.bs.modal', '#editApproverModal', function () {
        $('.modal-backdrop.add-approver-backdrop').remove();
    });

    $("#approverConfirmBtn").on("click", function () {
        let opType = $("#opType").val();
        if (opType == "add") {

        }
        else if (opType == "change") {
            var msgID = $("#msgID").val();
            var selectedPsnID = $('input[name="approverSelect"]:checked').val();;

            if (!msgID) {
                Swal.fire("Error","No correct ID (msgID)");
                return;
            }

            if (!selectedPsnID) {
                Swal.fire("Error", "Select one person first");
                return;
            }


            var obj = new Object();
            obj.uToken = localStorage.getItem('uToken');
            obj.action = 'getSearch';
            obj.reqData = [msgID.toString(), selectedPsnID.toString()]
            var url = '/core/api/Flows/changeApprover';
            httpPost(url, obj, function (result) {
                if (result.status === 1) {
                    Swal.fire("Success", "The approver has been changed successfully!").then(function () {
                        $("#editApproverModal").modal("hide");
                        $("#myModal").modal("hide");
                    })
                } else {
                    Swal.fire("Error", result.message); 
                }
            });
        }
    })
}

// Wrapper to ensure SweetAlert appears above custom modals
function showSwal() {
    // call original Swal.fire with arguments
    const res = Swal.fire.apply(Swal, arguments);
    setTimeout(() => {
        const el = document.querySelector('.swal2-container');
        if (el) {
            // ensure container is direct child of body so z-index positioning is global
            try { document.body.appendChild(el); } catch (e) { }
            // make swal topmost
            el.style.zIndex = '30000';
            const popup = el.querySelector('.swal2-popup');
            if (popup) popup.style.zIndex = '30001';

            // lower all modal backdrops so they don't cover swal
            const backs = document.querySelectorAll('.modal-backdrop');
            backs.forEach((b) => {
                // mark modified backdrops so we can restore later if needed
                b.classList.add('swal-lowered-backdrop');
                b.dataset._oldZ = b.style.zIndex || '';
                b.style.zIndex = '20000';
                // also allow pointer events to pass through
                b.style.pointerEvents = 'none';
            });

            // bring any open modals under the swal by lowering their z-index
            const modals = document.querySelectorAll('.modal.show');
            modals.forEach((m) => {
                m.dataset._oldZ = m.style.zIndex || '';
                m.style.zIndex = '25000';
                // ensure modal dialog still interactive if needed
                const dlg = m.querySelector('.modal-dialog');
                if (dlg) dlg.style.pointerEvents = 'auto';
            });
        }
    }, 0);

    // when swal closes, restore backdrops/modal z-index
    res.then(() => {
        setTimeout(() => {
            const backs = document.querySelectorAll('.modal-backdrop.swal-lowered-backdrop');
            backs.forEach((b) => {
                b.style.pointerEvents = '';
                b.style.zIndex = b.dataset._oldZ || '';
                b.classList.remove('swal-lowered-backdrop');
                delete b.dataset._oldZ;
            });
            const modals = document.querySelectorAll('.modal');
            modals.forEach((m) => {
                if (m.dataset._oldZ !== undefined) {
                    m.style.zIndex = m.dataset._oldZ || '';
                    delete m.dataset._oldZ;
                }
            });
        }, 50);
    });

    return res;
}