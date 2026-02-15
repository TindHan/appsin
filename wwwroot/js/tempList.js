jQuery(function ($) {
    getTemplate(1);
    bindEvent();
});

function queryList(pageIndex) {
    getTemplate(pageIndex);
}
function getTemplate(pageIndex) {

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getTemplate";

    var subData = new Object();
    subData.oid = "";
    subData.kw = $("#kw").val();
    subData.ons = $("#status").val();
    subData.ty = "";
    subData.pageIndex = pageIndex;
    subData.pageListNum = 20;

    obj.reqData = [subData];
    var url = "/core/api/Flows/tempList";
    httpPost(url, obj, function (result) {
        var No = 1;
        var listHtml = "";
        listHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
        listHtml += '    <thead class="table-secondary table-header-fixed">';
        listHtml += '        <tr>';
        listHtml += '            <th>No.</th>';
        listHtml += '            <th>Template Name</th>';
        listHtml += '            <th>Template Desc</th>';
        listHtml += '            <th>Template PK</th>';
        listHtml += '            <th>Is Node Ready</th>';
        listHtml += '            <th>Create User</th>';
        listHtml += '            <th>Create Time</th>';
        listHtml += '            <th>Display Order</th>';
        listHtml += '            <th>Operation</th>';
        listHtml += '        </tr>';
        listHtml += '    </thead>';
        listHtml += '    </tbody>';
        if (result.status == 1) {
            for (var i = 0; i < result.resData.length; i++) {
                listHtml += '<tr>';
                listHtml += '    <td>' + No + '</td>';
                listHtml += '    <td>' + result.resData[i]["templateName"] + '</td>';
                listHtml += '    <td>' + subStrText(result.resData[i]["templateDesc"], 50) + '</td>';
                listHtml += '    <td>' + result.resData[i]["templatePK"] + '</td>';
                listHtml += '    <td>' + (result.resData[i]["isReady"] == 1 ? "Yes" : "No") + '</td>';
                listHtml += '    <td>' + result.resData[i]["psnName"] + '</td>';
                listHtml += '    <td>' + result.resData[i]["createTime"].substr(0, 10) + '</td>';
                listHtml += '    <td>' + result.resData[i]["displayOrder"] + '</td>';
                listHtml += '    <td>';
                listHtml += '        <div class="text-center">';
                listHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toSetNode" data-id="' + result.resData[i]["templateID"] + '" title="SetNode"><i class="fas fa-align-justify"></i></button>';
                listHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toEdit" data-id="' + result.resData[i]["templateID"] + '" title="Edit"><i class="fas fa-pencil-alt"></i></button>';
                listHtml += '            <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toDel" data-id="' + result.resData[i]["templateID"] + '" title="Delete"><i class="fas fa-trash-alt"></i></button>';
                listHtml += '        </div>';
                listHtml += '    </td>';
                listHtml += '</tr>';
                No++;
            }
            listHtml += "</tbody></table>";
            $("#tblArea").html(listHtml);
            var pagination = getPageNumHtml(result.number, subData.pageListNum, pageIndex);
            $("#paginationArea").html(pagination);
            bindTableEvent();
        }
        else {
            listHtml += "<tr><td colspan='8' class='text-center'>No Result!</td></tr></tbody></table>";
            $("#tblArea").html(listHtml);
        }

    })
}

function bindEvent() {
    $("#btnSearch").on("click", function () {
        getTemplate(1);
    })

    $("#btnNew").on("click", function () {
        $("#templateID").val(0);
        $("#templateName").val("");
        $("#templateDesc").val("");
        $("#displayOrder").val(0);
        $("#str1Title").val("");
        $("#int1Title").val("");
        $("#date1Title").val("");
        $("#templateStatus").attr("checked", true)
        $("#myModal").modal("show");
    })

    $("#toClose").on("click", function () {
        $("#myModal").modal("hide");
    })

    $("#toSave").on("click", function () {
        $("#toSave").attr("disabled", true);
        var objTemp = formArrToObj($("#tempInfo").serializeArray());
        if (objTemp.templateName.trim() == "" || objTemp.templateDesc.trim() == "") {
            Swal.fire("Error!", "The template name and description cannot be null!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (objTemp.templateStatus == undefined || objTemp.templateStatus == null) { objTemp.templateStatus = "off"; }
        objTemp.int2Title = ""; objTemp.int3Title = ""; objTemp.str2Title = ""; objTemp.str3Title = ""; objTemp.date2Title = ""; objTemp.date3Title = "";

        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "tempSet";
        obj.reqData = [objTemp];
        var url = "/core/api/Flows/tempSet";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                $("#toSave").attr("disabled", false);
                getTemplate(1);
                $("#myModal").modal("hide");
            }
            else {
                Swal.fire("Error", "Query field set failed,please retry or contact administrator!");
            }
        })

    })
}

function bindTableEvent() {
    $('button[name="toSetNode"]').on("click", function () {
        let tid = Number(this.dataset.id);
        window.location.href = "tempConfig.html?tid=" + tid;
    })
    $('button[name="toEdit"]').on("click", function () {
        let tid = Number(this.dataset.id);
        $("#templateID").val(tid);

        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "tempDetail";
        obj.reqData = [tid.toString()];
        var url = "/core/api/Flows/tempDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {

                $("#templateName").val(result.resData[0].templateName);
                $("#templateDesc").val(result.resData[0].templateDesc);
                $("#displayOrder").val(result.resData[0].displayOrder);
                $("#str1Title").val(result.resData[0].str1Title);
                $("#int1Title").val(result.resData[0].int1Title);
                $("#date1Title").val(result.resData[0].date1Title);
                result.resData[0].templateStatus == 1 ? $("#templateStatus").attr("checked", true) : $("#templateStatus").attr("checked", false);

                $("#myModal").modal("show");
            }
            else {
                Swal.fire("Error", "Query field set failed,please retry or contact administrator!");
            }
        })



        $("#myModal").modal("show");
    })

    $('button[name="toDel"]').on("click", function () {


        let tid = Number(this.dataset.id);
        $("#templateID").val(tid);

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
                obj.action = "tempDel";
                obj.reqData = [tid.toString()];
                var url = "/core/api/Flows/tempDel";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        getTemplate(1);
                    }
                    else {
                        Swal.fire("Error", "Failed,please retry or contact administrator!");
                    }
                })
            }
        })
    })
}
