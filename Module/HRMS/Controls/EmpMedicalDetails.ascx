<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpMedicalDetails.ascx.cs"
    Inherits="Module_HRMS_Controls_EmpMedicalDetails" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
 
  <script language="javascript" type="text/javascript">
    function checkNumeric(evt) {
    evt = (evt) ? evt : window.event
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
    {      
        return false
    }   
         return true
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
<table width="100%" class="tContentArial" cellspacing="0" cellpadding="0" align="left">
    <tr>
        <td class="td">
            <table width="10%" class="tContentArial" cellspacing="0" cellpadding="0" align="left">
                <tr >
                <td id="tdSave" align="left" width="48" runat="server">
                     <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="M1" Width="48" />
                </td>
                <td id="tdUpdate" align="left" width="48" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" Width="48" />
                </td>
                <td id="tdClear" runat="server" align="left" width="48">
                    <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                </td>
                <td id="tdPrint" runat="server" align="left" width="48">
                    <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />           
                </td> 
                <td id="tdHelp" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnHelp"  runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                </td>                   
                </tr>
            </table>
        </td>
    </tr>
    <tr class="TableHeader">
        <td align="center" valign="top" class="td">
            <span class="titleheading">Employee Medical Details </span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="M1" />
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table width="100%">
                <tr>
                    <td class="tContentArial" align="left">
                        Employee Name
                    </td>
                    <td align="left" >                       
                        <cc2:ComboBox ID="ddlfind" runat="server" DataTextField="F_NAME" DataValueField="EMP_CODE"
                            EnableLoadOnDemand="True" TabIndex="1" OnLoadingItems="ddlfind_LoadingItems"  Enabled="false"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlfind_SelectedIndexChanged1" AutoValidate="True"
                            EmptyText="Find" Width="204" Height="200px" AppendDataBoundItems="False">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code
                                </div>
                                <div class="header c2">
                                    Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("EMP_CODE")%></div>
                                <div class="item c2">
                                    <%# Eval("F_NAME")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tContentArial ">
                        Blood Group
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlbldgrp" runat="server" TabIndex="2" Width="204px" CssClass="TextBox" >
                            <asp:ListItem>------Select-----</asp:ListItem>
                            <asp:ListItem>O+</asp:ListItem>
                            <asp:ListItem>O-</asp:ListItem>
                            <asp:ListItem>A+</asp:ListItem>
                            <asp:ListItem>A-</asp:ListItem>
                            <asp:ListItem>B+</asp:ListItem>
                            <asp:ListItem>B-</asp:ListItem>
                            <asp:ListItem>AB+</asp:ListItem>
                            <asp:ListItem>AB-</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial ">
                        Height
                    </td>
                    <td>
                        <asp:TextBox ID="Txthgt" runat="server"   TabIndex="3" MaxLength="5" onKeyPress="return checkNumeric(event)" Width="200px"  CssClass="TextBox"></asp:TextBox>(CMS)
                        
                    </td>
                    <td class="tContentArial">
                        Weight
                    </td>
                    <td>
                        <asp:TextBox ID="txtwgt" runat="server" TabIndex="4"  MaxLength="5" onKeyPress="return checkNumeric(event)" Width="200px" CssClass="TextBox"></asp:TextBox>(KG)
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial ">
                        ESI
                    </td>
                    <td>
                        <asp:TextBox ID="txtesi" runat="server" TabIndex="5" Width="200px" CssClass="TextBox"> 
                        </asp:TextBox>
                    </td>
                    <td class="tContentArial ">
                        Birth Mark
                    </td>
                    <td>
                        <asp:TextBox ID="Txtbmark" runat="server" TabIndex="6" Width="200px" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table width="100%">
                <tr>
                    <td class="tContentArial " colspan="4">
                        <asp:CheckBox ID="chkphydis" runat="server" AutoPostBack="True" OnCheckedChanged="chkphydis_CheckedChanged" />
                        Physical Disability(If any)
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial" colspan="1">
                        <asp:RadioButtonList ID="RBPhysicalDis" RepeatColumns="1" Width="120px" RepeatDirection="Vertical"
                            runat="server" TabIndex="7">
                            <asp:ListItem id="option1" runat="server" Value="P">Partial Disable</asp:ListItem>
                            <asp:ListItem id="option2" runat="server" Value="D">Disable</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td colspan="1">
                        <asp:TextBox ID="Txtphydis" runat="server" TextMode=" MultiLine " TabIndex="8"   Width="200px" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="tContentArial ">
                        Remark On Health
                    </td>
                    <td>
                        <asp:TextBox ID="Txthelth" runat="server" TextMode=" MultiLine " TabIndex="9"  Width="200px" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table width="100%">
                <tr>
                    <td class="tContentArial  " colspan="4">
                        <asp:Label ID="Label1" runat="server" CssClass="tContentArial " Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Underline="True" Style="text-align: right"   Text="In Case of Emergency Contact "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial ">
                        Shri/Smt. :
                    </td>
                    <td class="tContentArial  " colspan="3">
                        <asp:TextBox ID="txtmr" runat="server" TabIndex="10" Width="200px" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial ">
                        Address:
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtadd" runat="server" TabIndex="11"  TextMode="MultiLine"   Width="200px" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial">
                        City
                    </td>
                    <td>
                        <asp:TextBox ID="txtcity" runat="server" TabIndex="12" Width="200px" CssClass="TextBox"></asp:TextBox>
                        &nbsp;&nbsp; &nbsp;
                    </td>
                    <td class="tContentArial">
                        State
                    </td>
                    <td class="tContentArial">
                        <asp:TextBox ID="txtstate" runat="server" TabIndex="13" Width="200px" CssClass="TextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial">
                        Country
                    </td>
                    <td>
                        <asp:TextBox ID="TxtContry" runat="server" TabIndex="15" Width="200px" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td class="tContentArial">
                        Pin
                    </td>
                    <td>
                        <asp:TextBox ID="txtpin" runat="server" TabIndex="14" onKeyPress="return checkNumeric(event)" Width="200px" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial">
                        Mob
                    </td>
                    <td>
                        <asp:TextBox ID="Txtmob" runat="server" onKeyPress="return checkNumeric(event)" TabIndex="16" Width="200px" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                    <td class="tContentArial ">
                        Tel
                    </td>
                    <td class="tContentArial">
                        <asp:TextBox ID="TxtTel" runat="server" onKeyPress="return checkNumeric(event)" TabIndex="17" Width="200px" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial">
                        Email Id
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Width="200px" CssClass="TextBox"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic" ErrorMessage="Pls Enter Valid Email_Id" SetFocusOnError="True"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="S1"></asp:RegularExpressionValidator>
                    </td>
                    <td class="tContentArial">
                        Fax
                    </td>
                    <td class="tContentArial">
                        <asp:TextBox ID="Txtfax" runat="server" onKeyPress="return checkNumeric(event)" Width="200px" CssClass="TextBoxNo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tContentArial" colspan="1">
                        Status
                    </td>
                    <td class="tContentArial" colspan="3">
                        <asp:CheckBox ID="chkActive" runat="server" TabIndex="19" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>