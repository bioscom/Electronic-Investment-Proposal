using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for SystemValidator
/// The essence of this class is to ensure that proper settings are in place, before EIP can run properly.
/// This is necessary due to experience learnt from the testing and usage of EIP.
/// 27-10-2010
/// </summary>
public class SystemValidator
{
	public SystemValidator()
	{
		
	}

    //The following roles must have users created before the application can be used.
    //Where user is deleted, warnings must be sent to the system administrator and BPOs of the imminent danger of deleting such user.
    //Business Process Owner, GM Regional Planning, REVP, MD, Corporate Planning Manager for EPNigeria OUs, EPG IP Tracker, 
    // VP Finance and GM Finance For each OUs

    private DataTable RegisteredRolesFound()
    {        
        string sql = "SELECT EIP_USERMGT.IDUSERMGT, CPDMS_SHELLCOMPANIES.COMPANYID, EIP_USERROLES.USERROLESID, EIP_USERMGT.USERNAME, ";
        sql += "EIP_USERMGT.FULLNAME, EIP_USERMGT.USERMAIL, EIP_USERROLES.ROLES, CPDMS_SHELLCOMPANIES.COMPANYNAME, CPDMS_FUNCTIONS.FUNCTION ";
        sql += "FROM EIP_USERMGT INNER JOIN ";
        sql += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql += "CPDMS_SHELLCOMPANIES ON EIP_USERMGT.COMPANYID = CPDMS_SHELLCOMPANIES.COMPANYID INNER JOIN ";
        sql += "CPDMS_FUNCTIONS ON EIP_USERMGT.FUNCTIONID = CPDMS_FUNCTIONS.FUNCTIONID";

        return DataAccess.ExecuteQueryCommand(sql);
    }

    //public void ValidateRegisteredRoles()
    //{
    //    //To validate the Registered Roles Found, loop through the RegisteredRolesFound() method
    //    //to check if the following roles are registered: Business Process Owner, GM Regional Planning, REVP, MD OUs, 
    //    //Corporate Planning Manager OUs, GM Finance OUs, EPG IP Tracker and VP Finance

    //    int[] MandatoryUsers = {(int)appUsersRoles.userRole.Business_Process_Owner, (int)appUsersRoles.userRole.GM_Regional_Planning, (int)appUsersRoles.userRole.REVP, 
    //                            (int)appUsersRoles.userRole.Technical_Planning_Manager, (int)appUsersRoles.userRole.EPG_IP_Tracker};

    //    DataTable dt = RegisteredRolesFound();
    //    foreach (Int32 i in MandatoryUsers)
    //    {
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            if (Convert.ToInt32(dr["USERROLESID"]) == MandatoryUsers[i]) 
    //            {
    //                break;
    //            }
    //            else
    //            {

    //            }
    //        }
    //    }

    //    //For Finance guys

    //    //int[] FinanceMandatoryUsers = {eipUserRoles.iGM, eipUserRoles.iVP};
    //    //foreach (Int32 j in FinanceMandatoryUsers)
    //    //{
    //    //    foreach (DataRow dr in dt.Rows)
    //    //    {
    //    //        if ((Convert.ToInt32(dr["USERROLESID"]) == FinanceMandatoryUsers[j]) && (dr["FUNCTION"].ToString() == cpdmsFunctionsNames.Finance))
    //    //        {
    //    //            break;
    //    //        }
    //    //        else
    //    //        {

    //    //        }
    //    //    }
    //    //}
        
    //}
}
