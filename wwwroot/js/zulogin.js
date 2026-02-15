jQuery(function ($) {
    getwkey();
    login();
});

function login() {
    $("#btnLogin").on("click", function () {
        $("#btnLogin").attr("disabled", true);
        var obj = new Object();
        obj.userName = $("#txtUserName").val();
        obj.userPwd = $("#txtUserPwd").val();
        obj.vCode = $("#txtCaptcha").val();
        obj.bfp = bfp;

        if (obj.userName == null || obj.userName.trim() == "" || obj.userPwd == null || obj.userPwd.trim() == "") {
            Swal.fire("Error", "Username and password must be inputed！", "error");
            $("#btnLogin").attr("disabled", false);
        }
        else if (obj.userPwd.trim().length < 8) {
            Swal.fire("Error", "The length of password must be greater than or equal to 8 ", "error");
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

                var url = "/core/api/Users/loginCheck";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        localStorage.setItem("uToken", result.uToken);
                        window.location.href = "mainFrame.html";
                    }
                    else if (result.status == -1) {
                        var captcha = result.resData[0];
                        $("#captchaImg").attr("src", ("data:image/png;base64," + captcha));
                        $("#captchaArea").css("display", "block");
                        bindEvent();
                        Swal.fire("Error!", result.message, "error");
                    }
                    else {
                        Swal.fire("Error!", result.message, "error");
                    }
                    $("#btnLogin").attr("disabled", false);
                })
            }
            else {
                $("#btnLogin").remove("disabled");
                Swal.fire("Error", "The key is illegal，please refresh or contact administrator！！");
            }
        }
    })
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

function bindEvent() {
    $("#captchaImg").on("click", function () {
        var url = "/core/api/Users/alterCaptcha";
        var obj = new Object();
        obj.args = obj.userName = $("#txtUserName").val();;
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
    })
}
