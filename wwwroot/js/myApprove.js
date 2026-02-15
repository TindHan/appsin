jQuery(function ($) {
    getFlowInstance();
    bindEvents();
})
function getFlowInstance() {
    let nodeID = request("nid");
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getInstance";
    obj.reqData = [nodeID];
    var url = "/core/api/Flows/getInstance";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {

            let t = result.resData[0];
            if (t.instanceStatus == -1) {
                Swal.fire("Error", "This flow has been closed by administrator!", "error").then(function () {
                    history.back();
                });
            }
            else {
                $("#isClosedAlert").html("");

                $("#instanceTitle").html(t.instanceName);
                contentUrl(t.appID, t.domain , t.contentUrl);
                $("#flowStart").html(t.createPsn + " create this application at " + t.createTime.substr(0, 10));
                $("#btnView").attr("data-id", t.instanceID);
                $("#btnSubmit").attr("data-id", nodeID);

                if (t.isEnd == 1) {
                    $("#isEndAlert").html(t.isPass == 1 ? `<span class="badge bg-success">Passed</span>  This approve flow has passed.` : `<span class="badge bg-danger">Rejected</span>  This approve flow has rejected.`)
                    $("#approveAlert").css(`display`, `block`);
                }
                else {
                    $("#isEndAlert").html("");
                }

                getApproveLog(t.instanceID)
            }

        }
        else {
            Swal.fire("Error!", result.message);
        }
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
            let nodeID = request("nid");
            if (result.resData != null && result.resData.length > 0) {
                result.resData.forEach((item) => {
                    if (item.approverName !== null && item.approverName != "") {
                        apprHtml += `<li class='mb-2'>${item.approverName} from the ${item.approverDept} ${item.isAgree == 1 ? 'agreed' : 'disagreed'} this application on ${item.approveTime.substr(0, 10)}.
                             His/Her opinon: ${item.isNote}</li>`;
                    }
                    else {
                        apprHtml += `<li class='mb-2'>${item.isNote} on ${item.approveTime.substr(0, 10)}</li>`;
                    }
                    if (item.nodeID == nodeID) {
                        $("#isApprAlert").html(`The approve task has been processed by ${item.approverName} on ${item.approveTime.substr(0, 10)}.`);
                        $("#approveAlert").css("display", "block");
                    }
                })
                $("#approveLog").html(apprHtml);
            }
            else {
                $("#isApprAlert").html("");
            }
            if ($("#isApprAlert").html() == "" && $("#isEndAlert").html() == "") {
                $("#approveOp").css("display", "block");
            }
        }
        else {
            Swal.fire("Error!", result.message);
        }
    })
}
function bindEvents() {
    $("#btnView").on("click", function () {
        let instanceID = Number(this.dataset.id);
        $("#fview").attr("src", "flowView.html?iid=" + instanceID);
        $("#viewModel").modal("show");
    })

    $("#btnSubmit").on("click", function () {
        let nodeID = this.dataset.id;
        let approveResult = $('input[name="radioApprove"]:checked').attr('id'); // radioYes 或 radioNo
        if (!approveResult) {
            Swal.fire('Error', 'Please select Agree or Disagree!');
            return;
        }
        let opinion = (approveResult === 'radioYes') ? 'agree' : 'disagree';
        let reason = $('#isNote').val().trim();
        Swal.fire({
            title: "Are you sure?",
            text: "You are going to " + opinion + " this application,you won't be able to revert this!",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, Submit it!"
        }).then(function (e) {
            if (e.isConfirmed == true) {
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "submitApprove";
                obj.reqData = [nodeID, opinion, reason];
                var url = "/core/api/Flows/submitApprove";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        $("#approveOp").css("display", "none");
                        getFlowInstance();
                    }
                    else {
                        Swal.fire("Error!", result.message);
                    }
                })
            }
        })
    })
}

function contentUrl(appID,domain, curl) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query menu";
    obj.reqData = [appID.toString()];
    var url = "/core/api/Flows/genKey";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#contentUrl").attr("src", domain + curl + "&k=" + result.resData[0]);
        }
        else {
            Swal.fire("Error!", "There is something wrong occured,logout failed,please retry or contact administrator!", "error");
        }
    })


}