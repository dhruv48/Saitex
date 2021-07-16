<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LotPlanning.ascx.cs" Inherits="Module_PlanningAndScheduling_Controls_LotPlanning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

<table class="td tContentArial" width="95%">
    <tr>
        <td class="td" width="95%">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClick="imgPrint_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" OnClick="imgClear_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" OnClick="imgExit_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader" colspan="3">
            <table width="100%">
                <tr>
                    <td align="center" style="background-color: #336799; color: white;">
                        <b class="titleheading">Lot Planning </b>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="LblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="LblError" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table width="100%" style="font-weight: bold">
                <tr>
                    <td align="right" valign="top" class="tdRight" width="12%">
                        <asp:Label ID="lblProducttype" runat="server" Text="PRODUCT:" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlProductType" CssClass="SmallFont TextBox UpperCase BoldFont"
                            runat="server" Width="98%">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%" valign="top">
                        <asp:Label ID="lblOrderCategory" runat="server" Text="Order Category:" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlOrderCategory" runat="server" CssClass="SmallFont BoldFont"
                            Width="98%">
                            <asp:ListItem>DIRECT SALE</asp:ListItem>
                            <asp:ListItem>INHOUSE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblOrderType" runat="server" Text="Order Type:" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlordertype" runat="server" CssClass="SmallFont BoldFont UPPERCASE"
                            Width="99%">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblBusinessType" runat="server" Text="BUSINESS TYPE:" 
                            CssClass="SmallFont">

                        </asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBusiness" runat="server" 
                            CssClass="SmallFont TextBox UpperCase">
                            <asp:ListItem Value="SW">Sales Work</asp:ListItem>
                            <asp:ListItem Value="JW">Job Work</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                </tr>
               
            </table>
            </td>
            </tr>
            </table>