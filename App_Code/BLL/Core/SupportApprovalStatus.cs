using System;

/// <summary>
/// Summary description for SupportApprovalStaus
/// </summary>
/// 

public class SupportApprovalStatus
{
    //FinanceSignatureApproval FinanceSignature = new FinanceSignatureApproval();
    IPLimit oIPLimits = new IPLimit();

    public SupportApprovalStatus()
    {
        
    }

    public bool IPLessThan3rdQuartile(Proposal proposal)
    {
        bool IPValue = false;
        IPLimit oIPLimits = new IPLimit();
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

        decimal TotalIPValueDec = Convert.ToDecimal(proposal.m_lSS);
        int TotalIPValue = Convert.ToInt32(TotalIPValueDec * 100);

        if (TotalIPValue < (oIPLevels.iValue3 * 100))
        {
            IPValue = true;
        }
        else
        {
            IPValue = false;
        }
        return IPValue;
    }

    public bool IPLTEQ3rdQuartileGT2ndQuartile(Proposal proposal)
    {
        bool IPValue = false;
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

        decimal TotalIPValueDec = Convert.ToDecimal(proposal.m_lSS);
        int TotalIPValue = Convert.ToInt32(TotalIPValueDec * 100);

        if ((TotalIPValue <= (oIPLevels.iValue3 * 100)) && (TotalIPValue > (oIPLevels.iValue2 * 100)))
        {
            IPValue = true;
        }
        else
        {
            IPValue = false;
        }
        return IPValue;
    }

    public bool IPLT3rdQuartileGT2ndQuartile(Proposal proposal)
    {
        bool IPValue = false;
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

        //decimal TotalIPValueDec = Convert.ToDecimal(proposal.SS);
        int TotalIPValue = Convert.ToInt32(Convert.ToDecimal(proposal.m_lSS) * 100);

        if (TotalIPValue < (oIPLevels.iValue3 * 100) && (TotalIPValue > (oIPLevels.iValue2 * 100)))
        {
            IPValue = true;
        }
        else
        {
            IPValue = false;
        }
        return IPValue;
    }

    public bool IPGT2ndQuartile(Proposal proposal)
    {
        bool bRet = false;
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

        //decimal TotalIPValueDec = Convert.ToDecimal(proposal.SS);
        int TotalIPValue = Convert.ToInt32(proposal.m_lSS * 100);
        if (TotalIPValue > (oIPLevels.iValue2 * 100))
            bRet = true;
        else
            bRet = false;
        
        return bRet;
    }

    public bool IPLTEQ2ndQuartile(Proposal proposal)
    {
        bool IPValue = false;
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

        decimal TotalIPValueDec = Convert.ToDecimal(proposal.m_lSS);
        int TotalIPValue = Convert.ToInt32(TotalIPValueDec * 100);

        if (TotalIPValue <= (oIPLevels.iValue2 * 100))
        {
            IPValue = true;
        }
        else
        {
            IPValue = false;
        }
        return IPValue;
    }

    //Check the IP Value is >= $20mln for TAX
    public bool IPGTEQ3rdQuartile(Proposal proposal)
    {
        bool TaxSupportIsRequired = false;
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();
        decimal TotalIPValueDec = Convert.ToDecimal(proposal.m_lSS);
        int TotalIPValue = Convert.ToInt32(TotalIPValueDec * 100);

        if (TotalIPValue >= (oIPLevels.iValue3 * 100))
        {
            TaxSupportIsRequired = true;
        }
        else
        {
            TaxSupportIsRequired = false;
        }
        return TaxSupportIsRequired;
    }

    public bool IPLTEQLowerQuartile(Proposal proposal)
    {
        bool IPValue = false;
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();
        decimal TotalIPValueDec = Convert.ToDecimal(proposal.m_lSS);
        int TotalIPValue = Convert.ToInt32(TotalIPValueDec * 100);

        if (TotalIPValue <= (oIPLevels.iValue1 * 100))
        {
            IPValue = true;
        }
        else
        {
            IPValue = false;
        }

        return IPValue;
    }

    public bool IPGTLowerQuartileLTEQ2ndQuartile(Proposal proposal)
    {
        bool IPValue = false;
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();
        decimal TotalIPValueDec = Convert.ToDecimal(proposal.m_lSS);
        int TotalIPValue = Convert.ToInt32(TotalIPValueDec * 100);
        if ((TotalIPValue > (oIPLevels.iValue1 * 100)) || (TotalIPValue <= (oIPLevels.iValue2 * 100)))
        {
            IPValue = true;
        }
        else
        {
            IPValue = false;
        }

        return IPValue;
    }

    public bool IPLTEQLowerQuartileLTEQ2ndQuartile(Proposal proposal)
    {
        bool IPValue = false;
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();
        decimal TotalIPValueDec = Convert.ToDecimal(proposal.m_lSS);
        int TotalIPValue = Convert.ToInt32(TotalIPValueDec * 100);
        if ((TotalIPValue <= (oIPLevels.iValue1 * 100)) || (TotalIPValue <= (oIPLevels.iValue2 * 100)))
        {
            IPValue = true;
        }
        else
        {
            IPValue = false;
        }

        return IPValue;
    }
    
    //public void OrganisationalApproval(Proposal proposal, DropDownList approvalDropDownList, Label SupportLabel, Button SupportApproval)
    //{
    //    IPLimit iplimit = new IPLimit();
    //    VPComments VP = new VPComments();
    //    VP = new VPComments(proposal.m_lProposalId);

    //    if (IPLTEQ3rdQuartileGT2ndQuartile(proposal))
    //    {
    //        SupportApproval.Enabled = true;
    //        approvalDropDownList.Items.Clear();

    //        List<SupportApprovers> ThirdQuartileLimit = SupportApproverLogic.sqlThirdQuartileLimit();
    //        approvalDropDownList.Items.Add(new ListItem("None", "-1"));
    //        foreach (SupportApprovers xThirdQuartileLimit in ThirdQuartileLimit)
    //        {
    //            approvalDropDownList.Items.Add(new ListItem(xThirdQuartileLimit.FULL_NAME, xThirdQuartileLimit.IDUSERMGT));
    //        }

    //        if (VP.iSTAND == SupportState.iApproved)
    //        {
    //            SupportLabel.Text = SupportState.Supported;
    //            SupportLabel.ForeColor = System.Drawing.Color.Green;
    //            //SupportApproval.Enabled = true;
    //        }
    //        else
    //        {
    //            SupportLabel.Text = SupportState.NotSupported;
    //            SupportLabel.ForeColor = System.Drawing.Color.Red;
    //            //SupportApproval.Enabled = false;
    //        }
    //    }
    //}

    //Instruction, the Hidden fields on BPO/Remarks.aspx form 
    //#region Organisation Approval when the IP value is less than or equal to ThirdQuartile (Presently $20mln)

    //public void OrganisationalApprovalLT3rdQuartile(Proposal proposal, DropDownList approvalDropDownList, Label approvalLabel, Button approvalButton, DropDownList MDOUList, Button MDOUButton, Label MDOULabel, RadioButton Yes, RadioButton No)
    //{
    //    MDOUButton.Enabled = false;
    //    MDOUList.Enabled = false;
    //    MDOUList.Items.Clear();
    //    approvalDropDownList.Enabled = true;
    //    approvalButton.Enabled = true;

    //    if (IPLTEQLowerQuartile(proposal) == true)
    //    {
    //        //Fill the ApprovalListBox with VPs or MD within and above the LowerLimit Approval range                
    //        approvalDropDownList.Items.Clear();

    //        List<SupportApprovers> LimitLower = SupportApproverLogic.sqlLimitLower();
    //        approvalDropDownList.Items.Add(new ListItem("None", "-1"));
    //        foreach (SupportApprovers xLimitLower in LimitLower)
    //        {
    //            approvalDropDownList.Items.Add(new ListItem(xLimitLower.FULL_NAME, xLimitLower.IDUSERMGT));
    //        }
    //        findSelectedApprovalResponse(proposal, approvalLabel, approvalButton, approvalDropDownList, Yes, No);
    //    }
    //    else if (IPGTLowerQuartileLTEQ2ndQuartile(proposal) == true)
    //    {
    //        //Fill the ApprovalListBox with VPs or MD within and above the SecondQuartileLimit Approval range
    //        approvalDropDownList.Items.Clear();

    //        List<SupportApprovers> SecondQuartileLimit = SupportApproverLogic.sqlSecondQuartileLimit();
    //        approvalDropDownList.Items.Add(new ListItem("None", "-1"));
    //        foreach (SupportApprovers xSecondQuartileLimit in SecondQuartileLimit)
    //        {
    //            approvalDropDownList.Items.Add(new ListItem(xSecondQuartileLimit.FULL_NAME, xSecondQuartileLimit.IDUSERMGT));
    //        }

    //        findSelectedApprovalResponse(proposal, approvalLabel, approvalButton, approvalDropDownList, Yes, No);
    //    }
    //    else if (IPLT3rdQuartileGT2ndQuartile(proposal) == true)
    //    {   
    //        //Fill the ApprovalListBox with VPs or MD within and above the ThirdQuartileLimit Approval range
    //        approvalDropDownList.Items.Clear();

    //        List<SupportApprovers> ThirdQuartileLimit = SupportApproverLogic.sqlThirdQuartileLimit();
    //        approvalDropDownList.Items.Add(new ListItem("None", "-1"));
    //        foreach (SupportApprovers xThirdQuartileLimit in ThirdQuartileLimit)
    //        {
    //            approvalDropDownList.Items.Add(new ListItem(xThirdQuartileLimit.FULL_NAME, xThirdQuartileLimit.IDUSERMGT));
    //        }

    //        findSelectedApprovalResponse(proposal, approvalLabel, approvalButton, approvalDropDownList, Yes, No);

    //        //Fill the MDList with MDOU of the IP
    //        MDOUList.Items.Clear();
    //        IPInitiator IPInit = new IPInitiator(proposal.IDUSERMGT);
    //        int EPNIgeriaLogic = proposal.OriginatingUnit(proposal);
    //        //ManagingDirector MD = new ManagingDirector();

    //        List<SupportApprovers> MDSupport = SupportApproverLogic.GetMDOU(IPInit.iCompanyID, EPNIgeriaLogic);
    //        MDOUList.Items.Add(new ListItem("None", "-1"));
    //        foreach (SupportApprovers xMDSupport in MDSupport)
    //        {
    //            MDOUList.Items.Add(new ListItem(xMDSupport.FULL_NAME, xMDSupport.IDUSERMGT));
    //        }

    //        findSelectedMDResponse(proposal, MDOULabel, MDOUButton, MDOUList, Yes, No, approvalButton, approvalDropDownList);
    //    }
    //}

    //private void findSelectedApprovalResponse(Proposal proposal, Label approvalLabel, Button approvalButton, DropDownList approvalDropDownList, RadioButton Yes, RadioButton No)
    //{
    //    VPComments VPComment = new VPComments(proposal.IDPROPOSAL);

    //    if (VPComment.VPReceivedIP() == true)
    //    {
    //        approvalDropDownList.SelectedValue = VPComment.iIDUSERMGT.ToString();
    //        if (VPComment.iSTAND == SupportState.iApproved)
    //        {
    //            approvalLabel.ForeColor = System.Drawing.Color.Green;
    //            approvalLabel.Text = SupportState.Approved;
    //            approvalButton.Enabled = false;
    //        }
    //        else if (VPComment.iSTAND == SupportState.iNotApproved)
    //        {
    //            approvalLabel.ForeColor = System.Drawing.Color.Red;
    //            approvalLabel.Text = SupportState.NotApproved;
    //        }
    //        else if (VPComment.iSTAND == SupportState.iSupportApproverStandDefault)
    //        {
    //            approvalLabel.ForeColor = System.Drawing.Color.Red;
    //            approvalLabel.Text = "Not yet actioned.";
    //        }
    //    }
    //}

    //private void findSelectedMDResponse(Proposal proposal, Label MDOULabel, Button MDOUButton, DropDownList MDOUDropDownList, RadioButton Yes, RadioButton No, Button approvalButton, DropDownList approvalDropDownList)
    //{
    //    MDComments MDComment = new MDComments(proposal.IDPROPOSAL);

    //    if (MDComment.MDReceivedIP() == true)
    //    {
    //        No.Checked = true;
    //        Yes.Checked = false;

    //        MDOUDropDownList.SelectedValue = MDComment.iIDUSERMGT.ToString();
    //        if (MDComment.iSTAND == SupportState.iSupported)
    //        {
    //            MDOULabel.ForeColor = System.Drawing.Color.Green;
    //            MDOULabel.Text = SupportState.Supported;
    //            MDOUButton.Enabled = false;

    //            approvalButton.Enabled = true;
    //            approvalDropDownList.Enabled = true;
    //        }
    //        else if (MDComment.iSTAND == SupportState.iNotSupported)
    //        {
    //            MDOULabel.ForeColor = System.Drawing.Color.Red;
    //            MDOULabel.Text = SupportState.NotSupported;

    //            approvalButton.Enabled = false;
    //            approvalDropDownList.Enabled = false;
    //        }
    //        else if (MDComment.iSTAND == SupportState.iSupportApproverStandDefault)
    //        {
    //            MDOULabel.ForeColor = System.Drawing.Color.Red;
    //            MDOULabel.Text = "Not yet actioned.";

    //            approvalButton.Enabled = false;
    //            approvalDropDownList.Enabled = false;
    //        }
    //    }
    //}

    //#endregion


    //public bool HasAllVPSupportedIP(string ProposalID)
    //{
    //    bool AllSupported = false;
    //    int UserID = 0;
        
    //    //Check if this IP was forwarded to VP Finance and check for Support status
    //    string sqlVPFinance = "SELECT IDUSERMGT FROM EIP_VPFINANCE WHERE IDPROPOSAL = :IDPROPOSAL";
    //    sqlVPFinance = sqlVPFinance.Replace(":IDPROPOSAL", ProposalID);
    //    DataTable dtVPFinance = DataAccess.ExecuteQueryCommand(sqlVPFinance);
    //    if (dtVPFinance.Rows.Count > 0)
    //    {
    //        VPFinanceComments VPFin = new VPFinanceComments();
    //        foreach (DataRow dr2 in dtVPFinance.Rows)
    //        {
    //            UserID = Convert.ToInt32(dr2["IDUSERMGT"]);
    //            VPFin = new VPFinanceComments(ProposalID, UserID);
    //            if (VPFin.VPFinanceStand() == true)
    //            {
    //                AllSupported = true;
    //            }
    //            else
    //            {
    //                AllSupported = false;
    //            }
    //        }
    //    }

    //    //Spool out all the VPs found for the Proposal also check for VP Finance if he received the IP
    //    string sqlVPS = "SELECT IDUSERMGT FROM EIP_VPS WHERE IDPROPOSAL = :IDPROPOSAL";
    //    sqlVPS = sqlVPS.Replace(":IDPROPOSAL", ProposalID);
    //    DataTable dtVPS = DataAccess.ExecuteQueryCommand(sqlVPS);
    //    if (dtVPS.Rows.Count > 0)
    //    {
    //        VPComments VP = new VPComments();
    //        foreach (DataRow dr in dtVPS.Rows)
    //        {
    //            UserID = Convert.ToInt32(dr["IDUSERMGT"]);
    //            VP = new VPComments(ProposalID, UserID);
    //            if (VP.VPStand() == true)
    //            {
    //                AllSupported = true;
    //            }
    //            else
    //            {
    //                AllSupported = false;
    //                break;
    //            }
    //        }
    //    }

    //    return AllSupported;
    //}

    #region Please This Region is very important for BPO to resend IP to Functional Support to re-review an IP.

    public void ReviewIPAgain(structUserMailIdx mToEmail, int iUserId, Proposal oProposal)
    {

        ProposalReloaded(oProposal.m_lProposalId, iUserId);
        //MyMail.IPUpdateReloadNotification(mToEmail, SenderMail, mSubject, url, ProjectNumber, AppConfiguration.AdminEmail);
    }

    public void ProposalReloaded(long lProposalId, int iUserId)
    {
        //This method is to be called when the BPO wants to enable Support Function, Line Team Lead or an Approver
        // to resupport an IP atfer the IP Initiator has uploaded the updated IP as a result of comment made during the support process.
        
        //TODO: Please revisit this codes.
        string sql = "UPDATE EIP_SUPPORTAPPROVERCOMMENTS SET SUPPORT_BIT = '0' WHERE IDPROPOSAL = '" + lProposalId + "' AND IDUSERMGT = '" + iUserId + "'";
        DataAccess.ExecuteNonQueryCommand(sql);
    }
    #endregion
}