<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LedgerTree.ascx.cs" Inherits="Module_FA_Controls_LedgerTree" %>

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
            <tr id="trLedger" runat="server" class="td">
                <td>
                    Code :
                    <asp:Label ID="lblLedgerCode" runat="server"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp; Ledger Name :
                    <asp:Label ID="lblLedgerName" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <asp:TreeView ID="TreeView1" runat="server" Font-Size="Small" NodeWrap="True" ShowLines="True"
                        ToolTip="Click on " OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" CssClass="Label">
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
