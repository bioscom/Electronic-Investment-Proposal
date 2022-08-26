<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="SLA.aspx.cs" Inherits="IPAdministrator_SLA" Title="Investment Proposal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript" src="../JavaScripts/eip.js"></script>
    <script language="javascript" type="text/javascript" src="../JavaScripts/MaxCharacterValidator.js"></script>

    <table style="width: 600px" class="tMainBorder">
        <tr>
            <td class="cHeadTile">Service Level Agreement (SLA)</td>
        </tr>
        <tr>
            <td>
                <center>
                    <asp:Button ID="closeButton" runat="server" OnClick="closeButton_Click"
                        Text="Close" ValidationGroup="xxxx" />
                    &nbsp;</center>
            </td>
        </tr>
        <tr>
            <td>
                <table class="tMainBorder" style="width: 99%">
                    <tr>
                        <td class="cHeadTile" colspan="3">Set number of working week(s)</td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="3">
                            <asp:Label ID="MSGLabel" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 30%">
                            <asp:Label ID="Label2" runat="server" Text="Functional Support SLA"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtFSSLA" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Working days"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Approval SLA" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtASLA" runat="server" Width="50px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Working days"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <asp:Button ID="submitbtn" runat="server" Text="Submit"
                                OnClick="submitbtn_Click" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="tMainBorder" style="width: 99%">
                    <tr>
                        <td class="cHeadTile" colspan="2">Set Colour codes</td>
                        <td class="cHeadTile" colspan="3">Colour Codes</td>
                    </tr>
                    <tr>
                        <td style="width: 30%">IP
                        Under Construction</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:RadioButton ID="opt1" runat="server" BackColor="Red"
                                GroupName="underconstruction" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt2" runat="server" BackColor="#FF6600"
                                GroupName="underconstruction" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt3" runat="server" BackColor="Green"
                                GroupName="underconstruction" />
                        </td>
                    </tr>
                    <tr>
                        <td>IP
                        Under Approval</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:RadioButton ID="opt4" runat="server" BackColor="Red"
                                GroupName="underapproval" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt5" runat="server" BackColor="#FF6600"
                                GroupName="underapproval" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt6" runat="server" BackColor="Green"
                                GroupName="underapproval" />
                        </td>
                    </tr>
                    <tr>
                        <td>IP
                        Within SLA</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:RadioButton ID="opt7" runat="server" BackColor="Red"
                                GroupName="withinsla" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt8" runat="server" BackColor="#FF6600"
                                GroupName="withinsla" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt9" runat="server" BackColor="Green"
                                GroupName="withinsla" />
                        </td>
                    </tr>
                    <tr>
                        <td>IP
                        Outside SLA</td>
                        <td>&lt; 1 week</td>
                        <td>
                            <asp:RadioButton ID="opt10" runat="server" BackColor="Red"
                                GroupName="outsidesla" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt11" runat="server" BackColor="#FF6600"
                                GroupName="outsidesla" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt12" runat="server" BackColor="Green"
                                GroupName="outsidesla" />
                        </td>
                    </tr>
                    <tr>
                        <td>IP
                        Exceeds SLA</td>
                        <td>&gt; 1 week</td>
                        <td>
                            <asp:RadioButton ID="opt13" runat="server" BackColor="Red"
                                GroupName="exceedsla" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt14" runat="server" BackColor="#FF6600"
                                GroupName="exceedsla" />
                        </td>
                        <td>
                            <asp:RadioButton ID="opt15" runat="server" BackColor="Green"
                                GroupName="exceedsla" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5"><span style="color: #FF0000"><b>Note: IP = Investment Proposal
                        </b></span>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="4">
                            <asp:Button ID="savebtn" runat="server" Text="Save"
                                OnClick="savebtn_Click" CausesValidation="False" Width="100px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="cHeadTile">&nbsp;</td>
        </tr>
    </table>
</asp:Content>
