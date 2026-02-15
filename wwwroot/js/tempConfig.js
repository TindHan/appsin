/* ==========  全局变量  ========== */
const jp = jsPlumb.getInstance({ Container: "canvas", Connector: ["Flowchart", { cornerRadius: 5 }], Anchors: ["Right", "Left"] });
let nodeIdSeq = 2;             // 0=start 1=end 从2开始
let activeNode = null;
let nodeMap = {};
let editingNode = null;
let connMap = {};
let followOffset = {};

const modal = new bootstrap.Modal(document.getElementById('nodeModal'));

/* ==========  initialize  ========== */
$(function () {
    jp.setZoom(1);
    initDefaultNodes();
    bindEvents();
    bindResize();
    getFlow();
});

function initDefaultNodes() {
    const $can = $("#canvas");
    const h = $can.height();
    const w = $can.width();
    createFixedNode("start", "node0", 20, (h - 60) / 4, "Start");
    createFixedNode("end", "node1", w - 140, (h - 60) / 4, "End");
}

function createFixedNode(type, id, x, y, name) {
    const cls = type === 'start' ? 'start' : 'end';
    const $div = $(`<div class="node ${cls}" id="${id}">${name}</div>`);
    $div.css({ left: x, top: y });
    $("#canvas").append($div);
    // 固定节点拖动结束事件 - 修复位置获取逻辑
    jp.draggable($div, {
        containment: "parent",
        stop: function () {
            // 直接读取节点的CSS样式（已包含单位，需解析为数字）
            const $node = $(this);
            const newLeft = parseInt($node.css('left'), 10);
            const newTop = parseInt($node.css('top'), 10);

            if (nodeMap[id]) {
                nodeMap[id].left = newLeft;
                nodeMap[id].top = newTop;
            }
        }
    });
    nodeMap[id] = { type: type, name: name, left: x, top: y, condition: {}, approve: {} };
    connMap[id] = [];
    jp.addEndpoint($div, { anchors: ["Right"], endpoint: ["Dot", { radius: 6 }], paintStyle: { fill: "#007bff" }, isSource: true, maxConnections: -1 });
    jp.addEndpoint($div, { anchors: ["Left"], endpoint: ["Dot", { radius: 6 }], paintStyle: { fill: "#007bff" }, isTarget: true, maxConnections: -1 });
    return $div;
}

function addNode(type, x, y, name) {
    const id = "node" + (nodeIdSeq++);
    const $div = $(`<div class="node task" id="${id}">${name}</div>`);
    $div.css({ left: x, top: y });
    $("#canvas").append($div);
    jp.draggable($div, {
        containment: "parent",
        stop: function () {
            const $node = $(this);
            const newLeft = parseInt($node.css('left'), 10);
            const newTop = parseInt($node.css('top'), 10);

            if (nodeMap[id]) {
                nodeMap[id].left = newLeft;
                nodeMap[id].top = newTop;
            }
        }
    });
    nodeMap[id] = { type: type, name: name, left: x, top: y, condition: {}, approve: {} };
    connMap[id] = [];
    jp.addEndpoint($div, { anchors: ["Right"], endpoint: ["Dot", { radius: 6 }], paintStyle: { fill: "#007bff" }, isSource: true, maxConnections: -1 });
    jp.addEndpoint($div, { anchors: ["Left"], endpoint: ["Dot", { radius: 6 }], paintStyle: { fill: "#007bff" }, isTarget: true, maxConnections: -1 });
    return $div;
}

function loadFlow(json) {
    if (!json || !json.nodes || json.nodes.length === 0) {
        clearCanvas();
        initDefaultNodes();
        return;
    }

    clearCanvas();

    /* 1. 重建所有节点（含端点） */
    json.nodes.forEach(n => {
        if (!n.nodePK) {
            console.error("Missing nodePK for node:", n);
            return;
        }

        const left = parseInt(n.left, 10) || 0;
        const top = parseInt(n.top, 10) || 0;

        if (n.type === "start" || n.type === "end") {
            const fixedId = n.type === "start" ? "node0" : "node1";
            createFixedNode(n.type, fixedId, left, top, n.name || (n.type === "start" ? "Start" : "End"));

            const $fixedNode = $(`#${fixedId}`);
            if (n.type === "start") {
                $fixedNode.css('border-color', '#28a745');
            } else {
                $fixedNode.css('border-color', '#dc3545');
            }

        } else {
            const $node = $(`<div class="node task" id="${n.nodePK}">${n.name || "New Node"}</div>`);
            $node.css({ left: left + "px", top: top + "px" });
            $("#canvas").append($node);
            jp.draggable($node, {
                containment: "parent",
                stop: function () {
                    const $node = $(this);
                    const newLeft = parseInt($node.css('left'), 10);
                    const newTop = parseInt($node.css('top'), 10);

                    if (nodeMap[n.nodePK]) {
                        nodeMap[n.nodePK].left = newLeft;
                        nodeMap[n.nodePK].top = newTop;
                        console.log(`加载节点${n.nodePK}位置更新:`, newLeft, newTop);
                    }
                }
            });

            nodeMap[n.nodePK] = {
                type: n.type || "task",
                name: n.name || "New Node",
                left: left,
                top: top,
                condition: parseCondition(n.condition),
                approve: parseApprove(n.approve)
            };
            connMap[n.nodePK] = [];

            jp.addEndpoint($node, {
                anchors: ["Right"],
                endpoint: ["Dot", { radius: 6 }],
                paintStyle: { fill: "#007bff" },
                isSource: true,
                maxConnections: -1
            });
            jp.addEndpoint($node, {
                anchors: ["Left"],
                endpoint: ["Dot", { radius: 6 }],
                paintStyle: { fill: "#007bff" },
                isTarget: true,
                maxConnections: -1
            });

            const num = parseInt(n.nodePK.replace("node", ""), 10);
            if (num >= nodeIdSeq) {
                nodeIdSeq = num + 1;
            }
        }
    });

    repositionDefaultNodes();
    jp.repaintEverything();

    /* 2. 延迟连线 */
    setTimeout(() => {
        jp.batch(function () {
            json.nodes.forEach(n => {
                const currentId = n.nodePK;
                if (n.prevID && nodeMap[n.prevID] && nodeMap[currentId]) {
                    jp.connect({ source: n.prevID, target: currentId, anchors: ["Right", "Left"] });
                    connMap[n.prevID].push(currentId);
                }
                if ((n.isEnd == 1 ? true : false) && nodeMap[currentId] && nodeMap["node1"]) {
                    jp.connect({ source: currentId, target: "node1", anchors: ["Right", "Left"] });
                    connMap[currentId].push("node1");
                }
            });
        });
        jp.repaintEverything();
    }, 300);
}

function clearCanvas() {
    jp.deleteEveryConnection();
    jp.deleteEveryEndpoint();
    $("#canvas").empty();
    nodeMap = {};
    connMap = {};
    followOffset = {};
    nodeIdSeq = 2;
}

function parseCondition(condStr) {
    if (!condStr) return {};
    const parts = condStr.split('|');
    return {
        c: parts[0] || "",
        op: parts[1] || "",
        val: parts[2] || ""
    };
}

function parseApprove(approveStr) {
    if (!approveStr) return {};
    const parts = approveStr.split('|');
    return {
        type: parts[0] || "",
        target: parts[1] || ""
    };
}

function repositionDefaultNodes() {
    const $can = $("#canvas"); const h = $can.height(); const w = $can.width();
    const $start = $("#node0"), $end = $("#node1");
    if ($start.length) { $start.css({ left: 20, top: (h - 60) / 4 }); nodeMap["node0"].top = (h - 60) / 4; }
    if ($end.length) { $end.css({ left: w - 140, top: (h - 60) / 4 }); nodeMap["node1"].top = (h - 60) / 4; }
    jp.repaintEverything();
}

function bindResize() {
    $(window).on("resize", () => repositionDefaultNodes());
    let lastDevicePixelRatio = window.devicePixelRatio;
    setInterval(() => { if (window.devicePixelRatio !== lastDevicePixelRatio) { lastDevicePixelRatio = window.devicePixelRatio; repositionDefaultNodes(); } }, 300);
}

function bindEvents() {

    $("#btnAdd").on("click", function () {
        $("#nodeName").attr("title", '');
        $("#approveType").val("");
        $("#approveTarget").val("");
        $("#execCondition").val("");
        $("#logicOp").val("");
        $("#logicValue").val("");
        editingNode = null;
        $("#modalTitle").text("New Node");
        $("#nodeName").val("");
        $("#chkEnd").prop("checked", false);
        renderPrevSelect(); if (activeNode) $("#prevNode").val(activeNode.attr("id")); modal.show();
    });

    $("#btnDel").on("click", function () {
        if (!activeNode) { alert("Please click a node first!"); return; }
        const id = activeNode.attr("id"); if (id === "node0" || id === "node1") { alert("Default node cannot be deleted!"); return; }
        if (confirm("Delete this node and its connections?")) { removeNodeAndConns(id); activeNode = null; }
    });

    $("#modalOk").on("click", function () {
        const name = $.trim($("#nodeName").val());
        if (!name) { alert("Node name cannot be empty!"); return; }
        const condition = { c: $("#execCondition").val(), op: $("#logicOp").val(), val: $("#logicValue").val() };

        if (condition.c == "" && condition.op == "" && condition.val != "") { Swal.fire("Error", "You input the value but not set conditon and logic!"); return; }
        if (condition.c == 'int1Value' && !isNumeric(condition.val)) { Swal.fire("Error", "The value cannot be convert to number!"); return; }
        if (condition.c == 'int1Value' && !isNumeric(condition.val)) { Swal.fire("Error", "The value cannot be convert to datetime!"); return; }
        if (condition.c == 'int1Value' && condition.op == "in") { Swal.fire("Error", "The contain only suitable for string value!"); return; }
        if (condition.c == 'date1Value' && condition.op == "out") { Swal.fire("Error", "The exclude only suitable for string value!"); return; }
        if ((condition.c != "" && condition.op == "") || (condition.c == "" && condition.op != "")) { Swal.fire("Error", "The condition and the logic need to be set at the same time!"); return; }

        const approve = { type: $("#approveType").val(), target: $("#approveTarget").val() };
        if ((approve.type == "" && approve.target != "") || (approve.type != "" && approve.target == "")) {
            Swal.fire("Error", "The approve type and approve target cannot be null!"); return;
        }

        const prevId = $("#prevNode").val(); const toEnd = $("#chkEnd").is(":checked");
        if (editingNode) {
            const id = editingNode.attr("id");
            $.each(connMap, function (src, tgts) { const idx = tgts.indexOf(id); if (idx !== -1 && src !== prevId) disconnectIfExist(src, id); });
            connectAuto(prevId, id); if (toEnd) connectAuto(id, "node1"); else disconnectIfExist(id, "node1");
            nodeMap[id].name = name; nodeMap[id].condition = condition; nodeMap[id].approve = approve;
            editingNode.text(name); modal.hide(); return;
        }
        const $prev = $("#" + prevId); const pos = $prev.position(); const cnt = followOffset[prevId] || 0;
        const newTop = cnt === 0 ? pos.top : pos.top + cnt * 80; followOffset[prevId] = cnt + 1;
        const newNode = addNode("task", pos.left + 180, newTop, name);
        nodeMap[newNode.attr("id")].condition = condition; nodeMap[newNode.attr("id")].approve = approve;
        connectAuto(prevId, newNode.attr("id")); if (toEnd) connectAuto(newNode.attr("id"), "node1");
        modal.hide();
    });

    $("#btnSave").on("click", () => {
        const msg = validateFlow();
        const $box = $("#saveAlert");
        if (msg)
        {
            $box.text(msg).removeClass("d-none");
            setTimeout(() => $box.addClass("d-none"), 4000);
            return;
        }
        $box.addClass("d-none");
        saveFlow();
    });

    $("#canvas").on("click", ".node", function (e) {
        e.stopPropagation();
        if (activeNode) activeNode.removeClass("selected");
        $(this).addClass("selected"); activeNode = $(this);
    });

    $("#canvas").on("dblclick", ".node", async function () {
        const $n = $(this); const id = $n.attr("id");
        if (id === "node0" || id === "node1") { alert("Default node cannot be modified!"); return; }
        editingNode = $n; $("#modalTitle").text("Edit Node");
        $("#nodeName").val(nodeMap[id].name);
        $("#nodeName").attr("title", 'nodePK: ' + id);
        const cond = nodeMap[id].condition || { c: "value1", op: ">", val: "" };
        $("#execCondition").val(cond.c); $("#logicOp").val(cond.op); $("#logicValue").val(cond.val);
        const appr = nodeMap[id].approve || { type: "role", target: "leader1" };
        $("#approveType").val(appr.type);

        await getApproveObject();

        $("#approveTarget").val(appr.target);
        console.log($("#approveTarget").val());
        $("#chkEnd").prop("checked", connMap[id].includes("node1"));
        renderPrevSelect();
        $.each(connMap, function (src, tgts) { if (tgts.includes(id)) $("#prevNode").val(src); });
        modal.show();
    });

    $("#approveType").on("change", function () {
        getApproveObject();
    })

    getCondition();
}

function validateFlow() {
    const userNodes = Object.keys(nodeMap).filter(id => id !== "node0" && id !== "node1");
    if (userNodes.length === 0) return "No user nodes in the flow, save aborted!";
    for (const id of userNodes) {
        const hasNext = connMap[id].length > 0; const hasEnd = connMap[id].includes("node1");
        if (!hasNext && !hasEnd) return `Node【${nodeMap[id].name}】has no successor and is not marked as end, save aborted!`;
    }
    return "";
}

function renderPrevSelect() {
    const $sel = $("#prevNode").empty();
    $.each(nodeMap, function (id, node) { if (id !== "node1") $sel.append(`<option value="${id}">${node.name}</option>`); });
}

function connectAuto(sourceId, targetId) {
    if (connMap[sourceId].includes(targetId)) return;
    jp.connect({ source: sourceId, target: targetId, anchors: ["Right", "Left"] });
    connMap[sourceId].push(targetId);
}

function disconnectIfExist(sourceId, targetId) {
    const idx = connMap[sourceId].indexOf(targetId); if (idx === -1) return;
    jp.getConnections().forEach(c => { if (c.source.id === sourceId && c.target.id === targetId) jp.deleteConnection(c); });
    connMap[sourceId].splice(idx, 1);
}

function removeNodeAndConns(nodeId) {
    const outArr = connMap[nodeId].slice(); outArr.forEach(tgt => disconnectIfExist(nodeId, tgt));
    $.each(connMap, function (src, tgts) { if (tgts.includes(nodeId)) disconnectIfExist(src, nodeId); });
    jp.remove(nodeId); delete nodeMap[nodeId]; delete connMap[nodeId]; delete followOffset[nodeId];
}

function saveFlow() {
    let templateID = Number(request("tid"));

    for (let id in nodeMap) {
        const node = nodeMap[id];
        if (node.type === 'start' || node.type === 'end') continue;
        const a = node.approve || {};
        if (!a.type || !a.target) {
            Swal.fire('Error', 'Approve Type and Approve Target cannot be null！', 'error');
            return;               // 中断保存
        }

        const c = node.condition || {};
        const hasC = !!c.c;
        const hasOp = !!c.op;
        if (hasC !== hasOp) {          // 一个有一个没有
            Swal.fire("Error", "Node:" + node.name + "'s condition and logic need to be set at the same time！", "error");
            return;
        }
    }

    const nodeArr = Object.keys(nodeMap).map(id => {
        const $node = $(`#${id}`);
        const domLeft = parseInt($node.attr('data-left') || $node.css('left'), 10);
        const domTop = parseInt($node.attr('data-top') || $node.css('top'), 10);

        nodeMap[id].left = domLeft;
        nodeMap[id].top = domTop;

        let prevID = ""; $.each(connMap, (src, tgts) => { if (tgts.includes(id)) prevID = src; });
        const c = nodeMap[id].condition || {};
        const a = nodeMap[id].approve || {};
        return {
            templateID,
            nodePK: id,
            name: nodeMap[id].name,
            type: nodeMap[id].type,
            left: domLeft,  // 使用DOM读取的位置
            top: domTop,    // 使用DOM读取的位置
            condition: `${c.c || ""}|${c.op || ""}|${c.val || ""}`,
            approve: `${a.type || ""}|${a.target || ""}`,
            prevID,
            isEnd: connMap[id].includes("node1") ? 1 : 0
        };
    });
    console.log("output：", JSON.stringify({ nodes: nodeArr }, null, 2));
    submitFlow(nodeArr)
}

jQuery(function ($) {

});

function getFlow() {
    let templateID = request("tid");

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "query roleDel";
    obj.reqData = [templateID];
    var url = "/core/api/Flows/nodeList";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            var nodesList = new Object();
            nodesList.nodes = result.resData;
            loadFlow(nodesList);
        }
        else {
            loadFlow("");
        }
    })
}

function submitFlow(nodeArr) {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "nodeSet";
    obj.reqData = nodeArr;
    var url = "/core/api/Flows/nodeSet";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            Swal.fire("Success!", "Flow nodes have been saved!").then(function () { });
        }
        else {
            Swal.fire("Error!", result.message);
        }
    })
}

function getCondition() {
    let templateID = request("tid");
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "nodeSet";
    obj.reqData = [templateID];
    var url = "/core/api/Flows/tempDetail";
    httpPost(url, obj, function (result) {
        if (result.status == 1) {
            let t = result.resData[0];
            if (t.int1Title == undefined && t.str1Title == undefined && t.date1Title == undefined) {
                $("#execCondition").html(`<option value="">No valid condition</option>`);
            }
            else if (t.int1Title.trim() == "" && t.str1Title.trim() == "" && t.date1Title.trim() == "") {
                $("#execCondition").html(`<option value="">No valid condition</option>`);
            }
            else {
                let optList
                    = (t.int1Title.trim() != `` ? `<option value="int1Value">${t.int1Title}</option>` : ``)
                    + (t.str1Title.trim() != `` ? `<option value="str1Value">${t.str1Title}</option>` : ``)
                    + (t.date1Title.trim() != `` ? `<option value="date1Value">${t.date1Title}</option>` : ``);
                $("#execCondition").append(optList);
            }
        }
        else {
            Swal.fire("Error!", result.message);
        }
    })
}

function getApproveObject() {
    return new Promise((resolve) => {
        $("#approveTarget").html("")
        let apType = $("#approveType").val();
        switch (apType) {
            case "role":
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "query getField";

                obj.reqData = [""];
                var url = "/core/api/Osrzs/getAllRole";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        let listHtml = "";
                        for (var i = 0; i < result.resData.length; i++) {
                            listHtml += "<option value=" + result.resData[i]["objType"] + ":" + result.resData[i]["objID"] + ">" + result.resData[i]["objName"] + " (" + result.resData[i]["objType"] + " Role)</option>";
                        }
                        $("#approveTarget").html(listHtml)
                    }
                    else {
                        $("#approveTarget").html("")
                    }
                    resolve();
                })
                break;
            case "position":
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "getOrgInfo";
                var subData = new Object();
                subData.id = ""; subData.type = "post"; subData.order = "";
                obj.reqData = [subData];
                var url = "/core/api/Orgs/getOrgDoc";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        let listHtml = "";
                        for (var i = 0; i < result.resData.length; i++) {
                            listHtml += "<option value=" + result.resData[i]["orgID"] + ">" + result.resData[i]["parentName"] + " -- " + result.resData[i]["orgName"] + "</option>";
                        }
                        $("#approveTarget").html(listHtml)
                    }
                    else {
                        $("#approveTarget").html("")
                    }
                    resolve();
                })
                break;
            case "reportLine":
                $("#approveTarget").html(`<option value="dept1">The Superior of department</option>
                                      <option value="dept2">The Superior of higer Department</option>
                                      <option value="unit1">The head of Organization</option>
                                      <option value="unit2">The head of higer Organization</option>
                                      <option value="oneself">Oneself</option>`);
                resolve();
                break;
            case "person":
                var obj = new Object();
                obj.uToken = localStorage.getItem("uToken");
                obj.action = "getPsn";
                obj.reqData = [""];
                var url = "/core/api/Psns/getPsn";
                httpPost(url, obj, function (result) {
                    if (result.status == 1) {
                        let listHtml = "";
                        for (var i = 0; i < result.resData.length; i++) {
                            listHtml += "<option value=" + result.resData[i]["psnID"] + ">" + result.resData[i]["unitName"] + " -- " + result.resData[i]["deptName"] + " -- " + result.resData[i]["psnName"] + "</option>";
                        }
                        $("#approveTarget").html(listHtml)
                    }
                    else {
                        $("#approveTarget").html("")
                    }
                    resolve();
                })
                break;
        }
    })
}