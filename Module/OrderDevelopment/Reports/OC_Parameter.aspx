<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="OC_Parameter.aspx.cs" Inherits="Module_OrderDevelopment_Reports_OC_Parameter"
    Title="Untitled Page" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <table class="tContentArial" width="90%">
        <tr>
            <td class="td">
                <table align="left" >
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
            <td class="td" width="90%">
                <table width="90%">
                    <tr>
                        <td class="tdRight">
                            &nbsp;Business Type
                        </td>
                        <td class="tdLeft">
                            <cc3:OboutDropDownList ID="ddlBusinessType" runat="server" AutoPostBack="True" MenuWidth="200px"
                                Width="60%" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged">
                            </cc3:OboutDropDownList>
                        </td>
                        <td class="tdRight">
                            Order Category</td>
                        <td class="tdLeft">
                        <cc3:OboutDropDownList ID="ddlOrderCategory" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="60%" OnSelectedIndexChanged="ddlOrderCategory_SelectedIndexChanged">
                            <asp:ListItem>DIRECT SALE</asp:ListItem>
                            <asp:ListItem>CR_CLUB</asp:ListItem>
                        </cc3:OboutDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdRight">
                            Product Type</td>
                        <td class="tdLeft">
                        <cc3:OboutDropDownList ID="ddlProductType" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="60%" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                        </cc3:OboutDropDownList>
                        </td>
                        <td class="tdRight">
                            Order Type</td>
                        <td class="tdLeft">
                            <cc3:OboutDropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" 
                                MenuWidth="200px" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged" 
                                Width="60%">
                            </cc3:OboutDropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdRight">
                            <asp:Label ID="lblFrom" runat="server" Text="From" CssClass="Label SmallFont"></asp:Label>
                        </td>
                        <td class="tdLeft" colspan="1">
                            <asp:TextBox ID="txtFrom" CssClass="TextBoxNo SmallFont" runat="server" ValidationGroup="M1" Width="100px"></asp:TextBox>
                          <%--  <asp:RangeValidator ID="RvFrom" runat="server" ControlToValidate="txtFrom" Display="Dynamic"
                                ErrorMessage="Invalid Number" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                                ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>--%>
                        </td>
                        <td class="tdRight">
                            <asp:Label ID="lblTo" runat="server" Text="To" CssClass="Label SmallFont"></asp:Label>
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="txtTo" CssClass="TextBoxNo SmallFont" runat="server" ValidationGroup="M1" Width="100px"></asp:TextBox>
                            <%--<asp:RangeValidator ID="RvTo" runat="server" ControlToValidate="txtTo" Display="Dynamic"
                                ErrorMessage="Invalid Number" MaximumValue="9999999" MinimumValue="1" Type="Integer"
                                ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>--%>
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
