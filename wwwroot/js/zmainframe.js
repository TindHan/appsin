jQuery(function ($) {
    getApp();
    getPsn();
    bindEvent();
    scheck();
});

function getApp() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query applist";
    obj.reqData = new Array("");
    var url = "/core/api/Users/getOsrzApp";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            if (result.resData.length > 0) {
                var appHtml = '';
                for (var i = 0; i < result.resData.length; i++) {
                    appHtml += '<ul id="app' + result.resData[i].appID + '" name="app" class="mb-3 menu-unstyled">';
                    appHtml += '<li class="menu-title"><span style="color:rgb(81,86,190);font-weight:800"> ' + result.resData[i]["appName"] + '</span> </li >'
                    appHtml += '</ul>';
                    $("#sidebar-menu").append(appHtml);
                    appHtml = "";
                }
            }
            getMenu();
        }
    })
}
function getPsn() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query applist";
    obj.reqData = [""];
    var url = "/core/api/Psns/getMyDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            $("#psnName").html(result.resData[0].psnName);
            var pic = result.resData[0].psnPicture;
            if (pic != null && pic != "") {
                $("#psnImg").attr("src", pic);
            }
            else {
                $("#psnImg").attr("src", "/img/noavatar.png");
            }
        }
    })
}

function getMenu() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query menulist";
    obj.reqData = new Array("");
    var url = "/core/api/Users/getOsrzMenu";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            for (var j = 0; j < result.resData.length > 0; j++) {
                if (result.resData[j]["menuLevel"] == 1) {
                    var menu1 = '<li ' + (result.resData[j]["menuName"] == "DashBoard" ? 'class="mm-active"' : '') + '><a href="javascript: void(0);" class="has-arrow">';
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

function redirect(aid) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query menu";
    obj.reqData = [aid.toString()];
    var url = "/core/api/Users/gotoApp";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            localStorage.setItem("appDomain", (result.resData[0] + result.resData[1]))
            var u = result.resData[0] + result.resData[1] + result.resData[2] + "?k=" + result.resData[3];
            var w = $("#iFrameHome")[0].contentWindow;
            w.location.href = u;
        }
        else {
            Swal.fire("Error!", "There is something wrong occured,logout failed,please retry or contact administrator!", "error");
        }
    })
}


function closeModal() {
    $("#showModal").modal("hide");
}
function bindEvent() {
    $("#btnSearch").on("click", function () {
        let txtSearch = $("#txtSearch").val();
        if (txtSearch === undefined || txtSearch === null) { txtSearch = ''; }
        if (txtSearch.trim() == "") { Swal.fire("Error!", "Search keywords cannot be null", "error"); return }
        $("#iFrameHome")[0].contentWindow.location.href = 'searchFunc.html?kw=' + txtSearch;
    })

    $("#toMyDetail").on("click", function () {
        $("#showModalFrame").attr("src", "myDetail.html").attr("width", "950px").attr("height", "500px");
        $("#mtt").html("My Profile");
        $("#showModal").modal("show");
    })

    $("#toAlterPwd").on("click", function () {
        $("#showModalFrame").attr("src", "myPwd.html").attr("width", "950px").attr("height", "330px");
        $("#mtt").html("Change Password");
        $("#showModal").modal("show");
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
                        window.location.href = "/index.html";
                    }
                    else {
                        Swal.fire("Error!", "There is something wrong occured,logout failed,please retry or contact administrator!", "error");
                    }
                })
            }
        });
    })
}

setInterval(function () { scheck() }, 6000);

function scheck() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query sessionCheck";
    obj.reqData = new Array("");
    var url = "/core/api/Users/sessionCheck";
    httpPost(url, obj, function (result) {
        var al = document.querySelector('.swal2-modal');
        if (result.status != 1) {
            if (al != null && al.innerText.substr(0, 6) == "LOGOUT") { /*alert is exist, do nothing*/ }
            else {
                Swal.fire("LOGOUT", "You haven't clicked on the system for a while,you need reLogin now！", "error").then(function (e) {
                    window.location.href = "/index.html";
                });
            }
        }
    })
}

//For the third server page in iframe redirect
window.addEventListener('message', function redirectTo(e) {
    if (e.data.hello == undefined && typeof (e.data) == "string") {
        var u = e.origin + e.data;
        console.log(e);
        var w = $("#iFrameHome")[0].contentWindow;
        w.location.href = u;
    }
})
