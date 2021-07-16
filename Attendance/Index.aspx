<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Attendance Form</title>
      <link rel="stylesheet" href="css/faary.css" type="text/css" />
      </head>
<body >
    <form id="form1" class="iform"  runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    <table width="100%" border="0px" align="center"  cellpadding="0" cellspacing="0" >
        <tr>
            <td style="text-align:left "><a href="../Default.aspx"><img src="Images/logo.jpg" alt="" border="0" width="120px" height="46px" /></a></td>
            <td style="text-align:right"><img src="Images/sainath.jpg" width="370px" height="44px" /></td>
            <td style="text-align:right"><img src="Images/atoz.jpg" width="84px" height="56px" /></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="4">
                 <object type="application/x-shockwave-flash" data="Images/banner.swf" width="1000px" height="220px">
			        <param name="movie" value="Images/banner.swf"><param name="quality" value="high"><embed src="../Images/banner.swf" quality="high" width="1000px" height="220px">
              </object>
            </td>
        </tr>
        <tr><td colspan="4" ></td></tr>
        <tr>
            <td style="text-align:center" colspan="3"><h1>Welcome to Employee Online Attendance System</h1></td>
        </tr>
        <tr>
            <td colspan="4" class="div7"></td>
        </tr>        
         <tr>
            <td colspan="4" style="height:15px"></td>
        </tr>
        <tr><td></td>
            <td id="TdLogin" runat="server">
                <table cellpadding="0" style="border-color:Black; height:80px;" width="80%"  cellspacing="0">
                            <tr>
                                <td align="center" colspan="2" >
                                    <asp:Label ID="LblStatus" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" ForeColor="Red" Style="text-align: left" Width="50%"></asp:Label></td>
                            </tr>                           
                            <tr>
                                <td style="font-size:medium; font-weight:bold ">Employee Code </td>
                                <td >
                                    <asp:TextBox  Cssclass="itext"   ValidationGroup="L"  ID="TxtEmpCode" runat="server" TabIndex="1"></asp:TextBox>
                               </td>
                            </tr>
                            <tr>
                                <td style="font-size:medium; font-weight:bold ">Password </td>
                                <td >
                                    <asp:TextBox  Cssclass="itext"  ValidationGroup="L"  ID="TxtPassword" 
                                        runat="server" TextMode="Password"  TabIndex="2" AutoPostBack="True" 
                                        ontextchanged="TxtPassword_TextChanged"></asp:TextBox></td>
                            </tr>                          
                                                     
                        </table>
            </td>
            <td colspan="2" valign="top" >
            <table  cellspacing="0">
            <tr><td style="text-align:center "><b><asp:Label ID="LblDate" Font-Size="Large" runat="server" Text=""></asp:Label></b></td></tr>
                <tr><td bgcolor="White">
                <img src="Images/dg8.gif" name="hr1" /><img src="Images/dg8.gif" name="hr2" />
                <img src="Images/dgc.gif" /><img src="Images/dg8.gif" name="mn1" />
                <img src="Images/dg8.gif" name="mn2" />
                <img src="Images/dgc.gif" />
                <img src="Images/dg8.gif" name="se1" />
                <img src="Images/dg8.gif" name="se2" />
                <img src="Images/dgpm.gif" name="ampm" /></td></tr>
                </table>
             </td>
        </tr>
         <tr>
            <td colspan="4" style="height:15px"></td>
        </tr>
         <tr>
            <td colspan="4" class="div7"></td>
        </tr>
         <tr>
            <td colspan="4" style="height:15px"></td>
        </tr>
        <tr><td></td>
            <td id="TdWelcome"  colspan="4" visible="false"  runat="server">
               <table>
                    <tr>
                        <td>Employee Code:</td>
                        <td><b><asp:Label ID="LblCode" runat="server" Text=""></asp:Label></b></td>
                         <td></td>
                        <td>Employee Name:</td>
                        <td><b><asp:Label ID="LblUserName" runat="server" Text=""></asp:Label></b></td>                       
                     </tr>
                     <tr>  
                        <td>Department:</td>
                        <td><b><asp:Label ID="LblDept" runat="server" Text=""></asp:Label></b></td> 
                         <td></td> 
                        <td>Shift Name:</td>
                        <td><b><asp:Label ID="LblShift" runat="server" Text=""></asp:Label></b></td>                     
                     </tr>
                      <tr>
                      <td>Shift InTime:</td>
                        <td><b><asp:Label ID="LblStartTime" runat="server" Text=""></asp:Label></b></td>
                        <td></td>
                        <td>Shift OutTime:</td>
                        <td><b><asp:Label ID="LblOutTime" runat="server" Text=""></asp:Label></b></td>                        
                                            
                     </tr>
                     <tr>
                        <td colspan="5">
                            Your Request has been sucessfully submited,Your Requeest Time is: <b><asp:Label ID="LblTime" runat="server" Text=""></asp:Label></b>
                        </td>
                     </tr>
                </table>                
           </td>
        </tr>         
       <tr>
            <td colspan="4" style="height:15px"></td>
        </tr>
        <tr>
            <td colspan="4" valign="bottom"  align="center" >
                <div class="div2">Designed by : <a href="http://www.jingleinfo.com/" target="_blank" class="copyright-text">Jingle Infosolutions Pvt. Ltd. </a></div>
            </td>
        </tr>
    </table> 
<script type="text/javascript">
dg0=new Image();dg0.src="Images/dg0.gif";
dg1=new Image();dg1.src="Images/dg1.gif";
dg2=new Image();dg2.src="Images/dg2.gif";
dg3=new Image();dg3.src="Images/dg3.gif";
dg4=new Image();dg4.src="Images/dg4.gif";
dg5=new Image();dg5.src="Images/dg5.gif";
dg6=new Image();dg6.src="Images/dg6.gif";
dg7=new Image();dg7.src="Images/dg7.gif";
dg8=new Image();dg8.src="Images/dg8.gif";
dg9=new Image();dg9.src="Images/dg9.gif";
dgam=new Image();dgam.src="Images/dgam.gif";
dgpm=new Image();dgpm.src="Images/dgpm.gif";

function dotime(){ 
theTime=setTimeout('dotime()',1000);
d = new Date();
hr= d.getHours()+100;
mn= d.getMinutes()+100;
se= d.getSeconds()+100;
if(hr==100){hr=112;am_pm='am';}
else if(hr<112){am_pm='am';}
else if(hr==112){am_pm='pm';}
else if(hr>112){am_pm='pm';hr=(hr-12);}
tot=''+hr+mn+se;
document.hr1.src = 'Images/dg'+tot.substring(1,2)+'.gif';
document.hr2.src = 'Images/dg'+tot.substring(2,3)+'.gif';
document.mn1.src = 'Images/dg'+tot.substring(4,5)+'.gif';
document.mn2.src = 'Images/dg'+tot.substring(5,6)+'.gif';
document.se1.src = 'Images/dg'+tot.substring(7,8)+'.gif';
document.se2.src = 'Images/dg'+tot.substring(8,9)+'.gif';
document.ampm.src= 'Images/dg'+am_pm+'.gif';
}
dotime();

</script>
 </ContentTemplate>
 <Triggers>
<asp:PostBackTrigger ControlID="TxtPassword" />
</Triggers>
</asp:UpdatePanel>
    </form>
</body>
</html>
