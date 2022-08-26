using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using Microsoft.Reporting.WebForms;
    
public partial class Reports_rptSupportApproverAudit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        auditTrailGridView.DataSource = ProposalApprovalSupportAudit();
        auditTrailGridView.DataBind();
        if (!IsPostBack)
        {
            rptViewPanel.Visible = false;
        }
    }

    private DataTable ProposalApprovalSupportAudit()
    {
        DataTable dt = new DataTable();
        
        //SupporFunctions
        string sql1 = "SELECT CPDMS.EIP_PROPOSAL.IDPROPOSAL, CPDMS.EIP_PROPOSAL.PROJ_NUM, CPDMS.EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(CPDMS.EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY') AS DATE_INIT, ";
        sql1 += "CPDMS.EIP_PROPOSAL.JV, CPDMS.EIP_PROPOSAL.SS, CPDMS.EIP_USERMGT.FULLNAME, CPDMS.EIP_USERROLES.ROLES, ";
        sql1 += "TO_CHAR(CPDMS.EIP_SUPPORTAPPROVERCOMMENTS.DATE_RECEIVED, 'DD-MON-YYYY') AS DATE_RECEIVED, ";
        sql1 += "TO_CHAR(CPDMS.EIP_SUPPORTAPPROVERCOMMENTS.DATE_COMMENT, 'DD-MON-YYYY') AS DATE_COMMENT, ";
        sql1 += "CPDMS.EIP_SUPPORTAPPROVERCOMMENTS.COMMENTS FROM CPDMS.EIP_PROPOSAL INNER JOIN ";
        sql1 += "CPDMS.EIP_SUPPORTAPPROVERCOMMENTS ON CPDMS.EIP_PROPOSAL.IDPROPOSAL = CPDMS.EIP_SUPPORTAPPROVERCOMMENTS.IDPROPOSAL ";
        sql1 += "INNER JOIN CPDMS.EIP_USERMGT ON CPDMS.EIP_SUPPORTAPPROVERCOMMENTS.IDUSERMGT = CPDMS.EIP_USERMGT.IDUSERMGT ";
        sql1 += "INNER JOIN CPDMS.EIP_USERROLES ON CPDMS.EIP_USERMGT.USERROLESID = CPDMS.EIP_USERROLES.USERROLESID ";
        sql1 += "ORDER BY CPDMS.EIP_PROPOSAL.PROJ_NUM ";

        DbCommand comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql1;

        dt = GenericDataAccess.ExecuteSelectCommand(comm);

        //BPO
        string sql2 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME, ";
        sql2 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_BPO.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_BPO.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, EIP_BPO.COMMENTS ";
        sql2 += "FROM EIP_USERMGT INNER JOIN ";
        sql2 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql2 += "EIP_BPO ON EIP_USERMGT.IDUSERMGT = EIP_BPO.IDUSERMGT INNER JOIN ";
        sql2 += "EIP_PROPOSAL ON EIP_BPO.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql2 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql2;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));

        //Line Team Lead
        string sql3 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME, ";
        sql3 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_LINETEAMLEAD.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_LINETEAMLEAD.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, EIP_LINETEAMLEAD.COMMENTS ";
        sql3 += "FROM EIP_USERMGT INNER JOIN ";
        sql3 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql3 += "EIP_LINETEAMLEAD ON EIP_USERMGT.IDUSERMGT = EIP_LINETEAMLEAD.IDUSERMGT INNER JOIN ";
        sql3 += "EIP_PROPOSAL ON EIP_LINETEAMLEAD.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql3 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql3;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));

        //Finance Signature
        string sql4 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME, ";
        sql4 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_FINANCESIGNATURE.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_FINANCESIGNATURE.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, EIP_FINANCESIGNATURE.COMMENTS ";
        sql4 += "FROM EIP_USERMGT INNER JOIN ";
        sql4 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql4 += "EIP_FINANCESIGNATURE ON EIP_USERMGT.IDUSERMGT = EIP_FINANCESIGNATURE.IDUSERMGT INNER JOIN ";
        sql4 += "EIP_PROPOSAL ON EIP_FINANCESIGNATURE.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql4 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql4;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));

        //GM Finance
        string sql5 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME, ";
        sql5 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_GMFINANCE.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_GMFINANCE.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, EIP_GMFINANCE.COMMENTS ";
        sql5 += "FROM EIP_USERMGT INNER JOIN ";
        sql5 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql5 += "EIP_GMFINANCE ON EIP_USERMGT.IDUSERMGT = EIP_GMFINANCE.IDUSERMGT INNER JOIN ";
        sql5 += "EIP_PROPOSAL ON EIP_GMFINANCE.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql5 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql5;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));


        //VPFinance
        string sql6 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME, ";
        sql6 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_VPFINANCE.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_VPFINANCE.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, EIP_VPFINANCE.COMMENTS ";
        sql6 += "FROM EIP_USERMGT INNER JOIN ";
        sql6 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql6 += "EIP_VPFINANCE ON EIP_USERMGT.IDUSERMGT = EIP_VPFINANCE.IDUSERMGT INNER JOIN ";
        sql6 += "EIP_PROPOSAL ON EIP_VPFINANCE.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql6 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql6;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));


        //Corporate Planning Manager
        string sql7 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME, ";
        sql7 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_CPMIPMGT.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_CPMIPMGT.DATEFORWARDED, 'DD-MON-YYYY')DATE_COMMENT, EIP_CPMIPMGT.COMMENTS ";
        sql7 += "FROM EIP_USERMGT INNER JOIN ";
        sql7 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql7 += "EIP_CPMIPMGT ON EIP_USERMGT.IDUSERMGT = EIP_CPMIPMGT.IDUSERMGT INNER JOIN ";
        sql7 += "EIP_PROPOSAL ON EIP_CPMIPMGT.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql7 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql7;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));


        //MD
        string sql8 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME,  ";
        sql8 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_MD.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_MD.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, EIP_MD.COMMENTS ";
        sql8 += "FROM EIP_USERMGT INNER JOIN ";
        sql8 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql8 += "EIP_MD ON EIP_USERMGT.IDUSERMGT = EIP_MD.IDUSERMGT INNER JOIN ";
        sql8 += "EIP_PROPOSAL ON EIP_MD.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql8 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql8;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));

        //GM RE Planning
        string sql9 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME,  ";
        sql9 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_GMREPLAN.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_GMREPLAN.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, EIP_GMREPLAN.COMMENTS ";
        sql9 += "FROM EIP_USERMGT INNER JOIN ";
        sql9 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql9 += "EIP_GMREPLAN ON EIP_USERMGT.IDUSERMGT = EIP_GMREPLAN.IDUSERMGT INNER JOIN ";
        sql9 += "EIP_PROPOSAL ON EIP_GMREPLAN.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql9 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql9;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));

        //        //Vice Presidents
        string sql10 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME,  ";
        sql10 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_VPS.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_VPS.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, EIP_VPS.COMMENTS ";
        sql10 += "FROM EIP_USERMGT INNER JOIN ";
        sql10 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql10 += "EIP_VPS ON EIP_USERMGT.IDUSERMGT = EIP_VPS.IDUSERMGT INNER JOIN ";
        sql10 += "EIP_PROPOSAL ON EIP_VPS.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql10 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql10;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));

        //        //REVP
        string sql11 = "SELECT EIP_PROPOSAL.IDPROPOSAL, EIP_PROPOSAL.PROJ_NUM, EIP_PROPOSAL.PROJ_TITLE, TO_CHAR(EIP_PROPOSAL.DATE_INIT, 'DD-MON-YYYY')DATE_INIT, EIP_PROPOSAL.JV, EIP_PROPOSAL.SS, EIP_USERMGT.FULLNAME,  ";
        sql11 += "EIP_USERROLES.ROLES, TO_CHAR(EIP_REVP.DATE_RECEIVED, 'DD-MON-YYYY')DATE_RECEIVED, TO_CHAR(EIP_REVP.DATE_COMMENT, 'DD-MON-YYYY')DATE_COMMENT, EIP_REVP.COMMENTS ";
        sql11 += "FROM EIP_USERMGT INNER JOIN ";
        sql11 += "EIP_USERROLES ON EIP_USERMGT.USERROLESID = EIP_USERROLES.USERROLESID INNER JOIN ";
        sql11 += "EIP_REVP ON EIP_USERMGT.IDUSERMGT = EIP_REVP.IDUSERMGT INNER JOIN ";
        sql11 += "EIP_PROPOSAL ON EIP_REVP.IDPROPOSAL = EIP_PROPOSAL.IDPROPOSAL ";
        sql11 += "ORDER BY EIP_PROPOSAL.PROJ_NUM ";

        comm = GenericDataAccess.CreateCommand();
        comm.CommandText = sql11;
        dt.Merge(GenericDataAccess.ExecuteSelectCommand(comm));
        return dt;
    }

    void myDrillthroughEventHandler(object sender, DrillthroughEventArgs e)
    {
        LocalReport localReport = (LocalReport)e.Report;
    }

    private void ReportGenerator(string ReportFileName, string rptDataSource, DataTable rptDataTable)
    {
        rptViewer.Reset();
        rptViewer.LocalReport.DataSources.Clear();
        rptViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/rptRDLC/" + ReportFileName);

        // Add the handler for drillthrough.
        rptViewer.Drillthrough += new DrillthroughEventHandler(myDrillthroughEventHandler);

        // Supply a DataTable corresponding to each report data source.
        rptViewer.LocalReport.DataSources.Add(new ReportDataSource(rptDataSource, rptDataTable));

        //Generate Parameters to pass to the reposrt
        ReportParameter[] oReportParams = new ReportParameter[1];

        string rptTitle = "Investment Proposal Support/Approval Slippage Audit";

        //if (ddlParam.SelectedValue != "-1")
        //{
        //    rptTitle = ddlParam.SelectedItem.Text;
        //}

        oReportParams[0] = new ReportParameter("Report_Title", rptTitle);
        rptViewer.LocalReport.SetParameters(oReportParams);
        rptViewer.LocalReport.Refresh();
    }

    protected void auditTrailGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ButtonClicked = e.CommandName;
        int index = Convert.ToInt32(e.CommandArgument); //Command Argument stores the index of each row
        if (ButtonClicked == "ViewStatus")
        {
            LinkButton lbViewStatus = (LinkButton)auditTrailGridView.Rows[index].FindControl("ViewStatusLinkButton");
            string ProposalID = lbViewStatus.Attributes["PROPOSALID"].ToString();
            Response.Redirect("~/Common/ViewProposalStatus.aspx" + "?Proposalid=" + ProposalID, false);
        }
    }
    
    protected void rptGridViewerButton_Click(object sender, EventArgs e)
    {
        gridViewPanel.Visible = true;
        rptViewPanel.Visible = false;
    }
    
    protected void rptViewerButton_Click(object sender, EventArgs e)
    {
        gridViewPanel.Visible = false;
        rptViewPanel.Visible = true;
        ReportGenerator("rptApprovalSupportAudit.rdlc", "Report_rptApprovalSupportAudit", ProposalApprovalSupportAudit());
    }
}
