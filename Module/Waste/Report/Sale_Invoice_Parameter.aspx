<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="Sale_Invoice_Parameter.aspx.cs" Inherits="Module_OrderDevelopment_Reports_Sale_Invoice_Parameter"
    Title="Untitled Page" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table class="tContentArial">
        <tr>
            <td class="td">
                <table align="left">
                    <tbody>
                        <tr>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
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
                    <asp:Label ID="lblFormHeading" runat="server"></asp:Label></b></span>
            </td>
        </tr>
        <tr>
            <td class="td">
                <table>
                    <tr>
                        <td class="tdRight">
                            <asp:Label ID="lblFrom0" runat="server" Text="Invoice From" CssClass="Label SmallFont"></asp:Label>
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="txtInvoiceFrom" CssClass="TextBoxNo SmallFont" runat="server" ValidationGroup="M1"></asp:TextBox>
                        </td>
                        <td class="tdRight">
                            <asp:Label ID="lblFrom1" runat="server" Text="Invoice To" CssClass="Label SmallFont"></asp:Label>
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="txtInvoiceTo" CssClass="TextBoxNo SmallFont" runat="server" ValidationGroup="M1"></asp:TextBox>
                            <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="True" MenuWidth="200px"
                                Width="98%" Visible="False" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                            </asp:DropDownList>
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
                    <tr>
                        <td colspan="4" class="tdCenter">
                            <asp:RequiredFieldValidator ID="rfvInvoiceFrom" runat="server" ControlToValidate="txtInvoiceFrom"
                                ErrorMessage="Invoice From Range required" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="rfvInvoiceTo" runat="server" ControlToValidate="txtInvoiceTo"
                                ErrorMessage="Invoice to range required" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="M1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
