using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;

public partial class Reports_rptDiscontinuedIPs : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Load Functions
           // db.FillFunctions(functionddl);
        }
    }

    protected void closeButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reports/Default.aspx");
    }

    protected void viewButton_Click(object sender, EventArgs e)
    {
        ReportGenerator("rptRejectedDiscontinuedProposals", "Report_Proposals", LoadDiscontinuedProposals());
    }

    private DataTable LoadDiscontinuedProposals()
    {
        string sql = BaseQuery();

        if (functionddl.SelectedValue != "")
        {
            sql += "AND CPDMS_FUNCTIONS.FUNCTIONID = '" + functionddl.SelectedValue + "' ";
        }

        return DataAccess.ExecuteQueryCommand(sql);
    }

    private string BaseQuery()
    {
        string sql = "SELECT EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, EIP_PROPOSAL.DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, ";
        sql += "EIP_PROPOSAL.DATE_SUBMIT, EIP_PROPOSAL.DOC_STAND, EIP_PROPOSAL.STATUS, EIP_PROPOSAL.PROJ_DESC, CPDMS_EPPRIORITY.EPPRIORITY, ";
        sql += "EIP_PROPOSAL.DATE_LAST_ACTIONED, EIP_USERMGT.FULLNAME, EIP_USERMGT.IDUSERMGT, CPDMS_SHELLCOMPANIES.COMPANYNAME, ";
        sql += "CPDMS_FUNCTIONS.FUNCTION, EIP_USERMGT.FUNCTIONID, EIP_USERMGT.COMPANYID ";
        sql += "FROM EIP_PROPOSAL, EIP_IPINITIATOR, EIP_USERMGT, CPDMS_FUNCTIONS, CPDMS_SHELLCOMPANIES, CPDMS_EPPRIORITY ";
        sql += "WHERE EIP_PROPOSAL.IDPROPOSAL = EIP_IPINITIATOR.IDPROPOSAL AND EIP_PROPOSAL.IDUSERMGT = EIP_USERMGT.IDUSERMGT AND ";
        sql += "EIP_IPINITIATOR.IDUSERMGT = EIP_USERMGT.IDUSERMGT AND EIP_USERMGT.FUNCTIONID = CPDMS_FUNCTIONS.FUNCTIONID AND ";
        sql += "EIP_USERMGT.COMPANYID = CPDMS_SHELLCOMPANIES.COMPANYID AND EIP_PROPOSAL.EPPRIORITYID = CPDMS_EPPRIORITY.EPPRIORITYID AND ";
        sql += "EIP_PROPOSAL.DISCONTINUE = '" + 1 + "' ";

        return sql;
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
        string IPFunction = functionddl.SelectedItem.Text;
        if (IPFunction == "")
        {
            IPFunction = "All";
        }
        oReportParams[0] = new ReportParameter("sFunction", IPFunction);
        rptViewer.LocalReport.SetParameters(oReportParams);

        rptViewer.LocalReport.Refresh();
    }  
}




 //protected void viewButton_Click(object sender, EventArgs e)
 //   {
 //       ReportGenerator("rptApprovedIPByFunction", "Report_ApprovedProposals", QueryWizard());
 //   }

 //   void myDrillthroughEventHandler(object sender, DrillthroughEventArgs e)
 //   {
 //       LocalReport localReport = (LocalReport)e.Report;
 //   }

 //   private void ReportGenerator(string ReportFileName, string rptDataSource, DataTable rptDataTable)
 //   {
 //       rptViewer.LocalReport.DataSources.Clear();
 //       rptViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/rptRDLC/" + ReportFileName + ".rdlc");

 //       // Add the handler for drillthrough.
 //       rptViewer.Drillthrough += new DrillthroughEventHandler(myDrillthroughEventHandler);

 //       // Supply a DataTable corresponding to each report data source.
 //       rptViewer.LocalReport.DataSources.Add(new ReportDataSource(rptDataSource, rptDataTable));

 //       //Generate Parameters to pass to the reposrt
 //       ReportParameter[] oReportParams = new ReportParameter[3];
 //       string IPFunction = functionddl.SelectedItem.Text;
 //       if (IPFunction == "")
 //       {
 //           IPFunction = "All";
 //       }
 //       oReportParams[0] = new ReportParameter("sFunction", IPFunction);
 //       oReportParams[1] = new ReportParameter("dateFrom", fromDate.Value);
 //       oReportParams[2] = new ReportParameter("dateTo", toDate.Value);
 //       rptViewer.LocalReport.SetParameters(oReportParams);

 //       rptViewer.LocalReport.Refresh();
 //   }