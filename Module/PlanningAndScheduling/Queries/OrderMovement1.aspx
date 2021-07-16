<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OrderMovement1.aspx.cs" Inherits="Module_PlanningAndScheduling_Queries_OrderMovement1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <%@ register assembly="obout_ComboBox" namespace="Obout.ComboBox" tagprefix="cc2" %>
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
        width: 150px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 340px;
    }
    .c6
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<table align="left" class=" td tContentArial" width="945px">
        <tr>
            <td class="td" colspan="8">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" OnClick="imgbtnPrint_Click1" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                ToolTip="Clear" OnClick="imgbtnClear_Click1" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click1" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                ToolTip="Help" Width="48" onclick="imgbtnHelp_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TableHeader td" colspan="8">
                <span class="titleheading"><strong>OREDR MOVEMENT REPORT</strong></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                Branch:
            </td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
           <%-- <td align="right">
                Year:
            </td>
            <td>
               
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="gCtrTxt " Font-Size="9" 
                    Width="160px" AutoPostBack="True" 
                    onselectedindexchanged="ddlYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>--%>
            <td align="right">
                From Date:
            </td>
            <td>
                <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                    OnTextChanged="TxtFromDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
            </td>
            <td align="right">
                To Date:
            </td>
            <td>
                <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                    OnTextChanged="TxtToDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
           <td align="right">
                Business Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlBusinessType" runat="server" DataTextField="BUSINESS_TYPE" DataValueField="BUSINESS_TYPE"
                    Width="160px" CssClass="gCtrTxt " Font-Size="8">
                    <asp:ListItem>JOB WORK</asp:ListItem>
                    <asp:ListItem>SALE WORK</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="tdRight">
                Product Type:
            </td>
            <td class="tdLeft">
                <asp:DropDownList ID="ddlProductType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" DataTextField="PRODUCT_TYPE" DataValueField="PRODUCT_TYPE" 
                    AutoPostBack="True" 
                    onselectedindexchanged="ddlProductType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            
            <td align="right">
                Order No:
            </td>
            <td>
                <asp:DropDownList ID="ddlOrderNo" runat="server" DataTextField="ORDER_NO" DataValueField="ORDER_NO"
                    Width="160px" CssClass="gCtrTxt" Font-Size="8">
                </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
            <td align="right">
                Department:
            </td>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px">
                </asp:DropDownList>
                
        <tr>
            <td colspan="8">
                <cc1:calendarextender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                    TargetControlID="TxtFromDate">
                </cc1:calendarextender>
                <cc1:calendarextender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                    TargetControlID="TxtToDate">
                </cc1:calendarextender>
            </td>
        </tr>
    </table>
</asp:Content>



