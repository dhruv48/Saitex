<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fiber_PO_Report.aspx.cs" Inherits="Module_Fiber_Pages_Fiber_PO_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table>
<tr>
<td>
<table align="left">
<tbody>
<tr>
<td>
<asp:ImageButton runat="server" ImageUrl="~/CommonImages/link_print.png" ID="imgbtnPrint" OnClick="imgbtnPrint_Click" ToolTip="Print" Height="41" Width="48" />
</td>

<td>
<asp:ImageButton runat="server" ImageUrl="~/CommonImages/link_exit.png" ID="imgbtnExit" OnClick="imgbtnExit_Click" ToolTip="Exit" Height="41" Width="48" />
</td>
<td>
<asp:ImageButton runat="server" ImageUrl="~/CommonImages/clear.png" ID="imgbtnClear" OnClick="imgbtnClear_Click" ToolTip="Clear" Height="41" Width="48" />
</td>
</tr>
</tbody>
</table>
</td>
</tr>
<tr>
<td class="TableHeader td" width="100px" align="center">
<span class"titleheading">
<b>Print&nbsp;Purchase&nbsp;Order&nbsp;<asp:Label ID="lblPoType" runat="server"></asp:Label>
</b>
</span>
</td>
</tr>
<tr>
<td>
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
</asp:Content>

