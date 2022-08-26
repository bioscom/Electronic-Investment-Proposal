<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" AutoEventWireup="True" CodeFile="AuditTrail.aspx.cs" Inherits="IPAdministrator_AuditTrail" Title="Investment Proposal" %>

<asp:Content id="Content1" ContentPlaceHolderid="ContentPlaceHolder1" Runat="Server">  
    <table style="width: 98%;" class="tMainBorder">
    <tr class="cHeadTile">
        <td style="width: 15%">
                Audit Trail</td>
        <td style="width: 700px; text-align:center">
        &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList id="yearDropDownList" runat="server" AutoPostBack="True" 
                onselectedindexchanged="yearDropDownList_SelectedIndexChanged">
                <asp:ListItem Value="-1">--Select Year--</asp:ListItem>
            </asp:DropDownList>
	    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                ControlToValidate="yearDropDownList" ErrorMessage="Please select year" 
                Operator="NotEqual" Type="Integer" ValueToCompare="-1">*</asp:CompareValidator>	
        </td>
        <td>
            <asp:DropDownList id="proposalsDropDownList" runat="server" 
                onselectedindexchanged="proposalsDropDownList_SelectedIndexChanged" 
                AutoPostBack="True" Width="700px">
                <asp:ListItem Value="-1">--Select Proposal--</asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ControlToValidate="proposalsDropDownList" 
                ErrorMessage="Please select a proposal" Operator="NotEqual" Type="Integer" 
                ValueToCompare="-1">*</asp:CompareValidator>
        </td>
        <td>
                    <asp:Button ID="closeButton" runat="server" onclick="closeButton_Click" 
                        Text="Close" ValidationGroup="xxxx" />
        </td>
    </tr>
    </table>
    <table style="width: 98%" class="tMainBorder">
        <tr class="cHeadTile">
            <td>
                IP Number</td>
            <td>
                Project Initiator</td>
            <td>
                Date Initiated</td>
            <td>
                Date Forwarded</td>
            <td>
                Business Opportunity Manager</td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="projNumLabel" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="projInitLabel" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="dateInitLabel" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="dateForwardedLabel" runat="server"></asp:Label>
            </td>
            <td>
                <asp:Label ID="BOMLabel" runat="server"></asp:Label>
            </td>
        </tr>
    
        </table>
    <table style="width: 98%" class="tMainBorder">
        <tr>
            <td>
            <asp:GridView id="auditTrailGridView" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                onpageindexchanging="auditTrailGridView_PageIndexChanging" Width="100%" PageSize="50">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                           <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Approver/Support Function">
                        <ItemTemplate>
                            <asp:Label id="labelSupportFunction" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ROLES") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Responsible Approver/Support">
                        <ItemTemplate>
                            <asp:Label id="labelSupportPerson" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SUPPORTFULLNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label id="labelSupportStand" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "STAND") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Comment">
                        <ItemTemplate>
                            <asp:Label id="labelSupportComment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CCOMMENT") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="40%" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Date Reviewed">
                        <ItemTemplate>
                            <asp:Label id="labelDateReviewed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DDATE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
        
                </Columns>
            </asp:GridView>
            </td>
        </tr>
    </table>
   <br /><br />
   <br /><br />
   <br /><br />
</asp:Content>