<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FabricIndent_OPT_R.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Reports_FabricIndent_OPT_R" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="tContentArial">
    <tr>
        <td class="td">
            <table>
               <tr >
                <td id="tdPrint" runat="server" valign="top">
                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                        Width="48" Height="41" TabIndex="9" onclick="imgbtnPrint_Click" ></asp:ImageButton>
                </td>
                <td id="tdExit" runat="server" valign="top">
                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                        Width="48" Height="41" TabIndex="10" onclick="imgbtnExit_Click" ></asp:ImageButton>
                </td>
                <td id="tdHelp" runat="server" valign="top">
                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                        Width="48" Height="41" TabIndex="11"></asp:ImageButton>
                </td>
                </tr> 
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Fabric Indent Report</span>
        </td>
    </tr>
    
    <tr>
        <td class="td" align="center">From
           
           <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
            To           
             <asp:TextBox ID="txtTo" runat="server"> </asp:TextBox>       
                        </td>
                
    </tr>
  
</table>
    </div>
    </form>
</body>
</html>
