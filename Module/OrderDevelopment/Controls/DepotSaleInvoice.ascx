<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepotSaleInvoice.ascx.cs"
    Inherits="Module_OrderDevelopment_Controls_DepotSaleInvoice" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
      
    function Calculation(val)
    {                                                                
     document.getElementById('ctl00_cphBody_DepotSaleInvoice1_txtAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_DepotSaleInvoice1_txtFinalRate').value)*(parseFloat(document.getElementById('ctl00_cphBody_DepotSaleInvoice1_txtQTY').value))).toFixed(3) ;
   }
   
     
</script>

<style type="text/css">
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
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
        width: 120px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
    .c6
    {
        margin-left: 4px;
        width: 80px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<table class="tdMain" width="100%">
    <tr>
        <td width="100%" class="td">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="gg" Style="height: 41px">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png" Enabled="false"></asp:ImageButton>
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
            <b class="titleheading">Depot Sale Invoice Sewing Thread</b>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <span class="Mode">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
                <asp:Label ID="lblMode" runat="server"></asp:Label></span>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label15" runat="server" Text="Depot Invoice Number : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtTRNNUMBer" runat="server" ValidationGroup="M1" Width="80px" TabIndex="1"
                            CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="85px" Height="200px"
                            MenuWidth="500px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    MRN #</div>
                                <div class="header c2">
                                    MRN Date</div>
                                <div class="header c2">
                                    Party Code</div>
                                <div class="header c4">
                                    Party Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container6" Text='<%# Eval("TRN_DATE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal5" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label16" runat="server" Text="Date : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtMRNDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="80px"
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label17" runat="server" Text="Receipt Shift : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:DropDownList ID="ddlReceiptShift" runat="server" TabIndex="2">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label19" runat="server" Text="Party Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged" Width="98%" MenuWidth="450px"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c5">
                                    NAME</div>
                                <div class="header c4">
                                    Address</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container7" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container8" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Container9" Text='<%# Eval("Address") %>' /></div>
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
                        <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" Width="85%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label18" runat="server" Text="Transporter Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged" Width="98%"
                            Height="200px" MenuWidth="550px" EnableVirtualScrolling="true">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c5">
                                    NAME</div>
                                <div class="header c4">
                                    Address</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container10" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container11" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Container12" Text='<%# Eval("Address") %>' /></div>
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
                        <asp:TextBox ID="txtTransporterAddress" TabIndex="5" runat="server" Width="85%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label7" runat="server" Text="L.R. Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtLRNo" runat="server" TabIndex="14" Width="80px" CssClass="TextBoxNo SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label8" runat="server" Text="L.R. Date :" CssClass="Label SmallFont"></asp:Label>
                        <cc1:CalendarExtender ID="celr" runat="server" TargetControlID="txtLRDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                        <asp:RangeValidator ID="rvlr" runat="server" ControlToValidate="txtLRDate" Display="Dynamic"
                            ErrorMessage="Invalid LR Date" Type="Date" ValidationGroup="M1" MaximumValue="31/03/2012"
                            MinimumValue="01/04/2011" SetFocusOnError="True"></asp:RangeValidator>
                        <cc1:ValidatorCalloutExtender ID="vclr" TargetControlID="rvlr" runat="server">
                        </cc1:ValidatorCalloutExtender>
                    </td>
                    <td align="right" valign="top" width="15%">
                        Total Amount :
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="14" Width="80px" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" colspan="5">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="98%" TabIndex="21" CssClass="TextBox SmallFont"
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr bgcolor="#336699" class="SmallFont titleheading">
                    <td class="style1">
                        SO Numb
                    </td>
                    <td visible="false" class="style1">
                        Base UOM
                    </td>
                    <td visible="false" class="style1">
                        Unit Weight
                    </td>
                    <td class="style1">
                        Adjustment
                    </td>
                    <td class="style1">
                        No Of Unit
                    </td>
                    <td class="style1">
                        Qty
                    </td>
                    <td class="style1">
                        Final Rate
                    </td>
                    <td class="style1">
                        Amount
                    </td>
                    <td class="style1">
                        Remarks
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc2:ComboBox ID="lblPOITEM" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="680px" Width="80px"
                            OnLoadingItems="lblPOITEM_LoadingItems" OnSelectedIndexChanged="lblPOITEM_SelectedIndexChanged"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    SO No.</div>
                                <div class="header c2">
                                    Yarn Code</div>
                                <div class="header c3">
                                    Description</div>
                                <div class="header c2">
                                    Shade</div>
                                <div class="header c2">
                                    Quantity</div>
                                <div class="header c2">
                                    Qty Invoiced.</div>
                                <div class="header c6">
                                    Qty. Rem.</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("SO_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("YARN_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("YARN_DESC") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal6" Text='<%# Eval("SHADE_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("ORD_QTY") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("QTY_RCPT") %>' /></div>
                                <div class="item c6">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("QTY_REM") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td visible="false">
                        <asp:TextBox ID="txtBaseUOM" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                            Width="70px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td visible="false">
                        <asp:TextBox ID="txtUnitWeight" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            Width="70px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSubDetail" TabIndex="24" runat="server" Font-Size="8pt" Text="Adj. Rec."
                            Width="50px" OnClick="btnSubDetail_Click1" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfUnit" runat="server" CssClass="TextBoxNo SmallFont" Width="50px"
                            onkeyup="javascript:Calculation(this.id)" ReadOnly="True" OnTextChanged="txtNoOfUnit_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            Width="50px" onkeyup="javascript:Calculation(this.id)" ReadOnly="True" OnTextChanged="txtQTY_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Visible="false" onkeyup="javascript:Calculation(this.id)" Width="58px"></asp:TextBox>
                        <asp:TextBox ID="txtFinalRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px" onkeyup="javascript:Calculation(this.id)"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="50px" onkeyup="javascript:Calculation(this.id)" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="120px"
                            MaxLength="200"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="9">
                        SO Numb :<asp:TextBox ID="cmbPOITEM" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="80px" ReadOnly="true"></asp:TextBox>
                        Article Code/Desc :<asp:TextBox ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="70px" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="120px"></asp:TextBox>
                        &nbsp;Shade :<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="70px" ReadOnly="true"></asp:TextBox>
                        &nbsp;&nbsp;Rem Qty :<asp:TextBox ID="lblMaxQTY" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            Width="70px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                            Text="Cancel" Width="60px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                <asp:GridView ID="grdMaterialItemReceipt" Width="99%" runat="server" AutoGenerateColumns="False"
                    CssClass="SmallFont" ShowFooter="false" OnRowCommand="grdMaterialItemReceipt_RowCommand"
                    OnRowDataBound="grdMaterialItemReceipt_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="SO #">
                            <ItemTemplate>
                                <asp:Label ID="txtPONum" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Article Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade Code">
                            <ItemTemplate>
                                <asp:Label ID="txtShadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Base UOM">
                            <ItemTemplate>
                                <asp:Label ID="txtBaseUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Weight">
                            <ItemTemplate>
                                <asp:Label ID="txtUnitWeight" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtnoofUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="F. Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtFinalRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("FINAL_RATE") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("AMOUNT") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REMARKS") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>/
                                <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
                <asp:Label ID="lblSO_BRANCH" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSO_TYPE" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSO_COMP" runat="server" Visible="false"></asp:Label>
            </asp:Panel>
        </td>
    </tr>
</table>
<cc1:ValidatorCalloutExtender ID="vcMRNNo" TargetControlID="rfv1" runat="server">
</cc1:ValidatorCalloutExtender>
<cc1:ValidatorCalloutExtender ID="vcrv1" TargetControlID="rv1" runat="server">
</cc1:ValidatorCalloutExtender>
<asp:RangeValidator ID="rv1" ControlToValidate="txtTRNNUMBer" runat="server" ErrorMessage="Only numeric value allowed"
    Display="Dynamic" MaximumValue="1000000" MinimumValue="1" Type="Integer" ValidationGroup="M1"
    SetFocusOnError="True"></asp:RangeValidator><asp:RequiredFieldValidator ControlToValidate="txtTRNNUMBer"
        ID="rfv1" runat="server" ValidationGroup="M1" ErrorMessage="MRN number required"
        Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
       <%-- </ContentTemplate>
        </asp:UpdatePanel>
--%>