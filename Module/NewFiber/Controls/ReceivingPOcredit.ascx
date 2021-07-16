<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReceivingPOcredit.ascx.cs" Inherits="Module_Fiber_Controls_ReceivingPOcredit" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">

    function Calculation(val) {

        document.getElementById('ctl00_cphBody_ReceiptPOCredit1_txtAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_ReceiptPOCredit1_txtFinalRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_ReceiptPOCredit1_txtQTY').value))).toFixed(4);

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
        width: 300px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
    .c5
    {
        width: 120px;
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
            <b class="titleheading">Fiber Receiving Against PO Credit</b>
        </td>
</tr>
<tr>
        <td valign="top" align="left" class="td" width="100%">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
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
                        <asp:TextBox ID="txtTRNNUMBer" runat="server" ValidationGroup="M1" Width="80px" TabIndex="1"
                            CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                       <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="85px" Height="200px"
                            MenuWidth="600px">
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
                                    <asp:Literal runat="server" ID="Container6" Text='<%# Eval("TRN_DATE","{0:dd/MM/yyyy}") %>' /></div>
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
                        <asp:DropDownList ID="ddlReceiptShift" CssClass="SmallFont" runat="server" TabIndex="2">
                        </asp:DropDownList>
                    </td>
                </tr>
                  <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label10" runat="server" Text="Gate Entry No. :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                     <td align="left" valign="top" width="17%">
                       <cc2:ComboBox ID="ddlGateEntryNo" runat="server" AutoPostBack="True" DataTextField="GATE_NUMB"
                            DataValueField="GATE_NUMB" EnableLoadOnDemand="true" Height="200px" MenuWidth="800"
                            OnLoadingItems="ddlGateEntryNo_LoadingItems" OnSelectedIndexChanged="ddlGateEntryNo_SelectedIndexChanged"
                            Width="85px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Gate No</div>
                                <div class="header c2">
                                    Gate Date</div>
                                <div class="header c1">
                                    Gate Type</div>
                                <div class="header c2">
                                    Party Code</div>
                                <div class="header c3">
                                    Party Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("GATE_NUMB") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container8" runat="server" Text='<%# Eval("GATE_DATE") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Container9" runat="server" Text='<%# Eval("GATE_TYPE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                            </cc2:ComboBox>
                        <asp:TextBox ID="txtGateEntryNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
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
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label4" runat="server" Text="Party Challan Number :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPartyChallanNo" runat="server" TabIndex="9" Width="80px" ReadOnly="true"
                            CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label5" runat="server" Text="Party Challan Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPartyChallanDate" runat="server" TabIndex="10" Width="80px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label6" runat="server" Text="Department :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtDepartment" runat="server" TabIndex="11" Width="80px" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label19" runat="server" Text="Party Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="lblPartyCode" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Party Details :
                    </td>
                    <td align="left" valign="top" colspan="3" style="width: 32%">
                        <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" Width="75%" CssClass="TextBox TextBoxDisplay SmallFont"
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
                        Transporter Details :
                    </td>
                    <td align="left" valign="top" colspan="3" style="width: 32%">
                        <asp:TextBox ID="txtTransporterAddress" TabIndex="5" runat="server" Width="75%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label1" runat="server" Text="Party Bill Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPartyBillNo" runat="server" TabIndex="6" Width="80px" CssClass="TextBox SmallFont"
                            OnTextChanged="txtPartyBillNo_TextChanged" AutoPostBack="True" MaxLength="14"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label2" runat="server" Text="Party Bill Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPartyBillDate" runat="server" TabIndex="7" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                    
                          <cc1:MaskedEditExtender ID="MaskedEdittxtPartyBillDate" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtPartyBillDate">
                        </cc1:MaskedEditExtender>
                    
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label3" runat="server" Text="Party Bill Amount :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtTotalPartyAmt" runat="server" TabIndex="8" Width="80px" CssClass="TextBoxNo SmallFont"
                            AutoPostBack="True" OnTextChanged="txtPartyBillAmount_TextChanged" MaxLength="9"></asp:TextBox>
                            
                           <cc1:FilteredTextBoxExtender ID="FiltertxtTotalPartyAmt" runat="server"
                                                       TargetControlID="txtTotalPartyAmt"         
                                                       FilterType="Custom, Numbers"
                                                            />
                    </td>
                </tr>
                 <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label12" runat="server" Text="Form Type :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtFormType" runat="server" TabIndex="19" Width="80px" CssClass="TextBox SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label13" runat="server" Text="Form Ref No. :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtFormRefNo" runat="server" TabIndex="20" Width="80px" CssClass="TextBox SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Total Amount:
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="8" Width="99px" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            ReadOnly="true"  MaxLength="9"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label7" runat="server" Text="L.R. Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtLRNo" runat="server" TabIndex="14" Width="80px" CssClass="TextBoxNo SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label8" runat="server" Text="L.R. Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="80px" 
                            CssClass="TextBox SmallFont"></asp:TextBox>
                            
                            <cc1:MaskedEditExtender ID="MaskedEdittxtLRDate" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtLRDate">
                        </cc1:MaskedEditExtender>
                            
                    </td>
                    <td align="right" valign="top" width="17%">
                        Other Charges:
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:Button ID="btnOtherCharges" class="SmallFont" runat="server" Text="Other Charges"
                            OnClick="btnOtherCharges_Click" Width="105px" />
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" colspan="4">
                        <span style="font-size: 8pt; height: 9pt;" id="spnAInW" runat="server">Amount in words...</span>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Final Amount:
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtPartyBillAmount" runat="server" TabIndex="8" Width="99px" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            AutoPostBack="True" OnTextChanged="txtPartyBillAmount_TextChanged" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
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
                        PO Numb
                    </td>
                    <td visible="false" class="style1">
                        Remaining Qty
                    </td>
                    <td class="style1">
                        Qty
                    </td>
                    <td class="style1">
                        Total Bale
                    </td>
                    <td class="style1">
                        Weight Of Unit
                    </td>
                    <td class="style1">
                        Basic Rate
                    </td>
                    <td class="style1">
                        Po Rate
                    </td>
                    <td class="style1">
                        Dis/Tax
                    </td>
                    <td class="style1">
                        Final Rate
                    </td>
                    <td class="style1">
                        Amount
                    </td>
                    <td class="style1">
                        Cost Code
                    </td>
                    <td class="style1">
                        Remarks
                    </td>
                    <td class="style1">
                        Q.C.
                    </td>
                    <td>
                    </td>
                </tr>
                    <tr>
                    <td>
                        <cc2:ComboBox ID="cmbPOITEM" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="880px" Width="80px"
                            OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                            Height="200px" OnPreRender="cmbPOITEM_PreRender">
                            <HeaderTemplate>
                                <div class="header c1">
                                    PO No.</div>
                                <div class="header c2">
                                    Fiber Code</div>
                                <div class="header c3">
                                    Description</div>
                                <div class="header c2">
                                    Quantity</div>
                                <div class="header c2">
                                    Qty Rec.</div>
                                <div class="header c2">
                                    Qty. Rem.</div>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PO_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("FIBER_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("FIBER_DESC") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("ORD_QTY") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("QTY_RCPT") %>' /></div>
                                <div class="item c2">
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
                        <asp:TextBox ID="txtDOM" runat="server" CssClass="TextBox SmallFont" Visible="false"
                            Width="60px"></asp:TextBox>
                        <asp:TextBox ID="lblMaxQTY" runat="server" ReadOnly="true" CssClass="TextBoxDisplay TextBox SmallFont"
                            Width="60px" OnTextChanged="lblMaxQTY_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="60px" ReadOnly="true" OnTextChanged="txtQTY_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeightOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPoRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnDis" Width="50" CssClass="SmallFont" runat="server" Text="Dis/Tax"
                            OnClick="btnDis_Click" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtFinalRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px" AutoPostBack="true" 
                            ontextchanged="txtFinalRate_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCostCode" runat="server" CssClass="TextBox SmallFont" Width="50px"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="120px"
                            MaxLength="200"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="chk_QCFlag" runat="server" CssClass="TextBox SmallFont" />
                    </td>
                    <td>
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save"  Width="60px"/>
                    </td>
                    </tr>
                    <tr>
                    <td colspan="13" cssclass="SmallFont">
                        <asp:Button ID="btnSubDetail" TabIndex="24" runat="server" Font-Size="8pt" Text="Sub-Detail"
                            Width="60px" OnClick="btnSubDetail_Click1" />
                        Po Numb :
                        <asp:TextBox ID="txtPONumb" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                        Fiber Code :
                        <asp:TextBox ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="70px" ReadOnly="true"></asp:TextBox>
                        &nbsp;Fiber Description :
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="245px"></asp:TextBox>
                        &nbsp;Uom1 :
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                        &nbsp;UOM2:<asp:TextBox ID="txtUOm" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                             &nbsp;kg/Bail :
                        <asp:TextBox ID="txtkg_bail" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
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
                <asp:Label ID="lblPO_BRANCH" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblPO_TYPE" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblPO_COMP" runat="server" Visible="false"></asp:Label>
            </asp:Panel>
             <asp:GridView ID="grdMaterialItemReceipt" Width="99%" runat="server" AutoGenerateColumns="False"
                    CssClass="SmallFont" ShowFooter="false" OnRowCommand="grdMaterialItemReceipt_RowCommand"
                    OnRowDataBound="grdMaterialItemReceipt_RowDataBound" 
                    onselectedindexchanged="grdMaterialItemReceipt_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="PO #">
                            <ItemTemplate>
                                <asp:Label ID="txtPONum" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                    Width="40px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fiber Code">
                            <ItemTemplate>
                                <asp:LinkButton ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("FIBER_CODE") %>'
                                    Width="70px" ReadOnly="true">
                                </asp:LinkButton>
                                <cc1:HoverMenuExtender ID="hvrYarn" runat="server" TargetControlID="txtICODE" PopupControlID="pnlyarndtl"
                                    PopupPosition="Bottom" PopDelay="500">
                                </cc1:HoverMenuExtender>
                                <asp:Panel ID="pnlyarndtl" runat="server" BackColor="Red">
                                    <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("FIBER_DESC") %>'
                                        ReadOnly="true"></asp:Label></asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date of Mfg." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtDOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DATE_OF_MFG", "{0:dd-MMM-yyyy}") %>'
                                    Width="70px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Bale">
                            <ItemTemplate>
                                <asp:Label ID="txtNoOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UomOfUnit">
                            <ItemTemplate>
                                <asp:Label ID="txtUomOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'
                                    Width="50px" ReadOnly="true">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Uom2">
                            <ItemTemplate>
                                <asp:Label ID="txtUom2" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM1") %>'
                                    Width="50px" ReadOnly="true">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Kg/bail">
                            <ItemTemplate>
                                <asp:Label ID="txtkgbail" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_BAIL") %>'
                                    Width="50px" ReadOnly="true">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WeightOfUnit">
                            <ItemTemplate>
                                <asp:Label ID="txtWeightOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
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
                                <asp:Label ID="txtCostCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("COST_CENTER_CODE") %>'
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
                        <asp:TemplateField HeaderText="SubTran">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkunige" runat="server" CssClass="Label SmallFont" Text="View Sub Tran"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'>
                                </asp:LinkButton>
                                <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid"
                                    BorderWidth="5px" HorizontalAlign="Left">
                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                        <Columns> <asp:TemplateField HeaderText="Lot No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdBASIS" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdUOM" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("GRADE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                       
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No Of Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE14" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE15" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Weight Of Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE20" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               
                                            
                                             <asp:TemplateField HeaderText="Material Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdARTICLE_CODE" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of Manufacturing" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdVALUE_QTY" runat="server" CssClass="SmallFont LabelNo" 
                                                        Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                </asp:Panel>
                                <cc1:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnlBOM" TargetControlID="lnkunige"
                                    PopupPosition="Left">
                                </cc1:HoverMenuExtender>
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
        </td>
   
                    </tr>
                    </table>
                   
                    <cc1:ValidatorCalloutExtender ID="vcMRNNo" TargetControlID="rfv1" runat="server">
</cc1:ValidatorCalloutExtender>
<cc1:ValidatorCalloutExtender ID="vcrv1" TargetControlID="rv1" runat="server">
</cc1:ValidatorCalloutExtender>
<asp:RangeValidator ID="rv1" ControlToValidate="txtTRNNUMBer" runat="server" ErrorMessage="Only numeric value allowed"
    Display="Dynamic" MaximumValue="1000000" MinimumValue="1" Type="Integer" ValidationGroup="M1"
    SetFocusOnError="True"></asp:RangeValidator>
 <asp:RequiredFieldValidator ControlToValidate="txtTRNNUMBer"
        ID="rfv1" runat="server" ValidationGroup="M1" ErrorMessage="MRN number required"
        Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>

<cc1:CalendarExtender ID="cebill" runat="server" TargetControlID="txtPartyBillDate"
    Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<asp:RangeValidator ID="rvbill" runat="server" ControlToValidate="txtPartyBillDate"
    Display="Dynamic" ErrorMessage="Invalid Party Bill Date" Type="Date" ValidationGroup="M1"
    SetFocusOnError="True"></asp:RangeValidator>
 <cc1:ValidatorCalloutExtender ID="vcbill"
        TargetControlID="rvbill" runat="server">
    </cc1:ValidatorCalloutExtender>
<cc1:CalendarExtender ID="celr" runat="server" TargetControlID="txtLRDate" Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<asp:RangeValidator ID="rvlr" runat="server" ControlToValidate="txtLRDate" Display="Dynamic"
    ErrorMessage="Invalid LR Date" Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
<cc1:ValidatorCalloutExtender ID="vclr" TargetControlID="rvlr" runat="server">
</cc1:ValidatorCalloutExtender>
<cc1:CalendarExtender ID="ceDOM" runat="server" TargetControlID="txtDOM" Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>