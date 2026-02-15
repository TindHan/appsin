jQuery(function ($) {
    getApp();
    getMenu();
    bindEvents();
});

function getApp() {
    var appHtml = "";
    appHtml += '<ul id="app10001" name="app" class="mb-3 menu-unstyled">'
    appHtml += '<li class="menu-title"><span style="color:rgb(81,86,190);font-weight:800">Console</span> </li>'
    appHtml += '</ul>';
    $("#sidebar-menu").append(appHtml);
}

function getMenu() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("aToken");
    obj.action = "query menulist";
    obj.reqData = new Array("");
    var url = "/core/api/console/getAdminMenu";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            for (var j = 0; j < result.resData.length > 0; j++) {
                if (result.resData[j]["menuLevel"] == 1) {
                    var menu1 = '<li' + (j == 0 ? ' class="mm-active"' : '') + '><a href="javascript: void(0);" class="has-arrow">';
                    menu1 += '<img src="../../img/menu.svg" width="18"/> &nbsp;';
                    menu1 += '<span>' + result.resData[j]["menuName"] + '</span></a>';
                    menu1 += '<ul id="m1' + result.resData[j]["menuID"] + '"></ul>';
                    menu1 += '</li>';
                    $("#app" + result.resData[j]["menuAppID"]).append(menu1);
                }
                else {
                    var menu2 = "";
                    if (result.resData[j]["menuAppID"] == 10001) {
                        menu2 = '<li><a href="' + result.resData[j]["menuLink"] + '" target="iFrameHome" >' + result.resData[j]["menuName"] + '</a></li>';
                    }
                    else {
                        menu2 = '<li><a href="#" onclick="redirect(' + result.resData[j]["menuID"] + ')">' + result.resData[j]["menuName"] + '</a></li>';
                    }
                    $("#m1" + result.resData[j]["parentID"]).append(menu2);
                }
            }

        }
        $('ul[name="app"]').metisMenu();
    })

}



function closeModal() {
    $("#showModal").modal("hide");
}
function bindEvents() {
    $("#btnSearch").on("click", function () {
        let txtSearch = $("#txtSearch").val();
        if (txtSearch === undefined || txtSearch === null) { txtSearch = ''; }
        if (txtSearch.trim() == "") { Swal.fire("Error!", "Search keywords cannot be null", "error"); return }
        $("#iFrameHome")[0].contentWindow.location.href = 'searchMenu.html?kw=' + txtSearch;
    })
    $("#toSave").on("click", function () {
        var pwdObj = formArrToObj($("#psnPwd").serializeArray());
        if (pwdObj.userPwd0.trim() == "" || pwdObj.userPwd1.trim() == "" || pwdObj.userPwd2.trim() == "") {
            Swal.fire("Error!", "All passwords are required!");
            return;
        }
        if (pwdObj.userPwd1.trim() != pwdObj.userPwd2.trim()) {
            Swal.fire("Error!", "Two new passwords are not the same password,please make sure they are consistent!");
            return;
        }
        if (pwdCheck(pwdObj.userPwd1)) {
            Swal.fire("Error!", "Your new password complexity is too low (the password must contain uppercase and lowercase letters, numbers, and special symbols), please change your password!");
            return;
        }

        var wkey = localStorage.getItem("wkey");
        const encryptor = new JSEncrypt();
        encryptor.setPublicKey(wkey);
        var prePwd = encryptor.encrypt(pwdObj.userPwd0);
        var nextPwd = encryptor.encrypt(pwdObj.userPwd1);

        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query pwdUpdate";
        obj.reqData = [wkey, prePwd, nextPwd];
        var url = "/core/api/Psns/adminPwdUpdate";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                Swal.fire("Success!", "Password has been changed!").then(function () {
                    window.parent.closeModal();
                });
            }
            else {
                Swal.fire("Error!", result.message);
            }
        })

    })

    $("#toAlterPwd").on("click", function () {
        $("#showModal").modal("show");
    })

    $("#toClose").on("click", function () {
        $("#showModal").modal("hide");
    })

    $("#logout").on("click", function () {
        Swal.fire({
            title: "PROPMT",
            text: "Your are going to logout,are you sure?",
            icon: "warning",
            showCancelButton: !0,
            confirmButtonText: "Yes, logout now!"
        }).then(function (e) {
            if (e.isConfirmed) {
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "query sessionCheck";
                obj.reqData = new Array("");
                var url = "/core/api/Users/logout";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        localStorage.setItem("uToken", "");
                        window.location.href = "alogin.html";
                    }
                    else {
                        Swal.fire("Error!", "There is something wrong occured,logout failed,please retry or contact administrator!", "error");
                    }
                })
            }
        });
    })
}

//setInterval(function () { scheck() }, 6000);

function scheck() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("aToken");
    obj.action = "query sessionCheck";
    obj.reqData = new Array("");
    var url = "/core/api/console/sessionCheck";
    httpPost(url, obj, function (result) {
        var al = document.querySelector('.swal2-modal');
        if (result.status != 1) {
            if (al != null && al.innerText.substr(0, 6) == "LOGOUT") { /*alert have already existed, do nothing*/ }
            else {
                Swal.fire("LOGOUT", "You haven't clicked on the system for a while,you need reLogin now！", "error").then(function (e) {
                    window.location.href = "/index.html";
                });
            }
        }
    })
}
