<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PPC.ascx.cs" Inherits="Module_OrderDevelopment_Controls_PPC" %>
<%@ Register Src="../../../CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"
    TagPrefix="uc1" %>
<%@ Register Src="../../../CommonControls/LOV/CustReqArticleLOV.ascx" TagName="CustReqArticleLOV"
    TagPrefix="uc2" %>
<%@ Register Src="../../../CommonControls/LOV/OrderNoLOV.ascx" TagName="OrderNoLOV"
    TagPrefix="uc3" %>
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
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
    .c3
    {
        margin-left: 4px;
        width: 350px;
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
</style>
<table class="tdMain" width="100%">
    <tr>
        <td class="td" width="100%">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Order Capture Form for
                <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont"></asp:Label>
            </b>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft" width="100%">
            <span class="Mode">You are in
                <asp:Label ID="lblMode" runat="server"></asp:Label>
                Mode</span>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblBusinessType" runat="server" Text="Business Type :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlBusinessType" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="98%" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblProdType" runat="server" Text="Product Type :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="98%" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblOrderCategory" runat="server" Text="Order Category :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlOrderCategory" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="98%" OnSelectedIndexChanged="ddlOrderCategory_SelectedIndexChanged">
                            <asp:ListItem>DIRECT SALE</asp:ListItem>
                            <asp:ListItem>INHOUSE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblOrderType" runat="server" Text="Order Type :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="16%">
                        <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="99%" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblOrderNo" runat="server" Text="Order number :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:TextBox ID="txtOrderNo" runat="server" ReadOnly="true" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="98%"></asp:TextBox>
                        <uc3:OrderNoLOV ID="ddlOrderNo" runat="server" />
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblOrderDate" runat="server" Text="Order Date :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:TextBox ID="txtOrderDate" runat="server" ReadOnly="true" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="98%"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblCurrencyCode" runat="server" Text="Currency Code :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlCurrencyCode" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="98%">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblConversionRate" runat="server" Text="Conversion Rate :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="16%">
                        <asp:TextBox ID="txtConversionRate" runat="server" CssClass="TextBoxNo SmallFont"
                            Width="97%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblPartyCode" runat="server" Text="Party Code :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <uc1:PartyCodeLOV ID="ddlParty" runat="server" />
                    </td>
                    <td class="tdLeft" width="76%">
                        <asp:TextBox ID="txtPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="24%" ReadOnly="True"></asp:TextBox>
                        <asp:TextBox ID="txtPartyDetail" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="74%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblPartyRefNumber" runat="server" Text="Party Ref # :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:TextBox ID="txtPartyRefNumber" runat="server" CssClass="TextBoxNo SmallFont"
                            MaxLength="50" Width="98%"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblPartyRefDate" runat="server" Text="Party Ref Date :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:TextBox ID="txtPartyRefDate" runat="server" CssClass="TextBox SmallFont" Width="98%"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblOrderProcess" runat="server" Text="Order Process :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlOrderProcess" runat="server" MenuWidth="200px" Width="98%">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblShipment" runat="server" Text="Shipment :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="16%">
                        <asp:TextBox ID="txtShipment" runat="server" CssClass="TextBox SmallFont" MaxLength="500"
                            Width="97%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblConsigneeName" runat="server" Text="Consignee Name :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:TextBox ID="txtconsigneeName" runat="server" CssClass="TextBox SmallFont" MaxLength="100"
                            Width="98%"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblConsigneeAdd" runat="server" Text="Consignee Address :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="64%">
                        <asp:TextBox ID="txtConsigneeAdd" runat="server" CssClass="TextBox SmallFont" Width="99%"
                            MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="98%" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged">
                            <asp:ListItem Text="DD" Value="DD"></asp:ListItem>
                            <asp:ListItem Text="CHEQUE" Value="CHEQUE"></asp:ListItem>
                            <asp:ListItem Text="CASH" Value="CASH"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblPaymentTerm" runat="server" Text="Payment Term :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="64%">
                        <asp:TextBox ID="txtPaymentTerm" runat="server" CssClass="TextBox SmallFont" Width="99%"
                            MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblDeliveryMode" runat="server" Text="Delivery Mode :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="12%">
                        <asp:DropDownList ID="ddlDeliveryMode" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="98%" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged">
                            <asp:ListItem Text="DEPOT TRANSFER" Value="DEPOT TRANSFER"></asp:ListItem>
                            <asp:ListItem Text="DIRECT CUSTOMER BILLING" Value="DIRECT CUSTOMER BILLING"></asp:ListItem>
                            <asp:ListItem Text="DEEMED EXPORT" Value="DEEMED EXPORT"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblFromBranch" runat="server" Text="From Branch :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="64%">
                        <asp:DropDownList ID="ddlFromBranch" runat="server" AutoPostBack="True" MenuWidth="200px"
                            Width="20%">
                            <asp:ListItem Text="DEPOT TRANSFER" Value="DEPOT TRANSFER"></asp:ListItem>
                            <asp:ListItem Text="DIRECT CUSTOMER BILLING" Value="DIRECT CUSTOMER BILLING"></asp:ListItem>
                            <asp:ListItem Text="DEEMED EXPORT" Value="DEEMED EXPORT"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblBillTo" runat="server" Text="Bill To :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="88%">
                        <asp:TextBox ID="txtBillTo" runat="server" CssClass="TextBox SmallFont" Width="99%"
                            MaxLength="500"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="88%">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" Width="100%"
                            MaxLength="2000"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblGenInstr" runat="server" Text="General Instruction :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="88%">
                        <asp:TextBox ID="txtGenInstruction" runat="server" CssClass="TextBox SmallFont" Width="100%"
                            MaxLength="2000"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="12%">
                        <asp:Label ID="lblsplInstr" runat="server" Text="Special Instruction :" CssClass="SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="88%">
                        <asp:TextBox ID="txtSplInstruction" runat="server" CssClass="TextBox SmallFont" Width="100%"
                            MaxLength="2000"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont" style="border-color: Blue; border-width: 1px;">
            <asp:Panel ID="pnlTRNDetail" runat="server" Width="100%" Style="border-color: Blue;
                border-width: 1px;">
                <table width="100%">
                    <tr>
                        <td class="td" width="100%">
                            <table width="100%">
                                <tr bgcolor="#336699" class="SmallFont titleheading">
                                    <td width="15%" class="tdLeft">
                                        Select Customer Requst
                                    </td>
                                    <td width="25%" class="tdLeft">
                                        Article Details
                                    </td>
                                    <td width="15%" class="tdLeft">
                                        Del. Schedule
                                    </td>
                                    <td width="15%" class="tdLeft">
                                        Costing, Packing & BOM
                                    </td>
                                    <td width="18%" class="tdLeft">
                                        Other Details
                                    </td>
                                    <td width="10%" class="tdLeft">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15%" class="tdLeft">
                                        <uc2:CustReqArticleLOV ID="txtTRNYarnSpiningArticalCode" runat="server" />
                                        <br />
                                        <asp:Button ID="btnTRN_ADJ_BOM" runat="server" CssClass="SmallFont" Text="Adj BOM"
                                            Width="99%" OnClick="btnTRN_ADJ_BOM_Click" />
                                        <br />
                                        <asp:Label ID="lblTRNYarnSpiningArticalCode" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td width="25%" class="tdLeft">
                                        C. Req. no: &nbsp;<asp:TextBox ID="txtTRNYarnSpiningCReqNo" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                            Wrap="true" Width="70%" ReadOnly="True"></asp:TextBox>
                                        <br />
                                        Art. Desc :&nbsp;
                                        <asp:TextBox ID="lblTRNYSpinDesc" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                            Width="70%"></asp:TextBox>
                                        <br />
                                        Unit :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtTRNYarnSpiningUOM" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                            Wrap="true" Width="70%" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td width="15%" class="tdLeft">
                                        <asp:Button ID="btnTRN_YRNSPIN_DelSchedule" runat="server" CssClass="SmallFont" OnClick="btnTRN_YRNSPIN_DelSchedule_Click"
                                            Text="Del Schedule" Width="98%" />
                                        <br />
                                        T. Qty:
                                        <asp:TextBox ID="txtTRNYarnSpiningOrderQty" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                            ReadOnly="True" Width="60%" OnTextChanged="txtTRNYarnSpiningOrderQty_TextChanged"></asp:TextBox><br />
                                        F. Date:
                                        <asp:TextBox ID="txtTRNYarnSpiningDelDate" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                            ReadOnly="True" Width="60%"></asp:TextBox>
                                    </td>
                                    <td width="15%" class="tdLeft">
                                        <asp:Button ID="btnTRN_YRNSPIN_BOM" runat="server" CssClass="SmallFont" OnClick="btnTRN_YRNSPIN_BOM_Click"
                                            Text="BOM" Width="40%" />
                                        <asp:Button ID="btnTRN_YRNSPIN_Pack" runat="server" CssClass="SmallFont" OnClick="btnTRN_YRNSPIN_Pack_Click"
                                            Text="Packing" Width="50%" />
                                        <br />
                                        <asp:Button ID="btnTRN_YRNSPIN_CostPrice" runat="server" CssClass="SmallFont" OnClick="btnTRN_YRNSPIN_CostPrice_Click"
                                            Text="Cost Price" Width="98%" />
                                        Cost:
                                        <asp:TextBox ID="txtTRNYarnSpiningCost" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                            OnTextChanged="txtTRNYarnSpiningCost_TextChanged" ReadOnly="True" Width="60%"></asp:TextBox>
                                    </td>
                                    <td width="18%" class="tdLeft">
                                        Shade:
                                        <asp:DropDownList ID="txtTRNYarnSpiningShade" runat="server">
                                        </asp:DropDownList>
                                        <br />
                                        Process Root:
                                        <asp:DropDownList ID="ddlProcessRoot" runat="server">
                                        </asp:DropDownList>
                                        <br />
                                        Shrinkage(%):
                                        <asp:TextBox ID="txtTRNYarnSpiningSrinkage" runat="server" CssClass="TextBoxNo SmallFont"
                                            Width="50%"></asp:TextBox>
                                    </td>
                                    <td width="10%" class="tdLeft">
                                        <asp:Button ID="btnsaveTRNYarnSpiningDetail" runat="server" CssClass="SmallFont"
                                            OnClick="btnsaveTRNYarnSpiningDetail_Click" Text="Save" Width="98%" ValidationGroup="T1" /><br />
                                        <asp:Button ID="btnTRNYarnSpiningCancel" runat="server" CssClass="SmallFont" Width="98%"
                                            OnClick="btnTRNYarnSpiningCancel_Click" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" width="100%">
                            <asp:Panel ID="pnlTRNYarnSpiningGrid" runat="server" Width="100%">
                                <asp:GridView ID="grdTRNYarnSpiningDetail" runat="server" AutoGenerateColumns="False"
                                    CssClass="SmallFont" OnRowCommand="grdTRNYarnSpiningDetail_RowCommand" Width="99%"
                                    OnRowDataBound="grdTRNYarnSpiningDetail_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="PI Indent #">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnSpiningPI_NO" runat="server" CssClass="LabelNo SmallFont"
                                                    Text='<%# Bind("PI_NO") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust Req. No">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnSpiningCustReqNo" runat="server" CssClass="Label SmallFont"
                                                    Text='<%# Bind("CUST_REQ_NO") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Artical Code">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnSpiningArticalCode" runat="server" CssClass="Label SmallFont"
                                                    Text='<%# Bind("ARTICAL_CODE") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnSpiningUOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnSpiningOrderQuantity" runat="server" CssClass="LabelNo SmallFont"
                                                    Text='<%# Bind("ORD_QTY") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivery Date">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="txtTRNYarnSpiningDelDate" runat="server" CssClass="Label SmallFont"
                                                    Text='<%# Bind("DEL_DATE","{0:dd-MM-yyyy}") %>' CommandArgument='<%# Bind("UNIQUE_ID") %>'>
                                                </asp:LinkButton>
                                                <asp:Panel ID="pnlDelSchedule" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                                    BorderStyle="Solid" BorderWidth="5px">
                                                    <asp:GridView ID="grdDelSchedule" runat="server" BorderColor="#C5E7F1" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Delivery Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtDelAdd" runat="server" CssClass="Label" Text='<%# Bind("DEL_ADDRESS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req Delivery Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtDelDate" runat="server" CssClass="Label" Text='<%# Bind("DEL_DATE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delivery Quantity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtDelQty" runat="server" CssClass="Label" Text='<%# Bind("DEL_QTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delivery Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtDelRemarks" runat="server" CssClass="Label" Text='<%# Bind("DEL_REMARKS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="SmallFont" />
                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                    </asp:GridView>
                                                    <asp:Button ID="btnCancelDel" runat="server" Text="Close This" />
                                                </asp:Panel>
                                                <cc1:ModalPopupExtender ID="mpeDelSchedule" runat="server" PopupControlID="pnlDelSchedule"
                                                    TargetControlID="txtTRNYarnSpiningDelDate" BackgroundCssClass="modalBackground"
                                                    CancelControlID="btnCancelDel" DropShadow="true">
                                                </cc1:ModalPopupExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cost Price">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="txtTRNYarnSpiningCost" runat="server" CssClass="LabelNo SmallFont"
                                                    Text='<%# Bind("TOTAL_COST") %>' CommandArgument='<%# Bind("UNIQUE_ID") %>'>
                                                </asp:LinkButton>
                                                <asp:Panel ID="pnlCostDetail" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                                    BorderStyle="Solid" BorderWidth="5px">
                                                    <asp:DataList ID="dlTRNYRNSPIN_Cost" runat="server">
                                                        <ItemTemplate>
                                                            <table style="color: Black;">
                                                                <tr>
                                                                    <td class="tdRight">
                                                                        Sale Rate :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Sale" runat="server" Text='<%# Bind("SALE") %>' CssClass="LabelNo"
                                                                            Width="80px"></asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Freight :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Freight" runat="server" Text='<%# Bind("FREIGHT") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Commission :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Commission" runat="server" Text='<%# Bind("COMMISSION") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdRight">
                                                                        Incentives :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Incentives" runat="server" Text='<%# Bind("INCENTIVES") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Brokerage :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_Brokerage" runat="server" Text='<%# Bind("BROKERAGE") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Ex-Mill Rate :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="txtYRNSPIN_COST_ExMill" runat="server" Text='<%# Bind("EX_MILL_RATE") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdRight">
                                                                        FOB Value :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("FOB") %>' CssClass="LabelNo"
                                                                            Width="80px"></asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Handling Charges :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("HANDLING_CHARGES") %>' CssClass="LabelNo"
                                                                            Width="80px">0</asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Bill D Charges :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("BILL_D_CHARGES") %>' CssClass="LabelNo"
                                                                            Width="80px">0</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="tdRight">
                                                                        Export Incentives (%):
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("EXPORT_INCENTIVES") %>' CssClass="LabelNo"
                                                                            Width="80px">0</asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Export Incentives Amt :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("EXPORT_INCENTIVES_AMT") %>'
                                                                            CssClass="LabelNo" Width="80px">0</asp:Label>
                                                                    </td>
                                                                    <td class="tdRight">
                                                                        Other Cost :
                                                                    </td>
                                                                    <td class="tdLeft">
                                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("OTHER_COST") %>' CssClass="LabelNo"
                                                                            Width="80px">0</asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                    </asp:DataList>
                                                    <asp:Button ID="btnCancelCost" runat="server" Text="Close This" />
                                                </asp:Panel>
                                                <cc1:ModalPopupExtender ID="mpeCost" runat="server" PopupControlID="pnlCostDetail"
                                                    TargetControlID="txtTRNYarnSpiningCost" BackgroundCssClass="modalBackground"
                                                    CancelControlID="btnCancelCost" DropShadow="true">
                                                </cc1:ModalPopupExtender>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shade">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnSpiningShade" runat="server" CssClass="LabelNo SmallFont"
                                                    Text='<%# Bind("SHADE") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BOM">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="txtTRNYarnSpiningBOM" runat="server" CssClass="Label SmallFont"
                                                    Text="View BOM" CommandArgument='<%# Bind("UNIQUE_ID") %>'>
                                                </asp:LinkButton>
                                                <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                                    BorderStyle="Solid" BorderWidth="5px">
                                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Warp/Weft">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdW_SIDE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("W_SIDE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Product Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdPRODUCT_TYPE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BASE_ARTICAL_TYPE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Article Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdARTICLE_CODE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BASE_ARTICAL_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdUOM" runat="server" CssClass="SmallFont Label" Text='<%# Bind("UOM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Basis">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdBASIS" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BASIS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdVALUE_QTY" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("VALUE_QTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Requested Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdREQ_QTY" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("REQ_QTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="SmallFont" />
                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                    </asp:GridView>
                                                    <asp:Button ID="btnCancelBOM" runat="server" Text="Close This" />
                                                </asp:Panel>
                                                <cc1:ModalPopupExtender ID="mpeBOM" runat="server" PopupControlID="pnlBOM" TargetControlID="txtTRNYarnSpiningBOM"
                                                    BackgroundCssClass="modalBackground" CancelControlID="btnCancelBOM" DropShadow="true">
                                                </cc1:ModalPopupExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Packing">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="txtTRNYarnSpiningPACK" runat="server" CssClass="Label SmallFont"
                                                    Text="Packing Details" CommandArgument='<%# Bind("UNIQUE_ID") %>'>
                                                </asp:LinkButton>
                                                <asp:Panel ID="pnlPACK" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                                    BorderStyle="Solid" BorderWidth="5px">
                                                    <asp:GridView ID="grdPACK" runat="server" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Packing Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdPACK_CODE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PCK_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Packing Description">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdPACK_DESC" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PCK_DESC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgrdPACK_QTY" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PCK_QTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="SmallFont" />
                                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                    </asp:GridView>
                                                    <asp:Button ID="btnCancelPACK" runat="server" Text="Close This" />
                                                </asp:Panel>
                                                <cc1:ModalPopupExtender ID="mpePACK" runat="server" PopupControlID="pnlPACK" TargetControlID="txtTRNYarnSpiningPACK"
                                                    BackgroundCssClass="modalBackground" CancelControlID="btnCancelPACK" DropShadow="true">
                                                </cc1:ModalPopupExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Process Root">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnPROS_ROUTE_CODE" runat="server" CssClass="LabelNo SmallFont"
                                                    Text='<%# Bind("PROS_ROUTE_CODE") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Design">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnSpiningDesign" runat="server" CssClass="LabelNo SmallFont"
                                                    Text='<%# Bind("DESIGN") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lot No">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnSpiningLotNo" runat="server" CssClass="LabelNo SmallFont"
                                                    Text='<%# Bind("LOT_NO") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Srinkage/ Process Loss">
                                            <ItemTemplate>
                                                <asp:Label ID="txtTRNYarnSpiningSrinkage" runat="server" CssClass="Label SmallFont"
                                                    Text='<%# Bind("SRINKAGE") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                    CommandName="EditTRNYarnSpiningDetail" Text="Edit"></asp:LinkButton>/
                                                <asp:LinkButton ID="lnkbtnDel" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                    CommandName="DelTRNYarnSpiningDetail" Text="Delete"></asp:LinkButton></ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                    <RowStyle CssClass="SmallFont" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<cc1:CalendarExtender ID="cePartyPODate" runat="server" TargetControlID="txtPartyRefDate"
    PopupPosition="TopLeft">
</cc1:CalendarExtender>
