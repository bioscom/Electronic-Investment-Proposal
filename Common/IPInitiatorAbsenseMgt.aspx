<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="IPInitiatorAbsenseMgt.aspx.cs" Inherits="Common_IPInitiatorAbsenseMgt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 98%" class="tMainBorder">
        <tr class="cHeadTile">
            <td>IP Initiator Absense Management</td>
        </tr>

        <tr>
            <td>
                <div>
                    <div style="float: left">
                        <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" Width="350px">
                            <asp:ListItem Value="-1">Select Out of Office IP Inititator</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div style="float: right">
                        <asp:DropDownList ID="ddlDelegatedUsers" runat="server" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" Width="350px">
                            <asp:ListItem Value="-1">Select Delegated IP Initiator</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlDelegatedUsers" ErrorMessage="Select Delegated IP Initiator" Operator="NotEqual" Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
                        &nbsp;
                        <asp:Button ID="btnForward" runat="server" Text="Forward IP(s)" OnClick="btnForward_Click" />
                    </div>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlUsers" ErrorMessage="Select Out of Office IP Initiator" Operator="NotEqual" Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
                </div>
            </td>
        </tr>

        <tr>
            <td>
                <div style="background-color: White">
                    <asp:GridView ID="grdView" runat="server" AutoGenerateColumns="False" OnRowCommand="grdView_RowCommand" AllowPaging="True" PageSize="15" Width="100%">                       
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IP Number">
                                <ItemTemplate>
                                    <asp:Label ID="labelProjectNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Title">
                                <ItemTemplate>
                                    <asp:Label ID="labelProjectTitle" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>' ForeColor="#003366" Font-Bold="True"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Wrap="True" Width="350px"></ItemStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="JV($mln)">
                                <ItemTemplate>
                                    <div style="text-align: right">
                                        <asp:Label ID="labelAmountJV" runat="server" Style="color: Green" Font-Bold="True"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "JV") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="SS($mln)">
                                <ItemTemplate>
                                    <div style="text-align: right">
                                        <asp:Label ID="labelAmountSS" runat="server" Style="color: Green" Font-Bold="True"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "SS") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Initiator" SortExpression="PROJ_INIT">
                                <ItemTemplate>
                                    <%--<asp:Label id="labelInitiator" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_INIT") %>'></asp:Label>--%>
                                    <a href="mailto:<%# DataBinder.Eval(Container.DataItem, "USERMAIL") %>"><%# DataBinder.Eval(Container.DataItem, "PROJ_INIT")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date Initiated">
                                <ItemTemplate>
                                    <asp:Label ID="labelDateInitiated" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "DATE_INIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Date Submitted">
                                <ItemTemplate>
                                    <asp:Label ID="labelDateForwarded" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
                </div>
            </td>
        </tr>
    </table>

</asp:Content>
