<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="rptMonthlyBPPlan.aspx.cs" Inherits="Reports_rptMonthlyBPPlan" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tMainBorder" style="width: 98%">
        <tr>
            <td class="cHeadTile" colspan="8">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width:100px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Function:"></asp:Label>
            </td>
            <td style="width:100px">
                <asp:DropDownList ID="functionddl" runat="server" Width="200px">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width:100px">
                <asp:Label ID="Label1" runat="server" Text="Select Year:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="yearddl" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Select Month:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="monthddl" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="viewButton" runat="server" onclick="viewButton_Click" 
                    Text="View" />
            </td>
            <td>
                <asp:Button ID="closeButton" runat="server" Text="Close" 
                    onclick="closeButton_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="8" style="text-align:center">
                <asp:Label ID="mssgLabel" runat="server" CssClass="Warning"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="8" style="text-align:center" class="cHeadTile">
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <rsweb:ReportViewer ID="rptViewer" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="400px" Width="98%" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
    </rsweb:ReportViewer>
    <br />
    </asp:Content>

