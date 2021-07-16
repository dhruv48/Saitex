<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricTransactionReport.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Reports_FabricTransactionReport" %>

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
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        margin-left: 4px;
        width: 300px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
    .c6
    {
        margin-left: 4px;
        width: 80px;
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
                                ToolTip="Help" Width="48" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TableHeader td" colspan="8">
                <span class="titleheading"><strong> FABRIC TRANSACTION REPORT</strong></span>
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
            <td align="right">
                Year:
            </td>
            <td>
                <%--<asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px">--%>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="gCtrTxt " Font-Size="9" 
                    Width="160px" onselectedindexchanged="ddlYear_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
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
                Trn Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlTrnType" runat="server" DataTextField="TRN_TYPE" DataValueField="TRN_TYPE"
                    Width="160px" CssClass="gCtrTxt " Font-Size="8">
                </asp:DropDownList>
            </td>
           <%--<td class="tdRight">
             Fabric Category:
            </td>
            <td class="tdLeft">
                <asp:DropDownList ID="ddlYarnCate" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" DataTextField="YARN_CAT" DataValueField="YARN_CAT" 
                    onselectedindexchanged="ddlYarnCate_SelectedIndexChanged">
                </asp:DropDownList>
            </td>--%>
            <td class="tdRight">
                Fabric Type:
            </td>
            <td class="tdLeft">
                <asp:DropDownList ID="ddlFabrType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" onselectedindexchanged="ddlYarnType_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right">
                Party :
            </td>
            <td>
                <asp:DropDownList ID="ddlPartycode" runat="server" DataTextField="PRTY_CODE" DataValueField="PRTY_CODE"
                    Width="160px" CssClass="gCtrTxt" Font-Size="8">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Department:
            </td>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px">
                </asp:DropDownList>
                <td class="tdRight">
                    Fabric :
                </td>
                <td class="tdLeft">
                    <cc2:ComboBox ID="ddlYarn" runat="server" CssClass="SmallFont" EmptyText="------------All----------"
                        EnableLoadOnDemand="True" Height="200px" MenuWidth="800px" OnLoadingItems="ddlYarn_LoadingItems"
                        Width="161px">
                        <HeaderTemplate>
                            <div class="header c2">
                               Fabric Code</div>
                            <div class="header c4">
                                Description</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c2">
                                <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_CODE") %>' />
                            </div>
                            <div class="item c4">
                                <asp:Literal ID="Container3" runat="server" Text='<%# Eval("YARN_DESC") %>' />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                </td>
            </td>
            <td>
            </td>
            <td align="left" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
          
        </tr>
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

