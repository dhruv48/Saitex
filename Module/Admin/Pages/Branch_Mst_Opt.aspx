<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Branch_Mst_Opt.aspx.cs" Inherits="Admin_Branch_Mst_Opt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Branch Master Option</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <asp:Panel ID="Panel1" runat="server" BackColor="#336799" Height="600px" Width="700px">
            <table width="600"  align="left" border ="1" class ="tContentArial" >
                <tr>
                    <td align="center"  width="98%">
                        <table width="600" align="left" border ="1" class ="tContentArial">
                            <tr>
                                <td align="center" colspan="6">
                                    <span style="font-size: 13pt"><strong>Branch Master report</strong> </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Branch Code</td>
                                <td align="left">
                                    <asp:TextBox ID="txtBranchCodeRpt" runat="server" Width="150px"></asp:TextBox></td>
                                <td align="left">
                                    Branch Name</td>
                                <td align="left">
                                    <asp:TextBox ID="txtBranchNameRpt" runat="server" Width="150px"></asp:TextBox></td>
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
