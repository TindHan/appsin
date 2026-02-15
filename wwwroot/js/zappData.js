jQuery(function ($) {
    getData();
});

function getData() {
    var obj = new Object();
    obj.uToken = localStorage.getItem("uToken");
    obj.action = "getConsoleNum";
    obj.reqData = [""];
    var url = "/core/api/Console/getConsoleNum";
    httpPost(url, obj, function (result) {
        appLoad(result.resData[0]);
        apiLoad(result.resData[0]);
        loginLoad(result.resData[0]);
    })
}

function loginLoad(data) {
    var dom = document.getElementById('loginData');
    var myChart = echarts.init(dom, null, {
        renderer: 'canvas',
        useDirtyRect: false
    });
    var app = {};

    var option;

    option = {
        title: {
            text: ''
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: ['Login times', 'Login users']
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        toolbox: {
            feature: {
                saveAsImage: {}
            }
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: [getPrevMonth(5).toString(), getPrevMonth(4).toString(), getPrevMonth(3).toString(), getPrevMonth(2).toString(), getPrevMonth(1).toString(), getPrevMonth(0).toString(),]
        },
        yAxis: {
            type: 'value'
        },
        series: [
            {
                name: 'Login times',
                type: 'line',
                stack: 'Total',
                data: [data.data1[0].pre5Month, data.data1[0].pre4Month, data.data1[0].pre3Month, data.data1[0].pre2Month, data.data1[0].pre1Month, data.data1[0].thisMonth]
            },
            {
                name: 'Login users',
                type: 'line',
                stack: 'Total',
                data: [data.data2[0].pre5Month, data.data2[0].pre4Month, data.data2[0].pre3Month, data.data2[0].pre2Month, data.data2[0].pre1Month, data.data2[0].thisMonth]
            }
        ]
    };

    if (option && typeof option === 'object') {
        myChart.setOption(option);
    }

    window.addEventListener('resize', myChart.resize);
}

function appLoad(data) {

    let titles = [];
    let seriesData = [];
    data.data3.forEach(function (t) {
        titles.push(t.objName);

        let oneData = new Object();
        oneData.name = t.objName;
        oneData.type = 'line';
        oneData.stack = 'Total';
        oneData.data = [t.pre5Month, t.pre4Month, t.pre3Month, t.pre2Month, t.pre1Month, t.thisMonth]
        seriesData.push(oneData);
    })


    var dom = document.getElementById('appUse');
    var now = new Date();
    var myChart = echarts.init(dom, null, {
        renderer: 'canvas',
        useDirtyRect: false
    });
    var app = {};

    var option;

    option = {
        title: {
            text: ''
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: titles
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        toolbox: {
            feature: {
                saveAsImage: {}
            }
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: [getPrevMonth(5).toString(), getPrevMonth(4).toString(), getPrevMonth(3).toString(), getPrevMonth(2).toString(), getPrevMonth(1).toString(), getPrevMonth(0).toString(),]
        },
        yAxis: {
            type: 'value'
        },
        series: 
            seriesData
        
    };

    if (option && typeof option === 'object') {
        myChart.setOption(option);
    }

    window.addEventListener('resize', myChart.resize);
}

function apiLoad(data) {

    let titles = [];
    let seriesData = [];
    data.data4.forEach(function (t) {
        titles.push(t.objName);

        let oneData = new Object();
        oneData.name = t.objName;
        oneData.type = 'line';
        oneData.stack = 'Total';
        oneData.data = [t.pre5Month, t.pre4Month, t.pre3Month, t.pre2Month, t.pre1Month, t.thisMonth]
        seriesData.push(oneData);
    })

    var dom = document.getElementById('appApi');
    var now = new Date();
    var myChart = echarts.init(dom, null, {
        renderer: 'canvas',
        useDirtyRect: false
    });
    var app = {};

    var option;

    option = {
        title: {
            text: ''
        },
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            data: titles
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        toolbox: {
            feature: {
                saveAsImage: {}
            }
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: [getPrevMonth(5).toString(), getPrevMonth(4).toString(), getPrevMonth(3).toString(), getPrevMonth(2).toString(), getPrevMonth(1).toString(), getPrevMonth(0).toString(),]
        },
        yAxis: {
            type: 'value'
        },
        series: seriesData
    };

    if (option && typeof option === 'object') {
        myChart.setOption(option);
    }

    window.addEventListener('resize', myChart.resize);
}
function getPrevMonth(n, asStr = true) {
    if (!Number.isInteger(n) || n < 0) throw new RangeError('n 必须是负整数');
    const d = new Date();
    d.setMonth(d.getMonth() - n);   // 自动处理跨年
    return asStr
        ? `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}`
        : d.getMonth() + 1;
}
