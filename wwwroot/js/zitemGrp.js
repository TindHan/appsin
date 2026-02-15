jQuery(function ($) {
    getItemDetail();
    bindEvent();
})

function getItemDetail() {
    if (request("t") == "u") {
        var oid = request("i");
        var obj = new Object();
        obj.uToken = localStorage.getItem("uToken");
        obj.action = "query menu";
        obj.reqData = [oid];
        var url = "/core/api/Fieldcode/itemDetail";
        httpPost(url, obj, function (result) {
            if (result.status == 1) {
                $("#itemID").val(result.resData[0].itemID);
                $("#itemName").val(result.resData[0].itemName);
                $("#itemDesc").val(result.resData[0].itemDescription);
                $("#displayOrder").val(result.resData[0].displayOrder);
                if (result.resData[0].itemStatus == 1) { $("#itemStatus").attr("checked", "true"); }
                else { $("#itemStatus").attr("checked", "false") };
            }
            else {
                Swal.fire("Error", "Query menu failed,please retry or contact administrator!");
            }
        })
    }

}

function bindEvent() {
    $("#toSave").on("click", function () {
        var subObj = formArrToObj($("#objInfo").serializeArray());
        if (subObj.itemName.trim() == "") {
            Swal.fire("Error!", "Field item name is required!");
        }
        else {
            subObj.setID = request("i");
            if (!("itemStatus" in subObj)) {
                subObj.menuStatus = "off";
            }
            var obj = new Object();
            obj.uToken = localStorage.getItem("uToken");
            obj.action = "query orglist";
            obj.reqData = [subObj];

            if (request("t") == "a") {
                var url = "/core/api/Fieldcode/item1Add";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        window.parent.closeModal(); window.parent.getGroup();
                    }
                })
            }
            else {
                var url = "/core/api/Fieldcode/itemUpdate";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        window.parent.closeModal(); window.parent.getGroup();
                    }
                })
            }
        }
    })
    $("#toClose").on("click", function () {
        window.parent.closeModal();
    })
}