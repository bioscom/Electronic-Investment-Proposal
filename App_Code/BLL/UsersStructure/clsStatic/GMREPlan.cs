using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;


//public class GMREPlan
//{
//    private static string m_sUserID;
//    private static string m_sUserMail;

//    static GMREPlan()
//    {
//        new DataTable();
//        string sql = "SELECT IDUSERMGT, USERMAIL FROM EIP_USERMGT WHERE EIP_USERMGT.USERROLESID = '" + eipUserRoles.iGMRegionalPlanning + "' ";
//        sql += "AND STATUS = '" + IPStatus.Activated + "'";

//        DataTable dt = DataAccess.ExecuteQueryCommand(sql);
//        if (dt.Rows.Count > 0)
//        {
//            m_sUserID = dt.Rows[0]["IDUSERMGT"].ToString();
//            m_sUserMail = dt.Rows[0]["USERMAIL"].ToString();
//        }
//    }

//    public static string sUserID
//    {
//        get
//        {
//            return m_sUserID;
//        }
//    }

//    public static string sUserMail
//    {
//        get
//        {
//            return m_sUserMail;
//        }
//    }

    
//}

public class GMREPLANComments
{
    //private string m_sIDPROPOSAL;
    //private string m_sCOMMENTS;
    //private string m_sDATECOMMENT;
    //private string m_sDATERECEIVED;
    //private int m_iSTAND;
    //private int m_iIDUSERMGT;

    //public GMREPLANComments()
    //{

    //}

    //public GMREPLANComments(string ProposalID)
    //{
    //    string sql = "SELECT IDPROPOSAL, IDUSERMGT, COMMENTS, TO_CHAR(DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, STAND, TO_CHAR(DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED FROM EIP_GMREPLAN WHERE IDPROPOSAL = @IDPROPOSAL";
    //    sql = sql.Replace("@IDPROPOSAL", ProposalID);

    //    DataTable dt = DataAccess.ExecuteQueryCommand(sql); 
    //    if (dt.Rows.Count > 0)
    //    {
    //        m_sIDPROPOSAL = dt.Rows[0]["IDPROPOSAL"].ToString();
    //        m_sCOMMENTS = dt.Rows[0]["COMMENTS"].ToString();
    //        m_sDATECOMMENT = dt.Rows[0]["DATE_COMMENT"].ToString();
    //        m_sDATERECEIVED = dt.Rows[0]["DATE_RECEIVED"].ToString();
    //        m_iSTAND = Convert.ToInt32(dt.Rows[0]["STAND"]);
    //        m_iIDUSERMGT = Convert.ToInt32(dt.Rows[0]["IDUSERMGT"]);
    //    }
    //}

    //public string sIDPROPOSAL
    //{
    //    get
    //    {
    //        return m_sIDPROPOSAL;
    //    }
    //}

    //public string sCOMMENTS
    //{
    //    get
    //    {
    //        return m_sCOMMENTS;
    //    }
    //}

    //public string sDATECOMMENT
    //{
    //    get
    //    {
    //        return m_sDATECOMMENT;
    //    }
    //}

    //public string sDATERECEIVED
    //{
    //    get
    //    {
    //        return m_sDATERECEIVED;
    //    }
    //}

    //public int iSTAND
    //{
    //    get
    //    {
    //        return m_iSTAND;
    //    }
    //}

    //public int iIDUSERMGT
    //{
    //    get
    //    {
    //        return m_iIDUSERMGT;
    //    }
    //}

    //public bool GMREPlanStand()
    //{
    //    bool Supported = false;
    //    if (iSTAND == SupportState.iSupported)
    //    {
    //        Supported = true;
    //    }
    //    return Supported;
    //}

    //private static void AddComment(appUsers CurrentUser, Proposal proposal, string comment, int stand, string dateRev)
    //{
    //    string sql = "UPDATE EIP_GMREPLAN SET STAND = @STAND, COMMENTS = @COMMENTS, DATE_COMMENT = TO_DATE('" + dateRev + "', 'mm/dd/yyyy') ";
    //    sql += "WHERE (IDPROPOSAL = @IDPROPOSAL) AND (IDUSERMGT = @IDUSERMGT)";
    //    sql = sql.Replace("@STAND", "'" + stand.ToString() + "'");
    //    sql = sql.Replace("@COMMENTS", "'" + comment.Replace("'", "''") + "'");
    //    sql = sql.Replace("@IDPROPOSAL", "'" + proposal.IDPROPOSAL + "'");
    //    sql = sql.Replace("@IDUSERMGT", "'" + CurrentUser.iIDUSERMGT + "'");

    //    DataAccess.ExecuteNonQueryCommand(sql);

    //    db.AuditTrail(CurrentUser, stand, comment, dateRev, proposal.IDPROPOSAL);
    //    proposal.ProposalActionTrail(proposal.IDPROPOSAL, CurrentUser);
    //}

    //public bool GMREPlanningReceivedIP()
    //{
    //    bool IPFound = false;
    //    if (sIDPROPOSAL != null)
    //    {
    //        IPFound = true;
    //    }
    //    return IPFound;
    //}

    //public DataTable MyPendingProposal(string UserID)
    //{
    //    string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_USERMGT.FULLNAME AS PROJ_INIT, "; 
    //    sql += "TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY') AS DATE_INIT, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY') AS DATE_SUBMIT ";
    //    sql += "FROM EIP_PROPOSAL INNER JOIN ";
    //    sql += "EIP_USERMGT ON EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT INNER JOIN ";
    //    sql += "EIP_GMREPLAN ON EIP_PROPOSAL.IDPROPOSAL = EIP_GMREPLAN.IDPROPOSAL ";
    //    sql += "WHERE (EIP_GMREPLAN.IDUSERMGT = '" + UserID + "') AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "') ";
    //    sql += "AND (EIP_PROPOSAL.DISCONTINUE <> '" + IPStatus.Discontinued + "') ";
    //    sql += "AND ((EIP_GMREPLAN.STAND = '" + SupportState.iNotSupported + "') OR (EIP_GMREPLAN.STAND = '" + SupportState.iStandDefault + "'))";

    //    return DataAccess.ExecuteQueryCommand(sql);
    //}

    public DataTable MyProposalHistory(string UserID)
    {
        string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, ";
        sql += "EIP_USERMGT.FULLNAME AS PROJ_INIT, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, EIP_PROPOSAL.DOC_STAND, TO_CHAR(EIP_GMREPLAN.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT ";
        sql += "FROM EIP_PROPOSAL, EIP_USERMGT, EIP_GMREPLAN WHERE (EIP_USERMGT.IDUSERMGT = EIP_PROPOSAL.IDUSERMGT) ";
        sql += "AND (EIP_PROPOSAL.IDPROPOSAL = EIP_GMREPLAN.IDPROPOSAL) AND (EIP_GMREPLAN.IDUSERMGT = '" + UserID + "')";

        return DataAccess.ExecuteQueryCommand(sql);
    }

    public DataTable GetMyComment(string ProposalID, string UserID)
    {
        string sql = "SELECT EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY') AS DATE_INIT, ";
        sql += "TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY') AS DATE_SUBMIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME AS PROJ_INIT, ";
        sql += "EIP_AUDITTRAIL.STAND, EIP_AUDITTRAIL.CCOMMENT AS COMMENTS, TO_CHAR(EIP_AUDITTRAIL.DDATE, 'DD-MON-YYYY') AS DATE_COMMENT ";
        sql += "FROM EIP_PROPOSAL INNER JOIN ";
        sql += "EIP_USERMGT ON EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT INNER JOIN ";
        sql += "EIP_AUDITTRAIL ON EIP_PROPOSAL.IDPROPOSAL = EIP_AUDITTRAIL.IDPROPOSAL INNER JOIN ";
        sql += "EIP_USERMGT EIP_USERMGT_1 ON EIP_AUDITTRAIL.IDUSERMGT = EIP_USERMGT_1.IDUSERMGT INNER JOIN ";
        sql += "EIP_GMREPLAN ON EIP_USERMGT_1.IDUSERMGT = EIP_GMREPLAN.IDUSERMGT ";
        sql += "AND EIP_USERMGT_1.IDUSERMGT = '" + UserID + "' AND EIP_PROPOSAL.IDPROPOSAL = '" + ProposalID + "'";

        return DataAccess.ExecuteQueryCommand(sql);
    }


    //public DataTable ServiceLevelAgreement()
    //{
    //    string sql = "SELECT EIP_GMREPLAN.IDPROPOSAL, EIP_GMREPLAN.IDUSERMGT, EIP_GMREPLAN.STAND, EIP_GMREPLAN.DATE_RECEIVED ";
    //    sql += "FROM EIP_GMREPLAN INNER JOIN EIP_PROPOSAL ON EIP_GMREPLAN.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
    //    sql += "WHERE (EIP_GMREPLAN.STAND = '" + SupportState.iSupportApproverStandDefault + "') AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "')";

    //    return DataAccess.ExecuteQueryCommand(sql);
    //}

}