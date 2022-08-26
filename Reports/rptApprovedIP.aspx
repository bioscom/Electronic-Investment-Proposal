<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="rptApprovedIP.aspx.cs" Inherits="Reports_rptApprovedIP" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="../UserControl/Others/dateControl.ascx" TagName="dateControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1"
        runat="server" EnablePartialRendering="true">
    </ajaxToolkit:ToolkitScriptManager>

    <table style="width: 98%" class="tMainBorder">
        <tr class="cHeadTile">
            <td colspan="8">Approved Investment Proposal Reporting Criteria</td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Function:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="functionddl" runat="server" Width="200px">
                    <asp:ListItem Value="-1">Select Function</asp:ListItem>
                </asp:DropDownList>
            </td>

            <td>
                <asp:Label ID="Label6" runat="server" Font-Bold="True" Text="Org. Unit:"></asp:Label>
            </td>

            <td>
                <asp:DropDownList ID="OUList" runat="server"
                    OnSelectedIndexChanged="OUList_SelectedIndexChanged" AutoPostBack="True"
                    Width="200px">
                    <asp:ListItem Value="-1">Select OU</asp:ListItem>
                </asp:DropDownList>
            </td>

            <td>
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="From:"></asp:Label>
            </td>

            <td>
                <uc1:dateControl ID="fromDateControl" runat="server" />
            </td>

            <td>
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="To:"></asp:Label>
            </td>
            <td>
                <uc1:dateControl ID="toDateControl" runat="server" />
            </td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Org. Approver:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="OrganisationalApproverList" runat="server" Width="200px">
                    <asp:ListItem Value="-1">Select Organisational Approver</asp:ListItem>
                </asp:DropDownList>
            </td>

            <td>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="Fin. Approver:"></asp:Label>
            </td>

            <td>
                <asp:DropDownList ID="FinancialApproverDropDownList" runat="server"
                    Width="200px">
                    <asp:ListItem Value="-1">Select Finance Approver</asp:ListItem>
                </asp:DropDownList>
            </td>

            <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Amount SS:"></asp:Label>
            </td>

            <td>
                <asp:DropDownList ID="ass1ddl" runat="server" Width="80px">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Label ID="Label8" runat="server" Font-Bold="True" Text="and"></asp:Label>
                &nbsp;<asp:DropDownList ID="ass2ddl" runat="server" Width="80px">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </td>

            <td>&nbsp;</td>
            <td>&nbsp;</td>

        </tr>
        <tr>
            <td colspan="8" style="text-align: center">
                <asp:Button ID="viewButton" runat="server" OnClick="viewButton_Click" Text="View Report" />
                &nbsp;
                                <asp:Button ID="closeButton" runat="server" Text="Close"
                                    OnClick="closeButton_Click" />
            </td>

        </tr>
    </table>



    <table style="width: 98%" class="tMainBorder">
        <tr>
            <td>
                <%--<div style="margin-left: auto; margin-right: auto; overflow: auto; width: 98%">--%>
                <rsweb:ReportViewer ID="rptViewer" runat="server" Font-Names="Verdana"
                    Font-Size="8pt" Height="650px" Width="99%" BorderStyle="Solid"
                    BorderColor="Black" BorderWidth="1px" ZoomMode="Percent">
                </rsweb:ReportViewer>
                <%--</div>--%>

            </td>
        </tr>
    </table>
</asp:Content>

