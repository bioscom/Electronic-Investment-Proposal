using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class Reports_rptApprovedIP : System.Web.UI.Page
{
    ProposalMgt oProposalMgt = new ProposalMgt();
    appUserMgt oAppUserMgt = new appUserMgt();
    IPLimit oIPLimits = new IPLimit();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadControls();
        }
    }

    private void LoadControls()
    {
        eIPReport thisReport = new eIPReport();

        //Fill the Shell Companies
        //oProposalMgt.FillCompanies(OUList);

        //Amount Shell Share IP Limit Ranges

        //IPValueRange oIPValueRange = new IPValueRange();
        IPLimit.IPLevels oIPLevels = oIPLimits.Limits();

        string ass1_1 = "$" + oIPLevels.iValue1 + " mln"; string ass1_2 = oIPLevels.iValue1.ToString();
        string ass2_1 = "$" + oIPLevels.iValue2 + " mln"; string ass2_2 = oIPLevels.iValue2.ToString();
        string ass3_1 = "$" + oIPLevels.iValue3 + " mln"; string ass3_2 = oIPLevels.iValue3.ToString();
        string ass4_1 = "$" + oIPLevels.iValue4 + " mln"; string ass4_2 = oIPLevels.iValue4.ToString();
        string ass5_1 = "$" + oIPLevels.iValue5 + " mln"; string ass5_2 = oIPLevels.iValue5.ToString();
        string ass6_1 = "$" + 1000 + " mln"; string ass6_2 = Convert.ToString(1000);

        ass1ddl.Items.Add(new ListItem(ass1_1, ass1_2));
        ass1ddl.Items.Add(new ListItem(ass2_1, ass2_2));
        ass1ddl.Items.Add(new ListItem(ass3_1, ass3_2));
        ass1ddl.Items.Add(new ListItem(ass4_1, ass4_2));
        ass1ddl.Items.Add(new ListItem(ass5_1, ass5_2));

        ass2ddl.Items.Add(new ListItem(ass1_1, ass1_2));
        ass2ddl.Items.Add(new ListItem(ass2_1, ass2_2));
        ass2ddl.Items.Add(new ListItem(ass3_1, ass3_2));
        ass2ddl.Items.Add(new ListItem(ass4_1, ass4_2));
        ass2ddl.Items.Add(new ListItem(ass5_1, ass5_2));
        ass2ddl.Items.Add(new ListItem(ass6_1, ass6_2));

        //organisation Approvers, the GM guys and the REVP
        List<appUsers> oOrganisationalApprovers = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.VP);
        foreach (appUsers oOrganisationalApprover in oOrganisationalApprovers)
        {
            OrganisationalApproverList.Items.Add(new ListItem(oOrganisationalApprover.m_sFullName, oOrganisationalApprover.m_iUserId.ToString()));
        }

        List<appUsers> oRegionalVPs = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.REVP);
        foreach (appUsers oRegionalVP in oRegionalVPs)
        {
            OrganisationalApproverList.Items.Add(new ListItem(oRegionalVP.m_sFullName, oRegionalVP.m_iUserId.ToString()));
        }

        //Finance Guys
        List<appUsers> oFinancialApprovers = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.VP);
        foreach (appUsers oFinancialApprover in oFinancialApprovers)
        {
            if (oFinancialApprover.eFunction.m_sFunction == cpdmsFunctionsNames.Finance)
            {
                FinancialApproverDropDownList.Items.Add(new ListItem(oFinancialApprover.m_sFullName, oFinancialApprover.m_iUserId.ToString()));
            }
        }

        List<appUsers> oFinanceDirectors = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Finance_Director);
        foreach (appUsers oFinanceDirector in oFinanceDirectors)
        {
            FinancialApproverDropDownList.Items.Add(new ListItem(oFinanceDirector.m_sFullName, oFinanceDirector.m_iUserId.ToString()));
        }

        List<appUsers> oFinanceSignatures = oAppUserMgt.lstGetUsersByRole((int)appUsersRoles.userRole.Finance_Signature);
        foreach (appUsers oFinanceSignature in oFinanceSignatures)
        {
            FinancialApproverDropDownList.Items.Add(new ListItem(oFinanceSignature.m_sFullName, oFinanceSignature.m_iUserId.ToString()));
        }

        //Load Functions
        List<cFunctions> oFunctions = cFunctions.lstGetFunctions();
        foreach (cFunctions oFunction in oFunctions)
        {
            functionddl.Items.Add(new ListItem(oFunction.m_sFunction, oFunction.m_iFunctionId.ToString()));
        }
    }

    protected void OUList_SelectedIndexChanged(object sender, EventArgs e)
    {
        eIPReport thisReport = new eIPReport();
        //Finance Approvers are loaded when OUList selected index is changed.
        FinancialApproverDropDownList.Enabled = true;

        //Load All Finance Approvers
        thisReport.FinanceApprovers(OUList.SelectedValue, FinancialApproverDropDownList);
    }

    private DataTable QueryWizard()
    {
        string sql = BaseQuery();

        if (functionddl.SelectedValue != "-1")
        {
            sql += "AND CPDMS_FUNCTIONS.FUNCTIONID = '" + functionddl.SelectedValue + "' ";
        }
        if ((fromDateControl.DateSelectedDate.Year != 0) && (toDateControl.DateSelectedDate.Year != 0))
        {
            sql += "AND EIP_PROPOSAL.DATE_LAST_ACTIONED BETWEEN TO_DATE('" + fromDateControl.DateSelectedDate + "', 'DD/MM/YYYY') AND TO_DATE('" + toDateControl.DateSelectedDate + "', 'DD/MM/YYYY') ";
        }
        if ((ass1ddl.SelectedValue != "-1") && (ass2ddl.SelectedValue != "-1"))
        {
            sql += "AND EIP_PROPOSAL.SS BETWEEN ('" + ass1ddl.SelectedValue + "') AND ('" + ass2ddl.SelectedValue + "') ";
        }
        if (OUList.SelectedValue != "-1")
        {
            sql += "AND CPDMS_SHELLCOMPANIES.COMPANYID = '" + OUList.SelectedValue + "' ";
        }
        if (FinancialApproverDropDownList.SelectedValue != "-1")
        {
            sql += "AND (EIP_SUPPORTAPPROVERCOMMENTS.STAND = '" + SupportState.iFinanceApproval + "') ";
            sql += "AND EIP_USERMGT.IDUSERMGT = '" + OrganisationalApproverList.SelectedValue + "' ";
        }
        if (OrganisationalApproverList.SelectedValue != "-1")
        {
            sql += "AND (EIP_SUPPORTAPPROVERCOMMENTS.STAND = '" + SupportState.iApproved + "') ";
            sql += "AND EIP_USERMGT.IDUSERMGT = '" + OrganisationalApproverList.SelectedValue + "' ";
        }

        return DataAccess.ExecuteQueryCommand(sql);
    }

    private string BaseQuery()
    {
        string sql = "SELECT CPDMS_FUNCTIONS.FUNCTION, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_USERMGT.FULLNAME, EIP_USERMGT.USERMAIL, ";
        sql += "CPDMS_SHELLCOMPANIES.COMPANYNAME, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY') AS DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, ";
        sql += "TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY') AS DATE_SUBMIT, VPS.IDUSERMGT, VPS.FULLNAME AS APPROVEDBY, EIP_PROPOSAL.DOC_STAND, ";
        sql += "TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY') AS DATE_LAST_ACTIONED FROM CPDMS_FUNCTIONS ";
        sql += "INNER JOIN EIP_USERMGT ON CPDMS_FUNCTIONS.FUNCTIONID = EIP_USERMGT.FUNCTIONID ";
        sql += "INNER JOIN EIP_PROPOSAL ON EIP_USERMGT.IDUSERMGT = EIP_PROPOSAL.IDUSERMGT ";
        sql += "INNER JOIN CPDMS_SHELLCOMPANIES ON EIP_USERMGT.COMPANYID = CPDMS_SHELLCOMPANIES.COMPANYID ";
        sql += "INNER JOIN EIP_SUPPORTAPPROVERCOMMENTS ON EIP_PROPOSAL.IDPROPOSAL = EIP_SUPPORTAPPROVERCOMMENTS.IDPROPOSAL ";
        sql += "INNER JOIN EIP_USERMGT VPS ON EIP_SUPPORTAPPROVERCOMMENTS.IDUSERMGT = VPS.IDUSERMGT ";
        sql += "WHERE (EIP_PROPOSAL.DOC_STAND = '" + SupportState.iApproved + "') ";

        return sql;
    }

    protected void viewButton_Click(object sender, EventArgs e)
    {
        ReportGenerator("rptApprovedIP", "Report_ApprovedProposals", QueryWizard());
    }

    void myDrillthroughEventHandler(object sender, DrillthroughEventArgs e)
    {
        LocalReport localReport = (LocalReport)e.Report;
    }

    private void ReportGenerator(string ReportFileName, string rptDataSource, DataTable rptDataTable)
    {
        rptViewer.LocalReport.DataSources.Clear();
        rptViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/rptRDLC/" + ReportFileName + ".rdlc");

        // Add the handler for drillthrough.
        rptViewer.Drillthrough += new DrillthroughEventHandler(myDrillthroughEventHandler);

        // Supply a DataTable corresponding to each report data source.
        rptViewer.LocalReport.DataSources.Add(new ReportDataSource(rptDataSource, rptDataTable));

        //Generate Parameters to pass to the reposrt
        ReportParameter[] oReportParams = new ReportParameter[3];
        string IPFunction = functionddl.SelectedItem.Text;
        if (IPFunction == "")
        {
            IPFunction = "All";
        }
        oReportParams[0] = new ReportParameter("sFunction", IPFunction);
        oReportParams[1] = new ReportParameter("dateFrom", fromDateControl.DateSelectedDate.ToLongDateString());
        oReportParams[2] = new ReportParameter("dateTo", toDateControl.DateSelectedDate.ToLongDateString());
        rptViewer.LocalReport.SetParameters(oReportParams);

        rptViewer.LocalReport.Refresh();
    }
    
    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Default.aspx");
    }
}