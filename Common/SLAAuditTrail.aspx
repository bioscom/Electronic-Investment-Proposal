<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="SLAAuditTrail.aspx.cs" Inherits="Common_SLAAuditTrail" %>

<%@ Register Src="../UserControl/Others/dateControl.ascx" TagName="dateControl" TagPrefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="smtAjaxManager" CombineScripts="false" />

    <table style="width: 98%" class="tMainBorder">
        <tr class="cHeadTile">
            <td colspan="5">SLA Reminder Audit Trail</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="fromLabel" runat="server" Text="From:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <uc2:dateControl ID="fromDateControl" runat="server" />
            </td>
            <td>
                <asp:Label ID="toLabel" runat="server" Text="To:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <uc2:dateControl ID="toDateControl" runat="server" />
            </td>
            <td>
                <asp:Button ID="viewButton" runat="server" Text="View"
                    OnClick="viewButton_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                    <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="False"
                        Width="100%" AllowPaging="True"
                        OnPageIndexChanging="grdView_PageIndexChanging"
                        PageSize="40" AllowSorting="True"
                        OnRowCommand="grdView_RowCommand"
                        OnSorting="grdView_Sorting">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IP Number" SortExpression="PROJ_NUM">
                                <ItemTemplate>
                                    <asp:Label ID="labelIPNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Title" SortExpression="PROJ_TITLE">
                                <ItemTemplate>
                                    <asp:Label ID="labelProjectTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="350px" Wrap="True"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Responsible Support" SortExpression="FULLNAME">
                                <ItemTemplate>
                                    <asp:Label ID="labelSupport" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FULLNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="User Role" SortExpression="USERROLESID">
                                <ItemTemplate>
                                    <asp:Label ID="labelUserRole" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "USERROLESID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Stand" SortExpression="STAND">
                                <ItemTemplate>
                                    <asp:Label ID="labelStand" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "STAND") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Comment">
                                <ItemTemplate>
                                    <asp:Label ID="labelComment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COMMENTS") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="450px" Wrap="True"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date Received" SortExpression="DATE_RECEIVED">
                                <ItemTemplate>
                                    <asp:Label ID="labelDateReceived" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_RECEIVED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Time Received" SortExpression="TIME_RECEIVED">
                                <ItemTemplate>
                                    <asp:Label ID="labelTimeReceived" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TIME_RECEIVED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
