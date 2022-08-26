<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="IPRegister.aspx.cs" Inherits="Common_IPRegister" Title="Investment Proposal" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="../UserControl/oPendingProposals.ascx" TagName="oPendingProposals" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/oApprovedProposals.ascx" TagName="oApprovedProposals" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/oDiscontinuedProposals.ascx" TagName="oDiscontinuedProposals" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="smtAjaxManager" runat="Server" CombineScripts="false" EnablePartialRendering="true" />
    <div style="color: Black; margin-left: 2px; margin-top: 2px; width: 100%">
        <asp:UpdatePanel ID="uppAjaxBloc" runat="server">
            <ContentTemplate>
                <ajaxToolkit:TabContainer runat="server" ID="smtAjaxTabs" ActiveTabIndex="0" Width="100%">

                    <ajaxToolkit:TabPanel runat="server" ID="pnlAwaiting" HeaderText="Pending Proposals" Visible="true">
                        <HeaderTemplate>
                            Pending Proposals
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc1:oPendingProposals ID="oPendingProposals1" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel runat="server" ID="pnlApproved" HeaderText="Approved Proposals" Visible="true">
                        <HeaderTemplate>
                            Approved Proposals
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc2:oApprovedProposals ID="oApprovedProposals1" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                    <ajaxToolkit:TabPanel runat="server" ID="pnlDiscontinued" HeaderText="Discontinued Proposals" Visible="true">
                        <HeaderTemplate>
                            Discontinued Proposals
                        </HeaderTemplate>
                        <ContentTemplate>
                            <uc3:oDiscontinuedProposals ID="oDiscontinuedProposals1" runat="server" />
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                </ajaxToolkit:TabContainer>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Label runat="server" ID="CurrentTab" />
        <asp:Label runat="server" ID="Messages" />
    </div>
</asp:Content>
