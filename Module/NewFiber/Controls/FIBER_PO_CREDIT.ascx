<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FIBER_PO_CREDIT.ascx.cs" Inherits="Module_Fiber_Controls_FIBER_PO_CREDIT" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>

<script language="javascript" type="text/javascript">

    function Calculation(val) {
        var name = val;

       document.getElementById('ctl00_cphBody_POCredit1_txtAdvanceAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtAdvance').value) * (parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtFinalTotal').value) / 100)).toFixed(3);
       //document.getElementById('ctl00_cphBody_FIBER_PO_CREDIT1_txtAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_FIBER_PO_CREDIT1_txtOrderQty').value)) * (parseFloat(document.getElementById('ctl00_cphBody_FIBER_PO_CREDIT1_txtFinalRate').value)) ;
   
    
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
        width: 120px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 200px;
    }
    .ralign
    {
        text-align: right;
    }
</style>
<asp:UpdatePanel runat="server" id="UpdatePanel">
    <ContentTemplate>
<table class="tContentArial" width="100%">
    <tr>
        <td class="td tdLeft" width="100%">
            <table align="left">
                <tr>
                    <td id="tdSave" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" Height="41" Width="48" ValidationGroup="M1" 
                            onclick="imgbtnSave_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnUpdate"  runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" Width="48" Height="41" 
                            ValidationGroup="M1" onclick="imgbtnUpdate_Click">
                        </asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" Width="48" Height="41" 
                            onclick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" Width="48" Height="41" 
                            onclick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" Width="48" Height="41" 
                            onclick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41" 
                            onclick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" width="100%">
            <span class="titleheading"><b>Fiber Purchaser Order </b></span>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label2" runat="server" CssClass="LabelNo" Text="Order Type :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:DropDownList ID="ddlOrderType" TabIndex="1" runat="server" Width="125px" CssClass="TextBox SmallFont"
                            AutoPostBack="True" Font-Bold="true" 
                            onselectedindexchanged="ddlOrderType_SelectedIndexChanged">
                            <asp:ListItem Value="PUM">Main Order</asp:ListItem>
                            <asp:ListItem Value="PUS">Supplimentry Order</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label3" runat="server" CssClass="LabelNo" Text="Order Number :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:TextBox ID="txtOrderNumber" TabIndex="2" Font-Bold="true" runat="server" 
                            Width="60px" CssClass="TextBoxNo SmallFont" MaxLength="10" 
                            ReadOnly="True" ontextchanged="txtOrderNumber_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvon" runat="server" ValidationGroup="M1" Font-Bold="False"
                            Display="Dynamic" ErrorMessage="*Order number required" ControlToValidate="txtOrderNumber"></asp:RequiredFieldValidator>
                        <cc2:ComboBox ID="ddlOrderNumber" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="PO_NUMB" DataValueField="PO_NUMB" 
                            OnLoadingItems="ddlOrderNumber_LoadingItems" EmptyText="Find Item" 
                            EnableLoadOnDemand="true" 
                            OnSelectedIndexChanged="ddlOrderNumber_SelectedIndexChanged" MenuWidth="450px" 
                             TabIndex="3" Width="100px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Order Number</div>
                                <div class="header c1">
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
                                <div class="item c1">
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
                        <asp:Label ID="Label4" runat="server" CssClass="LabelNo" Text="Order Date :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="25%">
                        <asp:TextBox ID="txtOrderDate" TabIndex="4" Font-Bold="true" runat="server" Width="60px"
                            CssClass="TextBox SmallFont" MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvod" runat="server" ValidationGroup="M1" Font-Bold="False"
                            Display="Dynamic" ErrorMessage="*Order Date Required" ControlToValidate="txtOrderDate"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtOrderDate">
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
                        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="800px" Height="200px" TabIndex="5" >
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
                    <td valign="top" align="left" width="69%">
                        <asp:TextBox ID="txtPartyAddress" TabIndex="6" runat="server" Width="95%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="16%">
                        <asp:Label ID="Label5" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EnableVirtualScrolling="true" OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged"
                            Width="150px" EmptyText="Select transaporter" MenuWidth="800px" Height="200px" TabIndex="7">
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
                    <td valign="top" align="left" width="69%">
                        <asp:TextBox ID="txtTransporterName"  runat="server" Width="95%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine" TabIndex="8"></asp:TextBox>
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
                        <asp:DropDownList ID="ddlPONature" runat="server" CssClass="SmallFont TextBox" TabIndex="9" Width="80%">
                        </asp:DropDownList>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label15" runat="server" CssClass="LabelNo" Text="Currency Code :"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:DropDownList ID="txtCurrencyCode" Width="80%" runat="server" CssClass="SmallFont TextBox"
                            AppendDataBoundItems="True" AutoPostBack="True" TabIndex="10">
                        </asp:DropDownList>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label17" runat="server" CssClass="LabelNo" Text="Conversion Rate :"></asp:Label>
                    </td>
                    <td width="25%" class="tdLeft">
                        <asp:TextBox ID="txtconversionRate" CssClass="SmallFont TextBoxNo" runat="server"
                             Width="50px" TabIndex="11"></asp:TextBox>
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
                        <asp:Label ID="Label8" runat="server" CssClass="LabelNo" Text="Delivery Branch :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="35%">
                        <asp:DropDownList ID="ddlDelAdd" runat="server" CssClass="SmallFont TextBox" AutoPostBack="true"
                            Width="150px" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDelAdd_SelectedIndexChanged" TabIndex="12">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtDelAddress" TabIndex="13" runat="server" Width="98%" CssClass="SmallFont gCtrTxt"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="20%" align="right">
                        <asp:Label ID="Label7" runat="server" CssClass="LabelNo" Text="Delivery Date :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="30%">
                        <asp:TextBox ID="txtDeliveryDate" TabIndex="14" runat="server" Width="60px" CssClass="SmallFont gCtrTxt"
                            MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvd" runat="server" ValidationGroup="M1" Font-Bold="False"
                            Display="Dynamic" ErrorMessage="*Del Date required" ControlToValidate="txtDeliveryDate"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CE2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDeliveryDate">
                        </cc1:CalendarExtender>
                        
                          <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtDeliveryDate">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label6" runat="server" CssClass="LabelNo" Text="Pay Term :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtPayTerm" TabIndex="15" runat="server" Width="99%" CssClass="SmallFont gCtrTxt"
                            MaxLength="50" ></asp:TextBox>
                    </td>
                    <td valign="top" align="right" width="20%">
                        <asp:Label ID="Label9" runat="server" CssClass="LabelNo" Text="Despatch Mode :" ></asp:Label>
                    </td>
                    <td valign="top" align="left" width="30%">
                        <asp:TextBox ID="txtDespatchMode" TabIndex="16" runat="server" Width="90%" CssClass="SmallFont gCtrTxt"
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
                        <asp:TextBox ID="txtRemarks" TabIndex="17" runat="server" Width="96%" CssClass="SmallFont gCtrTxt"
                            MaxLength="500" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label16" runat="server" CssClass="LabelNo" Text="Instructions :"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="85%">
                        <asp:TextBox ID="txtInstructions" runat="server" CssClass="SmallFont gCtrTxt" MaxLength="250"
                            TabIndex="18" TextMode="SingleLine" Width="96%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        Fetch From Indent :
                    </td>
                    <td align="left" valign="top" width="85%">
                        <asp:CheckBox ID="chkFetchIndent" runat="server" AutoPostBack="True" 
                            Width="10px" oncheckedchanged="chkFetchIndent_CheckedChanged" TabIndex="19" />
                        <asp:DropDownList ID="ddlIndent" runat="server" AutoPostBack="True" CssClass="SmallFont TextBox"
                            OnSelectedIndexChanged="ddlIndent_SelectedIndexChanged" Width="100px" Visible="False" TabIndex="20">
                        </asp:DropDownList>
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
                        <asp:Label ID="Label18" runat="server" CssClass="LabelNo titleheading" Text="Fabric Code"></asp:Label>
                    </td>
                    
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label20" runat="server" CssClass="LabelNo titleheading" Text="Adjust"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label21" runat="server" CssClass="LabelNo titleheading" Text="Quantity"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label22" runat="server" CssClass="LabelNo titleheading" Text="UOM1"></asp:Label>
                    </td>
                      <td class="tdLeft SmallFont">
                        <asp:Label ID="Label10" runat="server" CssClass="LabelNo titleheading" Text="UOM2"></asp:Label>
                    </td>
                      <td class="tdLeft SmallFont">
                        <asp:Label ID="Label11" runat="server" CssClass="LabelNo titleheading" Text="Uom_Bail"></asp:Label>
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
                    <td class="tdLeft">
                        <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="FIBER_CODE" 
                            EmptyText="Find Item" EnableLoadOnDemand="true"
                            Height="200px" MenuWidth="900px" OnLoadingItems="txtItemCode_LoadingItems" 
                            TabIndex="21" EnableVirtualScrolling="true" Width="100px" 
                            onselectedindexchanged="txtItemCode_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c3">
                                    DESCRIPTION</div>
                                <%--<div class="header c3">
                                    Shade Code</div>--%>
                                <div class="header c1 ralign">
                                    Appr Qty.</div>
                                <div class="header c1 ralign">
                                    Adj Qty</div>
                                <div class="header c1 ralign">
                                    Bal Qty</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container4" runat="server" Text='<%# Eval("FIBER_CODE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container5" runat="server" Text='<%# Eval("FIBER_DESC") %>' />
                                </div>
                               <%-- <div class="item c3">
                                    <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("SHADE_CODE") %>' />
                                </div>--%>
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
                        
                    </td>
                   <%-- <td class="tdLeft">
                        <asp:TextBox ID="txtShadeCode" TabIndex="19" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" Text='<%# Bind("FABR_CODE") %>' Width="100px"></asp:TextBox>
                    </td>--%>
                    <td class="tdLeft">
                        <asp:Button ID="btnAdjustIndent" TabIndex="20" runat="server" Font-Size="8pt" 
                            Text="Ind.Ajust" onclick="btnAdjustIndent_Click" />
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtOrderQty" TabIndex="23" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            runat="server" Width="70px" Text='<%# Bind("ORD_QTY") %>' ReadOnly="true" 
                            AutoPostBack="True" ontextchanged="txtOrderQty_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtUnit" TabIndex="24" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" ReadOnly="true" Width="25px" Text='<%# Bind("UOM") %>'></asp:TextBox>
                    </td>
                     <td class="tdLeft">
                        <asp:TextBox ID="txtUnit1" TabIndex="24" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" ReadOnly="true" Width="25px" Text='<%# Bind("UOM1") %>'></asp:TextBox>
                    </td>
                     <td class="tdLeft">
                        <asp:TextBox ID="txtkg_bail" TabIndex="24" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" ReadOnly="true" Width="25px" Text='<%# Bind("UOM_BAIL") %>'></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtBaseRate" TabIndex="25" runat="server" Width="60px" Text='<%# Bind("BASIC_RATE") %>'
                            CssClass="TextBoxNo SmallFont" AutoPostBack="True" 
                            ontextchanged="txtBaseRate_TextChanged"></asp:TextBox>
                        <%--<cc11:FilteredTextBoxExtender ID="fteBaseRate" runat="server" FilterType="Numbers"
                            TargetControlID="txtBaseRate">
                        </cc11:FilteredTextBoxExtender>--%>
                    </td>
                    <td class="tdLeft">
                        <asp:Button ID="btnDiscountTaxes" TabIndex="26" runat="server" Font-Size="8pt" Text="Disc/Taxes"
                            onclick="btnDiscountTaxes_Click" Width="65px" 
                             />
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtFinalRate" TabIndex="27" ReadOnly="true" runat="server" Text='<%# Bind("FINAL_RATE") %>'
                            Width="60px" CssClass="TextBoxNo SmallFont TextBoxDisplay" 
                            AutoPostBack="True" ontextchanged="txtFinalRate_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtAmount" TabIndex="26" Width="70px" ReadOnly="true" runat="server"
                            Text='<%# Bind("Amount") %>' CssClass="TextBoxNo SmallFont TextBoxDisplay "></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtQuotation" TabIndex="28" Width="70px" runat="server" Text='<%# Bind("QUOTATION_NO") %>'
                            CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="txtTrnDeliveryDate" TabIndex="29" runat="server" Width="60px" Text='<%# Bind("DEL_DATE") %>'
                            CssClass="TextBox SmallFont"></asp:TextBox>
                        <cc1:CalendarExtender ID="CE3" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTrnDeliveryDate">
                        </cc1:CalendarExtender>
                        
                        
                      <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtTrnDeliveryDate">
                        </cc1:MaskedEditExtender>
                        
                    </td>
                    <td class="tdLeft">
                        <asp:Button ID="btnSaveDetail" Text="Save" CssClass=" SmallFont" runat="server" 
                            onclick="btnSaveDetail_Click" TabIndex="30" Width="60px">
                        </asp:Button>
                        
                    </td>
                </tr>
                <tr>
                <td class="tdLeft">
                <asp:TextBox ID="lblItemCode" TabIndex="22" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" Text='<%# Bind("FABR_CODE") %>' Width="100px"></asp:TextBox>
                
                </td>
                    <td class="tdLeft" colspan="11">
                        <asp:TextBox ID="txtItemDescription" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" Text='<%# Bind("FABR_DESC") %>' Width="98%" TabIndex="32"></asp:TextBox>
                    </td>
                    <td>
                    <asp:Button ID="btnCancelDetail" Text="Cancel" CssClass=" SmallFont" 
                            runat="server" onclick="btnCancelDetail_Click" TabIndex="31" Width="60px"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%">
                <tr id="trMaterialPOCreditTRN" runat="server">
                    <td width="100%" class="td SmallFont">
                        <asp:GridView ID="gvMaterialPOTRN" TabIndex="16" runat="server" 
                            CssClass="SmallFont" AutoGenerateColumns="False" AllowSorting="True" 
                            AllowPaging="false" onrowcommand="gvMaterialPOTRN_RowCommand">
                            <RowStyle CssClass="SmallFont" Width="100%" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="5%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fiber Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtItemCode" TabIndex="17" Width="80px" Font-Bold="true" CssClass="Label SmallFont"
                                            runat="server" Text='<%# Bind("FIBER_CODE") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Fiber Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtItemDescription" TabIndex="19" ReadOnly="true" runat="server" Width="120px"
                                            CssClass="Label SmallFont" Text='<%# Bind("FIBER_DESC") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Uom">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUnit" TabIndex="22" CssClass="Label SmallFont" runat="server" ReadOnly="true"
                                            Width="50px" Text='<%# Bind("UOM") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Uom1">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUnit1" TabIndex="22" CssClass="Label SmallFont" runat="server" ReadOnly="true"
                                            Width="50px" Text='<%# Bind("UOM1") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Uom_Bail">
                                    <ItemTemplate>
                                        <asp:Label ID="Uom_Bail" TabIndex="22" CssClass="Label SmallFont" runat="server" ReadOnly="true"
                                            Width="50px" Text='<%# Bind("UOM_BAIL") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>
                               <%-- <asp:TemplateField HeaderText="Shade Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtShadeCode" TabIndex="22" CssClass="Label SmallFont" runat="server"
                                            ReadOnly="true" Width="50px" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
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
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>    
    </ContentTemplate>
</asp:UpdatePanel>