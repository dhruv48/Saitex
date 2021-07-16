<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="LedgerMstOPT.aspx.cs" Inherits="Module_FA_Reports_LedgerMstOPT" Title="Untitled Page" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
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
            width: 70px;
        }
        .c5
        {
            margin-left: 4px;
            width: 300px;
        }
        .c6
        {
            margin-left: 4px;
            width: 200px;
        }
    </style>
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>--%>
    <table class="td tContentArial">
        <tr>
            <td class="td" colspan="4">
                <table>
                    <tr>
                        <td id="tdClear" runat="server">
                            <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                        </td>
                        <td id="tdHelp" runat="server">
                            <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                        </td>
                        <td id="tdExit" runat="server">
                            <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="TableHeader td" align="center" colspan="4">
                <b class="titleheading">Ledger Master Report</b>
            </td>
        </tr>
        <tr>
            <td class="tdLeft td" align="center" colspan="4">
                <span class="Mode">
                    <asp:Label ID="lblMode" runat="server"></asp:Label>
                </span>
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" colspan="4">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="false" ValidationGroup="M1" />
                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                </strong>
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                </strong>
            </td>
        </tr>
        <tr>
            <td class="tdRight">
                Start Ledger Code :
            </td>
            <td class="tdLeft">
                <cc1:ComboBox ID="cmbLedgerCodeStart" runat="server" AutoPostBack="True" CssClass="smallfont"
                    DataTextField="LDGR_CODE" DataValueField="LDGR_CODE" EnableLoadOnDemand="True"
                    Width="118px" MenuWidth="660" EnableVirtualScrolling="true" OpenOnFocus="true"
                    TabIndex="4" Visible="true" Height="200px" OnLoadingItems="cmbLedgerCodeStart_LoadingItems"
                    OnSelectedIndexChanged="cmbLedgerCodeStart_SelectedIndexChanged" EmptyText="Select Ledger">
                    <HeaderTemplate>
                        <div class="header c4">
                            Code</div>
                        <div class="header c5">
                            Ledger Name</div>
                        <div class="header c6">
                            Group Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item c4">
                            <%# Eval("LDGR_CODE")%></div>
                        <div class="item c5">
                            <%# Eval("LDGR_NAME")%></div>
                        <div class="item c6">
                            <%# Eval("GRP_NAME")%></div>
                    </ItemTemplate>
                    <FooterTemplate>
                        Displaying items
                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                        out of
                        <%# Container.ItemsCount %>.
                    </FooterTemplate>
                </cc1:ComboBox>
            </td>
            <td class="tdRight">
                End Ledger Code :
            </td>
            <td class="tdLeft">
                <cc1:ComboBox ID="cmbLedgerCodeEnd" runat="server" AutoPostBack="True" CssClass="smallfont"
                    DataTextField="LDGR_CODE" DataValueField="LDGR_CODE" EnableLoadOnDemand="True"
                    MenuWidth="660" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="5"
                    Width="118px" Visible="true" Height="200px" EmptyText="Select Ledger" OnLoadingItems="cmbLedgerCodeEnd_LoadingItems"
                    OnSelectedIndexChanged="cmbLedgerCodeEnd_SelectedIndexChanged">
                    <HeaderTemplate>
                        <div class="header c4">
                            Code</div>
                        <div class="header c5">
                            Ledger Name</div>
                        <div class="header c6">
                            Group Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item c4">
                            <%# Eval("LDGR_CODE")%></div>
                        <div class="item c5">
                            <%# Eval("LDGR_NAME")%></div>
                        <div class="item c6">
                            <%# Eval("GRP_NAME")%></div>
                    </ItemTemplate>
                    <FooterTemplate>
                        Displaying items
                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                        out of
                        <%# Container.ItemsCount %>.
                    </FooterTemplate>
                </cc1:ComboBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnprint" runat="server" Text="Print" OnClick="btnprint_Click" Width="100px" />
            </td>
        </tr>
    </table>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
