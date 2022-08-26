<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reports_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: auto; margin-bottom: auto; margin-left: auto; margin-right: auto">
        <table style="width: 50%;" class="tMainBorder">
            <tr>
                <td class="cHeadTile" colspan="4">EIP Reports</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="approvedProposalsImgBtn" runat="server"
                        AlternateText="Approved Proposals"
                        ImageUrl="~/Images/ApprovedProposals.png"
                        PostBackUrl="~/Reports/rptApprovedIP.aspx" />
                    <br />
                    Approved Proposals</td>
                <td style="text-align: center">
                    <asp:ImageButton ID="rejectedProposalImageButton" runat="server"
                        ImageUrl="~/Images/RejectedProposal.png"
                        PostBackUrl="~/Reports/rptDiscontinuedIPs.aspx" />
                    <br />
                    Rejected Proposals</td>
                <td style="text-align: center">
                    <asp:ImageButton ID="deletedProposalImageButton" runat="server"
                        ImageUrl="~/Images/DeletedProposals.png"
                        PostBackUrl="~/Reports/rptDeletedIPs.aspx" />
                    <br />
                    Deleted Proposals</td>
                <td style="text-align: center">
                    <asp:ImageButton ID="pendingProposalsImageButton" runat="server"
                        ImageUrl="~/Images/pending.png"
                        PostBackUrl="~/Reports/rptPendingIPs.aspx" />
                    <br />
                    Pending Proposals</td>
            </tr>
            <tr>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="businessPlanRptImageButton" runat="server"
                        ImageUrl="~/Images/archive.png" PostBackUrl="~/Reports/rptMonthlyBPPlan.aspx" />
                    <br />
                    Business Plan Report</td>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButton6" runat="server"
                        ImageUrl="~/Images/lastActioned.png"
                        PostBackUrl="~/Reports/rptLastAction.aspx" />
                    <br />
                    Date of Last Action</td>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButton7" runat="server"
                        ImageUrl="~/Images/Clock.jpg"
                        PostBackUrl="~/Reports/rptSupportApproverAudit.aspx" Height="84px"
                        Width="84px" />
                    <br />
                    <span style="color: #FF0000; font-family: 'Comic Sans MS'; font-size: xx-small">New!</span>
                    Support Approval 
                    <br />
                    Slippage Audit</td>
                <td style="text-align: center">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp; &nbsp;</td>
                <td>&nbsp; &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp; &nbsp;</td>
                <td>&nbsp;
                    &nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>

