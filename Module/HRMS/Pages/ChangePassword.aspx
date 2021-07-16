<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Module_HRMS_Pages_ChangePassword" Title="Change Password" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
<script type="text/javascript" language="javascript">
function clearTextBox()
{
   document.getElementById('<%=TxtOldPassword.ClientID%>').value = "";
    document.getElementById('<%=TxtNewPassword.ClientID%>').value = "";
}
function Check_Valid()
{
if(document.getElementById('<%=TxtOldPassword.ClientID%>').value == "")
{
    alert('Please enter the old password');
    document.getElementById('<%=TxtOldPassword.ClientID%>').focus();
    return false ;
}
if(document.getElementById('<%=TxtNewPassword.ClientID%>').value == "")
{
    alert('Please enter the new password');
    document.getElementById('<%=TxtNewPassword.ClientID%>').focus();
    return false ;
}
}
</script>
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
        width: 40px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
     .c3
    {
        width: 50px;
    }
    .c4
    {
        margin-left: 4px;
        width: 100px;
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
<table align="left" width="100%" class="tContentArial">
     <tr>
                                <td align="left"  class="td" colspan="3">
                                    <table class="tContentArial" cellspacing="0" cellpadding="0" border="1">
                                        <tbody>
                                            <tr>                                                                                          
                                                <td id="tdUpdate" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnUpdate"  runat="server" ValidationGroup="M1"
                                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" 
                                                        Width="48" OnClientClick="return Check_Valid()" onclick="imgbtnUpdate_Click">
                                                    </asp:ImageButton>
                                                </td>                                              
                                                <td id="tdClear" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnClear"  runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                        ToolTip="Clear" Height="41" Width="48" OnClientClick="clearTextBox()" ></asp:ImageButton>
                                                </td>                                               
                                                <td id="tdExit" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnExit"  runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                        ToolTip="Exit" Height="41" Width="48" onclick="imgbtnExit_Click"></asp:ImageButton>
                                                </td>
                                                <td id="tdHelp" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr >
                                <td class="TableHeader td" align="center" colspan="3">
                                    <b class="titleheading">Employee Password Change Form</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" valign="top" align="left" colspan="3">
                                    <span class="Mode">You are in
                                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                                </td>
                            </tr>
    <tr>
        <td>User Name:</td><td colspan="2"><asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox></td>
    </tr>
     <tr>
        <td>Old Password:</td><td colspan="2"><asp:TextBox ID="TxtOldPassword" TextMode="Password" runat="server"></asp:TextBox></td>
    </tr>
     <tr>
        <td>New Password:</td><td colspan="2"><asp:TextBox ID="TxtNewPassword" TextMode="Password" runat="server"></asp:TextBox></td>
    </tr>
</table>
</asp:Content>

