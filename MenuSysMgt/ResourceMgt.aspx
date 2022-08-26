<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="ResourceMgt.aspx.cs" Inherits="MenuSysMgt_ResourceMgt" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 98%" class="tMainBorder">
        <tr>
            <td style="width: 30%">
                <table class="tMainBorder" style="width: 450px">
                    <tr>
                        <td class="cHeadTile" colspan="2">Add Main Menu</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Main Menu:" Font-Bold="True"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMainMenu" ErrorMessage="Main Menu title is required" ValidationGroup="MainMenu">*</asp:RequiredFieldValidator>
                            </td>
                        <td>
                            <asp:TextBox ID="txtMainMenu" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Menu Description:" Font-Bold="True"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMainMenuDesc" ErrorMessage="Menu description is required" ValidationGroup="MainMenu">*</asp:RequiredFieldValidator>
                            </td>
                        <td>
                            <asp:TextBox ID="txtMainMenuDesc" runat="server" TextMode="MultiLine" Width="300px"
                                Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" ValidationGroup="MainMenu" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="MainMenu" />
                        </td>
                    </tr>
                </table>
                <table style="width: 450px" class="tMainBorder">
                    <tr>
                        <td class="cHeadTile" colspan="2">Add Sub Menu</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Main Menu:" Font-Bold="True"></asp:Label>
                            <asp:CompareValidator ID="CompareValidator1" runat="server"
                                ControlToValidate="ddlMainMenu" ErrorMessage="Main Menu is required"
                                Operator="NotEqual" Type="Integer" ValueToCompare="-1" ValidationGroup="SubMenu">*</asp:CompareValidator>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMainMenu" runat="server">
                                <asp:ListItem Value="-1">--Select Main Menu--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Menu Title:" Font-Bold="True"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="titleTextBox" ErrorMessage="Menu Title is required" ValidationGroup="SubMenu">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="titleTextBox" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Menu Description:" Font-Bold="True"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="descTextBox" ErrorMessage="Description is required" ValidationGroup="SubMenu">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="descTextBox" runat="server" TextMode="MultiLine" Width="300px"
                                Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Navigate URL:" Font-Bold="True"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="navigateURLTextBox" ErrorMessage="Navigate URL is required" ValidationGroup="SubMenu">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="navigateURLTextBox" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="addButton" runat="server" OnClick="addButton_Click"
                                Text="Add" ValidationGroup="SubMenu" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="SubMenu" />
                        </td>
                    </tr>
                </table>

            </td>
            <td>
                <asp:GridView ID="formsGridView" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" Width="100%" PageSize="30"
                    OnPageIndexChanging="formsGridView_PageIndexChanging"
                    OnRowCommand="formsGridView_RowCommand">
                    <Columns>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="...">
                            <ItemTemplate>
                                <asp:LinkButton ID="editLinkButton" runat="server" CommandName="EditThis"
                                    CommandArgument='<%# Container.DisplayIndex %>'
                                    MENUID='<%# DataBinder.Eval(Container.DataItem, "MENUID") %>'>Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="...">
                            <ItemTemplate>
                                <asp:LinkButton ID="deleteLinkButton" runat="server" CommandName="DeleteThis"
                                    CommandArgument='<%# Container.DisplayIndex %>'
                                    MENUID='<%# DataBinder.Eval(Container.DataItem, "MENUID") %>' OnClientClick="return confirm('Are you sure you want to delete this?')">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="labelformTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TITLE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="labelformDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="350px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Navigate URL">
                            <ItemTemplate>
                                <asp:Label ID="labelformURL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NAVIGATEURL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>