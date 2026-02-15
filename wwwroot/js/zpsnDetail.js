jQuery(function ($) {
    getPsnInfo();
    bindEvent();
})

function getPsnInfo() {
    var pid = request("pid");

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array(pid.toString());
    var url = "/core/api/Psns/getPsnDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            $("#uName").html(result.resData[0].psnName);
            $("#uAlian").html(result.resData[0].aliaName);
            $("#uUnit").html(result.resData[0].unitName);
            $("#uDept").html(result.resData[0].deptName);
            $("#uPost").html(result.resData[0].postName);
            $("#uCode").html(result.resData[0].psnCode);
            $("#uCert").html(result.resData[0].idTypeName);
            $("#uID").html(result.resData[0].idNo);
            $("#uSex").html(result.resData[0].psnSex);
            $("#uNation").html(result.resData[0].psnNational);
            $("#uType").html(result.resData[0].onTypeName);
            $("#uLogin").html(result.resData[0].psnUsername);
            $("#uPhone").html(result.resData[0].psnCellphone);
            $("#uMail").html(result.resData[0].psnEmail);
            $("#uIM").html(result.resData[0].psnIM);
            $("#uBirthday").html(result.resData[0].psnBirthday.substr(0,4) == "0001" ? "" : result.resData[0].psnBirthday.substr(0, 10));
            $("#uJoinday").html(result.resData[0].psnJoinday.substr(0, 4) == "0001" ? "" : result.resData[0].psnJoinday.substr(0, 10));
            $("#uJobday").html(result.resData[0].psnJobday.substr(0, 4) == "0001" ? "" : result.resData[0].psnJobday.substr(0, 10));
            $("#uM1").html(result.resData[0].psnMemo1);
            $("#uM2").html(result.resData[0].psnMemo2);
            $("#uM3").html(result.resData[0].psnMemo3);
            $("#uM4").html(result.resData[0].psnMemo4);
            $("#uM5").html(result.resData[0].psnMemo5);
            $("#uTime").html(result.resData[0].createTime.substr(0,10));
            if (result.resData[0].psnPicture != "") { $("#imgAvatar").attr("src", result.resData[0].psnPicture) };

        }
    });
}

function bindEvent() {
    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}