<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialIndentAPP_Opt.aspx.cs"
    Inherits="Inventory_MaterialIndentAPP_Opt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body bgcolor="#AFCAE4">
    <form id="form1" runat="server" >
        <div>
            <table class="tContentArial" align="left">
                <tr>
                    <td align="center" colspan="4">
                        <span style="font-size: 13pt"><strong>Material Approval report</strong> </span>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Indent Number(Optional)</td>
                    <td align="left">
                        <asp:TextBox ID="txtIndentRpt" Width="80px" runat="server"></asp:TextBox></td>
                </tr>
                <%-- <tr>
                            
                            <td>
                              Branch Code</td>
                            <td>
                                <asp:TextBox ID="txtBranchCodeRpt" runat="server" Width="100px"></asp:TextBox></td>
                            
                        </tr>--%>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Button ID="btnGetReport" runat="server" OnClick="btnGetReport_Click" Text="Get Report" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
