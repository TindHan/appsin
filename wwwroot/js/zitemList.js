jQuery(function ($) {
    bindNull();
    getSet();
    bindEvent();
})

function getSet() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query applist";
    obj.reqData = new Array("");
    var url = "/core/api/Fieldcode/setAll";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            if (result.resData.length > 0) {
                var optionHtml = '<option value="0">Please Choose</option>';
                var setID = request("i");
                for (var i = 0; i < result.resData.length; i++) {
                    optionHtml += '<option value="' + result.resData[i]["setID"] + '">' + result.resData[i]["setName"] + '</option>';
                    if (setID == result.resData[i]["setID"])
                    { $("#objLevel").val(result.resData[i]["setLevel"]) }
                }
                $("#oid").html(optionHtml);
                $("#oid").val(setID);

                var setLevel = $("#objLevel").val();
                console.log(setLevel);
                if (setLevel == 2) {
                    $("#tblGroup").css("height", "calc(40vh)");
                    $("#titleItem").css("display", "flex").css("justify-content","space-between");
                    $("#tblItem").css("display", "block");
                }
                getGroup(1);
            }
        }
    })
}

function getGroup(pageIndex) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query menulist";
    var subObj = new Object();
    subObj.oid = $("#oid").val();
    subObj.ons = $("#ons").val();
    subObj.ty = "1";
    subObj.kw = "";
    subObj.pageIndex = pageIndex;
    subObj.pageListNum = 15;
    obj.reqData = [subObj];

    if (subObj.oid == "0") {
        bindNull();
    }
    else {
        var url = "/core/api/Fieldcode/itemList";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                localStorage.setItem("uToken", result.uToken);
                var moduleHtml = "";
                moduleHtml += '<table id="dt1" class="table table-bordered dt-responsive table-striped w-100" >';
                moduleHtml += '    <thead class="table-secondary table-header-fixed">';
                moduleHtml += '        <tr>';
                moduleHtml += '            <th>No.</th>';
                moduleHtml += '            <th>Field Item Name</th>';
                moduleHtml += '            <th>Field Item Description</th>';
                moduleHtml += '            <th>Display Order</th>';
                moduleHtml += '            <th>Create User</th>';
                moduleHtml += '            <th>Create Time</th>';
                moduleHtml += '            <th>Status</th>';
                moduleHtml += '            <th class="width-50 text-center">Operation</th>';
                moduleHtml += '        </tr>';
                moduleHtml += '    </thead>';
                moduleHtml += '    <tbody>';
                for (var j = 0; j < result.resData.length > 0; j++) {
                    moduleHtml += '        <tr data-id="' + result.resData[j].itemID + '">';
                    moduleHtml += '            <td>' + (j + 1) + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].itemName + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].itemDescription + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].displayOrder + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].createUser + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].createTime.substr(0, 10) + '</td>';
                    moduleHtml += '            <td>' + (result.resData[j].itemStatus == 1 ? '<span class="badge rounded-pill bg-success">Enabled</span>' : '<span class="badge rounded-pill bg-danger">Disabled</span>') + '</td>';
                    moduleHtml += '            <td class="width-80 text-center">';
                    moduleHtml += '                <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toGroupEdit" data-id="' + result.resData[j]["itemID"] + '" title="edit"><i class="fas fa-pencil-alt"></i></button>';
                    moduleHtml += '                <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toGroupDel" data-id="' + result.resData[j]["itemID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button>';
                    moduleHtml += '            </td>';
                    moduleHtml += '        </tr>';
                }
                moduleHtml += "</tbody></table>";
            }
            $("#tblGroup").html(moduleHtml);
            bindGrpEvent();
            if ($("#objLevel").val() == 2) {
                highLightTr();
            }
        })
    }
}


function highLightTr() {
    $('#dt1 tr').click(function () {
        var mid = $(this).attr("data-id");
        $('#dt1 tr').removeClass('highlight');
        $(this).addClass('highlight');
        $("#mid").val(mid);
        if (mid != "" && mid != undefined) { getItem(); }
    });
    $("#dt1 tr:first-child").click();
}

function getItem() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query itemlist";
    var subObj = new Object();
    subObj.oid = $("#mid").val();
    subObj.ons = "1";
    subObj.ty = "2";
    subObj.kw = "";
    subObj.pageIndex = 1;
    subObj.pageListNum = 10;
    obj.reqData = [subObj];

    if (subObj.oid == "0") {
        bindNull();
    }
    else {
        var url = "/core/api/Fieldcode/itemList";
        httpPost(url, obj, function (result) {
            var menuHtml = "";
            menuHtml += '<table id = "dt2" class="table table-bordered dt-responsive table-striped w-100" >';
            menuHtml += '    <thead class="table-secondary table-header-fixed">';
            menuHtml += '        <tr>';
            menuHtml += '            <th>No.</th>';
            menuHtml += '            <th>Sub Item Name</th>';
            menuHtml += '            <th>Sub Item Description</th>';
            menuHtml += '            <th>Sub Item Display Order</th>';
            menuHtml += '            <th>Create User</th>';
            menuHtml += '            <th>Create Time</th>';
            menuHtml += '            <th class="width-50 text-center">Operation</th>';
            menuHtml += '        </tr>';
            menuHtml += '    </thead>';
            menuHtml += '    <tbody>';
            if (result.status == 1) {
                localStorage.setItem("uToken", result.uToken);
                for (var k = 0; k < result.resData.length > 0; k++) {
                    menuHtml += '        <tr data-id="' + result.resData[k].menuID + '">';
                    menuHtml += '            <td>' + (k + 1) + '</td>';
                    menuHtml += '            <td>' + result.resData[k].itemName + '</td>';
                    menuHtml += '            <td>' + result.resData[k].itemDescription + '</td>';
                    menuHtml += '            <td>' + result.resData[k].displayOrder + '</td>';
                    menuHtml += '            <td>' + result.resData[k].createUser + '</td>';
                    menuHtml += '            <td>' + result.resData[k].createTime.substr(0, 10) + '</td>';
                    menuHtml += '            <td class="width-80 text-center">';
                    menuHtml += '                <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toItemEdit" data-id="' + result.resData[k]["itemID"] + '" title="edit"><i class="fas fa-pencil-alt"></i></button>';
                    menuHtml += '                <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toItemDel" data-id="' + result.resData[k]["itemID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button>';
                    menuHtml += '            </td>';
                    menuHtml += '        </tr>';
                }
                menuHtml += "</tbody></table>";
            }
            else {
                menuHtml += "<tr><td colspan='8' class='text-center'>No Result!</td></tr></tbody></table>";
            }
            $("#tblItem").html(menuHtml);
            binditemEvent();
        })
    }
}

function bindNull() {
    var moduleHtml = "";
    moduleHtml += '<table id = "dt1" class="table table-bordered dt-responsive table-striped w-100" >';
    moduleHtml += '    <thead class="table-secondary table-header-fixed">';
    moduleHtml += '        <tr>';
    moduleHtml += '            <th>No.</th>';
    moduleHtml += '            <th>Module Name</th>';
    moduleHtml += '            <th>Module Description</th>';
    moduleHtml += '            <th>Module Icon</th>';
    moduleHtml += '            <th>Create User</th>';
    moduleHtml += '            <th>Create Time</th>';
    moduleHtml += '            <th>Status</th>';
    moduleHtml += '            <th class="width-50 text-center">Operation</th>';
    moduleHtml += '        </tr>';
    moduleHtml += '    </thead>';
    moduleHtml += '    <tbody>';
    moduleHtml += "<tr><td colspan='8' class='text-center'>Please Choose Application First!</td></tr></tbody></table>";
    $("#tblModule").html(moduleHtml);
    var menuHtml = "";
    menuHtml += '<table id = "dt2" class="table table-bordered dt-responsive table-striped w-100" >';
    menuHtml += '    <thead class="table-secondary table-header-fixed">';
    menuHtml += '        <tr>';
    menuHtml += '            <th>No.</th>';
    menuHtml += '            <th>Menu Name</th>';
    menuHtml += '            <th>Menu Description</th>';
    menuHtml += '            <th>Menu Icon</th>';
    menuHtml += '            <th>Menu Link</th>';
    menuHtml += '            <th>Create User</th>';
    menuHtml += '            <th>Create Time</th>';
    menuHtml += '            <th>Status</th>';
    menuHtml += '            <th class="width-50 text-center">Operation</th>';
    menuHtml += '        </tr>';
    menuHtml += '    </thead>';
    menuHtml += '    <tbody>';
    menuHtml += "<tr><td colspan='9' class='text-center'>Please Choose Module First!</td></tr></tbody></table>";
    $("#tblMenu").html(menuHtml);

}

function bindEvent() {
    $("#oid").on("change", function () {
        getGroup()
    })
    $("#btnNew").on("click", function () {
        var aid = $("#oid").val();
        if (aid == 0) {
            Swal.fire("Error!", "Please choose application first!", "error");
        }
        else {
            $("#myModalFrame").attr("src", "itemGrp.html?t=a&i=" + aid).attr("width", "950px").attr("height", "410px");
            $("#mtt").html("New Field Item");
            $("#myModal").modal("show");
        }
    })

    $("#btnNewSub").on("click", function () {
        var aid = $("#oid").val();
        var mid = $("#mid").val();
        if (mid == "" || mid == "0" || mid == undefined) {
            Swal.fire("Error!", "Please choose module first!", "error");
        }
        else {
            $("#myModalFrame").attr("src", "itemitem.html?t=a&i=" + aid + "&p=" + mid).attr("width", "950px").attr("height", "410px");
            $("#mtt").html("New Field Sub Item");
            $("#myModal").modal("show");
        }
    })
}

function bindGrpEvent() {
    $("button[name='toGroupEdit']").on("click", function () {
        var mid = $(this).attr("data-id");
        
        $("#myModalFrame").attr("src", "itemGrp.html?t=u&i=" + mid).attr("width", "950px").attr("height", "410px");
        $("#myModal").modal("show");
    })
    $("button[name='toGroupDel']").on("click", function () {
        var that = this;
        var mid = $(that).attr("data-id");
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
                obj.action = "query roleDel";
                obj.reqData = [mid];
                var url = "/core/api/Apps/menuDel";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success!", "This item already be deleted!").then(function () { getModule() });
                    }
                    else {
                        Swal.fire("Error!", result.message);
                    }
                })

            }
        })

    })

}

function binditemEvent() {



    $("button[name='toItemEdit']").on("click", function () {
        var mid = $(this).attr("data-id");
        $("#myModalFrame").attr("src", "itemItem.html?t=u&i=" + mid).attr("width", "950px").attr("height", "410px");
        $("#myModal").modal("show");
    })



    $("button[name='toItemDel']").on("click", function () {
        var that = this;
        var mid = $(that).attr("data-id");
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
                obj.action = "query roleDel";
                obj.reqData = [mid];
                var url = "/core/api/Apps/menuDel";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        Swal.fire("Success!", "This item already be deleted!").then(function () { getMenu() });
                    }
                    else {
                        Swal.fire("Error!", result.message);
                    }
                })

            }
        })

    })
}

function closeModal() {
    $("#myModal").modal("hide");
}