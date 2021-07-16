<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReceiptHeading.ascx.cs"
    Inherits="Inventory_Controls_ReceiptHeading" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
      
    function Calculation(val)
    {                                                                
     document.getElementById('ctl00_cphBody_ReceiptHeading1_txtAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_ReceiptHeading1_txtFinalRate').value)*(parseFloat(document.getElementById('ctl00_cphBody_ReceiptHeading1_txtQTY').value))).toFixed(3) ;
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
        width: 250px;
    }
    .style2
    {
        text-align: right;
        width: 120px;
        vertical-align: top;
    }
    .style3
    {
        width: 150px;
        text-align: right;
        vertical-align: top;
    }
    .style5
    {
        vertical-align: top;
        width: 150px;
    }
    .style6
    {
        text-align: right;
        vertical-align: top;
        width: 100px;
    }
    .style7
    {
        vertical-align: top;
        width: 50px;
    }
    .HeaderRow
    {
        font-size: 8pt;
        font-weight: bold;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="tdMain tContentArial" width="800px">
            <tr>
                <td width="100%" class="td">
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
                    <b class="titleheading">M.R.N. Against PO Credit</b>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="td" width="100%">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td SmallFont">
                    <table width="100%">
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label15" runat="server" Text="M.R.N. Number : " CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtTRNNUMBer" runat="server" ValidationGroup="M1" Width="85px" TabIndex="1"
                                    CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                                <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                                    EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                                    Width="85px" Height="200px" MenuWidth="500px">
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
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label16" runat="server" Text="M.R.N. Date : " CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtMRNDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="80px"
                                    CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label17" runat="server" Text="Receipt Shift : " CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlReceiptShift" runat="server" TabIndex="2">
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
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label10" runat="server" Text="Gate Entry No. :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <cc2:ComboBox ID="ddlGateEntryNo" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlGateEntryNo_LoadingItems" DataTextField="GATE_NUMB" DataValueField="GATE_NUMB"
                                    EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlGateEntryNo_SelectedIndexChanged"
                                    Width="65px" Height="200px" MenuWidth="950px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Gate #</div>
                                        <div class="header c2">
                                            Gate Date</div>
                                        <div class="header c3">
                                            Gate Type</div>
                                        <div class="header C3">
                                            Party Code</div>
                                        <div class="header c4">
                                            Party Name</div>
                                        <div class="header c3">
                                            Transporter Code</div>
                                        <div class="header c4">
                                            Transporter Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("GATE_NUMB") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("GATE_DATE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("GATE_TYPE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Literal8" Text='<%# Eval("TRSP_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Literal10" Text='<%# Eval("TRSP_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:TextBox ID="txtGateEntryNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="50px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label11" runat="server" Text="Gate Entry Date :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtGateEntryDate" runat="server" TabIndex="18" Width="80px" ReadOnly="true"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="80px" ReadOnly="true"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label4" runat="server" Text="Party Challan Number :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="17%" class="tdLeft">
                                <asp:TextBox ID="txtPartyChallanNo" runat="server" TabIndex="9" Width="80px" ReadOnly="true"
                                    CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label5" runat="server" Text="Party Challan Date :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="17%" class="tdLeft">
                                <asp:TextBox ID="txtPartyChallanDate" runat="server" TabIndex="10" Width="80px" ReadOnly="true"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label6" runat="server" Text="Department :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="15%" class="tdLeft">
                                <asp:TextBox ID="txtDepartment" runat="server" TabIndex="11" Width="80px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <%--    </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">--%>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label19" runat="server" Text="Party Code :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="lblPartyCode" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label20" runat="server" Text="Party Details :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="3" style="width: 32%">
                                <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" Width="100%" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label18" runat="server" Text="Transporter Code :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="lblTransporterCode" TabIndex="5" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label21" runat="server" Text="Party Details :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" colspan="3" style="width: 32%">
                                <asp:TextBox ID="txtTransporterAddress" TabIndex="5" runat="server" Width="100%"
                                    CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label1" runat="server" Text="Party Bill Number :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td width="17%" class="tdLeft">
                                <asp:TextBox ID="txtPartyBillNo" runat="server" TabIndex="6" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label2" runat="server" Text="Party Bill Date :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="17%" class="tdLeft">
                                <asp:TextBox ID="txtPartyBillDate" runat="server" TabIndex="7" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label3" runat="server" Text="Party Bill Amount :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="15%" class="tdLeft">
                                <asp:TextBox ID="txtPartyBillAmount" runat="server" TabIndex="8" Width="80px" CssClass="TextBoxNo SmallFont"
                                    AutoPostBack="True" OnTextChanged="txtPartyBillAmount_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label12" runat="server" Text="Form Type :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="17%" class="tdLeft">
                                <asp:TextBox ID="txtFormType" runat="server" TabIndex="19" Width="80px" CssClass="TextBox SmallFont"
                                    MaxLength="15"></asp:TextBox>
                            </td>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label13" runat="server" Text="Form Ref No. :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="17%" class="tdLeft">
                                <asp:TextBox ID="txtFormRefNo" runat="server" TabIndex="20" Width="80px" CssClass="TextBox SmallFont"
                                    MaxLength="15"></asp:TextBox>
                            </td>
                            <td width="32%" colspan="2" class="tdLeft">
                                <span style="font-size: 8pt; height: 9pt;" id="spnAInW" runat="server">Amount in words...</span>
                            </td>
                        </tr>
                        <tr>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label7" runat="server" Text="L.R. Number :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td width="17%" class="tdLeft">
                                <asp:TextBox ID="txtLRNo" runat="server" TabIndex="14" Width="80px" CssClass="TextBoxNo SmallFont"
                                    MaxLength="15"></asp:TextBox>
                            </td>
                            <td width="17%" class="tdRight">
                                <asp:Label ID="Label8" runat="server" Text="L.R. Date :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="17%" class="tdLeft">
                                <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td width="32%" colspan="2" class="tdLeft">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="83%">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="100%" TabIndex="21" CssClass="TextBox SmallFont"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%" class="SmallFont">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            <td class="style3 HeaderRow">
                                PO Numb
                            </td>
                            <td class="style2 HeaderRow">
                                Date Of Mfg. / Qty.
                            </td>
                            <td class="style6 HeaderRow">
                                Rate
                            </td>
                            <td class="style5 HeaderRow">
                                Amount and Other Details
                            </td>
                            <td class="style7 HeaderRow">
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                <cc2:ComboBox ID="cmbPOITEM" runat="server" CssClass="SmallFont" EmptyText="select..."
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="780px" Width="80px"
                                    EnableVirtualScrolling="true" OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                                    Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Year.</div>
                                        <div class="header c1">
                                            PO No.</div>
                                        <div class="header c2">
                                            Item Code</div>
                                        <div class="header c6">
                                            Description</div>
                                        <div class="header c1">
                                            Quantity</div>
                                        <div class="header c1">
                                            Qty Rec.</div>
                                        <div class="header c1">
                                            Qty. Rem.</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal6" Text='<%# Eval("YEAR") %>' /></div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PO_NUMB") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ITEM_CODE") %>' /></div>
                                        <div class="item c6">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ITEM_DESC") %>' /></div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("ORD_QTY") %>' /></div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("QTY_RCPT") %>' /></div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("QTY_REM") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:TextBox ID="txtPONumb" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="75px" ReadOnly="true"></asp:TextBox>
                                <br />
                                Item Code :<asp:TextBox ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="110px" ReadOnly="true"></asp:TextBox>
                                <br />
                                UOM :
                                <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px"></asp:TextBox>&nbsp;
                            </td>
                            <td class="style2">
                                Date Of Mfg. :
                                <asp:TextBox ID="txtDOM" runat="server" CssClass="TextBox SmallFont" Width="60px"></asp:TextBox>
                                <br />
                                Quantity :
                                <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo SmallFont" Width="80px"
                                    onkeyup="javascript:Calculation(this.id)"></asp:TextBox>
                                <br />
                                Max Qty :<asp:TextBox ID="lblMaxQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="80px" ReadOnly="true"></asp:TextBox>&nbsp;
                            </td>
                            <td class="style6">
                                Basic :<asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="70px"></asp:TextBox>
                                <br />
                                Final :<asp:TextBox ID="txtFinalRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="70px"></asp:TextBox>
                                <br />
                                Amount:<asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="70px"></asp:TextBox>&nbsp;
                            </td>
                            <td class="style5">
                                Cost Code:<asp:TextBox ID="txtCostCode" runat="server" CssClass="TextBox SmallFont"
                                    Width="65px" MaxLength="15"></asp:TextBox>Q.C.:<asp:CheckBox ID="chk_QCFlag" runat="server"
                                        CssClass="TextBox SmallFont" />
                                <br />
                                Remarks :<asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont"
                                    Width="120px" MaxLength="200" Height="35px"></asp:TextBox>
                            </td>
                            <td rowspan="2" class="style7">
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save" />
                                <br />
                                <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                                    Text="Cancel" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="97%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                        <asp:GridView ID="grdMaterialItemReceipt" Width="99%" runat="server" AutoGenerateColumns="False"
                            CssClass="SmallFont" ShowFooter="false" OnRowCommand="grdMaterialItemReceipt_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="PO #">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPONum" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                            Width="40px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_CODE") %>'
                                            Width="70px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'
                                            Width="120px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Mfg.">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DATE_OF_MFG", "{0:dd-MM-yyyy}") %>'
                                            Width="70px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                        <asp:Label ID="txtTRN_QTY_1" runat="server" CssClass="LabelNo SmallFont" Visible="false"
                                            Text='<%# Bind("TRN_QTY_1") %>' Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="B. Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtBasicRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BASIC_RATE") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFinalRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("FINAL_RATE") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("AMOUNT") %>'
                                            Width="70px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtCostCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("COST_CODE") %>'
                                            Width="70px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REMARKS") %>'
                                            Width="120px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Q.C.">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("QCFLAG") %>'
                                            Width="40px" ReadOnly="true"></asp:Label>
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
                        <asp:Label ID="lblPO_BRANCH" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblPO_TYPE" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblPO_COMP" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblPO_YEAR" runat="server" Visible="false"></asp:Label>
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
        </cc1:ValidatorCalloutExtender>
        <%--<cc1:CalendarExtender ID="ce" runat="server" TargetControlID="txtMRNDate">
</cc1:CalendarExtender><asp:RangeValidator ID="rvDate" runat="server" ControlToValidate="txtMRNDate" Display="Dynamic"
    ErrorMessage="Invalid Date" Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
    <cc1:ValidatorCalloutExtender ID="vcDate" TargetControlID="rvDate" runat="server">--%>
        <cc1:CalendarExtender ID="cebill" Format="dd/MM/yyyy" runat="server" TargetControlID="txtPartyBillDate">
        </cc1:CalendarExtender>
        <asp:RangeValidator ID="rvbill" runat="server" ControlToValidate="txtPartyBillDate"
            Display="Dynamic" ErrorMessage="Invalid Party Bill Date" Type="Date" ValidationGroup="M1"
            SetFocusOnError="True"></asp:RangeValidator><cc1:ValidatorCalloutExtender ID="vcbill"
                TargetControlID="rvbill" runat="server">
            </cc1:ValidatorCalloutExtender>
        <%--<cc1:CalendarExtender ID="cechalan" runat="server" TargetControlID="txtPartyChallanDate">
</cc1:CalendarExtender>--%>
        <%--<asp:RangeValidator ID="rvchalan" runat="server" ControlToValidate="txtPartyChallanDate"
    Display="Dynamic" ErrorMessage="Invalid Date" Type="Date" ValidationGroup="M1"
    SetFocusOnError="True"></asp:RangeValidator><cc1:ValidatorCalloutExtender ID="vcchalan" TargetControlID="rvchalan" runat="server">
</cc1:ValidatorCalloutExtender>--%>
        <cc1:CalendarExtender ID="celr" Format="dd/MM/yyyy" runat="server" TargetControlID="txtLRDate">
        </cc1:CalendarExtender>
        <asp:RangeValidator ID="rvlr" runat="server" ControlToValidate="txtLRDate" Display="Dynamic"
            ErrorMessage="Invalid LR Date" Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
        <cc1:ValidatorCalloutExtender ID="vclr" TargetControlID="rvlr" runat="server">
        </cc1:ValidatorCalloutExtender>
        <%--<cc1:CalendarExtender ID="cegateentry" runat="server" TargetControlID="txtGateEntryDate">
</cc1:CalendarExtender>--%>
        <%--<asp:RangeValidator ID="rvgate" runat="server" ControlToValidate="txtGateEntryDate"
    Display="Dynamic" ErrorMessage="Invalid Date" Type="Date" ValidationGroup="M1"
    SetFocusOnError="True"></asp:RangeValidator>&nbsp;<cc1:ValidatorCalloutExtender ID="vcgate"
        TargetControlID="rvgate" runat="server">
</cc1:ValidatorCalloutExtender>--%>
        <cc1:CalendarExtender ID="ceDOM" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDOM">
        </cc1:CalendarExtender>
    </ContentTemplate>
</asp:UpdatePanel>
