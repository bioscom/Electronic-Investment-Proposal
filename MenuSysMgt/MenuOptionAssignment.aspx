<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="MenuOptionAssignment.aspx.cs" Inherits="MenuSysMgt_MenuOptionAssignment" Title="Investment Proposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="tMainBorder" style="width: 98%">
        <tr>
            <td class="cHeadTile">Resource Assignment Management</td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlUserRole" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlUserRole_SelectedIndexChanged">
                    <asp:ListItem Value="-1">Please Select User Role</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
                <asp:CompareValidator ID="CompareValidator1" runat="server"
                    ControlToValidate="ddlUserRole" ErrorMessage="Please select User Role"
                    Operator="NotEqual" Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>
                <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click"
                    Text="Submit" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdView" runat="server"
                    AutoGenerateColumns="False" OnRowCommand="grdView_RowCommand"
                    AllowPaging="True" PageSize="100" Width="100%" OnSorted="grdView_Sorted"
                    OnSorting="grdView_Sorting" AllowSorting="True"
                    OnPageIndexChanging="grdView_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="...">
                            <ItemTemplate>
                                <asp:CheckBox ID="menuCheckBox" runat="server"
                                    MENUID='<%# DataBinder.Eval(Container.DataItem, "MENUID") %>'
                                    PARENTID='<%# DataBinder.Eval(Container.DataItem, "PARENTID") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Title" SortExpression="TITLE">
                            <ItemTemplate>
                                <asp:Label ID="labelTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TITLE") %>' ForeColor="#003366" Font-Bold="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" SortExpression="DESCRIPTION">
                            <ItemTemplate>
                                <asp:Label ID="labelDescription" runat="server"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "DESCRIPTION") %>'></asp:Label>
                            </ItemTemplate>

                            <ItemStyle Wrap="True"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Navigate Url" SortExpression="NAVIGATEURL">
                            <ItemTemplate>
                                <asp:Label ID="labelUrl" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NAVIGATEURL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--<asp:TemplateField HeaderText="Parent Menu" SortExpression="PARENT">
                            <ItemTemplate>
                                <asp:Label id="labelAmountJV" runat="server" style="color:Green" Font-Bold="True" Text='<%# DataBinder.Eval(Container.DataItem, "PARENT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
</asp:Content>
