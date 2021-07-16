<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dept_Mst_Opt.aspx.cs" Inherits="Admin_Dept_Mst_Opt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Department Master Option</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <asp:Panel ID="Panel1" runat="server" BackColor="#336799" Height="700px" Width="600px">
            <table width="590" align="left" >
                <tr>
                    <td >
                        <table width="590" align="left">
                            <tr>
                                <td align="center" colspan="6">
                                    <span style="font-size: 13pt"><strong>Department Master report</strong> </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Department Code</td>
                                <td align="left">
                                    <asp:TextBox ID="txtDeptCodeRpt" runat="server" Width="150px"></asp:TextBox></td>
                                <td align="left">
                                    Department Name</td>
                                <td align="left">
                                    <asp:TextBox ID="txtDeptNameRpt" runat="server" Width="150px"></asp:TextBox></td>
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
