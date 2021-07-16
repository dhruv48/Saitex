<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionEntryReport.ascx.cs"
    Inherits="Module_Production_Controls_ProductionEntryReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<table class="td tContentArial" width="950px">
    <tr>
        <td align="left" class="td" colspan="6">
            <table align="left">
                <tr>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" OnClick="imgbtnPrint_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan="6">
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Production Entry Report&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="6">
            <span class="Mode">
                <asp:Label ID="lblMode" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
    <tr>
        <td align="right">
            PA No :
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderNo" runat="server" DataTextField="ORDER_NO" DataValueField="ORDER_NO"
                Width="128px" CssClass="SmallFont">
            </asp:DropDownList>
        </td>
        <td align="right">
            Process Code :
        </td>
        <td>
            <asp:DropDownList ID="ddlprocessno" runat="server" DataTextField="PROS_DESC" DataValueField="PROS_CODE"
                Width="128px" CssClass="SmallFont">
            </asp:DropDownList>
        </td>
        <td align="right">
            Machine Code :
        </td>
        <td>
            <asp:DropDownList ID="ddlMachin" runat="server" DataTextField="MACHINE_CODE" DataValueField="MACHINE_CODE"
                Width="128px" CssClass="SmallFont">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            Department :
        </td>
        <td>
            <asp:DropDownList ID="ddldept" runat="server" DataTextField="PROS_DESC" DataValueField="PROS_CODE"
                Width="128px" CssClass="SmallFont">
            </asp:DropDownList>
        </td>
        <td align="right">
            Lot No :
        </td>
        <td>
            <asp:DropDownList ID="ddllotno" runat="server" DataTextField="LOT_NUMBER" DataValueField="LOT_NUMBER"
                Width="128px" CssClass="SmallFont">
            </asp:DropDownList>
        </td>
        <td align="right">
            Dyed Lot No :
        </td>
        <td>
            <asp:DropDownList ID="ddldyedlotno" runat="server" DataTextField="DYED_LOT_NO" DataValueField="DYED_LOT_NO"
                Width="128px" CssClass="SmallFont">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            Shift Name :
        </td>
        <td>
            <asp:DropDownList ID="ddlshift" runat="server" DataTextField="SFT_NAME" DataValueField="SFT_ID"
                Width="128px" CssClass="SmallFont">
            </asp:DropDownList>
        </td>
        <td align="right">
            From Date :
        </td>
        <td>
            <asp:TextBox ID="txtformdate" runat="server" Width="126px"></asp:TextBox>
            <cc4:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtformdate"
                PopupPosition="TopLeft">
            </cc4:CalendarExtender>
        </td>
        <td align="right">
            To Date :
        </td>
        <td>
            <asp:TextBox ID="txtTodate" runat="server" AutoPostBack="True" Width="126px" OnTextChanged="txtTodate_TextChanged"></asp:TextBox>
            <cc4:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTodate"
                PopupPosition="TopLeft">
            </cc4:CalendarExtender>
        </td>
    </tr>
</table>
