<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LedgerBook_OPT.ascx.cs"
    Inherits="Module_FA_Controls_LedgerBook_OPT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
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
        width: 125px;
    }
    .c3
    {
        margin-left: 4px;
        width: 350px;
    }
    .style1
    {
        width: 90px;
    }
    .style2
    {
        width: 131px;
    }
    .style3
    {
        width: 111px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
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
                    <b class="titleheading">Ledger Book Report</b>
                </td>
            </tr>
            <tr>
                <td>
                    Start Date :
                </td>
                <td>
                    <asp:TextBox ID="txtStartDT" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    End Date :
                </td>
                <td>
                    <asp:TextBox ID="txtEndDT" runat="server" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table style="width: 396px">
                        <tr>
                            <td align="right">
                                Select Ledger :
                            </td>
                            <td align="left">
                                <cc1:ComboBox ID="cmbLedger" EmptyText="Select Bank Ledger" runat="server" Width="200px"
                                    Height="250px" DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" EnableLoadOnDemand="True"
                                    MenuWidth="500px" TabIndex="10" OnLoadingItems="cmbLedger_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code
                                        </div>
                                        <div class="header c3">
                                            Ledger Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("LDGR_CODE")%></div>
                                        <div class="item c3">
                                            <%# Eval("LDGR_NAME")%></div>
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
                            <td colspan="2" align="center">
                                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtStartDT" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDT"
            Format="dd/MM/yyyy" PopupPosition="TopLeft">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtStartDT" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtEndDT" PromptCharacter="_">
        </cc4:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
