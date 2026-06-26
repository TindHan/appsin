
const I18n = (function() {
    const DEFAULT_LANG = 'EN';

    const dictionaries = {
        'EN': {
            'all.New': 'New',
            'all.More': 'More...',
            'all.Save': 'Save',
            'all.Close': 'Close',
            'all.Submit': 'Submit',
            'all.Cancel': 'Cancel',
            'all.Create': 'Create',
            'all.Detail': "Detail",
            'all.Edit': "Edit",
            'all.Delete': "Delete",
            'login.welcome': 'Welcome Back !',
            'login.enjoy': 'Log in to enjoy your work time.',
            'login.Username': 'Username',
            'login.enterUser': 'Enter username',
            'login.Password': 'Password',
            'login.enterPwd': 'Enter Password',
            'login.enterCapt':'Enter captcha',
            'login.forgot': 'Forgot password?',
            'login.login': 'Log In',
            'login.noAccount': 'Don\'t have an account? Contact your administrator!',
            'login.toConsole': 'Go to console',
            'login.adminAlert': 'This entrance is for administrator',
            'login.adminUser': 'Administrator Name',
            'login.adminPwd': 'Administrator Password',
            'login.adminNote': 'If you are not administrator, please click the "Go to user platform" link below!',
            'login.Captcha': 'Captcha',
            'login.toUser': 'Go to user platform',
            'dsb.Hub': 'Hub',
            'dsb.Dashboard': 'Dashboard',
            'dsb.searchFunc': 'Search Functions',
            'dsb.Messages': 'Messages',
            'dsb.Notices': 'Notices',
            'dsb.Tasks': 'Tasks',
            'dsb.workDays': 'Working days',
            'dsb.loginTimes': 'Number of logins',
            'dsb.allTasks': 'Number of all tasks',
            'dsb.doneTasks': 'Number of completed tasks',
            'dsb.workDaysNote': 'Since the day you start to work',
            'dsb.loginTimesNote': 'Since the day you start to use this system',
            'dsb.allTasksNote': 'Since the day you start to record your tasks',
            'dsb.doneTasksNote': 'Since the day you start to complete tasks',
            'dsb.profile': 'My Profile',
            'dsb.changePwd': 'Change Password',
            'dsb.logout': 'Logout',
            'dsb.app': "Applications",
            'dsb.appData': "Applications Data",
            'dsb.appRedirect': "Apps Redirect Data",
            'dsb.apiInvoking': "APIs Invoking Data",
            'dsb.appData': "Login Data",
            'dsb.loginTimes': "Login Times",
            'dsb.loginUsers': "Login Users",
        },
        'CN': {
            'all.New': '新增',
            'all.More': '更多...',
            'all.Save': '保存',
            'all.Close': '关闭',
            'all.Submit': '提交',
            'all.Cancel': '取消',
            'all.Create': '提交',
            'all.Detail': '详情',
            'all.Edit': "修改",
            'all.Delete': "删除",
            'login.welcome': '欢迎回来 !',
            'login.enjoy': '登录系统，享受便捷办公！',
            'login.Username': '用户名',
            'login.enterUser': '请输入用户名',
            'login.enterCapt': '请输入验证码',
            'login.Password': '密码',
            'login.enterPwd': '请输入密码',
            'login.forgot': '忘记密码？',
            'login.login': '登录',
            'login.noAccount': '没有账户？请联系您的管理员获取登录授权！',
            'login.toConsole': '控制台',
            'login.adminAlert': '此门户为管理员专用',
            'login.adminUser': '管理员用户名',
            'login.adminPwd': '管理员密码',
            'login.Captcha': '验证码',
            'login.adminNote': '如果您不是管理员，请点击下面的用户门户！',
            'login.toUser': '用户门户',
            'dsb.Hub': '首页',
            'dsb.Dashboard': '仪表台',
            'dsb.searchFunc': '搜索功能',
            'dsb.Messages': '消息',
            'dsb.Notices': '通知',
            'dsb.Tasks': '任务',
            'dsb.workDays': '本单位工作天数',
            'dsb.loginTimes': '系统登录次数',
            'dsb.allTasks': '工作任务总数',
            'dsb.doneTasks': '完成工作任务数',
            'dsb.workDaysNote': '从您加入本单位开始计算',
            'dsb.loginTimesNote': '从您开始使用本系统开始计算',
            'dsb.allTasksNote': '从您开始记录工作任务开始计算',
            'dsb.doneTasksNote': '从您开始完成第一个任务开始计算',
            'dsb.profile': '我的档案',
            'dsb.changePwd': '变更密码',
            'dsb.logout': '退出',
            'dsb.app': "应用",
            'dsb.appData': "应用数据统计",
            'dsb.appRedirect': "应用使用数据",
            'dsb.apiInvoking': "接口调用数据",
            'dsb.loginData':"用户登陆数据",
            'dsb.loginTimes': "登陆次数",
            'dsb.loginUsers': "登陆用户数",
        }
    };

    function getCurrentLang() {
        let lang = localStorage.getItem('language');
        if (!lang || !dictionaries[lang]) {
            lang = DEFAULT_LANG;
            localStorage.setItem('language', lang);
        }
        return lang;
    }

    function setLang(lang) {
        if (dictionaries[lang]) {
            localStorage.setItem('language', lang);
            updatePageContent(); 
            return true;
        }
        return false;
    }

    function t(key) {
        const lang = getCurrentLang();
        return dictionaries[lang][key] || key; 
    }

    function updatePageContent() {
        document.querySelectorAll('[data-i18n]').forEach(el => {
            el.textContent = t(el.getAttribute('data-i18n'));
        });

        document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
            el.placeholder = t(el.getAttribute('data-i18n-placeholder'));
        });
        
        document.documentElement.lang = getCurrentLang().toLowerCase();
    }

    function init() {
        updatePageContent();
    }

    return {
        t: t,
        setLang: setLang,
        getLang: getCurrentLang,
        init: init
    };
})();
