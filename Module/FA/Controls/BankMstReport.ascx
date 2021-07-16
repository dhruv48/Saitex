<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BankMstReport.ascx.cs"
    Inherits="Module_FA_Controls_BankMstReport" %>
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
        width: 40px;
    }
    .c2
    {
        margin-left: 4px;
        width: 170px;
    }
    .c3
    {
        margin-left: 4px;
        width: 170px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
    }
    .c5
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
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="td">
            <tr>
                <td class="td" colspan="2">
                    <table>
                        <tr>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td" colspan="2">
                    <span class="titleheading">Bank Master Report</span>
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
                <td class="td" align style="text-align: right">
                    Select Bank :
                </td>
                <td class="td">
                    <cc1:ComboBox ID="cmbBankCode" runat="server" CssClass="SmallFont" DataTextField="LGR_BANK_NAME"
                        DataValueField="LGR_BANK_CODE" EmptyText="Print All Banks " Width="207px" MenuWidth="500px"
                        Height="200px" TabIndex="1">
                        <HeaderTemplate>
                            <div class="header c1">
                                Code</div>
                            <div class="header c2">
                                Bank Name</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <asp:Literal runat="server" ID="Container1" Text='<%# Eval("LGR_BANK_CODE") %>' /></div>
                            <div class="item c2">
                                <asp:Literal runat="server" ID="Container2" Text='<%# Eval("LGR_BANK_NAME") %>' /></div>
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
    </ContentTemplate>
</asp:UpdatePanel>
