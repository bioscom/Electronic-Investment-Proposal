using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;

/// <summary>
/// Summary description for SLA
/// </summary>
/// 

public class SLA
{
    public string m_sUnderConstruction { get; set; }
    public string m_sUnderApproval { get; set; }
    public string m_sWithinSLA { get; set; }
    public string m_sOutSideSLA { get; set; }
    public string m_sExceedSLA { get; set; }
    public string m_sASLA { get; set; }
    public string m_sFSSLA { get; set; }   

    public SLA()
    {

    }

    public SLA(DataRow dr)
    {
        try
        {
            m_sASLA = dr["ASLA"].ToString();
            m_sFSSLA = dr["FSSLA"].ToString();
            m_sUnderConstruction = dr["UNDERCONSTR"].ToString();
            m_sUnderApproval = dr["UNDERAPPROVAL"].ToString();
            m_sWithinSLA = dr["WITHINSLA"].ToString();
            m_sOutSideSLA = dr["OUTSIDESLA"].ToString();
            m_sExceedSLA = dr["EXCEEDSLA"].ToString();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    
}

public class SLAMgt
{
    public SLAMgt()
    {

    }

    public DataTable dtGetSLASettings()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getSLASettings();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public SLA objGetSLASettings()
    {
        DataTable dt = dtGetSLASettings();
        SLA result = new SLA();
        foreach (DataRow dr in dt.Rows)
        {
            result = new SLA(dr);
        }
        return result;
    }

    public void ServiceLevelAgreement(appUsers OnlineUser)
    {
        //Check today's date against the date in EIP_SLA table

        //TODO: Please come back here to complete the code

        string sql = "SELECT TO_CHAR(TODAYSDATE, 'DS')TODAYSDATE FROM EIP_SLA WHERE (TO_CHAR(SYSDATE, 'IW') <> TO_CHAR(TODAYSDATE, 'IW')) AND COMPANYID = '" + OnlineUser.m_iCompany + "'";
        //try
        //{
        //    OracleDataReader reader = db.oReader(sql);
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            SendReminderMail(OnlineUser);
        //        }

        //        string sqlUpdate = "UPDATE EIP_SLA SET TODAYSDATE = TO_DATE('" + DateTime.Today.Date.ToShortDateString() + "', 'mm/dd/yyyy') WHERE COMPANYID = '" + OnlineUser.m_iCompany + "'";
        //        DataAccess.ExecuteNonQueryCommand(sqlUpdate);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        //}
    }

    public void SendReminderMail(appUsers OnlineUser)
    {
        ProposalApprovalDetailsMgt oProposalApprovalDetailsMgt = new ProposalApprovalDetailsMgt();
        List<ProposalApprovalDetails> oProposalApprovalDetails = new List<ProposalApprovalDetails>();

        ProposalMgt oProposalMgt = new ProposalMgt();
        List<Proposal> oProposals = oProposalMgt.lstGetIPTrackingLoadProposal(); //Get the list of proposals not yet approved

        foreach (Proposal oProposal in oProposals)
        {
            //For each proposal not yet approved, get the support/approval details
            oProposalApprovalDetails = oProposalApprovalDetailsMgt.lstGetProposalSupportDetailsByProposalId(oProposal.m_lProposalId);
            foreach (ProposalApprovalDetails oProposalApprovalDetail in oProposalApprovalDetails)
            {
                if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Business_Process_Owner)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Controllers)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Economics_Support)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Director)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Finance_Signature)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.HSE_Support)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.IT)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Line_Team_Lead)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.LEGAL_Support)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.REVP)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.SCM)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Security_Support)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.SPCA_Support)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.TAX_Support)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Technical_Planning_Manager)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.Treasury_Support)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
                else if (oProposalApprovalDetail.m_iUserRoleId == (int)appUsersRoles.userRole.VP)
                    SendSLA(oProposalApprovalDetail, OnlineUser, oProposal);
            }
        }
    }

    private void SendSLA(ProposalApprovalDetails oProposalApprovalDetail, appUsers OnlineUser, Proposal oProposal)
    {
        if (DateTime.Compare(DateTime.Parse(oProposalApprovalDetail.m_sDateReceived), System.DateTime.Today) > int.Parse(objGetSLASettings().m_sFSSLA))
        {
            SLASendMail(oProposalApprovalDetail.m_iUserId, OnlineUser, oProposal);
        }
    }
            
    private void SLASendMail(int iUserID, appUsers OnlineUser, Proposal oProposal)
    {
        appUserMgt oappUserMgt = new appUserMgt();
        appUsers oReceiver = oappUserMgt.objGetUserByUserId(iUserID);

        SLAAuditTrail(iUserID, OnlineUser, oProposal); //Here the EIP_SLA_AUDIT_TRAIL table is updated against each user who receives SLA mail

        sendMail oSendMail = new sendMail(OnlineUser.structUserIdx);
        bool success = oSendMail.SupportFunctionSLA(oReceiver.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
        //TODO: Not sure this should go with this
        if (success)
        {
            AppStatusMessages.SLANonCompliance(oReceiver.m_sFullName);
        }
    }

    private void SLAAuditTrail(int iToUserID, appUsers OnlineUser, Proposal oProposal)
    {
        //TODO: make this code Vera Code compliant
        //Here the EIP_SLA_AUDIT_TRAIL table is updated against each user who receives SLA mail
        string sql = "INSERT INTO EIP_SLA_AUDIT_TRAIL (IDUSERMGT, DATE_RECEIVED, TIME_RECEIVED, IDUSERMGT_SENDER, IDPROPOSAL) ";
        sql += "VALUES ('" + iToUserID + "', TO_DATE('" + DateTime.Today.Date.ToShortDateString() + "', 'mm/dd/yyyy'), ";
        sql += "'" + String.Format("{0:T}", DateTime.Now) + "', '" + OnlineUser.m_iUserId + "', '" + oProposal.m_lProposalId + "')";

        DataAccess.ExecuteNonQueryCommand(sql);
    }
}

//        string ProposalID = dr["IDPROPOSAL"].ToString();

//        LineTeamLeadComments LTL = new LineTeamLeadComments(ProposalID);
//        if (LTL.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            if (DateTime.Compare(DateTime.Parse(LTL.sDATERECEIVED), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//            {
//                SLASendMail(LTL.iUserId, CurrentUser, proposal);
//            }
//        }

//        //SLA for BPO not part of the requirement (for now);

//        SupportApproverComments funcSupport = new SupportApproverComments(ProposalID);
//        if (funcSupport.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            foreach (DataRow drr in funcSupport.ServiceLevelAgreement().Rows)
//            {
//                if (DateTime.Compare(DateTime.Parse(drr["DATE_RECEIVED"].ToString()), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//                {
//                    SLASendMail(funcSupport.iIDUSERMGT, CurrentUser, proposal);
//                }
//            }
//        }

//        FinanceSignatureComments FinSig = new FinanceSignatureComments(ProposalID);
//        if (FinSig.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            if (DateTime.Compare(DateTime.Parse(FinSig.sDATERECEIVED), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//            {
//                SLASendMail(FinSig.iIDUSERMGT, CurrentUser, proposal);
//            }
//        }

//        GMFinanceComments GMFin = new GMFinanceComments(ProposalID);
//        if (GMFin.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            if (DateTime.Compare(DateTime.Parse(GMFin.sDATERECEIVED), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//            {
//                SLASendMail(GMFin.iIDUSERMGT, CurrentUser, proposal);
//            }
//        }

//        GMComments GM = new GMComments(ProposalID);
//        if (GM.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            if (DateTime.Compare(DateTime.Parse(GM.sDATERECEIVED), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//            {
//                SLASendMail(GM.iIDUSERMGT, CurrentUser, proposal);
//            }
//        }

//        CorporatePlanningManager CPM = new CorporatePlanningManager(ProposalID);
//        if (CPM.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            if (DateTime.Compare(DateTime.Parse(CPM.sDATERECEIVED), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//            {
//                SLASendMail(CPM.iIDUSERMGT, CurrentUser, proposal);
//            }
//        }

//        MDComments MD = new MDComments(ProposalID);
//        if (MD.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            if (DateTime.Compare(DateTime.Parse(MD.sDATERECEIVED), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//            {
//                SLASendMail(MD.iIDUSERMGT, CurrentUser, proposal);
//            }
//        }

//        VPFinanceComments VPFin = new VPFinanceComments(ProposalID);
//        if (VPFin.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            if (DateTime.Compare(DateTime.Parse(VPFin.sDATERECEIVED), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//            {
//                SLASendMail(VPFin.iIDUSERMGT, CurrentUser, proposal);
//            }
//        }

//        GMREPLANComments GMREPlan = new GMREPLANComments(ProposalID);
//        if (GMREPlan.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            if (DateTime.Compare(DateTime.Parse(GMREPlan.sDATERECEIVED), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//            {
//                SLASendMail(GMREPlan.iIDUSERMGT, CurrentUser, proposal);
//            }
//        }

//        VPComments VP = new VPComments(ProposalID);
//        if (VP.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            foreach (DataRow drvp in funcSupport.ServiceLevelAgreement().Rows)
//            {
//                if (DateTime.Compare(DateTime.Parse(drvp["DATE_RECEIVED"].ToString()), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//                {
//                    SLASendMail(VP.iIDUSERMGT, CurrentUser, proposal);
//                }
//            }
//        }

//        REVPComments REVP = new REVPComments(ProposalID);
//        if (REVP.ServiceLevelAgreement().Rows.Count > 0)
//        {
//            if (DateTime.Compare(DateTime.Parse(REVP.sDATERECEIVED), System.DateTime.Today) > int.Parse(Mysla.m_sFSSLA))
//            {
//                SLASendMail(REVP.iIDUSERMGT, CurrentUser, proposal);
//            }
//        }
//    }
//}
