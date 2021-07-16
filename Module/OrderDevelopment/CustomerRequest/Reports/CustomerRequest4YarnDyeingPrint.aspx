<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CustomerRequest4YarnDyeingPrint.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Reports_CustomerRequest4YarnDyeingPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <table class="tContentArial">
<tr>
<td>
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
            <td align="center" >
            <asp:Label ID="lblPrint"  runat="server">Print</asp:Label>
            
            </td>
            </tr>
            <tr>
            <td>
            <table>
            <tr>
            <td class="tdRight">
            <asp:Label ID="lblOrderNo" runat="server" Text="Order No" CssClass="Label SmallFont"></asp:Label>
            </td>
            <td class="tdLeft">
            <asp:TextBox  ID="txtOrderno" CssClass="TextBoxNo SmallFont" runat="server"></asp:TextBox>
            </td >
            <td class="tdRight" >
            <asp:Label ID="Label1" CssClass="Label SmallFont" Text="Business Type" runat="server"></asp:Label>
            </td>
            <td>
            <asp:TextBox ID="txtBusinessType" runat="server" CssClass="TextBoxNo SmallFont"></asp:TextBox>
            </td>
            <td class="tdLeft" >
            <asp:Label ID="lblArticleNo" CssClass="Label SmallFont" Text="Article No" runat="server"></asp:Label>
            </td>
            <td class="tdRight" >
           <asp:TextBox ID="txtArticleNo" runat="server" CssClass="TextBoxNo SmallFont"></asp:TextBox>
            </td>
            
            </tr>
            <tr>
            <td class="tdLeft">
            <asp:Label ID="lblBranch" runat="server" CssClass="Label SmallFont" Text="Branch"></asp:Label>
            </td>
            <td class="tdRight" >
           <asp:DropDownList runat="server" id="ddlBranch" AutoPostBack="true"></asp:DropDownList>
            </td>
            </tr>
            </table>
            </td>
            </tr>
  </table>
</asp:Content>

