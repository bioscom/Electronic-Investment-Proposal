<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="IPLimit.aspx.cs" Inherits="Common_IPLimit" Title="Investment Proposal" %>

<asp:Content id="Content1" ContentPlaceHolderid="ContentPlaceHolder1" Runat="Server">
    <table style="width: 40%" class="tMainBorder">
        <tr>
            <td class="cHeadTile" colspan="2">
                IP Limit Setting</td>
        </tr>
        <tr>
            <td style="width: 30%">
                <asp:Label ID="Label1" runat="server" Text="Level One:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtOne" runat="server" Width="60px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Level Two:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTwo" runat="server" Width="60px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Level Three:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtThree" runat="server" Width="60px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Level Four:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFour" runat="server" Width="60px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Level Five:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFive" runat="server" Width="60px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="saveButton" runat="server" Text="Save" Width="80px" 
                    onclick="saveButton_Click" />
            &nbsp;
                <asp:Button ID="resetButton" runat="server" onclick="resetButton_Click" 
                    Text="Reset" Width="80px" />
            </td>
        </tr>
        </table>
      
    <%--<table style="width: 400px; margin-top:5em" class="tMainBorder">
        <tr>
            <td colspan="2" class="cHeadTile">
            <asp:Label id="approvalLimitsLabel" runat="server" Text="Approval Limits" 
                Font-Bold="True" ForeColor="White"></asp:Label></td>
        </tr>
        <tr>
            <td style="width:130px">
                <asp:Label id="ApprovalLimitLabel" runat="server" Text="IP Limit One ($ mln):"></asp:Label>
                    &nbsp;</td>
            <td>
                <asp:TextBox id="Level1TextBox" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="ApprovalLimitLabel0" runat="server" Text="IP Limit Two ($ mln):"></asp:Label>
                    </td>
            <td>
                <asp:TextBox id="Level2TextBox" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="ApprovalLimitLabel1" runat="server" Text="IP Limit Three ($ mln):"></asp:Label>
                    </td>
            <td>
                <asp:TextBox id="Level3TextBox" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label id="ApprovalLimitLabel2" runat="server" Text="IP Limit Four ($ mln):"></asp:Label>
                    </td>
            <td>
                <asp:TextBox id="Level4TextBox" runat="server" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button id="submitButton" runat="server" Text="Submit" 
                    onclick="submitButton_Click" Width="100px" />
            &nbsp;
                    
                    
                <input id="backButton" type="button" value="Close" style="width:100px; height:20px"  
                    onclick="javascript:history.back()" /></td>
        </tr>
        <tr>
            <td colspan="2" class="cHeadTile">
                &nbsp;</td>
        </tr>
    </table>--%>
                     
    
</asp:Content>

