<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="FunctionalIPS.aspx.cs" Inherits="Common_FunctionalIPS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 98%" class="tMainBorder">
        <tr class="cHeadTile">
            <td>
                Functional Investment Proposals</td>
        </tr>
        <tr>
            <td>
                <center>
                    <asp:Label ID="currentPageLabel" runat="server" ForeColor="#006600"></asp:Label>
                </center>
            </td>
        </tr>
        <tr>
            <td>
            <div style="background-color:White">
    <asp:GridView id="IPTrackRegGridView" runat="server" 
        AutoGenerateColumns="False" onrowcommand="IPTrackRegGridView_RowCommand" 
        AllowPaging="True" PageSize="15" Width="100%" 
        onsorted="IPTrackRegGridView_Sorted" 
    onsorting="IPTrackRegGridView_Sorting" AllowSorting="True"
    onpageindexchanging="IPTrackRegGridView_PageIndexChanging">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                   <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="IP Number" SortExpression="PROJ_NUM">
            <ItemTemplate>
                <asp:Label id="labelProjectNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Project Title" SortExpression="PROJ_TITLE">
                <ItemTemplate>
                    <asp:Label id="labelProjectTitle" runat="server"  
                    Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>' ForeColor="#003366" Font-Bold="True"></asp:Label>
                </ItemTemplate>

                <ItemStyle Wrap="True"></ItemStyle>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Project Initiator" SortExpression="PROJ_INIT">
                <ItemTemplate>
                    <asp:Label id="labelInitiator" runat="server" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_INIT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="JV($mln)" SortExpression="JV">
                <ItemTemplate>
                    <div style="text-align:right">
                        <asp:Label id="labelAmountJV" runat="server" style="color:Green" Font-Bold="True"
                        Text='<%# DataBinder.Eval(Container.DataItem, "JV") %>'></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="SS($mln)" SortExpression="SS">
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
            
            <asp:TemplateField HeaderText="Date Initiated" SortExpression="DATE_INIT">
                <ItemTemplate>
                    <asp:Label id="labelDateInitiated" runat="server" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "DATE_INIT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Date Forwarded" SortExpression="DATE_SUBMIT">
            <ItemTemplate>
                <asp:Label id="labelDateForwarded" runat="server" 
                Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
            
            <%--<asp:TemplateField HeaderText="Date Supported/Approved by VP">
                <ItemTemplate>
                    <asp:Label id="labelDateApprovedVP" runat="server" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "DIR_DATE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Date Approved by REVP">
                <ItemTemplate>
                    <asp:Label id="labelDateApprovedREVP" runat="server" 
                    Text='<%# DataBinder.Eval(Container.DataItem, "VP_DATE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
    
            <asp:TemplateField HeaderText="...">
                <ItemTemplate>
                    <asp:LinkButton id="ViewStatusLinkButton" runat="server" 
                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'  
                    CommandName="ViewStatus" CommandArgument='<%# Container.DisplayIndex %>'>View Status</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
            <%--<asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="OriginalProposalLinkButton" runat="server" 
                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                    CommandName="ViewOriginalProposal" CommandArgument='<%# Container.DisplayIndex %>'>View Proposal</asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>--%>
            
            <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="forwardProposalLinkButton" runat="server" 
                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                    CommandName="forwardProposal" CommandArgument='<%# Container.DisplayIndex %>'>Forward IP to my email</asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="...">
            <ItemTemplate>
                <asp:LinkButton id="DeleteProposalLinkButton" runat="server" 
                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                    CommandName="DeleteProposal" CommandArgument='<%# Container.DisplayIndex %>'>Delete</asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
                        
            <asp:TemplateField HeaderText="Flag">
                <ItemTemplate>
                    <asp:TextBox id="flagImage" runat="server" Width="50" ReadOnly="True" BorderStyle="None"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>    
            </div>
            </td>
        </tr>
        <tr class="cHeadTile">
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br /><br /><br />
    <br /><br /><br />
</asp:Content>
