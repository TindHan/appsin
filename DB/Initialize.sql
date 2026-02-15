--This sql is for initializing appsin main program to start your own system.
--After initialization, only one user Tindhan left and he has all menu permission, you can use this user login appsin to create your organization and users. 
--Tindhan's username is tindhan, password is Tind123!@#. Now, login and start your own appsin!
BEGIN TRY
    BEGIN TRANSACTION;

    DELETE FROM app_appMain WHERE appID <> 10001;
    DELETE FROM org_orgMain WHERE orgID >= 10008;
    DELETE FROM psn_psnMain WHERE psnID > 10001;
    DELETE FROM sys_dataOsrz WHERE osrzObjType <> 'psn' AND osrzObjID <> 10000 AND osrzStatus <> 1;
    DELETE FROM sys_menuOsrz WHERE osrzObjType <> 'psn' AND osrzObjID <> 10000 AND osrzStatus <> 1;
    DELETE FROM sys_menu WHERE menuAppID <> 10001;

    TRUNCATE TABLE api_apiOsrz;
    TRUNCATE TABLE api_uselog;
    TRUNCATE TABLE app_goToLog;
    TRUNCATE TABLE app_messageRead;
    TRUNCATE TABLE app_messages;
    TRUNCATE TABLE app_noticeRead;
    TRUNCATE TABLE app_notices;
    TRUNCATE TABLE app_taskProgress;
    TRUNCATE TABLE app_tasks;
    TRUNCATE TABLE flow_approveLog;
    TRUNCATE TABLE flow_instance;
    TRUNCATE TABLE flow_instanceNode;
    TRUNCATE TABLE flow_template;
    TRUNCATE TABLE flow_tempNode;
    TRUNCATE TABLE psn_actLog;
    TRUNCATE TABLE psn_captcha;
    TRUNCATE TABLE sys_RSAKey;
    TRUNCATE TABLE sys_tokenMain;
    TRUNCATE TABLE sys_tokenVerify;

    COMMIT TRANSACTION;
    PRINT 'All data is cleared, only Tindhan can be used to login system!';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;


