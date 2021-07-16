<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorMaster_opt.aspx.cs" Inherits="Inventory_VendorMaster_opt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="PnRpt" runat="server"  Width="600px" 
                       BackColor ="#336799" align="left"  >
                <table Width="600"  align="left" class="tContentArial">
            <tr>
                <td Width="600" class="tContentArial" align="center" >
                    <table Width="600"  align="left" class="tContentArial">
                        <tr>
                            <td align="center" colspan="6">
                                <span style="font-size: 13pt"><strong>Vendor  Master report</strong> </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Vendor Code</td>
                            <td>
                                <asp:TextBox ID="txtVendCodeRpt" Width="150px" runat="server"></asp:TextBox></td>
                            
                            
                            <td>
                                Vendor Name</td>
                            <td>
                                <asp:TextBox ID="txtVendNameRpt" runat="server" Width="150px"></asp:TextBox></td>
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
