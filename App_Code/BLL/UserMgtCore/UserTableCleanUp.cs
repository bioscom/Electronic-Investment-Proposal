using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Text;

/// <summary>
/// Summary description for UserTableCleanUp
/// </summary>
public class UserTableCleanUp
{
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();

	public UserTableCleanUp()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public struct procRet
    {
        public bool eStatus;
        public StringBuilder sb;
    }

    public procRet CleanUpTable()
    {
        procRet eRet = new procRet();

        List<appUsers> oAppUsers = oAppUserMgt.lstGetUsers();
        foreach (appUsers oAppUser in oAppUsers)
        {
            //Seek the appuser in the InformationWarehouse Database, if not found then disable user in the Local User Table.
            string[] Username = oAppUser.m_sEmail.Split('@');

            CompleteStaffDetailsInfo IamFoundOnIWH = Users.getStaffFromCompleteStaffDetails(Username[0]);
            if (string.IsNullOrEmpty(IamFoundOnIWH.m_sUserName))
            {
                //Then disable the User on the Local User Table
                eRet = DeleteUser(oAppUser);
            }  
        }
        return eRet;
    }

    private procRet DeleteUser(appUsers oAppUser)
    {
        procRet eRet = new procRet();

        List<string> IPFoundAwaiting = oProposalMgt.lstIPAwaitingThisUserSupport(oAppUser.m_iUserId, oAppUser.m_iUserRoleId);
        if (IPFoundAwaiting.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(oAppUser.m_sFullName + " was not deleted. There is/are Proposal(s) pending his/her action. You will need to reroute this(these) Proposal(s) to another user before this user can be deleted.");
            sb.Append("<br/>");
            foreach (string sResult in IPFoundAwaiting)
            {
                sb.Append(sResult);
                sb.Append("<br/>");
            }
            eRet.sb = sb;
        }
        else
        {
            bool success = oAppUserMgt.deleteUserProfile(oAppUser.m_iUserId);
            if (success)
            {
                eRet.eStatus = success;
            }
        }

        return eRet;
    }
}