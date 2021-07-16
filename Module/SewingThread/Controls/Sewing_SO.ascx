<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Sewing_SO.ascx.cs" Inherits="Module_Sewing_Thread_Controls_Sewing_SO" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>

<script language="javascript" type="text/javascript">

    function Calculation(val) {
        var name = val;

        document.getElementById('ctl00_cphBody_YARN_SO1_txtAdvanceAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_YARN_SO1_txtAdvance').value) * (parseFloat(document.getElementById('ctl00_cphBody_YARN_SO1_txtFinalTotal').value) / 100)).toFixed(3);
    }
    function SetFocus(ControlId) {
        document.getElementById(ControlId).focus();
    }
    function GetClick(ButtonId) {
        document.getElementById(ButtonId).click();
    }
   
</script>

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
        width: 150px;
    }
    .c2
    {
        margin-left: 4px;
        width: 400px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        margin-left: 4px;
        width: 60px;
    }
    .c5
    {
        margin-left: 4px;
        width: 200px;
    }
    .c6
    {
        margin-left: 4px;
        width: 300px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="tContentArial" width="95%">
            <tr>
                <td class="td tdLeft" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                                    ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" Width="48" Height="41" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" ValidationGroup="M1">
                                </asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" Width="48" Height="41" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <span class="titleheading"><b>Sewing Thread Sales Order </b></span>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label></span>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td class="td SmallFont" width="100%">
                    <table width="100%">
                        <tr style="font-weight: bold">
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label2" runat="server" CssClass="LabelNo" Text="Order Type :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" CssClass="TextBox SmallFont"
                                    Font-Bold="true" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged" TabIndex="1"
                                    Width="125px">
                                    <asp:ListItem Value="SSM">Main Order</asp:ListItem>
                                    <asp:ListItem Value="SSS">Supplimentry Order</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label3" runat="server" CssClass="LabelNo" Text="Order Number :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="TextBoxNo SmallFont" Font-Bold="true"
                                    MaxLength="10" OnTextChanged="txtOrderNumber_TextChanged1" ReadOnly="True" TabIndex="2"
                                    Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvon" runat="server" ControlToValidate="txtOrderNumber"
                                    Display="Dynamic" ErrorMessage="*Order number required" Font-Bold="False" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <cc2:ComboBox ID="ddlOrderNumber" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    DataTextField="SO_NUMB" DataValueField="SO_NUMB" EmptyText="Find Item" EnableLoadOnDemand="true"
                                    EnableVirtualScrolling="true" Height="200px" MenuWidth="450px" OnLoadingItems="ddlOrderNumber_LoadingItems"
                                    OnSelectedIndexChanged="ddlOrderNumber_SelectedIndexChanged" TabIndex="1" Width="100px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Order Number</div>
                                        <div class="header c2">
                                            Order Date</div>
                                        <div class="header c3">
                                            Party Name</div>
                                        <div class="header c1">
                                            Order Nature</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container7" runat="server" Text='<%# Eval("SO_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container8" runat="server" Text='<%# Eval("SO_DATE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Container9" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("SO_NATURE") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label4" runat="server" CssClass="LabelNo" Text="Order Date :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="TextBox SmallFont" Font-Bold="true"
                                    MaxLength="25" TabIndex="3" Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvod" runat="server" ControlToValidate="txtOrderDate"
                                    Display="Dynamic" ErrorMessage="*Order Date Required" Font-Bold="False" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtOrderDate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="Party Code :"></asp:Label>
                                &nbsp;
                            </td>
                            <td align="left" valign="top" width="17%">
                                <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" DataTextField="PRTY_CODE"
                                    DataValueField="Address" EmptyText="Select Vendor" EnableLoadOnDemand="true"
                                    Height="200px" MenuWidth="550px" OnLoadingItems="txtPartyCode_LoadingItems" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                                    Width="150px" EnableVirtualScrolling="true">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            Code</div>
                                        <div class="header c5">
                                            Name</div>
                                        <div class="header c6">
                                            Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                        </div>
                                        <div class="item c5">
                                            <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                        <div class="item c6">
                                            <asp:Literal ID="Container3" runat="server" Text='<%# Eval("Address") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td align="left" valign="top" colspan="4">
                                <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="10%" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="txtPartyAddress" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TabIndex="4" Width="87%"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label5" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" DataTextField="PRTY_CODE"
                                    DataValueField="Address" EmptyText="Select transaporter" EnableLoadOnDemand="true"
                                    Height="200px" MenuWidth="550px" OnLoadingItems="txtTransporterCode_LoadingItems"
                                    OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged" Width="150px"
                                    EnableVirtualScrolling="true">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            Code</div>
                                        <div class="header c5">
                                            Name</div>
                                        <div class="header c6">
                                            Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <asp:Literal ID="Container10" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                        </div>
                                        <div class="item c5">
                                            <asp:Literal ID="Container11" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                        <div class="item c6">
                                            <asp:Literal ID="Container12" runat="server" Text='<%# Eval("Address") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td align="left" valign="top" colspan="4">
                                <asp:TextBox ID="lblTransporterCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="10%" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="txtTransporterName" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TabIndex="5" Width="87%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                PO Nature :
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:DropDownList ID="ddlPONature" runat="server" CssClass="SmallFont TextBox">
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label15" runat="server" CssClass="LabelNo" Text="Currency Code :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:DropDownList ID="txtCurrencyCode" runat="server" CssClass="SmallFont TextBox"
                                    Width="80%">
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label17" runat="server" CssClass="LabelNo" Text="Conversion Rate :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtconversionRate" runat="server" CssClass="SmallFont TextBoxNo"
                                    TabIndex="15" Width="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label7" runat="server" CssClass="LabelNo" Text="Delivery Date :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="SmallFont gCtrTxt" MaxLength="25"
                                    TabIndex="7" Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvd" runat="server" ControlToValidate="txtDeliveryDate"
                                    Display="Dynamic" ErrorMessage="*Del Date required" Font-Bold="False" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label8" runat="server" CssClass="LabelNo" Text="Delivery Address :"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="3" style="width: 32%">
                                <asp:TextBox ID="txtDelAddress" runat="server" CssClass="SmallFont gCtrTxt" TabIndex="8"
                                    Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label6" runat="server" CssClass="LabelNo" Text="Pay Term :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtPayTerm" runat="server" CssClass="SmallFont gCtrTxt" MaxLength="50"
                                    TabIndex="6" Width="99%"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label9" runat="server" CssClass="LabelNo" Text="Despatch Mode :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtDespatchMode" runat="server" CssClass="SmallFont gCtrTxt" MaxLength="200"
                                    TabIndex="9" Width="99%"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <cc1:CalendarExtender ID="CE2" runat="server" TargetControlID="txtDeliveryDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <cc1:CalendarExtender ID="CE3" runat="server" TargetControlID="txtTrnDeliveryDate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label14" runat="server" CssClass="LabelNo" Text="Remarks :"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="5">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="SmallFont gCtrTxt" MaxLength="500"
                                    TabIndex="14" TextMode="SingleLine" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label16" runat="server" CssClass="LabelNo" Text="Instructions :"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="5">
                                <asp:TextBox ID="txtInstructions" runat="server" CssClass="SmallFont gCtrTxt" MaxLength="500"
                                    TabIndex="14" TextMode="SingleLine" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="td SmallFont" valign="top" width="100%">
                    <table width="100%">
                        <tr bgcolor="#336699">
                            <td class="tdLeft SmallFont" align="left" width="15%">
                                <asp:Label ID="Label18" runat="server" CssClass="LabelNo titleheading" Text="Sewing Thread Code"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont" align="left" width="10%">
                                <asp:Label ID="Label29" runat="server" CssClass="LabelNo titleheading" Text="Shade Code"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont" align="left" width="10%">
                                <asp:Label ID="Label30" runat="server" CssClass="LabelNo titleheading" Text="Base UOM"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont" align="left" width="10%">
                                <asp:Label ID="Label32" runat="server" CssClass="LabelNo titleheading" Text="Unit Weight"></asp:Label>
                            </td>
                            <%-- <td class="tdLeft SmallFont" align="left" width="7%">
                        <asp:Label ID="Label31" runat="server" CssClass="LabelNo titleheading" Text="No Of Unit"></asp:Label>
                    </td>--%>
                            <td class="tdRight SmallFont" width="7%">
                                <asp:Label ID="Label21" runat="server" CssClass="LabelNo titleheading" Text="Quantity"></asp:Label>
                            </td>
                            <td class="tdRight SmallFont" width="7%">
                                <asp:Label ID="Label23" runat="server" CssClass="LabelNo titleheading" Text="Basic Rate"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont" width="7%">
                                <asp:Label ID="Label24" runat="server" CssClass="LabelNo titleheading" Text="Dis./Tax"></asp:Label>
                            </td>
                            <td class="tdRight SmallFont" width="7%">
                                <asp:Label ID="Label25" runat="server" CssClass="LabelNo titleheading" Text="Final Rate"></asp:Label>
                            </td>
                            <td class="tdRight SmallFont" width="7%">
                                <asp:Label ID="Label26" runat="server" CssClass="LabelNo titleheading" Text="Amount"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont" width="7%">
                                <asp:Label ID="Label28" runat="server" CssClass="LabelNo titleheading" Text="Delivery Date"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont" width="7%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    DataTextField="YARN_CODE" DataValueField="YARN_CODE" EmptyText="Find Item" EnableLoadOnDemand="true"
                                    Height="200px" MenuWidth="800px" OnLoadingItems="txtItemCode_LoadingItems" OnSelectedIndexChanged="txtItemCode_SelectedIndexChanged"
                                    TabIndex="1" Width="100%">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            YARN CODE</div>
                                        <div class="header c1">
                                            YARN CAT</div>
                                        <div class="header c2">
                                            DESCRIPTION</div>
                                        <div class="header c3">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("YARN_CODE") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("YARN_CAT") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container5" runat="server" Text='<%# Eval("YARN_DESC") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Container6" runat="server" Text='<%# Eval("YARN_TYPE") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdLeft" width="10%">
                                <asp:DropDownList ID="ddlshadeCode" runat="server" Width="99%" TabIndex="12" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlshadeCode_SelectedIndexChanged" CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                            <td class="tdLeft" width="10%">
                                <%--   <asp:DropDownList ID="ddlBaseUOM" runat="server" Width="99%" TabIndex="12" AutoPostBack="True"
                            CssClass="SmallFont" OnSelectedIndexChanged="ddlBaseUOM_SelectedIndexChanged">
                            <asp:ListItem>BASE UNIT</asp:ListItem>
                            <asp:ListItem>BOX</asp:ListItem>
                            <asp:ListItem>CARTON</asp:ListItem>
                        </asp:DropDownList>--%>
                                <asp:TextBox ID="txtUnit" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                    ReadOnly="true" TabIndex="22" Text='<%# Bind("UOM") %>' Width="100px"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtUnitWeight" runat="server" AutoPostBack="True" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                    OnTextChanged="txtOrderQty1_TextChanged" TabIndex="21" Width="100%" ReadOnly="True"></asp:TextBox>
                            </td>
                            <%-- <td class="tdLeft" width="7%">--%>
                            <asp:TextBox ID="txtNoOfUnit" Visible="false" runat="server" AutoPostBack="True"
                                CssClass="TextBoxNo SmallFont" OnTextChanged="txtOrderQty1_TextChanged" TabIndex="21"
                                Width="100%"></asp:TextBox>
                            <%--</td>--%>
                            <td class="tdRight" width="7%">
                                <asp:TextBox ID="txtOrderQty" runat="server" CssClass=" TextBoxNo SmallFont" TabIndex="21"
                                    Width="100%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:TextBox ID="txtBaseRate" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                    OnTextChanged="txtBaseRate_TextChanged1" TabIndex="23" Text='<%# Bind("BASIC_RATE") %>'
                                    Width="100%"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:Button ID="btnDiscountTaxes" runat="server" Font-Size="8pt" OnClick="btnDiscountTaxes_Click1"
                                    TabIndex="24" Text="Disc/Taxes" Width="100%" />
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:TextBox ID="txtFinalRate" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                    OnTextChanged="txtFinalRate_TextChanged" ReadOnly="true" TabIndex="25" Text='<%# Bind("FINAL_RATE") %>'
                                    Width="100%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo SmallFont TextBoxDisplay "
                                    ReadOnly="true" TabIndex="26" Text='<%# Bind("Amount") %>' Width="100%"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtTrnDeliveryDate" runat="server" CssClass="TextBox SmallFont"
                                    TabIndex="28" Text='<%# Bind("DEL_DATE") %>' Width="100%"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:Button ID="btnSaveDetail" runat="server" OnClick="btnSaveDetail_Click" Text="Save"
                                    Width="48%" />
                                <asp:Button ID="btnCancelDetail" runat="server" OnClick="btnCancelDetail_Click" Text="Cancel"
                                    Width="48%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft" colspan="11" width="93%">
                                &nbsp;Article Code/Desc :<asp:TextBox ID="lblItemCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="100px" ReadOnly="true"></asp:TextBox>
                                <asp:TextBox ID="txtItemDescription" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                    ReadOnly="true" TabIndex="19" Text='<%# Bind("YARN_DESC") %>' Width="247px"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td class="tdLeft" width="7%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trMaterialPOCreditTRN" runat="server">
                <td class="td SmallFont" width="100%">
                    <asp:GridView ID="gvMaterialSOTRN" runat="server" AllowPaging="false" AllowSorting="True"
                        AutoGenerateColumns="False" CssClass="SmallFont" OnRowCommand="gvMaterialSOTRN_RowCommand"
                        TabIndex="16">
                        <RowStyle CssClass="SmallFont" Width="98%" />
                        <Columns>
                            <asp:TemplateField HeaderText="Yarn Code">
                                <ItemTemplate>
                                    <asp:Label ID="txtItemCode" runat="server" AutoCompleteType="Disabled" CssClass="Label SmallFont"
                                        Font-Bold="true" ReadOnly="true" TabIndex="17" Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Yarn Description">
                                <ItemTemplate>
                                    <asp:Label ID="txtItemDescription" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                        TabIndex="19" Text='<%# Bind("YARN_DESC") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shade">
                                <ItemTemplate>
                                    <asp:Label ID="txtSHADE" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                        TabIndex="19" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Base UOM">
                                <ItemTemplate>
                                    <asp:Label ID="txtBaseUOM" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                        TabIndex="19" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Weight">
                                <ItemTemplate>
                                    <asp:Label ID="txtUnitWeight" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                        TabIndex="19" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="txtOrderQty1" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                        ReadOnly="true" TabIndex="21" Text='<%# Bind("ORD_QTY") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:Label ID="txtUnit" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                        TabIndex="22" Text='<%# Bind("UOM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Base Rate">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="txtBaseRate" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                        TabIndex="23" Text='<%# Bind("BASIC_RATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Rate">
                                <ItemTemplate>
                                    <asp:Label ID="txtFinalRate" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                        ReadOnly="true" TabIndex="25" Text='<%# Bind("FINAL_RATE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                        ReadOnly="true" TabIndex="26" Text='<%# Bind("Amount") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Del Date">
                                <ItemTemplate>
                                    <asp:Label ID="txtTrnDeliveryDate" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                        TabIndex="28" Text='<%# Bind("DEL_DATE", "{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                        CommandName="SOMateialCreditEdit" TabIndex="29" Text="Edit"></asp:LinkButton>
                                    /
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                        CommandName="SOMateialCreditDelete" TabIndex="29" Text="Del"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="SmallFont" />
                        <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
