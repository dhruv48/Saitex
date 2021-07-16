<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="Waste_Stock_Statement.aspx.cs" Inherits="Module_Waste_Pages_Waste_Stock_Statement"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
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
            width: 150px;
        }
        .c3
        {
            margin-left: 4px;
            width: 80px;
        }
        .d1
        {
            width: 150px;
        }
        .d2
        {
            margin-left: 4px;
            width: 350px;
        }
        .d3
        {
            width: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                function NewWindow() {
                    document.forms[0].target = "_blank";
                }
            </script>

            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TxtFromDate"
                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
                ErrorTooltipEnabled="True">
            </cc1:MaskedEditExtender>
            <cc1:CalendarExtender ID="calender" runat="server" TargetControlID="TxtFromDate" Format="dd/MM/yyyy" ></cc1:CalendarExtender>
            
            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TxtToDate"
                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
                ErrorTooltipEnabled="True">
            </cc1:MaskedEditExtender>
              <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TxtToDate" Format="dd/MM/yyyy" ></cc1:CalendarExtender>
            <table class="tContentArial" width="100%">
                <tr>
                    <td valign="top" colspan="6" align="left">
                        <table cellspacing="0" cellpadding="0" align="left">
                            <tbody>
                                <tr>
                                    <td valign="top" align="center">
                                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                            ToolTip="Print" Height="41" Width="48" OnClientClick="NewWindow();" OnClick="imgbtnPrint_Click">
                                        </asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                            OnClick="imgbtnClear_Click"></asp:ImageButton>
                                    </td>
                                    <td valign="top" align="center">
                                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                            ToolTip="Exit" Height="41" Width="48" OnClick="imgbtnExit_Click"></asp:ImageButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" valign="top" class="tRowColorAdmin td">
                        <span class="titleheading">Material Stock Statement Reports</span>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        Financial Year:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLFinancialYear" Width="151px" runat="server" AutoPostBack="True"
                            CssClass="SmallFont TextBox UpperCase" OnSelectedIndexChanged="DDLFinancialYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        From date:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtFromDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                            AutoPostBack="True" OnTextChanged="TxtFromDate_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        To Date:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtToDate" CssClass="SmallFont TextBox UpperCase" Width="150px"
                            runat="server" AutoPostBack="True" OnTextChanged="TxtToDate_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        Item:
                    </td>
                    <td class="tdLeft">
                        <cc2:ComboBox ID="txtICODE" runat="server" CssClass="smallfont" Width="151px" EnableLoadOnDemand="True"
                            DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true"
                            OpenOnFocus="true" Visible="true" Height="200px" EmptyText="------------All----------"
                            OnLoadingItems="txtICODE_LoadingItems">
                            <HeaderTemplate>
                                <div class="header d1">
                                     CODE</div>
                                <div class="header d2">
                                     DESCRIPTION</div>
                                <div class="header d3">
                                    CATEGORY</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item d1">
                                    <%# Eval("ITEM_CODE")%></div>
                                <div class="item d2">
                                    <%# Eval("ITEM_DESC") %></div>
                                <div class="item d3">
                                    <%# Eval("CAT_CODE")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight">
                        Item Categories:
                    </td>
                    <td class="tdLeft">
                        <cc2:ComboBox runat="server" ID="ddlItemCategory" CssClass="SmallFont" MenuWidth="250px"
                            Width="150px" Height="180px" EmptyText="Select Item Category..." EnableLoadOnDemand="true"
                            OnLoadingItems="ddlItemCategory_LoadingItems" AutoPostBack="True" TabIndex="2" />
                    </td>
                    <td class="tdRight">
                       <%-- Item Make:--%>
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLItemMake" Width="151px" runat="server" CssClass="SmallFont TextBox UpperCase" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                       <%-- Item Rac:--%>
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLItemRac" Width="151px" runat="server" CssClass="SmallFont TextBox UpperCase" Visible="false">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                       <%-- Associate Item:--%>
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLAssociate" Width="151px" runat="server" CssClass="SmallFont TextBox UpperCase" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
