<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="ViewProposalStatus.aspx.cs" Inherits="Common_ViewProposalStatus" Title="Investment Proposal" %>

<%@ Register Src="../UserControl/IPDetailInfo.ascx" TagName="IPDetailInfo" TagPrefix="eip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div style="width: 100%; font-size:90%">
        <div style="float: left; width: 29%">--%>

            <eip:IPDetailInfo ID="IPDetailInfo1" runat="server" />

        <%--</div>
        <div style="float: left; width: 70%">--%>
            <table style="width: 98%" cellspacing="1" class="tMainBorder">
                <tr class="cHeadTile">

                    <td style="width: 2%">&nbsp;</td>

                    <td style="width: 18%">
                        <asp:Label ID="Label9" runat="server" Text="Support"></asp:Label>
                    </td>

                    <td style="width: 15%">
                        <asp:Label ID="Label34" runat="server" Text="Responsible Support"></asp:Label>
                    </td>
                    <td style="width: 11%">
                        <asp:Label ID="Label10" runat="server" Text="Stand"></asp:Label>
                    </td>
                    <td style="width: 34%">
                        <asp:Label ID="Label11" runat="server" Text="Comment"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="Label12" runat="server" Text="Date Received" Font-Bold="False"></asp:Label>
                    </td>
                    <td style="width: 15%">
                        <asp:Label ID="Label45" runat="server" Text="Date Reviewed" Font-Bold="False"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td>
                        <asp:CheckBox ID="ltleadCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label20" runat="server" Text="Line Team Lead" Font-Bold="True"
                            ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleTeamLeadLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="managerStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="managerCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="managerDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="managerDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="cplanCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="Business Process Owner"
                            Font-Bold="True" ForeColor="#003366"></asp:Label>
                        <br />
                        <asp:Label ID="Label7" runat="server" Text="(Regional Planning)"
                            Font-Bold="True" ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleCPLANLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="planStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="planCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="planDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="planDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr style="background-color: white">
                    <td colspan="7">
                        <hr />
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:CheckBox ID="bfmCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label32" runat="server" Text="Controller"
                            Font-Bold="True" ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleBFMLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="controllerStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="controllerCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="controllerDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="controllerDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="legalCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label17" runat="server" Text="Legal" Font-Bold="True" ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleLegalLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="legalStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="legalCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="legalDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="legalDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="taxCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label19" runat="server" Text="Tax" Font-Bold="True"
                            ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleTAXLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="taxStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="taxCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="taxDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="taxDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="treasuryCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label23" runat="server" Text="Treasury" Font-Bold="True"
                            ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleTreasuryLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="treasuryStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="treasuryCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="treasuryDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="treasuryDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ecoCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label15" runat="server" Text="Economics" Font-Bold="True"
                            ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleEcoLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="econsStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="econsCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="econsDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="econsDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="spcaCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label18" runat="server" Text="SPCA" Font-Bold="True"
                            ForeColor="#D42E12"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleSPCALabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="scdStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="scdCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="scdDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="scdDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="hseCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label14" runat="server" Text="HSE" Font-Bold="True"
                            ForeColor="#D42E12"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleHSELabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="hseStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="hseCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="hseDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="hseDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="secCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label22" runat="server" Text="Security" Font-Bold="True"
                            ForeColor="#D42E12"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleSecLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="securityStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="securityCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="securityDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="securityDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ITCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label43" runat="server" Text="IT" Font-Bold="True"
                            ForeColor="#D42E12"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleITLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="ITStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="ITCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="ITDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="ITDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="scmCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label44" runat="server" Text="SCM" Font-Bold="True"
                            ForeColor="#D42E12"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleSCMLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="SCMStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="SCMCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="SCMDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="SCMDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr style="background-color: white">
                    <td colspan="7" class="cHeadTileCenta">
                        <asp:Label ID="Label38" runat="server" Text="Finance Support/Approval" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <%--</div>--%>
                <tr>
                    <td>
                        <asp:CheckBox ID="finCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label16" runat="server" Text="Finance Signature" Font-Bold="True"
                            ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleFinLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="financeStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="financeCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="financeDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="financeDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="GMfinCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label35" runat="server" Text="Finance Director" Font-Bold="True"
                            ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleGMFinLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="GMfinanceStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="GMfinanceCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="GMfinanceDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="GMfinanceDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="VPfinCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label37" runat="server" Text="GM Finance" Font-Bold="True"
                            ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleVPFinLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="VPfinanceStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="VPfinanceCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="VPfinanceDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="VPfinanceDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td colspan="5">
                        <%--<asp:LinkButton ID="GMDetailedSupportLinkButton" runat="server"
                            OnClick="GMDetailedSupportLinkButton_Click">Click here to view support details from General Managers</asp:LinkButton>--%>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7" class="cHeadTileCenta">
                        <asp:Label ID="Label42" runat="server" Text="Organisational Support" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="CPMCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label36" runat="server" Text="Corporate Planning Mgr" Font-Bold="True"
                            ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleCPMLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CPMStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CPMCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CPMDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CPMDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <%--<tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="mdCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label21" runat="server" Text="Managing Director"
                            Font-Bold="True" ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleMDLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="directorStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="directorCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="directorDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="directorDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>--%>
                <tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="gmreplanCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label33" runat="server" Text="GM Regional Planning"
                            Font-Bold="True" ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleGMRELabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="GMREPlanStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="GMREPlanCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="GMREPlanDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="GMREPlanDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:CheckBox ID="cerpCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label46" runat="server" Text="Capital Expenditure Review Panel"
                            Font-Bold="True" ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleCERPLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CERPStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CERPCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CERPDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="CERPDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>



                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="7" class="cHeadTileCenta">
                        <asp:Label ID="Label24" runat="server" Text="Organisational Approval" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td>
                        <asp:CheckBox ID="vpCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label31" runat="server" Text="General Manager"
                            Font-Bold="True" ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleVPLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="approvalStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="approvalCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="approvalDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="approvalDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: #FFFFCC">
                    <td>
                        <asp:CheckBox ID="revpCheckBox" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="Label28" runat="server" Text="Regional Vice President"
                            Font-Bold="True" ForeColor="#003366"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="responsibleREVPLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="revpStandLabel" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="revpCommentLabel" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="revpDateReceived" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="revpDateReviewedLabel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="background-color: white">
                    <td colspan="6">
                        <asp:Button ID="forwardReminderButton" runat="server" Text="Forward Pending Proposal Reminder"
                            OnClick="forwardReminderButton_Click" Width="250px" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr class="cHeadTile">
                    <td colspan="7">&nbsp;
                    </td>
                </tr>
            </table>
        <%--</div>
    </div>--%>
    <asp:HiddenField ID="bfmHF" runat="server" />
    <asp:HiddenField ID="cplanHF" runat="server" />
    <asp:HiddenField ID="ecoHF" runat="server" />
    <asp:HiddenField ID="hseHF" runat="server" />
    <asp:HiddenField ID="legalHF" runat="server" />
    <asp:HiddenField ID="secHF" runat="server" />
    <asp:HiddenField ID="spcaHF" runat="server" />
    <asp:HiddenField ID="taxHF" runat="server" />
    <asp:HiddenField ID="treasuryHF" runat="server" />
    <asp:HiddenField ID="ltleadHF" runat="server" />
    <asp:HiddenField ID="finHF" runat="server" />
    <asp:HiddenField ID="GMfinHF" runat="server" />
    <asp:HiddenField ID="VPfinHF" runat="server" />
    <asp:HiddenField ID="mdHF" runat="server" />
    <asp:HiddenField ID="gmREHF" runat="server" />
    <asp:HiddenField ID="vpHF" runat="server" />
    <asp:HiddenField ID="revpHF" runat="server" />
    <asp:HiddenField ID="cpmHF" runat="server" />
    <asp:HiddenField ID="ITHF" runat="server" />
    <asp:HiddenField ID="scmHF" runat="server" />
    <asp:HiddenField ID="CerpHF" runat="server" />
</asp:Content>