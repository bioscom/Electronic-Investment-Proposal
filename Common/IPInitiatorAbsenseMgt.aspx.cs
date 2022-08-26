using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Common_IPInitiatorAbsenseMgt : aspnetPage
{
    appUserMgt oAppUserMgt = new appUserMgt();
    ProposalMgt oProposalMgt = new ProposalMgt();

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

        if (!IsPostBack)
        {
            List<appUsers> lstInitiators = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.IP_Initiator);
            foreach (appUsers oInitiator in lstInitiators)
            {
                ddlUsers.Items.Add(new ListItem(oInitiator.m_sFullName, oInitiator.m_iUserId.ToString()));
                ddlDelegatedUsers.Items.Add(new ListItem(oInitiator.m_sFullName, oInitiator.m_iUserId.ToString()));
            }
        }
    }

    protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = oProposalMgt.dtGetMyPendingProposals(int.Parse(ddlUsers.SelectedValue));
            LoadProposals(dt);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void LoadProposals(DataTable dt)
    {
        DataView dv = dt.DefaultView;

        if (dv.Count == 0)
        {
            ajaxWebExtension.showJscriptAlert(Page, this, "No Pending Investment Proposals found.");
        }
        else if (dv.Count > 0)
        {
            grdView.DataSource = dv;
            grdView.DataBind();
        }
    }

    protected void btnForward_Click(object sender, EventArgs e)
    {
        try
        {
            System.Text.StringBuilder sbMessage = new System.Text.StringBuilder();
            int x = 1;
            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            List<Proposal> lstProposals = oProposalMgt.lstGetMyPendingProposals(int.Parse(ddlUsers.SelectedValue));
            appUsers ToInitiator = oAppUserMgt.objGetUserByUserId(int.Parse(ddlDelegatedUsers.SelectedValue));
            if (lstProposals.Count > 0)
            {
                foreach (Proposal oProposal in lstProposals)
                {
                    bool bRet = oProposalMgt.DelegateIPInitiatorRole(int.Parse(ddlUsers.SelectedValue), int.Parse(ddlDelegatedUsers.SelectedValue), oProposal.m_lProposalId);
                    if (bRet)
                    {
                        sbMessage.Append(x + ". " + oProposal.m_sProj_Num + " - " + oProposal.m_sProj_Title);
                        x++;
                    }
                }

                oSendMail.DeligateIPInitiatorFunction(ToInitiator.structUserIdx, sbMessage, ToInitiator.m_sFullName);

                DataTable dt = oProposalMgt.dtGetMyPendingProposals(int.Parse(ddlDelegatedUsers.SelectedValue));
                LoadProposals(dt);

                ajaxWebExtension.showJscriptAlert(Page, this, "Investment Proposal(s) successfully forwarded to " + ddlDelegatedUsers.SelectedItem.Text);
            }
            else
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "No Proposal found for routing.");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/IPRegister.aspx");
    }

    protected void grdView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row

        //LinkButton lbForwardProposal = (LinkButton)grdView.Rows[index].FindControl("ForwardProposalButton");
        //string ProposalID = lbForwardProposal.Attributes["PROPOSALID"].ToString();
        //Label lbProjectTitle = (Label)grdView.Rows[index].FindControl("labelProjectTitle");
        //Label lbIPInitiator = (Label)grdView.Rows[index].FindControl("labelIPInitiator");
        //DropDownList UserEmail = (DropDownList)grdView.Rows[index].FindControl("userDropDownList");
        //string Email = UserEmail.SelectedItem.Text;

        //proposal = new Proposal(ProposalID);

        //if (UserEmail.SelectedItem.Text == "None")
        //{
        //    MessageBox.Show("Please select a user to forward the Investment Proposal.");
        //}
        //else
        //{
        //    IPInitiator IPInit = new IPInitiator(proposal.IDUSERMGT);

        //    //Assign the IP to another guy
        //    string OldIPInitiatorUserID = proposal.IDUSERMGT;
        //    ToEmail[0] = UserEmail.SelectedItem.Text;
        //    bool success = db.DelegateIPInitiatorRole(OldIPInitiatorUserID, UserEmail.SelectedValue, ProposalID);

        //    //Send Mail to the New IPInitiator.
        //    if (success == true)
        //    {

        //        MyMail.DeligateIPInitiatorFunction(ToEmail, CurrentUser.sUSERMAIL, proposal.PROJ_TITLE, proposal.PROJ_INIT, proposal.PROJ_NUM);
        //        MessageBox.Show("Investment Proposal successfully forwarded to '" + ToEmail[0] + "'");
        //    }
        //}
    }
}

//private void LoadDelegates(DataTable dt)
//{
//    try
//    {
//        if ((dt.Rows.Count > 0))
//        {
//            grdView.DataSource = dt;
//            grdView.DataBind();

//            foreach (GridViewRow grd in grdView.Rows)
//            {
//                Label status = (Label)grdView.Rows[grd.RowIndex].FindControl("statusLabel");
//                CheckBox BPOCheckBox = (CheckBox)grdView.Rows[grd.RowIndex].FindControl("BPOCheckBox");
//                int FlagColor = Convert.ToInt32(BPOCheckBox.Attributes["FLAG_COLOR"]);
//                if (FlagColor == 1)
//                {
//                    status.Text = "Current default role holder";
//                    BPOCheckBox.Checked = true;
//                }
//                else if (FlagColor == 0)
//                {
//                    status.Text = "";
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
//    }
//}

//foreach (GridViewRow grdRow in grdView.Rows)
//{
//    DropDownList ddlIPInitiator = (DropDownList)grdRow.FindControl("userDropDownList");
//    List<appUsers> lstInitiators = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.IP_Initiator);
//    foreach (appUsers oInitiator in lstInitiators)
//    {
//        ddlIPInitiator.Items.Add(new ListItem(oInitiator.m_sFullName, oInitiator.m_iUserId.ToString()));
//    }





//    //LinkButton proposalID = (LinkButton)grdRow.FindControl("ForwardProposalButton");
//    //string ProposalID = proposalID.Attributes["PROPOSALID"].ToString();

//    //Query to select IP Initiators in the same Function
//    //string sqlUserMails = "SELECT USERMAIL, IDUSERMGT FROM EIP_USERMGT WHERE USERROLESID = '" + eipUserRoles.iIPInitiator + "' ORDER BY USERMAIL";

//    //UsersEmail.Items.Clear();
//    //db.FillDBLSpecial(UsersEmail, sqlUserMails);
//}
