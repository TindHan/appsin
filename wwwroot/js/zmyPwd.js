jQuery(function ($) {
    getwkey();
    bindEvent();
})

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
            $("#psnID").val(request("i"));
        }
    })
}

function bindEvent() {

    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
    $("#toSave").on("click", function () {
        var pwdObj = formArrToObj($("#psnPwd").serializeArray());
        if (pwdObj.userPwd0.trim() == "" ||pwdObj.userPwd1.trim() == "" || pwdObj.userPwd2.trim() == "") {
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
        var url = "/core/api/Psns/myPwdUpdate";
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
}