<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttendanceOPT.ascx.cs" Inherits="Module_HRMS_Controls_AttendanceOPT" %>

<script type="text/javascript">
function NewWindow() {
document.forms[0].target = "_blank";
}
</script>
<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
                
                <td id="tdPrint" runat="server" valign="top">
                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                        Width="48" Height="41" TabIndex="9" OnClientClick="NewWindow();" onclick="imgbtnPrint_Click" ></asp:ImageButton>
                </td>
                <td id="tdExit" runat="server" valign="top">
                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                        Width="48" Height="41" TabIndex="10" onclick="imgbtnExit_Click"></asp:ImageButton>
                </td>
                <td id="tdHelp" runat="server" valign="top">
                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                        Width="48" Height="41" TabIndex="11"></asp:ImageButton>
                </td>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Monthly Attendance Report</span>
        </td>
    </tr>
    
    <tr>
        <td class="td" align="center">
          <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
    <tr ><td >Select Month</td>
        <td><asp:DropDownList ID="DDLMonth" runat="server"></asp:DropDownList>
        </td>
        <td></td>
        <td>Select Year</td>
        <td><asp:DropDownList ID="DDLYear" runat="server"></asp:DropDownList></td>
        
    </tr>
</table>        
 </td>
                
    </tr>
  
</table>