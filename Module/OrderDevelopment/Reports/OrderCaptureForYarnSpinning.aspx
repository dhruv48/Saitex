<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OrderCaptureForYarnSpinning.aspx.cs" Inherits="Module_OrderDevelopment_Reports_OrderCaptureForYarnSpinning" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
<style type="text/css">
        .style1
        {
            text-align: left;
            vertical-align: top;
            width: 205px;
        }
        .style2
        {
            text-align: right;
            vertical-align: top;
            width: 91px;
        }
        .style3
        {
            text-align: right;
            vertical-align: top;
            height: 31px;
        }
        .style4
        {
            text-align: left;
            vertical-align: top;
            width: 205px;
            height: 31px;
        }
        .style5
        {
            text-align: right;
            vertical-align: top;
            width: 91px;
            height: 31px;
        }
        .style6
        {
            text-align: left;
            vertical-align: top;
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

 <table class="tContentArial" width="100%">
        <tr>
            <td class="td">
                <table align="left">
                    <tbody>
                        <tr>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
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
            <td class="TableHeader td" align="center" width="100%">
                <span class="titleheading"><b>Print
                    <asp:Label ID="lblFormHeading" runat="server"></asp:Label></b></span>
            </td>
        </tr>
        <tr>
            <td class="td">
                <table width="100%">
                    <tr>
                        <td class="tdRight" width="45px">
                            Business&nbsp;Type
                        </td>
                        <td class="style1 tdLeft" width="98%" >
                            <cc3:OboutDropDownList ID="ddlBusinessType" runat="server" AutoPostBack="True" 
                                MenuWidth="200px" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
                            </cc3:OboutDropDownList>
                        </td>
                        <td class="style2 tdRight" width="45px" >
                            Order&nbsp;Category</td>
                        <td class="tdLeft" width="98%">
                        <cc3:OboutDropDownList ID="ddlOrderCategory" runat="server" AutoPostBack="True" 
                                MenuWidth="200px" 
                                OnSelectedIndexChanged="ddl_SelectedIndexChanged">
                            <asp:ListItem>DIRECT SALE</asp:ListItem>
                            <asp:ListItem>INHOUSE</asp:ListItem>
                        </cc3:OboutDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3 tdRight" width="45px">
                            Product&nbsp;Type</td>
                        <td class="style4 tdLeft" width="98px">
                        <cc3:OboutDropDownList ID="ddlProductType" runat="server" AutoPostBack="True" 
                                MenuWidth="200px" OnSelectedIndexChanged="ddl_SelectedIndexChanged">
                        </cc3:OboutDropDownList>
                        </td>
                        <td class="style5 tdRight" width="45px">
                            Order&nbsp;Type</td>
                        <td class="style6 tdLeft" width="98px">
                            <cc3:OboutDropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" 
                                MenuWidth="200px" 
                                OnSelectedIndexChanged="ddl_SelectedIndexChanged">
                            </cc3:OboutDropDownList>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="4"></td>
                    
                    </tr>
                    <tr>
                        <td colspan="4" class="tdCenter">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="M1" />
                        </td>
                    </tr>
                   
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

