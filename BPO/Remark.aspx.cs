using System;

public partial class BPO_Remark : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool bRet = false;
        try
        {
            string[] sPageAccess = { appUsersRoles.userRole.Administrator.ToString(), appUsersRoles.userRole.Business_Process_Owner.ToString(), appUsersRoles.userRole.CERP.ToString() };
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
            if (Request.QueryString["Proposalid"] != null)
            {
                long lProposalId = long.Parse(Request.QueryString["Proposalid"].ToString());
                if (!IsPostBack)
                {
                    ProposalMgt oProposalMgt = new ProposalMgt();
                    Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);
                    IPDetailInfo1.LoadProposalDetails(oProposal);

                    BPOComment1.initBPoComment(lProposalId);
                    ForwardProposalToFunctionalSupport1.Init_Page(lProposalId);
                    ForwardProposalForOrganisationalApproval1.Init_Page(lProposalId);
                    ForwardIPForFinanceSignature1.Init_Page(lProposalId);
                    //IPWorkFlowOverRide1.Init_Page(lProposalId);
                }
            }
        }
    }

    protected void forwardSupportButton_Click(object sender, EventArgs e)
    {

    }
    protected void WFOverrideBtn_Click(object sender, EventArgs e)
    {

    }
    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/PendingProposals.aspx");
    }
}

    //protected void SaveButton_Click(object sender, EventArgs e)
    //{
    //    string comment = "";
    //    if (commentTextBox.Text != "") 
    //    { 
    //        comment = commentTextBox.Text.Replace("'", "''"); 
    //    }
    //    else 
    //    { 
    //        comment = null; 
    //    }

    //    //BPOComments BPO = new BPOComments(proposal.IDPROPOSAL);
    //    BPOComments BPO = new BPOComments();
    //    BPO.AddComment(CurrentUser, proposal, comment, Convert.ToInt32(SupportStandDropDownList.SelectedValue), DateTime.Today.Date.ToShortDateString());
    //    bpoSupportStatus();
    //}


    //#region The codes that handle how IP is forwarded or assigned to Functional Support

    

    //#endregion End of the code

    //#region The codes that forward IP to Finance Signature or GM Finance depending on the value of the IP

    //protected void financeSignatureButton_Click(object sender, EventArgs e)
    //{
    //    
    //}

    //#endregion

    //#region Code that handles how IP is forwarded to MD or VP for Organisational Approver

    //protected void approvalSupportButton_Click(object sender, EventArgs e)
    //{
    //    //When forwarding IP to IP Approver by the BPO, it is either forwarded to a Vice President or an MD (since an MD = VP)
    //    //eipUsers MyRole = new eipUsers(Convert.ToInt32(approvalDropDownList.SelectedValue));

    //    success = forwardIPtoVicePresident(approvalDropDownList, approvalSupportLabel);
    //    if (success == true)
    //    {
    //        MessageBox.Show("Proposal successfully sent to " + approvalDropDownList.SelectedItem.Text + " for Vice President's approval.");
    //    }
    //}

    //private bool forwardIPtoVicePresident(DropDownList theDropDownList, Label theSupport)
    //{
    //    //Before IP is forwarded to the Final approver, check if the IP was forwarded to MD OU. If it was forwarded and BPO selects MDOU as the final approver,
    //    //generate a warning report that operation is not allowed.
    //    BPOComments BPO = new BPOComments();
    //    VPComments VPComment = new VPComments(ProposalID);
    //    MDComments MD = new MDComments(proposal.IDPROPOSAL);
    //    if (MD.MDReceivedIP() == true)
    //    {
    //        if (approvalDropDownList.SelectedValue == MD.iIDUSERMGT.ToString())
    //        {
    //            success = false;
    //            MessageBox.Show(approvalDropDownList.SelectedItem.Text + " had supported this proposal as the\nMD Organisational Unit and can not receive proposal again as the final approver.\n\nForward IP to another Vice President for final approval.");
    //        }
    //        else if ((MD.MDReceivedIP() == true) && (approvalDropDownList.SelectedValue != MD.iIDUSERMGT.ToString()))
    //        {
    //            success = AssignIPToFinalApprover(theDropDownList, theSupport);
    //        }
    //    }
    //    else if(MD.MDReceivedIP() == false)
    //    {
    //        success = AssignIPToFinalApprover(theDropDownList, theSupport);
    //    }
    //    return success;
    //}

   
    //#endregion

    //protected void remarksLinkButton_Click(object sender, EventArgs e)
    //{
    //    remarksPanel.Visible = true;
    //    forwardIPPanel.Visible = false;
    //}

    //protected void forwardSupportButton_Click(object sender, EventArgs e)
    //{
    //    remarksPanel.Visible = false;
    //    forwardIPPanel.Visible = true;
    //    financeSignatureButton.Enabled = false;
    //    //GMButton.Enabled = false;
    //    sendReminderButton.Enabled = false;
    //    approvalSupportButton.Enabled = false;
    //    //MDOUButton.Enabled = false;
    //    //YesRadioButton.Enabled = false;
    //    //NoRadioButton.Enabled = false;
    //    //mdOULabel.Enabled = false;
    //    GMsPanel.Visible = false;
    //    IPAbove2ndQuartilePanel1.Visible = false;

    //    //To be enabled if Functional Support has supported
    //    bfmCheckBox.Enabled = false; hseCheckBox.Enabled = false; treasuryCheckBox.Enabled = false;
    //    taxCheckBox.Enabled = false; econsCheckBox.Enabled = false; legalCheckBox.Enabled = false;
    //    securityCheckBox.Enabled = false; spcaCheckBox.Enabled = false;

    //    //1. Make all support functions available for selection
    //    LoadFunctionalSupports();

    //    //2. Display Support Status for all Required Support Functions
    //    DisplaySupportStatus();

    //    //3. The workFlow for Finance Support
    //    FinanceSignatureSupportFlow();

    //    //4.
    //    //BPOSendsIPToMDOUForSupport();
    //}

    //#region Load Functional Support and Support Status Display methods (working fine)


    

    


    //private void SupportStatusColor(Label theSupportLabel)
    //{
    //    if (theSupportLabel.Text == SupportState.Supported)
    //    {
    //        theSupportLabel.ForeColor = System.Drawing.Color.Green;
    //    }
    //    else
    //    {
    //        theSupportLabel.ForeColor = System.Drawing.Color.Red;
    //    }
    //}

    //private void SupportStatus(Label theSupportLabel, CheckBox reReviewIP, int SupportStand)
    //{
    //    //check if the IP was not supported at any point
    //    bool IPNotSupportedApproved = db.IPNotSupportedApproved(ProposalID);
    //    if (IPNotSupportedApproved == true)
    //    {
    //        if ((theSupportLabel.Text == SupportState.Supported) && (SupportStand == SupportState.iSupported))
    //        {
    //            reReviewIP.Enabled = true;
    //        }
    //    }
    //}

    //protected void sendReminderButton_Click(object sender, EventArgs e)
    //{
    //    string GMUserID;
    //    foreach (GridViewRow grdRow in FunctionsGMGridView.Rows)
    //    {
    //        CheckBox Reminder = (CheckBox)grdRow.FindControl("Reminder");
    //        Label RoleLabel = (Label)grdRow.FindControl("RoleLabel");
    //        //
    //        GMUserID = Reminder.Attributes["IDUSERMGT"].ToString();
    //        if (Reminder.Checked == true)
    //        {
    //            //Just send mail to the Selected GM
    //            ToEmail[0] = Reminder.Attributes["EMAILADDY"].ToString();
    //            success = MyMail.MailGeneralManager(ToEmail, CurrentUser.sUSERMAIL, proposal.PROJ_TITLE, ApplicationURL.MyAppURL(), proposal.PROJ_NUM);
    //            if (success == true)
    //            {
    //                MessageBox.Show("Reminder Mail successfully sent to " + ToEmail[0]);
    //            }
    //        }
    //    }
    //}

    
    //protected void WFOverrideBtn_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/BPO/IPWFOverRider.aspx?ProposalID=" + ProposalID , false);
    //    //Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + ProposalID, false);
    //}
    