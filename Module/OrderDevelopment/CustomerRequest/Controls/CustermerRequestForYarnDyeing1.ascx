<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustermerRequestForYarnDyeing1.ascx.cs"
    Inherits="Module_OrderDevelopment_CustomerRequest_Controls_CustermerRequestForYarnDyeing1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Src="~/CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"
    TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/LOV/ApproveLRLOV.ascx" TagName="ApproveLRLOV"
    TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<style type="text/css">
    .item {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1;
        *display: inline;
        overflow: hidden;
        white-space: nowrap;
    }

    .header {
        margin-left: 4px;
    }

    .c1 {
        width: 100px;
    }

    .c2 {
        margin-left: 4px;
        width: 300px;
    }

    .c3 {
        width: 200px;
    }

    .c4 {
        margin-left: 4px;
        width: 300px;
    }

    .c5 {
        width: 200px;
    }

    .d1 {
        width: 80px;
    }

    .d2 {
        margin-left: 4px;
        width: 180px;
    }

    .d3 {
        margin-left: 4px;
        width: 180px;
    }

    .d5 {
        margin-left: 4px;
        width: 150px;
    }

    .AutoExtender {
        font-family: Verdana, Helvetica, sans-serif;
        font-size: .8em;
        font-weight: normal;
        border: solid 1px #006699;
        line-height: 20px;
        padding: 10px;
        background-color: White;
        margin-left: 10px;
    }

    .AutoExtenderList {
        border-bottom: dotted 1px #006699;
        cursor: pointer;
        color: Maroon;
    }

    .AutoExtenderHighlight {
        color: White;
        background-color: #006699;
        cursor: pointer;
    }

    #divwidth {
        width: 200px !important;
    }

        #divwidth div {
            width: 200px !important;
        }

    .SmallFont {
    }
</style>
<style type="text/css">
    .AutoExtender {
        font-family: Verdana, Helvetica, Sans-Serif;
        font-size: .8em;
        font-weight: normal;
        line-height: 20px;
        padding: 10px;
        background-color: White;
        margin-left: 10px;
    }

    .AutoExtenderList {
        border-bottom: dotted 1px #006699;
        cursor: pointer;
        color: Maroon;
    }

    .AutoExtenderHighlight {
        color: White;
        background-color: #006699;
        cursor: pointer;
    }

    #divwidth {
        width: 200px !important;
    }

        #divwidth div {
            width: 200px !important;
        }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial" width="100%">
            <tr>
                <td align="left" valign="top" width="100%">
                    <table class="td">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                    ToolTip="Save" ValidationGroup="CR" OnClick="imgbtnSave_Click" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                    ToolTip="Update" ValidationGroup="CR" OnClick="imgbtnUpdate_Click" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/CommonImages/del6.png"
                                    OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                                    ToolTip="Delete" ValidationGroup="M1" Width="48px" OnClick="imgbtnDelete_Click" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                    ToolTip="Find" OnClick="imgbtnFind_Click" />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" OnClick="imgbtnPrint_Click" />
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" OnClick="imgbtnClear_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" OnClick="imgbtnExit_Click" />
                            </td>
                            <td style="font-style: italic">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <span class="titleheading"><b>Sales Order For Yarn Dyeing</b></span>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" valign="top" width="100%">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="CR" />
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%" class="td">
                        <tr class="td">
                            <td align="right" width="10%">Business Type :
                            </td>
                            <td align="left" width="10%">
                                <asp:DropDownList Width="150px" CssClass="SmallFont TextBox UpperCase" ID="ddlBusinessType"
                                    AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged"
                                    AppendDataBoundItems="True" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDLBusinessType"
                                    Display="None" ErrorMessage="Please Select Business Type" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" width="17%">Order Type:
                            </td>
                            <td align="left" width="17%">
                                <asp:DropDownList Width="150px" TabIndex="2" CssClass="SmallFont TextBox UpperCase"
                                    ID="ddlOrderType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLOrderType"
                                    Display="None" ErrorMessage="Please Select Order Type " InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" width="17%">Delivery Mode:
                            </td>
                            <td align="left" width="15%">
                                <asp:DropDownList Width="150px" TabIndex="3" CssClass="SmallFont TextBox UpperCase"
                                    ID="ddlDeliveryMode" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="10%">Order No :
                            </td>
                            <td align="left" width="10%">
                                <asp:TextBox ID="txtOrderNo" TabIndex="4" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay"
                                    ValidationGroup="M1" ReadOnly="True"></asp:TextBox>
                                <cc2:ComboBox ID="cmbOrderNo" TabIndex="4" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    EmptyText="Select CR No" DataTextField="ORDER_NO" DataValueField="Combined" EnableLoadOnDemand="True"
                                    MenuWidth="800" OnLoadingItems="cmbOrderNo_LoadingItems" OnSelectedIndexChanged="cmbOrderNo_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" OpenOnFocus="true" Width="150px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c5">
                                            CR No
                                        </div>
                                        <div class="header c5">
                                            CR Location
                                        </div>
                                        <div class="header c5">
                                            CR Type
                                        </div>
                                        <div class="header c5">
                                            CR date
                                        </div>
                                        <div class="header d1">
                                            Party
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c5">
                                            <%# Eval("ORDER_NO")%>
                                        </div>
                                        <div class="item c5">
                                            <%# Eval("ORDER_CAT")%>
                                        </div>
                                        <div class="item c5">
                                            <%# Eval("ORDER_TYPE")%>
                                        </div>
                                        <div class="item c5">
                                            <%# Eval("ORDER_DATE")%>
                                        </div>
                                        <div class="item d1">
                                            <%# Eval("PRTY_NAME")%>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                          <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                          <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td align="right" width="17%">Order Date :
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtDate" runat="server" TabIndex="5" Width="150px" MaxLength="10"
                                    CssClass="SmallFont TextBoxDisplay UpperCase" ValidationGroup="M1"></asp:TextBox>
                            </td>
                            <td align="right" width="17%">Branch:
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtCrLocation" runat="server" ReadOnly="true" TabIndex="6" Width="150px"
                                    CssClass="SmallFont TextBox UpperCase TextBoxDisplay" ValidationGroup="M1"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="10%">Billing Mode:
                            </td>
                            <td align="left" width="10%">
                                <asp:DropDownList Width="150px" CssClass="SmallFont TextBox UpperCase" ID="ddlBillingMode"
                                    runat="server" AppendDataBoundItems="True" TabIndex="10" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlBillingMode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right" width="17%">Agent:
                            </td>
                            <td align="left" width="17%">
                                <cc2:ComboBox ID="txtBrokerCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtBrokerCode_LoadingItems" DataTextField="PRTY_NAME" DataValueField="PRTY_NAME"
                                    EmptyText="Select Agent" EnableVirtualScrolling="true" Width="150px" MenuWidth="400px"
                                    Height="200px" TabIndex="11" ValidationGroup="M1">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code
                                        </div>
                                        <div class="header c3">
                                            NAME
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                          <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                          <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td align="right" width="17%">&nbsp;
                            </td>
                            <td align="left" width="15%">
                                <asp:Button ID="btnOther" runat="server" TabIndex="12" Text="Show Other" OnClick="btnOther_Click"
                                    Width="150px" CssClass="SmallFont" />
                            </td>
                        </tr>
                        <tr id="trOther" runat="server" visible="false">
                            <td class="tdRight" colspan="6">
                                <asp:Panel ID="pnlOtherDTL" runat="server" BackColor="LightGray" BorderStyle="Ridge"
                                    BorderWidth="5px">
                                    <table>
                                        <tr>
                                            <td align="right" cssclass="SmallFont">Pre - Carriage By :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPreCarriage" runat="server" CssClass="TextBox SmallFont" TabIndex="13"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" cssclass="SmallFont">Place of receipt :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPlaceToReceipt" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="14" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" cssclass="SmallFont">Port of Loading :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPortofLoading" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="15" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" cssclass="SmallFont">Port of Discharge :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPortOfDischarge" runat="server" CssClass="TextBox SmallFont"
                                                    MaxLength="50" TabIndex="16" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" cssclass="SmallFont">Mark & Nos. Container No. :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNoOfContainer" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="17" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" cssclass="SmallFont">Exporter's Ref:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtnoofpackages" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="18" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" cssclass="SmallFont">SC/ NO. :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLCNo" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="19" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" cssclass="SmallFont">SC/NO. Date :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLCDate" runat="server" CssClass="TextBox SmallFont" TabIndex="20"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" cssclass="SmallFont">Vessel No. :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtvesselNo" runat="server" CssClass="TextBox SmallFont" TabIndex="21"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" cssclass="SmallFont">Buyer's Date :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="TextBox SmallFont" TabIndex="22"
                                                    Width="80px"></asp:TextBox>
                                            </td>

                                            <tr>
                                                <td align="right" cssclass="SmallFont">No & kind Of Packing:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtpackages" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                        TabIndex="23" Width="80px"></asp:TextBox>
                                                </td>
                                                <td align="right" cssclass="SmallFont">Final Destination
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDestination" runat="server" CssClass="TextBox SmallFont" MaxLength="75"
                                                        TabIndex="24" Width="80px"></asp:TextBox>
                                                </td>
                                                <td align="right" cssclass="SmallFont">Country of Origin Of Goods.
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TxtofOG" runat="server" CssClass="TextBox SmallFont" MaxLength="75"
                                                        TabIndex="25" Width="80px"></asp:TextBox>
                                                </td>
                                                <td align="right" cssclass="SmallFont">Country of Final Destination.
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="TxtofFD" runat="server" CssClass="TextBox SmallFont" MaxLength="75"
                                                        TabIndex="26" Width="80px"></asp:TextBox>
                                                </td>
                                                <td align="right" cssclass="SmallFont">Delevery:
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDelevery" runat="server" CssClass="TextBox SmallFont" MaxLength="100"
                                                        TabIndex="27" Width="80px" Text="WITHIN 4-5 DAYS."></asp:TextBox>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td align="center" colspan="8">
                                                <asp:Button ID="btncncelpack" runat="server" OnClick="btncncelpack_Click" TabIndex="28"
                                                    Text="Hide Others" ValidationGroup="M1" Width="100px" />
                                            </td>

                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                </td>
            </tr>
            <tr>
                <td align="right" class="" width="10%">Customer Name :
                </td>
                <td align="left" colspan="4">
                    <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" DataTextField="PRTY_CODE"
                        DataValueField="PRTY_NAME" EmptyText="Select Party" EnableLoadOnDemand="true"
                        Height="200px" MenuWidth="700px" OnLoadingItems="cmbPartyCode_LoadingItems" OnSelectedIndexChanged="cmbPartyCode_SelectedIndexChanged"
                        TabIndex="29" Width="128px" EnableVirtualScrolling="true">
                        <HeaderTemplate>
                            <div class="header c1">
                                Party Code
                            </div>
                            <div class="header c2">
                                PARTY NAME
                            </div>
                            <div class="header d2">
                                PARTY Address
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                            </div>
                            <div class="item c2">
                                <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                            </div>
                            <div class="item d2">
                                <asp:Literal ID="Container3" runat="server" Text='<%# Eval("Address") %>' />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                              <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                              <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>

                    <asp:TextBox ID="txtPartyCode" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                        ReadOnly="true" ValidationGroup="M1" Width="80px"></asp:TextBox>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                        ReadOnly="true" ValidationGroup="M1" Width="550px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="" width="10%">Consignee Name :
                </td>
                <td align="left" colspan="4">
                    <cc2:ComboBox ID="cmbConsignee" runat="server" AutoPostBack="True" DataTextField="PRTY_CODE"
                        DataValueField="PRTY_NAME" EmptyText="Select Consignee" EnableLoadOnDemand="true"
                        Height="200px" MenuWidth="700px" OnLoadingItems="cmbConsignee_LoadingItems" OnSelectedIndexChanged="cmbConsignee_SelectedIndexChanged"
                        TabIndex="8" Width="128px" EnableVirtualScrolling="true">
                        <HeaderTemplate>
                            <div class="header c1">
                                Code
                            </div>
                            <div class="header c2">
                                Name
                            </div>
                            <div class="header d2">
                                Address
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                            </div>
                            <div class="item c2">
                                <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                            </div>
                            <div class="item d2">
                                <asp:Literal ID="Container3" runat="server" Text='<%# Eval("Address") %>' />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                              <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                              <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                    <%-- </td>
                            <td align="left" class="tdLeft" width="64%">--%>
                    <asp:TextBox ID="txtConsigneeCode" TabIndex="7" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                        ReadOnly="true" ValidationGroup="M1" Width="80px"></asp:TextBox>
                    <asp:TextBox ID="txtConsigneeAddress" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                        ReadOnly="true" TabIndex="8" ValidationGroup="M1" Width="550px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="" width="10%">Transporter:
                </td>
                <td align="left" colspan="4">
                    <cc2:ComboBox ID="cmbTransporter" runat="server" AutoPostBack="True" DataTextField="PRTY_CODE"
                        DataValueField="PRTY_NAME" EmptyText="Select Transporter" EnableLoadOnDemand="true"
                        Height="200px" MenuWidth="700px" OnLoadingItems="cmbTransporter_LoadingItems"
                        OnSelectedIndexChanged="cmbTransporter_SelectedIndexChanged" TabIndex="8" Width="128px"
                        EnableVirtualScrolling="true">
                        <HeaderTemplate>
                            <div class="header c1">
                                Code
                            </div>
                            <div class="header c2">
                                Name
                            </div>
                            <div class="header d2">
                                Address
                            </div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                            </div>
                            <div class="item c2">
                                <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                            </div>
                            <div class="item d2">
                                <asp:Literal ID="Container3" runat="server" Text='<%# Eval("Address") %>' />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                              <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                              <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                    <%-- </td>
                            <td align="left" colspan="4" class="tdLeft" width="64%">--%>
                    <asp:TextBox ID="txtTransporterCode" TabIndex="7" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                        ReadOnly="true" ValidationGroup="M1" Width="80px"></asp:TextBox>
                    <asp:TextBox ID="txtTransporterAddress" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                        ReadOnly="true" TabIndex="8" ValidationGroup="M1" Width="550px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <%-- &nbsp;Direct Billing :--%>Tolerance :
                </td>
                <td align="left" id="tdDirectBilling" runat="server">
                    <asp:TextBox ID="txtDirectBilling" runat="server" TabIndex="30" Width="150px" CssClass="SmallFont TextBox UpperCase"
                        ValidationGroup="M1" MaxLength="45" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtTolerance" runat="server" TabIndex="30" Width="150px" CssClass="SmallFont TextBox UpperCase"
                        ValidationGroup="M1" MaxLength="45"></asp:TextBox>
                </td>
                <td align="right">Party PO No:
                </td>
                <td align="left">
                    <asp:TextBox ID="txtCustomerReffNo" TabIndex="31" runat="server" Width="150px" CssClass="SmallFont TextBox UpperCase"
                        ValidationGroup="M1" MaxLength="24"></asp:TextBox>
                </td>
                <td align="right">PO Date :
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtDocumentDate" runat="server" TabIndex="32" Width="150px" CssClass="SmallFont TextBox UpperCase"
                        ValidationGroup="M1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvod" runat="server" ControlToValidate="TxtDocumentDate"
                        Display="Dynamic" ErrorMessage="*Please select the Document Date" Font-Bold="False"
                        ValidationGroup="CR"></asp:RequiredFieldValidator>
                    <cc3:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="TxtDocumentDate" PromptCharacter="_">
                    </cc3:MaskedEditExtender>
                    <cc3:CalendarExtender ID="CE1" Format="dd/MM/yyyy" runat="server" TargetControlID="TxtDocumentDate">
                    </cc3:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="tdRight" width="5%">
                    <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode :" CssClass="SmallFont"></asp:Label>
                </td>
                <td class="tdLeft" width="5%">
                    <asp:DropDownList ID="ddlPaymentMode" runat="server" MenuWidth="150px" CssClass="SmallFont"
                        Width="150px" OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged" TabIndex="33">
                        <asp:ListItem Text="CHEQUE" Value="CHEQUE"></asp:ListItem>
                        <asp:ListItem Text="CASH" Value="CASH"></asp:ListItem>
                        <asp:ListItem Text="RTGS" Value="RTGS"></asp:ListItem>
                        <asp:ListItem Text="NEFT" Value="NEFT"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdRight" width="12%">
                    <asp:Label ID="lblPaymentTerm" runat="server" Text="Payment Term :" CssClass="SmallFont"></asp:Label>
                </td>
                <td class="tdLeft" width="64%" colspan="3">
                    <asp:TextBox ID="txtPaymentTerm" runat="server" CssClass="TextBox SmallFont" Width="90%"
                        MaxLength="450" TabIndex="34"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdRight" width="10%">
                    <asp:Label ID="lblBillTo" runat="server" Text="Bill To :" CssClass="SmallFont" Visible="false"></asp:Label>
                </td>
                <td class="tdLeft" width="10%" colspan="5">
                    <asp:TextBox ID="txtBillTo" runat="server" CssClass="TextBox SmallFont" Width="94%"
                        MaxLength="450" TabIndex="35" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdRight" width="10%">
                    <asp:Label ID="Label1" Visible="false" runat="server" Text="Delevered To:" CssClass="SmallFont"></asp:Label>
                </td>
                <td class="tdLeft" width="10%" colspan="5">
                    <asp:TextBox ID="txtDeleveredTo" runat="server" Visible="false" CssClass="TextBox SmallFont"
                        Width="95%" MaxLength="450" TabIndex="36"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="17%">&nbsp;Party Specific Quality:
                </td>
                <td align="left" width="83%" colspan="5">
                    <asp:TextBox ID="txtMstRemarks" runat="server" TabIndex="38" CssClass="SmallFont TextBox UpperCase"
                        ValidationGroup="M1" Width="94%" MaxLength="450"></asp:TextBox>
                </td>
                <td class="tdRight" width="10%" visible="false">
                    <asp:Label ID="Label2" runat="server" Visible="false" Text="Packing Process:" CssClass="SmallFont"></asp:Label>
                </td>
                <td class="tdLeft" width="10%" visible="false">
                    <asp:DropDownList ID="ddlTaxAgainst" Visible="false" runat="server" Width="150px"
                        CssClass="SmallFont" TabIndex="37">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="CCS" runat="server">
                <td align="right" width="10%">
                    <asp:Label ID="lblCurrencyCode" runat="server" Text="Currency Code :" CssClass="SmallFont BoldFont"
                        Visible="false"></asp:Label>
                </td>
                <td align="left" width="10%">
                    <asp:DropDownList ID="ddlCurrencyCode" runat="server" CssClass="SmallFont TextBox UpperCase"
                        Width="150px" OnSelectedIndexChanged="ddlCurrencyCode_SelectedIndexChanged" TabIndex="7"
                        Visible="false">
                    </asp:DropDownList>
                </td>
                <td align="right" width="17%">
                    <asp:Label ID="lblConversionRate" runat="server" Text="Package Weight :" CssClass="SmallFont BoldFont"></asp:Label>
                </td>
                <td align="left" width="17%">
                    <asp:TextBox ID="txtConversionRate" runat="server" Width="150px" MaxLength="4" CssClass="SmallFont TextBox UpperCase"
                        TabIndex="8"></asp:TextBox>
                </td>
                <td align="right" width="17%">
                    <asp:Label ID="lblShipment" runat="server" Text="Shipment :" CssClass="SmallFont"
                        Visible="false"></asp:Label>
                </td>
                <td align="left" width="17%">
                    <asp:TextBox ID="txtShipment" runat="server" Width="150px" MaxLength="250" CssClass="SmallFont TextBox UpperCase"
                        TabIndex="9" Visible="false"></asp:TextBox>
                </td>
            </tr>

        </table>
        </td> </tr>
        <tr>
            <td width="100%" class="td">
                <table width="100%">
                    <tr>
                        <td class="td" width="100%">
                            <table width="100%" class="td">
                                <tr bgcolor="#006699">
                                    <td width="10%" align="center">
                                        <span class="titleheading"><b>Yarn&nbsp;Code</b></span>
                                    </td>
                                    <td width="5%" align="center">
                                        <span class="titleheading"><b>Shade&nbsp;No</b></span>
                                    </td>
                                    <td width="7%" visible="TRUE">
                                        <span class="titleheading"><b>Last&nbsp;Order</b></span>
                                    </td>
                                    <td width="10%" align="center">
                                        <span class="titleheading"><b>Pack&nbsp;Process</b></span>
                                    </td>
                                    <td width="10%" align="center">
                                        <span class="titleheading"><b>GRADE&nbsp;</b></span>
                                    </td>
                                    <td class="tdRight" width="10%" align="center">
                                        <span class="titleheading"><b>Quantity</b></span>
                                    </td>
                                    <td width="10%" align="center">
                                        <span class="titleheading"><b>&nbsp;Rate/Kg</b></span>
                                    </td>
                                    <td width="10%" align="center">
                                        <span class="titleheading"><b>Tax/Disc.</b></span>
                                    </td>
                                    <td width="10%" align="center">
                                        <span class="titleheading"><b>Dis</b></span>
                                    </td>
                                    <td width="10%" align="center">
                                        <span class="titleheading"><b>Freight</b></span>
                                    </td>
                                    <td width="10%" align="center">
                                        <span class="titleheading"><b>Net Rate</b></span>
                                    </td>
                                    <td width="7%" align="center">
                                        <span class="titleheading"><b>SGST</b></span>
                                    </td>
                                    <td width="7%" align="center">
                                        <span class="titleheading"><b>CGST</b></span>
                                    </td>
                                    <td width="7%" align="center">
                                        <span class="titleheading"><b>IGST</b></span>
                                    </td>
                                    <td width="10%" align="center">
                                        <span class="titleheading"><b>Rate&nbsp;&&nbsp;Tax</b></span>
                                    </td>
                                    <td width="5%" align="center">
                                        <span class="titleheading"><b>Total&nbsp;Amount</b></span>
                                    </td>
                                    <td width="5%" align="center">
                                        <span class="titleheading"><b>Exp&nbsp;Dispatch&nbsp;Date</b></span>
                                    </td>
                                    <td width="20%" align="center">
                                        <span class="titleheading"><b>Reff. No</b></span>
                                    </td>
                                    <td width="20%" align="center">
                                        <span class="titleheading"><b>Machine</b></span>
                                    </td>
                                    <td width="20%" align="center">
                                        <span class="titleheading"><b>Remarks</b></span>
                                    </td>
                                    <td width="20%"></td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" width="10%">
                                        <cc2:ComboBox ID="cmbArticleNo" runat="server" AutoPostBack="True" CssClass="smallfont"
                                            DataTextField="YARN_CODE" DataValueField="Combined" EnableLoadOnDemand="True"
                                            MenuWidth="700" OnLoadingItems="cmbArticleNo_LoadingItems" OnSelectedIndexChanged="cmbArticleNo_SelectedIndexChanged"
                                            EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="39" Visible="true"
                                            Width="100%" Height="200px">
                                            <HeaderTemplate>
                                                <div class="header c1">
                                                    Yarn Code
                                                </div>
                                                <div class="header c4">
                                                    Yarn Desc
                                                </div>
                                                <div class="header c3">
                                                    Dyed/Non Dyed
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c1">
                                                    <%# Eval("YARN_CODE") %>
                                                </div>
                                                <div class="item c4">
                                                    <%# Eval("YARN_DESC") %>
                                                </div>
                                                <div class="item c3">
                                                    <%# Eval("YARN_TYPE")%>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                  <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                  <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>
                                    </td>
                                    <td align="left" valign="top" width="7%">
                                        <asp:DropDownList ID="ddlYarnShade" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                            TabIndex="40" Width="127" OnSelectedIndexChanged="ddlYarnShade_SelectedIndexChanged"
                                            AutoPostBack="true" Visible="false">
                                        </asp:DropDownList>
                                        <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                            DataTextField="SHADE_CODE" DataValueField="Combined" EnableLoadOnDemand="True"
                                            MenuWidth="400" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="40"
                                            Height="200px" Visible="true" Width="80%" OnLoadingItems="cmbShade_LoadingItems"
                                            OnSelectedIndexChanged="cmbShade_SelectedIndexChanged">
                                            <HeaderTemplate>
                                                <div class="header d2">
                                                    Shade Family Name
                                                </div>
                                                <div class="header d4">
                                                    Shade Name
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item d2">
                                                    <%# Eval("SHADE_FAMILY_CODE")%>
                                                </div>
                                                <div class="item d4">
                                                    <%# Eval("SHADE_CODE")%>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                  <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                  <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>
                                    </td>
                                    <td align="left" valign="top" width="7%" visible="TRUE">
                                        <uc2:ApproveLRLOV ID="txtMatchingReff" Width="100%" TabIndex="41" runat="server"
                                            Visible="false" />
                                        <asp:TextBox ID="TxtLastDate" runat="server" CssClass="SmallFont TextBoxNo" Enabled="false" Width="95%" TabIndex="43"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList CssClass="SmallFont TextBox UpperCase" TabIndex="41" ID="txtEndUse"
                                            runat="server" AppendDataBoundItems="true" Width="100%" Visible="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList CssClass="SmallFont TextBox UpperCase" TabIndex="41" ID="txtGrade"
                                            runat="server" AppendDataBoundItems="true" Width="100%" Visible="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" valign="top" width="10%">
                                        <asp:TextBox ID="txtNoofUnit" runat="server" CssClass="SmallFont TextBoxNo" Width="95%"
                                            TabIndex="42" OnTextChanged="txtNoofUnit_TextChanged" MaxLength="8"></asp:TextBox>
                                        <cc3:FilteredTextBoxExtender ID="FiltertxtNoofUnit" runat="server" TargetControlID="txtNoofUnit"
                                            FilterType="Custom, Numbers" ValidChars="." />
                                    </td>
                                    <td align="left" valign="top" width="10%">
                                        <asp:TextBox ID="txtSaleRate" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="6"
                                            OnTextChanged="txtsalerate_TextChanged" AutoPostBack="True" Width="95%" TabIndex="43"></asp:TextBox>
                                        <cc3:FilteredTextBoxExtender ID="FilteredTexttxtSaleRate" runat="server" TargetControlID="txtSaleRate"
                                            FilterType="Custom, Numbers" ValidChars="." />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDisc" runat="server" Text="Disc/Taxes" OnClick="btnDisc_Click"
                                            CssClass="SmallFont " Width="100%" TabIndex="44" />

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscount" runat="server" CssClass="SmallFont TextboxNo" Width="95%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFright" runat="server" CssClass="SmallFont TextboxNo" Width="95%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNetRate" runat="server" CssClass="SmallFont TextboxNo" Width="95%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSGST" runat="server" CssClass="SmallFont TextboxNo" Width="95%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCGST" runat="server" CssClass="SmallFont TextboxNo" Width="80%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIGST" runat="server" CssClass="SmallFont TextboxNo" Width="95%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfinal" runat="server" CssClass="SmallFont TextboxNo" OnTextChanged="txtfinal_TextChanged"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotalCost" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                            Width="95%" OnTextChanged="txtTotalCost_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" width="10%">
                                        <asp:TextBox ID="txtReqDate" runat="server" TabIndex="46" Width="95%" CssClass="SmallFont TextBox UpperCase"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" width="20%" rowspan="2">
                                        <asp:TextBox ID="txtShadeReffNo" runat="server" TabIndex="47" CssClass="SmallFont"
                                            Width="80px"></asp:TextBox>
                                    </td>
                                    <td class="tdLeft">
                                        <asp:Button ID="btnMachineBatch" runat="server" Text="Machine" CssClass="SmallFont "
                                            Width="100%" OnClick="btnMachine_Click" />
                                    </td>
                                    <td align="left" valign="top" width="20%" rowspan="2">
                                        <asp:TextBox ID="txtRemarks" runat="server" TabIndex="47" CssClass="SmallFont" Width="95%"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top" width="10%">
                                        <asp:Button ID="btnSTSave" runat="server" CssClass="SmallFont" OnClick="btnSTSave_Click"
                                            Text=" Add " Width="60px" TabIndex="49" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" valign="top" colspan="20" width="90%">Quality&nbsp;Desc&nbsp;:
                                        <asp:TextBox ID="txtParyItemDesc" runat="server" Width="250px" CssClass="TextBox  SmallFont"
                                            Font-Bold="False" TabIndex="48"></asp:TextBox>&nbsp; Code/Desc :<asp:TextBox ID="txtItemCodeLabel"
                                                runat="server" Width="80px" CssClass="TextBox TextBoxDisplay SmallFont" Font-Bold="False"
                                                ReadOnly="True"></asp:TextBox><asp:TextBox ID="txtItemDesc" runat="server" Width="300px"
                                                    CssClass="TextBox TextBoxDisplay SmallFont" Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                        Shade Family :<asp:TextBox ID="txtShadeFamilyName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                            ReadOnly="true" Width="70px"></asp:TextBox>
                                        &nbsp;Shade No :<asp:TextBox ID="txtShadeName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                            ReadOnly="true" Width="70px"></asp:TextBox>
                                        <asp:TextBox ID="ddlShadeCode" runat="server" CssClass="TextBoxDisplay SmallFont"
                                            ReadOnly="true" Visible="false" Width="1px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnSTCancel" runat="server" CssClass="SmallFont" OnClick="btnSTCancel_Click"
                                            Text="Cancel" Width="60px" TabIndex="50" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
                            <table width="100%">
                                <tr id="tr4" runat="server">
                                    <td id="Td4" runat="server" align="left" class="td" width="100%">
                                        <asp:GridView ID="GridSpinningThread" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            CssClass="SmallFont" Font-Bold="False" OnRowCommand="GridSpinningThread_RowCommand"
                                            Width="100%" TabIndex="39" OnRowDataBound="GridSpinningThread_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Article No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtArticleNo" runat="server" Font-Bold="true" Text='<%# Bind("ARTICLE_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Article Desc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtArticleDesc" runat="server" Font-Bold="true" Text='<%# Bind("ARTICLE_DESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Party Article Desc">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPartyArticleDesc" runat="server" Font-Bold="true" Text='<%# Bind("PARTY_ARTICLE_DESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shade Family">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtShadeFamily" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_FAMILY_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shade Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtShade" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack Procee">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPackProcess" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("END_USE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lab Dip No." Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtMatchingReff" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MATCHING_REFF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtQuantity" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                            Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Case/Box">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtNoOfCones" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                            Text='<%# Bind("CR_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sale&nbsp;Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSale" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SALE_RATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Final Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtfRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                                            Text='<%# Bind("FINAL_RATE") %>' Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Cost.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTotalCost" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TOTAL_COST") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Use." Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtEndUse" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("END_USE_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Final Date.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtReqDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REQ_DATE" , "{0:d}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Shade Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtShaderefff" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                            Text='<%# Bind("SHADE_REFF_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Grade">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgrade" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                            Text='<%# Bind("GRADE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemarks" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                            Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Machine">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Idn_Adj" runat="server" CommandArgument='<%#Bind("SHADE_NAME") %>'
                                                            Text="Machine" CommandName="DEL"></asp:LinkButton>
                                                        <asp:Panel ID="IdnPanel" runat="server" BackColor="White">
                                                            <asp:GridView runat="server" ID="Idn_grid" AutoGenerateColumns="false" CssClass="SmallFont">
                                                                <RowStyle CssClass="SmallFont" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px"
                                                                        HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle VerticalAlign="Top" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="TRN NO" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtTrnNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_NO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MACHINE" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="txtMACHINE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MACHINE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="BATCH" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBATCH" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BATCH") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Machine Capacity" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMacCapacity" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MACHINE_CAPACITY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Assigned Qty" HeaderStyle-HorizontalAlign="Left">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblASSQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ASS_QTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <RowStyle CssClass="SmallFont" />
                                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <cc4:HoverMenuExtender ID="idnHover" runat="server" PopupControlID="IdnPanel" PopupPosition="Left"
                                                            TargetControlID="Idn_Adj">
                                                        </cc4:HoverMenuExtender>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                            CommandName="EditItem" Text="Edit" />
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                            CommandName="DelItem" Text="Delete" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                            <RowStyle CssClass="SmallFont" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
        <asp:Label ID="lblUNIT_WT" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="lblNETBOX_WT" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="lblNETCART_WT" runat="server" Visible="false" Text=""></asp:Label>
        <p>
            &nbsp;
        </p>
        <cc3:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" PromptCharacter="_" TargetControlID="txtReqDate">
        </cc3:MaskedEditExtender>
        <cc3:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtReqDate">
        </cc3:CalendarExtender>
        <cc3:CalendarExtender ID="ceDoc" runat="server" TargetControlID="TextBox1" Format="dd/MM/yyyy">
        </cc3:CalendarExtender>
        <cc3:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="TextBox1" PromptCharacter="_">
        </cc3:MaskedEditExtender>
        <cc3:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtLCDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc3:CalendarExtender>
        <cc3:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtLCDate" PromptCharacter="_">
        </cc3:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
