<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Vat_Retail_Price.ascx.cs"
    Inherits="Module_Inventory_Controls_Vat_Retail_Price" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<table class="td tContent" style="width: 724px">
    <tr>
        <td>
            <table align="left">
                <tr>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Height="41" Width="48" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Height="41" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Height="41" Width="48" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center">
            <span class="titleheading">VAT / Retail Invoice Report</span>
        </td>
    </tr>
    <tr>
        <td>
            <table style="width: 712px">
                <tr>
                    <td class="tdRight">
                        Invoice From :
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtInvoiceFrom" Width="100px" runat="server" MaxLength="10" CssClass="SmallFont TextBoxNo UpperCase"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        Invoice To :
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtInvoiceTo" Width="100px" runat="server" MaxLength="10" CssClass="SmallFont TextBoxNo UpperCase"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:TextBox ID="txt" runat="server" Width="1px" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Panel ID="pnlCheck" runat="server" BorderColor="White" BorderWidth="1px">
                            <asp:CheckBoxList ID="chkLstInvoiceType" runat="server" RepeatDirection="Horizontal"
                                RepeatColumns="4" Font-Size="X-Small">
                            </asp:CheckBoxList>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
