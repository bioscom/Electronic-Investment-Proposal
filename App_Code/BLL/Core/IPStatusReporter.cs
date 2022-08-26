using System;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for IPStatusReporter
/// This class is used to report the Status of each IPs displayed
/// </summary>
public class IPStatusReporter
{
    public IPStatusReporter()
    {

    }
    public static void IPStatusReport(appUsers OnlineUser, GridView oGridView, DataTable dtProposal, string sortExpression)
    {
        //SupportApprovalStatus SupportStatus = new SupportApprovalStatus();
        //FinanceSignatureApproval financeSignature = new FinanceSignatureApproval();
        //appUserMgt oAppUserMgt = new appUserMgt();
        ProposalMgt oProposalMgt = new ProposalMgt();

        DataTable dt = dtProposal;
        DataView dv = dt.DefaultView;
        dv.Sort = sortExpression;

        SLAMgt xsla = new SLAMgt();
        SLA oSla = xsla.objGetSLASettings();

        try
        {
            //IPRegisterDeletePrivilege searches the Roles assigned to a user, if the System Administrator or BPO is found,
            //the user should be able to delete IPs.
            //Also, if the user is an IP Initiator, and the status of the IP shows that it has not been forwarded, he/she should be able to delete the IP.

            //bool deleteRole = false;
            //if ((OnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Administrator) || (OnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner))
            //{
            //    deleteRole = true;
            //}

            oGridView.DataSource = dv;
            oGridView.DataBind();

            foreach (GridViewRow grdRow in oGridView.Rows)
            {
                LinkButton lnkProposal = (LinkButton)grdRow.FindControl("ViewStatusLinkButton");
                long lProposalId = long.Parse(lnkProposal.Attributes["PROPOSALID"].ToString());
                Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

                Label Status = (Label)grdRow.FindControl("labelStatus");
                LinkButton lbDelete = (LinkButton)grdRow.FindControl("DeleteProposalLinkButton");
                LinkButton lbDiscontinue = (LinkButton)grdRow.FindControl("DiscontinueLinkButton");

                if (lbDiscontinue != null)
                {
                    lbDiscontinue.Attributes.Add("onClick", "return DiscontinueProposal('" + oProposal.m_sProj_Title + "')");
                }
                if (lbDelete != null)
                {
                    lbDelete.Attributes.Add("onClick", "return DeleteProject('" + oProposal.m_sProj_Title + "')");
                }
                if (oProposal.m_iDiscontinue == IPStatus.iDiscontinued)
                {
                    LinkButton lnkRemarks = (LinkButton) grdRow.FindControl("RemarkLinkButton");
                    if (lnkRemarks != null)
                    {
                        lnkRemarks.Enabled = false;
                    }

                    LinkButton lnkDiscontinue = (LinkButton) grdRow.FindControl("DiscontinueLinkButton");
                    if (lnkDiscontinue != null)
                    {
                        lnkDiscontinue.Enabled = false;
                    }
                    
                    Label lblTitle = (Label)grdRow.FindControl("labelProjectTitle");
                    lblTitle.Text += " [Note: This Proposal has been discontinued]";
                    lblTitle.ForeColor = System.Drawing.Color.DarkRed;
                    lblTitle.Font.Bold = true;
                    grdRow.BackColor = System.Drawing.Color.Pink;
                }
                
                Status.Text = "";

                supportProcess(oProposal, Status, OnlineUser);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public static void supportProcess(Proposal oProposal, Label Status, appUsers OnlineUser)
    {
        SLAMgt xsla = new SLAMgt();
        SLA oSla = xsla.objGetSLASettings();

        if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.IPIntiatorDevIP)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.IPIntiatorDevIP); //"IP Intiator still developing IP...";
            Status.ForeColor = System.Drawing.Color.Red; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.LTLSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.LTLSupport); //"Awaiting Line Team Lead's Support"; //LineTeamLeadSupported == false) 
            Status.ForeColor = System.Drawing.Color.OrangeRed; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.BPOSupport) 
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.BPOSupport);//"Awaiting Business Process Owner's Support"
            Status.ForeColor = System.Drawing.Color.Navy; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.BPOYetToFunctionalSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.BPOYetToFunctionalSupport);
            Status.ForeColor = System.Drawing.Color.Gray; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.FunctionalSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.FunctionalSupport);
            Status.ForeColor = System.Drawing.Color.Red; Status.Font.Bold = true;
        }

        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.BPOToFinSig)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.BPOToFinSig);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.BPOToOUFinDirector)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.BPOToOUFinDirector);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitFinSig)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitFinSig);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.BPOTOGMFin)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.BPOTOGMFin);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.BPOToFinanceDirector)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.BPOToFinanceDirector);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitsCERPSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitsCERPSupport);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitGMFin)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitGMFin);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitCorpPlanMgr)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitCorpPlanMgr);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitVPFin)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitVPFin);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.BPOVPApprSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.BPOVPApprSupport);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitsMDOUSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitsMDOUSupport);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitsMDSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitsMDSupport);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitsGMREPlanSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitsGMREPlanSupport);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitsVPSSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitsVPSSupport);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitsREVPSupport)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitsREVPSupport);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.BPOToOrgApprover)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.BPOToOrgApprover);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.AwaitsVPApprover)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.AwaitsVPApprover);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.Approved)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.Approved);
            Status.ForeColor = System.Drawing.Color.Green; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.Rejected)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.Rejected);
            Status.ForeColor = System.Drawing.Color.Red; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.LTLNotSupported)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.LTLNotSupported);
            Status.ForeColor = System.Drawing.Color.Red; Status.Font.Bold = true;
        }
        else if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.BPONotSupported)
        {
            Status.Text = ipStatusRptDesc(ipStatusRpt.BPONotSupported);
            Status.ForeColor = System.Drawing.Color.Red; Status.Font.Bold = true;
        }
    }

    public enum ipStatusRpt
    {
        IPIntiatorDevIP = 1,
        IPNotYetSub = 2,
        LTLSupport = 3,
        BPOSupport = 4,
        BPOYetToFunctionalSupport = 5,
        FunctionalSupport = 6,
        BPOToFinSig = 7,
        BPOToOUFinDirector = 77,
        AwaitFinSig = 8,
        BPOToFinanceDirector = 81,
        AwaitsCERPSupport = 82,
        BPOTOGMFin = 9,
        AwaitGMFin = 10,
        AwaitCorpPlanMgr = 11,
        AwaitVPFin = 12,
        BPOVPApprSupport = 13,
        AwaitsMDOUSupport = 14,
        AwaitsMDSupport = 15,
        AwaitsGMREPlanSupport = 16,
        AwaitsVPSSupport = 17,
        AwaitsREVPSupport = 18,
        BPOToOrgApprover = 19,
        AwaitsVPApprover = 20,
        Approved = 21,
        Rejected = 22,
        LTLNotSupported = 23,
        BPONotSupported = 24,

    };

    public static string ipStatusRptDesc(ipStatusRpt eIPStatus)
    {
        string sRet = "UnKnown Account";
        try
        {
            switch (eIPStatus)
            {
                case ipStatusRpt.IPIntiatorDevIP: sRet = "IP Intiator still developing IP..."; break;
                case ipStatusRpt.IPNotYetSub: sRet = "IP not yet submitted by Inititator."; break;
                case ipStatusRpt.LTLSupport: sRet = "Awaiting Line Team Lead's Support"; break;
                case ipStatusRpt.BPOSupport: sRet = "Awaiting Business Process Owner's Support"; break;
                case ipStatusRpt.BPOYetToFunctionalSupport: sRet = "BPO yet to forward IP for Functional Support"; break;
                case ipStatusRpt.FunctionalSupport: sRet = "Functional Support in Progress..."; break;
                case ipStatusRpt.BPOToFinSig: sRet = "BPO yet to forward IP to Finance Signature"; break;
                case ipStatusRpt.BPOToOUFinDirector: sRet = "BPO to forward IP to OU Finance Director"; break;
                case ipStatusRpt.AwaitFinSig: sRet = "Awaiting Finance Signature's Support"; break;
                case ipStatusRpt.BPOToFinanceDirector: sRet = "BPO yet to forward IP to OU Finance Director"; break;
                case ipStatusRpt.AwaitsCERPSupport: sRet = "Awaits Capital Expenditure Review Panel's Support"; break;
                case ipStatusRpt.BPOTOGMFin: sRet = "BPO Yet to forward IP to GM Finance"; break;
                case ipStatusRpt.AwaitGMFin: sRet = "Awaiting OU Finance Director's Approval"; break;
                case ipStatusRpt.AwaitCorpPlanMgr: sRet = "Awaiting Corporate Planning Manager's Support."; break;
                case ipStatusRpt.AwaitVPFin: sRet = "VP Finance yet to approve IP."; break;
                case ipStatusRpt.BPOVPApprSupport: sRet = "BPO not yet forwarded IP for VP's Approval/Support."; break;
                case ipStatusRpt.AwaitsMDOUSupport: sRet = "Awaiting MD OU support."; break;
                case ipStatusRpt.AwaitsMDSupport: sRet = "Awaiting MD's Support"; break;
                case ipStatusRpt.AwaitsGMREPlanSupport: sRet = "Awaiting GM Regional Planning's Support"; break;
                case ipStatusRpt.AwaitsVPSSupport: sRet = "Awaiting VPs Support"; break;
                case ipStatusRpt.AwaitsREVPSupport: sRet = "Awaiting Regional VP's Approval"; break;
                case ipStatusRpt.BPOToOrgApprover: sRet = "BPO not yet forwarded IP for Organisational Approval"; break;
                //case ipStatusRpt.BPOToFinSig: sRet = "BPO yet to forward IP for final Organisational Approval"; break;
                case ipStatusRpt.AwaitsVPApprover: sRet = "Awaiting General Manager's Approval"; break;
                case ipStatusRpt.Approved: sRet = "Approved"; break;
                case ipStatusRpt.Rejected: sRet = "Rejected"; break;
                case ipStatusRpt.LTLNotSupported: sRet = "Not Supported by Team Lead"; break;
                case ipStatusRpt.BPONotSupported: sRet = "Not Supported by BPO"; break;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return sRet;
    }
}

public class SupportState
{
    public static string NotSupported { get; set; }
    public static string NotApproved { get; set; }
    public static string Supported { get; set; }
    public static string Approved { get; set; }
    public static string FinanceApproval { get; set; }
    public static string sSupportApproverStandDefault { get; set; }

    public static int iNotSupported { get; set; }
    public static int iNotApproved { get; set; }
    public static int iSupported { get; set; }
    public static int iApproved { get; set; }
    public static int iStandDefault { get; set; }
    public static int iBPOStandDefault { get; set; }
    public static int iSupportApproverStandDefault { get; set; }
    public static int iFinanceApproval { get; set; }

    static SupportState()
    {
        Supported = "Supported";
        NotSupported = "Not Supported";
        Approved = "Approved";
        NotApproved = "Not Approved";
        sSupportApproverStandDefault = "Awaiting review";

        iNotSupported = 1;
        iSupported = 2;
        iApproved = 3;
        iNotApproved = 4;
        iFinanceApproval = 5;

        iStandDefault = 0;
        iBPOStandDefault = 0;
        iSupportApproverStandDefault = 0;
    }
}

public class IPStatus
{
    public static string Activated { get; set; }
    public static string Deactivated { get; set; }
    public static int iDiscontinued { get; set; }
    public static int iReactivate { get; set; }
    public static int iDefault { get; set; }

    static IPStatus()
    {
        Activated = "Activated";
        Deactivated = "Deactivated";
        iDiscontinued = 1;
        iReactivate = 0;
        iDefault = -1;
    }
}


//private static void IPLessThanSecondQuartile(Label Status, Proposal proposal, SupportApprovalStatus SupportStatus, FinanceSignatureApproval FinanceSignature, TextBox flag, SLA sla)
//{
//    try
//    {
//        //Test to see if the BPO has forwarded the IP to a Finance Signature
//        //Please Note: IPs that are less than SecondQuatile could be approved by GM Finance as the Finance Signature.
//        //Also test to see if it was forwarded to GM finance, in case not found in FinanceSignature.
//        FinanceSignatureComments FinSig = new FinanceSignatureComments(proposal.IDPROPOSAL);
//        bool BPOHasForwardedIPToFinanceSignature = FinSig.FinanceSignatureReceivedIP();
//        GMFinanceComments GMFin = new GMFinanceComments(proposal.IDPROPOSAL);
//        bool BPOHasForwardedIPToGMFinance = GMFin.GMFinanceReceivedIP();
//        if ((BPOHasForwardedIPToFinanceSignature == false) && (BPOHasForwardedIPToGMFinance == false))
//        {
//            Status.Text = "BPO yet to forward IP to Finance Signature";
//            flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//        }
//        else if ((BPOHasForwardedIPToFinanceSignature == true) || (BPOHasForwardedIPToGMFinance == true))
//        {
//            //Check to see if the finance signature has approved the IP
//            //Please Note: IPs that are less than SecondQuatile could be approved by GM Finance as the Finance Signature.
//            //Also test to see if it was approved by GM finance, in case not found in FinanceSignature.
//            bool FinanceSignatureApproved = FinSig.FinanceSignatureStand();
//            bool GMFinanceApproved = GMFin.GMFinanceStand();
//            if ((FinanceSignatureApproved == false) && (GMFinanceApproved == false))
//            {
//                Status.Text = "Awaiting Finance Signature's Support";
//                flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//            }
//            else if ((FinanceSignatureApproved == true) || (GMFinanceApproved == true))
//            {
//                //IP is sent for Organisational Approval
//                OrganisationalApprover(Status, proposal, SupportStatus, FinanceSignature, flag, sla);
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
//    }
//}

//private static void IPGreaterThanSecondQuartile(Label Status, Proposal proposal, SupportApprovalStatus SupportStatus, FinanceSignatureApproval FinanceSignature, TextBox flag, SLA sla)
//{
//    try
//    {
//        //Check if Business Process Owner has sent the IP to GM Finance OU
//        GMFinanceComments GMFin = new GMFinanceComments(proposal.IDPROPOSAL);
//        bool BPOSentIPToGMFinanceOU = GMFin.GMFinanceReceivedIP();
//        if (BPOSentIPToGMFinanceOU == false)
//        {
//            Status.Text = "BPO Yet to forward IP to GM Finance";
//            flag.BackColor = System.Drawing.Color.FromName(sla.m_sWithinSLA);
//        }
//        else if (BPOSentIPToGMFinanceOU == true)
//        {
//            //Check if GM Finance OU has Approved the IP
//            bool GMFinanceOUApprovedIP = GMFin.GMFinanceStand();
//            if (GMFinanceOUApprovedIP == false)
//            {
//                Status.Text = "Awaiting GM Finance OU's Approval";
//                flag.BackColor = System.Drawing.Color.FromName(sla.m_sWithinSLA);
//            }
//            else if (GMFinanceOUApprovedIP == true)
//            {
//                int EPNigeria = proposal.OriginatingUnit(proposal);
//                CorporatePlanningManager CPMComment = new CorporatePlanningManager(proposal.IDPROPOSAL);
//                bool CPMSupportedIP = CPMComment.CPMStand();
//                if (CPMSupportedIP == false)
//                {
//                    Status.Text = "Awaiting Corporate Planning Manager's Support.";
//                    flag.BackColor = System.Drawing.Color.FromName(sla.m_sWithinSLA);
//                }
//                else if (CPMSupportedIP == true)
//                {
//                    bool IPLT3rdQuartile = SupportStatus.IPLessThan3rdQuartile(proposal);
//                    if (IPLT3rdQuartile == true)
//                    {
//                        IPLessThanThirdQuartile(Status, proposal, SupportStatus, FinanceSignature, flag, sla);
//                    }
//                    else
//                    {
//                        IPGreaterThanOrEqualToThirdQuartile(Status, proposal, SupportStatus, FinanceSignature, flag, sla);
//                    }
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
//    }
//}

//private static void IPLessThanThirdQuartile(Label Status, Proposal proposal, SupportApprovalStatus SupportStatus, FinanceSignatureApproval FinanceSignature, TextBox flag, SLA sla)
//{
//    VPFinanceComments VPFin = new VPFinanceComments(proposal.IDPROPOSAL);
//    if (VPFin.VPFinanceStand() == false)
//    {
//        Status.Text = "VP Finance yet to approve IP.";
//        flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//    }
//    else if (VPFin.VPFinanceStand() == true)
//    {
//        //Is MD OU the Final approver?
//        //Check if the IP is with the MD OU, if it is not there, check if it is with a VP.
//        //If it is not with any of them, then the IP is still with the BPO.

//        VPComments VPComment = new VPComments(proposal.IDPROPOSAL);
//        MDComments MDComment = new MDComments(proposal.IDPROPOSAL);
//        if ((VPComment.VPReceivedIP() == false) && (MDComment.MDReceivedIP() == false))
//        {
//            Status.Text = "BPO not yet forwarded IP for VP's Approval/Support.";
//            flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//        }
//        else if (MDComment.MDReceivedIP() == true)
//        {
//            if (MDComment.MDStand() == false)
//            {
//                Status.Text = "MD OU not yet supported IP.";
//                flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//            }
//            else if (MDComment.MDStand() == true)
//            {
//                OrganisationalApprover(Status, proposal, SupportStatus, FinanceSignature, flag, sla);
//            }
//        }
//        else if (VPComment.VPReceivedIP() == true)
//        {
//            OrganisationalApprover(Status, proposal, SupportStatus, FinanceSignature, flag, sla);
//        }
//    }

//}

//private static void IPGreaterThanOrEqualToThirdQuartile(Label Status, Proposal proposal, SupportApprovalStatus SupportStatus, FinanceSignatureApproval FinanceSignature, TextBox flag, SLA sla)
//{
//    try
//    {
//        //Check for the MD's support
//        MDComments MDComment = new MDComments(proposal.IDPROPOSAL);
//        bool MDStand = MDComment.MDStand();
//        if (MDStand == false)
//        {
//            Status.Text = "Awaiting MD's Support";
//            flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//        }
//        else if (MDStand == true) //if MD has approved, check for the GM Regional Planning's support.
//        {
//            GMREPLANComments GMREComment = new GMREPLANComments(proposal.IDPROPOSAL);
//            bool GMREStand = GMREComment.GMREPlanStand();
//            if (GMREStand == false)
//            {
//                Status.Text = "Awaiting GM Regional Planning's Support";
//                flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//            }
//            else if (GMREStand == true)
//            {
//                //VPComments VPComment = new VPComments(proposal.IDPROPOSAL);
//                //bool VPStand = VPComment.VPStand(proposal.IDPROPOSAL);

//                //check if all VPs have supported the IP
//                SupportApprovalStatus support = new SupportApprovalStatus();
//                bool VPStand = support.HasAllVPSupportedIP(proposal.IDPROPOSAL);

//                if (VPStand == false)
//                {
//                    Status.Text = "Awaiting VPs Support";
//                    flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//                }
//                else if (VPStand == true)
//                {
//                    REVPComments REVPComment = new REVPComments(proposal.IDPROPOSAL);
//                    bool REVPStand = REVPComment.REVPApprovalStand();
//                    if (REVPStand == false)
//                    {
//                        Status.Text = "Awaiting Regional VP's Approval";
//                        flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//                    }
//                    else if (REVPStand == true)
//                    {
//                        Status.Text = "Approved";
//                        flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//                    }
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
//    }
//}

//private static void OrganisationalApprover(Label Status, Proposal proposal, SupportApprovalStatus SupportStatus, FinanceSignatureApproval FinanceSignature, TextBox flag, SLA sla)
//{
//    try
//    {
//        bool Approved = false;
//        VPComments VPComment = new VPComments(proposal.IDPROPOSAL);
//        MDComments MDComment = new MDComments(proposal.IDPROPOSAL);

//        //Check if BPO has forwarded the IP to a VP or MD for Organisational Approval
//        if (VPComment.VPReceivedIP() == true)
//        {
//            Approved = VPComment.VPApprovalStand();
//            GetOrganisationalApproval(Status, proposal, SupportStatus, FinanceSignature, flag, sla, Approved);
//        }
//        else if (MDComment.MDReceivedIP() == true)
//        {
//            Approved = MDComment.MDStand();
//            GetOrganisationalApproval(Status, proposal, SupportStatus, FinanceSignature, flag, sla, Approved);
//        }
//        else
//        {
//            flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//            Status.Text = "BPO not yet forwarded IP for Organisational Approval";
//        }
//    }
//    catch (Exception ex)
//    {
//        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
//    }
//}

//private static void GetOrganisationalApproval(Label Status, Proposal proposal, SupportApprovalStatus SupportStatus, FinanceSignatureApproval FinanceSignature, TextBox flag, SLA sla, bool Approved)
//{
//    try
//    {
//        //Test for the value of Approved
//        VPComments VPComment = new VPComments(proposal.IDPROPOSAL);
//        MDComments MDComment = new MDComments(proposal.IDPROPOSAL);

//        if ((Approved == false) && (MDComment.MDReceivedIP() == true))
//        {
//            flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//            Status.Text = "Awaiting MD's Support.";
//        }
//        else if ((Approved == false) && (VPComment.VPReceivedIP() == false))
//        {
//            flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//            Status.Text = "BPO yet to forward IP for final Organisational Approval";
//        }
//        else
//        {
//            bool IPLT3rdQuartileGTEQ2ndQuartile = SupportStatus.IPLTEQ3rdQuartileGT2ndQuartile(proposal);
//            if (IPLT3rdQuartileGTEQ2ndQuartile == true)
//            {
//                //Check if Organisational Support has approved the IP
//                if (Approved == true)
//                {
//                    Status.Text = "Approved";
//                    flag.BackColor = System.Drawing.Color.FromName(sla.m_sWithinSLA);
//                }
//                else if (Approved == false)
//                {
//                    flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//                    Status.Text = "Awaiting VP's Approval";
//                }
//            }
//            else if (IPLT3rdQuartileGTEQ2ndQuartile == false)
//            {
//                //If the required Finance Signature has supported, check if the VP required has approved the IP
//                if (Approved == true)
//                {
//                    Status.Text = "Approved";
//                    flag.BackColor = System.Drawing.Color.FromName(sla.m_sWithinSLA);
//                }
//                else if (Approved == false)
//                {
//                    flag.BackColor = System.Drawing.Color.FromName(sla.m_sUnderApproval);
//                    Status.Text = "Awaiting VP's Approval";
//                }
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
//    }
//}  


//If the Proposal has been approved disable the delete button
//bool DocStandAPPROVED = proposal.DocStandAPPROVED(ProposalID);
//if(proposal.DOC_STAND == (int) ipStatusRpt.Approved)
//if (DocStandAPPROVED == true)

//if (oProposal.m_iDoc_Stand == (int)ipStatusRpt.IPIntiatorDevIP)
//{
//    Status.Text = ipStatusRptDesc(ipStatusRpt.IPIntiatorDevIP); //"IP Intiator still developing IP...";
//    //flag.BackColor = System.Drawing.Color.FromName(oSla.m_sUnderConstruction);
//}
//else if ((oProposal.m_lSS == 0) || ((oProposal.m_lSS == 0)))
//{
//decimal decAmountSS = decimal.Parse(AmountSS.Text);
//int iAmountSS = Convert.ToInt32(decAmountSS * 100);

//Check if IP was submitted
//if ((oProposal.m_sDate_Submit == null) || (oProposal.m_sDate_Submit == ""))
//{
//    Status.Text = ipStatusRptDesc(ipStatusRpt.IPNotYetSub); //"IP not yet submitted by Inititator.";
//    //flag.BackColor = System.Drawing.Color.FromName(oSla.m_sUnderConstruction);
//}
//else if ((oProposal.m_sDate_Submit != null) && (oProposal.m_sDate_Submit != ""))
//{
//if IP was submitted, check if the Line Team Lead has supported the IP
//LineTeamLeadComments ltlComment = new LineTeamLeadComments(ProposalID);
//bool LineTeamLeadSupported = ltlComment.LineTeamLeadStand();


//supportProcess(oProposal, flag, Status, OnlineUser);

//TODO: COme back here to ensure the new system works well
//if (LineTeamLeadSupported == true)
//{
//    //if Line Team Lead supported the IP, check if Business Process Owner supported the IP
//    BPOComments bpoComment = new BPOComments(ProposalID);
//    bool HasBPOSupported = bpoComment.BPOStand();
//    if (HasBPOSupported == false)
//    {
//        flag.BackColor = System.Drawing.Color.FromName(sla.UNDERAPPROVAL);
//        Status.Text = "Awaiting Business Process Owner's Support";
//    }
//    else if (HasBPOSupported == true)
//    {
//        //If BPO supported the IP, 
//        //Check if BPO has forwarded the IP to required Support Functions
//        bool HasBPOForwardedIPToSupportFunctions = BPO.BPOForwadedIPToFunctionalSuppport(proposal);
//        if (HasBPOForwardedIPToSupportFunctions == false)
//        {
//            Status.Text = "BPO yet to forward IP for Functional Support";
//            flag.BackColor = System.Drawing.Color.FromName(sla.WITHINSLA);
//        }
//        else if (HasBPOForwardedIPToSupportFunctions == true)
//        {
//            //check if all required support function have supported the IP
//            //bool HasAllSupported = SupportStatus.CheckMandatorySupportFunctionsStatus(proposal);
//            FunctionalSupportComments FSupportComment = new FunctionalSupportComments();
//            bool RequiredMandatorySupport = FSupportComment.FuntionalSupportStand(proposal);
//            if (RequiredMandatorySupport == false)
//            {
//                Status.Text = "Functional Support in Progress...";
//                flag.BackColor = System.Drawing.Color.FromName(sla.WITHINSLA);
//            }
//            else if (RequiredMandatorySupport == true)
//            {
//                if (SupportStatus.IPGT2ndQuartile(proposal) == false) //Route 1. IP < $10mln
//                {
//                    IPLessThanSecondQuartile(Status, proposal, SupportStatus, financeSignature, flag, sla);
//                }
//                else if (SupportStatus.IPGT2ndQuartile(proposal) == true) //Route 2 IP >= $10mln and < $20mln
//                {
//                    IPGreaterThanSecondQuartile(Status, proposal, SupportStatus, financeSignature, flag, sla);
//                }
//            }
//        }
//    }
//}
//}
//}
//}
//}