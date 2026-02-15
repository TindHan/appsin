jQuery(function ($) {
	bindEvent();
})

function bindEvent() {
	$("#toImport").on("click", function () {
		upLoad();
	})

	$("#toDownload").on("click", function () {
		const a = document.createElement('a');
		a.href = "\\files\\Template\\PersonList.xlsx";
		a.download = "PersonList.xlsx";
		a.target = "_blank";
		a.click();
	})

	$("#toClose").on("click", function () {
		window.parent.closeModal();
	})

}

function upLoad() {
	var formData = new FormData();
	var file = $('#xlsxFile').get(0).files[0];
	if (file == undefined || file == "") {
		Swal.fire("Error!", "No file be choosed");
		return;
	}

	$("#toImport").attr("disabled", true);
	$("#msg").text("Verifying data format, please wait a moment!");

	formData.append('file', file);
	var uToken = localStorage.getItem("uToken")

	$.ajax({
		url: "/upload/FileSave",
		type: 'POST',
		cache: false,
		data: formData,
		processData: false,
		contentType: false,
		headers: {"uToken": uToken},
		dataType: "json",
		success: function (res) {
			if (res.status == 1) {
				$("#msg").text("Uploading file completed, importing data now, please wait a moment!");
				var imn = res.namelist[0];
				psnImport(imn);
			}
			else {
				$("#toImport").attr("disabled", false);
				$("#msg").text("");
				Swal.fire("Error!", result.message)
			}
		},
		error: function (XmlHttpRequest, textStatus, errorThrown) { },
		complete: function () { }
	});
}

function psnImport(imn){
	var obj = new Object();
	obj.uToken = localStorage.getItem("uToken");
	obj.action = "query psnDel";
	obj.reqData = new Array(imn);
	var url = "/core/api/Psns/psnImport";
	httpPost(url, obj, function (result) {
		$("#toImport").attr("disabled", false);
		if (result.status == 1) {
			$("#msg").text("");
			Swal.fire("Success!", result.message).then(function (e) {
				window.parent.queryList(1);
				window.parent.closeModal();
			});
		}
		else {
			Swal.fire("Error!", result.message);
		}
	})

}
