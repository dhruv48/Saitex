<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FIBER_MASTER_NEW.ascx.cs"
    Inherits="Module_Fiber_Controls_FIBER_MASTER_NEW" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
        width: 250px;
    }
    .c4
    {
        margin-left: 4px;
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 100px;
    }
    .TextBoxNo
    {
        height: 22px;
    }
    .style2
    {
        width: 61px;
    }
</style>

<script type="text/javascript" src="../../../javascript/jquery-1.4.1.min.js"></script>

<script src="../../../javascript/jquery-ui.min.js" type="text/javascript"></script>

<link href="../../../javascript/jquery-ui.css" rel="Stylesheet" type="text/css" />

<script type="text/javascript">
    $(document).ready(function() {
        $("#<%=txtPalletCode.ClientID %>").autocomplete({

            source: function(request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/MOM.asmx/GetMOMPalletMaster") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        response(data.d);

                    },
                    error: function(response) {
                        alert(response.responseText);
                    },
                    failure: function(response) {
                        alert(response.responseText);
                    }
                });
            },

            minLength: 1
        });



        $("#<%=txtGrade.ClientID %>").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/MOM.asmx/GetMOMGrade") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        response(data.d);

                    },
                    error: function(response) {
                        alert(response.responseText);
                    },
                    failure: function(response) {
                        alert(response.responseText);
                    }
                });
            },

            minLength: 1
        });


        $("#<%=txtLotNo.ClientID %>").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/MOM.asmx/GetMOMMerge") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        response(data.d);

                    },
                    error: function(response) {
                        alert(response.responseText);
                    },
                    failure: function(response) {
                        alert(response.responseText);
                    }
                });
            },
            minLength: 1
        });


        $("#<%=txtdescription.ClientID %>").autocomplete({
            source: function(request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/MOM.asmx/GetFiberDescription") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        response(data.d);
                        //                        response($.map(data.d, function(item) {
                        //                            return {
                        //                                label: item.split('-')[0],
                        //                                val: item.split('-')[1]
                        //                            }
                        //                        }))
                    },
                    error: function(response) {
                        alert(response.responseText);
                    },
                    failure: function(response) {
                        alert(response.responseText);
                    }
                });
            },

            minLength: 1
        });
    });
</script>


 <asp:UpdatePanel ID="uppnl" runat="server">
    <ContentTemplate>
<table width="100%" style="border: 0px; border-style: inset;">
    <tr>
        <td align="left" class="td" valign="top">
            <table class="tContentArial">
                <tr>
                    <td>
                        <asp:Label ID="lblMode" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" ValidationGroup="YM" TabIndex="38" OnClick="imgbtnSave_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Update" ValidationGroup="M1" OnClick="imgbtnUpdate_Click" TabIndex="38" />
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/CommonImages/list.jpg"
                            ToolTip="Poy Master List" OnClick="imgbtnDelete_Click" TabIndex="39" />
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" OnClick="imgbtnFind_Click" TabIndex="40" CausesValidation="false" />
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClick="imgbtnPrint_Click" TabIndex="41" CausesValidation="false" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Style="height: 41px" OnClick="imgbtnClear_Click" TabIndex="42"
                            CausesValidation="false" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" OnClick="imgbtnExit_Click" TabIndex="43" CausesValidation="false" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" TabIndex="56" CausesValidation="false" />
                    </td>
                </tr>
            </table>
            <table width="100%" class="tContentArial">
                <tr>
                    <td align="center" class="TableHeader td" valign="top">
                        <span class="titleheading"><b>P.O.Y. MASTER</b></span>
                    </td>
                </tr>
                <tr>
                    <td class="td" align="left" width="50%">
                        <table class="tContentArial">
                            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>--%>
                            <tr>
                                <td align="right" valign="top">
                                    <font color="#ff0000">*</font>Poy Code:
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtFiberCode" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        Width="125px" TabIndex="1" AutoPostBack="True" OnTextChanged="txtFiberCode_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV1" runat="server" ControlToValidate="txtFiberCode"
                                        ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                    <%-- <asp:DropDownList ID="DDLFiberCode" Width="130px" 
                            CssClass="SmallFont TextBox UpperCase" runat="server" 
                            AutoPostBack="True" 
                            onselectedindexchanged="DDLFiberCode_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                                    <cc2:ComboBox ID="DDLFiberCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="Combined" DataValueField="FIBER_CODE" EmptyText="Find Poy Code"
                                        EnableLoadOnDemand="true" Height="200px" MenuWidth="400" OnLoadingItems="DDLFiberCode_LoadingItems"
                                        OnSelectedIndexChanged="DDLFiberCode_SelectedIndexChanged" TabIndex="1" Width="125px"
                                        Visible="False">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                POY CODE</div>
                                            <div class="header c2">
                                                POY Description</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("FIBER_CODE") %>' /></div>
                                            <div class="item c2">
                                                <asp:Literal ID="Container2" runat="server" Text='<%# Eval("FIBER_DESC") %>' /></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td align="right" valign="top">
                                    Poy&nbsp;Category:
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList ID="ddlfibercat" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlfibercat_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <%--<asp:TextBox ID="ddlfibercat" runat="server" CssClass="SmallFont TextBox UpperCase" Width="125px"></asp:TextBox>--%>
                                </td>
                                <td align="right" valign="top">
                                    Poy&nbsp;Sub&nbsp;Cat:
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList ID="ddlsubfiber_cat" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                        runat="server" TabIndex="3">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Poy&nbsp;Description:
                                </td>
                                <td align="left" valign="top" colspan="3">
                                    <asp:TextBox ID="txtdescription" runat="server" TabIndex="4" CssClass="SmallFont TextBox UpperCase"  onkeyup="javascript:this.value = this.value.toUpperCase();"
                                        MaxLength="75" Width="98%"></asp:TextBox>
                                </td>
                                <%-- <td align="right" valign="top">
                       
                    </td>
                    <td align="left" valign="top">
                        
                    </td>--%>
                                <td align="right" valign="top">
                                    Uom:
                                    <%-- Uom2:--%>
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList ID="ddluom1" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="5" runat="server">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddluom2" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="5" runat="server" Visible="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    Denier
                                    <%-- Poy&nbsp;Length:--%><%-- Length Type:--%>
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList ID="ddlLengthType" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="6" runat="server" OnSelectedIndexChanged="ddlLengthType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" valign="top">
                                    Filament
                                    <%-- Length Value:--%>
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList ID="ddllengthvalue" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="7" runat="server">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtlengthvalue" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="7" Width="125px" MaxLength="20" Visible="false"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" visible="false">
                                    Poy Lusture:
                                </td>
                                <td align="left" valign="top" visible="false">
                                    <asp:DropDownList ID="ddlfiberlusture" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="8" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="tdDenier" runat="server" visible="false">
                                <td align="right" valign="top">
                                    Poy Denier:
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtFiberDenier" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="9" Width="125px" OnTextChanged="txtFiberDenier_TextChanged" MaxLength="50"></asp:TextBox>
                                </td>
                                <td align="right" valign="top">
                                    Fancy Effect:
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList ID="ddlFancyEffect" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="10" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" valign="top" visible="false">
                                    Poy Tenacity:
                                </td>
                                <td align="left" valign="top" visible="false">
                                    <asp:TextBox ID="txtTanacity" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="11" Width="125px" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <font color="#ff0000">*</font>Opening Stock:
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtOpeningBalanceStock" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        MaxLength="7" TabIndex="12" Width="125px" OnTextChanged="txtOpeningBalanceStock_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="txtOpeningBalanceStock"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                    <cc11:FilteredTextBoxExtender ID="f1" runat="server" TargetControlID="txtOpeningBalanceStock"
                                        FilterType="Custom, Numbers" ValidChars="." />
                                </td>
                                <td align="right" valign="top">
                                    <font color="#ff0000">*</font>Minimum Stock:
                                </td>
                                <td class="tdleft" width="15%">
                                    <asp:TextBox ID="txtMimimumStock" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        MaxLength="10" TabIndex="13" Width="125px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV3" runat="server" ControlToValidate="txtMimimumStock"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                    <cc11:FilteredTextBoxExtender ID="f2" runat="server" TargetControlID="txtMimimumStock"
                                        FilterType="Custom, Numbers" ValidChars="." />
                                </td>
                                <td align="right" valign="top">
                                    <font color="#ff0000">*</font>Min Procure Days:
                                </td>
                                <td class="tdleft" width="15%">
                                    <asp:TextBox ID="txtMinimumProcureDays" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        MaxLength="12" TabIndex="14" Width="125px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV5" runat="server" ControlToValidate="txtMinimumProcureDays"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                    <cc11:FilteredTextBoxExtender ID="f3" runat="server" TargetControlID="txtMinimumProcureDays"
                                        FilterType="Custom, Numbers" ValidChars="." />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <font color="#ff0000">*</font>Opening Rate:
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtOpeningRate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        MaxLength="10" TabIndex="15" Width="125px" Text="0"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV6" runat="server" ControlToValidate="txtOpeningRate"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                    <cc11:FilteredTextBoxExtender ID="f4" runat="server" TargetControlID="txtOpeningRate"
                                        FilterType="Custom, Numbers" ValidChars="." />
                                </td>
                                <td align="right" valign="top">
                                    Reorder Level:
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRecorderLevel" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        MaxLength="10" TabIndex="16" Width="125px"></asp:TextBox>
                                    <cc11:FilteredTextBoxExtender ID="FilteredTextBoxtxtRecorderLevel" runat="server"
                                        TargetControlID="txtRecorderLevel" FilterType="Custom, Numbers" ValidChars="." />
                                </td>
                                <td align="right" valign="top">
                                    <font color="#ff0000">*</font>Reorder Quantity
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRecorderQuantity" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        MaxLength="10" TabIndex="17" Width="125"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV7" runat="server" ControlToValidate="txtRecorderQuantity"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                    <cc11:FilteredTextBoxExtender ID="F5" runat="server" TargetControlID="txtRecorderQuantity"
                                        FilterType="Custom, Numbers" ValidChars="." />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <font color="#ff0000">*</font>Maximum Stock:
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtMaximumStock" runat="server" TabIndex="18" CssClass="SmallFont TextBox UpperCase"
                                        MaxLength="12" Width="125px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV8" runat="server" ControlToValidate="txtMaximumStock"
                                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                    <cc11:FilteredTextBoxExtender ID="F6" runat="server" TargetControlID="txtMaximumStock"
                                        FilterType="Custom, Numbers" ValidChars="." />
                                </td>
                                <td align="right" valign="top">
                                    Poy Supplier:
                                </td>
                                <td align="left" valign="top">
                                    <cc2:ComboBox ID="txtPartyCodecmb" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                        DataTextField="PRTY_NAME" DataValueField="PRTY_CODE" TabIndex="19" OnLoadingItems="txtPartyCodecmb_LoadingItems"
                                        EmptyText="N/A" EnableVirtualScrolling="true" Width="125px" MenuWidth="400px"
                                        Height="200px" OnTextChanged="txtPartyCodecmb_TextChanged">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Code</div>
                                            <div class="header c3">
                                                NAME</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                            <div class="item c3">
                                                <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td align="right" valign="top">
                                    Remarks:
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="Txtremark" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        TabIndex="20" Width="125px" MaxLength="1000"></asp:TextBox>
                                </td>
                                <%--<asp:TextBox ID="txtFiberSupplier" runat="server"  TabIndex="22" CssClass="SmallFont TextBox UpperCase" 
                            Width="125px"></asp:TextBox>--%>
                                <%--<asp:TextBox ID="txtpartycode" runat="server"  ></asp:TextBox>--%>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        <font color="#ff0000">*</font>Is&nbsp;Excisable&nbsp;:
                    </td>
                    <td align="left" valign="top">
                        <asp:RadioButtonList ID="rdIsExciable" runat="server" CssClass="SmallFont" RepeatColumns="4"
                            RepeatDirection="Horizontal" TabIndex="21" Height="11px" RepeatLayout="Table"
                            Width="100px" OnSelectedIndexChanged="rdIsExciable_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="right" valign="top">
                        <font color="#ff0000">*</font>ITCHS&nbsp;Code&nbsp;(Sales)&nbsp;:
                    </td>
                    <td align="left" valign="top">
                        <asp:DropDownList ID="ddlSales_ITCHS" Width="125px" CssClass="SmallFont TextBox UpperCase"
                            TabIndex="22" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtSales_ITCHS" runat="server" CssClass="gCtrTxt SmallFont" Width="125px"
                            Visible="false" TabIndex="22" MaxLength="8"></asp:TextBox>
                        <%-- <asp:RangeValidator ID="txtSales_ITCHS_RangeValidator" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter sales itchs code."
                                                    Display="None" Type="Double" ControlToValidate="txtSales_ITCHS" MinimumValue="00000000"
                                                    MaximumValue="99999999"></asp:RangeValidator>
                                                    <cc11:FilteredTextBoxExtender ID="txtSales_ITCHS_FilteredTextBoxExtender1" runat="server"
                    Enabled="True" TargetControlID="txtSales_ITCHS" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                </cc11:FilteredTextBoxExtender>--%>
                    </td>
                    <td align="right" valign="top">
                        <font color="#ff0000">*</font>ITCHS&nbsp;Code&nbsp;(Custom)&nbsp;:
                    </td>
                    <td align="left" valign="top">
                        <asp:DropDownList ID="ddlCustom_ITCHS" Width="125px" CssClass="SmallFont TextBox UpperCase"
                            TabIndex="23" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtCustom_ITCHS" runat="server" CssClass="gCtrTxt SmallFont" Width="125px"
                            TabIndex="23" MaxLength="6" Visible="false"></asp:TextBox>
                        <%--  <asp:RangeValidator ID="txtCustom_ITCHS_RangeValidator" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter custom itchs code."
                                                    Display="None" Type="Double" ControlToValidate="txtCustom_ITCHS" MinimumValue="00000000"
                                                    MaximumValue="99999999"></asp:RangeValidator>--%>
                        <%-- <cc11:FilteredTextBoxExtender ID="txtCustom_ITCHS_FilteredTextBoxExtender1" runat="server"
                    Enabled="True" TargetControlID="txtCustom_ITCHS" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                </cc11:FilteredTextBoxExtender>--%>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Tariff&nbsp;Heading&nbsp;(Chapter&nbsp;No)&nbsp;:
                    </td>
                    <td align="left" valign="top">
                        <asp:DropDownList ID="ddlTariffHeading" Width="125px" TabIndex="24" CssClass="SmallFont TextBox UpperCase"
                            runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtTariffHeading" runat="server" CssClass="gCtrTxt SmallFont" Width="125px"
                            TabIndex="24" MaxLength="8" Visible="false"></asp:TextBox>
                        <asp:RangeValidator ID="txtTariffHeadingValidator" ValidationGroup="M1" runat="server"
                            ErrorMessage="Pls enter tariff heading." Display="None" Type="Double" ControlToValidate="ddlTariffHeading"
                            MinimumValue="00000000" MaximumValue="99999999"></asp:RangeValidator>
                        <%--<cc11:FilteredTextBoxExtender ID="txtTariffHeading_FilteredTextBoxExtender" runat="server"
                    Enabled="True" TargetControlID="txtTariffHeading" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                </cc11:FilteredTextBoxExtender>--%>
                        <asp:RequiredFieldValidator ID="ddlTariffHeadingValidator" ControlToValidate="ddlTariffHeading"
                            InitialValue="0" runat="server" ErrorMessage="Pls enter tariff heading." ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" valign="top" id="trKgBail" runat="server" visible="false">
                        KG/Bail:
                    </td>
                    <td align="left" valign="top" id="trKgBail2" runat="server" visible="false">
                        <asp:TextBox ID="Txtuomperbail" runat="server" CssClass="SmallFont TextBox UpperCase"
                            TabIndex="25" Width="125px" MaxLength="1000"></asp:TextBox>
                    </td>
                </tr>
                <%--                <tr>
                    <td align="right">
                        *Maximum Stock:
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtMaximumStock" runat="server"  TabIndex="20" CssClass="SmallFont TextBox UpperCase" MaxLength="16"
                            Width="125px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RFV8" runat="server" ControlToValidate="txtMaximumStock"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                    </td>
                    
               
                
             
                    <td align="right">
                        Fiber Supplier:
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="TextBox1" runat="server"  TabIndex="20" CssClass="SmallFont TextBox UpperCase" MaxLength="16"
                            Width="125px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaximumStock"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    &nbsp;
                    </td>
                    
                </tr>--%>
                <%-- </ContentTemplate>
                 </asp:UpdatePanel>--%>
            </table>
        </td>
    </tr>
    <tr id="trhopbal" runat="server" visible="false">
        <td align="left" class="td SmallFont" valign="top" width="100%">
            <table width="100%">
                <tr bgcolor="#006699">
                    <td align="left" class="tdLeft SmallFont" valign="top" width="150px">
                        <span class="titleheading"><b>Party&nbsp;Code</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Party&nbsp;Name</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Merge</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Grade</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Pallet&nbsp;Code</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>C/P&nbsp;No</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Rate</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Qty</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>UOM</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Tubes</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Tube&nbsp;Wt.</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top" runat="server" visible="false">
                        <span class="titleheading"><b>Dat&nbsp;of&nbsp;Manufacturing</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top" runat="server" visible="false">
                        <span class="titleheading"><b>Material&nbsp;Status</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="95%" MenuWidth="400px" Height="200px" TabIndex="26">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c3">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
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
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtPartyName" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="27"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtLotNo" Width="95%" class="SmallFont uppercase" runat="server"
                            MaxLength="14" TabIndex="28"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtGrade" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="29"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtPalletCode" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="30"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtPalletNo" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="31"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtRate" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="31"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtQty" Width="95%" class="SmallFont" runat="server" AutoPostBack="True"
                            OnTextChanged="txtQty_TextChanged" MaxLength="9" TabIndex="32"></asp:TextBox>
                        <asp:Label ID="lblIssueQty" runat="server" Visible="false"></asp:Label>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>
                        <cc11:FilteredTextBoxExtender ID="FiltertxtQty" runat="server" TargetControlID="txtQty"
                            FilterType="Custom, Numbers" ValidChars="." />
                    </td>
                    <td align="left" valign="top">
                        <asp:DropDownList ID="ddlUOM" class="SmallFont" runat="server" Width="95%" TabIndex="33">
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtNoofUnit" runat="server" class="SmallFont" Width="95%" AutoPostBack="True"
                            OnTextChanged="txtNoofUnit_TextChanged" MaxLength="5" TabIndex="34"></asp:TextBox>
                        <%--  <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtNoofUnit"
                                            Display="Dynamic" ErrorMessage="Please Enter No of Unit in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>
                        <cc11:FilteredTextBoxExtender ID="FilteredTextBoxtxtNoofUnit" runat="server" TargetControlID="txtNoofUnit"
                            FilterType="Custom, Numbers" ValidChars="." />
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtWeightofUnit" class="SmallFont" runat="server" Width="95%" Enabled="False"
                            TabIndex="35"></asp:TextBox>
                        <%-- <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtWeightofUnit"
                                            Display="None" ErrorMessage="Please Enter Weight of Unit in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>
                    </td>
                    <td align="left" valign="top" runat="server" visible="false">
                        <asp:TextBox ID="txtDofMfd" class="SmallFont" Width="95%" runat="server" TabIndex="36"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDofMfd"
                                            Display="Dynamic" ErrorMessage="Please Enter Date Of Manufacutring" SetFocusOnError="True"
                                            ValidationGroup="YM"></asp:RequiredFieldValidator>--%>
                        <cc11:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                            TargetControlID="txtDofMfd">
                        </cc11:CalendarExtender>
                        <cc11:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtDofMfd">
                        </cc11:MaskedEditExtender>
                        <br />
                    </td>
                    <td align="left" valign="top" runat="server" visible="false">
                        <asp:DropDownList ID="ddlMaterialStatus" class="SmallFont" runat="server" Width="95%"
                            TabIndex="37">
                            <%--<asp:ListItem Value="0">------Select---------</asp:ListItem>--%>
                            <%--<asp:ListItem>UnCheck</asp:ListItem>
                                            <asp:ListItem>Extracted</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>--%>
                            <asp:ListItem>Approved</asp:ListItem>
                            <asp:ListItem>Rejected</asp:ListItem>
                            <asp:ListItem>Hold</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td align="left" valign="top">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="BtnBOMSave" class="SmallFont" runat="server" OnClick="BtnBOMSave_Click"
                                        Text="Add" ValidationGroup="YM" Width="70px" CausesValidation="false" TabIndex="38" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnBOMCancel" runat="server" class="SmallFont" OnClick="BtnBOMCancel_Click"
                                        CausesValidation="false" Text="Cancel" Width="70px" TabIndex="39" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trgridopbal" runat="server" visible="false">
        <td align="left" class="td SmallFont" valign="top" width="100%">
            <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand" ShowFooter="true"
                Width="100%" OnRowDataBound="grdsub_trn_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                        <ItemTemplate>
                            <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party Name">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblParty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PRTY_NAME") %>' ToolTip='<%# Bind("PRTY_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Merge No">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lbtlotno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pallet Code">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblPalletCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PALLET_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            Total:
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C/P No">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblCPNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PALLET_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Literal ID="totalPalletNo" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Rate">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("FINAL_RATE") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                           
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QTY">
                        <ItemTemplate>
                            <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <FooterTemplate>
                            <asp:Literal ID="totalQty" runat="server" />
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No of Tubes">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Literal ID="totalNoOfUnit" runat="server" />
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Weight of Tube">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblWeightofUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date of Mfd" Visible="false">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DATE_OF_MFG","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Status" Visible="false">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="txtBOMArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issue Qty" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="txtIssueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ISS_QTY") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="lnkBOMEdit" class="SmallFont" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                Width="70px" CommandName="BOMEdit" TabIndex="12" Text="Edit" />
                            <asp:Button ID="lnkBOMDelete" class="SmallFont" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                Width="70px" CommandName="BOMDelete" OnClientClick="return confirm('Are you Sure want to delete this BOM Detail?');"
                                TabIndex="12" Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="RowStyle " />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <HeaderStyle BackColor="#336699" CssClass="SmallFont" ForeColor="White" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <%--    <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid"
                                    BorderWidth="5px" HorizontalAlign="Left">
                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No Of Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE14" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE15" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Weight Of Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE20" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdARTICLE_CODE" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdUOM" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("GRADE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lot No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdBASIS" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of Manufacturing">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdVALUE_QTY" runat="server" CssClass="SmallFont LabelNo" 
                                                        Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                </asp:Panel>
                                <cc11:hovermenuextender ID="hmeBOM" runat="server" 
                        PopupControlID="pnlBOM" TargetControlID="lnkshow"
                                    PopupPosition="Left"></cc11:hovermenuextender>--%>
        </td>
    </tr>
</table>
</td> </tr> </table> </br></br></br></br></br></br>
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="txtdescription" />
<asp:PostBackTrigger ControlID="txtLotNo" />
<asp:PostBackTrigger ControlID="txtGrade" />
<asp:PostBackTrigger ControlID="txtPalletCode" />
<asp:PostBackTrigger ControlID="imgbtnSave" />
<asp:PostBackTrigger ControlID="imgbtnUpdate" />
<%--<asp:PostBackTrigger ControlID="imgbtnClear" />
<asp:PostBackTrigger ControlID="imgbtnFind" />--%>
</Triggers>
    </asp:UpdatePanel>