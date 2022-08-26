using System;
using System.Web.UI;

public partial class ApprovalSupportFunction_DigitalSignature : CustomBasePage
{
    ProposalMgt oProposalMgt = new ProposalMgt();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ProposalID"] != null)
        {
            long lProposalId = long.Parse(Request.QueryString["ProposalID"].ToString());
            closeButton.Attributes.Add("onclick", "return closeWindow()");
        }
    }

    protected void digiSignImgBtn_Click(object sender, ImageClickEventArgs e)
    {
        WriteFile MyFile = new WriteFile();

        try
        {
            long lProposalId = long.Parse(Request.QueryString["ProposalID"].ToString());
            Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);
            byte[] MyData = MyFile.DownLoadProposal(oProposal.m_sProposalFileName);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + oProposal.m_sProj_Num + ".pdf");
            Response.AddHeader("Content-Length", Convert.ToString(MyData.Length));
            Response.BinaryWrite(MyData);
            Response.End();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
    }

    protected void uploadButton_Click(object sender, EventArgs e)
    {
        long lProposalId = long.Parse(Request.QueryString["ProposalID"].ToString());
        Proposal oProposal = oProposalMgt.objGetProposalById(lProposalId);

        SaveIP2FileSystem UpLoadMe = new SaveIP2FileSystem();
        string oMessage = "";
        fileProperty MyFileProperties = UpLoadMe.UploadInvestmentProposal(UploadProposal, oProposal.m_sProj_Num, ref oMessage); //return the name with which proposal pdf doc was saved
        if (MyFileProperties.sFileName != "")
        {
            bool bRet = oProposalMgt.UpdateFileName(lProposalId, MyFileProperties.sFileName);
            if (bRet) ajaxWebExtension.showJscriptAlert(Page, this, "Proposal successfully attached.");
        }
    }
}