<%@ Register Src="../../../CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"
    TagPrefix="uc1" %>
<%@ Register Src="../../../CommonControls/LOV/OrderNoLOV.ascx" TagName="OrderNoLOV"
    TagPrefix="uc3" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OC_YARN_SPINING.ascx.cs"
    Inherits="Module_OrderDevelopment_Controls_OC_YARN_SPINING" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
        width: 80px;
    }
    .c2
    {
        margin-left: 2px;
        width: 150px;
    }
    .c3
    {
        margin-left: 2px;
        width: 300px;
    }
    .c4
    {
        margin-left: 2px;
        width: 40px;
    }
    .c5
    {
        margin-left: 2px;
        width: 60px;
    }
    .c6
    {
        margin-left: 2px;
        width: 60px;
    }
    .c7
    {
        margin-left: 2px;
        width: 100px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
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
                    <b class="titleheading"><%--Order Capture Form for--%>Production Order For
                        <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="TRN" />
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
                    <table width="100%" style="font-weight: bold">
                        <tr>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblBusinessType" runat="server" Text="Business Type :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlBusinessType" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont"
                                    Width="98%" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblProdType" runat="server" Text="Product Type :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont"
                                    Width="99%" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblOrderCategory" runat="server" Text="Order Category :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlOrderCategory" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont"
                                    Width="98%" OnSelectedIndexChanged="ddlOrderCategory_SelectedIndexChanged">
                                    <asp:ListItem>DIRECT SALE</asp:ListItem>
                                    <asp:ListItem>INHOUSE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblOrderNo" runat="server" Text="Order number :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:TextBox ID="txtOrderNo" runat="server" ReadOnly="true" CssClass="TextBoxNo TextBoxDisplay SmallFont BoldFont"
                                    Width="98%"></asp:TextBox>
                                <uc3:OrderNoLOV ID="ddlOrderNo" runat="server"  Width="98%"  />
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblOrderDate" runat="server" Text="Order Date :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:TextBox ID="txtOrderDate" runat="server" ReadOnly="true" CssClass="TextBox TextBoxDisplay SmallFont BoldFont"
                                    Width="98%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblOrderType" runat="server" Text="Order Type :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="16%">
                                <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont UPPERCASE"
                                    Width="99%" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                           
                        </tr>
                         <tr id="TR0" runat="server" visible="false">
                               <td class="tdRight" width="12%">
                                <asp:Label ID="lblCurrencyCode" runat="server" Text="Currency Code :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlCurrencyCode" runat="server" AutoPostBack="True" CssClass="SmallFont BoldFont"
                                    Width="98%" OnSelectedIndexChanged="ddlCurrencyCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblConversionRate" runat="server" Text="Conversion Rate :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="16%">
                                <asp:TextBox ID="txtConversionRate" runat="server" CssClass="TextBoxNo SmallFont BoldFont"
                                    Width="97%"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td></td>
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
                                <cc2:ComboBox ID="txtPartyCodecmb" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtPartyCodecmb_LoadingItems" DataTextField="PRTY_CODE" DataValueField="ADDRESS"
                                    EmptyText="Select Party" OnSelectedIndexChanged="txtPartyCodecmb_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="150px" MenuWidth="500px" Height="200px">
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
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ADDRESS") %>' /></div>
                                            
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:TextBox ID="txtPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="100%" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="64%">
                                <asp:TextBox ID="txtPartyDetail" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="100%" ReadOnly="True"></asp:TextBox>
                             
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr id="TR1" runat="server" visible="false">
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
                                <asp:DropDownList ID="ddlOrderProcess" runat="server" MenuWidth="200px" Width="98%"
                                    CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblShipment" runat="server" Text="Shipment :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="16%">
                                <asp:TextBox ID="txtShipment" runat="server" CssClass="TextBox SmallFont" MaxLength="500"
                                    Width="100%"></asp:TextBox>
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
                                <uc1:PartyCodeLOV ID="ddlConsignee" runat="server" />
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:TextBox ID="txtconsigneeName" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                    MaxLength="100" Width="98%" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="64%" style="margin-left: 40px">
                                <asp:TextBox ID="txtConsigneeAdd" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                    Width="100%" MaxLength="500" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="TR2" runat="server" visible="false">
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="True" MenuWidth="200px"
                                    CssClass="SmallFont" Width="98%" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged">
                                    <asp:ListItem Text="DD" Value="DD"></asp:ListItem>
                                    <asp:ListItem Text="CHEQUE" Value="CHEQUE"></asp:ListItem>
                                    <asp:ListItem Text="CASH" Value="CASH"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblPaymentTerm" runat="server" Text="Payment Term :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="64%">
                                <asp:TextBox ID="txtPaymentTerm" runat="server" CssClass="TextBox SmallFont" Width="100%"
                                    MaxLength="500"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="TR3" runat="server" visible="false">
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblDeliveryMode" runat="server" Text="Delivery Mode :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlDeliveryMode" runat="server" AutoPostBack="True" MenuWidth="200px"
                                    CssClass="SmallFont" Width="98%" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged">
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
                                    CssClass="SmallFont" Width="20%">
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
                        <tr id="TR4" runat="server" visible="false">
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblBillTo" runat="server" Text="Bill To :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="88%">
                                <asp:TextBox ID="txtBillTo" runat="server" CssClass="TextBox SmallFont" Width="100%"
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
                                    MaxLength="800"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblsplInstr" runat="server" Text="Special Instruction :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="88%">
                                <asp:TextBox ID="txtSplInstruction" runat="server" CssClass="TextBox SmallFont" Width="100%"
                                    MaxLength="800"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td SmallFont">
                    <%-- <asp:Panel ID="pnlTRNDetail" runat="server" Width="100%" Style="border-color: Blue;
                        border-width: 1px;">--%>
                    <table width="100%">
                        <tr>
                            <td class="td" width="100%">
                                <table width="100%">
                                    <tr bgcolor="#336699" class="SmallFont titleheading">
                                        <td class="tdLeft">
                                            Select Artical Code
                                        </td>
                                        <td class="td" align="center">
                                            Shade Code
                                        </td>
                                        <td align="center" class="td">
                                            Adj Cust Req
                                        </td>
                                        <td class="tdRight">
                                            Total Quantity
                                        </td>
                                        <td class="tdRight">
                                            Final Del. Date
                                        </td>
                                        <td class="tdRight">
                                            Waste (%)
                                        </td>
                                        <td class="tdRight">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLeft">
                                            <cc2:ComboBox ID="txtTRNYarnSpiningArticalCode" runat="server" AutoPostBack="True"
                                                CssClass="smallfont" EnableLoadOnDemand="True" DataTextField="ARTICAL_CODE" DataValueField="Combined"
                                                MenuWidth="900" OnLoadingItems="txtTRNYarnSpiningArticalCode_LoadingItems" OnSelectedIndexChanged="txtTRNYarnSpiningArticalCode_SelectedIndexChanged"
                                                EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="11" Visible="true"
                                                Height="200px">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        CODE</div>
                                                    <div class="header c3">
                                                        DESCRIPTION</div>
                                                    <div class="header c1">
                                                        SHADE CODE</div>
                                                    <div class="header c1">
                                                        QTY.REQ
                                                    </div>
                                                    <div class="header c1">
                                                        QTY.APPR</div>
                                                    <div class="header c1">
                                                        QTY.BAL</div>
                                                        <div class="header c1">
                                                        ORDER NO.</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <%# Eval("ARTICAL_CODE")%></div>
                                                    <div class="item c3">
                                                        <%# Eval("ARTICAL_DESC")%></div>
                                                    <div class="item c1">
                                                        <%# Eval("SHADE_CODE") %></div>
                                                    <div class="item c1">
                                                        <%# Eval("QUANTITY")%></div>
                                                    <div class="item c1">
                                                        <%# Eval("QTY_APPROVED")%></div>
                                                    <div class="item c1">
                                                        <%# Eval("QTYBAL")%></div>
                                                        <div class="item c1">
                                                        <%# Eval("ORDER_NO")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:TextBox ID="txtShadeCode" ReadOnly="true" CssClass="SmallFont TextBoxDisplay"
                                                runat="server"></asp:TextBox>
                                            <%--<asp:DropDownList ID="ddlShadeCode" CssClass="SmallFont"  runat="server" 
                                                    AutoPostBack="True" onselectedindexchanged="ddlShadeCode_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtShadeCode"
                                                Display="None" ErrorMessage="Please Select Shade Code" ValidationGroup="TRN"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:Button ID="btnAdjCustReq" runat="server" CssClass="SmallFont" OnClick="btnAdjCustReq_Click"
                                                Text="Adj Cust Req" Width="100%" />
                                        </td>
                                        <td class="tdRight">
                                            <asp:TextBox ID="txtTRNYarnSpiningOrderQty" Width="100%" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                runat="server" OnTextChanged="txtTRNYarnSpiningOrderQty_TextChanged" ReadOnly="True"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTRNYarnSpiningOrderQty"
                                                ValidationGroup="TRN" Display="None" ErrorMessage="Please Enter Order Quantity"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="tdRight">
                                            <asp:TextBox ID="txtTRNYarnSpiningDelDate" runat="server" CssClass="SmallFont TextBoxNo"
                                                Width="100%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="TRN" runat="server"
                                                ControlToValidate="txtTRNYarnSpiningDelDate" Display="None" ErrorMessage="Please Select Enter Date"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="tdLeft" ">
                                            <asp:TextBox ID="txtTRNYarnSpiningSrinkage" runat="server" CssClass="TextBoxNo SmallFont"
                                                Width="100%" MaxLength="2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="TRN" runat="server"
                                                ControlToValidate="txtTRNYarnSpiningSrinkage" Display="None" ErrorMessage="Please Enter Shrinkage"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" ValidationGroup="TRN" runat="server" ErrorMessage="Percentage In Between 0 - 100%"
                                                ControlToValidate="txtTRNYarnSpiningSrinkage" Display="None" MaximumValue="100"
                                                MinimumValue="0" Type="Double"></asp:RangeValidator>
                                                
                                                <cc1:FilteredTextBoxExtender ID="FiltertxtTRNYarnSpiningSrinkage" runat="server"  TargetControlID="txtTRNYarnSpiningSrinkage"   FilterType="Custom, Numbers" ValidChars="."/>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:Button ID="btnTrnSave" runat="server" Text="Save" OnClick="btnTrnSave_Click"
                                                ValidationGroup="TRN" />
                                            <asp:Button ID="btnTrnCancel" runat="server" Text="Cancel" OnClick="btnTrnCancel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdLeft" colspan="7">
                                            <b>Article Code :<asp:TextBox ID="lblTRNYarnSpiningArticalCode" CssClass="TextBoxDisplay TextBox SmallFont "
                                                runat="server" Text=""></asp:TextBox>
                                                Art. Desc :
                                                <asp:TextBox ID="lblTRNYSpinDesc" runat="server" CssClass="TextBoxDisplay TextBox SmallFont "
                                                    ReadOnly="true" Width="400px"></asp:TextBox>
                                                Unit :<asp:TextBox ID="txtTRNYarnSpiningUOM" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                                    ReadOnly="True" Wrap="true" Width="100px"></asp:TextBox>
                                                <asp:Label ID="lblpi_no" Visible="false" runat="server" Text=""></asp:Label>
                                                 
                                                 <asp:Label ID="lblCRNo" runat="server" Visible="false" Text=""></asp:Label>
                                        
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr width="100%">
                            <td class="td" width="100%" align="left" >
                                <asp:Panel ID="pnlTRNYarnSpiningGrid" runat="server" Width="100%">
                                    <asp:GridView ID="grdTRNYarnSpiningDetail" runat="server" AutoGenerateColumns="False"
                                        CssClass="SmallFont" OnRowCommand="grdTRNYarnSpiningDetail_RowCommand" Width="100%"
                                        OnRowDataBound="grdTRNYarnSpiningDetail_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="PI Indent #">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningPI_NO" runat="server" CssClass="LabelNo SmallFont"
                                                        Text='<%# Bind("PI_NO") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Artical Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningArticalCode" runat="server" CssClass="Label SmallFont"
                                                        Text='<%# Bind("ARTICAL_CODE") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false" HeaderText="FINAL ORDER CONF CLAG">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFINAL_ORDER_CONF_CLAG" runat="server" CssClass="Label SmallFont"
                                                        Text='<%# Bind("FINAL_ORDER_CONF_CLAG") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Shade Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningShade" runat="server" CssClass="LabelNo SmallFont"
                                                        Text='<%# Bind("SHADE_CODE") %>'></asp:Label></ItemTemplate>
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
                                            <asp:TemplateField HeaderText="Del Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningOrderDelDate" runat="server" CssClass="LabelNo SmallFont"
                                                        Text='<%# Bind("DEL_DATE","{0:d}") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Srinkage/ Process Loss" HeaderStyle-Width="75px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTRNYarnSpiningSrinkage" runat="server" CssClass="Label SmallFont"
                                                        Text='<%# Bind("SRINKAGE") %>'></asp:Label></ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">
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
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="cePartyPODate" runat="server" TargetControlID="txtPartyRefDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="cetxtDeliverySchedule" OnClientDateSelectionChanged="checkBackDate"
            runat="server" TargetControlID="txtTRNYarnSpiningDelDate" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MEditApplicationDate" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtTRNYarnSpiningDelDate" PromptCharacter="_"
            UserDateFormat="DayMonthYear">
        </cc1:MaskedEditExtender>
 <%--   </ContentTemplate>
</asp:UpdatePanel>--%>