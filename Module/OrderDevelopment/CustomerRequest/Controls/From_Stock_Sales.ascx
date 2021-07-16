<%@ Control Language="C#" AutoEventWireup="true" CodeFile="From_Stock_Sales.ascx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Controls_From_Stock_Sales" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="tContentArial">
            <tr>
                <td class="td">
                    <table align="left">
                        <tbody>
                            <tr>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="ImageButton1" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                        ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="ImageButton2" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                        ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="ImageButton3" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                        ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="ImageButton4" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center" width="100%">
                    <span class="titleheading"><b>Print
                <asp:Label ID="Label1" runat="server"></asp:Label>
                    </b></span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td colspan="4" class="tdCenter">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="M1" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 12%;">Report Type :- 
                            </td>
                            <td align="left" style="width: 12%;">
                                <asp:DropDownList ID="ddlReportType" runat="server"
                                    class="SmallFont" Width="100px">
                                    <asp:ListItem Text="PRODUCTION" Value="PRODUCTION" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="From_Stock" Value="From_Stock"></asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td align="right" style="width: 12%;">FROM&nbsp;DATE :
                            </td>
                            <td align="left" style="width: 12%;">
                                <asp:TextBox ID="txtFROMDATE" runat="server" TabIndex="6" Width="95px" CssClass="SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 12%;">TO&nbsp;DATE :
                            </td>
                            <td align="left" style="width: 12%;">
                                <asp:TextBox ID="txtTODATE" runat="server" TabIndex="6" Width="95px" CssClass="SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFROMDATE" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtTODATE" Format="dd/MM/yyyy"
            PopupPosition="TopLeft">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="ME1" runat="server" Mask="99/99/9999" MaskType="Date"
            TargetControlID="txtFROMDATE" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="ME2" runat="server" Mask="99/99/9999" MaskType="Date"
            TargetControlID="txtTODATE" PromptCharacter="_">
        </cc1:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
