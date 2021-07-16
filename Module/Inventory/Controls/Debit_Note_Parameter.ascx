<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Debit_Note_Parameter.ascx.cs" Inherits="Module_Inventory_Controls_Debit_Note_Parameter" %>
<asp:UpdatePanel ID="uppnl" runat="server">
<ContentTemplate>
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
            <span class="titleheading"><b>Print&nbsp;
                <asp:Label ID="lblPOTYPE" runat="server"></asp:Label></b></span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td class="tdRight">
                        <asp:Label ID="lblFrom" runat="server" Text="From" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtFrom" CssClass="TextBoxNo SmallFont" runat="server" ValidationGroup="M1"></asp:TextBox>
                        <asp:RangeValidator ID="RvFrom" runat="server" ControlToValidate="txtFrom" Display="Dynamic"
                            ErrorMessage="Invalid Number" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                            ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="lblTo" runat="server" Text="To" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtTo" CssClass="TextBoxNo SmallFont" runat="server" ValidationGroup="M1"></asp:TextBox>
                        <asp:RangeValidator ID="RvTo" runat="server" ControlToValidate="txtTo" Display="Dynamic"
                            ErrorMessage="Invalid Number" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                            ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="tdCenter">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="M1" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>