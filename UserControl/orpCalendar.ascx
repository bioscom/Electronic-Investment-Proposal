<%@ Control Language="C#" AutoEventWireup="True" CodeFile="orpCalendar.ascx.cs" Inherits="UserControl_orpCalendar" %>
   
    <input type="text" name="date1" id="sel1" size="15" runat="server" readonly="readonly"/>
    <input type="button" value="..." id="button1"  runat="server"/>
    <script type="text/javascript">
        var cal = new Zapatec.Calendar(
        {
            inputField: "<%=sel1.ClientID%>",
            ifFormat: "%d/%m/%Y",
            button: "<%=button1.ClientID%>", 
            theme: "aqua" 
        });
    </script>