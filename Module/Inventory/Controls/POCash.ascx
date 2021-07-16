<%@ Control Language="C#" AutoEventWireup="true" CodeFile="POCash.ascx.cs" Inherits="Module_Inventory_Controls_POCash" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>

<script language="javascript" type="text/javascript">

    function Calculation(val)
    { 
         var name=val;
       
         document.getElementById('ctl00_cphBody_txtAdvanceAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_txtAdvance').value)*(parseFloat(document.getElementById('ctl00_cphBody_txtFinalTotal').value)/100)).toFixed(3) ;
      }
           
    function SetFocus(ControlId)
    {    
        document.getElementById(ControlId).focus();       
    }
    function GetClick(ButtonId)
    {
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
    .c5
    {
        margin-left: 4px;
        width: 200px;
    }
    .ralign
    {
        text-align: right;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table class="tContentArial" width="95%">
    <tr>
        <td class="td tdLeft" width="100%">
            <table align="left">
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" Width="48" Height="41" ValidationGroup="M1">
                        </asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" Width="48" Height="41"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" width="100%">
            <span class="titleheading"><b>Material Purchaser Order Cash</b></span>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        Order Number :
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:TextBox ID="txtOrderNumber" TabIndex="1" runat="server" Width="150px" CssClass="TextBoxNo SmallFont"
                            MaxLength="10" OnTextChanged="txtOrderNumber_TextChanged1" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="M1"
                            Font-Bold="true" Display="dynamic" ErrorMessage="*" ControlToValidate="txtOrderNumber"></asp:RequiredFieldValidator>
                        <cc2:ComboBox ID="ddlOrderNumber" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="PO_NUMB" DataValueField="PO_NUMB" EmptyText="Find Item" EnableLoadOnDemand="true"
                            Height="200px" MenuWidth="450px" OnLoadingItems="ddlOrderNumber_LoadingItems"
                            OnSelectedIndexChanged="ddlOrderNumber_SelectedIndexChanged" TabIndex="1" Width="150px">
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
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("PO_NUMB") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container8" runat="server" Text='<%# Eval("PO_DATE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container9" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("PO_NATURE") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items - out of .
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td valign="top" align="right" width="15%">
                        Order Date :
                    </td>
                    <td valign="top" align="left" width="55%" colspan="3">
                        <asp:TextBox ID="txtOrderDate" TabIndex="2" runat="server" Width="150px" CssClass="TextBox SmallFont"
                            MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="M1"
                            Font-Bold="true" Display="dynamic" ErrorMessage="*" ControlToValidate="txtOrderDate"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtOrderDate" 
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td valign="top" align="right" width="16%">
                        <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="Party Code :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <cc2:ComboBox ID="txtPartyCode" runat="server" TabIndex="3" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="350px" Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c4">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1"  Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c4">
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
                    <td valign="top" align="left" width="69%">
                        <asp:TextBox ID="txtPartyAddress"  runat="server" Width="95%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="16%">
                        <asp:Label ID="Label5" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <cc2:ComboBox ID="txtTransporterCode" runat="server" TabIndex="4"  AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EnableVirtualScrolling="true" OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged"
                            Width="150px" EmptyText="Select transaporter" MenuWidth="350px" Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c4">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c4">
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
                    <td valign="top" align="left" width="69%">
                        <asp:TextBox ID="txtTransporterName"  runat="server" Width="95%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td width="15%" class="tdRight">
                        PO Nature :
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:DropDownList ID="ddlPONature" runat="server" TabIndex="7" CssClass="SmallFont TextBox" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label15" runat="server" CssClass="LabelNo" Text="Currency Code :"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:DropDownList ID="txtCurrencyCode" TabIndex="8" Width="150px" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label17" runat="server" CssClass="LabelNo" Text="Conversion Rate :"></asp:Label>
                    </td>
                    <td width="25%" class="tdLeft">
                        <asp:TextBox ID="txtconversionRate" CssClass="SmallFont TextBoxNo" runat="server"
                            TabIndex="7" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label8" runat="server" CssClass="LabelNo" Text="Delivery Branch :"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft" colspan="3" style="width: 30%">
                        <asp:DropDownList ID="ddlDelAdd" runat="server" TabIndex="8" CssClass="SmallFont TextBox" AutoPostBack="true" 
                            Width="150px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDelAdd_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtDelAddress" TabIndex="8" runat="server" Width="300px" CssClass="SmallFont gCtrTxt TextBox TextBoxDisplay"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label7" runat="server" CssClass="LabelNo" Text="Delivery Date :"></asp:Label>
                    </td>
                    <td width="25%" class="tdLeft">
                        <asp:TextBox ID="txtDeliveryDate" TabIndex="9" runat="server" Width="150px" CssClass="SmallFont gCtrTxt"
                            MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvd" runat="server" ValidationGroup="M1" Font-Bold="False"
                            Display="Dynamic" ErrorMessage="*Del Date required" ControlToValidate="txtDeliveryDate"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CE2" runat="server" TargetControlID="txtDeliveryDate" Format="dd-MM-yyyy">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label6" runat="server" CssClass="LabelNo" Text="Payment Term :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtPayTerm" TabIndex="10" runat="server" Width="99%" CssClass="SmallFont gCtrTxt"
                            MaxLength="50"></asp:TextBox>
                    </td>
                    <td valign="top" align="right" width="20%">
                        <asp:Label ID="Label9" runat="server" CssClass="LabelNo" Text="Despatch Mode :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="30%">
                        <asp:TextBox ID="txtDespatchMode" TabIndex="11" runat="server" Width="90%" CssClass="SmallFont gCtrTxt"
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label14" runat="server" CssClass="LabelNo" Text="Remarks :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="85%">
                        <asp:TextBox ID="txtRemarks" TabIndex="12" runat="server" Width="96%" CssClass="SmallFont gCtrTxt"
                            MaxLength="500" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label16" runat="server" CssClass="LabelNo" Text="Instructions :"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="85%">
                        <asp:TextBox ID="txtInstructions" runat="server" CssClass="SmallFont gCtrTxt" MaxLength="500"
                            TabIndex="13" TextMode="SingleLine" Width="96%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td SmallFont" valign="top" width="100%">
            <table width="100%">
                <tr bgcolor="#336699">
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label18" runat="server" CssClass="LabelNo titleheading" Text="Item Code"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label20" runat="server" CssClass="LabelNo titleheading" Text="PO Quantity"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label22" runat="server" CssClass="LabelNo titleheading" Text="UOM"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label23" runat="server" CssClass="LabelNo titleheading" Text="Basic Rate"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label24" runat="server" CssClass="LabelNo titleheading" Text="Dis./Tax"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label25" runat="server" CssClass="LabelNo titleheading" Text="Final Rate"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label26" runat="server" CssClass="LabelNo titleheading" Text="Amount"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label27" runat="server" CssClass="LabelNo titleheading" Text="Quotation No"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label28" runat="server" CssClass="LabelNo titleheading" Text="Delivery Date"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft" width="210px">
                        <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" EmptyText="Find Item" EnableLoadOnDemand="true"
                            EnableVirtualScrolling="true" Height="200px" MenuWidth="650px" OnLoadingItems="txtItemCode_LoadingItems"
                            OnSelectedIndexChanged="txtItemCode_SelectedIndexChanged" TabIndex="14" Width="100px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c5">
                                    DESCRIPTION</div>
                                <div class="header c1 ralign">
                                    Appr Qty.</div>
                                <div class="header c1 ralign">
                                    Adj Qty</div>
                                <div class="header c1 ralign">
                                    Bal Qty</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ITEM_CODE") %>' />
                                </div>
                                <div class="item c5">
                                    <asp:Literal ID="Container5" runat="server" Text='<%# Eval("ITEM_DESC") %>' />
                                </div>
                                <div class="item c1 ralign">
                                    <asp:Literal ID="Container6" runat="server" Text='<%# Eval("APPR_QTY") %>' />
                                </div>
                                <div class="item c1 ralign">
                                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("PUR_ADJ_QTY") %>' />
                                </div>
                                <div class="item c1 ralign">
                                    <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("BAL_QTY") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:TextBox ID="lblItemCode"  ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" Text='<%# Bind("ITEM_CODE") %>' Width="100px"></asp:TextBox>
                    </td>
                    <td class="tdLeft" width="160px">
                        <asp:Button ID="btnAdjustIndent" TabIndex="15" runat="server" Font-Size="8pt" Text="Ind. Adj"
                            Width="48px" OnClick="btnAdjustIndent_Click1" /><asp:TextBox ID="txtOrderQty" TabIndex="21"
                                CssClass="TextBoxNo SmallFont TextBoxDisplay" runat="server" Width="100px" Text='<%# Bind("ORD_QTY") %>'
                                ReadOnly="true" AutoPostBack="True" OnTextChanged="txtOrderQty_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtUnit"  CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" ReadOnly="true" Width="35px" Text='<%# Bind("UOM") %>'></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtBaseRate" TabIndex="16" runat="server" Width="60px" Text='<%# Bind("BASIC_RATE") %>'
                            CssClass="TextBoxNo SmallFont" AutoPostBack="True" OnTextChanged="txtBaseRate_TextChanged1"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:Button ID="btnDiscountTaxes" TabIndex="17" runat="server" Font-Size="8pt" Text="Disc/Taxes"
                            Width="50px" OnClick="btnDiscountTaxes_Click1" />
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtFinalRate"  ReadOnly="true" runat="server" Text='<%# Bind("FINAL_RATE") %>'
                            Width="60px" CssClass="TextBoxNo SmallFont TextBoxDisplay" AutoPostBack="True"
                            OnTextChanged="txtFinalRate_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtAmount"  ReadOnly="true" Width="70px" runat="server"
                            Text='<%# Bind("Amount") %>' CssClass="TextBoxNo SmallFont TextBoxDisplay"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtQuotation" TabIndex="18"  Width="70px" runat="server" Text='<%# Bind("QUOTATION_NO") %>'
                            CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtTrnDeliveryDate" TabIndex="19" runat="server" Width="60px" Text='<%# Bind("DEL_DATE") %>'
                            CssClass="TextBox SmallFont"></asp:TextBox>
                        <cc1:CalendarExtender ID="CE3" runat="server" TargetControlID="txtTrnDeliveryDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td class="tdLeft" rowspan="2">
                        <cc3:OboutButton ID="btnSaveDetail" Text="Save" runat="server" TabIndex="21" OnClick="btnSaveDetail_Click" Width="60px">
                        </cc3:OboutButton>
                        <cc3:OboutButton ID="btnCancelDetail" Text="Cancel" runat="server" TabIndex="22" OnClick="btnCancelDetail_Click" Width="60px">
                        </cc3:OboutButton>
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft" colspan="9">
                        <asp:TextBox ID="txtItemDescription" TabIndex="20" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" Text='<%# Bind("ITEM_DESC") %>' Width="98%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trMaterialPOCreditTRN" runat="server">
        <td width="100%" class="td SmallFont">
            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
            <asp:GridView ID="gvMaterialPOTRN" TabIndex="16" runat="server" OnRowCommand="gvMaterialPOTRN_RowCommand"
                CssClass="SmallFont" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false">
                <RowStyle CssClass="SmallFont" Width="98%" />
                <Columns>
                    <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="5%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Code">
                        <ItemTemplate>
                            <asp:Label ID="txtItemCode" TabIndex="17" Width="80px" Font-Bold="true" CssClass="Label SmallFont"
                                runat="server" Text='<%# Bind("ITEM_CODE") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Description">
                        <ItemTemplate>
                            <asp:Label ID="txtItemDescription" TabIndex="19" ReadOnly="true" runat="server" Width="120px"
                                CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="20%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="txtOrderQty" TabIndex="21" CssClass="LabelNo SmallFont" runat="server"
                                Width="60px" Text='<%# Bind("ORD_QTY") %>' Font-Bold="true" ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>
                            <asp:Label ID="txtUnit" TabIndex="22" CssClass="Label SmallFont" runat="server" ReadOnly="true"
                                Width="50px" Text='<%# Bind("UOM") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Base Rate">
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:Label ID="txtBaseRate" TabIndex="23" runat="server" Width="50px" Text='<%# Bind("BASIC_RATE") %>'
                                CssClass="LabelNo SmallFont" ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Final Rate">
                        <ItemTemplate>
                            <asp:Label ID="txtFinalRate" TabIndex="25" ReadOnly="true" runat="server" Text='<%# Bind("FINAL_RATE") %>'
                                Width="70px" Font-Bold="true" CssClass="LabelNo SmallFont"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="txtAmount" TabIndex="26" ReadOnly="true" Width="70px" runat="server"
                                Text='<%# Bind("Amount") %>' Font-Bold="true" CssClass="LabelNo SmallFont"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quotation No">
                        <ItemTemplate>
                            <asp:Label ID="lblCurr" runat="server" Width="60px" Text='<%# Bind("QUOTATION_nO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Del Date">
                        <ItemTemplate>
                            <asp:Label ID="txtTrnDeliveryDate" TabIndex="28" runat="server" Width="90px" Text='<%# Bind("DEL_DATE", "{0:d}") %>'
                                CssClass="Label SmallFont" ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" TabIndex="29" runat="server" Text="Edit" CommandName="POMateialCreditEdit"
                                CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                            /
                            <asp:LinkButton ID="lnkDelete" TabIndex="29" runat="server" Text="Del" CommandName="POMateialCreditDelete"
                                CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="SmallFont" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
            <%--     </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtOrderNumber" EventName="TextChanged"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="imgbtnClear" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="imgbtnFind" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="imgbtnUpdate" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="gvMaterialPOTRN" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>--%>
        </td>
    </tr>
</table>
<%--</ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnClear" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="imgbtnFind" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="imgbtnUpdate" EventName="Click"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtDeliveryDate" EventName="TextChanged"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="txtOrderNumber" EventName="TextChanged"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="txtPartycode" EventName="TextChanged"></asp:AsyncPostBackTrigger>
        <asp:AsyncPostBackTrigger ControlID="txtTransporterCode" EventName="TextChanged">
        </asp:AsyncPostBackTrigger>
    </Triggers>
</asp:UpdatePanel>
--%>