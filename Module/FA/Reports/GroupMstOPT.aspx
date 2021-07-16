<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="GroupMstOPT.aspx.cs" Inherits="Module_FA_Reports_GroupMstOPT" Title="Untitled Page" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
            width: 100px;
        }
        .c2
        {
            margin-left: 4px;
            width: 250px;
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
                <b class="titleheading">Group Master Report</b>
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
            <td class="tdLeft td" colspan="4">
                <span class="Mode">
                    <asp:Label ID="lblMode" runat="server"></asp:Label>
                </span>
            </td>
        </tr>
        <tr>
            <td>
                Start Group Code :
            </td>
            <td>
                <cc2:ComboBox ID="cmbGroupCodeStart" runat="server" AutoPostBack="True" CssClass="smallfont"
                    DataTextField="GRP_CODE" DataValueField="GRP_CODE" EnableLoadOnDemand="True"
                    Width="118px" MenuWidth="400" EnableVirtualScrolling="true" OpenOnFocus="true"
                    TabIndex="4" Visible="true" Height="200px" EmptyText="Select Ledger" OnLoadingItems="cmbGroupCodeStart_LoadingItems"
                    OnSelectedIndexChanged="cmbGroupCodeStart_SelectedIndexChanged">
                    <HeaderTemplate>
                        <div class="header c1">
                            Code</div>
                        <div class="header c2">
                            Group Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item c1">
                            <%# Eval("GRP_CODE")%></div>
                        <div class="item c2">
                            <%# Eval("GRP_NAME")%></div>
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
                End Group Code :
            </td>
            <td>
                <cc2:ComboBox ID="cmbGroupCodeEnd" runat="server" AutoPostBack="True" CssClass="smallfont"
                    DataTextField="GRP_CODE" DataValueField="GRP_CODE" EnableLoadOnDemand="True"
                    MenuWidth="400" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="5"
                    Width="118px" Visible="true" Height="200px" EmptyText="Select Ledger" OnLoadingItems="cmbGroupCodeEnd_LoadingItems"
                    OnSelectedIndexChanged="cmbGroupCodeEnd_SelectedIndexChanged">
                    <HeaderTemplate>
                        <div class="header c1">
                            Code</div>
                        <div class="header c2">
                            Group Name</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item c1">
                            <%# Eval("GRP_CODE")%></div>
                        <div class="item c2">
                            <%# Eval("GRP_NAME")%></div>
                    </ItemTemplate>
                    <FooterTemplate>
                        Displaying items
                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                        out of
                        <%# Container.ItemsCount %>.
                    </FooterTemplate>
                </cc2:ComboBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:Button ID="btnPrint" runat="server" Text="Print Group" OnClick="btnPrint_Click" />
            </td>
        </tr>
    </table>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
