<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CRForSTReportOPT.ascx.cs"
    Inherits="Module_SewingThread_Controls_CRForSTReportOPT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 200px;
    }
    .d2
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 180px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class=" td tContentArial" width="100%">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFindTop_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" class="TableHeader td">
                    <b class="titleheading">Customer Request For Sewing Thread Report</b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlFilter" runat="server">
                        <table>
                            <tr>
                                <td align="right" style="width: 12.5%;">
                                    Branch :
                                </td>
                                <td align="left" style="width: 12.5%;">
                                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="SmallFont" TabIndex="1"
                                        Width="100px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" style="width: 12.5%;">
                                    Party :
                                </td>
                                <td align="left" style="width: 12.5%;">
                                    <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                        DataTextField="PRTY_CODE" DataValueField="Address" EmptyText="Select Party" EnableVirtualScrolling="true"
                                        Width="100px" MenuWidth="350px" Height="200px" OnLoadingItems="txtPartyCode_LoadingItems">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Code</div>
                                            <div class="header c2">
                                                Name</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                            <div class="item c2">
                                                <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td align="right" style="width: 12.5%;">
                                    Article :
                                </td>
                                <td align="left" style="width: 12.5%;">
                                    <cc2:ComboBox ID="ddlArticle" runat="server" AutoPostBack="True" CssClass="smallfont"
                                        EnableLoadOnDemand="True" DataTextField="YARN_CODE" DataValueField="Combined"
                                        EmptyText="Select Article" MenuWidth="350px" EnableVirtualScrolling="true" OpenOnFocus="true"
                                        TabIndex="11" Visible="true" Height="200px" Width="100px" OnLoadingItems="ddlArticle_LoadingItems">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Article Code</div>
                                            <div class="header c2">
                                                Description</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("YARN_CODE") %></div>
                                            <div class="item c2">
                                                <%# Eval("YARN_DESC") %></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td align="right" style="width: 12.5%;">
                                    Shade Family :
                                </td>
                                <td align="left" style="width: 12.5%;">
                                    <asp:DropDownList ID="ddlShadeFamily" runat="server" CssClass="SmallFont" TabIndex="4"
                                        Width="100px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 12.5%;">
                                    Shade Code :
                                </td>
                                <td align="left" style="width: 12.5%;">
                                    <cc2:ComboBox ID="ddlShadeCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                                        DataTextField="SHADE_CODE" DataValueField="SHADE_CODE" EnableLoadOnDemand="True"
                                        MenuWidth="400" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="5"
                                        Height="200px" Visible="true" Width="100px" OnLoadingItems="ddlShadeCode_LoadingItems"
                                        EmptyText="ALL">
                                        <HeaderTemplate>
                                            <div class="header d2">
                                                Shade Family Name</div>
                                            <div class="header d4">
                                                Shade Name</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item d2">
                                                <%# Eval("SHADE_FAMILY_NAME")%></div>
                                            <div class="item d4">
                                                <%# Eval("SHADE_NAME")%></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td align="right" style="width: 12.5%;">
                                    CR Date From :
                                </td>
                                <td align="left" style="width: 12.5%;">
                                    <asp:TextBox ID="txtCRFrom" runat="server" TabIndex="6" Width="95px" CssClass="SmallFont"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 12.5%;">
                                    CR Date To :
                                </td>
                                <td align="left" style="width: 12.5%;">
                                    <asp:TextBox ID="txtCRTo" runat="server" TabIndex="7" Width="95px" CssClass="SmallFont"
                                        AutoPostBack="true" OnTextChanged="txtCRTo_TextChanged"></asp:TextBox>
                                </td>
                                <td align="right" style="width: 12.5%;">
                                    Status :
                                </td>
                                <td align="left" style="width: 12.5%;">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="SmallFont" TabIndex="8"
                                        Width="100px">
                                        <asp:ListItem Text="------ALL------" Value="SELECT" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="UNCONFIRMED" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="CONFIRMED" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="CLOSED" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtCRFrom" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtCRTo" Format="dd/MM/yyyy"
            PopupPosition="TopLeft">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="ME1" runat="server" Mask="99/99/9999" MaskType="Date"
            TargetControlID="txtCRFrom" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="ME2" runat="server" Mask="99/99/9999" MaskType="Date"
            TargetControlID="txtCRTo" PromptCharacter="_">
        </cc1:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
