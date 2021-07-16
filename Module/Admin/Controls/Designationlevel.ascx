<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Designationlevel.ascx.cs"
    Inherits="Module_Admin_Controls_Designationlevel" %>

<script type="text/javascript">
function CallPrint(strid)
{
   var  partContent = document.getElementById(strid);
   var  WinPrint = window.open("", "mywindow","location=0,status=0,scrollbars=1,width=800,height=600");
        WinPrint.document.write(partContent.innerHTML);
        WinPrintdocument.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();
        prtContent.innerHTML=strOldOne;
}
</script>

<table>
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td id="tdPrint" runat="server" onclick="CallPrint('divPrint')">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" />
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" OnClick="imgbtnHelp_Click" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="TableHeader">
        <td align="center" valign="top" class="td">
            <span class="titleheading">DesigNation Level</span></td>
    </tr>
    <tr>
        <td class="td">
            <div id="divPrint">
                <asp:TreeView ID="TreeView1" runat="server" NodeWrap="True" ShowLines="True"
                    ToolTip="Click on ">
                </asp:TreeView>
            </div>
        </td>
    </tr>
</table>
