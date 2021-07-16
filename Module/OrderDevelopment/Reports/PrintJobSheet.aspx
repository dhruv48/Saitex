<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PrintJobSheet.aspx.cs" Inherits="Module_OrderDevelopment_Reports_PrintJobSheet" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
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
                        <td class="tdRight">
                            <asp:Label ID="Label2" runat="server" Text="From Date" CssClass="Label SmallFont"></asp:Label>
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="txtFDATE" CssClass="TextBoxNo SmallFont" runat="server" ValidationGroup="M1" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtFrom" CssClass="TextBoxNo SmallFont" runat="server" Visible="false" ></asp:TextBox>
                          <%--  <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtFrom" Display="Dynamic"
                            ErrorMessage="Invalid Number" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                            ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>--%>

                         
                        </td>
                        <td class="tdRight">
                            <asp:Label ID="Label3" runat="server" Text="To Date" CssClass="Label SmallFont"></asp:Label>
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="txtTDATE" CssClass="TextBoxNo SmallFont" runat="server" ValidationGroup="M1" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtTo" CssClass="TextBoxNo SmallFont" runat="server" Visible="false"></asp:TextBox>
                           <%-- <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtTo" Display="Dynamic"
                            ErrorMessage="Invalid Number" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                            ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>--%>
                        </td>
                    </tr>
                    
                   
                    
                    <tr>
                        <td colspan="4" class="tdCenter">
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="M1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

     <cc1:CalendarExtender ID="cetsub_FDT" Format="dd/MM/yyyy" TargetControlID="txtFDATE"
            runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="cetsub_TODT" Format="dd/MM/yyyy" TargetControlID="txtTDATE"
            runat="server">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtTDATE" PromptCharacter="_">
        </cc1:MaskedEditExtender>
     <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

