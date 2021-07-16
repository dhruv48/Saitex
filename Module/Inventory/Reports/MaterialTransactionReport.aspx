<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="MaterialTransactionReport.aspx.cs" Inherits="Module_Inventory_Reports_MaterialTransactionReport"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <style type="text/css">
        .item
        {
            position: relative !important;
            display: -moz-inline-stack;
        }
        .header
        {
            margin-left: 4px;
        }
        .c1
        {
            width: 80px;
        }
        .c4
        {
            margin-left: 4px;
            width: 300px;
        }
        .d1
        {
            width: 150px;
        }
        .d2
        {
            margin-left: 4px;
            width: 200px;
        }
        .d3
        {
            width: 100px;
        }
    </style>
    <asp:UpdatePanel ID="upnl" runat="server">
   <ContentTemplate>  
    <table align="left" class=" td tContentArial" width="945px">
        <tr>
            <td class="td" colspan="8">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                OnClick="imgbtnPrint_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                OnClick="imgbtnClear_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
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
                <span class="titleheading"><strong>Material Transaction Report </strong></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                Branch:
            </td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="tdRight">
                Year:
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                    Font-Size="8" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="160px">
                </asp:DropDownList>
            </td>
            <td class="tdRight">
                From date:
            </td>
            <td class="tdLeft">
                <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                    OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
            </td>
            <td class="tdRight">
                To Date:
            </td>
            <td class="tdLeft">
                <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="150px"
                    runat="server" OnTextChanged="TxtToDate_TextChanged1" AutoPostBack="True"></asp:TextBox>
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
            <td align="right">
                Item Category:
            </td>
            <td>
                <asp:DropDownList ID="ddlItemCate" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px">
                </asp:DropDownList>
            </td>
            <td align="right">
                Item Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px">
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
            </td>
            <td align="right">
                Location:
            </td>
            <td>
                <asp:DropDownList ID="ddllocation" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px">
                </asp:DropDownList>
            </td>
            <td align="right">
                Store:
            </td>
            <td>
                <asp:DropDownList ID="ddlstore" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px">
                </asp:DropDownList>
            </td>
            <td class="tdRight">
                Item:
            </td>
            <td class="tdLeft">
                <cc2:ComboBox ID="txtICODE" runat="server" CssClass="SmallFont" Width="151px" EnableLoadOnDemand="True"
                    DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true"
                    OpenOnFocus="true" Visible="true" Height="200px" EmptyText="------------All----------"
                    OnLoadingItems="txtICODE_LoadingItems">
                    <HeaderTemplate>
                        <div class="header d1">
                            ITEM CODE</div>
                        <div class="header d2">
                            ITEM DESCRIPTION</div>
                        <div class="header d3">
                            TYPE</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item d1">
                            <%# Eval("ITEM_CODE")%></div>
                        <div class="item d2">
                            <%# Eval("ITEM_DESC") %></div>
                        <div class="item d3">
                            <%# Eval("ITEM_TYPE")%></div>
                    </ItemTemplate>
                    <FooterTemplate>
                        Displaying items
                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                        out of
                        <%# Container.ItemsCount %>.
                    </FooterTemplate>
                </cc2:ComboBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="TdBackVir" colspan="8">
                <b>
                    <%--Total Records : --%>&nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"
                        Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td tContentArial" colspan="8">
                <asp:Panel ID="pnlShowHover" runat="server" Width="945px" ScrollBars="Auto" Height="350px">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="8">
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate"
                    PopupPosition="TopLeft" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate"
                    PopupPosition="TopLeft" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
    </table>
    
</ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
