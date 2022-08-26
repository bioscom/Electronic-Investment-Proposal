<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="rptDeletedIPs.aspx.cs" Inherits="Reports_rptDeletedIPs" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 98%">
        <tr>
            <td class="cHeadTile" colspan="4">
                Deleted Proposals</td>
        </tr>
        <tr style="background-color:Silver">
            <td style="width: 100px">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Function:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="functionddl" runat="server" Width="200px">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="viewButton" runat="server" onclick="viewButton_Click" 
                    Text="View Report" />
            </td>
            <td>
                <asp:Button ID="closeButton" runat="server" Text="Close" 
                    onclick="closeButton_Click" />
            </td>
        </tr>
        <tr>
            <td class="cHeadTile" colspan="4">
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <rsweb:ReportViewer ID="rptViewer" runat="server" Font-Names="Verdana" 
        Font-Size="8pt" Height="400px" Width="98%" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px">
    </rsweb:ReportViewer>
    </asp:Content>

