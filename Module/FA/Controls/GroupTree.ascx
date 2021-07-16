<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupTree.ascx.cs" Inherits="Module_FA_Controls_GroupTree" %>
<link href="~/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript">
    function GetRowValue(val,TextBoxId)
    {           
        window.opener.document.getElementById(TextBoxId).value=val;   
        window.opener.document.forms[0].submit();      
        window.close();
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table bgcolor="#afcae4">
            <tr id="trGroup" runat="server" class="td">
                <td>
                    Code :
                    <asp:Label ID="lblGroupCode" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; Group Name :
                    <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <asp:TreeView ID="TreeView1" runat="server" NodeWrap="True" ShowLines="True" Font-Size="X-Small"
                        ToolTip="Click on " OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" CssClass="Label">
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
