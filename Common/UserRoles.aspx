<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="UserRoles.aspx.cs" Inherits="Common_UserRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tMainBorder" style="width: 50%">
        <tr>
            <td class="cHeadTile">User Roles (Set Mandatory Functional Support)</td>
        </tr>
        <tr>
            <td style="background-color:White">
                <div style="font-size:120%">
                    <asp:GridView id="grdView" runat="server" 
                        AutoGenerateColumns="False" onrowcommand="grdView_RowCommand" 
                        AllowPaging="True" PageSize="12" Width="100%" onsorted="grdView_Sorted" 
                        onsorting="grdView_Sorting" AllowSorting="True" 
                        onpageindexchanging="grdView_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="User Roles" SortExpression="ROLES">
                            <ItemTemplate>
                                <asp:Label id="labelUserRoles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ROLES") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Mandatory Functional Support">
                                <ItemTemplate>
                                    <asp:Label id="labelMandatory" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Make Mandatory">
                                <ItemTemplate>
                                    <asp:CheckBox ID="mandatoryCheckBox" runat="server" 
                                    MANDATORY='<%# DataBinder.Eval(Container.DataItem, "MANDATORY") %>' 
                                    USERROLESID='<%# DataBinder.Eval(Container.DataItem, "USERROLESID") %>' 
                                    CommandArgument='<%# Container.DisplayIndex %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td style="background-color:White">
                <div style="float:right; margin-right:1.5em">
                    <asp:Button ID="submitButton" runat="server" Text="Submit" 
                        onclick="submitButton_Click" />
                </div>
            </td>
        </tr>
        <tr>
            <td class="cHeadTile">&nbsp;</td>
        </tr>
    </table>
</asp:Content>