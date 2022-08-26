<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="rptLastAction.aspx.cs" Inherits="Reports_rptLastAction" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 99%">
        <tr>
            <td class="cHeadTile">
                Date of Last Action</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width: 99%">
                    <tr>
                        <td style="width:250px">
                            <table style="width: 99%">
                                <tr>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                            <asp:Label ID="Label1" runat="server" Text="Enter number of days:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                            <asp:TextBox ID="daysTextBox" runat="server" Width="90px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
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
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <rsweb:ReportViewer ID="rptViewer" runat="server" Font-Names="Verdana" 
                                Font-Size="8pt" Height="400px" Width="100%" BorderStyle="Solid" 
                                BorderColor="Black" BorderWidth="1px">
                            </rsweb:ReportViewer>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="cHeadTile">
                &nbsp;</td>
        </tr>
    </table>
    <br />
</asp:Content>

