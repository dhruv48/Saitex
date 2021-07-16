<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YARN_SO.ascx.cs" Inherits="Module_Yarn_SalesWork_Controls_YARN_SO" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>


<script language="javascript" type="text/javascript">
        
    function Calculation(val)
    { 
        var name=val;
                   
        document.getElementById('ctl00_cphBody_YARN_SO1_txtAdvanceAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_YARN_SO1_txtAdvance').value)*(parseFloat(document.getElementById('ctl00_cphBody_YARN_SO1_txtFinalTotal').value)/100)).toFixed(3) ;
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
   
</style>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="tContentArial" width="95%">
            <tr>
                <td class="td tdLeft" width="100%">
                    <table align="left">
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" 
                                    ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" 
                                    ToolTip="Update" ValidationGroup="M1" Width="48" TabIndex="23"/>
                            </td>
                            <td ID="tdfind">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" 
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click" 
                                    ToolTip="Find" Width="48" TabIndex="24" />
                            </td>
                            <td ID="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" 
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" 
                                    ToolTip="Print" Width="48" TabIndex="25" />
                                &nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" 
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" ToolTip="Clear" 
                                    Width="48" TabIndex="26" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" 
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" 
                                    ToolTip="Exit" Width="48" TabIndex="27" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" 
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click" 
                                    ToolTip="Help" Width="48" TabIndex="28" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <span class="titleheading"><b>Yarn Sales Order </b></span>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft">
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                    &nbsp;Mode </span>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td class="td SmallFont" width="100%">
                    <table width="100%">
                        <tr style="font-weight: bold">
                            <td align="right" valign="top" width="15%">
                                <asp:Label ID="Label2" runat="server" CssClass="LabelNo" Text="Order Type :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" 
                                    CssClass="TextBox SmallFont" Font-Bold="true" 
                                    OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged" TabIndex="1" 
                                    Width="125px">
                                    <asp:ListItem Value="SYM">Main Order</asp:ListItem>
                                    <asp:ListItem Value="SYS">Supplimentry Order</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="15%">
                                <asp:Label ID="Label3" runat="server" CssClass="LabelNo" Text="Order Number :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="TextBoxNo SmallFont" 
                                    Font-Bold="true" MaxLength="10" OnTextChanged="txtOrderNumber_TextChanged1" 
                                    ReadOnly="True" TabIndex="2" Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvon" runat="server" 
                                    ControlToValidate="txtOrderNumber" Display="Dynamic" 
                                    ErrorMessage="*Order number required" Font-Bold="False" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <cc2:combobox id="ddlOrderNumber" runat="server" autopostback="True" 
                                    cssclass="SmallFont" datatextfield="SO_NUMB" datavaluefield="SO_NUMB" 
                                    emptytext="Find Item" enableloadondemand="true" height="200px" 
                                    menuwidth="450px" onloadingitems="ddlOrderNumber_LoadingItems" 
                                    onselectedindexchanged="ddlOrderNumber_SelectedIndexChanged" tabindex="125" 
                                    width="100px">
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
                                        Displaying items - out of .
                                    </FooterTemplate>
                                </cc2:combobox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                <asp:Label ID="Label4" runat="server" CssClass="LabelNo" Text="Order Date :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="25%">
                                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="TextBox SmallFont" 
                                    Font-Bold="true" MaxLength="25" TabIndex="3" Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvod" runat="server" 
                                    ControlToValidate="txtOrderDate" Display="Dynamic" 
                                    ErrorMessage="*Order Date Required" Font-Bold="False" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <cc1:calendarextender id="CE1" runat="server" targetcontrolid="txtOrderDate">
                                </cc1:calendarextender>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td SmallFont" width="100%">
                    <table width="100%">
                        <tr>
                            <td align="right" valign="top" width="16%">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="Party Code :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <cc2:combobox id="txtPartyCode" runat="server" autopostback="True" 
                                    datatextfield="PRTY_CODE" datavaluefield="Address" emptytext="Select Vendor" 
                                    enableloadondemand="true" height="200px" menuwidth="350px" 
                                    onloadingitems="txtPartyCode_LoadingItems" 
                                    onselectedindexchanged="txtPartyCode_SelectedIndexChanged" width="150px" TabIndex="4">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c2">
                                            NAME</div>
                                        <div class="header c3">
                                            Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Container3" runat="server" Text='<%# Eval("Address") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:combobox>
                            </td>
                            <td align="left" valign="top" width="69%">
                                <asp:TextBox ID="txtPartyAddress" runat="server" 
                                    CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="True" TabIndex="400" 
                                    TextMode="SingleLine" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="16%">
                                <asp:Label ID="Label5" runat="server" CssClass="LabelNo" 
                                    Text="Transporter Code :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <cc2:combobox id="txtTransporterCode" runat="server" autopostback="True" 
                                    datatextfield="PRTY_CODE" datavaluefield="Address" 
                                    emptytext="Select transaporter" enableloadondemand="true" height="200px" 
                                    menuwidth="350px" onloadingitems="txtTransporterCode_LoadingItems" 
                                    onselectedindexchanged="txtTransporterCode_SelectedIndexChanged" width="150px" TabIndex="5">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c2">
                                            NAME</div>
                                        <div class="header c3">
                                            Address</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container10" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container11" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Container12" runat="server" Text='<%# Eval("Address") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:combobox>
                            </td>
                            <td align="left" valign="top" width="69%">
                                <asp:TextBox ID="txtTransporterName" runat="server" 
                                    CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="True" TabIndex="500" 
                                    TextMode="SingleLine" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td SmallFont" width="100%">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="15%">
                                PO Nature :
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:DropDownList ID="ddlPONature" runat="server" CssClass="SmallFont TextBox" TabIndex="6">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label15" runat="server" CssClass="LabelNo" 
                                    Text="Currency Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:DropDownList ID="txtCurrencyCode" runat="server" 
                                    CssClass="SmallFont TextBox" Width="80%" TabIndex="7">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label17" runat="server" CssClass="LabelNo" 
                                    Text="Conversion Rate :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtconversionRate" runat="server" 
                                    CssClass="SmallFont TextBoxNo" TabIndex="8" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td SmallFont" width="100%">
                    <table width="100%">
                        <tr>
                            <td align="right" valign="top" width="15%">
                                <asp:Label ID="Label8" runat="server" CssClass="LabelNo" 
                                    Text="Delivery Address :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="35%">
                                <asp:TextBox ID="txtDelAddress" runat="server" CssClass="SmallFont gCtrTxt" 
                                    TabIndex="9" Width="98%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="20%">
                                <asp:Label ID="Label7" runat="server" CssClass="LabelNo" Text="Delivery Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="30%">
                                <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="SmallFont gCtrTxt" 
                                    MaxLength="25" TabIndex="10" Width="60px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvd" runat="server" 
                                    ControlToValidate="txtDeliveryDate" Display="Dynamic" 
                                    ErrorMessage="*Del Date required" Font-Bold="False" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <cc1:calendarextender id="CE2" runat="server" targetcontrolid="txtDeliveryDate">
                                </cc1:calendarextender>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label6" runat="server" CssClass="LabelNo" Text="Pay Term :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="35%">
                                <asp:TextBox ID="txtPayTerm" runat="server" CssClass="SmallFont gCtrTxt" 
                                    MaxLength="50" TabIndex="11" Width="99%"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="20%">
                                <asp:Label ID="Label9" runat="server" CssClass="LabelNo" Text="Despatch Mode :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="30%">
                                <asp:TextBox ID="txtDespatchMode" runat="server" CssClass="SmallFont gCtrTxt" 
                                    MaxLength="200" TabIndex="12" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td SmallFont" width="100%">
                    <table width="100%">
                        <tr>
                            <td align="right" valign="top" width="15%">
                                <asp:Label ID="Label14" runat="server" CssClass="LabelNo" Text="Remarks :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="85%">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="SmallFont gCtrTxt" 
                                    MaxLength="500" TabIndex="13" TextMode="SingleLine" Width="96%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="15%">
                                <asp:Label ID="Label16" runat="server" CssClass="LabelNo" Text="Instructions :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="85%">
                                <asp:TextBox ID="txtInstructions" runat="server" CssClass="SmallFont gCtrTxt" 
                                    MaxLength="500" TabIndex="14" TextMode="SingleLine" Width="96%"></asp:TextBox>
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
                                <asp:Label ID="Label18" runat="server" CssClass="LabelNo titleheading" 
                                    Text="Yarn Code"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont">
                                <asp:Label ID="Label19" runat="server" CssClass="LabelNo titleheading" 
                                    Text="Description"></asp:Label>
                            </td>
                            <td class="tdRight SmallFont">
                                <asp:Label ID="Label21" runat="server" CssClass="LabelNo titleheading" 
                                    Text="Quantity"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont">
                                <asp:Label ID="Label22" runat="server" CssClass="LabelNo titleheading" 
                                    Text="UOM"></asp:Label>
                            </td>
                            <td class="tdRight SmallFont">
                                <asp:Label ID="Label23" runat="server" CssClass="LabelNo titleheading" 
                                    Text="Basic Rate"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont">
                                <asp:Label ID="Label24" runat="server" CssClass="LabelNo titleheading" 
                                    Text="Dis./Tax"></asp:Label>
                            </td>
                            <td class="tdRight SmallFont">
                                <asp:Label ID="Label25" runat="server" CssClass="LabelNo titleheading" 
                                    Text="Final Rate"></asp:Label>
                            </td>
                            <td class="tdRight SmallFont">
                                <asp:Label ID="Label26" runat="server" CssClass="LabelNo titleheading" 
                                    Text="Amount"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont">
                                <asp:Label ID="Label27" runat="server" CssClass="LabelNo titleheading" 
                                    Text="Quotation No"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont">
                                <asp:Label ID="Label28" runat="server" CssClass="LabelNo titleheading" 
                                    Text="Delivery Date"></asp:Label>
                            </td>
                            <td class="tdLeft SmallFont">
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                <cc2:combobox id="txtItemCode" runat="server" autopostback="True" 
                                    cssclass="SmallFont" datatextfield="YARN_CODE" datavaluefield="YARN_CODE" 
                                    emptytext="Find Item" enableloadondemand="true" height="200px" 
                                    menuwidth="800px" onloadingitems="txtItemCode_LoadingItems" 
                                    onselectedindexchanged="txtItemCode_SelectedIndexChanged" tabindex="15" 
                                    width="100px">
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
                                        Displaying items - out of .
                                    </FooterTemplate>
                                </cc2:combobox>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtItemDescription" runat="server" 
                                    CssClass="TextBox SmallFont TextBoxDisplay" ReadOnly="true" TabIndex="190" 
                                    Text='<%# Bind("YARN_DESC") %>' Width="120px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtOrderQty" runat="server" AutoPostBack="True" 
                                    CssClass="TextBoxNo SmallFont " 
                                    OnTextChanged="txtOrderQty_TextChanged" TabIndex="16" 
                                    Width="70px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtUnit" runat="server" 
                                    CssClass="TextBox SmallFont TextBoxDisplay" ReadOnly="true" TabIndex="220" 
                                    Text='<%# Bind("UOM") %>' Width="25px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtBaseRate" runat="server" AutoPostBack="True" 
                                    CssClass="TextBoxNo SmallFont" OnTextChanged="txtBaseRate_TextChanged1" 
                                    TabIndex="17" Text='<%# Bind("BASIC_RATE") %>' Width="60px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:Button ID="btnDiscountTaxes" runat="server" Font-Size="8pt" 
                                    OnClick="btnDiscountTaxes_Click1" TabIndex="18" Text="Disc/Taxes" 
                                    Width="50px" />
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtFinalRate" runat="server" AutoPostBack="True" 
                                    CssClass="TextBoxNo SmallFont TextBoxDisplay" 
                                    OnTextChanged="txtFinalRate_TextChanged" ReadOnly="true" TabIndex="250" 
                                    Text='<%# Bind("FINAL_RATE") %>' Width="60px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtAmount" runat="server" 
                                    CssClass="TextBoxNo SmallFont TextBoxDisplay " ReadOnly="true" TabIndex="260" 
                                    Text='<%# Bind("Amount") %>' Width="70px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtQuotation" runat="server" CssClass="TextBoxNo SmallFont" 
                                    TabIndex="19" Text='<%# Bind("QUOTATION_NO") %>' Width="70px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtTrnDeliveryDate" runat="server" 
                                    CssClass="TextBox SmallFont" TabIndex="20" Text='<%# Bind("DEL_DATE") %>' 
                                    Width="60px"></asp:TextBox>
                                <cc1:calendarextender id="CE3" runat="server" 
                                    targetcontrolid="txtTrnDeliveryDate">
                                </cc1:calendarextender>
                            </td>
                            <td class="tdLeft">
                                <asp:Button ID="btnSaveDetail" runat="server" OnClick="btnSaveDetail_Click" 
                                    Text="Save" TabIndex="21" />
                                <asp:Button ID="btnCancelDetail" runat="server" OnClick="btnCancelDetail_Click" 
                                    Text="Cancel" TabIndex="22" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr ID="trMaterialPOCreditTRN" runat="server">
                <td class="td SmallFont" width="100%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvMaterialSOTRN" runat="server" AllowPaging="false" 
                                AllowSorting="True" AutoGenerateColumns="False" CssClass="SmallFont" 
                                OnRowCommand="gvMaterialSOTRN_RowCommand" TabIndex="16">
                                <RowStyle CssClass="SmallFont" Width="98%" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Yarn Code">
                                        <ItemTemplate>
                                            <asp:Label ID="txtItemCode" runat="server" AutoCompleteType="Disabled" 
                                                CssClass="Label SmallFont" Font-Bold="true" ReadOnly="true" TabIndex="17" 
                                                Text='<%# Bind("YARN_CODE") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Yarn Description">
                                        <ItemTemplate>
                                            <asp:Label ID="txtItemDescription" runat="server" CssClass="Label SmallFont" 
                                                ReadOnly="true" TabIndex="19" Text='<%# Bind("YARN_DESC") %>' Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="txtOrderQty" runat="server" CssClass="LabelNo SmallFont" 
                                                Font-Bold="true" ReadOnly="true" TabIndex="21" Text='<%# Bind("ORD_QTY") %>' 
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%" />
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="txtUnit" runat="server" CssClass="Label SmallFont" 
                                                ReadOnly="true" TabIndex="22" Text='<%# Bind("UOM") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Base Rate">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%" />
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtBaseRate" runat="server" CssClass="LabelNo SmallFont" 
                                                ReadOnly="true" TabIndex="23" Text='<%# Bind("BASIC_RATE") %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Final Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="txtFinalRate" runat="server" CssClass="LabelNo SmallFont" 
                                                Font-Bold="true" ReadOnly="true" TabIndex="25" Text='<%# Bind("FINAL_RATE") %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%" />
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" 
                                                Font-Bold="true" ReadOnly="true" TabIndex="26" Text='<%# Bind("Amount") %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%" />
                                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quotation No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurr" runat="server" Text='<%# Bind("QUOTATION_nO") %>' 
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Del Date">
                                        <ItemTemplate>
                                            <asp:Label ID="txtTrnDeliveryDate" runat="server" CssClass="Label SmallFont" 
                                                ReadOnly="true" TabIndex="28" Text='<%# Bind("DEL_DATE", "{0:d}") %>' 
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" 
                                                CommandArgument='<%# Eval("UniqueId") %>' CommandName="SOMateialCreditEdit" 
                                                TabIndex="29" Text="Edit"></asp:LinkButton>
                                            /
                                            <asp:LinkButton ID="lnkDelete" runat="server" 
                                                CommandArgument='<%# Eval("UniqueId") %>' CommandName="SOMateialCreditDelete" 
                                                TabIndex="29" Text="Del"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="SmallFont" />
                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtOrderNumber" EventName="TextChanged" />
                            <asp:AsyncPostBackTrigger ControlID="imgbtnClear" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgbtnFind" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="imgbtnUpdate" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvMaterialSOTRN" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            </table> 
</ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnClear" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="imgbtnFind" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="imgbtnUpdate" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="txtDeliveryDate" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtOrderNumber" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtPartycode" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtTransporterCode" 
            EventName="TextChanged" />
    </Triggers>
 </asp:UpdatePanel>