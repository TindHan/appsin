jQuery(function ($) {
    bindEvent();
    getOrgList();
})

function getOrgList() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    var reqData = new Object();
    reqData.id = "10001";// $("#unitID").val();
    reqData.type = "unit";
    reqData.order = "";
    obj.reqData = [reqData];
    var url = "/core/api/Orgs/getOrgList";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            var unitList = result.resData;
            var orgHtml = "<option value='0'>---Please Choose---</option>";
            for (var i = 0; i < unitList.length; i++) {
                orgHtml += "<option value='" + unitList[i]["orgID"] + "'>" + unitList[i]["orgName"] + "</option>";
            }
            $("#unitID").html(orgHtml)
        }
        getPsnType();
    })
}
function getPsnType() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array("type");
    var url = "/core/api/Psns/getField";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var itemList = result.resData;
            var itemHtml = "<option value='0'>---Please Choose---</option>";
            for (var i = 0; i < itemList.length; i++) {
                itemHtml += "<option value='" + itemList[i]["itemID"] + "'>" + itemList[i]["itemName"] + "</option>";
            }
            $("#onType").html(itemHtml);
        }
        idType();
    })
}

function idType() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array("id");
    var url = "/core/api/Psns/getField";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var itemList = result.resData;
            var itemHtml = "<option value='0'>---Please Choose---</option>";
            for (var i = 0; i < itemList.length; i++) {
                itemHtml += "<option value='" + itemList[i]["itemID"] + "'>" + itemList[i]["itemName"] + "</option>";
            }
            $("#idType").html(itemHtml);
        }
        if (request("t") == "e") {
            getPsnInfo();
        }
    })
}




function getPsnInfo() {
    var pid = request("i");

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query orglist";
    obj.reqData = new Array(pid.toString());
    var url = "/core/api/Psns/getPsnDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            localStorage.setItem("uToken", result.uToken);
            $("#psnID").val(result.resData[0].psnID);
            $("#psnName").val(result.resData[0].psnName);
            $("#aliaName").val(result.resData[0].aliaName);
            $("#unitID").html("<option value='" + result.resData[0].unitID + "'>" + result.resData[0].unitName + "</option>");
            $("#deptID").html("<option value='" + result.resData[0].deptID + "'>" + result.resData[0].deptName + "</option>");
            $("#postID").html("<option value='" + result.resData[0].postID + "'>" + result.resData[0].postName + "</option>");
            $("#psnCode").val(result.resData[0].psnCode);
            $("#idType").val(result.resData[0].idType);
            $("#idNo").val(result.resData[0].idNo);
            $("#psnSex").val(result.resData[0].psnSex);
            $("#psnNational").val(result.resData[0].psnNational);
            $("#onType").val(result.resData[0].onType);
            $("#psnUsername").val(result.resData[0].psnUsername);
            $("#psnCellphone").val(result.resData[0].psnCellphone);
            $("#psnIM").val(result.resData[0].psnIM);
            $("#psnBirthday").val(result.resData[0].psnBirthday.substr(0, 4) == "0001" ? "" : result.resData[0].psnBirthday.substr(0,10));
            $("#psnJoinday").val(result.resData[0].psnJoinday.substr(0, 4) == "0001" ? "" : result.resData[0].psnJoinday.substr(0, 10));
            $("#psnJobday").val(result.resData[0].psnJobday.substr(0, 4) == "0001" ? "" : result.resData[0].psnJobday.substr(0, 10));
            $("#psnEmail").val(result.resData[0].psnEmail);
            $("#psnMemo1").val(result.resData[0].psnMemo1);
            $("#psnMemo2").val(result.resData[0].psnMemo2);
            $("#psnMemo3").val(result.resData[0].psnMemo3);
            $("#psnMemo4").val(result.resData[0].psnMemo4);
            $("#psnMemo5").val(result.resData[0].psnMemo5);
            if (result.resData[0].psnPicture != "") { $("#imgAvatar").attr("src", result.resData[0].psnPicture) } ;
            $("div[name='orgdiv']").css("display", "none");
        }
    });
}




function bindEvent() {
    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })

    $("#unitID").on("change", function () {
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query orglist";
        var reqData = new Object();
        reqData.id = $("#unitID").val();
        reqData.type = "dept";
        reqData.order = "";
        obj.reqData = [reqData];
        var url = "/core/api/Orgs/getOrgList";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                localStorage.setItem("uToken", result.uToken);
                var deptList = result.resData;
                var deptHtml = "<option value='0'>---Please Choose---</option>";
                for (var i = 0; i < deptList.length; i++) {
                    deptHtml += "<option value='" + deptList[i]["orgID"] + "'>" + deptList[i]["orgName"] + "</option>";
                }
                $("#deptID").html(deptHtml)

                var postHtml = "<option value='0'>---Please choose department first---</option>";
                $("#postID").html(postHtml)
            }
        })

    });


    $("#deptID").on("change", function () {
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query orglist";
        var reqData = new Object();
        reqData.id = $("#deptID").val();
        reqData.type = "post";
        reqData.order = "";
        obj.reqData = [reqData];
        var url = "/core/api/Orgs/getOrgList";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                localStorage.setItem("uToken", result.uToken);
                var postList = result.resData;
                var postHtml = "<option value='0'>---Please Choose---</option>";
                for (var i = 0; i < postList.length; i++) {
                    postHtml += "<option value='" + postList[i]["orgID"] + "'>" + postList[i]["orgName"] + "</option>";
                }
                $("#postID").html(postHtml)
            }
        })

    })

    $("#toSave").on("click", function () {
        $("#toSave").attr("disabled", true);
        var objPsn = formArrToObj($("#psnInfo").serializeArray());
        if (objPsn.psnName == "") { Swal.fire("Error", "Person name must be input ,please check again!", "error"); $("#toSave").attr("disabled", false); return; }
        if (objPsn.psnSex == "0") { Swal.fire("Error", "Person sex  must be input ,please check again!", "error"); $("#toSave").attr("disabled", false); return; }
        if (objPsn.unitID == "0") { Swal.fire("Error", "The organization must be select,please check again!", "error"); $("#toSave").attr("disabled", false); return; }
        if (objPsn.deptID == "0") { Swal.fire("Error", "The deptment must be select ,please check again!", "error"); $("#toSave").attr("disabled", false); return; }
        if (objPsn.onType == "0") { Swal.fire("Error", "Person type must be select ,please check again!", "error"); $("#toSave").attr("disabled", false); return; }
        if (objPsn.psnUsername == "") { Swal.fire("Error", "User name must be ,please check again!", "error"); $("#toSave").attr("disabled", false); return; }

        objPsn.psnPicture = $("#imgAvatar").attr("src");

        if (request("t") == "a") {
            if (objPsn.psnPassword == "") { Swal.fire("Error", "User password must be input ,please check again!", "error"); return; }
            var obj = new Object();
            obj.uToken = localStorage.getItem("uToken");
            obj.action = "query addPsn";
            obj.reqData = [objPsn];
            var url = "/core/api/Psns/psnAdd";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.getPsnList(1);
                    window.parent.closeModal();
                }
                else {
                    Swal.fire("Error!", result.message, "error");
                }
                $("#toSave").attr("disabled", false);
            })
        }
        else {
            var obj = new Object();
            obj.uToken = localStorage.getItem("uToken");
            obj.action = "query addPsn";
            obj.reqData = [objPsn];
            var url = "/core/api/Psns/psnUpdate";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.getPsnList(1);
                    window.parent.closeModal();
                }
                else {
                    Swal.fire("Error!", result.message, "error");
                }
                $("#toSave").attr("disabled", false);
            })
        }
    })

    $("#imgUp").on("click", function () {
        $("#imgFile").click();
    })

    $("#imgFile").on("change", function () {
        var avatar = new Image();
        avatar = this.files[0];
        if (avatar.size < 1024000) {
            const reader = new FileReader();
            var iss = true;
            reader.onload = (e) => {
                getImgSizeFromBase64(e.target.result, function (size) {
                    var wh = size.width / size.height * 10;
                    if (wh > 7 && wh < 8) {
                        $("#imgAvatar").attr("src", e.target.result);
                    }
                    else {
                        iss = false;
                        Swal.fire("Error!", "The picture dimension must be 250*350,please adjust your picture to proper dimension!")
                    }
                })
            }
            if (iss) {
                reader.readAsDataURL(avatar);
            }
        }
        else {
            Swal.fire("Error!", "The picture size must be less than 1M,please adjust your picture to proper size!")
        }

    })


}


function getImgSizeFromBase64(base64, callback) {
    var image = new Image();
    image.onload = function () {
        callback({ width: this.width, height: this.height });
    };
    image.src = base64;
}