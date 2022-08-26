<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="Remark.aspx.cs" Inherits="BPO_Remark" Title="Investment Proposal" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="../UserControl/BPOComment.ascx" TagName="BPOComment" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ForwardProposalToFunctionalSupport.ascx" TagName="ForwardProposalToFunctionalSupport" TagPrefix="uc2" %>
<%@ Register src="../UserControl/ForwardIPForFinanceSignature.ascx" tagname="ForwardIPForFinanceSignature" tagprefix="uc3" %>
<%@ Register src="../UserControl/IPWorkFlowOverRide.ascx" tagname="IPWorkFlowOverRide" tagprefix="uc4" %>
<%@ Register Src="../UserControl/ForwardProposalForOrganisationalApproval.ascx" TagName="ForwardProposalForOrganisationalApproval" TagPrefix="uc5" %>
<%--<%@ Register Src="../UserControl/Help.ascx" TagName="Help" TagPrefix="uc7" %>--%>

<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">
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

        function ToggleHidden(value)
        {
            $find('<%=smtAjaxTabs.ClientID%>').get_tabs()[2].set_enabled(value);
        }
    </script>

    <div style="width:98%">
    <div style="float:left; width:30%">   
        <eip:IPDetailInfo ID="IPDetailInfo1" runat="server" />
    </div>                        
    <div style="margin-left:15px; width:68%; color:Black; float:left">
        <asp:UpdatePanel ID="uppAjaxBloc" runat="server">
            <ContentTemplate>
                <ajaxToolkit:TabContainer runat="server" ID="smtAjaxTabs" ActiveTabIndex="0" Width="99%" Height="90px" TabIndex="3">
                    <ajaxToolkit:TabPanel runat="server" ID="pnlAwaiting" HeaderText="Awaiting Approval" Visible="true">
                        <HeaderTemplate>                  
                            Add Comment
                        </HeaderTemplate>
                        
                        <ContentTemplate>
                            <uc1:BPOComment ID="BPOComment1" runat="server" />                                       
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    
                    <ajaxToolkit:TabPanel runat="server" ID="pnlApproved" HeaderText="Approved PPS Code" Visible="true">
                        <HeaderTemplate>
                            Functional Support
                        </HeaderTemplate>
                        
                        <ContentTemplate>
                              <uc2:ForwardProposalToFunctionalSupport ID="ForwardProposalToFunctionalSupport1" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="Rejected PPS Code" Visible="true">
                        <HeaderTemplate>
                            Finance Signature
                        </HeaderTemplate>
                        
                        <ContentTemplate>
                            <uc3:ForwardIPForFinanceSignature ID="ForwardIPForFinanceSignature1" runat="server" />
                        </ContentTemplate>                                      
                        
                    </ajaxToolkit:TabPanel>
                                        
                    <ajaxToolkit:TabPanel runat="server" ID="TabPanel2" HeaderText="Rejected PPS Code" Visible="true">
                        <HeaderTemplate>
                            Organisational Approvers
                        </HeaderTemplate>
                        
                        <ContentTemplate>
                            <uc5:ForwardProposalForOrganisationalApproval ID="ForwardProposalForOrganisationalApproval1" runat="server"/>
                        </ContentTemplate>                                      
                        
                    </ajaxToolkit:TabPanel>
                    
                    <%--<ajaxToolkit:TabPanel runat="server" ID="TabPanel3" HeaderText="Rejected PPS Code" Visible="true">
                        <HeaderTemplate>
                            Override IP Workflow
                        </HeaderTemplate>
                        
                        <ContentTemplate>
                            
                            <uc4:IPWorkFlowOverRide ID="IPWorkFlowOverRide1" runat="server" />
                            
                        </ContentTemplate>                                      
                    </ajaxToolkit:TabPanel>
                    
                    <ajaxToolkit:TabPanel runat="server" ID="TabPanel4" HeaderText="Rejected PPS Code" Visible="true">
                        <HeaderTemplate>
                            Help
                        </HeaderTemplate>
                        
                        <ContentTemplate>
                            <uc7:Help ID="Help1" runat="server" />
                        </ContentTemplate>                                      
                        
                    </ajaxToolkit:TabPanel>--%>
                                    
                </ajaxToolkit:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:Label runat="server" ID="CurrentTab" />
        <asp:Label runat="server" ID="Messages" />  
                                    
    </div>
    
    </div>

    <%--<asp:HiddenField ID="bfmIDHF" runat="server" />
    <asp:HiddenField ID="ecoIDHF" runat="server" />
    <asp:HiddenField ID="hseIDHF" runat="server" />
    <asp:HiddenField ID="legalIDHF" runat="server" />
    <asp:HiddenField ID="secIDHF" runat="server" />
    <asp:HiddenField ID="spcaIDHF" runat="server" />
    <asp:HiddenField ID="taxIDHF" runat="server" />
    <asp:HiddenField ID="treasuryIDHF" runat="server" />  --%>  
    <br /><br /><br />
    <br /><br /><br />
</asp:content>

