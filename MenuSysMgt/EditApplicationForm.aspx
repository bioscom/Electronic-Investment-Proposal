<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="EditApplicationForm.aspx.cs" Inherits="IP.MenuSysMgt.EditApplicationForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 60%; background-color:Silver">
        <tr>
            <td colspan="3"  class="cHeadTile">
                Edit Application Form</td>
        </tr>
        <tr>
            <td colspan="3">
                    &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 5%">
                    &nbsp;</td>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Title" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="titleTextBox" runat="server" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Description" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="descTextBox" runat="server" TextMode="MultiLine" Width="300px" 
                        Height="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Navigate URL" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="navigateURLTextBox" runat="server" Width="500px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                    &nbsp;</td>
            <td>
                    &nbsp;</td>
            <td>
                <asp:Button ID="updateButton" runat="server" onclick="updateButton_Click" 
                        Text="Update" Width="80px" />
            &nbsp;
                <asp:Button ID="closeButton" runat="server" onclick="closeButton_Click" 
                        Text="Close" Width="80px" />
            </td>
        </tr>
    </table>
</asp:Content>
