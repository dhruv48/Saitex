<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BillEntryFabric.ascx.cs"
    Inherits="Module_Inventory_Controls_BillEntryFabric" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
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
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table class="tContentArial" width="945px">
            <tr>
                <td class="td" width="945px">
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
                <td align="center" class="TableHeader td" width="945px">
                    <b class="titleheading">Party/Vendor Bill Entry for Fabric</b>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="945px">
                    <asp:ValidationSummary ID="VsMain" runat="server" ShowMessageBox="True" ValidationGroup="M1"
                        ShowSummary="False" />
                    <asp:ValidationSummary ID="VsTRN" runat="server" ShowMessageBox="True" ValidationGroup="T1"
                        ShowSummary="False" />
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="945px">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                        Mode </span>
                </td>
            </tr>
            <tr>
                <td width="945px" class="td">
                    <table width="945px">
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblBillType" runat="server" Text="Bill Type :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc3:OboutDropDownList ID="ddlBillType" runat="server" OnSelectedIndexChanged="ddlBillType_SelectedIndexChanged"
                                    AutoPostBack="True" MenuWidth="200px">
                                </cc3:OboutDropDownList>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblBillNumb" runat="server" Text="Bill Number :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="TextBoxNo TextBoxDisplayTextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                                <cc2:ComboBox ID="ddlFindBill" runat="server" EmptyText="select..." Height="200px"
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="80px"
                                    OnLoadingItems="ddlFindBill_LoadingItems" OnSelectedIndexChanged="ddlFindBill_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Bill Year</div>
                                        <div class="header c1">
                                            Bill Type</div>
                                        <div class="header c1">
                                            Bill #</div>
                                        <div class="header c3">
                                            Party</div>
                                        <div class="header c2">
                                            Amount</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("BILL_YEAR") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("BILL_TYPE") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("BILL_NUMB") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("PARTY") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("BILL_AMNT") %>' />
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
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblBillDate" runat="server" Text="Party Bill Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtPartyBillDate" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="945px" class="td">
                    <table width="945px">
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblPartyCode" runat="server" Text="Party Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="ddlParty" runat="server" EmptyText="select..." AutoPostBack="True"
                                    EnableLoadOnDemand="true" MenuWidth="580px" Width="99%" OnLoadingItems="ddlParty_LoadingItems"
                                    OnSelectedIndexChanged="ddlParty_SelectedIndexChanged" Height="200px">
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
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("Address") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdLeft" width="70%">
                                <asp:TextBox ID="txtPartyDetail" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="98%" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="945px" class="td">
                    <table width="945px">
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblPartyBillAmount" runat="server" Text="Party Bill Amount :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtPartyBillAmount" runat="server" CssClass="TextBoxNo SmallFont" Enabled ="true" 
                                    AutoPostBack="True" MaxLength="10" OnTextChanged="txtPartyBillAmount_TextChanged"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="30%">
                                <asp:TextBox ID="txtPartyBillAmtInWords" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="98%" ReadOnly="True" Font-Size="XX-Small"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblSuppType" runat="server" Text="Supp. Type :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <cc3:OboutDropDownList ID="ddlSuppType" AppendDataBoundItems="true" runat="server"
                                    MenuWidth="200px">
                                </cc3:OboutDropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="945px" class="td">
                    <table width="945px">
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblVatCategory" runat="server" Text="Vat Category :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc3:OboutDropDownList ID="ddlVatCategory" AppendDataBoundItems="true" runat="server"
                                    MenuWidth="200px">
                                </cc3:OboutDropDownList>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblVatTRNAmount" runat="server" Text="Vat TRN Amount :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtVatTRNAmount" runat="server" CssClass="TextBoxNo SmallFont" Rows="12"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblVatAmount" runat="server" Text="Vat Amount :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtVatAmount" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="12"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="945px" class="td">
                    <table width="945px">
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="85%">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" Width="98%"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="945px" class="td">
                    <table width="945px">
                        <tr>
                            <td class="tdRight" width="20%">
                                <asp:Label ID="lblFwdToPurDept" runat="server" Text="Frwd. To Purchase Dept. :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="10%">
                                <asp:CheckBox ID="chkFwdToPurDept" runat="server" AutoPostBack="True" OnCheckedChanged="chkFwdToPurDept_CheckedChanged" />
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblForwardedBy" runat="server" Text="Forwarded By :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtForwardedBy" runat="server" ReadOnly="true" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="lblForwardedDate" runat="server" Text="Forwarded Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtForwardedDate" runat="server" ReadOnly="true" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="945px" class="td">
                    <table width="98%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            <td>
                            </td>
                            <td>
                                Year
                            </td>
                            <td>
                                TRN Type
                            </td>
                            <td>
                                TRN #
                            </td>
                            <td>
                                TRN Amount
                            </td>
                            <td>
                                P Flag
                            </td>
                            <td>
                                Quality
                            </td>
                            <td>
                                Del.
                            </td>
                            <td>
                                Price
                            </td>
                            <td>
                                Support
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc2:ComboBox ID="ddlTRNDetail" runat="server" CssClass="SmallFont" EmptyText="select..."
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="80px"
                                    OnLoadingItems="ddlTRNDetail_LoadingItems" OnSelectedIndexChanged="ddlTRNDetail_SelectedIndexChanged"
                                    Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Year</div>
                                        <div class="header c1">
                                            Trn Type</div>
                                        <div class="header c1">
                                            Trn #</div>
                                        <div class="header c2">
                                            Amount</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("YEAR") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("TRN_TYPE") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("TRN_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("AMOUNT") %>' />
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
                            <td>
                                <asp:TextBox ID="txtTRNYear" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTRNType" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="40px"></asp:TextBox>
                            </td>
                            <td style="font-weight: 700">
                                <asp:TextBox ID="txtTRNNo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="40px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTRNAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="50px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTRNPFlag" runat="server" MaxLength="1" CssClass="TextBoxNo SmallFont"
                                    Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTRNQuality" runat="server" CssClass="TextBoxNo SmallFont" Width="50px"
                                    MaxLength="4"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTRNDel" runat="server" CssClass="TextBoxNo SmallFont" Width="50px"
                                    MaxLength="4"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTRNPrice" runat="server" CssClass="TextBoxNo SmallFont" Width="50px"
                                    MaxLength="4"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTRNSupport" runat="server" CssClass="TextBoxNo SmallFont" Width="50px"
                                    MaxLength="4"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save" ValidationGroup="T1" />
                                <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                        <asp:GridView ID="grdBillTRNDetail" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                            Width="99%" ShowFooter="false" OnRowCommand="grdBillTRNDetail_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="txtYear" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YEAR") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TRN Type">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTRN_Type" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TRN_TYPE") %>'
                                            ReadOnly="True" Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TRN #">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTRN_NUMB" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TRN_NUMB") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TRN Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTRN_Amount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_AMT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P Flag">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPRN_FLAG" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("PRN_FLAG") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quality">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQUALITY_POINT" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("QUALITY_POINT") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Del.">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDEL_POINT" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("DEL_POINT") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPRICE_POINT" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("PRICE_POINT") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Support">
                                    <ItemTemplate>
                                        <asp:Label ID="txtSUPPORT_POINT" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("SUPPORT_POINT") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditTRNDEtail" Text="Edit"
                                            CommandArgument='<%# Bind("UNIQUE_ID") %>'></asp:LinkButton>
                                        /
                                        <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelTRNDetail" Text="Delete"
                                            CommandArgument='<%# Bind("UNIQUE_ID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="SmallFont" />
                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="cePartyBillDate" runat="server" TargetControlID="txtPartyBillDate"
            PopupPosition="TopLeft">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="ceFwdDate" runat="server" TargetControlID="txtForwardedDate"
            PopupPosition="TopLeft">
        </cc1:CalendarExtender>
        <asp:RangeValidator ID="rvPartyBillAmount" runat="server" ControlToValidate="txtPartyBillAmount"
            ErrorMessage="Enter Correct Party Bill Amount" MaximumValue="9999999.99" MinimumValue="0"
            SetFocusOnError="True" Type="Double" ValidationGroup="M1" Display="None"></asp:RangeValidator>
        <asp:RangeValidator ID="rvVatTRNAmount" runat="server" ControlToValidate="txtVatTRNAmount"
            ErrorMessage="Enter Correct Vat TRN Amount" MaximumValue="999999999.99" MinimumValue="0"
            SetFocusOnError="True" Type="Double" ValidationGroup="M1" Display="None"></asp:RangeValidator>
        <asp:RangeValidator ID="rvVatAmount" runat="server" ControlToValidate="txtVatAmount"
            ErrorMessage="Enter Correct Vat Amount" MaximumValue="999999999.99" MinimumValue="0"
            SetFocusOnError="True" Type="Double" ValidationGroup="M1" Display="None"></asp:RangeValidator>
        <asp:RangeValidator ID="rvTRNQuality" runat="server" ControlToValidate="txtTRNQuality"
            ErrorMessage="Enter Correct Quality Point" MaximumValue="9999" MinimumValue="0"
            SetFocusOnError="True" Type="Integer" ValidationGroup="T1" Display="Dynamic"></asp:RangeValidator>
        <asp:RangeValidator ID="rvTRNDel" runat="server" ControlToValidate="txtTRNDel" ErrorMessage="Enter Correct Del Point"
            MaximumValue="9999" MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="T1"
            Display="Dynamic"></asp:RangeValidator>
        <asp:RangeValidator ID="rvTRNPrice" runat="server" ControlToValidate="txtTRNPrice"
            ErrorMessage="Enter Correct Price Point" MaximumValue="9999" MinimumValue="0"
            SetFocusOnError="True" Type="Integer" ValidationGroup="T1" Display="Dynamic"></asp:RangeValidator>
        <asp:RangeValidator ID="rvTRNSupport" runat="server" ControlToValidate="txtTRNSupport"
            ErrorMessage="Enter Correct Support Point" MaximumValue="9999" MinimumValue="0"
            SetFocusOnError="True" Type="Integer" ValidationGroup="T1" Display="Dynamic"></asp:RangeValidator>
    </ContentTemplate>
</asp:UpdatePanel>
