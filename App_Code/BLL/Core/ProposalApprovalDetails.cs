using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for ProposalApprovalDetails
/// </summary>

public class ProposalApprovalDetails
{
    public int m_iCommentId { get; set; }
    public long m_iIDPROPOSAL { get; set; }
    public string m_sComments { get; set; }
    public string m_sDateComment { get; set; }
    public string m_sDateReceived { get; set; }
    public int m_iStand { get; set; }
    public int m_iUserId { get; set; }
    public int m_iUserRoleId { get; set; }
    public int m_iSupportBit { get; set; }

    public ProposalApprovalDetails()
    {

    }

    public ProposalApprovalDetails(DataRow dr)
    {
        m_iIDPROPOSAL = Convert.ToInt64(dr["IDPROPOSAL"].ToString());
        m_iUserId = Convert.ToInt32(dr["IDUSERMGT"]);
        m_sComments = dr["COMMENTS"].ToString();
        m_sDateComment = dr["DATE_COMMENT"].ToString();
        m_sDateReceived = dr["DATE_RECEIVED"].ToString();
        m_iStand = Convert.ToInt32(dr["STAND"].ToString());
        m_iUserRoleId = Convert.ToInt32(dr["USERROLESID"].ToString());
        m_iSupportBit = Convert.ToInt32(dr["SUPPORT_BIT"].ToString());
        m_iCommentId = Convert.ToInt32(dr["IDCOMMENTS"].ToString());
    }
}

public class ProposalApprovalDetailsMgt
{
    public DataTable dtGetProposalSupportDetailsByProposalId(long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getProposalSupportDetailsByProposalId();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetProposalSupportDetailsByProposalUserId(int iUserId, long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getProposalSupportDetailsByProposalUserId();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public ProposalApprovalDetails objGetProposalSupportDetailsByProposalUserId(int iUserId, long lProposalId)
    {
        DataTable dt = dtGetProposalSupportDetailsByProposalUserId(iUserId, lProposalId);

        ProposalApprovalDetails xRows = new ProposalApprovalDetails();
        foreach (DataRow dr in dt.Rows)
        {
            xRows = new ProposalApprovalDetails(dr);
        }
        return xRows;
    }

    //public ProposalApprovalDetails objGetProposalSupportDetailsByProposalId(long lProposalId)
    //{
    //    DataTable dt = dtGetProposalSupportDetailsByProposalId(lProposalId);

    //    ProposalApprovalDetails xRows = new ProposalApprovalDetails();
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        xRows = new ProposalApprovalDetails(dr);
    //    }
    //    return xRows;
    //}


    public List<ProposalApprovalDetails> lstGetProposalSupportDetailsByProposalId(long lProposalId)
    {
        DataTable dt = dtGetProposalSupportDetailsByProposalId(lProposalId);

        List<ProposalApprovalDetails> xRows = new List<ProposalApprovalDetails>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new ProposalApprovalDetails(dr));
        }
        return xRows;
    }
}

public class SupportApproverComments
{
    public long m_lProposalId { get; set; }
    public string m_sComments { get; set; }
    public int m_iUserId { get; set; }
    public string m_sDateComment { get; set; }
    public string m_sDateReceived { get; set; }
    public int m_iStand { get; set; }
    public int m_iUserRoleId { get; set; }

    public SupportApproverComments()
    {

    }

    public SupportApproverComments(DataRow dr)
    {
        m_lProposalId = long.Parse(dr["IDPROPOSAL"].ToString());
        m_sComments = dr["COMMENTS"].ToString();
        m_iUserId = Convert.ToInt32(dr["IDUSERMGT"].ToString());
        m_sDateComment = dr["DATE_COMMENT"].ToString();
        m_sDateReceived = dr["DATE_RECEIVED"].ToString();
        m_iStand = Convert.ToInt32(dr["STAND"]);
        m_iUserRoleId = Convert.ToInt32(dr["USERROLESID"]);
    }
}

public class SupportApproverCommentMgt
{
    ProposalMgt oProposalMgt = new ProposalMgt();

    public SupportApproverCommentMgt()
    {

    }

    public DataTable dtGetFunctionalSupportApproverCommentByUserId(long lProposalID, int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getFunctionalSupportApproverCommentByUserId();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalID;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetFunctionalSupportApproverCommentByRoleId(long lProposalID, int iRoleId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getFunctionalSupportApproverCommentByRoleId();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalID;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":USERROLESID";
        param.Value = iRoleId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public SupportApproverComments objGetFunctionalSupportsApproverCommentByRoleId(long lProposalID, int iRoleId)
    {
        DataTable dt = dtGetFunctionalSupportApproverCommentByRoleId(lProposalID, iRoleId);

        SupportApproverComments oResult = new SupportApproverComments();
        foreach (DataRow dr in dt.Rows)
        {
            oResult = new SupportApproverComments(dr);
        }
        return oResult;
    }

    public SupportApproverComments objGetFunctionalSupportsApproverCommentByUserId(long lProposalID, int iUserId)
    {
        DataTable dt = dtGetFunctionalSupportApproverCommentByUserId(lProposalID, iUserId);

        SupportApproverComments oResult = new SupportApproverComments();
        foreach (DataRow dr in dt.Rows)
        {
            oResult = new SupportApproverComments(dr);
        }
        return oResult;
    }

    public bool MandatoryFuntionalSupportStand(Proposal oProposal)
    {
        //TODO: When there is an addition to Mandatory Functional Support, the change will be effected here
        bool Supported = false;
        bool requiredSupportSelected = IsAllRequiredSupportSelected(oProposal);
        if (requiredSupportSelected)
        {
            SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
            List<SupportApproverComments> oSupportApproverComments = oSupportApproverCommentMgt.lstGetMandatoryFunctionalSupportsApproverComments(oProposal.m_lProposalId);

            //int iTotalFound = oSupportApproverComments.Count; int i = 0;
            //if (iTotalFound == 7) i = (iTotalFound - 2); //Remove Line Team Lead and BPO
            //if (iTotalFound == 12) i = (iTotalFound - 7); //Remove Line Team Lead and BPO, and the 5 Non Mandatory Functional Supports
            
            //int kounter = 0;

            foreach (SupportApproverComments oSupportApproverComment in oSupportApproverComments)
            {
                if (oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.Controllers)
                {
                    if (oSupportApproverComment.m_iStand == SupportState.iSupported)
                    {
                        Supported = true;
                    }
                    else
                    {
                        Supported = false; break;
                    }
                }
                else if (oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.TAX_Support)
                {
                    if (oSupportApproverComment.m_iStand == SupportState.iSupported)
                    {
                        Supported = true;
                    }
                    else
                    {
                        Supported = false; break;
                    }
                }
                else if (oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.Treasury_Support)
                {
                    if (oSupportApproverComment.m_iStand == SupportState.iSupported)
                    {
                        Supported = true;
                    }
                    else
                    {
                        Supported = false; break;
                    }
                }
                else if (oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.Economics_Support)
                {
                    if (oSupportApproverComment.m_iStand == SupportState.iSupported)
                    {
                        Supported = true;
                    }
                    else
                    {
                        Supported = false; break;
                    }
                }
                else if (oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.LEGAL_Support)
                {
                    if (oSupportApproverComment.m_iStand == SupportState.iSupported)
                    {
                        Supported = true;
                    }
                    else
                    {
                        Supported = false; break;
                    }
                }
            }

            //if (i == kounter) Supported = true;
        }
        return Supported;
    }

    private bool IsAllRequiredSupportSelected(Proposal oProposal)
    {
        //TODO: When there is an addition to Mandatory Functional Support, the change will be effected here
        //To know if all required mandatory support were selected
        bool AllRequiredSupportWereSelected = false;

        SupportApproverCommentMgt oSupportApproverCommentMgt = new SupportApproverCommentMgt();
        List<SupportApproverComments> oSupportApproverComments = oSupportApproverCommentMgt.lstGetFunctionalSupportsApproverComments(oProposal.m_lProposalId);
        foreach (SupportApproverComments oSupportApproverComment in oSupportApproverComments)
        {
            if ((oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.Controllers) 
                && (appUsersRoles.MandatoryFunctionalSupport(appUsersRoles.userRole.Controllers) == true))
                AllRequiredSupportWereSelected = true;

            else if ((oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.TAX_Support) 
                && (appUsersRoles.MandatoryFunctionalSupport(appUsersRoles.userRole.TAX_Support) == true))
                AllRequiredSupportWereSelected = true;

            else if ((oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.Treasury_Support) 
                && (appUsersRoles.MandatoryFunctionalSupport(appUsersRoles.userRole.Treasury_Support) == true))
                AllRequiredSupportWereSelected = true;

            else if ((oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.Economics_Support) 
                && (appUsersRoles.MandatoryFunctionalSupport(appUsersRoles.userRole.Economics_Support) == true))
                AllRequiredSupportWereSelected = true;

            else if ((oSupportApproverComment.m_iUserRoleId == (int)appUsersRoles.userRole.LEGAL_Support) 
                && (appUsersRoles.MandatoryFunctionalSupport(appUsersRoles.userRole.LEGAL_Support) == true))
                AllRequiredSupportWereSelected = true;
        }
        return AllRequiredSupportWereSelected;
    }

    //returns all the functional Support that exists for an IP
    public DataTable dtGetFunctionalSupportsApproverComments(long lProposalID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getFunctionalSupportApproverComments();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalID;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public DataTable dtGetMandatoryFunctionalSupportsApproverComments(long lProposalID)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getMandatoryFunctionalSupportApproverComments();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalID;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public SupportApproverComments objGetFunctionalSupportsApproverComments(long lProposalID)
    {
        DataTable dt = dtGetFunctionalSupportsApproverComments(lProposalID);

        SupportApproverComments oResult = new SupportApproverComments();
        foreach (DataRow dr in dt.Rows)
        {
            oResult = new SupportApproverComments(dr);
        }
        return oResult;
    }

    public List<SupportApproverComments> lstGetFunctionalSupportsApproverComments(long lProposalID)
    {
        DataTable dt = dtGetFunctionalSupportsApproverComments(lProposalID);
        List<SupportApproverComments> result = new List<SupportApproverComments>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new SupportApproverComments(dr));
        }

        return result;
    }

    public List<SupportApproverComments> lstGetMandatoryFunctionalSupportsApproverComments(long lProposalID)
    {
        DataTable dt = dtGetMandatoryFunctionalSupportsApproverComments(lProposalID);
        List<SupportApproverComments> result = new List<SupportApproverComments>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            result.Add(new SupportApproverComments(dr));
        }

        return result;
    } 

    public DataTable MyPendingProposal(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.ProposalsPendingMySuppportApproval();

        // create a new parameter
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
        param.ParameterName = ":DISCONTINUE";
        param.Value = IPStatus.iDiscontinued;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":DOC_STAND";
        //param.Value = SupportState.iFinanceApproval;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":SUPPORT_BIT";
        //param.Value = 0;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND";
        param.Value = SupportState.iNotSupported;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND2";
        param.Value = SupportState.iFinanceApproval;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public DataTable MyRejectedProposal(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.MyRejectedProposals();

        // create a new parameter
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
        param.ParameterName = ":DISCONTINUE";
        param.Value = IPStatus.iDiscontinued;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND";
        param.Value = SupportState.iSupported;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND2";
        param.Value = SupportState.iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = (int)IPStatusReporter.ipStatusRpt.Rejected;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //sql += "AND(EIP_SUPPORTAPPROVERCOMMENTS.STAND <> :STAND) ";
        //sql += "AND(EIP_SUPPORTAPPROVERCOMMENTS.STAND <> :STAND2) ";

        //sql += "WHERE (EIP_SUPPORTAPPROVERCOMMENTS.IDUSERMGT = :IDUSERMGT) AND (EIP_PROPOSAL.STATUS = :STATUS) ";
        //sql += "AND (EIP_PROPOSAL.DISCONTINUE <> :DISCONTINUE) ";

        return GenericDataAccess.ExecuteSelectCommand(comm);

    }

    public DataTable MyPendingProposal2(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.ProposalsPendingMySuppportApproval2();

        // create a new parameter
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
        param.ParameterName = ":DISCONTINUE";
        param.Value = IPStatus.iDiscontinued;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND";
        param.Value = SupportState.iSupported;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND2";
        param.Value = SupportState.iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = (int)IPStatusReporter.ipStatusRpt.Rejected;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);

    }


    public DataTable MyProposalHistory(int iUserId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getMyProposalHistory();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND";
        param.Value = SupportState.iSupported;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND2";
        param.Value = SupportState.iFinanceApproval;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND3";
        param.Value = SupportState.iApproved;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }


    public DataTable ServiceLevelAgreement()
    {
        string sql = "SELECT EIP_SUPPORTAPPROVERCOMMENTS.IDPROPOSAL, EIP_SUPPORTAPPROVERCOMMENTS.IDUSERMGT, EIP_SUPPORTAPPROVERCOMMENTS.STAND, EIP_SUPPORTAPPROVERCOMMENTS.DATE_RECEIVED ";
        sql += "FROM EIP_SUPPORTAPPROVERCOMMENTS INNER JOIN EIP_PROPOSAL ON EIP_SUPPORTAPPROVERCOMMENTS.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql += "WHERE (EIP_SUPPORTAPPROVERCOMMENTS.STAND = '" + SupportState.iSupportApproverStandDefault + "') AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "')";
        
        return DataAccess.ExecuteQueryCommand(sql);
    }

    public bool AddComment(appUsers OnlineUser, Proposal proposal, string comment, int iStand)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.AddComment();

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = proposal.m_lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = OnlineUser.m_iUserId;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":COMMENTS";
        param.Value = comment;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":STAND";
        param.Value = iStand;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //TODO: Note, SUPPORT_BIT field is used to close out a support for an IP by functional supports by setting the value to '1'.
        //When the BPO deems fit that a functional support needs to re-review an IP, the value is set to '0' by the BPO. 
        //This bring the IP back into the in-box of the suport person
        param = comm.CreateParameter();
        param.ParameterName = ":SUPPORT_BIT";
        if (OnlineUser.m_iUserRoleId == (int)appUsersRoles.userRole.GM_Regional_Planning)
        {
            param.Value = 0;
        }
        else
        {
            param.Value = 1;
        }
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_COMMENT";
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
}

//public DataTable GetMyComment(string ProposalID, string UserID)
//{
//    string sql = "SELECT EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY') AS DATE_INIT, ";
//    sql += "TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY') AS DATE_SUBMIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME AS PROJ_INIT, ";
//    sql += "EIP_AUDITTRAIL.STAND, EIP_AUDITTRAIL.CCOMMENT AS COMMENTS, TO_CHAR(EIP_AUDITTRAIL.DDATE, 'DD-MON-YYYY') AS DATE_COMMENT ";
//    sql += "FROM EIP_PROPOSAL INNER JOIN ";
//    sql += "EIP_USERMGT ON EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT INNER JOIN ";
//    sql += "EIP_AUDITTRAIL ON EIP_PROPOSAL.IDPROPOSAL = EIP_AUDITTRAIL.IDPROPOSAL INNER JOIN ";
//    sql += "EIP_USERMGT EIP_USERMGT_1 ON EIP_AUDITTRAIL.IDUSERMGT = EIP_USERMGT_1.IDUSERMGT INNER JOIN ";
//    sql += "EIP_SUPPORTAPPROVERCOMMENTS ON EIP_USERMGT_1.IDUSERMGT = EIP_SUPPORTAPPROVERCOMMENTS.IDUSERMGT ";
//    sql += "AND EIP_USERMGT_1.IDUSERMGT = :IDUSERMGT AND EIP_PROPOSAL.IDPROPOSAL = :IDPROPOSAL";

//    DbCommand comm = GenericDataAccess.CreateCommand();
//    comm.CommandText = sql;

//    // create a new parameter
//    DbParameter param = comm.CreateParameter();
//    param.ParameterName = ":IDUSERMGT";
//    param.Value = UserID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    param = comm.CreateParameter();
//    param.ParameterName = ":IDPROPOSAL";
//    param.Value = ProposalID;
//    param.DbType = DbType.Int32;
//    comm.Parameters.Add(param);

//    return GenericDataAccess.ExecuteSelectCommand(comm);
//}


// This method is used by Everyone Functional Support people
//public bool FunctionalSupportApprovedNotApproved(appUsers OnlineUser, Proposal proposal, int stand, string comment, appUsers oBpo, appUsers oInitiator, ref string oMessage)
//{
//    bool success = false;
//    ProposalMgt oProposalMgt = new ProposalMgt();
//    if (stand == SupportState.iNotSupported)
//    {
//        success = oProposalMgt.ProposalNotSupported(proposal, OnlineUser, comment);
//        oMessage = "Proposal Not Supported/Not Approved reasons have been sent to, Business Process Owner and the IP Initiator have been notified.";
//    }
//    else if (stand == SupportState.iSupported)
//    {
//        success = oProposalMgt.ProposalSupportedApproved(proposal, OnlineUser, oBpo.structUserIdx, oInitiator.structUserIdx);
//        oMessage = "Proposal Supported, Business Process Owner and the IP Initiator have been notified. Thank you for your Support.";
//    }
//    return success;
//}


//public bool SupportApproverAddCommentProcedure(appUsers OnlineUser, Proposal proposal, string comment, int stand, appUsers oBpo, appUsers oInitiator, ref string oMessage)
//{
//    bool success = false;
//    success = FunctionalSupportApprovedNotApproved(OnlineUser, proposal, stand, comment, oBpo, oInitiator, ref oMessage);
//    if (success == true)
//    {
//        IPTracker.MailEPGIPTracker(proposal, CurrentUser.sUSERMAIL);
//    }
//    return success;
//}