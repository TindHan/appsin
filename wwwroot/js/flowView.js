/* ==========  全局变量  ========== */
const jp = jsPlumb.getInstance({ Container: "canvas", Connector: ["Flowchart", { cornerRadius: 5 }], Anchors: ["Right", "Left"] });
let nodeIdSeq = 2;             // 0=start 1=end 从2开始
let activeNode = null;
let nodeMap = {};
let editingNode = null;
let connMap = {};
let followOffset = {};

const modal = new bootstrap.Modal(document.getElementById('nodeModal'));

/* ==========  初始化  ========== */
$(function () {
    jp.setZoom(1);
    initDefaultNodes();
    bindEvents();
    bindResize();

});

/* 默认节点（ID固定） */
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
                console.log(`固定节点${id}位置更新:`, newLeft, newTop);
            }
        }
    });
    nodeMap[id] = { type: type, name: name, left: x, top: y, condition: {}, approve: {} };
    connMap[id] = [];
    jp.addEndpoint($div, { anchors: ["Right"], endpoint: ["Dot", { radius: 6 }], paintStyle: { fill: "#007bff" }, isSource: true, maxConnections: -1 });
    jp.addEndpoint($div, { anchors: ["Left"], endpoint: ["Dot", { radius: 6 }], paintStyle: { fill: "#007bff" }, isTarget: true, maxConnections: -1 });
    return $div;
}

/* 普通节点创建（ID 自增） */
function addNode(type, x, y, name) {
    const id = "node" + (nodeIdSeq++);
    const $div = $(`<div class="node task" id="${id}">${name}</div>`);
    $div.css({ left: x, top: y });
    $("#canvas").append($div);
    // 普通节点拖动结束事件 - 修复位置获取逻辑
    jp.draggable($div, {
        containment: "parent",
        stop: function () {
            const $node = $(this);
            const newLeft = parseInt($node.css('left'), 10);
            const newTop = parseInt($node.css('top'), 10);

            if (nodeMap[id]) {
                nodeMap[id].left = newLeft;
                nodeMap[id].top = newTop;
                console.log(`普通节点${id}位置更新:`, newLeft, newTop);
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

    $("#canvas").on("click", ".node", async function (e) {
        e.stopPropagation();
        if (activeNode) activeNode.removeClass("selected");
        $(this).addClass("selected"); activeNode = $(this);

        let instanceID = request("iid");
        const $n = $(this);
        const id = $n.attr("id");

        if (id == "node0" || id == "node1") {
            $("#nodeContent").html("");
        }
        else {
            var obj = new Object();
            obj.uToken = localStorage.getItem("uToken");
            obj.action = "nodeDetail";
            obj.reqData = [instanceID, id];
            var url = "/core/api/Flows/nodeDetail";
            httpPost(url, obj, function (result) {
                if (result.status == 1) {
                    let nodeContent = `Node name: ${result.resData[0]} | approver: ${result.resData[1]} | condition: ${(result.resData[2] == '' ? 'No Set' : result.resData[2])} `;
                    $("#nodeContent").html(nodeContent);
                }
                else {
                    Swal.fire("Error", result.message);
                }
            })
        }

    });
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


jQuery(function ($) {
    getFlow();
});

function getFlow() {
    let instanceID = request("iid");

    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "instNodeList";
    obj.reqData = [instanceID];
    var url = "/core/api/Flows/instNodeList";
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