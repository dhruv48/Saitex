<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Trn_MSt_Opt.aspx.cs" Inherits="Inventory_Trn_MSt_Opt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:Panel ID="Panel1" runat="server"  Width="600px" align="left" class="tContentArial" 
                       BackColor ="#336799"  >
                <table Width="600" align="left" class="tContentArial">
            <tr>
                <td  align="left" Width="600" >
                    <table class="tContentArial" Width="600" align="left">
                        <tr>
                            <td align="center" colspan="6">
                                <span style="font-size: 13pt"><strong> Transaction Master report</strong> </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Master Code</td>
                            <td>
                                <asp:TextBox ID="txtMstCodeRpt" Width="150px" runat="server"></asp:TextBox></td>
                              
                               
                           
                        </tr>
                          <tr>
                            <td align="center" colspan="6">
                                <asp:Button ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" Text="Get Report" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
          
        </table>
       </asp:Panel> 
    </div>
    </form>
</body>
</html>