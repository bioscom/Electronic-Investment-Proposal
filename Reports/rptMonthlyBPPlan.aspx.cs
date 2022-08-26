using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class Reports_rptMonthlyBPPlan : System.Web.UI.Page
{
   

    int IPSubmitted = 0;
    string ExpectedIP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Load Functions
            //db.FillFunctions(functionddl);
            db.DropDownYearFiller(yearddl);
            db.FillMonth2(monthddl);
        }
    }

    private void ExpectedIPs()
    {
        string monthSelected = monthddl.SelectedValue;
        string sql = "SELECT IDYEARBP, DATE_SUBMIT, JAN, FEB, MAR, APR, MAY, JUN, JUL, AUG, SEP, OCT, NOV, DEC ";
        sql += "FROM EIP_IPYEARBP WHERE TO_CHAR(TO_DATE(DATE_SUBMIT, 'DD-MON-YY') ,'YYYY') = '" + yearddl.SelectedItem.Text + "'";

        DataTable dt = DataAccess.ExecuteQueryCommand(sql);
        if (dt.Rows.Count > 0)
        {
            ExpectedIP = dt.Rows[0][monthSelected].ToString();
            string message = "Expected Investment Proposal for the month of " + monthddl.SelectedItem.Text + " is " + ExpectedIP + " proposal(s). IP submitted " + IPSubmitted + "";
            mssgLabel.Text = message;
        }
    }

    private DataTable LoadProposalsByYearMonth()
    {
        string sql = "SELECT CPDMS_FUNCTIONS.FUNCTION, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_USERMGT.FULLNAME, ";
        sql += "EIP_USERMGT.USERMAIL, CPDMS_SHELLCOMPANIES.COMPANYNAME, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, ";
        sql += "EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, ";
        sql += "EIP_PROPOSAL.DOC_STAND, TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED ";
        sql += "FROM CPDMS_FUNCTIONS, EIP_PROPOSAL, EIP_USERMGT, CPDMS_SHELLCOMPANIES ";
        sql += "WHERE CPDMS_FUNCTIONS.FUNCTIONID = EIP_USERMGT.FUNCTIONID ";
        sql += "AND EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT AND EIP_USERMGT.COMPANYID = CPDMS_SHELLCOMPANIES.COMPANYID ";
        sql += "AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "') ";
        sql += "AND TO_CHAR(TO_DATE(DATE_SUBMIT, 'DD-MON-YY') ,'MON') = '" + monthddl.SelectedValue + "' ";
        sql += "AND TO_CHAR(TO_DATE(DATE_SUBMIT, 'DD-MON-YY') ,'YYYY') = '" + yearddl.SelectedItem.Text + "'";

        IPSubmitted = DataAccess.ExecuteQueryCommand(sql).Rows.Count;
        return DataAccess.ExecuteQueryCommand(sql);
    }

    private DataTable LoadProposalsByYearMonthAndFunction()
    {
        string sql = "SELECT CPDMS_FUNCTIONS.FUNCTION, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_USERMGT.FULLNAME, ";
        sql += "EIP_USERMGT.USERMAIL, CPDMS_SHELLCOMPANIES.COMPANYNAME, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, ";
        sql += "EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, ";
        sql += "EIP_PROPOSAL.DOC_STAND, TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED ";
        sql += "FROM CPDMS_FUNCTIONS, EIP_PROPOSAL, EIP_USERMGT, CPDMS_SHELLCOMPANIES ";
        sql += "WHERE CPDMS_FUNCTIONS.FUNCTIONID = EIP_USERMGT.FUNCTIONID ";
        sql += "AND EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT AND EIP_USERMGT.COMPANYID = CPDMS_SHELLCOMPANIES.COMPANYID ";
        sql += "AND CPDMS_FUNCTIONS.FUNCTIONID = '" + functionddl.SelectedValue + "' ";
        sql += "AND (EIP_PROPOSAL.STATUS = '" + IPStatus.Activated + "') ";
        sql += "AND TO_CHAR(TO_DATE(DATE_SUBMIT, 'DD-MON-YY') ,'MON') = '" + monthddl.SelectedValue + "' ";
        sql += "AND TO_CHAR(TO_DATE(DATE_SUBMIT, 'DD-MON-YY') ,'YYYY') = '" + yearddl.SelectedItem.Text + "'";

        IPSubmitted = DataAccess.ExecuteQueryCommand(sql).Rows.Count;
        return DataAccess.ExecuteQueryCommand(sql);
    }

    protected void viewButton_Click(object sender, EventArgs e)
    {
        if ((functionddl.SelectedValue == ""))
        {
            ReportGenerator("rptMonthlyBPPLan", "Report_ApprovedProposals", LoadProposalsByYearMonth());
        }
        else if ((functionddl.SelectedValue != ""))
        {
            ReportGenerator("rptMonthlyBPPLan", "Report_ApprovedProposals", LoadProposalsByYearMonthAndFunction());
        }
        ExpectedIPs();
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
        ReportParameter[] oReportParams = new ReportParameter[5];
        string IPFunction = functionddl.SelectedItem.Text;
        if (IPFunction == "")
        {
            IPFunction = "All";
        }
        oReportParams[0] = new ReportParameter("sFunction", IPFunction);
        oReportParams[1] = new ReportParameter("sYear", yearddl.SelectedValue);
        oReportParams[2] = new ReportParameter("sMonth", monthddl.SelectedItem.Text);
        oReportParams[3] = new ReportParameter("sExpectedIP", ExpectedIP);
        oReportParams[4] = new ReportParameter("sIPSubmitted", IPSubmitted.ToString());

        rptViewer.LocalReport.SetParameters(oReportParams);

        rptViewer.LocalReport.Refresh();
    }
    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Default.aspx");
    }
}
