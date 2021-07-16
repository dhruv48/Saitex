<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyMaster_Opt.aspx.cs" Inherits="Admin_CompanyMaster_Opt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Company Master Option</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <asp:Panel ID="Panel1" runat="server" BackColor="#336799" Height="700px" Width="600px">
            <table  Width="590px" align="left"  class="tContentArial">
                <tr>
                    <td align="left"   width="98%" valign="top">
                        <table Width="590px" align="left" border="1">
                            <tr>
                                <td align="center" colspan="6" valign="top">
                                    <span style="font-size: 13pt"><strong>Company Master report</strong> </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    Company Code</td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCompCodeRpt" runat="server" Width="150px"></asp:TextBox></td>
                                <td align="left" valign="top">
                                    Comapny Name</td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtcompNameRpt" runat="server" Width="100px"></asp:TextBox></td>
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
