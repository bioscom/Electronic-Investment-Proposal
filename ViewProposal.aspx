<%@ Outputcache Location="None"%>
<%@ Page Language="C#" MasterPageFile="~/MasterPages/eIPMasterPage.master" EnableViewState="false" AutoEventWireup="True" CodeFile="ViewProposal.aspx.cs" Inherits="ViewProposal" Title="Investment Proposal" %>
<%--<%@ OutputCache Duration="5" VaryByParam="none" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <eip:IPDetailInfo ID="IPDetailInfo1" runat="server" />
    <table style="width:98%" class="tMainBorder">
        <tr>
            <td class="cHeadTile">Electronic Investment Proposal</td>
        </tr>
        <tr>
            <td style="text-align:center">
                
            </td> 
        </tr>
        <tr>
            <td>
                <iframe id="frame1" runat="server" style="width:100%; height:600px" enableviewstate="False"> </iframe>
            </td>
        </tr>
        <tr>
            <td class="cHeadTile">&nbsp;</td>
        </tr>
    </table>
</asp:Content>

