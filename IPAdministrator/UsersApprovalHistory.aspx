<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="UsersApprovalHistory.aspx.cs" Inherits="IPAdministrator_UsersApprovalHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tMainBorder" style="width: 98%">
        <tr>
            <td class="cHeadTile">
                Users Approval/Support Audit Trail</td>
        </tr>
        <tr>
            <td>
    &nbsp;<asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlUser_SelectedIndexChanged" Width="300px">
        <asp:ListItem Value="-1">--Select User--</asp:ListItem>
    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            <asp:GridView id="auditTrailGridView" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                onpageindexchanging="auditTrailGridView_PageIndexChanging" Width="100%" PageSize="50">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                           <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                            <asp:Label id="labelSupportPerson" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FULLNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Role(s)">
                        <ItemTemplate>
                            <asp:Label id="labelSupportFunction" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "USERROLESID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Proposal Number">
                        <ItemTemplate>
                            <asp:Label id="labelProposalNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Proposal Name">
                        <ItemTemplate>
                            <asp:Label id="labelProposalName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>'></asp:Label>
                        </ItemTemplate>
			<ItemStyle Width="400px" />
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label id="labelSupportStand" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "STAND") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Comment">
                        <ItemTemplate>
                            <asp:Label id="labelSupportComment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "COMMENTS") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Date Reviewed">
                        <ItemTemplate>
                            <asp:Label id="labelDateReviewed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_RECEIVED") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Date Comment">
                        <ItemTemplate>
                            <asp:Label id="labelDateComment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DATE_COMMENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
        
                </Columns>
            </asp:GridView>
            </td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>

