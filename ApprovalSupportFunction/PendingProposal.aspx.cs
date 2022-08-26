using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ApprovalSupportFunction_PendingProposal : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["rtMsg"] != null)
        {
            ajaxWebExtension.showJscriptAlert(Page, this, Request.QueryString["rtMsg"].ToString());
        }
        MyPendingProposals1.LoadMyPendingProposals(oSessnx.getOnlineUser);
        MyApprovedProposalsHistory1.LoadMyProposalsHistory(oSessnx.getOnlineUser);
        MyRejectedProposals.LoadRejectedProposals(oSessnx.getOnlineUser);
    }
}
