using System;
using System.Data;
using Microsoft.Reporting.WebForms;

public partial class Reports_rptLastAction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Default.aspx");
    }
    protected void viewButton_Click(object sender, EventArgs e)
    {
        if (daysTextBox.Text != "")
        {
            ReportGenerator("rptLastAction", "Report_ApprovedProposals", LoadProposals(daysTextBox.Text));
        }
    }

    private DataTable LoadProposals(string NumberOfDays)
    {
        string sql = "SELECT CPDMS_FUNCTIONS.FUNCTION, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_USERMGT.FULLNAME, ";
        sql += "EIP_USERMGT.USERMAIL, CPDMS_SHELLCOMPANIES.COMPANYNAME, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, ";
        sql += "EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, TO_CHAR(EIP_PROPOSAL.DATE_SUBMIT, 'DD-MON-YYYY')DATE_SUBMIT, ";
        sql += "EIP_PROPOSAL.DOC_STAND, TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED, ";
        sql += "TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DDD') AS LASTACTIONED, TO_CHAR(SYSDATE, 'DDD') AS TODAYSDATE ";
        sql += "FROM CPDMS_FUNCTIONS, EIP_PROPOSAL, EIP_USERMGT, CPDMS_SHELLCOMPANIES ";
        sql += "WHERE CPDMS_FUNCTIONS.FUNCTIONID = EIP_PROPOSAL.FUNCTIONID AND CPDMS_FUNCTIONS.FUNCTIONID = EIP_USERMGT.FUNCTIONID ";
        sql += "AND EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT AND EIP_USERMGT.COMPANYID = CPDMS_SHELLCOMPANIES.COMPANYID ";
        sql += "AND (TO_CHAR(SYSDATE, 'DDD') - TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DDD') = '" + NumberOfDays + "') ";
        sql += "AND EIP_PROPOSAL.DOC_STAND = '" + SupportState.iNotApproved + "'";

        return DataAccess.ExecuteQueryCommand(sql);
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
        ReportParameter[] oReportParams = new ReportParameter[1];
       
        oReportParams[0] = new ReportParameter("sNoOfDays", daysTextBox.Text);
        rptViewer.LocalReport.SetParameters(oReportParams);

        rptViewer.LocalReport.Refresh();
    }

//    SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DDD') AS LASTACTIONED, 
//TO_CHAR(SYSDATE, 'DDD') AS TODAYSDATE, TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DD-MON-YYYY')DATE_LAST_ACTIONED FROM EIP_PROPOSAL
//WHERE (TO_CHAR(SYSDATE, 'DDD') - TO_CHAR(EIP_PROPOSAL.DATE_LAST_ACTIONED, 'DDD') = '25')
}
