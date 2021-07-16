<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Work_order_Entry_report.aspx.cs" Inherits="Module_WorkOrder_Pages_Work_order_Entry_report" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table class="tContentArial" width="60%">
    <tr>
        <td class="td">
            <table align="left">
                <tbody>
                    <tr>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click"  runat="server" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnClear"  runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnExit"  runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnHelp"  runat="server" ImageUrl="~/CommonImages/link_help.png"
                                ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" width="100%">
            <span class="titleheading"><b>Print Job Work Detail
                <asp:Label ID="lblTRNType" runat="server"></asp:Label></b></span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr>
                    
                    <td valign="top" align="right" width="35%">
                        <asp:Label ID="Label2" runat="server" CssClass="LabelNo" Text="Order Type :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="25%">
                        <asp:DropDownList ID="ddlWOType" runat="server" Width="120px" CssClass="TextBox SmallFont"
                            Font-Bold="true" TabIndex="1" AutoPostBack="true" 
                            onselectedindexchanged="ddlWOType_SelectedIndexChanged">
                            <asp:ListItem Value="YARN">YARN</asp:ListItem>
                            <asp:ListItem Value="SEWING THREAD">SEWING THREAD</asp:ListItem>
                            <asp:ListItem Value="ITEM">ITEM</asp:ListItem>
                          
                        </asp:DropDownList>
                    </td>
                    <td>
                   
                        <asp:Label ID="lblFrom" runat="server" Text="From" CssClass="Label SmallFont"></asp:Label>
                    </td>
                      <td >
                        <asp:TextBox ID="txtFrom" CssClass="TextBoxNo SmallFont" Width="60%" runat="server" ValidationGroup="M1"></asp:TextBox>
                        <asp:RangeValidator ID="RvFrom" runat="server" ControlToValidate="txtFrom" Display="Dynamic"
                            ErrorMessage="Invalid Number" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                            ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
                   </td>
                    <td>
                        <asp:Label ID="lblTo" runat="server" Text="To" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtTo" CssClass="TextBoxNo SmallFont" Width="60%" runat="server" ValidationGroup="M1"></asp:TextBox>
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