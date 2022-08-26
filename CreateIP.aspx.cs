using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;

public partial class CreateIP : aspnetPage
{
    shellCompanies Companies = new shellCompanies();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Load OUs
                OU ou = new OU();
                List<OU> OUs = ou.lstGetOU();
                foreach (OU oOU in OUs)
                {
                    OUDropDownList.Items.Add(new ListItem(oOU.s_CompanyName, oOU.m_iCompanyId.ToString()));
                }

                //Load Line Team Leads
                appUserMgt oAppUserMgt = new appUserMgt();
                List<appUsers> MyLTLs = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Line_Team_Lead);
                foreach (appUsers MyLTL in MyLTLs)
                {
                    LineTeamLeadDropDownList.Items.Add(new ListItem(MyLTL.m_sFullName, MyLTL.m_iUserId.ToString()));
                }

                //Load EP Priorities
                EPPriority priority = new EPPriority();
                List<EPPriority> Priorities = priority.lstGetEPPriority();
                foreach (EPPriority oPriority in Priorities)
                {
                    EPPriorityDropDownList.Items.Add(new ListItem(oPriority.m_sEPPriority, oPriority.m_iEPPriority.ToString()));
                }
                oLocator1.initUserInfo("Select BOM", 250);
                imgBOM.ToolTip = "Enter the First Name or Last Name of the Business Opportunity Manager (BOM) in the text box. Then, click the button next to the text box, the list of BOM will be listed in a drop down box, select the BOM and continue.";

                //drpBOM.Visible = false;

                ValidateBPO();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        //AmountJVTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        //AmountJVTextBox.Attributes.Add("onchange", "validateDecimal(this)");
        //AmountJVTextBox.Attributes.Add("onkeypress", "return checkForSecondDecimal(this, event)");

        ////IPValueTextBox.Attributes.Add("onkeypress", "return isNumberKey(event)");
        //IPValueTextBox.Attributes.Add("onchange", "validateDecimal(this)");
        //IPValueTextBox.Attributes.Add("onkeypress", "return checkForSecondDecimal(this, event)");
    }

    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        bool bRet = CheckIfBPOExistsForTheInitiatorOU();
        if (!bRet)
        {
            string sCompany = Companies.objGetShellCompanyById(oSessnx.getOnlineUser.m_iCompany).m_sCompanyname;
            ajaxWebExtension.showJscriptAlert(Page, this, "Sorry, you will not be able to submit this IP, because " + sCompany + ", Business Process Owner(BPO) not found!!! Please contact System Administrator to set the profile of the BPO, to include " + sCompany + " as OU.");
        }
        else
        {
            if (!UploadProposal.HasFile)
            {
                ajaxWebExtension.showJscriptAlert(Page, this, "No proposal attached. Kindly check and try again.");

                return;
            }

            AutoGenerateProjectNumber MyProjectNumber = new AutoGenerateProjectNumber();
            long lProposalId = 0;
            string uMessage = "";
            string uMsg = "";
            string sProjectNumber = MyProjectNumber.GenerateProjectNumber(IPSourceDropDownList.SelectedItem.Text);

            try
            {
                Proposal oProposal = new Proposal();
                ProposalMgt oProposalMgt = new ProposalMgt();

                oProposal.m_iFunctionId = int.Parse(Encoder.HtmlEncode(oSessnx.getOnlineUser.m_iFunction.ToString()));
                oProposal.m_sProj_Title = projTitleTextBox.Text;
                oProposal.m_lJV = decimal.Parse(AmountJVTextBox.Text);
                oProposal.m_lSS = decimal.Parse(IPValueTextBox.Text);
                oProposal.m_sProj_Num = sProjectNumber;
                oProposal.m_sProj_Desc = projDescTextBox.Text;
                oProposal.m_iEppriorityId = int.Parse(EPPriorityDropDownList.SelectedValue);

                bRet = oProposalMgt.CreateProposal(oProposal, UploadProposal, int.Parse(Encoder.HtmlEncode(oSessnx.getOnlineUser.m_iUserId.ToString())), ref lProposalId);
                if (bRet == true)
                {
                    bRet = oProposalMgt.forwardProposalToLineTeamLead(lProposalId, sProjectNumber, int.Parse(LineTeamLeadDropDownList.SelectedValue),
                         projTitleTextBox.Text, int.Parse(Encoder.HtmlEncode(oSessnx.getOnlineUser.m_iCompany.ToString())),
                         int.Parse(Encoder.HtmlEncode(oSessnx.getOnlineUser.m_iFunction.ToString())), oSessnx.getOnlineUser);

                    //ajaxWebExtension.showJscriptAlert(Page, this, "Error on page - " + uMsg +"=="+ bRet);
                    //return;

                    if (bRet)
                    {
                        Clear();
                        ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully submitted. Your Line Team Lead has been notified.");
                    }
                    else
                    {
                        Response.Write("<script>alert('" + uMsg + "');</script>");
                        //ajaxWebExtension.showJscriptAlert(Page, this, "Error on page. \n" + uMsg);

                    }
                }
                else
                {
                    ajaxWebExtension.showJscriptAlert(Page, this, uMessage);

                }
            }
            catch (Exception ex)
            {
                ajaxWebExtension.showJscriptAlert(Page, this, ex.ToString());
                System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
            }
        }
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Common/MyProposal.aspx");
    }

    protected void projInitTextBox_TextChanged(object sender, EventArgs e)
    {

    }

    //private void HideObjects()
    //{
    //    IPSourceDropDownList.Enabled = false;
    //    ipSourceLabel.Enabled = false;
    //}

    //[WebMethodAttribute(), ScriptMethodAttribute()]
    //public string[] GetCompletionList(string prefixText, int count, string contextKey)
    //public string[] GetCompletionList(string prefixText)
    //{
    //    List<string> MyItems = new List<string>();

    //    List<CompleteStaffDetailsInfo> BOMS = Users.lstGetStaffInfoByPrefixText(prefixText);
    //    foreach (CompleteStaffDetailsInfo BOM in BOMS)
    //    {
    //      drpBOM.Items.Add(new ListItem(BOM.m_sFullName, BOM.m_sUserMail));
    //    }

    //    return MyItems.ToArray();
    //}

    //protected void txtBOM_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtBOM.Text.Length > 4)
    //    {
    //        GetCompletionList(txtBOM.Text);
    //    }
    //    txtBOM.Visible = false;
    //    drpBOM.Visible = true;
    //}

    //protected void drpBOM_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtBOM.Visible = true;
    //    txtBOM.Text = drpBOM.SelectedItem.Text;
    //    drpBOM.Visible = false;
    //}

    private void Clear()
    {
        projTitleTextBox.Text = "";
        //txtBOM.Text = "";
        AmountJVTextBox.Text = "";
        IPValueTextBox.Text = "";
        projDescTextBox.Text = "";

        IPSourceDropDownList.ClearSelection();
        OUDropDownList.ClearSelection();
        EPPriorityDropDownList.ClearSelection();
    }

    private bool CheckIfBPOExistsForTheInitiatorOU()
    {
        bool bRet = false;

        appUserMgt oAppUserMgt = new appUserMgt();
        appUsers oBusinessProcessOwner = oAppUserMgt.objGetUserByUserRoleCompany(oSessnx.getOnlineUser.m_iCompany, (int)appUsersRoles.userRole.Business_Process_Owner);
       // string texttd = oSessnx.getOnlineUser.m_iCompany.ToString() + "====" + (int)appUsersRoles.userRole.Business_Process_Owner;
      
        if(oBusinessProcessOwner.m_iUserId == 0)
        {
            bRet = false;
        }
        else 
        { 
            bRet = true;
        }

        return bRet;
    }

    private void ValidateBPO()
    {
        bool bRet = CheckIfBPOExistsForTheInitiatorOU();
        if (!bRet)
        {
            string sCompany = Companies.objGetShellCompanyById(oSessnx.getOnlineUser.m_iCompany).m_sCompanyname;
            ajaxWebExtension.showJscriptAlert(Page, this, "Sorry, you will not be able to submit this IP, because " + sCompany + ", Business Process Owner(BPO) not found!!! Please contact System Administrator to set the profile of the BPO, to include " + sCompany + " as OU.");
        }
    }
}