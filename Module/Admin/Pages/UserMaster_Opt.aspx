<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserMaster_Opt.aspx.cs" Inherits="Admin_UserMaster_Opt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width: 100px; height: 21px">
                    User_Code</td>
                <td style="width: 100px; height: 21px">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get_Report" /></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
