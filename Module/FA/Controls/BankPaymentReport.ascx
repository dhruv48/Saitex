<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BankPaymentReport.ascx.cs"
    Inherits="Module_FA_Controls_BankPaymentReport" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
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
        width: 200px;
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
                    <b class="titleheading">Bank Payment Report</b>
                </td>
            </tr>
            <tr>
                <td>
                    Start Date :
                </td>
                <td>
                    <asp:TextBox ID="txtstartdate" runat="server" Width="80px"></asp:TextBox>
                </td>
                <td>
                    End Date :
                </td>
                <td>
                    <asp:TextBox ID="txtenddate" runat="server" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <table style="width: 412px">
                        <tr>
                            <td align="right">
                                Select Voucher No :
                            </td>
                            <td align="left">
                                <cc1:ComboBox ID="ddlVoucherNo" EmptyText="select Voucher No" runat="server" Width="150px"
                                    Height="250px" TabIndex="3" DataTextField="VCHR_NO" DataValueField="VCHR_NO"
                                    EnableLoadOnDemand="True" MenuWidth="400px" OnLoadingItems="ddlVoucherNo_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Voucher Type</div>
                                        <div class="header c3">
                                            Voucher No</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <%# Eval("VCHR_NAME")%></div>
                                        <div class="item c3">
                                            <%# Eval("VCHR_NO")%></div>
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
                                <asp:Button ID="btnprint" runat="server" Text="Print" Style="height: 26px" OnClick="btnprint_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtstartdate" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtenddate"
            Format="dd/MM/yyyy" PopupPosition="TopLeft">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtstartdate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtenddate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
<%--    </ContentTemplate>
</asp:UpdatePanel>
--%>