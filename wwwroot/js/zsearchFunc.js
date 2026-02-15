jQuery(function ($) {
    getKw();
});

function getKw() {
    let kw = request("kw");
    $("#kwshow").val(kw);
    if (kw.trim() == "") { Swal.fire("Alert!", "Search keyword cannot be null!", "erorr"); return; }

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getSearchFunc";
    obj.reqData = [kw];
    var url = "/core/api/DashBoard/getSearchFunc";
    httpPost(url, obj, function (result) {
            let funcHtml = "";
        if (result.status == 1 && result.resData.length > 0) {
            let t = result.resData;

            t.forEach(function (t, i) {
                funcHtml += `     <li class="hover-bg-color">
                                    <div class="row border-bottom  ">
                                        <div class="col-9 pt-3 pb-2_5">
                                            <h6 class="text-truncate font-size-14">${t.appName} > ${t.moduleName} > ${t.menuName}</h6>
                                            <span>${t.menuDescription == null ? 'No description' : t.menuDescription}</span>
                                        </div>
                                    </div>
                                </li>`;
            })

        }
        else {
            funcHtml = `         <li class="hover-bg-color">
                                    <div class="row border-bottom  ">
                                        <div class="col-9 pt-3 pb-2_5">
                                            <h6 class="text-truncate font-size-14">No functions meet your search conditions</h6>
                                            <span></span>
                                        </div>
                                    </div>
                                </li>`;
        }
        $("#funcList").html(funcHtml);

    })
}