<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateSelector.ascx.cs" Inherits="UserControl_DateSelector" %>
<script language ="javascript" type ="text/javascript">
function validateDate(oDay,oMonth,oYear)
{
    //alert(document.getElementById(oDay));
    var xDay = oDay.value;
    if (xDay==31){
        var xMonth =oMonth.value;
        if (xMonth==2){
            window.alert ("Invalid Day for the Month");
        }
        else if (xMonth==4){
            window.alert ("Invalid Day for the Month");
        }
        else if (xMonth==6){
            window.alert ("Invalid Day for the Month");
        }
        else if (xMonth==9){
            window.alert ("Invalid Day for the Month");
        }
        else if (xMonth==11){
            window.alert ("Invalid Day for the Month");
        }
    }
    else if (xDay==30){
        var xMonth =oMonth.value;
        if (xMonth==2){
            window.alert ("Invalid Day for the Month");
        }
    }
    if (xDay==29){//check for feburary
        var xMonth =oMonth.value;
        if (xMonth==2){//check for leap year
            var xYear=oYear.value
            if ((xYear % 4) == 0){//probably leap year
                if ((xYear % 100) == 0){//end of century
                    if ((xYear % 400) != 0){//end of century
                        window.alert ("Invalid Day for the Month");
                    }
                }
            }
            else{//alert - not leap year
                window.alert ("Invalid Day for the Month");
            }
        }
    }
}
</script>
<asp:DropDownList ID="drpDay" runat="server" AutoPostBack="false" Width="40px" 
    Height="20px"></asp:DropDownList>
<asp:DropDownList ID="drpMonth" runat="server" AutoPostBack="false" 
    Width="40px" Height="20px"></asp:DropDownList>
<asp:DropDownList ID="drpYear" runat="server" AutoPostBack="false" Width="60px" 
    Height="20px"></asp:DropDownList>