<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Leave_OD_Detail.aspx.cs" Inherits="Module_Admin_Pages_Leave_OD_Detail" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
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
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 130px;
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
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table cellpadding="2" cellspacing="1"  border="0" align="left" width="100%" align="left" class="tContentArial">
    <tbody>
    <tr>
                            <td colspan="5" align="center"  valign="top" class="tRowColorAdmin">
                                <span class="titleheadingBold">Employee Records Search</span>
                            </td>
                        </tr>
    <tr>
        <td >Record From Table:</td>
        <td>
            <asp:DropDownList ID="DDLTableName" Width="150px" runat="server">
            </asp:DropDownList>
        </td>
        <td style="width:50px"></td>
        <td>Month:</td> 
        <td>
            <asp:DropDownList ID="DDLMonth" Width="150px"  runat="server">
            </asp:DropDownList>
         </td>
    </tr>
    <tr>
        <td>Employee:</td>
        <td>
            <asp:DropDownList ID="DDLEmployee" Width="150px" runat="server">
            </asp:DropDownList>
        </td>
        <td></td>
        <td>Department</td> 
        <td>
            <asp:DropDownList ID="DDLDepartment" Width="150px" runat="server">
            </asp:DropDownList>
         </td>
    </tr>
    <tr>
        <td>Branch:</td>
        <td>
            <asp:DropDownList ID="DDLBranch" Width="150px" runat="server">
            </asp:DropDownList>
        </td>
        <td></td>
        <td>Shift:</td> 
        <td>
            <asp:DropDownList ID="DDLShift" Width="150px" runat="server">
            </asp:DropDownList>
         </td>
    </tr>
    <tr>
        <td>From Date:</td>
        <td>
            <asp:TextBox ID="TxtFromDate" Width="146px" runat="server"></asp:TextBox>
            <cc1:CalendarExtender ID="CEFromDate"  runat="server" Format="dd/MM/yyyy" TargetControlID="TxtFromDate">
                            </cc1:CalendarExtender> 
        </td>
        <td></td>
        <td>To Date:</td> 
        <td>
            <asp:TextBox ID="TxtToDate" Width="146px" runat="server"></asp:TextBox>
            <cc1:CalendarExtender ID="CEToDate" runat="server" Format="dd/MM/yyyy" TargetControlID="TxtToDate">
                            </cc1:CalendarExtender> 
         </td>
    </tr>
    <tr><td></td><td style="text-align: right">
        <asp:Button ID="CmdSearch" runat="server" Text="Search" /></td><td colspan="2"><asp:Button ID="CmdCancel" runat="server" Text="Cancel" /></td><td></td></tr>
</tbody>
</table>
</asp:Content>

