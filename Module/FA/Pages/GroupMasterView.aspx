<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GroupMasterView.aspx.cs"
    Inherits="Module_FA_Pages_GroupMasterView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Master Hierarchy</title>
    <link href="~/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    function CloseMe()
    {           
        window.close();
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table bgcolor="#afcae4">
            <tr id="trGroup" runat="server" class="td">
                <td>
                    <b>Group Master</b>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <asp:TreeView ID="TreeView1" runat="server" Font-Size="X-Small" NodeWrap="True" ShowLines="True"
                        ToolTip="Click On">
                    </asp:TreeView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnClose" runat="server" Text="Close Window" OnClientClick="CloseMe();" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
