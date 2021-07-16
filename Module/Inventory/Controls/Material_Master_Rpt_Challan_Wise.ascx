<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Material_Master_Rpt_Challan_Wise.ascx.cs"
    Inherits="Module_Inventory_Controls_Material_Master_Rpt_Challan_Wise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <style type="text/css">
            .item
            {
                position: relative !important;
                display: -moz-inline-stack;
                display: inline-block;
                zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
            .header
            {
                margin-left: 4px;
            }
            .c1
            {
                width: 80px;
            }
            .c2
            {
                margin-left: 4px;
                width: 131px;
            }
            .c3
            {
                margin-left: 4px;
                width: 80px;
            }
            .c4
            {
                margin-left: 4px;
                width: 120px;
            }
            .d1
            {
                width: 150px;
            }
            .d2
            {
                margin-left: 4px;
                width: 200px;
            }
            .d3
            {
                width: 100px;
            }
        </style>
       
        <cc1:MaskedEditExtender runat="server" TargetControlID="TxtFromDate" Mask="99/99/9999"
            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
            MaskType="Date" InputDirection="LeftToRight" ErrorTooltipEnabled="True">
        </cc1:MaskedEditExtender>
         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtFromDate" Format="dd/MM/yyyy" ></cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TxtToDate"
            Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
            OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
            ErrorTooltipEnabled="True">
        </cc1:MaskedEditExtender>
         <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TxtToDate" Format="dd/MM/yyyy" ></cc1:CalendarExtender>
         
        <table class="td tContent" width="100%">
            <tr>
                <td>
                    <table align="left">
                        <tr>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Height="41" Width="48" OnClientClick="aspnetForm.target ='_blank';"
                                    OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Height="41" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <span class="titleheading">MATERIAL TRANSACTION REPORT CHALLAN WISE REPORT</span>
                </td>
            </tr>
            <tr class="tContentArial">
                <td>
                    <table width="100%">
                        <tr>
                            <td class="td">
                                Trn. Type:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="DDLTrnType" Width="131px" CssClass="SmallFont TextBox UpperCase"
                                    runat="server">
                                    <asp:ListItem Value="">------------Select-------------</asp:ListItem>
                                    <asp:ListItem Value="I"> Issue </asp:ListItem>
                                    <asp:ListItem Value="R"> Receive </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                Financial Year:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="DDLFinancialYear" Width="131px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="DDLFinancialYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                Item:
                            </td>
                            <td class="tdLeft">
                                <cc2:ComboBox ID="CmbItem" runat="server" CssClass="smallfont" Width="131px" EnableLoadOnDemand="True"
                                    DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true"
                                    OpenOnFocus="true" Visible="true" Height="200px" EmptyText="Select Item..." OnLoadingItems="CmbItem_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header d1">
                                            ITEM CODE</div>
                                        <div class="header d2">
                                            ITEM DESCRIPTION</div>
                                        <div class="header d3">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item d1">
                                            <%# Eval("ITEM_CODE")%></div>
                                        <div class="item d2">
                                            <%# Eval("ITEM_DESC") %></div>
                                        <div class="item d3">
                                            <%# Eval("ITEM_TYPE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="td">
                                Item Categ.:
                            </td>
                            <td class="tdLeft">
                                <cc2:ComboBox runat="server" ID="ddlItemCategory" CssClass="SmallFont UpperCase"
                                    MenuWidth="250px" Width="131px" Height="180px" EmptyText="Select Item Category..."
                                    EnableLoadOnDemand="true" OnLoadingItems="ddlItemCategory_LoadingItems" AutoPostBack="True"
                                    TabIndex="2" />
                            </td>
                        </tr>
                        <tr>
                           <td class="td">
                                From date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="TxtFromDate" Width="128px" runat="server" MaxLength="10" AutoPostBack="True"
                                    CssClass="SmallFont TextBox UpperCase" OnTextChanged="TxtFromDate_TextChanged"></asp:TextBox>
                            </td>
                            <td class="td">
                                To Date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="TxtToDate" MaxLength="10" Width="128px" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    AutoPostBack="True" OnTextChanged="TxtToDate_TextChanged"></asp:TextBox>
                            </td>
                            <td class="td">
                                Department
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddldepartment" runat="server" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE"
                                    Width="131px" CssClass="SmallFont TextBox UpperCase">
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                Item Make
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="DDLItemMake" runat="server" DataTextField="ITEM_MAKE" DataValueField="ITEM_MAKE"
                                    Width="131px" CssClass="SmallFont TextBox UpperCase">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                Rac Code:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="DDLRacCode" runat="server" Width="131px" CssClass="SmallFont TextBox UpperCase">
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                Associate Item:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="DDLAssociateItem" runat="server" Width="131px" CssClass="SmallFont TextBox UpperCase">
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                Report Name:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="DDLReportName" runat="server" Width="131px" CssClass="SmallFont TextBox UpperCase">
                                    <asp:ListItem Value="ML">Material Ledger</asp:ListItem>
                                    <asp:ListItem Value="MS">Material Stock Statement</asp:ListItem>
                                    <asp:ListItem Value="CW">Challan Wise</asp:ListItem>
                                    <asp:ListItem Value="DI">Department/Item Wise</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
