<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="ActivateDeletedIPs.aspx.cs" Inherits="Common_ActivateDeletedIPs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tMainBorder" style="width: 99%">
        <tr>
            <td class="cHeadTile">
                Deleted IPs</td>
        </tr>
        <tr>
            <td style="background-color:White">
    
    <asp:GridView id="DeletedIPsGridView" runat="server" 
        AutoGenerateColumns="False" onrowcommand="DeletedIPsGridView_RowCommand" 
        AllowPaging="True" PageSize="50" Width="100%">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                   <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="IP Number">
            <ItemTemplate>
                <asp:Label id="labelProjectNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Project Title">
                <ItemTemplate>
                    <asp:Label id="labelProjectTitle" runat="server"  
                    Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>' ForeColor="#003366" Font-Bold="True"></asp:Label>
                </ItemTemplate>

                <ItemStyle Wrap="True"></ItemStyle>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Project Initiator">
                <ItemTemplate>
                    <asp:Label id="labelInitiator" runat="server" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_INIT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Amount JV($mln)">
                <ItemTemplate>
                    <div style="text-align:right">
                        <asp:Label id="labelAmountJV" runat="server" style="color:Green" Font-Bold="True"
                        Text='<%# DataBinder.Eval(Container.DataItem, "JV") %>'></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Amount SS($mln)">
                <ItemTemplate>
                    <div style="text-align:right">
                        <asp:Label id="labelAmountSS" runat="server" style="color:Green" Font-Bold="True"
                        Text='<%# DataBinder.Eval(Container.DataItem, "SS") %>'></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
                
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label id="labelStatus" runat="server" Text =""></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Date Initiated">
                <ItemTemplate>
                    <asp:Label id="labelDateInitiated" runat="server" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "DATE_INIT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Date Forwarded">
            <ItemTemplate>
                <asp:Label id="labelDateForwarded" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
            
               
            <%--<asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="OriginalProposalLinkButton" runat="server" 
                    PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                    CommandName="ViewProposal" CommandArgument='<%# Container.DisplayIndex %>'>View Proposal</asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>--%>
            
            <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="UndeleteProposalLinkButton" runat="server" 
                    PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                    CommandName="UndeleteProposal" CommandArgument='<%# Container.DisplayIndex %>'>Undelete Proposal</asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="DeleteProposalLinkButton" runat="server" 
                    PROPOSALID='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                    CommandName="DeleteProposal" CommandArgument='<%# Container.DisplayIndex %>'>Permanently Delete Proposal</asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="cHeadTile">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

