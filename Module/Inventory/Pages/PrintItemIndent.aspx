<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintItemIndent.aspx.cs"
    Inherits="Inventory_PrintItemIndent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Indent Report</title>
    <link href="../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td class="td">
                <table align="left">
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
                </table>
            </td>
        </tr>
        <tr>
            <td class="TableHeader td" align="center">
                <span class="titleheading">Item Indent Report</span>
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
                            <asp:TextBox ID="txtFrom" runat="server" Width="100px">0</asp:TextBox>
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
                            <asp:TextBox ID="txtTo" runat="server" Width="100px">10</asp:TextBox>
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
    </form>
</body>
</html>
