<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FabricReceiptAgainstRetGatePass.ascx.cs" Inherits="Module_Inventory_Controls_FabricReceiptAgainstRetGatePass" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
      
    function Calculation(val)
    {                                                                
     document.getElementById('ctl00_cphBody_ReceiptPOCash1_txtAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_ReceiptPOCash1_txtFinalRate').value)*(parseFloat(document.getElementById('ctl00_cphBody_ReceiptPOCash1_txtQTY').value))).toFixed(3) ;
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
            <b class="titleheading">M.R.N.(Fabric)Against Returnable Gate Pass</b></td>
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
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label15" runat="server" Text="M.R.N. Number : " CssClass="LabelNo SmallFont"></asp:Label>
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
                        <asp:Label ID="Label16" runat="server" Text="M.R.N. Date : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtMRNDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="80px"
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label17" runat="server" Text="Receipt Shift : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="25%">
                        <cc3:OboutDropDownList ID="ddlReceiptShift" runat="server" TabIndex="2">
                        </cc3:OboutDropDownList>
                        <%--  <asp:DropDownList ID="ddlReceiptShift" runat="server" TabIndex="2" CssClass="TextBox SmallFont">
                            <asp:ListItem>Shift A</asp:ListItem>
                            <asp:ListItem>Shift B</asp:ListItem>
                            <asp:ListItem>Shift C</asp:ListItem>
                            <asp:ListItem>Shift D</asp:ListItem>
                            <asp:ListItem>Shift E</asp:ListItem>
                        </asp:DropDownList>--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label19" runat="server" Text="Party Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
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
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                <div class="item c4">
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
                    <td valign="top" align="left" width="70%">
                        <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" Width="98%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label18" runat="server" Text="Transporter Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged" Width="98%"
                            Height="200px" MenuWidth="450px">
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
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                <div class="item c4">
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
                    <td valign="top" align="left" width="70%">
                        <asp:TextBox ID="txtTransporterAddress" TabIndex="5" runat="server" Width="98%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label1" runat="server" Text="Party Bill Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtPartyBillNo" runat="server" TabIndex="6" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label2" runat="server" Text="Party Bill Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtPartyBillDate" runat="server" TabIndex="7" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label3" runat="server" Text="Party Bill Amount :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="25%" class="tdLeft">
                        <asp:TextBox ID="txtPartyBillAmount" runat="server" TabIndex="8" Width="80px" CssClass="TextBoxNo SmallFont"
                            AutoPostBack="True" OnTextChanged="txtPartyBillAmount_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label12" runat="server" Text="Form Type :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtFormType" runat="server" TabIndex="19" Width="80px" CssClass="TextBox SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label13" runat="server" Text="Form Ref No. :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtFormRefNo" runat="server" TabIndex="20" Width="80px" CssClass="TextBox SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td width="40%" colspan="2" class="tdLeft">
                        <span style="font-size: 8pt; height: 9pt;" id="spnAInW" runat="server">Amount in words...</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label10" runat="server" Text="Gate Entry No. :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <cc2:ComboBox ID="ddlGateEntryNo" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlGateEntryNo_LoadingItems" DataTextField="GATE_NUMB" DataValueField="GATE_NUMB"
                            OnSelectedIndexChanged="ddlGateEntryNo_SelectedIndexChanged" Width="85px" Height="200px"
                            MenuWidth="350px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Gate #</div>
                                <div class="header c2">
                                    Gate Date</div>
                                <div class="header c3">
                                    Gate Type</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("GATE_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("GATE_DATE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("GATE_TYPE") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <%--        <asp:TextBox ID="txtGateEntryNo" runat="server" TabIndex="17" Width="80px" CssClass="TextBoxNo SmallFont"></asp:TextBox> --%>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label11" runat="server" Text="Gate Entry Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtGateEntryDate" runat="server" TabIndex="18" Width="80px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="80px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label4" runat="server" Text="Party Challan Number :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtPartyChallanNo" runat="server" TabIndex="9" Width="80px" ReadOnly="true"
                            CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label5" runat="server" Text="Party Challan Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtPartyChallanDate" runat="server" TabIndex="10" Width="80px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td width="15%" class="tdRight">
                        <asp:Label ID="Label6" runat="server" Text="Department :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="25%" class="tdLeft">
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
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label7" runat="server" Text="L.R. Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtLRNo" runat="server" TabIndex="14" Width="80px" CssClass="TextBoxNo SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label8" runat="server" Text="L.R. Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        Total Amount</td>
                    <td align="left" valign="top" width="25%">
                        &nbsp;
                                <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="8" Width="99px" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                    ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="85%">
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
                        TRN Numb
                    </td>
                    <td class="style1">
                       NO Of Unit
                    </td>
                    <td class="style1">
                        Weight of Unit
                    </td>
                    <td class="style1">
                        UOM
                    </td>
                    <td class="style1">
                        Date Of Mfg.
                    </td>
                    <td class="style1">
                        Qty
                    </td>
                    <td class="style1">
                        Basic Rate
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
                    <%--<td class="style1">
                        Q.C.
                    </td>--%>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc2:ComboBox ID="cmbPOITEM" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="80px"
                            OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    TRN No.</div>
                                <div class="header c2">
                                    No Of Unit</div>
                                <div class="header c3">
                                    Weight of Unit</div>
                                <div class="header c2">
                                    Quantity</div>
                                <div class="header c2">
                                    Qty Rec.</div>
                                <div class="header c6">
                                    Qty. Rem.</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("TRN_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("FABR_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("FABR_DESC") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("TRN_QTY") %>' /></div>
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
                    <td>
                        <asp:TextBox ID="txtNoOfUnit" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="70px" ReadOnly="true" ontextchanged="txtNoOfUnit_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeightofUnit" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="120px" ontextchanged="txtWeightofUnit_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="25px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDOM" runat="server" CssClass="TextBox SmallFont" Width="60px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo SmallFont" Width="50px"
                            onkeyup="javascript:Calculation(this.id)"></asp:TextBox>
                        <asp:Label ID="lblMaxQTY" runat="server" Visible="false" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFinalRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCostCode" Width="100%" runat="server" 
                            AppendDataBoundItems="True" CssClass="tContentArial">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="120px"
                            MaxLength="200"></asp:TextBox>
                    </td>
                   <%-- <td>
                        <asp:CheckBox ID="chk_QCFlag" runat="server" CssClass="TextBox SmallFont" />
                    </td>--%>
                    <td>
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" /><asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                                Text="Cancel" />
                    </td>
                </tr>
                <tr>
              
                            <td colspan="12">
                            <asp:Button ID="btnSubDetail" runat="server"  Text="Sub Details"
                                    Width="80px" onclick="btnSubDetail_Click" />
                                    
                                    
                                    PO NUMB:
                                    <asp:TextBox ID="txtPONumb" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="60px" ReadOnly="true"></asp:TextBox>
                                &nbsp;Fabric Code :
                                <asp:TextBox ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="110px" ReadOnly="true"></asp:TextBox>
                                &nbsp;Fabric Desc :
                                <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="221px"></asp:TextBox>
                                &nbsp; Shade Code:
                          
                            
                      <asp:DropDownList ID="cmbShade" runat="server" AutoPostBack="True" 
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                          
                            
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
                                <asp:Label ID="txtPONum" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_NUMB") %>'
                                    Width="40px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("FABR_CODE") %>'
                                    Width="70px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("FABR_DESC") %>'
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
    SetFocusOnError="True"></asp:RangeValidator>
    <asp:RequiredFieldValidator ControlToValidate="txtTRNNUMBer"
        ID="rfv1" runat="server" ValidationGroup="M1" ErrorMessage="MRN number required"
        Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
<%--</cc1:ValidatorCalloutExtender>--%>
<%--<cc1:CalendarExtender ID="ce" runat="server" TargetControlID="txtMRNDate">
</cc1:CalendarExtender><asp:RangeValidator ID="rvDate" runat="server" ControlToValidate="txtMRNDate" Display="Dynamic"
    ErrorMessage="Invalid Date" Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
    <cc1:ValidatorCalloutExtender ID="vcDate" TargetControlID="rvDate" runat="server">--%>
<cc1:CalendarExtender ID="cebill" runat="server" TargetControlID="txtPartyBillDate" Format="dd/MM/yyyy">
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
<cc1:CalendarExtender ID="celr" runat="server" TargetControlID="txtLRDate" Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<asp:RangeValidator ID="rvlr" runat="server" ControlToValidate="txtLRDate" Display="Dynamic"
    ErrorMessage="Invalid LR Date" Type="Date" ValidationGroup="M1"   SetFocusOnError="True"></asp:RangeValidator>
<cc1:ValidatorCalloutExtender ID="vclr" TargetControlID="rvlr" runat="server">
</cc1:ValidatorCalloutExtender>
<%--<cc1:CalendarExtender ID="cegateentry" runat="server" TargetControlID="txtGateEntryDate">
</cc1:CalendarExtender>--%>
<%--<asp:RangeValidator ID="rvgate" runat="server" ControlToValidate="txtGateEntryDate"
    Display="Dynamic" ErrorMessage="Invalid Date" Type="Date" ValidationGroup="M1"
    SetFocusOnError="True"></asp:RangeValidator>&nbsp;<cc1:ValidatorCalloutExtender ID="vcgate"
        TargetControlID="rvgate" runat="server">
</cc1:ValidatorCalloutExtender>--%>
<cc1:CalendarExtender ID="ceDOM" runat="server" TargetControlID="txtDOM" Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<%--</ContentTemplate>
</asp:UpdatePanel>
--%>