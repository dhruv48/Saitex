<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Grp_Mst_Opt.aspx.cs" Inherits="Admin_Grp_Mst_Opt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Group Master Option</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server" BackColor="#336799" Height="1000px" Width="1000px">
            <table width="100%">
                <tr>
                    <td align="center" style="height: 130px" width="98%">
                        <table>
                            <tr>
                                <td align="center" colspan="6">
                                    <span style="font-size: 13pt"><strong>Company Master report</strong> </span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Company Code</td>
                                <td>
                                    <asp:TextBox ID="txtCompCodeRpt" runat="server" Width="100px"></asp:TextBox></td>
                                <td>
                                    Comapny Name</td>
                                <td>
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
