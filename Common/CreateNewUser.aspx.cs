using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Common_CreateNewUser : aspnetPage
{
    shellCompanies Companies = new shellCompanies();
    IPLimit oIPLimits = new IPLimit();

    public bool success = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        bool bRet = false;
        try
        {
            string[] sPageAccess = { appUsersRoles.userRole.Administrator.ToString() };
            appUsersRoles oAccess = new appUsersRoles();
            bRet = oAccess.grantPageAccess(sPageAccess, this.oSessnx.getOnlineUser.m_eUserRole);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        if (!bRet) Response.Redirect("~/Profiles/pageDenied.aspx");

        string url = ApplicationURL.MyAppURL() + "/Common/Confirmation.aspx";

        if (!IsPostBack)
        {
            List<cFunctions> oFunctions = cFunctions.lstGetFunctions();
            foreach (cFunctions oFunction in oFunctions)
            {
                functionDropDownList.Items.Add(new ListItem(oFunction.m_sFunction, oFunction.m_iFunctionId.ToString()));
            }

            List<shellOus> oCompanies = Companies.lstGetShellCompanies();
            foreach (shellOus oCompany in oCompanies)
            {
                ddlOus.Items.Add(new ListItem(oCompany.m_sCompanyname, oCompany.m_iCompanyId.ToString()));
            }

            Utilities.getAllRoles(userRoleDropDownList);

            //companyDropDownList.Enabled = false;
            //UserOUDropDownList.Enabled = false;

            //Load Approval Limit

            IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

            L1.Text = "$ " + oIPLevels.iValue1.ToString() + " mln";
            L2.Text = "$ " + oIPLevels.iValue2.ToString() + " mln";
            L3.Text = "$ " + oIPLevels.iValue3.ToString() + " mln";
            L4.Text = "$ " + oIPLevels.iValue4.ToString() + " mln";
            L5.Text = "$ " + oIPLevels.iValue5.ToString() + " mln";
            NArdb.Text = "Not Applicable"; NArdb.Checked = true;

            Search4User1.initUserInfo("Select User", 250);
        }
    } 

    private void Clear()       
    {
        functionDropDownList.ClearSelection();
        userRoleDropDownList.ClearSelection();
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IPAdministrator/UsersList.aspx");
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        try
        {
            int iIPLimitId = 0;
            IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

            if (L1.Checked) iIPLimitId = oIPLevels.iLimitId1;
            else if (L2.Checked) iIPLimitId = oIPLevels.iLimitId2;
            else if (L3.Checked) iIPLimitId = oIPLevels.iLimitId3;
            else if (L4.Checked) iIPLimitId = oIPLevels.iLimitId4;
            else if (L5.Checked) iIPLimitId = oIPLevels.iLimitId5;
            else if (NArdb.Checked) iIPLimitId = oIPLevels.iLimitId0; //TODO: this may not be absolutely correct, consider next line commented out.
            //else if (NArdb.Checked) iIPRange = -1; //Note: this ia the default value of the field in the database.

            //Before you define a user, check if the user exists already
            appUserMgt oAppUserMgt = new appUserMgt();
            appUsers oAppUsers = oAppUserMgt.objGetUserByUserName(Search4User1.selectedUserDetails.m_sUserName);
            if (oAppUsers.m_sFullName != null)
            {
                string oMessage = Search4User1.selectedUserDetails.m_sFullName + " has already been defined in the role of " + appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUsers.m_iUserRoleId) + ", but may have not accepted his/her role. ";
                oMessage += "Check the user in the (Roles Awaiting Acceptance) and send a reminder mail. ";
                oMessage += "If not found then the user has been deactiviated. Call the system administrator to reactivate the user.";

                ajaxWebExtension.showJscriptAlert(Page, this, oMessage);
            }
            else
            {
                //TODO: more work to be done here, all the email must go

                bool bRet = oAppUserMgt.CreateUserProfile(Search4User1.selectedUserDetails.m_sUserName, Search4User1.selectedUserDetails.m_sFullName, Search4User1.selectedUserDetails.m_sUserMail, int.Parse(functionDropDownList.SelectedValue), iIPLimitId, int.Parse(userRoleDropDownList.SelectedValue), int.Parse(ddlOus.SelectedValue));
                if (bRet == true)
                {
                    oAppUsers = oAppUserMgt.objGetUserByUserName(Search4User1.selectedUserDetails.m_sUserName);
                    sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
                    if (int.Parse(userRoleDropDownList.SelectedValue) == (int)appUsersRoles.userRole.IP_Initiator)
                    {
                        oAppUsers = oAppUserMgt.objGetUserByUserName(Search4User1.selectedUserDetails.m_sUserName);
                        bRet = oSendMail.MailIPInitiatorUser(oAppUsers.structUserIdx, oAppUsers.m_sUserName);
                    }
                    else if (int.Parse(userRoleDropDownList.SelectedValue) == (int)appUsersRoles.userRole.Functional_Planner)
                    {
                        //Mail attributes selected for this user to the user for necessary database update
                        oAppUsers = oAppUserMgt.objGetUserByUserName(Search4User1.selectedUserDetails.m_sUserName);
                        bRet = oSendMail.FunctionalPlannerCreated(oAppUsers.structUserIdx, oAppUsers.m_sUserName);
                    }
                    else
                    {
                        oAppUsers = oAppUserMgt.objGetUserByUserName(Search4User1.selectedUserDetails.m_sUserName);
                        bRet = oSendMail.MailEIPUser(oAppUsers.structUserIdx, oAppUsers.m_sUserName, appUsersRoles.userRoleDesc((appUsersRoles.userRole)int.Parse(userRoleDropDownList.SelectedValue)));
                    }

                    if (bRet)
                    {
                        ajaxWebExtension.showJscriptAlert(Page, this, Search4User1.selectedUserDetails.m_sFullName + " has been successfully defined and notification email sent.");
                    }
                    else
                    {
                        ajaxWebExtension.showJscriptAlert(Page, this, "User not defined, please try again later.");
                    }
                    Clear();
                }
            }
        }
        catch (Exception ex)
        {
            AppStatusMessages.ConnectionToDataBaseServer(ex);
        }
    }
}
