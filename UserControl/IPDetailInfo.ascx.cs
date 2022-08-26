using System;
using Microsoft.Security.Application;

public partial class UserControl_IPDetailInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void LoadProposalDetails(Proposal proposal)
    {
        projNumberLabel.Text = Encoder.HtmlEncode(proposal.m_sProj_Num);
        projTitleLabel.Text = Encoder.HtmlEncode(proposal.m_sProj_Title);
        initiatorLabel.Text = Encoder.HtmlEncode(proposal.m_sPROJ_INIT);
        //dateInitLabel.Text = Encoder.HtmlEncode(proposal.m_sDate_Init);
        AmountJVLabel.Text = String.Format("{0:c}", Encoder.HtmlEncode(proposal.m_lJV.ToString()));
        AmountSSLabel.Text = String.Format("{0:c}", Encoder.HtmlEncode(proposal.m_lSS.ToString()));
        dateSubmitLabel.Text = Encoder.HtmlEncode(proposal.m_sDate_Submit);
    }
}