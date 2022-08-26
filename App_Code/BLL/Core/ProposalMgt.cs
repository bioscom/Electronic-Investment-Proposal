using System;
using System.Data;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Summary description for ProposalMgt
/// </summary>

public class ProposalMgt
{
    public ProposalMgt()
    {

    }

    //public DataTable dtGetProposalProposals()
    //{
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = StoredProcedure.getProposals();

    //    return GenericDataAccess.ExecuteSelectCommand(comm);
    //}

    #region Get Pending and Approved Proposals

    public DataTable dtGetPendingProposals()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getPendingProposals();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DISCONTINUE";
        param.Value = IPStatus.iDiscontinued;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = (int)IPStatusReporter.ipStatusRpt.Approved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    #endregion 


    #region Get IP Waitinf Support

    public DataTable dtGetIPWaitingMySupport(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.ProposalsPendingMyAction();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":xSTAND";
        param.Value = SupportState.iSupportApproverStandDefault;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":ySTAND";
        param.Value = SupportState.iNotApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":zSTAND";
        param.Value = SupportState.iNotSupported;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
         
        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public List<Proposal> lstGetIPWaitingMySupport(int iUserId)
    {
        DataTable dt = dtGetIPWaitingMySupport(iUserId);

        List<Proposal> xRows = new List<Proposal>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new Proposal(dr));
        }
        return xRows;
    }

    public string IPAwaitingThisUserSupport(int iUserId, int iRoleId)
    {
        StringBuilder awaitingSupport = new StringBuilder();
        DataTable dt = dtGetIPWaitingMySupport(iUserId);

        List<Proposal> lstProposal = lstGetIPWaitingMySupport(iUserId);
        foreach (Proposal oProposal in lstProposal)
        {
            awaitingSupport.Append(oProposal.m_sProj_Num + " - " + oProposal.m_sProj_Title);
        }

        return awaitingSupport.ToString();
    }


    public List<string> lstIPAwaitingThisUserSupport(int iUserId, int iRoleId)
    {
        List<string> sResult = new List<string>();
        DataTable dt = dtGetIPWaitingMySupport(iUserId);

        List<Proposal> lstProposal = lstGetIPWaitingMySupport(iUserId);
        foreach (Proposal oProposal in lstProposal)
        {
            sResult.Add(oProposal.m_sProj_Num + " - " + oProposal.m_sProj_Title);
        }

        return sResult;
    }

    #endregion


    #region Deleted IPs 
    public DataTable dtGetDeletedProposal()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getDeletedProposals();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Deactivated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    #endregion

    //public void DropDownYearFiller(DropDownList theDropDownList, string sql)
    //{
    //    DataTable dt = DataAccess.ExecuteQueryCommand(sql);

    //    theDropDownList.Items.Clear();
    //    theDropDownList.Items.Add(new ListItem("<<Select Year>>", "-1"));
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        string sListItem = dr[0].ToString();
    //        theDropDownList.Items.Add(new ListItem(sListItem));
    //    }
    //}

    public bool UndeleteProposal(long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UndeleteProposal();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public bool DiscontinueProposal(long lProposalId, int iStatus)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.DiscontinueReactivateProposal();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DISCONTINUE";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public bool UpdateFileName(long lProposalId, string sFileName)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UpdateFileName();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROPOSALFILENAME";
        param.Value = sFileName;
        param.DbType = DbType.String;
        param.Size = 2000;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public bool DelegateIPInitiatorRole(int Old_IPInitiator_UserID, int New_IPInitiator_UserID, long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.reRouteIPtoIPInitiator();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":NEWIDUSERMGT";
        param.Value = New_IPInitiator_UserID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":OLDIDUSERMGT";
        param.Value = Old_IPInitiator_UserID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return (result != -1);
    }

    public bool DeactivateProposal(long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UndeleteProposal();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Deactivated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public bool DeleteProposal(long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.DeleteProposal();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Deactivated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public DataTable dtGetAuditTrail()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getAuditTrail();

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetAuditTrailByDate(DateTime fromDate, DateTime toDate)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getAuditTrailByDate();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":FROMDATE";
        param.Value = fromDate;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":TODATE";
        param.Value = toDate;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetProposalSupportApprovalDetailsByRole(long lProposalId, int iRole)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.GetProposalApprovalDetailsByRole();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRole;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetProposalSupportApprovalDetails(long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.GetProposalApprovalDetailsByRole();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetProposalByProposalId(long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getProposalByProposalId();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public Proposal objGetProposalById(long lProposalId)
    {
        DataTable dt = dtGetProposalByProposalId(lProposalId);

        Proposal xRows = new Proposal();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new Proposal(dr);
        }
        return xRows;
    }

    //public List<Proposal> lstGetProposals()
    //{
    //    DataTable dt = dtGetProposalProposals();

    //    List<Proposal> xRows = new List<Proposal>(dt.Rows.Count);
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        xRows.Add(new Proposal(dr));
    //    }
    //    return xRows;
    //}

    public List<Proposal> lstGetPendingProposals()
    {
        DataTable dt = dtGetPendingProposals();

        List<Proposal> xRows = new List<Proposal>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new Proposal(dr));
        }
        return xRows;
    }

    public List<ProposalApprovalDetails> lstGetProposalSupportApprovalDetails(long lProposalId)
    {
        DataTable dt = dtGetProposalSupportApprovalDetails(lProposalId);

        List<ProposalApprovalDetails> xRows = new List<ProposalApprovalDetails>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new ProposalApprovalDetails(dr));
        }
        return xRows;
    }

    public ProposalApprovalDetails objGetProposalSupportApprovalDetailsByRole(long lProposalId, int iRoleid)
    {
        DataTable dt = dtGetProposalSupportApprovalDetailsByRole(lProposalId, iRoleid);

        ProposalApprovalDetails xRows = new ProposalApprovalDetails();
        foreach (DataRow dr in dt.Rows)
        {
            xRows= new ProposalApprovalDetails(dr);
        }
        return xRows;
    }

    public List<ProposalApprovalDetails> lstGetProposalSupportApprovalDetailsByRole(long lProposalId, int iRoleid)
    {
        DataTable dt = dtGetProposalSupportApprovalDetailsByRole(lProposalId, iRoleid);

        List<ProposalApprovalDetails> xRows = new List<ProposalApprovalDetails>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new ProposalApprovalDetails(dr));
        }
        return xRows;
    }

    public bool CreateProposal(Proposal eProposal, FileUpload UploadProposal, int iUserId, ref long lProposalId)
    {
        //Put the Proposal Document into the file folder, and return the name with which it was saved
        string oMessage = "";
        SaveIP2FileSystem UpLoadMe = new SaveIP2FileSystem();
        fileProperty MyFileProperties = UpLoadMe.UploadInvestmentProposal(UploadProposal, eProposal.m_sProj_Num, ref oMessage); //return the name with which proposal pdf doc was saved

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.CreateProposal();

        lProposalId = GetProposalID();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJ_NUM";
        param.Value = eProposal.m_sProj_Num;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJ_TITLE";
        param.Value = eProposal.m_sProj_Title;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJ_DESC";
        param.Value = eProposal.m_sProj_Desc;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROPOSALFILENAME";
        param.Value = MyFileProperties.sFileName;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_INIT";
        param.Value = DateTime.Today.Date.ToShortDateString();
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_SUBMIT";
        param.Value = DateTime.Today.Date.ToShortDateString();
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_LAST_ACTIONED";
        param.Value = DateTime.Today.Date.ToShortDateString();
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":JV";
        param.Value = eProposal.m_lJV;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SS";
        param.Value = eProposal.m_lSS;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":EPPRIORITYID";
        param.Value = eProposal.m_iEppriorityId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return (result != -1);
    }

    public int NoAwaitingSupport(long lProposalId)
    {
        int iReturnValue = 0;
        int xNo = 0; int xNoofGMs = 0;
        
        SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
        List<SupportApproverComments> oComments = oSupportApproverCommentMgt.lstGetFunctionalSupportsApproverComments(lProposalId);

        foreach (SupportApproverComments oComment in oComments)
        {
            //Note: if VP is found, do not count the funtional Support that have defaulted the SLA.
            if (oComment.m_iUserRoleId == (int)appUsersRoles.userRole.VP)
            {
                if ((oComment.m_iStand == SupportState.iSupportApproverStandDefault) || (oComment.m_iStand == SupportState.iNotSupported) || (oComment.m_iStand == SupportState.iNotApproved))
                    xNoofGMs += 1;
            }
            else
            {
                if ((oComment.m_iStand == SupportState.iSupportApproverStandDefault) || (oComment.m_iStand == SupportState.iNotSupported) || (oComment.m_iStand == SupportState.iNotApproved))
                    xNo += 1;
            }
        }

        if (xNoofGMs == 0) iReturnValue = xNo;
        else iReturnValue = xNoofGMs;
        
        return iReturnValue;
    }

    private long GetProposalID()
    {
        long ProposalId = 0;
        try
        {
            string sql = "SELECT PROPOSAL_SEQ.NEXTVAL FROM DUAL";
            DataTable dt = DataAccess.ExecuteQueryCommand(sql);
            ProposalId = Convert.ToInt64(dt.Rows[0]["NEXTVAL"].ToString());
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return ProposalId;
    }

    public int OriginatingUnit(Proposal proposal)
    {
        int EPNIGERIALOGIC = 0;

        if (proposal.m_sIPOriginatingUnit == "SPDC")
        {
            EPNIGERIALOGIC = 1;
        }
        else if (proposal.m_sIPOriginatingUnit == "SNEP")
        {
            EPNIGERIALOGIC = 2;
        }

        return EPNIGERIALOGIC;
    }

    public bool UpdateProposalStatus(long lProposalId, int iStatus)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.UpdateProposalStatus();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = iStatus;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }

    public bool CloseGMREPlanIntray(long lProposalId, int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.CloseGMREPlanIntray();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":STAND";
        param.Value = (int)SupportState.iSupported;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SUPPORT_BIT";
        param.Value = 1;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);
        
        param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        // result will be 1 in case of success
        return (result != -1);
    }


    #region IP Tracking Register

    public DataTable dtGetIPTrackingLoadProposal()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getPendingProposals();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = SupportState.iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DISCONTINUE";
        param.Value = IPStatus.iDiscontinued;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public Proposal objGetIPTrackingLoadProposal()
    {
        DataTable dt = dtGetIPTrackingLoadProposal();
        Proposal result = new Proposal();
        foreach (DataRow dr in dt.Rows)
        {
            result = new Proposal(dr);
        }
        return result;
    }

    public List<Proposal> lstGetIPTrackingLoadProposal()
    {
        List<Proposal> result = new List<Proposal>();
        DataTable dt = dtGetIPTrackingLoadProposal();
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new Proposal(dr));
        }
        return result;
    }

    public DataTable dtGetDiscontinuedProposal()
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getDiscontinuedProposals();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DISCONTINUE";
        param.Value = IPStatus.iDiscontinued;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable IPTrackingLoadEPGProposal(int Approved)
    {
        string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, ";
        sql += "EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_PROPOSAL.PROPOSALFILENAME, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, ";
        sql += "TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED, EIP_USERMGT.FULLNAME AS PROJ_INIT, EIP_USERMGT.USERMAIL ";
        sql += "FROM EIP_PROPOSAL, EIP_USERMGT WHERE (EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT) ";

        if (Approved == 2)
        {
            sql += "AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "') ";
            sql += "AND (EIP_PROPOSAL.DOC_STAND = '" + SupportState.iApproved + "') ORDER BY IDPROPOSAL DESC";

            //sql += "AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "') AND (EIP_PROPOSAL.SS >= '" + IPLimit.iTHIRDQUARTILE + "') ";
            //sql += "AND (EIP_PROPOSAL.DOC_STAND = '" + SupportState.iApproved + "') ORDER BY IDPROPOSAL DESC";
        }
        else
        {
            sql += "AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "') ";
            sql += "ORDER BY IDPROPOSAL DESC";

            //sql += "AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "') AND (EIP_PROPOSAL.SS >= '" + IPLimit.iTHIRDQUARTILE + "') ";
            //sql += "ORDER BY IDPROPOSAL DESC";
        }

        return DataAccess.ExecuteQueryCommand(sql);
    }


    public DataTable IPTrackingLoadApprovedProposalByYearOfApproval(int iYYear)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getApprovedProposalsByYearOfApproval();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":iYYear";
        param.Value = iYYear;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = (int)IPStatusReporter.ipStatusRpt.Approved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":GM";
        //param.Value = (int)appUsersRoles.userRole.VP;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":REVP";
        //param.Value = (int)appUsersRoles.userRole.REVP;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":STAND";
        //param.Value = SupportState.iApproved;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetApprovedProposal()
    {
        //IPLimit.IPLevels oIPLimit = new IPLimit.IPLevels();

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getApprovedProposals();

        // create a new parameter

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = (int)IPStatusReporter.ipStatusRpt.Approved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":GM";
        //param.Value = (int)appUsersRoles.userRole.VP;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":REVP";
        //param.Value = (int)appUsersRoles.userRole.REVP;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":CERP";
        //param.Value = (int)appUsersRoles.userRole.CERP;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":STAND";
        //param.Value = SupportState.iApproved;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //public DataTable IPTrackingLoadProposalFunctionalPlanner(appUsers CurrentUser)
    //{
    //    string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, ";
    //    sql += "EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_PROPOSAL.PROPOSALFILENAME, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, ";
    //    sql += "TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED, EIP_USERMGT.FULLNAME AS PROJ_INIT, EIP_USERMGT.USERMAIL ";
    //    sql += "FROM CPDMS_FUNCTIONS ";
    //    sql += "INNER JOIN EIP_USERMGT ON CPDMS_FUNCTIONS.FUNCTIONID = EIP_USERMGT.FUNCTIONID ";
    //    sql += "INNER JOIN EIP_PROPOSAL ON EIP_USERMGT.IDUSERMGT = EIP_PROPOSAL.IDUSERMGT ";
    //    sql += "WHERE (CPDMS_FUNCTIONS.FUNCTIONID = '" + CurrentUser.iFUNCTION + "') ";
    //    sql += "AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "') ORDER BY EIP_PROPOSAL.IDPROPOSAL DESC";

    //    return DataAccess.ExecuteQueryCommand(sql);
    //}


    #endregion


    #region  Module to Edit Proposal

    public bool EditProposalWtFile(Proposal eProposal, FileUpload UploadProposal, int iUserId, ref string mssg) 
    {
        SaveIP2FileSystem UpLoadMe = new SaveIP2FileSystem();
        fileProperty MyFileProperties = UpLoadMe.UploadInvestmentProposal(UploadProposal, eProposal.m_sProj_Num, ref mssg); //return the name with which proposal pdf doc was saved

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.EditProposal();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = eProposal.m_lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJ_TITLE";
        param.Value = eProposal.m_sProj_Title;
        param.DbType = DbType.String;
        param.Size = 2000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJ_DESC";
        param.Value = eProposal.m_sProj_Desc;
        param.DbType = DbType.String;
        param.Size = 2000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROPOSALFILENAME";
        param.Value = MyFileProperties.sFileName;
        param.DbType = DbType.String;
        param.Size = 2000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_LAST_ACTIONED";
        param.Value = DateTime.Today.Date.ToShortDateString();
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":JV";
        param.Value = eProposal.m_lJV;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SS";
        param.Value = eProposal.m_lSS;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":EPPRIORITYID";
        param.Value = eProposal.m_iEppriorityId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return (result != -1);
    }

    public bool EditProposalWithoutPDFFile(Proposal oProposal, int iUserId, FileUpload UploadProposal)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.EditProposalWithoutFileUpload();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = oProposal.m_lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJ_TITLE";
        param.Value = oProposal.m_sProj_Title;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJ_DESC";
        param.Value = oProposal.m_sProj_Desc;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_LAST_ACTIONED";
        param.Value = DateTime.Today.Date.ToShortDateString();
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":JV";
        param.Value = oProposal.m_lJV;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SS";
        param.Value = oProposal.m_lSS;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":EPPRIORITYID";
        param.Value = oProposal.m_iEppriorityId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return (result != -1);
    }


    #endregion


    public bool AuditTrail(appUsers CurrentUser, int iStand, string comment, long lProposalId)
    {
        string sql = "INSERT INTO EIP_AUDITTRAIL (IDPROPOSAL, IDUSERMGT, STAND, CCOMMENT, SUPPORT, SUPPORTFULLNAME) ";
        sql += "VALUES (:lProposalId, :iUserId, :iStand, :sComment, :iUserIdSupport, :sFullName)";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":lProposalId";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iUserId";
        param.Value = CurrentUser.m_iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStand";
        param.Value = iStand;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sComment";
        param.Value = comment;
        param.DbType = DbType.String;
        param.Size = 2000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iUserIdSupport";
        param.Value = CurrentUser.m_iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":sFullName";
        param.Value = CurrentUser.m_sFullName;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return (result != -1);
    }

    public bool DateLastActioned(long lProposalId, appUsers CurrentUser)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.DateProposalLastActioned();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_LAST_ACTIONED";
        param.Value = DateTime.Today.Date;
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return (result != -1);
    }

    #region Proposal Not Supported Region

    public void IPInitiatorReloadsUpdate(Proposal oProposal, appUsers logOnUser, string mSubject, string _url)
    {
        //This Method will send mail to Line Team Lead, BPO and all Support Functions and Approver 
        //when a not supported IP comment is updated and reattached back to the system by the IP Initiator

        sendMail oSendMail = new sendMail(logOnUser.structUserIdx);
        oSendMail.IPUpdateReloadNotification(getEmailAddressForIPLine(oProposal.m_lProposalId), mSubject, _url, oProposal.m_sProj_Num);
    }

    public bool ProposalNotSupported(Proposal proposal, appUsers logOnUser, string Comment)
    {
        appUserMgt oAppUserMgt = new appUserMgt();

        //1.    Stop all Reminders 
        sendMail oSendMail = new sendMail(logOnUser.structUserIdx);
        IPNSACOUNTER(proposal.m_lProposalId); //To show the number of times an IP underwent changes
        return oSendMail.IPNotSupported(getEmailAddressForIPLine(proposal.m_lProposalId), oAppUserMgt.objGetUserByUserId(proposal.m_iUserId).structUserIdx, 
                            proposal.m_sProj_Title, Comment, logOnUser.m_sFullName, logOnUser.eFunction.m_sFunction, proposal.m_sProj_Num);
    }

    public List<structUserMailIdx> getEmailAddressForIPLine(long lProposalId)
    {
        appUserMgt oappUserMgt = new appUserMgt();
        List<structUserMailIdx> AllUsers = new List<structUserMailIdx>();

        try
        {
            //Gets email addresses of all Approvers who have received the IP
            ProposalApprovalDetailsMgt oProposalDetails = new ProposalApprovalDetailsMgt();

            List<ProposalApprovalDetails> lstProposalDetails = oProposalDetails.lstGetProposalSupportDetailsByProposalId(lProposalId); 
            foreach (ProposalApprovalDetails oProposalDetail in lstProposalDetails)
            {
                AllUsers.Add(oappUserMgt.objGetUserByUserId(oProposalDetail.m_iUserId).structUserIdx);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return AllUsers;
    }

    private void IPNSACOUNTER(long lProposalId)
    {
        try
        {
            string sql = "SELECT NSACOUNTER FROM EIP_PROPOSAL WHERE IDPROPOSAL = '" + lProposalId + "'";
            DataTable dt = new DataTable();

            dt = DataAccess.ExecuteQueryCommand(sql);
            int kounter = Convert.ToInt32(dt.Rows[0]["NSACOUNTER"]);

            kounter += 1;
            string sqlUpdate = "UPDATE EIP_PROPOSAL SET NSACOUNTER = '" + kounter + "' WHERE IDPROPOSAL = '" + lProposalId + "'";
            DataAccess.ExecuteNonQueryCommand(sqlUpdate);
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message.ToString());
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    public bool IPNotSupportedApproved(long lProposalId)
    {
        bool IPNotSupported = false;

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.NoOfIPNotSupportedApproved();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
        if (Convert.ToInt32(dt.Rows[0]["NSACOUNTER"]) > 0)
        {
            IPNotSupported = true;
        }
        return IPNotSupported;
    }
    
    #endregion


    

    public DataTable DtGetRequiredSupportApprovers(int iRoleId, decimal lSS)
    {
        IPLimit oIPLimit = new IPLimit();
        int iShellShare = Convert.ToInt32(lSS * 100);
        int iLevel1 = 0; int iLevel2 = 0; int iLevel3 = 0; int iLevel4 = 0; int iLevel5 = 0; 

        DataTable dt = new DataTable();

        List<IPLimit> iIPLimits = oIPLimit.lstGetIPLimits();
        foreach (IPLimit iIPLimit in iIPLimits)
        {
            if (iIPLimit.m_iSeq == 1) iLevel1 = iIPLimit.m_iIPLimit;
            else if (iIPLimit.m_iSeq == 2) iLevel2 = iIPLimit.m_iIPLimit;
            else if (iIPLimit.m_iSeq == 3) iLevel3 = iIPLimit.m_iIPLimit;
            else if (iIPLimit.m_iSeq == 4) iLevel4 = iIPLimit.m_iIPLimit;
            else if (iIPLimit.m_iSeq == 5) iLevel5 = iIPLimit.m_iIPLimit;
        }
        
        List<IPLimit> xIPLimits = oIPLimit.lstGetIPLimits();
        foreach (IPLimit xIPLimit in xIPLimits)
        {
            if (xIPLimit.m_iSeq == 1)
            {
                if (iShellShare <= (xIPLimit.m_iIPLimit * 100))
                {
                    dt = oGetRequiredSupportApprovers(iRoleId, iShellShare, StoredProcedure.GetAppUsersByIpLimitRangeOne());
                }
            }
            else if (xIPLimit.m_iSeq == 2)
            {
                if ((iShellShare <= (xIPLimit.m_iIPLimit * 100)) && (iShellShare > (iLevel1 * 100)))
                {
                    dt = oGetRequiredSupportApprovers2(iRoleId, iShellShare, StoredProcedure.getAppUsersByIPLimitRangeTwoThreeFourFive(), xIPLimit.m_iSeq);
                }
            }
            else if (xIPLimit.m_iSeq == 3)
            {
                if ((iShellShare <= (xIPLimit.m_iIPLimit * 100)) && (iShellShare > (iLevel2 * 100)))
                {
                    dt = oGetRequiredSupportApprovers2(iRoleId, iShellShare, StoredProcedure.getAppUsersByIPLimitRangeTwoThreeFourFive(), xIPLimit.m_iSeq);
                }
            }
            else if (xIPLimit.m_iSeq == 4)
            {
                if ((iShellShare <= (xIPLimit.m_iIPLimit * 100)) && (iShellShare > (iLevel3 * 100)))
                {
                    dt = oGetRequiredSupportApprovers2(iRoleId, iShellShare, StoredProcedure.getAppUsersByIPLimitRangeTwoThreeFourFive(), xIPLimit.m_iSeq);
                }
            }
            else if (xIPLimit.m_iSeq == 5)
            {
                if ((iShellShare <= (xIPLimit.m_iIPLimit * 100)) && (iShellShare > (iLevel4 * 100)))
                {
                    dt = oGetRequiredSupportApprovers2(iRoleId, iShellShare, StoredProcedure.getAppUsersByIPLimitRangeTwoThreeFourFive(), xIPLimit.m_iSeq);
                }
                else if (iShellShare > (xIPLimit.m_iIPLimit * 100))
                {
                    dt = oGetRequiredSupportApprovers(iRoleId, iShellShare, StoredProcedure.GetAppUsersByIpLimitRangeOne());
                }
            }
        }
        return dt;
    }

    private DataTable oGetRequiredSupportApprovers(int iRoleId, int iShellShare, string sql)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        //DbParameter param = comm.CreateParameter();
        //param.ParameterName = ":iSHELLSHARE";
        //param.Value = iShellShare;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    private DataTable oGetRequiredSupportApprovers2(int iRoleId, int iShellShare, string sql, int iSeq)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":iSHELLSHARE";
        param.Value = iShellShare;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iSEQ";
        param.Value = iSeq;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public List<appUsers> GetRequiredSupportApprovers(Proposal oProposal, int iRoleId)
    {
        List<appUsers> result = new List<appUsers>();
        DataTable dt = DtGetRequiredSupportApprovers(iRoleId, oProposal.m_lSS);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new appUsers(dr));
        }
        return result;
    }
    public List<appUsers> lstGetSupportApproverByRole(int iRole)
    {
        List<appUsers> result = new List<appUsers>();
        DataTable dt = dtGetSupportApproverByRole(iRole);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new appUsers(dr));
        }
        return result;
    }

    public bool forwardProposalToLineTeamLead(long lProposalId, string sProjectNumber, int iLineLeadId, string sProjectTitle, int iCompanyId, int iFunctionId, appUsers eInitiator)
    {
        bool bRet = false;
        try
        {
            Proposal oProposal = objGetProposalById(lProposalId);
            SupportApprovalStatus SupportApproval = new SupportApprovalStatus();
            string sSubject = sProjectTitle;

            bRet = forwardIPtoBPO(iLineLeadId, oProposal, eInitiator); //Note: next approver here is the Line team lead
            if (bRet)
            {
                bRet = UpdateProposalStatus(lProposalId, (int)IPStatusReporter.ipStatusRpt.LTLSupport);
                if (bRet)
                {
                    //TODO:   A mail should be sent to the BOM, informing him that an IP has been registered in the system
                    //Here the BOM might not have been registered in e-IP, but should be able to get a mail.
                    //In case he could not login into eIP, he should resolve with the BPO.(January 2010 update)

                    appUserMgt oAppUserMgt = new appUserMgt();
                    sendMail oSendMail = new sendMail(eInitiator.structUserIdx);
                    List<structUserMailIdx> oMailTo = new List<structUserMailIdx>();
                    List<structUserMailIdx> cCopyMail = new List<structUserMailIdx>();

                    appUsers oLineTeamLead = oAppUserMgt.objGetUserByUserId(iLineLeadId);
                    oMailTo.Add(oLineTeamLead.structUserIdx);

                    appUsers oBusinessProcessOwner = oAppUserMgt.objGetUserByUserRoleCompany(iCompanyId, (int)appUsersRoles.userRole.Business_Process_Owner); //This is used to get BPO of the IP Initiator's OU.
                    cCopyMail.Add(oBusinessProcessOwner.structUserIdx);

                    List<appUsers> oFunctionalPlanners = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Functional_Planner); //This is used to get the functional Planner of the IP origin.
                    foreach (appUsers oFunctionalPlanner in oFunctionalPlanners)
                    {
                        if (oFunctionalPlanner.m_iFunction == iFunctionId)
                        {
                            cCopyMail.Add(oFunctionalPlanner.structUserIdx);
                        }
                    }

                    //This role(EPGIPTracker) no longer exist in SHELL
                    //bool IPLT3rdQuartile = SupportApproval.IPLessThan3rdQuartile(oProposal);
                    //if (IPLT3rdQuartile == false)
                    //{
                    //    appUsers oEPGIPTracker = oAppUserMgt.objGetUsersByRole((int)appUsersRoles.userRole.EPG_IP_Tracker);
                    //    oMailTo.Add(oEPGIPTracker.structUserIdx);
                    //}
                    bRet = oSendMail.IPInitiatorLoadedIP(oMailTo, cCopyMail, sSubject, eInitiator.m_sFullName, sProjectNumber);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }

        return bRet;
    }

    public DataTable dtGetSupportApproverByRole(int iRole)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getUserByRoleId();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRole;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = (int)appUsersRoles.userStatus.activeUser;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public string MyStand(int iStand)
    {
        string stand = "";
        if (iStand == SupportState.iSupported)
        {
            stand = SupportState.Supported;
        }
        else if (iStand == SupportState.iApproved)
        {
            stand = SupportState.Approved;
        }
        else if (iStand == SupportState.iFinanceApproval)
        {
            stand = SupportState.Approved;
        }
        else if (iStand == SupportState.iNotSupported)
        {
            stand = SupportState.NotSupported;
        }
        else if (iStand == SupportState.iNotApproved)
        {
            stand = SupportState.NotApproved;
        }
        else
        {
            stand = SupportState.sSupportApproverStandDefault;
        }

        return stand;
    }

    public void SupportStatusCode(int iStand, Label Stand)
    {
        if (iStand == SupportState.iSupported)
            Stand.ForeColor = System.Drawing.Color.Green;
        else if (iStand == SupportState.iApproved)
            Stand.ForeColor = System.Drawing.Color.Green;
        else if (iStand == SupportState.iFinanceApproval)
            Stand.ForeColor = System.Drawing.Color.Green;
        else if (iStand == SupportState.iNotSupported)
            Stand.ForeColor = System.Drawing.Color.Red;
        else
            Stand.ForeColor = System.Drawing.Color.DarkOrange;
    }

    public void SupportStatusCode(int iStand, Label Stand, CheckBox chk)
    {
        if (iStand == SupportState.iSupported)
        {
            Stand.ForeColor = System.Drawing.Color.Green;
            chk.Enabled = false;
        }
        else if (iStand == SupportState.iApproved)
        {
            Stand.ForeColor = System.Drawing.Color.Green;
            chk.Enabled = false;
        }
        else if (iStand == SupportState.iFinanceApproval)
        {
            Stand.ForeColor = System.Drawing.Color.Green;
            chk.Enabled = false;
        }
        else if (iStand == SupportState.iNotSupported)
        {
            Stand.ForeColor = System.Drawing.Color.Red;
            chk.Enabled = false;
        }
        else
        {
            Stand.ForeColor = System.Drawing.Color.DarkOrange;
            chk.Enabled = true;
        }
    }


    public void FillApproveState(DropDownList SupportStandDropDownList)
    {
        string[] listItemText = new string[2];
        string[] listItemValue = new string[2];

        listItemText[0] = SupportState.Approved; listItemValue[0] = SupportState.iApproved.ToString();
        listItemText[1] = SupportState.NotApproved; listItemValue[1] = SupportState.iNotApproved.ToString();


        for (int i = 0; i <= 1; i++)
        {
            SupportStandDropDownList.Items.Add(new ListItem(listItemText[i], listItemValue[i]));
        }
    }

    public void FillFinanceApprovalState(DropDownList SupportStandDropDownList)
    {
        string[] listItemText = new string[2];
        string[] listItemValue = new string[2];

        listItemText[0] = SupportState.Approved; listItemValue[0] = SupportState.iFinanceApproval.ToString();
        listItemText[1] = SupportState.NotApproved; listItemValue[1] = SupportState.iNotApproved.ToString();


        for (int i = 0; i <= 1; i++)
        {
            SupportStandDropDownList.Items.Add(new ListItem(listItemText[i], listItemValue[i]));
        }
    }

    public void FillSupportState(DropDownList SupportStandDropDownList)
    {
        string[] listItemText = new string[2];
        string[] listItemValue = new string[2];

        listItemText[0] = SupportState.Supported; listItemValue[0] = SupportState.iSupported.ToString();
        listItemText[1] = SupportState.NotSupported; listItemValue[1] = SupportState.iNotSupported.ToString();

        for (int i = 0; i <= 1; i++)
        {
            SupportStandDropDownList.Items.Add(new ListItem(listItemText[i], listItemValue[i]));
        }
    }


    public bool forwardIPtoBPO(int iUserId, Proposal oProposal, appUsers OnlineUser)
    {
        bool success = false;

        appUserMgt oAppUserMgt = new appUserMgt();
        appUsers oSupportApprover = oAppUserMgt.objGetUserByUserId(iUserId);
        success = AssignIPtoNextSupportApprover(iUserId, oProposal.m_lProposalId, oSupportApprover.m_iUserRoleId);

        //Send a copy of the IP to the Support/or Approver

        var oSendMail = new sendMail(OnlineUser.structUserIdx);
        success = oSendMail.ForwardIP(oProposal, OnlineUser.structUserIdx);
        
        return success;
    }

    //This method is used by the BPO to forward, assign or re-assign IP to Functional Support. It is different from Assign IP to next support/approver
    public bool forwardIPtoNextSupportApprover(int iUserId, Proposal oProposal, appUsers OnlineUser, int iOldUserId, int iRoleId)
    {
        //B4 an IP will go to a functional support or approver, Check if the same person has been forwarded the IP previously
        bool success = false;
        appUserMgt oAppUserMgt = new appUserMgt();
        sendMail oSendMail = new sendMail(OnlineUser.structUserIdx);

        SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
        //SupportApproverComments exSupportApprover = oSupportApproverCommentMgt.objGetFunctionalSupportsApproverCommentByUserId(proposal.m_lProposalId, iUserId);
        SupportApproverComments exSupportApprover = oSupportApproverCommentMgt.objGetFunctionalSupportsApproverCommentByRoleId(oProposal.m_lProposalId, iRoleId);


        appUsers oSupportApprover = new appUsers();
        
        //i.e the selected user does has not been assigned the IP. The user will not be found for the IP in the support aprovers comment table.
        if (exSupportApprover.m_iUserId == 0)
        {
            oSupportApprover = oAppUserMgt.objGetUserByUserId(iUserId);
            AssignIPtoNextSupportApprover(iUserId, oProposal.m_lProposalId, oSupportApprover.m_iUserRoleId);

            //Send a copy of the IP to the Support/or Approver
            //success = oSendMail.ForwardIP(oProposal, OnlineUser.structUserIdx);
        }
        else
        {
            oSupportApprover = oAppUserMgt.objGetUserByUserId(iUserId);
            ProposalRouter.reAssignIPtoFunctionalSupport(iUserId, oProposal.m_lProposalId, iOldUserId);
        }

        appUsers oInitiator = oAppUserMgt.objGetUserByUserId(oProposal.m_iUserId);
        success = oSendMail.IPForwardForSupportApproval(oSupportApprover.structUserIdx, oInitiator.structUserIdx, oInitiator.m_sFullName, oProposal);

        return success;
    }

    public bool AssignIPtoNextSupportApprover(int UserID, long lProposalId, int iUserRoleId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.assignProposalToNextSupportApprover();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = UserID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iUserRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_RECEIVED";
        param.Value = DateTime.Today.Date.ToShortDateString();
        param.DbType = DbType.Date;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return (result != -1);
    }


    //public bool CheckIfFunctionalSupportFoundForRole(int UserID, Proposal proposal, appUsers CurrentUser)
    //{
    //    //Before a User is Inserted to perform a role for an IP, check if someone has been previously assigned to perform same role for same IP.
    //    //If true then update the IDUSERMGT to the IDUSERMGT of the new person to take over the role on the IP.
    //    //This is very important in case someone goes on leave and another person is to take over the role.

    //    //1. Get the role of the selected Functional Support
    //    appUsers MyRole = new appUsers();
    //    MyRole = new appUsers(UserID);
    //    int RoleID = MyRole.iUSERROLESID;  //This is the role of the currently selected person

    //    //2. Now, compare this role with the Existing role for this IP and replace the UserID of the current guy with the existing guy.
    //    SupportApproverComments CheckRole = new SupportApproverComments();
    //    DataTable dt = new DataTable();
    //    dt = CheckRole.FunctionalSupports(proposal.IDPROPOSAL, RoleID.ToString()); //gets functional support for an IP.
    //    BPOComments BPO = new BPOComments();

    //    if (dt.Rows.Count == 0) //i.e Proposal has not been assigned to anyone before. 
    //    {
    //        AssignIPtoNextSupportApprover(UserID, proposal.IDPROPOSAL, RoleID.ToString());
    //    }
    //    else
    //    {
    //        //i.e Proposal has not been assigned to someone in same role. Now Reassign the IP to new person
    //        //whose UserID is in the theDropDownList object and the current person UserID is in the row returned.
    //        router.reAssignIPtoFunctionalSupport(UserID, proposal.IDPROPOSAL, Convert.ToInt32(dt.Rows[0]["IDUSERMGT"]));
    //    }

    //    //When BPO does anything on the IP, the Action performed against the IP should be recorded.
    //    //ProposalCore ActionTrail = new ProposalCore();
    //    proposal.ProposalActionTrail(proposal.IDPROPOSAL, CurrentUser);
    //}


    public bool ProposalSupportedApproved(Proposal oProposal, appUsers OnlineUser, structUserMailIdx copyBPOEmail, structUserMailIdx toIPInitiatorMail)
    {
        ProposalMgt oProposalMgt = new ProposalMgt();
        sendMail oSendMail = new sendMail(OnlineUser.structUserIdx);
        int xNo = oProposalMgt.NoAwaitingSupport(oProposal.m_lProposalId);

        return oSendMail.IPSupported(toIPInitiatorMail, copyBPOEmail, OnlineUser.m_sFullName, oProposal.m_sProj_Title, oProposal.m_sProj_Num, xNo);
    }

    public bool MailCERP(Proposal oProposal, appUsers OnlineUser)
    {
        bool bRet = false;
        sendMail oSendMail = new sendMail(OnlineUser.structUserIdx);
        SupportApprovalStatus SupportApproval = new SupportApprovalStatus();
        appUserMgt oAppUserMgt = new appUserMgt();
        appUsers oAppUsers = oAppUserMgt.objGetDefaultUsersByRole((int)appUsersRoles.userRole.CERP, DefaultRoleHolder.iDefault);
        if (oAppUsers.m_iUserId != 0)
        {
            bRet = true;
            IPLimit oIPLimits = new IPLimit();
            IPLimit.IPLevels oIPLevels = oIPLimits.Limits();
            if (oProposal.m_lSS >= oIPLevels.iValue1)
            {
               oSendMail.mailEPGIPTrackerOnIPUpdate(oAppUsers.structUserIdx, oProposal.m_sProj_Num, oProposal.m_sProj_Title, oProposal.m_lProposalId);
            }
        }
        else
        {
            bRet = false;
        }

        return bRet;
    }


    #region How to Get Pending and Approved Proposals

    public List<Proposal> lstGetMyPendingProposals(int iUserId)
    {
        List<Proposal> result = new List<Proposal>();
        DataTable dt = dtGetMyPendingProposals(iUserId);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new Proposal(dr));
        }
        return result;
    }

    public DataTable dtGetMyPendingProposal(int iUserId)
    {
        //TODO: Deactivated and Discontinued STATUS to be well organised
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.approverSupportPendingProposals();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":Discontinued";
        param.Value = IPStatus.iDiscontinued;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":Deactivated";
        param.Value = IPStatus.Deactivated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SUPPORT_BIT";
        param.Value = 0;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":notSuppSTAND";
        param.Value = SupportState.iNotSupported;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":defaultSTAND";
        param.Value = SupportState.iStandDefault;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public DataTable dtGetMyPendingProposals(appUsers OnlineUser)
    {
        //TODO:Please revisit this code, there are issues with the status data type used.
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MyPendingProposals();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = OnlineUser.m_iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = (int)IPStatusReporter.ipStatusRpt.Approved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetMyPendingProposals(int iUserId)
    {
        //TODO:Please revisit this code, there are issues with the status data type used.
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MyPendingProposals();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = (int)IPStatusReporter.ipStatusRpt.Approved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable BPOPendingProposal(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.BPOPendingProposals();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STATUS";
        param.Value = IPStatus.Activated;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND";
        param.Value = SupportState.iSupported;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = (int)IPStatusReporter.ipStatusRpt.Approved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":FADOC_STAND";
        //param.Value = SupportState.iFinanceApproval;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtMyApprovedProposals(appUsers OnlineUser)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MyApprovedProposals();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = OnlineUser.m_iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = SupportState.iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":GM";
        param.Value = (int)appUsersRoles.userRole.VP;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REVP";
        param.Value = (int)appUsersRoles.userRole.REVP;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtMyProposalActivityHistory(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MyProposalActivityHistory();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":sIdUser";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable MyApprovedProposalByYearOfApproval(int iYear, appUsers OnlineUser)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.approvedProposalByYearOfApproval();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = OnlineUser.m_iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = SupportState.iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":GM";
        param.Value = (int)appUsersRoles.userRole.VP;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":REVP";
        param.Value = (int)appUsersRoles.userRole.REVP;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":YYEAR";
        param.Value = iYear;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetProposalByProjTitleOrNumber(string ProjectTitleNum)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.eSearchProposal();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":PROJ_TITLE";
        param.Value = "%" + ProjectTitleNum + "%";
        param.DbType = DbType.String;
        param.Size = 300;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":PROJ_NUM";
        param.Value = "%" + ProjectTitleNum + "%";
        param.DbType = DbType.String;
        param.Size = 300;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

#endregion
}


public class ProposalRouter
{
    //FinanceSignatureApproval FinanceSignature = new FinanceSignatureApproval();
    SupportApprovalStatus SupportApproval = new SupportApprovalStatus();

    public ProposalRouter()
    {

    }

    #region Load all pending proposals in the role

    public static DataTable dtAllSupportApproverForThisProposal(long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getSupportApproversForThisProposal();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    //public void LoadProposalSupportApprovers(Proposal oProposal, appUsers OnlineUser, GridView IPRouterGridView)
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();





    //        if (UserRoleID == eipUserRoles.iIPInitiator)
    //        {
    //            dt = IPRoutingIPInitiator(proposalList.SelectedValue);
    //        }
    //        else if (UserRoleID == eipUserRoles.iLineTeamLead)
    //        {
    //            dt = IPRoutingLineTeamLead(proposalList.SelectedValue);
    //        }
    //        else if ((UserRoleID == eipUserRoles.iSecuritySupport) || (UserRoleID == eipUserRoles.iTreasurySupport) || (UserRoleID == eipUserRoles.iEconomicsSupport) || (UserRoleID == eipUserRoles.iTAXSupport) || (UserRoleID == eipUserRoles.iLEGALSupport) || (UserRoleID == eipUserRoles.iSPCASupport) || (UserRoleID == eipUserRoles.iControllers) || (UserRoleID == eipUserRoles.iHSESupport) || (UserRoleID == eipUserRoles.iITSupport) || (UserRoleID == eipUserRoles.iSCMSupport))
    //        {
    //            dt = IPRoutingFunctionalSupport(proposalList.SelectedValue, UserRoleID.ToString());
    //        }

    //        //GM Finance and VP Finance
    //        else if (UserRoleID == eipUserRoles.iFinanceSignature)
    //        {
    //            ds = BPO.IPRoutingFinanceSignature(proposalList.SelectedValue);
    //        }
    //        else if (UserRoleID == eipUserRoles.iFinanceSignature)
    //        {
    //            ds = BPO.IPRoutingFinanceSignature(proposalList.SelectedValue);
    //        }

    //        if (UserRoleID == eipUserRoles.iGMRegionalPlanning)
    //        {
    //            ds = BPO.IPRoutingGMREPLAN(proposalList.SelectedValue);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //}

    #endregion

    #region   IP Routing Methods

    //private DataTable IPRoutingIPInitiator(string ProposalID)
    //{
    //    string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_TITLE, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, ";
    //    sql += "TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY') AS DATE_INIT, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY') AS DATE_SUBMIT, ";
    //    sql += "EIP_USERMGT.USERROLESID, EIP_USERMGT.FULLNAME, EIP_USERMGT.IDUSERMGT, EIP_USERROLES.ROLES ";
    //    sql += "FROM EIP_USERMGT INNER JOIN ";
    //    sql += "EIP_PROPOSAL ON EIP_USERMGT.IDUSERMGT = EIP_PROPOSAL.IDUSERMGT INNER JOIN ";
    //    sql += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID ";
    //    sql += "WHERE (EIP_PROPOSAL.IDPROPOSAL = '" + ProposalID + "') ";
    //    sql += "ORDER BY EIP_PROPOSAL.PROJ_NUM";

    //    return DataAccess.ExecuteQueryCommand(sql);
    //}

    //private DataTable IPRoutingFunctionalSupport(string ProposalID, string UserRoleID)
    //{
    //    string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_TITLE, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, ";
    //    sql += "TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY') AS DATE_INIT, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY') AS DATE_SUBMIT, ";
    //    sql += "EIP_SUPPORTAPPROVERCOMMENTS.STAND, EIP_USERMGT.USERROLESID, EIP_USERMGT.FULLNAME, EIP_USERMGT.IDUSERMGT, EIP_USERROLES.ROLES ";
    //    sql += "FROM EIP_USERMGT INNER JOIN ";
    //    sql += "EIP_SUPPORTAPPROVERCOMMENTS ON EIP_USERMGT.IDUSERMGT = EIP_SUPPORTAPPROVERCOMMENTS.IDUSERMGT INNER JOIN ";
    //    sql += "EIP_PROPOSAL ON EIP_SUPPORTAPPROVERCOMMENTS.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL INNER JOIN ";
    //    sql += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID ";
    //    sql += "WHERE (EIP_PROPOSAL.IDPROPOSAL = '" + ProposalID + "') AND (EIP_USERROLES.USERROLESID = '" + UserRoleID + "') ";
    //    sql += "AND ((EIP_SUPPORTAPPROVERCOMMENTS.STAND = '" + SupportState.iStandDefault + "') OR (EIP_SUPPORTAPPROVERCOMMENTS.STAND = '" + SupportState.iNotSupported + "')) ";
    //    sql += "ORDER BY EIP_PROPOSAL.PROJ_NUM";

    //    return DataAccess.ExecuteQueryCommand(sql);
    //}

    
    
    #endregion   **** END IP Routing Methods ****

    #region Load the UserEmailDropDownList control with the email address of the their corresponding approval/support function registered in this role

    //private void LoadUserMailControl(DropDownList UserEmailDropDownList, int UserRoleID, Proposal proposal)
    //{
    //    //Clear the contents of each dropdown before refilling
    //    UserEmailDropDownList.Items.Clear();
    //    if (UserRoleID == eipUserRoles.iEconomicsSupport)
    //    {
    //        //db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iEconomicsSupport));
    //        List<SupportApprovers> EconomicsFunctionalSupport = SupportApproverLogic.GetFunctionalSupportByRole(eipUserRoles.iEconomicsSupport);
    //        foreach (SupportApprovers EconomicsFunctionalSupporters in EconomicsFunctionalSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(EconomicsFunctionalSupporters.EMAIL, EconomicsFunctionalSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iHSESupport)
    //    {
    //        //db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iHSESupport));

    //        List<SupportApprovers> HSEFunctionalSupport = SupportApproverLogic.GetFunctionalSupportByRole(eipUserRoles.iHSESupport);
    //        foreach (SupportApprovers HSEFunctionalSupporters in HSEFunctionalSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(HSEFunctionalSupporters.EMAIL, HSEFunctionalSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iSecuritySupport)
    //    {
    //        //db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iSecuritySupport));

    //        List<SupportApprovers> SecurityFunctionalSupport = SupportApproverLogic.GetFunctionalSupportByRole(eipUserRoles.iSecuritySupport);
    //        foreach (SupportApprovers SecurityFunctionalSupporters in SecurityFunctionalSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(SecurityFunctionalSupporters.EMAIL, SecurityFunctionalSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iSPCASupport)
    //    {
    //        //db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iSPCASupport));

    //        List<SupportApprovers> SPCAFunctionalSupport = SupportApproverLogic.GetFunctionalSupportByRole(eipUserRoles.iSPCASupport);
    //        foreach (SupportApprovers SPCAFunctionalSupporters in SPCAFunctionalSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(SPCAFunctionalSupporters.EMAIL, SPCAFunctionalSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iTreasurySupport)
    //    {
    //        //db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iTreasurySupport));
    //        List<SupportApprovers> TreasuryFunctionalSupport = SupportApproverLogic.GetFunctionalSupportByRole(eipUserRoles.iTreasurySupport);
    //        foreach (SupportApprovers TreasuryFunctionalSupporters in TreasuryFunctionalSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(TreasuryFunctionalSupporters.EMAIL, TreasuryFunctionalSupporters.IDUSERMGT));
    //        }
    //    }

    //    else if (UserRoleID == eipUserRoles.iLEGALSupport)
    //    {
    //        List<SupportApprovers> LegalSupport = SupportApproverLogic.GetLegalApprovers(proposal);
    //        foreach (SupportApprovers LegalSupporters in LegalSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(LegalSupporters.EMAIL, LegalSupporters.IDUSERMGT));
    //        }
    //    }

    //    else if (UserRoleID == eipUserRoles.iTAXSupport)
    //    {
    //        List<SupportApprovers> TaxSupport = SupportApproverLogic.GetTaxApprovers(proposal);
    //        foreach (SupportApprovers TaxSupporters in TaxSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(TaxSupporters.EMAIL, TaxSupporters.IDUSERMGT));
    //        }
    //    }

    //    else if (UserRoleID == eipUserRoles.iControllers)
    //    {
    //        List<SupportApprovers> ControllerSupport = SupportApproverLogic.GetControllerApprovers(proposal);
    //        foreach (SupportApprovers ControllerSupporters in ControllerSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(ControllerSupporters.EMAIL, ControllerSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iITSupport)
    //    {
    //        List<SupportApprovers> ITFunctionalSupport = SupportApproverLogic.GetFunctionalSupportByRole(eipUserRoles.iITSupport);
    //        foreach (SupportApprovers ITFunctionalSupporters in ITFunctionalSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(ITFunctionalSupporters.EMAIL, ITFunctionalSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iSCMSupport)
    //    {
    //        List<SupportApprovers> SCMFunctionalSupport = SupportApproverLogic.GetFunctionalSupportByRole(eipUserRoles.iSCMSupport);
    //        foreach (SupportApprovers SCMFunctionalSupporters in SCMFunctionalSupport)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(SCMFunctionalSupporters.EMAIL, SCMFunctionalSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iMD)
    //    {
    //        //db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iMD));
    //        List<SupportApprovers> MD = SupportApproverLogic.GetFunctionalSupportByRole(eipUserRoles.iMD);
    //        foreach (SupportApprovers MDSupporters in MD)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(MDSupporters.EMAIL, MDSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iGM)
    //    {
    //        //db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iGM));
    //        List<SupportApprovers> GM = SupportApproverLogic.GetFunctionalSupportByRole(eipUserRoles.iGM);
    //        foreach (SupportApprovers GMSupporters in GM)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(GMSupporters.EMAIL, GMSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iFinanceSignature)
    //    {
    //        //string sqlFinanceSignature = FinanceSignature.FinanceSignature(proposal);
    //        //db.FillDBLSpecial(UserEmailDropDownList, sqlFinanceSignature);

    //        //Fill Finance Signature dropdownlist
    //        List<SupportApprovers> FinanceSig = SupportApproverLogic.GetFinanceSignature(proposal);
    //        UserEmailDropDownList.Items.Add(new ListItem("None", "-1"));
    //        foreach (SupportApprovers FinanceSignatureSupporters in FinanceSig)
    //        {
    //            UserEmailDropDownList.Items.Add(new ListItem(FinanceSignatureSupporters.FULL_NAME, FinanceSignatureSupporters.IDUSERMGT));
    //        }
    //    }
    //    else if (UserRoleID == eipUserRoles.iGMRegionalPlanning)
    //    {
    //        db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iGMRegionalPlanning));
    //    }
    //    else if (UserRoleID == eipUserRoles.iVP)
    //    {
    //        string sql = "";
    //        bool IPValue = SupportApproval.IPLessThan3rdQuartile(proposal);
    //        if (IPValue == false)
    //        {
    //            sql = "SELECT USERMAIL, IDUSERMGT FROM EIP_USERMGT WHERE (USERROLESID = '" + eipUserRoles.iVP + "' OR USERROLESID = '" + eipUserRoles.iMD + "') ";
    //            sql += "AND (IDIPLIMIT = '" + IPLimit.iTHIRDQUARTILEID + "' OR IDIPLIMIT = '" + IPLimit.iUPPERQUARTILEID + "')";
    //        }
    //        else
    //        {
    //            sql = "SELECT USERMAIL, IDUSERMGT FROM EIP_USERMGT WHERE (USERROLESID = '" + eipUserRoles.iVP + "' OR USERROLESID = '" + eipUserRoles.iMD + "') ";
    //        }

    //        //Fill the ApprovalListBox with VPs or MD within  and above the LowerLimit Approval range
    //        db.FillDBLSpecial(UserEmailDropDownList, sql);
    //    }
    //    else if (UserRoleID == eipUserRoles.iREVP)
    //    {
    //        db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iREVP));
    //    }
    //    else if (UserRoleID == eipUserRoles.iCorporatePlanningManager)
    //    {
    //        db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iCorporatePlanningManager));
    //    }
    //    else if (UserRoleID == eipUserRoles.iIPInitiator)
    //    {
    //        db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iIPInitiator));
    //    }
    //    else if (UserRoleID == eipUserRoles.iLineTeamLead)
    //    {
    //        db.FillDBLSpecial(UserEmailDropDownList, db.LoadUsersByRole(eipUserRoles.iLineTeamLead));
    //    }
    //}

    #endregion

    public void CheckIPInIPInitiator(DropDownList theDropDownList, long lProposalId)
    {
        appUserMgt oInitiator = new appUserMgt();

        
        //if (thisUser.sUserId != theDropDownList.SelectedValue)
        //{
        //    string sql = "UPDATE EIP_PROPOSAL SET IDUSERMGT = '" + theDropDownList.SelectedValue + "' WHERE IDPROPOSAL = '" + ProposalID + "'";
        //    DataAccess.ExecuteNonQueryCommand(sql);
        //}
        //else if (thisUser.sUserId == theDropDownList.SelectedValue)
        //{
        //    MessageBox.Show("Selected user is the original Initiator of this proposal, please select another IP Initiator.");
        //}
    }

    
    //public void CheckIfRoleExistsInFunctionalSupport(DropDownList theDropDownList, string theUserRole, string ProposalID)
    //{
    //    //Before a User is Inserted to perform a role for an IP, check if someone has been previously assigned to perform same role for same IP.
    //    //If true then update the IDUSERMGT to the IDUSERMGT of the new person to take over the role on the IP.
    //    //This is very important in case someone goes on leave and another person is to take over the role.

    //    string sqlCheckRole = "SELECT * FROM EIP_SUPPORTAPPROVERCOMMENTS WHERE USERROLESID = '" + theUserRole + "' AND IDPROPOSAL = '" + ProposalID + "'";
    //    DataTable dtCheckRole = DataAccess.ExecuteQueryCommand(sqlCheckRole);
    //    if (dtCheckRole.Rows.Count > 0)
    //    {
    //        //if this statement is true, it means that someone exists to play that role and the person may probably be on leave.
    //        //In this case assign the role to another person's IDUSERMGT
    //        string sqlUpdate = "UPDATE EIP_SUPPORTAPPROVERCOMMENTS SET IDUSERMGT = '" + theDropDownList.SelectedValue + "' WHERE IDPROPOSAL = '" + ProposalID + "' AND USERROLESID= '" + theUserRole + "'";
    //        DataAccess.ExecuteNonQueryCommand(sqlUpdate);
    //    }
    //    else
    //    {
    //        string sqlForwardproposal = "INSERT INTO EIP_SUPPORTAPPROVERCOMMENTS (IDUSERMGT, IDPROPOSAL, USERROLESID) VALUES ('" + theDropDownList.SelectedValue + "', '" + ProposalID + "', '" + theUserRole + "')";
    //        DataAccess.ExecuteNonQueryCommand(sqlForwardproposal);
    //    }
    //}

    //------ Rerouting IP to another person--------------------------//

    public static bool reAssignIPtoFunctionalSupport(int iNewUserID, long lProposalId, int iOldUserID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.ReRouteIP();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iNewUserID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":oldIDUSERMGT";
        param.Value = iOldUserID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        // result will represent the number of changed rows
        int result = -1;
        try
        {
            result = GenericDataAccess.ExecuteNonQuery(comm);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        return (result != -1);
    }
}

public class eIPAuditTrail
{
    public eIPAuditTrail()
    {

    }

    public DataTable AuditTrail(string ProposalID)
    {
        string sql = "SELECT EIP_AUDITTRAIL.STAND, EIP_AUDITTRAIL.CCOMMENT, TO_CHAR(EIP_AUDITTRAIL.DDATE, 'DD-MON-YYYY') AS DDATE, EIP_AUDITTRAIL.SUPPORTFULLNAME, ";
        sql += "EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY') AS DATE_INIT, EIP_USERROLES.ROLES, ";
        sql += "TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY') AS DATE_SUBMIT, EIP_USERMGT.FULLNAME FROM EIP_AUDITTRAIL ";
        sql += "INNER JOIN EIP_PROPOSAL ON EIP_AUDITTRAIL.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql += "INNER JOIN EIP_USERROLES ON EIP_AUDITTRAIL.SUPPORT = EIP_USERROLES.USERROLESID ";
        sql += "INNER JOIN EIP_USERMGT ON EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT ";
        sql += "WHERE EIP_AUDITTRAIL.IDPROPOSAL = '" + ProposalID + "' ORDER BY SUPPORTFULLNAME";

        return DataAccess.ExecuteQueryCommand(sql);
    }
}

//public DataTable dtGetNoOfAwaitingSupport(long lProposalId)
//{
//    DbCommand comm = GenericDataAccess.CreateCommand();
//    comm.CommandText = StoredProcedure.getProposalSupportDetailsByProposalId();

//    DbParameter param = comm.CreateParameter();
//    param.ParameterName = ":IDPROPOSAL";
//    param.Value = lProposalId;
//    param.DbType = DbType.Int64;
//    comm.Parameters.Add(param);

//    return GenericDataAccess.ExecuteSelectCommand(comm);
//}

//public SupportApproverComments objGetNoOfAwaitingSupport(long lProposalId)
//{
//    DataTable dt = dtGetNoOfAwaitingSupport(lProposalId);

//    SupportApproverComments xRows = new SupportApproverComments();
//    foreach (DataRow dr in dt.Rows)
//    {
//        xRows = new SupportApproverComments(dr);
//    }
//    return xRows;
//}


//public List<appUsers> GetFinanceSignature(Proposal proposal)
//{
//    SupportApprovalStatus support = new SupportApprovalStatus();
//    //This is required to accurately select between GM Finance SPDC, SNEPCO, SHLGB and Pectel
//    int EPNIGERIALOGIC = proposal.OriginatingUnit(proposal);

//    string sql = "";
//    DataSet ds = new DataSet();

//    IPInitiator IPInit = new IPInitiator(proposal.IDUSERMGT);

//    DbCommand comm = GenericDataAccess.CreateCommand();
//    DbParameter param = comm.CreateParameter();

//    if (support.IPLTEQLowerQuartileLTEQ2ndQuartile(proposal))
//    {
//        sql = "SELECT EIP_USERMGT.USERMAIL, EIP_USERMGT.IDUSERMGT, EIP_USERMGT.FULLNAME, EIP_USERMGT.USERNAME FROM EIP_USERMGT ";
//        sql += "INNER JOIN CPDMS_FUNCTIONS ON EIP_USERMGT.FUNCTIONID = CPDMS_FUNCTIONS.FUNCTIONID ";
//        sql += "WHERE ((EIP_USERMGT.COMPANYID = :COMPANYID) AND (EIP_USERMGT.USERROLESID = :xUSERROLESID) AND (EIP_USERMGT.STATUS = :STATUS)) ";
//        sql += "OR ((EIP_USERMGT.COMPANYID = :COMPANYID) AND (EIP_USERMGT.USERROLESID = :yUSERROLESID) AND (EIP_USERMGT.STATUS = :STATUS)) ";
//        sql += "AND (EIP_USERMGT.EPNIGERIALOGIC = :EPNIGERIALOGIC) AND (CPDMS_FUNCTIONS.FUNCTION = :FUNCTION)";

//        comm.CommandText = sql;

//        param = comm.CreateParameter();
//        param.ParameterName = ":COMPANYID";
//        param.Value = IPInit.iCompanyID;
//        param.DbType = DbType.Int32;
//        comm.Parameters.Add(param);

//        param = comm.CreateParameter();
//        param.ParameterName = ":xUSERROLESID";
//        param.Value = eipUserRoles.iFinanceSignature;
//        param.DbType = DbType.Int32;
//        comm.Parameters.Add(param);

//        param = comm.CreateParameter();
//        param.ParameterName = ":yUSERROLESID";
//        param.Value = eipUserRoles.iGM;
//        param.DbType = DbType.Int32;
//        comm.Parameters.Add(param);

//        param = comm.CreateParameter();
//        param.ParameterName = ":FUNCTION";
//        param.Value = cpdmsFunctionsNames.Finance;
//        param.DbType = DbType.String;
//        param.Size = 500;
//        comm.Parameters.Add(param);

//        param = comm.CreateParameter();
//        param.ParameterName = ":EPNIGERIALOGIC";
//        param.Value = EPNIGERIALOGIC;
//        param.DbType = DbType.Int32;
//        comm.Parameters.Add(param);

//        param = comm.CreateParameter();
//        param.ParameterName = ":STATUS";
//        param.Value = IPStatus.Activated;
//        param.DbType = DbType.String;
//        param.Size = 500;
//        comm.Parameters.Add(param);
//    }
//    else if (support.IPGT2ndQuartile(proposal))
//    {
//        //IP > $10mln Corporate Planning Sends to GM Finance of OU

//        sql = "SELECT EIP_USERMGT.USERMAIL, EIP_USERMGT.IDUSERMGT, EIP_USERMGT.FULLNAME, EIP_USERMGT.USERNAME FROM EIP_USERMGT INNER JOIN CPDMS_FUNCTIONS ON EIP_USERMGT.FUNCTIONID = CPDMS_FUNCTIONS.FUNCTIONID ";
//        sql += "WHERE (EIP_USERMGT.COMPANYID = :COMPANYID) AND (EIP_USERMGT.USERROLESID = :USERROLESID) AND (EIP_USERMGT.STATUS = :STATUS) ";
//        sql += "AND (EIP_USERMGT.EPNIGERIALOGIC = :EPNIGERIALOGIC) AND (CPDMS_FUNCTIONS.FUNCTION = :FUNCTION)";

//        comm.CommandText = sql;

//        param = comm.CreateParameter();
//        param.ParameterName = ":COMPANYID";
//        param.Value = IPInit.iCompanyID;
//        param.DbType = DbType.Int32;
//        comm.Parameters.Add(param);

//        param = comm.CreateParameter();
//        param.ParameterName = ":USERROLESID";
//        param.Value = eipUserRoles.iGM;
//        param.DbType = DbType.Int32;
//        comm.Parameters.Add(param);

//        param = comm.CreateParameter();
//        param.ParameterName = ":FUNCTION";
//        param.Value = cpdmsFunctionsNames.Finance;
//        param.DbType = DbType.String;
//        param.Size = 500;
//        comm.Parameters.Add(param);

//        param = comm.CreateParameter();
//        param.ParameterName = ":EPNIGERIALOGIC";
//        param.Value = EPNIGERIALOGIC;
//        param.DbType = DbType.Int32;
//        comm.Parameters.Add(param);

//        param = comm.CreateParameter();
//        param.ParameterName = ":STATUS";
//        param.Value = IPStatus.Activated;
//        param.DbType = DbType.String;
//        param.Size = 500;
//        comm.Parameters.Add(param);
//    }

//    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
//    List<SupportApprovers> result = new List<SupportApprovers>();
//    foreach (DataRow row in dt.Rows)
//    {
//        SupportApprovers rowData = new SupportApprovers();
//        rowData.EMAIL = row["USERMAIL"].ToString();
//        rowData.FULL_NAME = row["FULLNAME"].ToString();
//        rowData.IDUSERMGT = row["IDUSERMGT"].ToString();
//        rowData.USERNAME = row["USERNAME"].ToString();
//        result.Add(rowData);
//    }
//    return result;
//}


//public static List<eipUsers> sqlLimit20()
//{
//    string sql = "SELECT USERMAIL, IDUSERMGT, FULLNAME, USERNAME FROM EIP_USERMGT WHERE (IDIPLIMIT = :THIRDQUARTILEID OR IDIPLIMIT = :UPPERQUARTILEID ) ";
//    sql += "AND (USERROLESID = :VP OR USERROLESID = :MD) AND STATUS = :STATUS";

//    DbCommand comm = GenericDataAccess.CreateCommand();
//    comm.CommandText = sql;

//    DbParameter param = comm.CreateParameter();
//    param.ParameterName = ":THIRDQUARTILEID";
//    param.Value = IPLimit.iTHIRDQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":UPPERQUARTILEID";
//    param.Value = IPLimit.iUPPERQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":VP";
//    param.Value = eipUserRoles.iVP;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":MD";
//    param.Value = eipUserRoles.iMD;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":STATUS";
//    param.Value = IPStatus.Activated;
//    param.DbType = DbType.String;
//    param.Size = 500;
//    comm.Parameters.Add(param);

//    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
//    List<SupportApprovers> result = new List<SupportApprovers>();
//    foreach (DataRow row in dt.Rows)
//    {
//        SupportApprovers rowData = new SupportApprovers();
//        rowData.EMAIL = row["USERMAIL"].ToString();
//        rowData.FULL_NAME = row["FULLNAME"].ToString();
//        rowData.IDUSERMGT = row["IDUSERMGT"].ToString();
//        rowData.USERNAME = row["USERNAME"].ToString();
//        result.Add(rowData);
//    }
//    return result;
//}

//public List<appUsers> sqlLimitLower()
//{
//    string sql = "SELECT USERMAIL, IDUSERMGT, FULLNAME, USERNAME FROM EIP_USERMGT WHERE (IDIPLIMIT = :LOWERQUARTILEID OR IDIPLIMIT = :SECONDQUARTILEID ";
//    sql += "OR IDIPLIMIT = :THIRDQUARTILEID OR IDIPLIMIT = :UPPERQUARTILEID) AND (USERROLESID = :VP OR USERROLESID = :MD) AND STATUS = :STATUS";

//    DbCommand comm = GenericDataAccess.CreateCommand();
//    comm.CommandText = sql;

//    DbParameter param = comm.CreateParameter();
//    param.ParameterName = ":LOWERQUARTILEID";
//    param.Value = IPLimit.m_iLOWERQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":SECONDQUARTILEID";
//    param.Value = IPLimit.m_iSECONDQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":THIRDQUARTILEID";
//    param.Value = IPLimit.m_iTHIRDQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":UPPERQUARTILEID";
//    param.Value = IPLimit.iUPPERQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":VP";
//    param.Value = eipUserRoles.iVP;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":MD";
//    param.Value = eipUserRoles.iMD;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":STATUS";
//    param.Value = IPStatus.Activated;
//    param.DbType = DbType.String;
//    param.Size = 500;
//    comm.Parameters.Add(param);

//    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
//    List<appUsers> result = new List<appUsers>();
//    foreach (DataRow row in dt.Rows)
//    {
//        appUsers rowData = new appUsers();
//        rowData.EMAIL = row["USERMAIL"].ToString();
//        rowData.FULL_NAME = row["FULLNAME"].ToString();
//        rowData.IDUSERMGT = row["IDUSERMGT"].ToString();
//        rowData.USERNAME = row["USERNAME"].ToString();
//        result.Add(rowData);
//    }

//    return result;
//}

//public List<appUsers> sqlSecondQuartileLimit()
//{
//    string sql = "SELECT USERMAIL, IDUSERMGT, FULLNAME, USERNAME FROM EIP_USERMGT WHERE (IDIPLIMIT = :SECONDQUARTILEID OR IDIPLIMIT = :THIRDQUARTILEID ";
//    sql += "OR IDIPLIMIT = :UPPERQUARTILEID) AND (USERROLESID = :VP OR USERROLESID = :MD) AND STATUS = :STATUS";

//    DbCommand comm = GenericDataAccess.CreateCommand();
//    comm.CommandText = sql;

//    DbParameter param = comm.CreateParameter();
//    param.ParameterName = ":SECONDQUARTILEID";
//    param.Value = IPLimit.m_iSECONDQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":THIRDQUARTILEID";
//    param.Value = IPLimit.m_iTHIRDQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":UPPERQUARTILEID";
//    param.Value = IPLimit.m_iUPPERQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":VP";
//    param.Value = eipUserRoles.iVP;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":MD";
//    param.Value = eipUserRoles.iMD;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":STATUS";
//    param.Value = IPStatus.Activated;
//    param.DbType = DbType.String;
//    param.Size = 500;
//    comm.Parameters.Add(param);

//    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
//    List<appUsers> result = new List<appUsers>();
//    foreach (DataRow row in dt.Rows)
//    {
//        appUsers rowData = new appUsers();
//        rowData.EMAIL = row["USERMAIL"].ToString();
//        rowData.FULL_NAME = row["FULLNAME"].ToString();
//        rowData.IDUSERMGT = row["IDUSERMGT"].ToString();
//        rowData.USERNAME = row["USERNAME"].ToString();
//        result.Add(rowData);
//    }

//    return result;
//}

//public List<appUsers> sqlThirdQuartileLimit()
//{
//    string sql = "SELECT USERMAIL, IDUSERMGT, FULLNAME, USERNAME FROM EIP_USERMGT WHERE (IDIPLIMIT = :THIRDQUARTILEID OR IDIPLIMIT = :UPPERQUARTILEID) ";
//    sql += "AND (USERROLESID = :VP OR USERROLESID = :MD OR USERROLESID = :REVP) AND STATUS = :STATUS";

//    DbCommand comm = GenericDataAccess.CreateCommand();
//    comm.CommandText = sql;

//    DbParameter param = comm.CreateParameter();
//    param.ParameterName = ":THIRDQUARTILEID";
//    param.Value = IPLimit.m_iTHIRDQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":UPPERQUARTILEID";
//    param.Value = IPLimit.m_iUPPERQUARTILEID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":VP";
//    param.Value = eipUserRoles.iVP;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":MD";
//    param.Value = eipUserRoles.iMD;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":REVP";
//    param.Value = eipUserRoles.iREVP;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":STATUS";
//    param.Value = IPStatus.Activated;
//    param.DbType = DbType.String;
//    param.Size = 500;
//    comm.Parameters.Add(param);

//    DataTable dt = GenericDataAccess.ExecuteSelectCommand(comm);
//    List<appUsers> result = new List<appUsers>();
//    foreach (DataRow row in dt.Rows)
//    {
//        appUsers rowData = new appUsers();
//        rowData.EMAIL = row["USERMAIL"].ToString();
//        rowData.FULL_NAME = row["FULLNAME"].ToString();
//        rowData.IDUSERMGT = row["IDUSERMGT"].ToString();
//        rowData.USERNAME = row["USERNAME"].ToString();
//        result.Add(rowData);
//    }
//    return result;
//}

//public DataTable IPTrackingLoadProposalByProjNumber(string ProjectNumber)
//{
//    string sql = "";
//    sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_PROPOSAL.PROPOSALFILENAME, ";
//    sql += "EIP_PROPOSAL.IDUSERMGT, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, ";
//    sql += "EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED ";
//    sql += "FROM EIP_PROPOSAL WHERE EIP_PROPOSAL.PROJ_NUM = '" + ProjectNumber + "' ";
//    sql += "AND STATUS = '" + IPStatus.Activated + "' ORDER BY IDPROPOSAL";

//    return DataAccess.ExecuteQueryCommand(sql);
//}



//public DataTable IPTrackingLoadEPGProposalByProjName(string ProjectName)
//{
//    string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_PROPOSAL.PROPOSALFILENAME, ";
//    sql += "TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, ";
//    sql += "TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED FROM EIP_PROPOSAL, EIP_IPLIMIT ";
//    sql += "WHERE (EIP_PROPOSAL.SS > EIP_IPLIMIT.THIRDQUARTILE) AND UPPER(EIP_PROPOSAL.PROJ_TITLE) ";
//    sql += "LIKE '%" + ProjectName + "%' AND STATUS = '" + IPStatus.Activated + "' ORDER BY IDPROPOSAL";

//    return DataAccess.ExecuteQueryCommand(sql);
//}

//public string IPTrackingLoadEPGProposalByProjNumber(string ProjectNumber)
//{
//    string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_PROPOSAL.PROPOSALFILENAME, ";
//    sql += "TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, ";
//    sql += "TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT FROM EIP_PROPOSAL, EIP_IPLIMIT, TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED ";
//    sql += "WHERE (EIP_PROPOSAL.SS > EIP_IPLIMIT.THIRDQUARTILE) ";
//    sql += "AND EIP_PROPOSAL.PROJ_NUM = '" + ProjectNumber + "' AND STATUS = '" + IPStatus.Activated + "' ORDER BY IDPROPOSAL";

//    return sql;
//}

//public DataTable IPTrackingLoadProposalByFullName(string fullname)
//{
//    string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE,  EIP_PROPOSAL.PROPOSALFILENAME, ";
//    sql += "TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, ";
//    sql += "TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED FROM EIP_PROPOSAL ";
//    sql += "WHERE EIP_PROPOSAL.PROJ_INIT =  '" + fullname + "' AND STATUS = '" + IPStatus.Activated + "' ORDER BY EIP_PROPOSAL.PROJ_TITLE";

//    return DataAccess.ExecuteQueryCommand(sql);
//}

//public DataTable IPTrackingLoadProposalByUserID(string UserID)
//{
//    string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_PROPOSAL.PROPOSALFILENAME, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, ";
//    sql += "TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MM-YYYY')DATE_INIT, EIP_USERMGT.FULLNAME AS PROJ_INIT, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MM-YYYY')DATE_SUBMIT, ";
//    sql += "TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MM-YYYY')DATE_LAST_ACTIONED FROM EIP_PROPOSAL, EIP_USERMGT ";
//    sql += "WHERE EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT AND EIP_PROPOSAL.IDUSERMGT =  '" + UserID + "' AND EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "' ORDER BY IDPROPOSAL DESC";

//    return DataAccess.ExecuteQueryCommand(sql);
//}

//public bool ForwardProposalToNextSupportApprover(long lProposalId, int iUserId)
//{
//    DbCommand comm = GenericDataAccess.CreateCommand();
//    comm.CommandText = StoredProcedure.ForwardProposal();

//    DbParameter param = comm.CreateParameter();
//    param.ParameterName = ":IDPROPOSAL";
//    param.Value = lProposalId;
//    param.DbType = DbType.Int64;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":IDUSERMGT";
//    param.Value = iUserId;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":DATE_RECEIVED";
//    param.Value = DateTime.Today.Date.ToShortDateString();
//    param.DbType = DbType.Date;
//    comm.Parameters.Add(param);

//    int result = -1;
//    try
//    {
//        result = GenericDataAccess.ExecuteNonQuery(comm);
//    }
//    catch (Exception ex)
//    {
//        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
//    }

//    return (result != -1);
//}

//public DataTable dtGetApproverSupportDetailsByProposalUserID(int iUserId, long lProposalId)
//{
//    DbCommand comm = GenericDataAccess.CreateCommand();
//    comm.CommandText = StoredProcedure.getProposalSupportDetailsByProposalUserId();

//    DbParameter param = comm.CreateParameter();
//    param.ParameterName = ":IDUSERMGT";
//    param.Value = iUserId;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":IDPROPOSAL";
//    param.Value = iUserId;
//    param.DbType = DbType.Int64;
//    comm.Parameters.Add(param);

//    return GenericDataAccess.ExecuteSelectCommand(comm);
//}

//public appUsers objGetApproverSupportDetailsByProposalUserID(int iUserId, long lProposalId)
//{
//    DataTable dt = dtGetApproverSupportDetailsByProposalUserID(iUserId, lProposalId);
//    appUsers result = new appUsers();
//    foreach (DataRow dr in dt.Rows)
//    {
//        result = new appUsers(dr);
//    }
//    return result;
//}
