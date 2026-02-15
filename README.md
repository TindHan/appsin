# appsin
APPSIN Cross-Domain Heterogeneous Application Integrated Information System. Its most distinctive feature is that customized applications of this system can be developed and deployed using any programming language, database, middleware, or server, and an integrated system usage experience can be obtained.

This system is targeted at department-level applications of enterprises with less than 1,000 users (or more). Its most distinctive feature is that customized applications of this system can be developed and deployed using any programming language, database, middleware, or server, and an integrated system usage experience can be obtained. This set of programs has already The basic functions required for an enterprise information management system, such as organizational management, user management, permission management, log management, and login and logout, have been completed. The enterprise only needs to develop and deploy the actual business functions it requires using the familiar technology stack and register the application In the main program of APPSIN, you can use your self-developed integrated applications within the APPSIN system.

The original design intention of this system is to address the issue of rapidly developing customized applications within the current framework of artificial intelligence capabilities to meet the informatization needs of enterprises. Although artificial intelligence currently has strong development capabilities, for demands with high business complexity such as enterprise information management systems, At present, effective understanding and development are still impossible. This system has completed the complex and common parts of the underlying business logic. When enterprises use this system for customized development, they only need to focus on the development of the actual business part, which greatly simplifies the business complexity and fully utilizes the current artificial intelligence provided Develop the ability to quickly build an information system that suits one's own needs.

This system contains two programs: the APPSIN main program and the integrated application demonstration program. This programs is the main program. Both programs are in English and currently do not support multiple languages.

The front end of the APPSIN main program is developed using bootstrap and jquery, and the back end is developed with NET10, Integrating the front-end and back-end into a single program，the database adopts MS SQL SERVER 2016, and the development tool is Visual Studio 2022.

The APPSIN integrated application demonstration program is developed using React and Antd, and the backend developed with NET10, the front-end and back-end are separate programs, the database uses My SQL 8.0.18, and the development tool is Visual studio 2022.

The main program of APPSIN consists of two functional groups. One is the business function, which mainly includes desktop applications, organization management, user management, permission management, approval flow engine and logs, etc. Business functions can be logged in with the full-permission user tindhan; The other one is the integrated application management console, which mainly includes integrated application registration, integrated application menu management, integrated application logs, integrated application usage statistics and other functions. The two have different login entries. There is a link to switch login pages at the bottom of the login page. 

The integrated application demonstration program consists of two demonstration applications. One is event registration, which includes event management and event registration management. The other one is leave management, which includes leave application and leave approval query (the approval process is implemented by calling the approval flow driver engine of the main program).

The main program and the integrated application use the HTML iframe component to achieve page integration. In the main program, when the menu of the integrated application is clicked, the main program will open the iframe component and request the address on the pre-registered integrated application menu in this component.

More information, you can look for the introducation page "wwwroot/ReadMe_EN.html", thank you for your attention.

Chinese introduction：
APPSIN跨域异构应用一体化集成系统介绍

该系统定位于1000用户以下的企业部门级应用，其最大特点是可以使用任何开发语言、数据库、中间件、服务器开发和部署该系统的定制化应用，并获得一体化系统的使用体验。该套程序已经 完成了企业信息管理系统所需的基本的组织管理、用户管理、权限管理、日志管理，登录登出等基本功能，企业只需使用熟悉的技术栈开发和部署自己所需要的实际业务功能，并将应用注册到 APPSIN主程序中，便可在APPSIN系统内使用自己开发的集成应用。

该系统设计初衷是解决在目前人工智能能力框架下，进行快速开发定制化应用满足企业信息化需求。目前人工智能虽然具备较强的开发能力，但是对于企业信息管理系统这样业务复杂度较高的需求， 目前还无法进行有效的理解和开发。该系统已经完成底层业务逻辑复杂和通用部分，企业在使用本系统定制开发时，只需关注实际业务部分的开发，大幅度简化业务复杂度，充分利用目前人工智能提供的 开发能力，从而快速构建起适合自身需要的信息系统。

该系统包含APPSIN主程序和集成应用演示程序两套程序文件，本套程序是主程序。两套程序均为英文版，目前尚未支持多语言。

APPSIN主程序前端采用bootstrap和jquery开发，后端采用.NET10开发，前后端集成为一套程序，数据库采用MS SQL SERVER 2016，开发工具为Visual Studio 2022。

APPSIN集成应用演示程序采用React和Antd开发，后端采用.NET10开发，前后端为独立的程序，数据库采用My SQL 8.0.18，开发工具为Visual studio 2022。

APPSIN主程序包含两个功能组，一个是业务功能，主要包含桌面应用、组织管理、用户管理、权限管理、审批流驱动引擎和日志管理等功能；另外一个是集成应用管理控制台，主要包含集成应用注册、集成应用菜单管理、集成应用日志、集成应用使用统计等功能。两者有不同的登录入口，登录界面下方有切换登录页面的链接。 业务功能登录入口为IP:PORT/pages/users/ulogin.html；集成应用管理控制台登录入口IP:PORT/pages/console/alogin.html

集成应用演示程序包含两个演示应用，一个是活动报名，包含活动管理、活动报名管理；另一个是请假管理，包含请假申请，请假审批查询（审批过程采用调用主程序的审批流驱动引擎来实现）

主程序和集成应用程序之间采用HTML的iframe组件来实现页面的集成，主程序中，当点击集成应用的菜单时，主程序会开启iframe组件，并在该组件中请求提前注册好的集成应用菜单上的地址。

如有更多疑问，您可以在程序文件“wwwroot/ReadMe_CN.html”中查找介绍内容。感谢您的关注。

