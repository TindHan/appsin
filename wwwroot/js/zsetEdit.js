jQuery(function ($) {
    getSet();
    bindEvent();
});

function getSet() {
    if (request("t") == "u") {
        var aid = request("i");
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query app";
        obj.reqData = [aid];
        var url = "/core/api/Fieldcode/setDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                $("#setID").val(result.resData[0].setID);
                $("#setName").val(result.resData[0].setName);
                $("#setType").val(result.resData[0].setType);
                $("#setCode").val(result.resData[0].setCode);
                $("#setLevel").val(result.resData[0].setLevel);
                $("#setDesc").val(result.resData[0].setDescription);
                $("#setRank").val(result.resData[0].setRank);
                $("#displayOrder").val(result.resData[0].displayOrder);
                $("#setStatus").val(result.resData[0].setStatus);
            }
            else {
                Swal.fire("Error", "Query field set failed,please retry or contact administrator!");
            }
        })
    }
}

function bindEvent() {

    $("#toSave").on("click", function () {
        $("#toSave").attr("disabled", true);
        var reqdata = formArrToObj($("#objInfo").serializeArray());
        if (reqdata.setName.trim() == "" || reqdata.setType.trim() == "" ) {
            Swal.fire("Error!", "Api name and description is required!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (reqdata.setLevel.trim()=="") {
            Swal.fire("Error!", "Field set level is required!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (reqdata.setLevel.trim() !== "1" && reqdata.setLevel.trim() !== "2") {
            Swal.fire("Error!", "Field set level must be 1 or 2!");
            $("#toSave").attr("disabled", false);
            return;
        }
        if (!("setStatus" in reqdata)) {
            reqdata.setStatus = "off";
        }
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query set";
        obj.reqData = [reqdata];
        
        if (request("t") == "a" || request("t") == "a#") {
            var url = "/core/api/Fieldcode/setAdd";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.queryList(1);
                    window.parent.closeModal();
                }
                else {
                    Swal.fire("Error!", "There is something wrong occured, please retry or contact administrator!");
                }
                $("#toSave").attr("disabled", false);
            })
        }
        else {
            var url = "/core/api/Fieldcode/setUpdate";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    window.parent.queryList(1);
                    window.parent.closeModal();
                }
                else {
                    Swal.fire("Error!", result.message);
                }
                $("#toSave").attr("disabled", false);
            })
        }

    })

    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}