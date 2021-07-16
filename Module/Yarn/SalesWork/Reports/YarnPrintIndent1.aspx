<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YarnPrintIndent1.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_YarnPrintIndent1" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
 <asp:Panel ID="pnlItemMst" runat="server">
        <table>
            <tr>
                <td class="td">
                    <table align="left"> 
                        <tr>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint"  runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Height="41" Width="48" onclick="imgbtnPrint_Click1"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Height="41" Width="48" onclick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnExit"  runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Height="41" Width="48" onclick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnHelp"  runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <span class="titleheading">Yarn Indent Report</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr id="trRange" runat="server">
                            <td>
                                From
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="server" CssClass="SmallFont TextBoxNo">0</asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtFrom"
                                    Display="Dynamic" ErrorMessage="Numeric value required" MaximumValue="9999999"
                                    MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrom"
                                    Display="Dynamic" ErrorMessage="Provede starting No." SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                            <td>
                                To
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server" CssClass="SmallFont TextBoxNo">10</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTo"
                                    Display="Dynamic" ErrorMessage="Provede ending No." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtTo"
                                    Display="Dynamic" ErrorMessage="Numeric value required" MaximumValue="9999999"
                                    MinimumValue="1" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

