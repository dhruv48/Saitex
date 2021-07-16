<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsDetail.aspx.cs" Inherits="Module_Admin_Pages_NewsDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
    <title>VALSON INDUSTRIES LIMITED</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
    <link rel="stylesheet" type="text/css" href="~/StyleSheet/CommonStyle.css" />
    <link rel="stylesheet" type="text/css" href="~/StyleSheet/cssdesign.css" />
    <link href="../StyleSheet/MailMenuCss.css" rel="stylesheet" type="text/css" />
    
    </head>

<script type="text/javascript">
function blinklink()
{
if (!document.getElementById('lblHeading').style.color)
	{
	document.getElementById('lblHeading').style.color="red"
	}
if (document.getElementById('lblHeading').style.color=="red")
	{
	document.getElementById('lblHeading').style.color="Yellow"
	}
else
	{
	document.getElementById('lblHeading').style.color="red"
	}
timer=setTimeout("blinklink()",200)
}

function stoptimer()
{
clearTimeout(timer)
}
</script>
<body onload="blinklink()" onunload="stoptimer()" style="background: #AFCAE4">
    <form id="form1" runat="server" >
    <div  >
     <table width="100%" class="tContentArial" >
     <tr align ="left" >        
      
     </tr>
     <tr>        
      <td align="center">           
     <asp:Label ID="lblHeading"  CssClass ="blinkytext" runat="server" Text="Label" 
              Font-Bold="True" BorderColor="#05239F"></asp:Label> </td>
     </tr>
     <tr>        
      <td align="center" > 
     <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>        
      <td align="center" > 
          Posted Date:<asp:Label ID="lblPostedDAte" runat="server" Text="Label"></asp:Label></td>
     </tr>
     <tr>        
      <td> 
          &nbsp;</td>
     </tr>
     </table>
    </div>
    </form>
</body>
</html>
