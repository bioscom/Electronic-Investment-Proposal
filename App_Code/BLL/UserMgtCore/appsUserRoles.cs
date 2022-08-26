using System;

/// <summary>
/// Summary description for appsUser
/// </summary>

public class appUsersRoles
{
    public enum userRole
    {
        //Note: New role (Finance_Director just added August 2013
        SPCA_Support = 1,
        Controllers = 2,
        HSE_Support = 3,
        TAX_Support = 4,
        LEGAL_Support = 5,
        Economics_Support = 6,
        Treasury_Support = 7,
        Security_Support = 8,
        Line_Team_Lead = 9,
        Business_Process_Owner = 11,
        IP_Initiator = 12,
        GM_Regional_Planning = 21,
        Finance_Signature = 22,
        Administrator = 41,
        BOM = 61,
        REVP = 81,
        VP = 82, //Note VPs are now GMs
        MD = 83,
        Functional_Planner = 84,
        Technical_Planning_Manager = 121,
        //EPG_IP_Tracker = 122,
        CERP = 123,
        Finance_Director = 141,
        SCM = 142,
        IT = 143,
        Auditor = 144,
    };

    public static string userRoleDesc(userRole eRole)
    {
        string sRet = "UnKnown Account";
        try
        {
            switch (eRole)
            {
                case userRole.Administrator: sRet = "System Administrator"; break;
                case userRole.Business_Process_Owner: sRet = "Business Process Owner"; break;
                case userRole.HSE_Support: sRet = "HSE Support"; break;
                case userRole.Controllers: sRet = "Controller Support"; break;
                case userRole.TAX_Support: sRet = "Tax Support"; break;
                case userRole.LEGAL_Support: sRet = "Legal Support"; break;
                case userRole.SPCA_Support: sRet = "SPCA Support"; break;
                case userRole.Economics_Support: sRet = "Economics Support"; break;
                case userRole.Treasury_Support: sRet = "Treasury Support"; break;
                case userRole.Security_Support: sRet = "Security Support"; break;
                case userRole.Line_Team_Lead: sRet = "Line Team Lead"; break;
                case userRole.Technical_Planning_Manager: sRet = "Technical Planning Manager"; break;
                case userRole.CERP: sRet = "Capital Expenditure Review Panel"; break;
                case userRole.Finance_Signature: sRet = "Finance Signature"; break;
                case userRole.VP: sRet = "General Manager"; break;
                //case userRole.GM: sRet = "GM"; break;
                case userRole.MD: sRet = "MD"; break;
                case userRole.REVP: sRet = "Vice President"; break;
                case userRole.GM_Regional_Planning: sRet = "GM Regional Planning"; break;
                case userRole.IP_Initiator: sRet = "IP Initiator"; break;
                case userRole.BOM: sRet = "BOM"; break;
                //case userRole.EPG_IP_Tracker: sRet = "EPG IP Tracker"; break;
                case userRole.Functional_Planner: sRet = "Functional Planner"; break;
                case userRole.IT: sRet = "IT Support"; break;
                case userRole.SCM: sRet = "SCM Support"; break;
                case userRole.Auditor: sRet = "Auditor"; break;
                case userRole.Finance_Director: sRet = "Finance Director"; break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    public static bool MandatoryFunctionalSupport(userRole eRole)
    {
        //TODO: When there is an addition to Mandatory Functional Support, the change will be effected here by changing false to true 
        bool bRet = false;
        try
        {
            switch (eRole)
            {
                case userRole.HSE_Support: bRet = false; break;
                case userRole.SPCA_Support: bRet = false; break;
                case userRole.Security_Support: bRet = false; break;
                case userRole.IT: bRet = false; break;
                case userRole.SCM: bRet = false; break;

                case userRole.Controllers: bRet = true; break;
                case userRole.TAX_Support: bRet = true; break;
                case userRole.Treasury_Support: bRet = true; break;
                case userRole.Economics_Support: bRet = true; break;
                case userRole.LEGAL_Support: bRet = true; break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return bRet;
    }

    public enum userStatus
    {
        activeUser = 1,
        lockedUser = 0,
        disabledMe = 3,
        unKnownMe = 4
    };

    public static string userStatusDesc(userStatus eUserStatus)
    {
        string sRet = "UnKnown Account";
        try
        {
            switch (eUserStatus)
            {
                case userStatus.activeUser: sRet = "Active Account"; break;
                case userStatus.disabledMe: sRet = "Deleted Account"; break;
                case userStatus.lockedUser: sRet = "Locked Account"; break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }

    public bool grantPageAccess(string[] sAccountRole, userRole eMyRole)
    {
        bool bRet = false;
        try
        {
            //if ((eMyRole == userRole.Administrator) || (eMyRole == userRole.Business_Process_Owner))
            //{
            //    bRet = true;
            //}
            //else
            //{
                foreach (string sId in sAccountRole)
                {
                    if (eMyRole.ToString() == sId)
                    {
                        bRet = true;
                        break;
                    }
                }
            //}
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }

    public bool grantPageAccessAdminInit(string[] sAccountRole, userRole eMyRole)
    {
        bool bRet = false;
        try
        {
            if ((eMyRole == userRole.Administrator) || (eMyRole == userRole.IP_Initiator))
            {
                bRet = true;
            }
            else
            {
                foreach (string sId in sAccountRole)
                {
                    if (eMyRole.ToString() == sId)
                    {
                        bRet = true;
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }

        return bRet;
    }
}