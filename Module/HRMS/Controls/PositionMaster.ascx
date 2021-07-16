<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PositionMaster.ascx.cs"
    Inherits="Module_HRMS_Controls_PositionMaster" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 180px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<table id="tblDesgMainTable" runat="server" cellspacing="0" cellpadding="0" align="Left"
    class="tContentArial">
    <tr>
        <td align="Right" class="td">
            <table align="left">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnSave_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" OnClick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td">
            <span class="titleheading">Position Master</span>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td align="right" valign="top">
                        Position Code
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtPositionCode" runat="server" CssClass="SmallFont TextBox UpperCase"
                            Width="130px" TabIndex="1" Rows="2" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="dynamic" runat="server"
                            ErrorMessage="*Enter Position Code" ControlToValidate="txtPositionCode" ValidationGroup="M1"></asp:RequiredFieldValidator>
                        <obout:ComboBox runat="server" ID="ddlPosition" Width="200px" Height="250px" MenuWidth="200px"
                            DataTextField="POSITION_NAME" DataValueField="POSITION_CODE" EnableLoadOnDemand="true"
                            AutoPostBack="True" OnLoadingItems="ddlPosition_LoadingItems" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged1">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Position Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <%# Eval("Position_NAME")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </obout:ComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Position Name
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtPositionName" runat="server" CssClass="SmallFont TextBox UpperCase"
                            Width="130px" TabIndex="2" Rows="2" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="dynamic" runat="server"
                            ErrorMessage="*Enter Position Name" ControlToValidate="txtPositionName" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Senior Position
                    </td>
                    <td align="left" valign="top">
                        <cc2:OboutDropDownList ID="ddlSRPosition" runat="server" Height="250px" MenuWidth="200px"
                            Width="150px" TabIndex="2">
                        </cc2:OboutDropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Remarks
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" Width="300px"
                            TabIndex="4" Rows="2" MaxLength="50" TextMode="MultiLine" AutoPostBack="True"
                            Height="50px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
