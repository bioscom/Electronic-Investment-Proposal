<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="YearBusinessPlan.aspx.cs" Inherits="BPO_YearBusinessPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tMainBorder" style="width: 500px; margin-top:5em">
        <tr>
            <td class="cHeadTile" colspan="4">
                IP Business Plan</td>
        </tr>
        <tr>
            <td colspan="4" style="text-align:center">
                <asp:Label ID="Label13" runat="server" Text="Number of IPs expected monthly for the year" Font-Bold="True" ForeColor="Red"></asp:Label>
                    &nbsp;
                <asp:Label ID="yearLabel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="January:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="janTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label7" runat="server" Text="July:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="julTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="February:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="febTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Text="August:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="augTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="March:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="marTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server" Text="September:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="sepTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="April:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="aprTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label10" runat="server" Text="October:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="octTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="May:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="mayTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server" Text="November:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="novTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="June:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="junTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label12" runat="server" Text="December:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="decTextBox" runat="server" Width="80px">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2" style="text-align:center">
                <asp:Button ID="submitButton" runat="server" Text="Submit" 
                    onclick="submitButton_Click" />
                <asp:Button ID="updateButton" runat="server" onclick="updateButton_Click" 
                    Text="Submit" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" style="text-align:right">
                <asp:Label ID="Total" runat="server" Font-Bold="True" ForeColor="Red">Total:</asp:Label>
            </td>
            <td>
                <asp:Label ID="totalLabel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="cHeadTile" colspan="4">
                
                Values Fixed by: [<asp:Label ID="fixedByLabel" runat="server" ForeColor="White"></asp:Label>
                ]</td>
        </tr>
    </table>
    <asp:HiddenField ID="IDYEARBPHF" runat="server" />
</asp:Content>

