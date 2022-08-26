using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Common_EditUser : aspnetPage
{
    shellCompanies Companies = new shellCompanies();
    public bool success = false;

    appUserMgt oAppUserMgt = new appUserMgt();
    IPLimit oIPLimits = new IPLimit();

    protected void Page_Load(object sender, EventArgs e)
    {
        bool bRet = false;
        try
        {
            string[] sPageAccess = { appUsersRoles.userRole.Administrator.ToString(), appUsersRoles.userRole.Business_Process_Owner.ToString() };
            appUsersRoles oAccess = new appUsersRoles();
            bRet = oAccess.grantPageAccess(sPageAccess, this.oSessnx.getOnlineUser.m_eUserRole);
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
        }
        if (!bRet) Response.Redirect("~/Profiles/pageDenied.aspx");

        if (Request.QueryString["iUserId"] != null)
        {
            int iUserId = int.Parse(Request.QueryString["iUserId"].ToString());
            appUsers oAppUser = oAppUserMgt.objGetUserByUserId(iUserId);

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
                    ddlOUs.Items.Add(new ListItem(oCompany.m_sCompanyname, oCompany.m_iCompanyId.ToString()));
                }

                //Fill the User Roles
                Utilities.getAllRoles(userRoleDropDownList);

                //Load Approval Limit
                
                IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

                L1.Text = "$ " + oIPLevels.iValue1.ToString() + " mln";
                L2.Text = "$ " + oIPLevels.iValue2.ToString() + " mln";
                L3.Text = "$ " + oIPLevels.iValue3.ToString() + " mln";
                L4.Text = "$ " + oIPLevels.iValue4.ToString() + " mln";
                L5.Text = "$ " + oIPLevels.iValue5.ToString() + " mln";
                NArdb.Text = "Not Applicable"; 
                retrieveUser(oAppUser);
            }
        }
    }

    private void Clear()
    {
        userNameTextBox.Text = "";
        functionDropDownList.ClearSelection();
        userRoleDropDownList.ClearSelection();
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/IPAdministrator/UsersList.aspx");
    }

    protected void saveButton_Click(object sender, EventArgs e)
    {
        if ((ddlOUs.Enabled == true) && (ddlOUs.SelectedValue == "-1"))
        {
            ajaxWebExtension.showJscriptAlert(Page, this, "You must select OU for EP Nigeria Users in the Roles of GM and Corporate Planning Manager.");
        }
        else
        {
            //Get the Approval Limit for this User
            int iIPLimitId = 0;
            IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

            if (L1.Checked) iIPLimitId = oIPLevels.iLimitId1;
            else if (L2.Checked) iIPLimitId = oIPLevels.iLimitId2;
            else if (L3.Checked) iIPLimitId = oIPLevels.iLimitId3;
            else if (L4.Checked) iIPLimitId = oIPLevels.iLimitId4;
            else if (L5.Checked) iIPLimitId = oIPLevels.iLimitId5;
            else if (NArdb.Checked) iIPLimitId = oIPLevels.iLimitId0; //TODO: this may not be absolutely correct, consider next line commented out.
            //else if (NArdb.Checked) iIPRange = -1; //Note: this ia the default value of the field in the database.

            int iUserId = int.Parse(Request.QueryString["iUserId"].ToString());

            bool bRet = oAppUserMgt.UpdateUserProfile(iUserId, userNameTextBox.Text.ToUpper(), int.Parse(functionDropDownList.SelectedValue), int.Parse(userRoleDropDownList.SelectedValue), int.Parse(ddlOUs.SelectedValue), iIPLimitId);

            Response.Redirect("~/IPAdministrator/UsersList.aspx");
        }
    }

    private void retrieveUser(appUsers oAppUser)
    {
        fullNameLabel.Text = oAppUser.m_sFullName;
        userNameTextBox.Text = oAppUser.m_sUserName;
        functionDropDownList.SelectedValue = oAppUser.m_iFunction.ToString();
        userRoleDropDownList.SelectedValue = oAppUser.m_iUserRoleId.ToString();
        ddlOUs.SelectedValue = oAppUser.m_iCompany.ToString();

        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

        List<IPLimit> eIPLimits = oIPLimits.lstGetIPLimits();
        foreach (IPLimit eIPLimit in eIPLimits)
        {
            if (eIPLimit.m_iIPLimitId == oAppUser.m_iIPLimitId)
            {
                if (oIPLimits.m_iSeq == 0) NArdb.Checked = true;
                else if (oIPLimits.m_iSeq == 1) L1.Checked = true;
                else if (oIPLimits.m_iSeq == 2) L2.Checked = true;
                else if (oIPLimits.m_iSeq == 3) L3.Checked = true;
                else if (oIPLimits.m_iSeq == 4) L4.Checked = true;
                else if (oIPLimits.m_iSeq == 5) L5.Checked = true;
                break;
            }
        }
    }

    protected void userRoleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int iRoleSelected = int.Parse(userRoleDropDownList.SelectedValue);
        if ((iRoleSelected == (int)appUsersRoles.userRole.REVP) || (iRoleSelected == (int)appUsersRoles.userRole.GM_Regional_Planning) || (iRoleSelected == (int)appUsersRoles.userRole.CERP))
        {
            // || (roleSelected == eipUserRoles.CorporatePlanningManager)
            //Check the database if roleSelected already exists for someone and the STATUS = 'Activated'
            //if STATUS = 'Activated' do not allow registration of another user in that role, 
            //there can be only one person for each of these roles
            bool RoleStatus = db.GetRoleStatus(iRoleSelected);
            if (RoleStatus == true)
            {
                userRoleDropDownList.ClearSelection();
                AppStatusMessages.UserAlreadyExistsForThisRole(Session["rolesCurrentUser"].ToString());
            }
        }
    }
}