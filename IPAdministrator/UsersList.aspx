<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="UsersList.aspx.cs" Inherits="IPAdministrator_UsersList" Title="Investment Proposal" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 98%" class="tMainBorder">
        <tr>
            <td class="cHeadTile">
                <asp:Label ID="NewUserLabel" runat="server" Font-Bold="True"
                    Text="eIP Users List" ForeColor="#003366"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>
                <div>
                    <div style="float: left">
                        <table>
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlUserRole" runat="server" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlUserRole_SelectedIndexChanged">
                                        <asp:ListItem Value="-1">--Select User Role--</asp:ListItem>
                                    </asp:DropDownList>
                                </td>

                                <td>
                                    <asp:DropDownList ID="ddlFunctions" runat="server" Width="300px" AutoPostBack="True" OnSelectedIndexChanged="ddlFunctions_SelectedIndexChanged">
                                        <asp:ListItem Value="-1">View...</asp:ListItem>
                                        <asp:ListItem Value="1">Roles Accepted</asp:ListItem>
                                        <asp:ListItem Value="2">Roles Awaiting Acceptance</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td>
                                    <asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click"
                                        Text="Close" ValidationGroup="xxxx" Width="70px" Height="22px" /></td>
                            </tr>
                        </table>
                    </div>
                    <div style="float: right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Find User"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="userTextBox" runat="server" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:ImageButton ID="searchButton" runat="server" ImageUrl="~/Images/gosearch.gif"
                                        OnClick="searchButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
		&nbsp;
		<asp:Button ID="btnCleanUp" runat="server" Text="Clean Up Users Database" Width="200px" Height="22px" OnClick="btnCleanUp_Click"/>
                </div>
            </td>
        </tr>

        <tr>
            <td>
                <div>
                    <asp:GridView ID="UsersGridView" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" OnRowCommand="UsersGridView_RowCommand"
                        OnPageIndexChanging="UsersGridView_PageIndexChanging" PageSize="40"
                        AllowSorting="True" OnPageIndexChanged="UsersGridView_PageIndexChanged"
                        Width="100%" OnSorting="UsersGridView_Sorting"
                        OnSorted="UsersGridView_Sorted">
                        <Columns>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="...">
                                <ItemTemplate>
                                    <center>
                                        <asp:CheckBox ID="sendMailCheckBox" runat="server" />
                                    </center>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="..." HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Button ID="editLinkButton" runat="server" CommandName="EditThis" Text="Edit" Height="20px" Width="70px" CommandArgument='<%# Container.DisplayIndex %>'
                                        IDUSERMGT='<%# DataBinder.Eval(Container.DataItem, "IDUSERMGT") %>' USERMAIL='<%# DataBinder.Eval(Container.DataItem, "USERMAIL") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Full Name" SortExpression="FULLNAME">
                                <ItemTemplate>
                                    <asp:Label ID="labelFullName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FULLNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--<asp:TemplateField HeaderText="User Name" SortExpression="USERNAME">
                                <ItemTemplate>
                                    <asp:Label ID="labelUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "USERNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="User Role(s)" HeaderStyle-Width="150px" SortExpression="USERROLESID">
                                <ItemTemplate>
                                    <asp:Label ID="labelUserRole" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "USERROLESID") %>'
                                        USERROLESID='<%# DataBinder.Eval(Container.DataItem, "USERROLESID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Function" SortExpression="FUNCTION">
                                <ItemTemplate>
                                    <asp:Label ID="functionLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FUNCTION") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Company(ies)" SortExpression="COMPANYNAME">
                                <ItemTemplate>
                                    <asp:Label ID="labelCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COMPANYNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approval Limit ($ mln)" SortExpression="IDIPLIMIT" ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <div style="text-align: right">
                                        <asp:Label ID="labelApprovalLimit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IDIPLIMIT") %>'
                                            IDIPLIMIT='<%# DataBinder.Eval(Container.DataItem, "IDIPLIMIT") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Email Address" SortExpression="USERMAIL">
                                <ItemTemplate>
                                    <a href="mailto: <%# DataBinder.Eval(Container.DataItem, "USERMAIL") %>">
                                        <%# DataBinder.Eval(Container.DataItem, "USERMAIL") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="..." HeaderStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:Button ID="deleteLinkButton" runat="server" CommandName="DeleteThis" Text="Delete" Height="20px" Width="70px" CommandArgument='<%# Container.DisplayIndex %>'
                                        USERID='<%# DataBinder.Eval(Container.DataItem, "IDUSERMGT") %>'
                                        USERROLESID='<%# DataBinder.Eval(Container.DataItem, "USERROLESID") %>'
                                        FUNCTION='<%# DataBinder.Eval(Container.DataItem, "FUNCTION") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="reminderButton" runat="server" Text="Send Reminder Mail"
                    OnClick="reminderButton_Click" Width="200px" />
            </td>
        </tr>
    </table>
</asp:Content>
