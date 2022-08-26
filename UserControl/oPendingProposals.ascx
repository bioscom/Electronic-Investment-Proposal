<%@ Control Language="C#" AutoEventWireup="true" CodeFile="oPendingProposals.ascx.cs" Inherits="UserControl_oPendingProposals" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1"  CombineScripts="false" EnablePartialRendering="true" />--%>
<table class="tMainBorder" style="width: 100%">
    <tr class="cHeadTile">
        <td>
            <asp:Label ID="Label1" runat="server" Font-Bold="True"
                Text="IP Tracking Register"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="hlpButton" runat="server" Text="Click here for help..."
                Width="150px" OnClientClick="return false;" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="IPTrackRegGridView" runat="server"
                AutoGenerateColumns="False" OnRowCommand="IPTrackRegGridView_RowCommand"
                AllowPaging="True" PageSize="20" Width="100%" OnSorted="IPTrackRegGridView_Sorted"
                OnSorting="IPTrackRegGridView_Sorting" AllowSorting="True"
                OnPageIndexChanging="IPTrackRegGridView_PageIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IP Number" SortExpression="PROJ_NUM">
                        <ItemTemplate>
                            <asp:Label ID="labelProjectNumber" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_NUM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project Title" SortExpression="PROJ_TITLE">
                        <ItemTemplate>
                            <asp:Label ID="labelProjectTitle" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_TITLE") %>' ForeColor="#003366"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project Initiator" SortExpression="PROJ_INIT">
                        <ItemTemplate>
                            <%--<asp:Label id="labelInitiator" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROJ_INIT") %>'></asp:Label>--%>
                            <a href="mailto:<%# DataBinder.Eval(Container.DataItem, "USERMAIL") %>"><%# DataBinder.Eval(Container.DataItem, "PROJ_INIT")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JV($mln)" SortExpression="JV">
                        <ItemTemplate>
                            <div style="text-align: right">
                                <asp:Label ID="labelAmountJV" runat="server" Style="color: Green" Font-Bold="True"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "JV") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SS($mln)" SortExpression="SS">
                        <ItemTemplate>
                            <div style="text-align: right">
                                <asp:Label ID="labelAmountSS" runat="server" Style="color: Green" Font-Bold="True"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "SS") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="labelStatus" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Submitted" SortExpression="DATE_SUBMIT">
                        <ItemTemplate>
                            <%--<asp:Label id="labelDateForwarded" runat="server" 
                            Text='<%# DataBinder.Eval(Container.DataItem, "DATE_SUBMIT") %>'></asp:Label>--%>
                            <asp:Label ID="labelDateForwarded" runat="server" Text='<%# Bind("DATE_SUBMIT", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date of Last Action" SortExpression="DATE_LAST_ACTIONED">
                        <ItemTemplate>
                            <%--<asp:Label id="labelDateInitiated" runat="server" 
                                Text='<%# DataBinder.Eval(Container.DataItem, "DATE_LAST_ACTIONED") %>'></asp:Label>--%>
                            <asp:Label ID="labelDateInitiated" runat="server" Text='<%# Bind("DATE_LAST_ACTIONED", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="ViewStatusLinkButton" runat="server"
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="ViewStatus" CommandArgument='<%# Container.DisplayIndex %>'>View Status</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="OriginalProposalLinkButton" runat="server"
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="ViewOriginalProposal" CommandArgument='<%# Container.DisplayIndex %>'>View Proposal</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="forwardProposalLinkButton" runat="server"
                                PROPOSALid='<%# DataBinder.Eval(Container.DataItem, "IDPROPOSAL") %>'
                                CommandName="forwardProposal" CommandArgument='<%# Container.DisplayIndex %>'>Forward IP to my email</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

<!-- "Wire frame" div used to transition from the button to the info panel -->
<div id="flyout" style="display: none; overflow: hidden; z-index: 2; background-color: #FFFFFF; border: solid 1px #D0D0D0;"></div>

<!-- Info panel to be displayed as a flyout when the button is clicked -->
<div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
    <div id="btnCloseParent" style="float: right; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);">
        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X" ToolTip="Close"
            Style="background-color: #666666; color: #FFFFFF; text-align: center; font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
    </div>
    <div>
        <p>
            <b><span style="text-decoration: underline">Electronic Investment Proposal</span></b>
            <br />
            <br />
            Menu are available to you depending on the role you play in this application.<br />
            At the Top-Left-Corner of your screen, immediately above this help, there are drop down list of menu.<br />
            <br />
            To action a proposal, click <b>"Pending Proposal".</b> The list(s) of proposal(s) waiting for your action will be listed.
                    Then click on <b>"Action..."</b> against the proposal you want to action.<br />
            Select <b>"Supported/Not Supported</b> or <b>"Approved/Not Approved"</b>, enter your comments and click Forward or Submit button.<br />
            These options are available to you based on your role or expected relevant action on the Proposal.
        </p>
        <br />
        <p>
            In case of further assistance, please call ext 24772 or isaac.bejide@shell.com. Thank you.
                    <%--<asp:LinkButton id="lnkShow" OnClientClick="return false;" runat="server">show</asp:LinkButton> and
                    <asp:LinkButton OnClientClick="return false;" id="lnkClose" runat="server">close</asp:LinkButton>--%>
        </p>
    </div>
</div>

<script type="text/javascript" language="javascript">
    // Move an element directly on top of another element (and optionally
    // make it the same size)
    function Cover(bottom, top, ignoreSize) {
        var location = Sys.UI.DomElement.getLocation(bottom);
        top.style.position = 'absolute';
        top.style.top = location.y + 'px';
        top.style.left = location.x + 'px';
        if (!ignoreSize) {
            top.style.height = bottom.offsetHeight + 'px';
            top.style.width = bottom.offsetWidth + 'px';
        }
    }
</script>
<ajaxToolkit:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="hlpButton">
    <Animations>
                <OnClick>
                    <Sequence>
                        <%-- Disable the button so it can't be clicked again --%>
                        <EnableAction Enabled="false" />
                        
                        <%-- Position the wire frame on top of the button and show it --%>
                        <ScriptAction Script="Cover($get('ctl00_ContentPlaceHolder1_hlpButton'), $get('flyout'));" />
                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                        
                        <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
                        <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                            <Move Horizontal="150" Vertical="-50" />
                            <Resize Width="260" Height="280" />
                            <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                        </Parallel>
                        
                        <%-- Move the info panel on top of the wire frame, fade it in, and hide the frame --%>
                        <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                        <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                        <FadeIn AnimationTarget="info" Duration=".2"/>
                        <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                        
                        <%-- Flash the text/border red and fade in the "close" button --%>
                        <Parallel AnimationTarget="info" Duration=".5">
                            <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                            <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                        </Parallel>
                        <Parallel AnimationTarget="info" Duration=".5">
                            <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                            <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                            <FadeIn AnimationTarget="btnCloseParent" MaximumOpacity=".9" />
                        </Parallel>
                    </Sequence>
                </OnClick>
    </Animations>
</ajaxToolkit:AnimationExtender>
<ajaxToolkit:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
    <Animations>
                <OnClick>
                    <Sequence AnimationTarget="info">
                        <%--  Shrink the info panel out of view --%>
                        <StyleAction Attribute="overflow" Value="hidden"/>
                        <Parallel Duration=".3" Fps="15">
                            <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                            <FadeOut />
                        </Parallel>
                        
                        <%--  Reset the sample so it can be played again --%>
                        <StyleAction Attribute="display" Value="none"/>
                        <StyleAction Attribute="width" Value="250px"/>
                        <StyleAction Attribute="height" Value=""/>
                        <StyleAction Attribute="fontSize" Value="12px"/>
                        <OpacityAction AnimationTarget="btnCloseParent" Opacity="0" />
                        
                        <%--  Enable the button so it can be played again --%>
                        <EnableAction AnimationTarget="hlpButton" Enabled="true" />
                    </Sequence>
                </OnClick>
                <OnMouseOver>
                    <Color Duration=".2" PropertyKey="color" StartValue="#FFFFFF" EndValue="#FF0000" />
                </OnMouseOver>
                <OnMouseOut>
                    <Color Duration=".2" PropertyKey="color" StartValue="#FF0000" EndValue="#FFFFFF" />
                </OnMouseOut>
    </Animations>
</ajaxToolkit:AnimationExtender>
