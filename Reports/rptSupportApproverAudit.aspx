<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="rptSupportApproverAudit.aspx.cs" Inherits="Reports_rptSupportApproverAudit" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="tMainBorder" style="width: 98%">
        <tr class="cHeadTile">
            <td>
                Investment Proposal Support/Approval Slippage Audit</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="rptGridViewerButton" runat="server" 
                    onclick="rptGridViewerButton_Click" Text="Grid View Mode" Width="150px" />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="rptViewerButton" runat="server" onclick="rptViewerButton_Click" 
                    Text="Report Viewer Mode" Width="150px" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="gridViewPanel" runat="server">
                    <asp:GridView id="auditTrailGridView" runat="server" 
                        AutoGenerateColumns="False" Width="100%" 
                        onrowcommand="auditTrailGridView_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                   <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="IP Number" SortExpression="PROJ_NUM">
                            <ItemTemplate>
                                <asp:LinkButton id="ViewStatusLinkButton" runat="server" 
                                    PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>' 
                                    CommandName="ViewStatus" CommandArgument='<%# Container.DisplayIndex %>' 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'>
                                </asp:LinkButton>
                            
                                <%--<asp:Label id="labelProjectNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>--%>
                            </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Project Title" SortExpression="PROJ_TITLE">
                                <ItemTemplate>
                                    <asp:Label id="labelProjectTitle" runat="server"  
                                    Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>' ForeColor="#003366" Font-Bold="True"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Wrap="True"></ItemStyle>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Support/Approver" SortExpression="FULLNAME">
                                <ItemTemplate>
                                    <asp:Label id="labelInitiator" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FULLNAME") %>'></asp:Label>                              
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
                                
                            <asp:TemplateField HeaderText="Roles">
                                <ItemTemplate>
                                    <asp:Label id="labelStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ROLES") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                                    
                            <asp:TemplateField HeaderText="Date Received" SortExpression="DATE_RECEIVED">
                            <ItemTemplate>
                                <asp:Label id="labelDateForwarded" runat="server" Text='<%# Bind("DATE_RECEIVED", "{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                        
                            <asp:TemplateField HeaderText="Date Comment" SortExpression="DATE_COMMENT">
                                <ItemTemplate>
                                    <asp:Label id="labelDateInitiated" runat="server" Text='<%# Bind("DATE_COMMENT", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Comment">
                                <ItemTemplate>
                                    <asp:Label id="labelDateApprovedVP" runat="server" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "COMMENTS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="rptViewPanel" runat="server">
                    <rsweb:ReportViewer ID="rptViewer" runat="server" BorderColor="Black" 
                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" 
                        Height="650px" Width="99%" ZoomMode="Percent">
                    </rsweb:ReportViewer>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <br /><br /><br />
    <br /><br /><br />
</asp:Content>

