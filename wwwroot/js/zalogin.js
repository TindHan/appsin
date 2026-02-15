jQuery(function ($) {
    getwkey();
    getCaptcha();
    bindEvents();
});

function bindEvents() {

    $("#captchaImg").on("click", function () {
        getCaptcha();
    })
    $("#btnLogin").on("click", function () {
        $("#btnLogin").attr("disabled", true);
        login();
    })

    $(document).on("keydown", function (e) {
        if (e.key === 'Enter') {
            login();
        }
    })
}

function login() {
    var obj = new Object();
    obj.userName = $("#txtAdminName").val();
    obj.userPwd = $("#txtAdminPwd").val();
    obj.vCode = $("#txtCaptcha").val();
    obj.bfp = bfp;

    if (obj.userName == null || obj.userName.trim() == "" || obj.userPwd == null || obj.userPwd.trim() == "") {
        Swal.fire("Error", "Administrator name and password must be inputed！", "error");
        $("#btnLogin").attr("disabled", false);
    }
    else if (obj.userPwd.trim().length < 8) {
        Swal.fire("Error", "The length of password must be greater than or equal to 8 ", "error");
        $("#btnLogin").attr("disabled", false);
    }
    else if (obj.vCode == null || obj.vCode.trim() == "") {
        Swal.fire("Error", "The captcha must be inputed！", "error");
        $("#btnLogin").attr("disabled", false);
    }
    else if (pwdCheck(obj.userPwd.trim())) {
        Swal.fire("Error", "Your password complexity is too low (the password must contain uppercase and lowercase letters, numbers, and special symbols), please change your password in time！", "error");
        $("#btnLogin").attr("disabled", false);
    }
    else {
        const encryptor = new JSEncrypt();
        var wkey = localStorage.getItem("wkey");
        if (wkey != null && wkey != "") {
            encryptor.setPublicKey(wkey);
            obj.userPwd = encryptor.encrypt(obj.userPwd);
            obj.wkey = wkey;

            var url = "/core/api/Console/adminLogin";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    localStorage.setItem("aToken", result.uToken);
                    window.location.href = "aFrame.html";
                }
                else {
                    Swal.fire("Error!", result.message, "error");
                }
                $("#btnLogin").attr("disabled", false);
            })
        }
        else {
            Swal.fire("Error", "The key is not valid，please refresh or contact administrator！！");
            $("#btnLogin").attr("disabled", false);
        }
    }
}
function getwkey() {
    var url = "/core/api/Users/getwkey";
    var obj = new Object();
    obj.wkeyStr = "no";
    obj.wkeyFor = "login";
    obj.wkeyStamp = Date.now().toString();
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var wkey = result.resData[0].wkeyStr;
            localStorage.setItem("wkey", wkey);
        }
    })
}

function getCaptcha() {

    var url = "/core/api/Console/genCaptcha";
    var obj = new Object();
    obj.args = obj.userName = $("#txtAdminName").val();
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var captcha = result.resData[0];
            $("#captchaImg").attr("src", ("data:image/png;base64," + captcha));
            $("#captchaArea").css("display", "block");
        }
        else {
            Swal.fire("Error!", result.message, "error");
        }
    })
}