<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="PrintPackingSlip.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_PrintPackingSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
 <asp:UpdatePanel ID="upnl" runat="server">
   <ContentTemplate>  
    <asp:Panel ID="pnlItemMst" runat="server">
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
                    <span class="titleheading">Packing Slip Report</span>
                     <asp:Label ID="lblTRNType" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr id="trRange" runat="server">
                         <td class="tdRight" id="tdtype" runat="server">
                        <asp:Label ID="Label1" runat="server" Text="Report Type" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" id="tdtypeD" runat="server">
                        <asp:DropDownList ID="ddltype" runat="server"  CssClass="SmallFont" width="100px"
                            AutoPostBack="True" onselectedindexchanged="ddltype_SelectedIndexChanged">
                            <asp:ListItem>Main</asp:ListItem>
                            <asp:ListItem>Cartons</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                            <td>
                                From
                            </td>
                            <td>
                                <asp:TextBox ID="txtFrom" runat="server">0</asp:TextBox>
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
                                <asp:TextBox ID="txtTo" runat="server">10</asp:TextBox>
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
    </ContentTemplate>
   </asp:UpdatePanel>
</asp:Content>
