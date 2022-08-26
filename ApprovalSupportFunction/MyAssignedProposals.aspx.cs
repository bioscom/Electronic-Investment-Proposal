using System;

public partial class ApprovalSupportFunction_MyAssignedProposals : aspnetPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MyPendingProposals1.LoadMyPendingProposals(oSessnx.getOnlineUser);
        MyApprovedProposalsHistory1.LoadMyProposalsHistory(oSessnx.getOnlineUser);
    }
}
