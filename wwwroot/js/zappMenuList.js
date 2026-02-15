jQuery(function ($) {
    bindNull();
    getApp();
    bindEvent();
})

function getApp() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query applist";
    obj.reqData = new Array("");
    var url = "/core/api/Users/getApp";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            if (result.resData.length > 0) {
                var optionHtml = '<option value="0">Please Choose</option>';
                for (var i = 0; i < result.resData.length; i++) {
                    optionHtml += '<option value="' + result.resData[i]["appID"] + '">' + result.resData[i]["appName"] + '</option>';
                }
                $("#oid").html(optionHtml);
            }
        }
    })
}

function getModule() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query menulist";
    var subObj = new Object();
    subObj.oid = $("#oid").val();
    subObj.ons = $("#ons").val();
    subObj.ty = "1";
    subObj.kw = "";
    subObj.pageIndex = 1;
    subObj.pageListNum = 20;
    obj.reqData = [subObj];

    if (subObj.oid == "0") {
        bindNull();
    }
    else {
        var url = "/core/api/Users/getAppMenu";
        httpPost(url, obj, function (result) {
            if (result.status == 1 ) {
                localStorage.setItem("uToken", result.uToken);
                var moduleHtml = "";
                moduleHtml += '<table id="dt1" class="table table-bordered dt-responsive table-striped w-100" >';
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
                for (var j = 0; j < result.resData.length > 0; j++) {
                    moduleHtml += '        <tr data-id="' + result.resData[j].menuID + '">';
                    moduleHtml += '            <td>' + (j + 1) + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].menuName + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].menuDescription + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].menuIcon + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].createUser + '</td>';
                    moduleHtml += '            <td>' + result.resData[j].createTime.substr(0, 10) + '</td>';
                    moduleHtml += '            <td>' + (result.resData[j].menuStatus == 1 ? '<span class="badge rounded-pill bg-success">Enabled</span>' : '<span class="badge rounded-pill bg-danger">Disabled</span>') + '</td>';
                    moduleHtml += '            <td class="width-80 text-center">';
                    moduleHtml += '                <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toModuleEdit" data-id="' + result.resData[j]["menuID"] + '" title="edit"><i class="fas fa-pencil-alt"></i></button>';
                    moduleHtml += '                <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toModuleDel" data-id="' + result.resData[j]["menuID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button>';
                    moduleHtml += '            </td>';
                    moduleHtml += '        </tr>';
                }
                moduleHtml += "</tbody></table>";
            }
            else {
                bindNull();
            }
            $("#tblModule").html(moduleHtml);
            highLightTr();
        })
    }
}

function getMenu() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query menulist";
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
        var url = "/core/api/Users/getAppMenu";
        httpPost(url, obj, function (result) {
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
            menuHtml += '            <th class="width-50 text-center">Operation</th>';
            menuHtml += '        </tr>';
            menuHtml += '    </thead>';
            menuHtml += '    <tbody>';
            if (result.status == 1) {
                localStorage.setItem("uToken", result.uToken);
                for (var k = 0; k < result.resData.length > 0; k++) {
                    menuHtml += '        <tr data-id="' + result.resData[k].menuID + '">';
                    menuHtml += '            <td>' + (k + 1) + '</td>';
                    menuHtml += '            <td>' + result.resData[k].menuName + '</td>';
                    menuHtml += '            <td>' + (result.resData[k].menuDescription == null ? "" : result.resData[k].menuDescription) + '</td>';
                    menuHtml += '            <td>' + (result.resData[k].menuIcon == null ? "" : result.resData[k].menuIcon) + '</td>';
                    menuHtml += '            <td>' + result.resData[k].menuLink + '</td>';
                    menuHtml += '            <td>' + result.resData[k].createUser + '</td>';
                    menuHtml += '            <td>' + result.resData[k].createTime.substr(0, 10) + '</td>';
                    menuHtml += '            <td class="width-80 text-center">';
                    menuHtml += '                <button type="button" class="btn btn-soft-primary waves-effect waves-light btn-sm" name="toMenuEdit" data-id="' + result.resData[k]["menuID"] + '" title="edit"><i class="fas fa-pencil-alt"></i></button>';
                    menuHtml += '                <button type="button" class="btn btn-soft-warning waves-effect waves-light btn-sm" name="toMenuDel" data-id="' + result.resData[k]["menuID"] + '" title="delete"><i class="fas fa-trash-alt"></i></button>';
                    menuHtml += '            </td>';
                    menuHtml += '        </tr>';
                }
                menuHtml += "</tbody></table>";
            }
            else {
                menuHtml += "<tr><td colspan='9' class='text-center'>No Result!</td></tr></tbody></table>";
            }
            $("#tblMenu").html(menuHtml);
            bindListEvent();
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
    moduleHtml += "<tr><td colspan='8' class='text-center'>No data!</td></tr></tbody></table>";
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
    menuHtml += "<tr><td colspan='9' class='text-center'>No data!</td></tr></tbody></table>";
    $("#tblMenu").html(menuHtml);

}

function highLightTr() {
    $('#dt1 tr').click(function () {
        var mid = $(this).attr("data-id");
        $('#dt1 tr').removeClass('highlight');
        $(this).addClass('highlight');
        $("#mid").val(mid);
        if (mid != "" && mid != undefined) { getMenu(); }
    });
    $("#dt1 tr:first-child").click();
}

function bindEvent() {
    $("#btnSearch").on("click", function () {
        getModule()
    })
    $("#btnNew").on("click", function () {
        var aid = $("#oid").val();
        if (aid == 0) {
            Swal.fire("Error!", "Please choose application first!", "error");
        }
        else {
            $("#myModalFrame").attr("src", "appMenuGrp.html?t=a&i=" + aid).attr("width", "950px").attr("height", "410px");
            $("#mtt").html("New App Module");
            $("#myModal").modal("show");
        }
    })

    $("#btnMenu").on("click", function () {
        var mid = $("#mid").val();
        if (mid == "" || mid == "0" || mid == undefined) {
            Swal.fire("Error!", "Please choose module first!", "error");
        }
        else {
            $("#myModalFrame").attr("src", "appMenuLink.html?t=a&i=" + mid).attr("width", "950px").attr("height", "410px");
            $("#mtt").html("New App Menu");
            $("#myModal").modal("show");
        }
    })
}

function bindListEvent() {

    $("button[name='toModuleEdit']").on("click", function () {
        var aid = $("#oid").val();
        var mid = $(this).attr("data-id");
        $("#myModalFrame").attr("src", "appMenuGrp.html?t=u&i=" + aid + "&m=" + mid).attr("width", "950px").attr("height", "410px");
        $("#mtt").html("Edit App Module");
        $("#myModal").modal("show");
    })

    $("button[name='toMenuEdit']").on("click", function () {
        var mid = $(this).attr("data-id");
        var pid = $("#mid").val();
        $("#myModalFrame").attr("src", "appMenuLink.html?t=u&i=" + pid + "&m=" + mid).attr("width", "950px").attr("height", "410px");
        $("#mtt").html("Edit App Menu");
        $("#myModal").modal("show");
    })

    $("button[name='toModuleDel']").on("click", function () {
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
                        Swal.fire("Success!", "This item has already be deleted!").then(function () { getModule() });
                    }
                    else {
                        Swal.fire("Error!", result.message);
                    }
                })

            }
        })

    })

    $("button[name='toMenuDel']").on("click", function () {
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