<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FabricIndentOPT.ascx.cs" Inherits="Module_Inventory_Controls_FabricIndentOPT" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 50px;
    }
    .c2
    {
        margin-left: 4px;
        width: 90px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
 .td
{
	border-style: ridge;
	border-bottom-width:.5px;
	border-color: #C1D3FB;
}
</style>
<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
                
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
           
            <cc1:OboutTextBox ID="txtFrom" runat="server"></cc1:OboutTextBox> 
            To           
             <cc1:OboutTextBox ID="txtTo" runat="server"></cc1:OboutTextBox>             
                        </td>
                
    </tr>
  
</table>