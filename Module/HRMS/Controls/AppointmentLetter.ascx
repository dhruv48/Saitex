<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AppointmentLetter.ascx.cs"
    Inherits="Module_HRMS_Controls_AppointmentLetter" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
<tr>
        <td class="td">
            <table align="left">
                <tbody>
                    <tr> 
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" Height="41" Width="48" TabIndex="4"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center">
            <span class="titleheading">APPOINTMENT LETTER</span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table class="tContentArial" cellspacing="0" cellpadding="0" border="1">
                <tr>
                    <td id="ValidationSummary2" align="center" runat="server" valign="top" visible="false"
                        colspan="2">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="M1"
                            ShowMessageBox="True" ShowSummary="True"></asp:ValidationSummary>
                    </td>
                </tr>
                 <tr>
                    <td align="right" valign="top">
                        Employee Name :
                    </td>
                    <td align="left" valign="top">
                        <cc2:ComboBox ID="cmbEmpName" runat="server" EnableLoadOnDemand="true"
                            Width="150px" MenuWidth="350px" Height="200px" CssClass="SmallFont" TabIndex="3"
                            EmptyText="Select Employee" DataValueField="EMP_CODE" DataTextField="F_NAME" 
                            onloadingitems="cmbEmpName_LoadingItems" AutoPostBack="True" 
                            onselectedindexchanged="cmbEmpName_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    EMP_CODE</div>
                                <div class="header c2">
                                    F_NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("EMP_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("F_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                            ErrorMessage="Please Select Employee" ControlToValidate="cmbEmpName" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Ref No :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtRef" runat="server" ReadOnly="true"  MaxLength="50" Width="150px" 
                            CssClass="gCtrTxt UpperCase" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                            ErrorMessage="Please enter the Offer Ref no" ControlToValidate="txtRef" ValidationGroup="M1"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Date :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtDate" runat="server" CssClass="gCtrTxt UpperCase" Width="150px"
                            TabIndex="2" MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtDate" runat="server">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                            ErrorMessage="Please enter the Offer Date" ControlToValidate="txtDate" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
               
            </table>
        </td>
    </tr>
</table>
<br />
<br />
<br />
<br />
<br />
<br />
<br />
</ContentTemplate>
</asp:UpdatePanel>
