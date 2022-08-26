<%@ Outputcache Location="None"%>
<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="EditIP.aspx.cs" Inherits="EditIP" Title="Investment Proposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--       <asp:Panel ID="Panel3" runat="server" BackColor="LightSlateGray" BorderStyle="Outset" BorderWidth="2px" Style="z-index: 100; left: 0px;" Width="95%">--%>
    <table style="width: 99%">
        <tr>
            <td style="width: 510px; vertical-align: top">
                <table style="width: 500px" class="tMainBorder">
                    <tr>
                        <td class="cHeadTile" colspan="2">Edit Investment Proposal</td>
                    </tr>
                    <tr>
                        <td style="width: 160px">
                            <asp:Label ID="Label1" runat="server" Text="Project No:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="projNumTextBox" runat="server" ReadOnly="True" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Project Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="projTitleTextBox" runat="server" Width="300px"
                                TextMode="MultiLine" Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label109" runat="server" Text="Project Description:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="projDescTextBox" runat="server" Width="300px"
                                TextMode="MultiLine" Height="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label110" runat="server" Text="EP Priority Alignment:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="EPPriorityDropDownList" runat="server"
                                Width="300px">
                                <asp:ListItem Value="-1">[Please Select EP EPriority Alignment]</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Amount SS ($mln):"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="IPValueTextBox" runat="server" Font-Bold="True"
                                ForeColor="Navy" Width="100px">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label108" runat="server" Text="Amount JV ($mln):"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="AmountJVTextBox" runat="server" Font-Bold="True"
                                ForeColor="Navy" Width="100px">0</asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Date Initiated:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="dateInitTextBox" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td>
                            <asp:Label ID="Label106" runat="server" Text="BOM:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="BOMTextBox" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label107" runat="server" Text="Upload IP:"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="UploadProposal" runat="server" Width="187px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;</td>
                        <td>
                            <asp:Button ID="UploadFileBtn" runat="server" OnClick="UploadFileBtn_Click"
                                Text="Save" Width="100px" />&nbsp;
                                <asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click"
                                    Text="Close" ValidationGroup="xxxx" />
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
            <td style="vertical-align: top">
                <%--<table style="width: 98%" class="tMainBorder">
                    <tr>
                        <td class="cHeadTile" colspan="2">&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HyperLink ID="OpenPDFHyperLink" runat="server" NavigateUrl="~/Proposal.pdf" Target="_blank">Open PDF into New Page</asp:HyperLink>
                        </td>
                        <td>Click
                            <asp:ImageButton ID="refreshPageImageButton" runat="server" ImageUrl="~/Images/Refresh.jpg" Width="20px" />
                            &nbsp;to refresh view for updated Proposal
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 450px">
                            <iframe src="Proposal.pdf" style="width: 99%; height: 436px"></iframe>
                        </td>
                    </tr>
                </table>--%>
            </td>
        </tr>
    </table>

    <asp:ValidationSummary ID="LineTeamLeadValidationSummary" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="LineTeamLead" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Function" />

</asp:Content>
