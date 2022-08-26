<%@ Page Language="C#" MasterPageFile="~/MasterPages/FrontPage.master" AutoEventWireup="True" CodeFile="Confirmation.aspx.cs" Inherits="Common_Confirmation" Title="Investment Proposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>
        <div style="font-size: 100%; width: 600px">
            <asp:Panel ID="ViewPanel" runat="server">
                <asp:Label ID="mssgLabel" runat="server" CssClass="Warning"></asp:Label>
                <table style="width: 99%" class="tMainBorder">
                    <tr>
                        <td class="cHeadTile">IP Role Acceptance form</td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <div style="font-size: 120%; background-color: White">
                                <table>
                                    <%-- style="border:outset 2px; border-collapse:separate"--%>
                                    <tr>
                                        <td colspan="3">
                                            <center>
                                                <div style="font-size: 130%; text-align: center">
                                                    You have currently been nominated to play
                                                <br />
                                                    <asp:Label ID="roleLabel" runat="server" Font-Bold="True"></asp:Label>
                                                    &nbsp;<br />
                                                    role on the e-IP system<br />
                                                </div>
                                            </center>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="SystemAdminCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="BPOCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="IPInitiatorCheckBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="SystemAdminHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="BPOHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="IPInitiatorHF" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="LineTeamLeadCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="BOMCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="securityCheckBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="LineTeamLeadHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="BOMHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="SecurityHF" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="HSECheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="BFMCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="TAXCheckBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="HSEHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="BFMHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="TAXHF" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="SPCACheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="econsCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="treasuryCheckBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="SPCAHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="EconsHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="TreasuryHF" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="FinanceCheckBox" runat="server" />
                                        </td>
                                        <td>

                                            <asp:CheckBox ID="LegalCheckBox" runat="server" />

                                        </td>
                                        <td>
                                            <asp:CheckBox ID="GMREPlanCheckBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="FinanceHF" runat="server" />
                                        </td>
                                        <td>

                                            <asp:HiddenField ID="LegalHF" runat="server" />

                                        </td>
                                        <td>
                                            <asp:HiddenField ID="GMREPlanHF" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="MDCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="REVPCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="VPCheckBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="MDHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="REVPHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="VPHF" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="CPMCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="GMCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="EPGIPTrackerCheckBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="CPMHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="GMFinanceHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="EPGIPTrackerHF" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="functionalPlannerCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ITCheckBox" runat="server" />
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="scmCheckBox" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="functionalPlannerHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="ITHF" runat="server" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="scmHF" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <center>
                                <div style="font-size: 130%;">
                                    The eIP system is governed by the following behavioural guidelines;
                                </div>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="font-size: 150%; font-weight: bold">
                                <center>
                                    <asp:HyperLink ID="behaviouralGuideHyperLink" runat="server"
                                        Target="_blank"> Click here to read IP Behavioural Guidelines
                                    </asp:HyperLink>
                                </center>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <center>
                                <div style="font-size: 130%;">
                                    I confirm that I have read the behavioural guidelines
                            <br />
                                    and accept the responsiblities and service levels
                            <br />
                                    specified therein.
                                </div>
                            </center>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="font-size: 120%">
                                <table style="border: outset 0px; border-collapse: separate; width: 100%">
                                    <tr>
                                        <td valign="bottom">
                                            <center>
                                                <asp:Button ID="AcceptButton" runat="server" Text="Accept"
                                                    OnClick="AcceptButton_Click" /></center>
                                        </td>
                                        <td valign="bottom">
                                            <center>
                                                <asp:Button ID="DeclineButton" runat="server" Text="Decline"
                                                    OnClick="DeclineButton_Click" /></center>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

    </center>
</asp:Content>
