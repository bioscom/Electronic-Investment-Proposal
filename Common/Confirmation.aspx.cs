using System;
using System.Data;
using System.Text;

public partial class Common_Confirmation : aspnetPage
{
    appUserMgt oAppUserMgt = new appUserMgt();
    IPLimit oIPLimits = new IPLimit(); 

    protected void Page_Load(object sender, EventArgs e)
    {
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();
        if (Request.QueryString["UserName"].ToString() != null)
        {
            string UserName = Request.QueryString["UserName"].ToString().ToUpper();
            appUsers oAppUser = oAppUserMgt.objGetUserByUserName(UserName);

            //Check if this user is trying to log into EIP from this page
            //To do this, check if this user has already accepted the role previously

            if (oAppUser.m_iStatus == (int)appUsersRoles.userStatus.activeUser)
            {
                ViewPanel.Visible = false;
                mssgLabel.Text = "Hi, the system discovered that you had accepted a role in EIP<br/><br/> <a href='" + ApplicationURL.MyAppURL() + "'>Click here to login to EIP</a>";
            }
            else
            {
                behaviouralGuideHyperLink.NavigateUrl = ApplicationURL.eIPBehaviouralGuideLines();

                if (!IsPostBack)
                {
                    HideObjects();

                    if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Administrator)
                    {
                        SystemAdminCheckBox.Visible = true;
                        SystemAdminCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        SystemAdminCheckBox.Checked = true;
                        SystemAdminHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner)
                    {
                        BPOCheckBox.Visible = true;
                        BPOCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        BPOCheckBox.Checked = true;
                        BPOHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.BOM)
                    {
                        BOMCheckBox.Visible = true;
                        BOMCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        BOMCheckBox.Checked = true;
                        BOMHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Line_Team_Lead)
                    {
                        LineTeamLeadCheckBox.Visible = true;
                        LineTeamLeadCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        LineTeamLeadCheckBox.Checked = true;
                        LineTeamLeadHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.HSE_Support)
                    {
                        HSECheckBox.Visible = true;
                        HSECheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        HSECheckBox.Checked = true;
                        HSEHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Controllers)
                    {
                        BFMCheckBox.Visible = true;
                        BFMCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        BFMCheckBox.Checked = true;
                        BFMHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.TAX_Support)
                    {
                        TAXCheckBox.Visible = true;
                        TAXCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        TAXCheckBox.Checked = true;
                        TAXHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.LEGAL_Support)
                    {
                        LegalCheckBox.Visible = true;
                        LegalCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        LegalCheckBox.Checked = true;
                        LegalHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.SPCA_Support)
                    {
                        SPCACheckBox.Visible = true;
                        SPCACheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        SPCACheckBox.Checked = true;
                        SPCAHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Economics_Support)
                    {
                        econsCheckBox.Visible = true;
                        econsCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        econsCheckBox.Checked = true;
                        EconsHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Treasury_Support)
                    {
                        treasuryCheckBox.Visible = true;
                        treasuryCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        treasuryCheckBox.Checked = true;
                        TreasuryHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Security_Support)
                    {
                        securityCheckBox.Visible = true;
                        securityCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        securityCheckBox.Checked = true;
                        SecurityHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature)
                    {
                        FinanceCheckBox.Visible = true;
                        FinanceCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        FinanceCheckBox.Checked = true;
                        FinanceHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.VP)//Where VP == General Managers
                    {
                        VPCheckBox.Visible = true;
                        VPCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        VPCheckBox.Checked = true;
                        VPHF.Value = oAppUser.m_iUserRoleId.ToString();

                        if (oAppUser.m_iIPLimitId == oIPLevels.iValue4)
                        {
                            roleLabel.Text = "Approver of IPs <= $" + oIPLevels.iValue4 + " mln";
                        }
                        else if (oAppUser.m_iIPLimitId == oIPLevels.iValue3)
                        {
                            roleLabel.Text = "Support of IPs > $" + oIPLevels.iValue3 + " mln and Approver of IPs <= $" + oIPLevels.iValue3 + " mln";
                        }
                        else if (oAppUser.m_iIPLimitId == oIPLevels.iValue2)
                        {
                            roleLabel.Text = "Approver of IPs <= $" + oIPLevels.iValue2 + " mln";
                        }
                        else if (oAppUser.m_iIPLimitId == oIPLevels.iValue1)
                        {
                            roleLabel.Text = "Approver of IPs <= $" + oIPLevels.iValue1 + " mln";
                        }
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
                    {
                        GMREPlanCheckBox.Visible = true;
                        GMREPlanCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        GMREPlanCheckBox.Checked = true;
                        GMREPlanHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }

                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.REVP)
                    {
                        REVPCheckBox.Visible = true;
                        REVPCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        REVPCheckBox.Checked = true;
                        REVPHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = "Approver of IPs > $" + oIPLevels.iValue3 + " mln <= $" + oIPLevels.iValue4 + " mln";
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.IP_Initiator)
                    {
                        IPInitiatorCheckBox.Visible = true;
                        IPInitiatorCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        IPInitiatorCheckBox.Checked = true;
                        IPInitiatorHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Technical_Planning_Manager)
                    {
                        CPMCheckBox.Visible = true;
                        CPMCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        CPMCheckBox.Checked = true;
                        CPMHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director)
                    {
                        GMCheckBox.Visible = true;
                        GMCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        GMCheckBox.Checked = true;
                        GMFinanceHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = oAppUser.m_iUserRoleId + " " + oAppUser.eFunction.m_sFunction;
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.CERP)
                    {
                        EPGIPTrackerCheckBox.Visible = true;
                        EPGIPTrackerCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        EPGIPTrackerCheckBox.Checked = true;
                        EPGIPTrackerHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.Functional_Planner)
                    {
                        functionalPlannerCheckBox.Visible = true;
                        functionalPlannerCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        functionalPlannerCheckBox.Checked = true;
                        functionalPlannerHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.IT)
                    {
                        ITCheckBox.Visible = true;
                        ITCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        ITCheckBox.Checked = true;
                        ITHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                    else if (oAppUser.m_iUserRoleId == (int)appUsersRoles.userRole.SCM)
                    {
                        scmCheckBox.Visible = true;
                        scmCheckBox.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                        scmCheckBox.Checked = true;
                        scmHF.Value = oAppUser.m_iUserRoleId.ToString();
                        roleLabel.Text = appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId);
                    }
                }
            }
        }
    }

    private void HideObjects()
    {
        SystemAdminCheckBox.Visible = false; BPOCheckBox.Visible = false; 
        IPInitiatorCheckBox.Visible = false; BOMCheckBox.Visible = false;
        LineTeamLeadCheckBox.Visible = false; CPMCheckBox.Visible = false; 
        GMREPlanCheckBox.Visible = false; EPGIPTrackerCheckBox.Visible = false;

        HSECheckBox.Visible = false; BFMCheckBox.Visible = false; 
        TAXCheckBox.Visible = false; LegalCheckBox.Visible = false;
        SPCACheckBox.Visible = false; econsCheckBox.Visible = false; 
        treasuryCheckBox.Visible = false; securityCheckBox.Visible = false;

        FinanceCheckBox.Visible = false; REVPCheckBox.Visible = false; 
        VPCheckBox.Visible = false; GMCheckBox.Visible = false;
        MDCheckBox.Visible = false; functionalPlannerCheckBox.Visible = false;

        ITCheckBox.Visible = false; scmCheckBox.Visible = false;
    }

    protected void AcceptButton_Click(object sender, EventArgs e)
    {
        //Check if user has already been created on the EIP_USERMGT
        string UserName = Request.QueryString["UserName"].ToString().ToUpper();
        appUserMgt oAppUserMgt = new appUserMgt();
        appUsers oAppUser = oAppUserMgt.objGetUserByUserName(UserName);

        bool success = oAppUserMgt.RoleAcceptance(oAppUser.m_iUserId);
        if (success)
        {
            AcceptMailBPOSystemAdmin(oAppUser.m_iCompany, appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId));
            Response.Redirect("~/Index.aspx");
        }
    }

    protected void DeclineButton_Click(object sender, EventArgs e)
    {
        string UserName = Request.QueryString["UserName"].ToString().ToUpper();
        appUserMgt oAppUserMgt = new appUserMgt();
        appUsers oAppUser = oAppUserMgt.objGetUserByUserName(UserName);

        DeclineMailBPOSystemAdmin(oAppUser.m_iCompany, appUsersRoles.userRoleDesc((appUsersRoles.userRole)oAppUser.m_iUserRoleId));
    }

    private void AcceptMailBPOSystemAdmin(int iCompany, string sRole)
    {
        try
        {
            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            appUsers oBpo = oAppUserMgt.objGetUserByUserRoleCompany(iCompany, (int)appUsersRoles.userRole.Business_Process_Owner);
            oSendMail.AcceptMailBPOSystemAdmin(oBpo.structUserIdx, sRole);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    private void DeclineMailBPOSystemAdmin(int iCompany, string sRole)
    {
        try
        {
            sendMail oSendMail = new sendMail(oSessnx.getOnlineUser.structUserIdx);
            appUsers oBpo = oAppUserMgt.objGetUserByUserRoleCompany(iCompany, (int)appUsersRoles.userRole.Business_Process_Owner);
            oSendMail.DeclineMailBPOSystemAdmin(oBpo.structUserIdx, sRole);
            Server.Transfer("~/Logout.aspx");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }
}