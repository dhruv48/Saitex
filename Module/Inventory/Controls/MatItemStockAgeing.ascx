<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MatItemStockAgeing.ascx.cs"
    Inherits="Module_Inventory_Controls_MatItemStockAgeing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="td tContent" style="width: 724px">
            <tr>
                <td>
                    <table align="left">
                        <tr>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Height="41" ValidationGroup="M1" Width="48" OnClick="imgbtnPrint_Click">
                                </asp:ImageButton>
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
                                    ToolTip="Help" Height="41" Width="48" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" colspan="5">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <span class="titleheading">Material Stock Ageing</span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 715px">
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="Label1" runat="server" Text="Branch :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="SmallFont" Width="150px"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                             <td class="tdRight">
                                <asp:Label ID="Label2" runat="server" Text="Location :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddllocation" runat="server" CssClass="SmallFont" Width="150px"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                             <td class="tdRight">
                                <asp:Label ID="Label8" runat="server" Text="Store :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlstore" runat="server" CssClass="SmallFont" Width="150px"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label3" runat="server" Text="Item Type :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="SmallFont" Width="150px"
                                    TabIndex="2">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label4" runat="server" Text="Category Code :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlCatCode" runat="server" CssClass="SmallFont" Width="150px"
                                    TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="Label5" runat="server" Text="Day 1 :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDay1" runat="server" CssClass="TextBoxNo" Width="147px" TabIndex="4"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label6" runat="server" Text="Day 2 :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDay2" runat="server" CssClass="TextBoxNo" Width="147px" TabIndex="5"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label7" runat="server" Text="Day 3 :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDay3" runat="server" CssClass="TextBoxNo" Width="147px" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="RFDay1" runat="server" ControlToValidate="txtDay1"
            Display="None" ErrorMessage="Please enter Day 1" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RVDay1" runat="server" ControlToValidate="txtDay1" Display="None"
            ErrorMessage="Please enter only numeric value that should be greater than zero"
            MaximumValue="99999" MinimumValue="1" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RFDay2" runat="server" ControlToValidate="txtDay2"
            Display="None" ErrorMessage="Please enter Day 2" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RVDay2" runat="server" ControlToValidate="txtDay2" Display="None"
            ErrorMessage="Please enter only numeric value that should be greater than zero"
            MaximumValue="99999" MinimumValue="1" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RFDay3" runat="server" ControlToValidate="txtDay3"
            Display="None" ErrorMessage="Please enter Day 3" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RVDay3" runat="server" ControlToValidate="txtDay3" Display="None"
            ErrorMessage="Please enter only numeric value that should be greater than zero"
            MaximumValue="99999" MinimumValue="1" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
    </ContentTemplate>
</asp:UpdatePanel>
