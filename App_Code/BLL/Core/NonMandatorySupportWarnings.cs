using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for NonMandatorySupportWarnings
/// </summary>
public class NonMandatorySupportWarnings
{
	public NonMandatorySupportWarnings()
	{
		
	}

    private DataTable dtGetNonMandatoryFunctionalSuppport(long lProposalId)
    {
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.getNonMandatoryFunctionalSupport();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":iStand";
        param.Value = SupportState.iStandDefault;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        //param = comm.CreateParameter();
        //param.ParameterName = ":STAND2";
        //param.Value = SupportState.iNotSupported;
        //param.DbType = DbType.Int32;
        //comm.Parameters.Add(param);

        return GenericDataAccess.ExecuteSelectCommand(comm);
    }

    public List<appUsers> GetNonMandatoryFunctionalSuppport(long lProposalId)
    {
        DataTable dt = dtGetNonMandatoryFunctionalSuppport(lProposalId);

        List<appUsers> xRows = new List<appUsers>(dt.Rows.Count);
        foreach (DataRow dr in dt.Rows)
        {
            xRows.Add(new appUsers(dr));
        }
        return xRows;
    }

    public bool BPOWarningMessage(Proposal oProposal, appUsers OnlineUser)
    {
        sendMail oSendMail = new sendMail(OnlineUser.structUserIdx);

        List<appUsers> NonMandatoryFunctionalSupport = GetNonMandatoryFunctionalSuppport(oProposal.m_lProposalId);
        //int i = NonMandatoryFunctionalSupport.Count;
        List<structUserMailIdx> sToEmail = new List<structUserMailIdx>();
        foreach (appUsers NoSupport in NonMandatoryFunctionalSupport)
        {
            sToEmail.Add(NoSupport.structUserIdx);
        }

        return oSendMail.NonMandatorySupportWarning(sToEmail, OnlineUser.structUserIdx, oProposal.m_sProj_Title, oProposal.m_sProj_Num);
    }

    public bool FinanceSignatureApprovedIPAccessLocked(long lProposalId)
    {
        //Removes the IP from non-mandatory support after the finance signatory approves.
        // and put "No comments received during SLA period." in the comments box.
        
        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = StoredProcedure.LockIP();

        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;  
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DOC_STAND";
        param.Value = SupportState.iFinanceApproval;
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


    public bool AddComment(int UserID, long lProposalId, string comment)
    {
        string sql = "UPDATE EIP_SUPPORTAPPROVERCOMMENTS SET COMMENTS = :COMMENTS, DATE_COMMENT = :DATE_COMMENT, SUPPORT_BIT = :SUPPORT_BIT ";
        sql += "WHERE (IDPROPOSAL = :IDPROPOSAL) AND (IDUSERMGT = :IDUSERMGT)";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql;

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = ":IDPROPOSAL";
        param.Value = lProposalId;
        param.DbType = DbType.Int64;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":IDUSERMGT";
        param.Value = UserID;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":COMMENTS";
        param.Value = comment;
        param.DbType = DbType.String;
        param.Size = 1000;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":SUPPORT_BIT";
        param.Value = 1; //1 means the user can not access the IP for approval any more
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = ":DATE_COMMENT";
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
}
