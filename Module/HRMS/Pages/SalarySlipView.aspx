<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="SalarySlipView.aspx.cs" Inherits="Module_HRMS_Pages_SalarySlipView" Title="Salary Slip" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
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
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 180px;
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
</style>
<script type="text/javascript">
function NewWindow() {
document.forms[0].target = "_blank";
}
</script>
<table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
    <tr ><td >Select Month</td>
        <td><asp:DropDownList ID="DDLMonth" runat="server"></asp:DropDownList>
        </td>
        <td></td>
        <td>Select Year</td>
        <td><asp:DropDownList ID="DDLYear" runat="server"></asp:DropDownList></td>
        
    </tr>
    <tr><td colspan="5" align="center"><asp:Button ID="CmdDisplay" runat="server" OnClientClick="NewWindow();" Text="Display" onclick="CmdDisplay_Click" /></td></tr>
</table>
</asp:Content>

