<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IPWorkFlowOverRide.ascx.cs" Inherits="UserControl_IPWorkFlowOverRide" %>
<table style="width: 98%" class="tMainBorder">
        <tr class="cHeadTile">
            <td style="width: 15%">Support</td>
            <td style="width: 15%">Responsible</td>
            <td style="width: 10%">Stand</td>
            <td style="width: 35%">&nbsp;Reason for Work Flow By-Pass</td>

            <td>Forward to another user</td>

        </tr>

        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Font-Bold="True" ForeColor="#003366"
                    Text="Finance Signature"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFinSignature" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFinanceStand" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFinSig" runat="server" Height="50px" TextMode="MultiLine"
                    Width="250px"></asp:TextBox>
            </td>

            <td>
                <asp:DropDownList ID="ddlFinSig" runat="server" Width="250px">
                </asp:DropDownList>
                <br />
                <asp:Button ID="btnFinSig" runat="server" Text="Forward"
                    OnClick="btnFinSig_Click" />
            </td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="Label35" runat="server" Font-Bold="True" ForeColor="#003366"
                    Text="OU Finance Director"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblOUFinDirector" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblOUfinDirStand" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtOUFinDir" runat="server" Height="50px" TextMode="MultiLine"
                    Width="250px"></asp:TextBox>
            </td>

            <td>
                <asp:DropDownList ID="ddlOUFinDir" runat="server" Width="250px">
                </asp:DropDownList>
                <br />
                <asp:Button ID="btnOUFinDir" runat="server" Text="Forward"
                    OnClick="btnGMFin_Click" />
                <br />
            </td>

        </tr>
        <tr>
            <td colspan="5">
                <asp:LinkButton ID="GMDetailedSupportLinkButton" runat="server"
                    OnClick="GMDetailedSupportLinkButton_Click">Click here to view support details from General Managers</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td class="cHeadTile" colspan="5">Organisational Support</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label36" runat="server" Font-Bold="True" ForeColor="#003366"
                    Text="Corporate Planning Mgr"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCPM" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCPMStand" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCPM" runat="server" Height="50px" TextMode="MultiLine"
                    Width="250px"></asp:TextBox>
            </td>

            <td>
                <asp:DropDownList ID="ddlCPM" runat="server" Width="250px">
                </asp:DropDownList>
                <br />
                <asp:Button ID="btnCPM" runat="server" Text="Forward" OnClick="btnCPM_Click" />
                <br />
            </td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="Label33" runat="server" Font-Bold="True" ForeColor="#003366"
                    Text="GM Regional Planning"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblGMRE" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblGMREPlanStand" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtGMRE" runat="server" Height="50px" TextMode="MultiLine"
                    Width="250px"></asp:TextBox>
            </td>

            <td>
                <asp:DropDownList ID="ddlGMREPlan" runat="server" Width="250px">
                </asp:DropDownList>
                <br />
                <asp:Button ID="btnGMREPlan" runat="server" Text="Forward"
                    OnClick="btnGMREPlan_Click" />
                <br />
            </td>

        </tr>
        <tr>
            <td class="cHeadTile" colspan="5">Organisational Approval</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label31" runat="server" Font-Bold="True" ForeColor="#003366"
                    Text="Approval (General Manager)"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblGM" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblGMStand" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtGM" runat="server" Height="50px" TextMode="MultiLine"
                    Width="250px"></asp:TextBox>
            </td>

            <td>
                <asp:DropDownList ID="ddlGM" runat="server" Width="250px">
                </asp:DropDownList>
                <br />
                <asp:Button ID="btnVP" runat="server" Text="Forward" OnClick="btnVP_Click" />
                <br />
            </td>

        </tr>
        <tr>
            <td colspan="5">
                <asp:LinkButton ID="VPDetailedSupportLinkButton" runat="server"
                    OnClick="VPDetailedSupportLinkButton_Click">Click here to view support details from Vice Presidents</asp:LinkButton>
            </td>

        </tr>
        <tr>
            <td>
                <asp:Label ID="Label28" runat="server" Font-Bold="True" ForeColor="#003366"
                    Text="Regional Vice president"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblREVP" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblRevpStand" runat="server" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtREVP" runat="server" Height="50px" TextMode="MultiLine"
                    Width="250px"></asp:TextBox>
            </td>

            <td>
                <asp:DropDownList ID="ddlREVP" runat="server" Width="250px">
                </asp:DropDownList>
                <br />
                <br />
                <asp:Button ID="btnREVP" runat="server" Text="Forward"
                    OnClick="btnREVP_Click" />
                <asp:HiddenField ID="proposalIDHiddenField" runat="server" />                     
            </td>

        </tr>
        </table>
   
