<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="WasteLedgerreport.aspx.cs" Inherits="Module_Waste_Reports_Waste_Ledger_report"
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
    <asp:UpdatePanel ID="uppnl" runat="server">
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
                <span class="titleheading"><strong>Waste Ledger Report </strong></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                Branch:
            </td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td class="tdRight">
                Year:
            </td>
            <td>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Width="165px">
                </asp:DropDownList>
            </td>
            <td class="tdRight">
                From date:
            </td>
            <td class="tdLeft">
                <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                    AutoPostBack="True" OnTextChanged="TxtFromDate_TextChanged"></asp:TextBox>
                    
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="TxtFromDate">
                        </cc1:MaskedEditExtender>
            </td>
            <td class="tdRight">
                To Date:
            </td>
            <td class="tdLeft">
                <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="150px"
                    runat="server" AutoPostBack="True" OnTextChanged="TxtToDate_TextChanged"></asp:TextBox>
                    
                    
                 <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="TxtToDate">
                        </cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td class="tdRight">
                Waste:
            </td>
            <td class="tdLeft">
                <cc2:ComboBox ID="txtICODE" runat="server" CssClass="SmallFont" Width="151px" EnableLoadOnDemand="True"
                    DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true"
                    OpenOnFocus="true" Visible="true" Height="200px" EmptyText="------------All----------"
                    OnLoadingItems="txtICODE_LoadingItems">
                    <HeaderTemplate>
                        <div class="header d1">
                             CODE</div>
                        <div class="header d2">
                             DESCRIPTION</div>
                        <div class="header d3">
                            CATEGORY</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item d1">
                            <%# Eval("ITEM_CODE")%></div>
                        <div class="item d2">
                            <%# Eval("ITEM_DESC") %></div>
                        <div class="item d3">
                            <%# Eval("CAT_CODE")%></div>
                    </ItemTemplate>
                    <FooterTemplate>
                        Displaying items
                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                        out of
                        <%# Container.ItemsCount %>.
                    </FooterTemplate>
                </cc2:ComboBox>
            </td>
            <td align="right">
                Category:
            </td>
            <td>
                <asp:DropDownList ID="ddlItemCate" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px">
                </asp:DropDownList>
            </td>
            <td align="right">
               <%-- Item Type:--%>
            </td>
            <td>
                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" Visible="false">
                </asp:DropDownList>
            </td>
            <td align="right">
                <%-- Department:--%>
            </td>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="TdBackVir" colspan="8">
                &nbsp;
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
