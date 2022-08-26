<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BPOComment.ascx.cs" Inherits="UserControl_BPOComment" %>

<asp:Label ID="Label1" runat="server" Text="Remarks / Comments" Font-Bold="true"></asp:Label>
<hr />
<br />
<table>
    <tr>
        <td>
            <asp:Label ID="StandLabel" runat="server" Text="Stand:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="SupportStandDropDownList" runat="server" Width="200px">
                <asp:ListItem Value="-1">Select Stand...</asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="SupportStandDropDownList" ErrorMessage="Please select your stand.." Operator="NotEqual" Type="Integer" ValidationGroup="comments" ValueToCompare="-1">*</asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="RemarkLabel" runat="server" Text="Remark/Comments:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="commentTextBox" runat="server" Height="200px"
                TextMode="MultiLine" Width="500px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click"
                Text="Submit" Width="100px" ValidationGroup="comments" />
            &nbsp; &nbsp;&nbsp; </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="mssgLabel" runat="server" CssClass="Warning"></asp:Label>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="comments" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="xproposalIDHiddenField" runat="server" />