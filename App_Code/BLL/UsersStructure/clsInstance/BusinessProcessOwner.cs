using System;
using System.Data;
using System.Data.Common;
using System.Web.UI.WebControls;

public class BPOComments
{
    //db db = new db();

    ProposalRouter router = new ProposalRouter();


    //public bool AssignIPtoFunctionalSupport(int UserID, string ProposalID, string UserRoleID)
    //{
    //    string sql = "INSERT INTO EIP_SUPPORTAPPROVERCOMMENTS (IDUSERMGT, IDPROPOSAL, USERROLESID, DATE_RECEIVED) VALUES (:IDUSERMGT, :IDPROPOSAL, :USERROLESID, :DATE_RECEIVED)";
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = sql;

    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = ":IDUSERMGT";
    //    param.Value = UserID;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":USERROLESID";
    //    param.Value = UserRoleID;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":IDPROPOSAL";
    //    param.Value = ProposalID;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":DATE_RECEIVED";
    //    param.Value = DateTime.Today.Date.ToShortDateString();
    //    param.DbType = DbType.Date;
    //    comm.Parameters.Add(param);

    //    // result will represent the number of changed rows
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

    //public bool AssignIPtoFinanceApproval(DropDownList theDropDownList, string ProposalID)
    //{
    //    string sql = "INSERT INTO EIP_FINANCESIGNATURE (IDUSERMGT, IDPROPOSAL, DATE_RECEIVED) VALUES (:IDUSERMGT, :IDPROPOSAL, :DATE_RECEIVED)";
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = sql;

    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = ":IDUSERMGT";
    //    param.Value = theDropDownList.SelectedValue;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":IDPROPOSAL";
    //    param.Value = ProposalID;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":DATE_RECEIVED";
    //    param.Value = DateTime.Today.Date.ToShortDateString();
    //    param.DbType = DbType.Date;
    //    comm.Parameters.Add(param);

    //    // result will represent the number of changed rows
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

    //public bool AssignIPtoGMFinance(DropDownList theDropDownList, string ProposalID)
    //{
    //    string sql = "INSERT INTO EIP_GMFINANCE (IDUSERMGT, IDPROPOSAL, DATE_RECEIVED) VALUES (:IDUSERMGT, :IDPROPOSAL, :DATE_RECEIVED)";
    //    DbCommand comm = GenericDataAccess.CreateCommand();
    //    comm.CommandText = sql;

    //    DbParameter param = comm.CreateParameter();
    //    param.ParameterName = ":IDUSERMGT";
    //    param.Value = theDropDownList.SelectedValue;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":IDPROPOSAL";
    //    param.Value = ProposalID;
    //    param.DbType = DbType.Int32;
    //    comm.Parameters.Add(param);

    //    param = comm.CreateParameter();
    //    param.ParameterName = ":DATE_RECEIVED";
    //    param.Value = DateTime.Today.Date.ToShortDateString();
    //    param.DbType = DbType.Date;
    //    comm.Parameters.Add(param);

    //    // result will represent the number of changed rows
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

    public DataTable MyProposalHistory(string UserID)
    {
        string sql = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, ";
        sql += "EIP_USERMGT.FULLNAME AS PROJ_INIT, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, EIP_PROPOSAL.DOC_STAND, TO_CHAR(EIP_BPO.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT ";
        sql += "FROM EIP_PROPOSAL, EIP_USERMGT, EIP_BPO WHERE (EIP_USERMGT.IDUSERMGT = EIP_PROPOSAL.IDUSERMGT) ";
        sql += "AND (EIP_PROPOSAL.IDPROPOSAL = EIP_BPO.IDPROPOSAL) AND (EIP_BPO.IDUSERMGT = '" + UserID + "')";

        return DataAccess.ExecuteQueryCommand(sql);
    }

    public DataTable GetMyComment(string ProposalID, string UserID)
    {
        string sql = "SELECT EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY') AS DATE_INIT, ";
        sql += "TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY') AS DATE_SUBMIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME AS PROJ_INIT, ";
        sql += "EIP_AUDITTRAIL.STAND, EIP_AUDITTRAIL.CCOMMENT AS COMMENTS, TO_CHAR(EIP_AUDITTRAIL.DDATE, 'DD-MON-YYYY') AS DATE_COMMENT ";
        sql += "FROM  EIP_PROPOSAL INNER JOIN ";
        sql += "EIP_USERMGT ON EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT INNER JOIN ";
        sql += "EIP_AUDITTRAIL ON EIP_PROPOSAL.IDPROPOSAL = EIP_AUDITTRAIL.IDPROPOSAL INNER JOIN ";
        sql += "EIP_USERMGT EIP_USERMGT_1 ON EIP_AUDITTRAIL.IDUSERMGT = EIP_USERMGT_1.IDUSERMGT INNER JOIN ";
        sql += "EIP_BPO ON EIP_USERMGT_1.IDUSERMGT = EIP_BPO.IDUSERMGT ";
        sql += "AND EIP_USERMGT_1.IDUSERMGT = '" + UserID + "' AND EIP_PROPOSAL.IDPROPOSAL = '" + ProposalID + "'";

        return DataAccess.ExecuteQueryCommand(sql);
    }

    //public bool BPOForwadedIPToFunctionalSuppport(Proposal proposal)
    //{
    //    bool IPForwarded = false;
    //    SupportApproverComments functSupport = new SupportApproverComments();
    //    DataTable dt = functSupport.FunctionalSupports(proposal.IDPROPOSAL);
    //    if (dt.Rows.Count > 0)
    //    {
    //        IPForwarded = true;
    //    }

    //    return IPForwarded;
    //}

    //------------ Note: the set of codes here come from BPO.Remarks web form partial class.
    //------------ This is separated to have more control on the form

    //public void ForwardIPToSupportFunctions(int UserID, Proposal proposal, string IPInitiatorEmail, appUsers CurrentUser)
    //{
    //    bool success = forwardIPtoSupportFunction(UserID, proposal, IPInitiatorEmail, CurrentUser);
    //    if (success == true)
    //    {
    //        SupportApprovers SupportFunction = SupportApproverLogic.GetApproverSupportDetailsByUserID(UserID);
    //        //MessageBox.Show("Mail sent to " + SupportFunction.FULL_NAME + " Functional Support.");
    //    }
    //}

    //private bool forwardIPtoSupportFunction(int UserID, Proposal proposal, string IPInitiatorEmail, appUsers CurrentUser)
    //{
    //    //B4 an IP will go to a functional support, check if someone in the same functional support already has the IP
    //    //Check if the Functional Support has been forwarded the IP previously
    //    bool success = false;
    //    string[] ToEmail = { "" };
    //    SupportApprovers SupportFunction = SupportApproverLogic.GetApproverSupportDetailsByUserID(UserID);
    //    ToEmail[0] = SupportFunction.EMAIL;
    //    SupportApproverComments functionalSupport = new SupportApproverComments(proposal.IDPROPOSAL, UserID);
    //    bool found = functionalSupport.FunctionalSupportHasReceivedIP();
    //    if (found == true)
    //    {
    //        success = false;
    //        //MessageBox.Show(SupportFunction.FULL_NAME + " functional support had received this IP, please send a reminder. ");
    //    }
    //    else
    //    {
    //        CheckIfFunctionalSupportFoundForRole(UserID, proposal, CurrentUser);
    //        success = MyMail.BPOSendsIPForFunctionalSupport(ToEmail, CurrentUser.sUSERMAIL, proposal.PROJ_TITLE, ApplicationURL.MyAppURL(), IPInitiatorEmail, proposal.PROJ_NUM);
    //    }
    //    return success;
    //}

    //private void CheckIfFunctionalSupportFoundForRole(int UserID, Proposal proposal, appUsers CurrentUser)
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
    //        BPO.AssignIPtoFunctionalSupport(UserID, proposal.IDPROPOSAL, RoleID.ToString());
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


    //2. Finance Signature approval work flow

    //public bool forwardIPtoFinanceSignatureApprover(DropDownList theDropDownList, Label theSupport, Proposal proposal, appUsers CurrentUser, Label mssgLabel)
    //{
    //    bool success = false;
    //    string[] ToEmail = { "" };
    //    //Check if the user has been forwarded the IP previously
    //    FinanceSignatureComments finComment = new FinanceSignatureComments(proposal.IDPROPOSAL);
    //    bool found = finComment.FinanceSignatureReceivedIP();
    //    if ((found == true) && (finComment.iIDUSERMGT.ToString() == theDropDownList.SelectedValue))
    //    {
    //        mssgLabel.Text = theDropDownList.SelectedItem.Text + ". had received this IP, please send a reminder.";
    //    }
    //    else
    //    {
    //        CheckIfFinanceApproverExistsForIP(theDropDownList, proposal, CurrentUser);

    //        //Test if the dropdownlist does not contain "None" (i.e. Not Applicable) and send Mail to the Selected Finance Signature
    //        //Mail FIN (Finance Signature)
    //        if ((theDropDownList.SelectedItem.Text != "None") && (theSupport.Text != SupportState.Approved))
    //        {
    //            SupportApprovers finSignature = SupportApproverLogic.GetApproverSupportDetailsByUserID(Convert.ToInt32(theDropDownList.SelectedValue));
    //            ToEmail[0] = finSignature.EMAIL;
    //            success = MyMail.mailFinanceSignature(ToEmail, CurrentUser.sUSERMAIL, proposal.PROJ_TITLE, ApplicationURL.MyAppURL(), proposal.PROJ_INIT, proposal.PROJ_NUM);
    //        }
    //    }
    //    return success;
    //}

    //private void CheckIfFinanceApproverExistsForIP(DropDownList theDropDownList, Proposal proposal, appUsers CurrentUser)
    //{
    //    //Before a User is Inserted to perform a role for an IP, check if someone has been previously assigned to perform same role for same IP.
    //    //If true then update the IDUSERMGT to the IDUSERMGT of the new person to take over the role on the IP.
    //    //This is very important in case someone goes on leave and another person is to take over the role.

    //    BPOComments BPO = new BPOComments();
    //    DataTable dt = new DataTable();
    //    FinanceSignatureComments finSigExist = new FinanceSignatureComments();
    //    dt = finSigExist.FinanceSignatureForIP(proposal.IDPROPOSAL);
    //    if (dt.Rows.Count > 0)
    //    {
    //        router.reAssignIPtoFinanceApproval(theDropDownList, proposal.IDPROPOSAL, Convert.ToInt32(dt.Rows[0]["IDUSERMGT"]));
    //    }
    //    else
    //    {
    //        BPO.AssignIPtoFinanceApproval(theDropDownList, proposal.IDPROPOSAL);
    //    }

    //    //When BPO does anything on the IP, the Action performed against the IP should be recorded.
    //    proposal.ProposalActionTrail(proposal.IDPROPOSAL, CurrentUser);
    //}

    //public bool forwardIPtoGMFinanceApprover(DropDownList theDropDownList, Label theSupport, Proposal proposal, appUsers CurrentUser, Label mssgLabel)
    //{
    //    bool success = false;
    //    string[] ToEmail = { "" };
    //    //Check if the user has been forwarded the IP previously
    //    GMFinanceComments GMFinComment = new GMFinanceComments(proposal.IDPROPOSAL);
    //    bool found = GMFinComment.GMFinanceReceivedIP(); //FinanceSignatureReceivedIP();
    //    if ((found == true) && (GMFinComment.iIDUSERMGT.ToString() == theDropDownList.SelectedValue))
    //    {
    //        mssgLabel.Text = theDropDownList.SelectedItem.Text + ". had received this IP, please send a reminder.";
    //    }
    //    else
    //    {
    //        CheckIfGMFinanceExistsForIP(theDropDownList, proposal, CurrentUser);

    //        //Test if the dropdownlist does not contain "None" (i.e. Not Applicable) and send Mail to the Selected Finance Signature
    //        //Mail FIN (Finance Signature)
    //        if ((theDropDownList.SelectedItem.Text != "None") && (theSupport.Text != SupportState.Approved))
    //        {
    //            SupportApprovers GMfinApprover = SupportApproverLogic.GetApproverSupportDetailsByUserID(Convert.ToInt32(theDropDownList.SelectedValue));
    //            ToEmail[0] = GMfinApprover.EMAIL;
    //            success = MyMail.mailFinanceSignature(ToEmail, CurrentUser.sUSERMAIL, proposal.PROJ_TITLE, ApplicationURL.MyAppURL(), proposal.PROJ_INIT, proposal.PROJ_NUM);
    //        }
    //    }
    //    return success;
    //}

    //private void CheckIfGMFinanceExistsForIP(DropDownList theDropDownList, Proposal proposal, appUsers CurrentUser)
    //{
    //    //Before a User is Inserted to perform a role for an IP, check if someone has been previously assigned to perform same role for same IP.
    //    //If true then update the IDUSERMGT to the IDUSERMGT of the new person to take over the role on the IP.
    //    //This is very important in case someone goes on leave and another person is to take over the role.

    //    BPOComments BPO = new BPOComments();
    //    DataTable dt = new DataTable();
    //    GMFinanceComments GMExistsForIP = new GMFinanceComments();
    //    dt = GMExistsForIP.GMFinanceSignatureForIP(proposal.IDPROPOSAL);
    //    if (dt.Rows.Count > 0)
    //    {
    //        router.reAssignIPtoGMFinance(theDropDownList, proposal.IDPROPOSAL, Convert.ToInt32(dt.Rows[0]["IDUSERMGT"]));
    //    }
    //    else
    //    {
    //        BPO.AssignIPtoGMFinance(theDropDownList, proposal.IDPROPOSAL);
    //    }

    //    proposal.ProposalActionTrail(proposal.IDPROPOSAL, CurrentUser);
    //}
}