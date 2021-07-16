<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Setting.ascx.cs" Inherits="Module_Mail_Controls_Setting" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>

<script language="javascript" type="text/javascript">

    function Calculation(val)
    { 
         var name=val;
       
         document.getElementById('ctl00_cphBody_txtAdvanceAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_txtAdvance').value)*(parseFloat(document.getElementById('ctl00_cphBody_txtFinalTotal').value)/100)).toFixed(3) ;
      }
           
    function SetFocus(ControlId)
    {    
        document.getElementById(ControlId).focus();       
    }
    function GetClick(ButtonId)
    {
        document.getElementById(ButtonId).click();
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
        margin-left: 2px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<table class="tContentArial" width="95%">
    <tr>
        <td class="td tdLeft" width="100%">
            <table align="left">
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="48" Height="41" ValidationGroup="M1" onclick="imgbtnSave_Click"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server"  visible="false">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" Width="48" Height="41" ValidationGroup="M1">
                        </asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server"  visible="false">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" ValidationGroup="M1" onclick="imgbtnDelete_Click"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server"  visible="false">
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server"  >
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server"  >
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server"  visible="false">
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" width="100%">
            <span class="titleheading"><b>Mail Account Setting</b></span>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <span class="Mode">You are in
                <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ValidationGroup="M1" />
        </td>
    </tr>
    
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
              <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblDispName" runat="server" Text="Display Name :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtDispName" CssClass="TextBox SmallFont" runat="server" Width="150px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblEmailAdd" runat="server" Text="Email Address :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtEmailAdd" CssClass="TextBox SmallFont" runat="server" Width="150px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revMail" runat="server" 
                            ControlToValidate="txtEmailAdd" ErrorMessage="Invelid Email Address" 
                            SetFocusOnError="True" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            ValidationGroup="M1"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtEmailAdd" ErrorMessage="Email Address required" 
                            SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblIncominServer" runat="server" Text="Incoming Server :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtIncomingServer" CssClass="TextBox SmallFont" runat="server" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtIncomingServer" ErrorMessage="Incoming Server Required" 
                            SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblOutgoingServer" runat="server" Text="Outgoing Server :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtOutgoingServer" CssClass="TextBox SmallFont" runat="server" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtOutgoingServer" ErrorMessage="Outgoing Server Required" 
                            SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblIncomingPort" runat="server" Text="Incoming Port :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtIncomingPort" CssClass="TextBox SmallFont" runat="server" Width="150px">995</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtIncomingPort" ErrorMessage="Incoming Port Required" 
                            SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblOutgoingPort" runat="server" Text="Outgoing Port :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtOutgoingPort" CssClass="TextBox SmallFont" runat="server" Width="150px">25</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="txtOutgoingPort" ErrorMessage="Outgoing Port Required" 
                            SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblIncomingSsl" runat="server" Text="Incoming Ssl :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:CheckBox ID="chkIncomingSsl" runat="server" />
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblOutgoingSsl" runat="server" Text="Outgoing Ssl :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:CheckBox ID="chkOutgoingSsl" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblUserName" runat="server" Text="User Name :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtUserName" CssClass="TextBox SmallFont" runat="server" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="txtUserName" ErrorMessage="User Name Required" 
                            SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblPassword" runat="server" Text="Password :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="TextBox SmallFont" runat="server" Width="150px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                            ControlToValidate="txtPassword" ErrorMessage="Password Required" 
                            SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblRememberPwd" runat="server" Text="Remember Password :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:CheckBox ID="chkRememberPwd" runat="server" />
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="lblDeleteFromServer" runat="server" Text="Delete From Server :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:CheckBox ID="chkDeleteFromServer" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
