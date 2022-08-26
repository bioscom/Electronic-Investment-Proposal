using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Text;
using System.Collections.Generic;

public partial class IPAdministrator_UsersList : aspnetPage
{
    //shellCompanies company = new shellCompanies();
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();

    string sortArgument = "";

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
      
        //if (!bRet) Response.Redirect("~/Profiles/pageDenied.aspx");
       
        if (!IsPostBack)
        {
            BindUserData(sortArgument);
            reminderButton.Visible = false;
            Utilities.getAllRoles(ddlUserRole);
        }
    }

    protected void UsersGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName; //Determines which button was clicked (stores the name of each button)
        DataSorter SortMe = new DataSorter();

        if ((ButtonClicked == "Sort") || (ButtonClicked == "Page"))
        {
            sortArgument = SortMe.MySortExpression(e);
        }
        else
        {
            int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row

            if (ButtonClicked == "EditThis")
            {
                Button edit = (Button)UsersGridView.Rows[index].FindControl("editLinkButton");
                int iUserId = int.Parse(edit.Attributes["IDUSERMGT"].ToString());
                Response.Redirect("~/Common/EditUser.aspx" + "?iUserId=" + iUserId, false);

                BindUserData(sortArgument);
            }

            if (ButtonClicked == "DeleteThis")
            {
                Button delete = (Button)UsersGridView.Rows[index].FindControl("deleteLinkButton");
                int iUserId = int.Parse(delete.Attributes["USERID"].ToString());
                int iRoleId = Convert.ToInt32(delete.Attributes["USERROLESID"]);
                //string myFunction = delete.Attributes["FUNCTION"].ToString();

                //Before a user can be deleted, find out if there is an outstanding Proposal to be Approved or Supported by the user.
                //If there is, then don't allow delete, report to the current user the number of proposals outstanding by the user to be deleted.
                //And advise to reroute the IPs to another person, before the user can be deleted.

                string IPFoundAwaiting = oProposalMgt.IPAwaitingThisUserSupport(iUserId, iRoleId);
                if (IPFoundAwaiting.Length > 0)
                {
                    Label lbFullName = (Label)UsersGridView.Rows[index].FindControl("labelFullName");
                    ajaxWebExtension.showJscriptAlert(Page, this, lbFullName.Text + " can not be deleted, there are Proposal(s) awaiting this user's attention:" + IPFoundAwaiting + ". You will need to reroute this(these) Proposal(s) to another user before this user can be deleted.");
                }
                else
                {
                    bool success = oAppUserMgt.deleteUserProfile(iUserId);
                    if (success)
                    {
                        Label lbFullName = (Label)UsersGridView.Rows[index].FindControl("labelFullName");
                        ajaxWebExtension.showJscriptAlert(Page, this, lbFullName.Text + " has been successfully deleted from the users list.");
                    }
                    BindUserData(sortArgument);
                }
            }
        }
    }

    protected void UsersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UsersGridView.PageIndex = e.NewPageIndex;
        BindUserData(sortArgument);

        //bool bRet = getUsersByRole(sortArgument, int.Parse(ddlUserRole.SelectedValue));
        //if (!bRet)
        //{
        //    //if (int.Parse(ddlUserRole.SelectedValue) != -1)
        //    //{
        //        BindUserData(sortArgument);
        //    //}
        //}
    }

    protected void UsersGridView_PageIndexChanged(object sender, EventArgs e)
    {

    }

    private void BindUserData(string sortArgument)
    {
        DataTable dt = oAppUserMgt.dtGetActiveUsers();
        DataView dv = dt.DefaultView;
        dv.Sort = sortArgument;
        UsersGridView.DataSource = dv;
        UsersGridView.DataBind();

        roleDescription();
    }

    private bool getUsersByRole(string sortArgument, int iUserRoleId)
    {
        bool bRet = false;
        DataTable dt = oAppUserMgt.dtGetUsersByRole(iUserRoleId);
        DataView dv = dt.DefaultView;
        dv.Sort = sortArgument;
        UsersGridView.DataSource = dv;
        UsersGridView.DataBind();

        roleDescription();

        if (dt.Rows.Count > 0)
        {
            bRet = true;
        }
        return bRet;
    }

    protected void sortList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void UsersGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindUserData(sortArgument);
    }

    protected void UsersGridView_Sorted(object sender, EventArgs e)
    {

    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/IPRegister.aspx");
    }

    protected void ddlUserRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserRole.SelectedValue == "-1")
        {
            BindUserData(sortArgument);
        }
        else
        {
            getUsersByRole(sortArgument, int.Parse(ddlUserRole.SelectedValue));
        }
    }

    protected void searchButton_Click(object sender, ImageClickEventArgs e)
    {
        appUserMgt oAppUserMgt = new appUserMgt();
        oAppUserMgt.SearchUser(UsersGridView, sortArgument, userTextBox.Text.Replace("'", "''"));
        roleDescription();
        userTextBox.Text = "";
    }
    protected void ddlFunctions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFunctions.SelectedValue == "1")
        {
            BindRolesAccepted();
            reminderButton.Visible = false;
        }
        else if (ddlFunctions.SelectedValue == "2")
        {
            BindRolesPending();
            reminderButton.Visible = true;
        }
    }

    private void BindRolesAccepted()
    {
        UsersGridView.DataSource = oAppUserMgt.dtGetRolesAcceptedOrPendingAcceptance((int)appUsersRoles.userStatus.activeUser).DefaultView;
        UsersGridView.DataBind();

        roleDescription();
    }

    private void BindRolesPending()
    {
        UsersGridView.DataSource = oAppUserMgt.dtGetRolesAcceptedOrPendingAcceptance((int)appUsersRoles.userStatus.lockedUser).DefaultView;
        UsersGridView.DataBind();

        roleDescription();
    }

    //private void AssignedRoles()
    //{
    //    UsersGridView.DataSource = oAppUserMgt.dtGetRolesAcceptedOrPendingAcceptance((int)appUsersRoles.userStatus.activeUser).DefaultView;
    //    UsersGridView.DataBind();

    //    roleDescription();
    //}

    protected void reminderButton_Click(object sender, EventArgs e)
    {
        sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
        structUserMailIdx ToEmail = new structUserMailIdx();
        try
        {
            foreach (GridViewRow grdRow in UsersGridView.Rows)
            {
                //Label lbUserEmail = (Label)grdRow.FindControl("labelEmailAddress");
                CheckBox cb = (CheckBox)grdRow.FindControl("sendMailCheckBox");

                Button edit = (Button)grdRow.FindControl("editLinkButton");
                int iUserId = int.Parse(edit.Attributes["IDUSERMGT"].ToString());

                if (cb.Checked == true)
                {
                    //Send Reminder Mail
                    appUsers oAppUser = oAppUserMgt.objGetUserByUserId(iUserId);

                    ToEmail = oAppUser.structUserIdx;

                    if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.IP_Initiator)
                    {
                        oSendMail.MailIPInitiatorUser(ToEmail, oAppUser.m_sUserName);
                    }
                    else
                    {
                        oSendMail.MailEIPUser(ToEmail, oAppUser.m_sUserName, appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId));
                    }
                    ajaxWebExtension.showJscriptAlert(Page, this, "Reminder succesfully sent to " + oAppUser.m_sFullName);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

    }

    private void roleDescription()
    {
        try
        {
            IPLimit oIPLimits = new IPLimit();
            IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

            foreach (GridViewRow grdRow in UsersGridView.Rows)
            {
                Label labelUserRole = (Label)grdRow.FindControl("labelUserRole");
                int iUserRoleId = int.Parse(labelUserRole.Attributes["USERROLESID"].ToString());
                labelUserRole.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)iUserRoleId);

                Label labelApprovalLimit = (Label)grdRow.FindControl("labelApprovalLimit");
                int iIPLimitId = int.Parse(labelApprovalLimit.Attributes["IDIPLIMIT"].ToString());

                if (iIPLimitId == oIPLevels.iLimitId0) labelApprovalLimit.Text = "N/A"; //oIPLevels.iValue0.ToString();
                else if (iIPLimitId == oIPLevels.iLimitId1) labelApprovalLimit.Text = oIPLevels.iValue1.ToString();
                else if (iIPLimitId == oIPLevels.iLimitId2) labelApprovalLimit.Text = oIPLevels.iValue2.ToString();
                else if (iIPLimitId == oIPLevels.iLimitId3) labelApprovalLimit.Text = oIPLevels.iValue3.ToString();
                else if (iIPLimitId == oIPLevels.iLimitId4) labelApprovalLimit.Text = oIPLevels.iValue4.ToString();
                else if (iIPLimitId == oIPLevels.iLimitId5) labelApprovalLimit.Text = oIPLevels.iValue5.ToString();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void btnCleanUp_Click(object sender, EventArgs e)
    {
        UserTableCleanUp oUserTableCleanUp = new UserTableCleanUp();
        UserTableCleanUp.procRet eRet = oUserTableCleanUp.CleanUpTable();

        if (eRet.eStatus)
        {
            ajaxWebExtension.showJscriptAlert(Page, this, "User Table Successfully cleaned up. An email has been sent to you with details of operation done in the Clean Up.");
            sendMail oSendMail = new sendMail();

            oSendMail.cleanUpReport(oSessnx.getOnlineUser.structUserIdx, eRet.sb);            
        }
    }
}