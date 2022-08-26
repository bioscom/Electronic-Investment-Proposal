using System;
using System.Data.OracleClient;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.Common;

public static class db
{
    static db()
    {
        
    }

    #region Utilities

    public static OracleDataReader oReader(string sql)
    {
        OracleConnection cn = new OracleConnection(AppConfiguration.DbConnectionString);
        cn.Open();
        OracleCommand cmd = new OracleCommand(sql, cn);
        OracleDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static void FillDBL(DropDownList theDropDownList, string sql)
    {
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            string listItemValue = dr[1].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void FillDBL(DropDownList theDropDownList, DataTable dt)
    {
        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            string listItemValue = dr[1].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void FillDBLByValue(DropDownList theDropDownList, string listItemValue, string listItemText)
    {
        theDropDownList.Items.Clear();
        theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
    }

    public static void FillDBLByValue2(DropDownList theDropDownList, string listItemValue, string listItemText)
    {
        theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
    }

    public static void FillDBLSpecial(DropDownList theDropDownList, string sql)
    {
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        theDropDownList.Items.Add(new ListItem("None", "None"));

        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            string listItemValue = dr[1].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void FillListBox(ListBox theListBox, string sql, string ItemValue, string ItemText)
    {
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        theListBox.Items.Clear();
        theListBox.Items.Add(new ListItem(ItemValue, ItemText));

        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            string listItemValue = dr[1].ToString();
            theListBox.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void DropDownYearFiller(DropDownList theDropDownList)
    {
        string sql = "SELECT DISTINCT TO_CHAR(TO_DATE(DATE_SUBMIT, 'DD-MON-YY') ,'YYYY') DATE_SUBMIT FROM EIP_PROPOSAL ORDER BY DATE_SUBMIT";
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        theDropDownList.Items.Clear();
        theDropDownList.Items.Add("--Select Year--");
        foreach (DataRow dr in dt.Rows)
        {
            string sListItem = dr[0].ToString();
            theDropDownList.Items.Add(new ListItem(sListItem));
        }
    }

    public static void FillMonth(DropDownList theDropDownList)
    {
        int[] themonth = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        string[] MyMonth = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        string listItemText;
        string listItemValue;
        //
        for (int i = 0; i < 12; i++)
        {
            listItemText = MyMonth[i];
            listItemValue = themonth[i].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void FillMonth2(DropDownList theDropDownList)
    {
        string[] themonth = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
        string[] MyMonth = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        string listItemText;
        string listItemValue;
        //
        for (int i = 0; i < 12; i++)
        {
            listItemText = MyMonth[i];
            listItemValue = themonth[i].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void ProposalInfoFiller(DropDownList theDropDownList, string yyear)
    {
        string sql = "SELECT PROJ_NUM || ' <--> ' || PROJ_TITLE AS PROJ_INFO, IDPROPOSAL ";
        sql += "FROM EIP_PROPOSAL WHERE TO_CHAR(TO_DATE(DATE_INIT, 'DD-MON-YY') ,'YYYY') = '" + yyear + "'";

        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        string listItemText1 = "--Please Select--";
        string listItemValue1 = "-1";
        theDropDownList.Items.Add(new ListItem(listItemText1, listItemValue1));

        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            string listItemValue = dr[1].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void LoadProposalByYear(DropDownList theDropDownList, string yyear)
    {
        string sql = "SELECT PROJ_NUM, PROJ_TITLE, IDPROPOSAL FROM EIP_PROPOSAL WHERE ";
        sql += "TO_CHAR(TO_DATE(DATE_INIT, 'DD-MON-YY') ,'YYYY') = '" + yyear + "' ";
        sql += "AND ((DOC_STAND = '" + SupportState.iNotApproved + "') OR (DOC_STAND = '" + SupportState.iFinanceApproval + "')) AND STATUS = '" + IPStatus.Activated + "'";

        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        string listItemText = "[Select Proposal to be Routed]";
        string listItemValue = "-1";
        theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));

        foreach (DataRow dr in dt.Rows)
        {
            listItemText = dr[0].ToString() + " - " + dr[1].ToString();
            listItemValue = dr[2].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void LoadProposalByYear2(DropDownList theDropDownList, string yyear)
    {
        string sql = "SELECT PROJ_NUM, PROJ_TITLE, IDPROPOSAL FROM EIP_PROPOSAL WHERE ";
        sql += "TO_CHAR(TO_DATE(DATE_INIT, 'DD-MON-YY') ,'YYYY') = '" + yyear + "' ";
        sql += "AND DOC_STAND = '" + SupportState.iNotApproved + "'";

        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        string listItemText = "[Select Proposal]";
        string listItemValue = "-1";
        theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));

        foreach (DataRow dr in dt.Rows)
        {
            listItemText = dr[0].ToString() + " - " + dr[1].ToString();
            listItemValue = dr[2].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static void shellCompanies(DropDownList theDropDownList)
    {
        string sql = "SELECT COMPANYNAME, COMPANYID FROM CPDMS_SHELLCOMPANIES ORDER BY COMPANYNAME";
        FillDBL(theDropDownList, sql);
    }

    
    //public static void FillCompanies(DropDownList companyDropDownList)
    //{
    //    //Fill the Shell Companies
    //    string sql = "SELECT COMPANYNAME, COMPANYID FROM CPDMS_SHELLCOMPANIES ORDER BY COMPANYID";
    //    FillDBL(companyDropDownList, sql);
    //}

    

    public static void GetFunctions(DropDownList FUNCTIONDropDown, string functionID)
    {
        string sql = "SELECT DISTINCT FUNCTION, FUNCTIONID FROM CPDMS_FUNCTIONS WHERE FUNCTIONID='" + functionID + "'";
        FillDBL(FUNCTIONDropDown, sql);
    }

    #endregion


    #region Others.................................................


    public static string sqlFunction = "SELECT FUNCTION, FUNCTIONID FROM CPDMS_FUNCTIONS ORDER BY FUNCTION";
    public static string UserRolesSQL = "SELECT ROLES, USERROLESID FROM EIP_USERROLES ORDER BY ROLES";
    public static string ShellCompaniesSQL = "SELECT COMPANYNAME, COMPANYID FROM CPDMS_SHELLCOMPANIES ORDER BY COMPANYID";

    #endregion


    #region BOM

    public static DataTable GetBOMDetails(string FullName)
    {
        string sql = "";
        sql = "SELECT IDPROPOSAL, PROJ_NUM, PROJ_TITLE, TO_CHAR(DATE_INIT, 'DD-MON-YYYY') DATE_INIT, ";
        sql += "TO_CHAR(DATE_SUBMIT, 'DD-MON-YYYY') DATE_SUBMIT, DOC_STAND FROM EIP_PROPOSAL WHERE ";
        sql += "BOM='" + FullName + "' ORDER BY PROJ_TITLE";

        return DataAccess.ExecuteQueryCommand(sql);
    }

    //public static string GetBOMemailByFullName(string fullname)
    //{
    //    string sql = "SELECT USERMAIL FROM EIP_USERMGT WHERE FULLNAME='" + fullname + "' AND USERROLESID = '" + eipUserRoles.iBOM + "'";
    //    string BOMEmailAddress = "";
    //    DataTable dt = DataAccess.ExecuteQueryCommand(sql);

    //    if (dt.Rows.Count > 0)
    //    {
    //        BOMEmailAddress = dt.Rows[0]["USERMAIL"].ToString();
    //    }
    //    return BOMEmailAddress;
    //}

    public static void loadBOMs(DropDownList theDropDownList)
    {
        string sql = "SELECT DISTINCT BOM FROM EIP_PROPOSAL";

        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        theDropDownList.Items.Clear();
        theDropDownList.Items.Add("--Select BOM--");
        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            //string listItemValue = dr[1].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText));
        }
    }

    public static void LoadFunctionBOM(DropDownList theDropDownList)
    {
        string sql = "SELECT DISTINCT CPDMS_FUNCTIONS.FUNCTION ||'<-->'|| EIP_PROPOSAL.BOM AS BOMM, EIP_PROPOSAL.BOM FROM CPDMS_FUNCTIONS, EIP_PROPOSAL ";
        sql += "WHERE (EIP_PROPOSAL.FUNCTIONID = CPDMS_FUNCTIONS.FUNCTIONID)";

        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        theDropDownList.Items.Clear();
        theDropDownList.Items.Add(new ListItem("--Select Function/BOM--", "--Select Function/BOM--"));
        foreach (DataRow dr in dt.Rows)
        {
            string listItemText = dr[0].ToString();
            string listItemValue = dr[1].ToString();
            theDropDownList.Items.Add(new ListItem(listItemText, listItemValue));
        }
    }

    public static bool LocateBOM(string BOMFullName)
    {
        string sql = "SELECT * FROM EIP_USERMGT WHERE FULLNAME='" + BOMFullName + "'";
        bool BOMFound = false;

        DataTable dt = DataAccess.ExecuteQueryCommand(sql);

        if (dt.Rows.Count > 0)
        {
            BOMFound = true;
        }
        else
        {
            BOMFound = false;
        }
        return BOMFound;
    }

    #endregion

    #region Functional Support Planner


    #endregion

    

    //19/01/2010 New Update
    //NSACOUNTER
    
    public static string GetEPPriorityByID(string EPPriorityID)
    {
        string EPPriority = "";
        string sql = "SELECT EPPRIORITY, EPPRIORITYID FROM CPDMS_EPPRIORITY WHERE EPPRIORITYID = '" + EPPriorityID + "'";
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);
        if (dt.Rows.Count > 0)
        {
            EPPriority = dt.Rows[0]["EPPRIORITY"].ToString();
        }

        return EPPriority;
    }

    #region Proposal Work Flow Related Methods

    public static void AuditTrail(appUsers CurrentUser, int stand, string comment, string dateRev, long lProposalId)
    {
        string sql = "INSERT INTO EIP_AUDITTRAIL (IDUSERMGT, IDPROPOSAL, STAND, CCOMMENT, DDATE, SUPPORT, SUPPORTFULLNAME) ";
        sql += "VALUES ('" + CurrentUser.m_iUserId + "', '" + lProposalId + "', '" + stand + "', ";
        sql += "'" + comment + "', to_date('" + dateRev + "', 'mm/dd/yyyy'), '" + CurrentUser.m_iUserId + "', '" + CurrentUser.m_sFullName + "')";

        DataAccess.ExecuteNonQueryCommand(sql);
    }

    public static DataTable IsAllMandatoryRequiredSupportSelected(long lProposalId)
    {
        string sql = "SELECT * FROM EIP_USERROLES, EIP_SUPPORTAPPROVERCOMMENTS WHERE (EIP_USERROLES.USERROLESID = EIP_SUPPORTAPPROVERCOMMENTS.USERROLESID) ";
        sql += "AND EIP_SUPPORTAPPROVERCOMMENTS.IDPROPOSAL = '" + lProposalId + "' AND EIP_USERROLES.MANDATORY = '" + MandatorySupport.iMandatory + "'";

        return DataAccess.ExecuteQueryCommand(sql);
    }

    public static DataTable MandatorySupportFunctions()
    {
        string sql = "SELECT EIP_USERROLES.USERROLESID, EIP_USERROLES.ROLES, EIP_USERROLES.MANDATORY ";
        sql += "FROM EIP_USERROLES WHERE EIP_USERROLES.MANDATORY = '" + MandatorySupport.iMandatory + "'";

        return DataAccess.ExecuteQueryCommand(sql);
    }

    public static void UpdateDOCSTAND(string ProposalID)
    {
        string sql = "UPDATE EIP_PROPOSAL SET DOC_STAND = '" + SupportState.iApproved + "' WHERE IDPROPOSAL = '" + ProposalID + "'";
        DataAccess.ExecuteNonQueryCommand(sql);
    }

    #endregion


    #region Users Settings and other details

    public static string LoadUsersByRole(int iRole)
    {
        string sql = "SELECT USERMAIL, IDUSERMGT FROM EIP_USERMGT WHERE USERROLESID = '" + iRole + "' AND STATUS = '" + IPStatus.Activated + "'";
        return sql;
    }

    public static DataTable GetUserAcceptedRoles(string UserName)
    {
        string sql = "SELECT USERROLESID FROM EIP_USERMGT WHERE UPPER(USERNAME) = '" + UserName.ToUpper() + "'";
        return DataAccess.ExecuteQueryCommand(sql);
    }

    public static bool GetRoleStatus(int iRole)
    {
        bool UserExistsForThisRole = false;
        string sql = "SELECT * FROM EIP_USERMGT WHERE USERROLESID = '" + iRole + "' AND STATUS = '" + IPStatus.Activated + "'";
        DataTable dt = DataAccess.ExecuteQueryCommand(sql);
        if (dt.Rows.Count > 0)
        {
            UserExistsForThisRole = true;
            HttpContext.Current.Session["rolesCurrentUser"] = dt.Rows[0]["FULLNAME"].ToString();
        }

        return UserExistsForThisRole;
    }

    public static DataTable CheckIfUserExistsByUserName(string UserName)
    {
        string sql = "SELECT * FROM EIP_USERMGT WHERE UPPER(USERNAME) = '" + UserName.ToUpper() + "'";
        return DataAccess.ExecuteQueryCommand(sql);
    }

    #endregion
    

    //public static DataTable VPSupportDetails(string ProposalID)
    //{
    //    string sql1 = "SELECT EIP_USERMGT.FULLNAME, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_PROPOSAL.JV, EIP_MD.STAND, ";
    //    sql1 += "TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, TO_CHAR(EIP_MD.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, ";
    //    sql1 += "EIP_MD.COMMENTS, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, EIP_PROPOSAL.SS ";
    //    sql1 += "FROM EIP_USERMGT INNER JOIN EIP_MD ON EIP_USERMGT.IDUSERMGT = EIP_MD.IDUSERMGT ";
    //    sql1 += "INNER JOIN EIP_PROPOSAL ON EIP_MD.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
    //    sql1 += "WHERE EIP_PROPOSAL.IDPROPOSAL = '" + ProposalID + "'";
        
    //    string sql2 = "SELECT EIP_USERMGT.FULLNAME, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_PROPOSAL.JV, EIP_VPS.STAND, ";
    //    sql2 += "TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, TO_CHAR(EIP_VPS.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, ";
    //    sql2 += "EIP_VPS.COMMENTS, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, EIP_PROPOSAL.SS ";
    //    sql2 += "FROM EIP_USERMGT INNER JOIN EIP_VPS ON EIP_USERMGT.IDUSERMGT = EIP_VPS.IDUSERMGT ";
    //    sql2 += "INNER JOIN EIP_PROPOSAL ON EIP_VPS.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
    //    sql2 += "WHERE EIP_PROPOSAL.IDPROPOSAL = '" + ProposalID + "'";
        
    //    DataTable dt1 = DataAccess.ExecuteQueryCommand(sql1);
    //    DataTable dt2 = DataAccess.ExecuteQueryCommand(sql2);

    //    dt1.Merge(dt2);
    //    return dt1;
    //}

    //public static bool IsOrgSupportFound(string ProposalID)
    //{
    //    //Note VP Finance Cannot be the Organisational Approval for IPs > $10Mln and < $20mln
    //    bool Found = false;
    //    string sql = "SELECT EIP_SUPPORTAPPROVERCOMMENTS.IDPROPOSAL, EIP_USERMGT.IDUSERMGT FROM EIP_USERMGT, CPDMS_FUNCTIONS, EIP_SUPPORTAPPROVERCOMMENTS, EIP_USERROLES ";
    //    sql += "WHERE EIP_USERMGT.FUNCTIONID = CPDMS_FUNCTIONS.FUNCTIONID AND EIP_USERMGT.IDUSERMGT = EIP_SUPPORTAPPROVERCOMMENTS.IDUSERMGT ";
    //    sql += "AND EIP_SUPPORTAPPROVERCOMMENTS.ROLEID = EIP_USERROLES.USERROLESID AND (EIP_USERROLES.ROLES = '" + eipUserRoles.VP + "') ";
    //    sql += "AND (CPDMS_FUNCTIONS.FUNCTION <> '" + cpdmsFunctionsNames.Finance + "') AND (EIP_SUPPORTAPPROVERCOMMENTS.IDPROPOSAL = '" + ProposalID + "')";

    //    DataTable dt = DataAccess.ExecuteQueryCommand(sql);

    //    if (dt.Rows.Count > 0)
    //    {
    //        Found = true;
    //    }
    //    return Found;
    //}


}