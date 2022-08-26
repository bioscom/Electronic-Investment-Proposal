<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IPDetailInfo.ascx.cs" Inherits="UserControl_IPDetailInfo" %>

<table style="width: 98%" class="tMainBorder">
    <tr>
        <td colspan="2" class="cHeadTile">
            <asp:Label ID="Label1" runat="server" Text="Proposal Details" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 127px">
            <asp:Label ID="Label2" runat="server" Text="IP Number:" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:Label ID="projNumberLabel" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label3" runat="server" Text="Project Title:" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:Label ID="projTitleLabel" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label4" runat="server" Text="Initiator:" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <%--<a href="mailto:<%# CurrentUser.sUSERMAIL %>">--%>
            <asp:Label ID="initiatorLabel" runat="server"></asp:Label>
            <%--</a>--%>
        </td>
    </tr>
    <%--<tr>
        <td>
            <asp:Label ID="Label5" runat="server" Text="Date Initiated:" Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:Label ID="dateInitLabel" runat="server"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td>
            <asp:Label ID="Label6" runat="server" Text="Date Submitted:" Font-Bold="True"></asp:Label>
        </td>
        <td>

            <asp:Label ID="dateSubmitLabel" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label29" runat="server" Text="Amount JV ($mln):  "
                Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:Label ID="AmountJVLabel" runat="server" Font-Bold="True"
                ForeColor="#006600"></asp:Label>
        </td>
    </tr>

    <tr>
        <td>
            <asp:Label ID="Label30" runat="server" Text="Amount SS ($mln):  "
                Font-Bold="True"></asp:Label>
        </td>
        <td>
            <asp:Label ID="AmountSSLabel" runat="server" Font-Bold="True"
                ForeColor="#006600"></asp:Label>
        </td>
    </tr>
</table>

