<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="true" CodeFile="MyProposalInformation.aspx.cs" Inherits="Common_MyProposalInformation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%@ Register src="../UserControl/IPInitiatorApprovedProposals.ascx" tagname="IPInitiatorApprovedProposals" tagprefix="uc1" %>
<%@ Register src="../UserControl/IPInitiatorPendingProposals.ascx" tagname="IPInitiatorPendingProposals" tagprefix="uc2" %>

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
    
    
<div style="color:Black; width:98%; text-align:left">
    <asp:UpdatePanel ID="uppAjaxBloc" runat="server">
        <ContentTemplate>
            <ajaxToolkit:TabContainer runat="server" ID="smtAjaxTabs" ActiveTabIndex="0" Width="99%">
                <ajaxToolkit:TabPanel runat="server" ID="pnlAwaiting" HeaderText="Awaiting Approval" Visible="true">
                    <HeaderTemplate>                  
                        My Pending Proposals
                    </HeaderTemplate>
                    
                    <ContentTemplate>
                        <uc2:IPInitiatorPendingProposals ID="IPInitiatorPendingProposals2"  runat="server" />                
                    </ContentTemplate>
                    
                </ajaxToolkit:TabPanel>
                
                <ajaxToolkit:TabPanel runat="server" ID="pnlApproved" HeaderText="Approved PPS Code" Visible="true">
                    <HeaderTemplate>                        
                        My Approved Proposals                        
                    </HeaderTemplate>
                                        
                    <ContentTemplate>
                        <uc1:IPInitiatorApprovedProposals ID="IPInitiatorApprovedProposals2"  runat="server" />                              
                    </ContentTemplate>
                    
                </ajaxToolkit:TabPanel>
                                
            </ajaxToolkit:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
        
    <asp:Label runat="server" ID="CurrentTab" />
    <asp:Label runat="server" ID="Messages" />  
</div>
</asp:Content>

