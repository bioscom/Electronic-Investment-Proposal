<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="PendingProposal.aspx.cs" Inherits="ApprovalSupportFunction_PendingProposal" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%@ Register src="../UserControl/MyPendingProposals.ascx" tagname="MyPendingProposals" tagprefix="uc1" %>
<%@ Register src="../UserControl/MyApprovedProposalsHistory.ascx" tagname="MyApprovedProposalsHistory" tagprefix="uc2" %>
<%@ Register Src="~/UserControl/MyRejectedProposals.ascx" TagPrefix="uc1" TagName="MyRejectedProposals" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
<ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="smtAjaxManager" CombineScripts="false" />

     <script type="text/javascript">
         function PanelClick(sender, e) {
             var Messages = $get('<%=Messages.ClientID%>');
             Highlight(Messages);
         }

         function ActiveTabChanged(sender, e) {
             var CurrentTab = $get('<%=CurrentTab.ClientID%>');
             CurrentTab.innerHTML = sender.get_activeTab().get_headerText();
             Highlight(CurrentTab);
         }

         var HighlightAnimations = {};
         function Highlight(el) {
             if (HighlightAnimations[el.uniqueID] == null) {
                 HighlightAnimations[el.uniqueID] = Sys.Extended.UI.Animation.createAnimation({
                     AnimationName: "color",
                     duration: 0.5,
                     property: "style",
                     propertyKey: "backgroundColor",
                     startValue: "#FFFF90",
                     endValue: "#FFFFFF"
                 }, el);
             }
             HighlightAnimations[el.uniqueID].stop();
             HighlightAnimations[el.uniqueID].play();
         }

         function ToggleHidden(value) {
             $find('<%=smtAjaxTabs.ClientID%>').get_tabs()[2].set_enabled(value);
         }
    </script>
    
    
<div style="color:Black; width:100%; text-align:left">
    <asp:UpdatePanel ID="uppAjaxBloc" runat="server">
        <ContentTemplate>
            <ajaxToolkit:TabContainer runat="server" ID="smtAjaxTabs" ActiveTabIndex="0" Width="99%">
                <ajaxToolkit:TabPanel runat="server" ID="pnlAwaiting" HeaderText="Awaiting Approval" Visible="true">
                    <HeaderTemplate>                  
                        Pending Proposals
                    </HeaderTemplate>
                    
                    <ContentTemplate>
                        <uc1:MyPendingProposals ID="MyPendingProposals1" runat="server" />                    
                    </ContentTemplate>
                    
                </ajaxToolkit:TabPanel>
                
                <ajaxToolkit:TabPanel runat="server" ID="pnlApproved" HeaderText="Approved PPS Code" Visible="true">
                    <HeaderTemplate>                        
                        Approved Proposals History                      
                    </HeaderTemplate>
                                        
                    <ContentTemplate>
                        <uc2:MyApprovedProposalsHistory ID="MyApprovedProposalsHistory1" runat="server" />                             
                    </ContentTemplate>
                    
                </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="Rejected PPS Code" Visible="true">
                    <HeaderTemplate>                        
                        Rejected Proposals                      
                    </HeaderTemplate>
                                        
                    <ContentTemplate>
                        <uc1:MyRejectedProposals runat="server" ID="MyRejectedProposals" />          
                    </ContentTemplate>
                    
                </ajaxToolkit:TabPanel>
                                
            </ajaxToolkit:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
        
    <asp:Label runat="server" ID="CurrentTab" />
    <asp:Label runat="server" ID="Messages" />  
</div>
</asp:Content>

