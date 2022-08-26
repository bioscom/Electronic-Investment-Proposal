using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Web;
using System.Data.Common;

public class loginUser
{
    public enum statusx
    {
        loginFailed = 0,
        loginSucceed = 1,
        idIsNotFound = 2,
        statusDisabld = 3,
        statusLocked = 4,
        statusUnKnown = 5  
    };

    public struct loginRet
    {
        public statusx eStatus;
        public appUsers eUserInfo;
    }

    public loginRet verifyAppUser()
    {
        loginRet eRet = new loginRet();
        eRet.eStatus = statusx.loginFailed;
        eRet.eUserInfo = new appUsers();

        try
        {
            appUserMgt oAppUsersMgt = new appUserMgt();
            appUsers IamFound = oAppUsersMgt.objGetUserByUserDomainName(Apps.LoginIDNoDomain(HttpContext.Current.User.Identity.Name)); 

            if (IamFound.m_sUserName == null)
            {
                eRet.eStatus = statusx.idIsNotFound;
            }
            else
            {
                if (IamFound.m_sUserName != null)
                {
                    string p_sUserId = IamFound.m_sUserName;

                    eRet.eStatus = statusx.loginSucceed;
                    eRet.eUserInfo = IamFound;
                    httpSessionx oInitSessn = new httpSessionx(HttpContext.Current.Session, eRet.eUserInfo);

                    switch (IamFound.m_iStatus)
                    {
                        case (int)appUsersRoles.userStatus.activeUser:
                            eRet.eStatus = statusx.loginSucceed;
                            //eRet.eUserInfo = IamFound; //OnlineUserAccess.objGetOnlineUser(apps.Apps.LoginIDNoDomain(HttpContext.Current.User.Identity.Name));
                            //httpSessionx oInitSessn = new httpSessionx(HttpContext.Current.Session, eRet.eUserInfo);
                            break;

                        case (int)appUsersRoles.userStatus.disabledMe:
                            eRet.eStatus = statusx.statusDisabld;
                            break;

                        case (int)appUsersRoles.userStatus.lockedUser:
                            eRet.eStatus = statusx.statusLocked;
                            break;

                        default:
                            eRet.eStatus = statusx.statusUnKnown;
                            break;
                    }
                }
                else
                {
                    eRet.eStatus = statusx.idIsNotFound;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return eRet;
    }
}
