<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReceiveAgainstWorkOrder.ascx.cs"
    Inherits="Module_WorkOrder_Controls_ReceiveAgainstWorkOrder" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    
    function Calculation(val)
    {                                                                
     document.getElementById('ctl00_cphBody_ReceiptHeading1_txtAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_ReceiptHeading1_txtFinalRate').value)*(parseFloat(document.getElementById('ctl00_cphBody_ReceiptHeading1_txtQTY').value))).toFixed(3) ;
    }
    function updateValues(popupValues)
    {   
        document.getElementById('txtPartyBillAmount').innerHTML=popupValues[0];
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
    .HeaderRow
    {
        font-size: 8pt;
        font-weight: bold;
    }
    .style2
    {
        font-weight: bold;
    }
    .SmallFont
    {}
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
            <b class="titleheading">M.R.N. Against Work Order</b>
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
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td width="17%" class="tdRight style2">
                        <asp:Label ID="Label15" runat="server" Text="M.R.N. Number : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <b>
                            <asp:TextBox ID="txtTRNNUMBer" runat="server" ValidationGroup="M1" Width="150px" TabIndex="1"
                                CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true" Font-Bold="True"></asp:TextBox>
                            <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                                EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                                Width="150px" Height="200px" MenuWidth="500px">
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
                                        <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' />
                                    </div>
                                    <div class="item c2">
                                        <asp:Literal runat="server" ID="Container6" Text='<%# Eval("TRN_DATE") %>' />
                                    </div>
                                    <div class="item c2">
                                        <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("PRTY_CODE") %>' />
                                    </div>
                                    <div class="item c4">
                                        <asp:Literal runat="server" ID="Literal5" Text='<%# Eval("PRTY_NAME") %>' />
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Displaying items
                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                    <%# Container.ItemsCount %>.
                                </FooterTemplate>
                            </cc2:ComboBox>
                        </b>
                    </td>
                    <td width="17%" class="tdRight style2">
                        <asp:Label ID="Label16" runat="server" Text="M.R.N. Date : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <b>
                            <asp:TextBox ID="txtMRNDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="150px"
                                CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true" Font-Bold="True"></asp:TextBox>
                        </b>
                    </td>
                    <td width="17%" class="tdRight style2">
                        <asp:Label ID="Label17" runat="server" Text="Receipt Shift : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <b>
                            <asp:DropDownList ID="ddlReceiptShift" runat="server" CssClass="SmallFont" Width="150px"
                                TabIndex="2" Font-Bold="True">
                            </asp:DropDownList>
                        </b>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label10" runat="server" Text="Gate Entry No. :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <cc2:ComboBox ID="ddlGateEntryNo" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlGateEntryNo_LoadingItems" DataTextField="GATE_NUMB" DataValueField="GATE_NUMB"
                            EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlGateEntryNo_SelectedIndexChanged"
                            Width="85px" Height="200px" MenuWidth="950px">
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
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("GATE_NUMB") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("GATE_DATE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("GATE_TYPE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("PRTY_CODE") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("PRTY_NAME") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal8" Text='<%# Eval("TRSP_CODE") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal10" Text='<%# Eval("TRSP_NAME") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:TextBox ID="txtGateEntryNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="50px" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label11" runat="server" Text="Gate Entry Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <asp:TextBox ID="txtGateEntryDate" runat="server" TabIndex="18" Width="150px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="150px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label4" runat="server" Text="Party Challan Number :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <asp:TextBox ID="txtPartyChallanNo" runat="server" TabIndex="9" Width="150px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label5" runat="server" Text="Party Challan Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <asp:TextBox ID="txtPartyChallanDate" runat="server" TabIndex="10" Width="150px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label6" runat="server" Text="Department :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtDepartment" runat="server" TabIndex="11" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label19" runat="server" Text="Jober Party :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <asp:TextBox ID="lblPartyCode" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="150px"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label20" runat="server" Text="Party Details :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" colspan="1" style="width: 49%">
                        <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" Width="100%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%" >
                        <asp:Label ID="Label22" runat="server" Text="Location :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%" >
                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Font-Size="9"    Width="150px" TabIndex="3">    </asp:DropDownList>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label18" runat="server" Text="Transporter Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <asp:TextBox ID="lblTransporterCode" TabIndex="5" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="150px"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label21" runat="server" Text="Transporter Details :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" colspan="1" style="width: 49%">
                        <asp:TextBox ID="txtTransporterAddress" TabIndex="5" runat="server" Width="100%"
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="True"></asp:TextBox>
                    </td>
                    
                     <td align="right" valign="top" width="17%" >
                        <asp:Label ID="Label23" runat="server" Text="Store :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%" >
                         <asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Font-Size="9"    Width="150px" TabIndex="3">   </asp:DropDownList>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label1" runat="server" Text="Party Bill Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <asp:TextBox ID="txtPartyBillNo" runat="server" TabIndex="6" Width="150px" CssClass="TextBox SmallFont"
                            AutoPostBack="True" OnTextChanged="txtPartyBillNo_TextChanged"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label2" runat="server" Text="Party Bill Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <asp:TextBox ID="txtPartyBillDate" runat="server" TabIndex="7" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        Party Bill Amount :
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtTotalPartyAmt" runat="server" TabIndex="8" Width="150px" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-weight: bold" >
                    <td valign="top" class="tdRight" width="15%" >
                        <asp:Label ID="Label24" runat="server" CssClass="LabelNo" Text="Party / Self Job:"></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="3" width="85%">
                        <cc2:ComboBox ID="ddlPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="ddlPartyCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="600px" Height="200px" TabIndex="6">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c5">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container10" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container11" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:TextBox ID="txtPartyCode" TabIndex="400" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="80px"></asp:TextBox>
                        <asp:TextBox ID="txtPartyAddress1" TabIndex="401" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="550px"></asp:TextBox>
                            
                             <td width="17%" class="tdRight">
                        &nbsp;<asp:Label ID="Label3" runat="server" Text="Total Amount :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="8" Width="150px" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    </td>
                </tr>
                <tr>
                    <td width="17%" class="tdRight" visible="false">
                        <asp:Label ID="Label12" runat="server" visible="false" Text="Form Type :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft" visible="false">
                        <asp:TextBox ID="txtFormType" runat="server" visible="false" TabIndex="19" Width="150px" CssClass="TextBox SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight" visible="false">
                        <asp:Label ID="Label13" runat="server" visible="false" Text="Form Ref No. :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft" visible="false">
                        <asp:TextBox ID="txtFormRefNo" runat="server" visible="false" TabIndex="20" Width="150px" CssClass="TextBox SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr style="font-weight: bold">
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label7" runat="server" Text="L.R. Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <asp:TextBox ID="txtLRNo" runat="server" TabIndex="14" Width="150px" CssClass="TextBoxNo SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label8" runat="server" Text="L.R. Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td width="17%" class="tdLeft">
                        <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td width="17%" class="tdRight">
                        Other Charges :
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:Button ID="btnDisTaxAdjMST" runat="server" Text="Other Charges" Width="100px"
                            CssClass="SmallFont" OnClick="btnDisTaxAdjMST_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="font-size: x-small; width: 100%;" colspan="4">
                        <asp:Label ID="txtAmountInWords" runat="server" Width="100%" TabIndex="21" Font-Size="X-Small"></asp:Label>
                    </td>
                    <td width="17%" class="tdRight">
                        Final Amount :
                    </td>
                    <td width="15%" class="tdLeft">
                        <asp:TextBox ID="txtPartyBillAmount" runat="server" TabIndex="8" Width="150px" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            AutoPostBack="True" OnTextChanged="txtPartyBillAmount_TextChanged" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td width="17%" class="tdRight">
                        <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="83%" colspan="5">
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
                    <td class="HeaderRow">
                        WO Numb
                    </td>
                    
                    <td class="HeaderRow">
                        Lot No
                    </td>
                    <td class="HeaderRow" >
                        Grade
                    </td>
                     <td >
                        
                    </td>
                    <td class="HeaderRow">
                        No Of Unit
                    </td>
                    <td class="HeaderRow">
                        Unit Weight
                    </td>
                    <td class="HeaderRow">
                        Quantity
                    </td>
                    <td class="HeaderRow">
                        Basic Rate
                    </td>
                    <td class="HeaderRow">
                        WO Rate
                    </td>
                    <td class="HeaderRow">
                        Dis/Tax
                    </td>
                    <td class="HeaderRow">
                        Final Rate
                    </td>
                    <td class="HeaderRow">
                        Amount
                    </td>
                    <td class="HeaderRow">
                        Remarks
                    </td>
                    <td class="HeaderRow">
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc2:ComboBox ID="cmbPOITEM" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="900px" Width="80px"
                            EnableVirtualScrolling="true" OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    WO No.</div>
                                   <div class="header c2">
                                    Issue Challan No</div> 
                                <div class="header c2">
                                    Article Code</div>
                                <div class="header c3">
                                    Description</div>
                                <div class="header c2">
                                    Shade Code</div>
                                <div class="header c2">
                                  Order  Quantity</div>
                                <div class="header c2">
                                  Lot No</div>
                                  
                                  <div class="header c2">
                                  Issue  Quantity</div>
                                <div class="header c2">
                                    Qty Rec.</div>
                                <div class="header c2">
                                    Qty. Rem.</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("WO_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal11" Text='<%# Eval("TRN_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ARTICLE_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ARTICLE_DESC") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal6" Text='<%# Eval("SHADE_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("QTY") %>' /></div>
                                    <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal13" Text='<%# Eval("LOT_NO") %>' /></div>
                                     <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal12" Text='<%# Eval("TRN_QTY") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("QTY_REC") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("QTY_REM") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    
                    <td>
                      <cc2:ComboBox ID="txtLotNo" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtLotNo_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Lot No" 
                            EnableVirtualScrolling="true" Width="100px" MenuWidth="300px" Height="200px"  TabIndex="28">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Lot No</div>
                                <div class="header c2">
                                    Desc</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MST_DESC") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    
                    <td>
                        <cc2:ComboBox ID="txtGrade" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtGrade_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Grade" 
                            EnableVirtualScrolling="true" Width="60px" MenuWidth="100px" Height="200px"  TabIndex="29">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Grade</div>
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' />
                                    </div>
                               
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                     <td>
                     <asp:Button ID="btnSubDetail" runat="server" OnClick="btnSubDetail_Click" Text="Cortoons"
                            Width="60px" CssClass="SmallFont" />
                            </td>
                    <td>
                        <asp:TextBox ID="txtNoOfUnit" runat="server" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            AutoPostBack="true" Width="70px" OnTextChanged="txtNoOfUnit_TextChanged" 
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeightofUnit" runat="server" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            AutoPostBack="true" Width="70px" OnTextChanged="txtWeightofUnit_TextChanged"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            AutoPostBack="true" Width="70px" ReadOnly="true" 
                            OnTextChanged="txtQTY_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtpoRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="70px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnDisTaxAdj" runat="server" Text="Dis/Tax" Width="50px" CssClass="SmallFont"
                            OnClick="btnDisTaxAdj_Click" Height="27px" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtFinalRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="70px" AutoPostBack="True" 
                            OnTextChanged="txtFinalRate_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="80px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="170px"
                            MaxLength="200" Height="100%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" Width="50px" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="13">
                       
                        WO Numb :
                        <asp:TextBox ID="txtPONumb" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="60px" ReadOnly="true"></asp:TextBox>
                        &nbsp;Article Code/Desc :
                        <asp:TextBox ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="75px" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="250px"></asp:TextBox>
                        &nbsp;UOM :
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="55px"></asp:TextBox>
                        &nbsp;Max Qty :
                        <asp:TextBox ID="lblMaxQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="70px" ReadOnly="true"></asp:TextBox>
                        &nbsp;Shade:<asp:TextBox ID="txtShadeCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="80px" ReadOnly="true"></asp:TextBox>
                              &nbsp; <%--Shade Family:--%><asp:TextBox ID="txtShadeFamily" Visible="false" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                            <asp:HiddenField ID="txtGrossWt" runat="server" />
                             <asp:HiddenField ID="txtTareWt" runat="server" />
                              <asp:HiddenField ID="txtCartons" runat="server" />
                              <asp:HiddenField ID="txtNoOfPallet" runat="server"   />
                    </td>
                    <td>
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                            Text="Cancel" Width="50px" />
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
                     OnRowDataBound="grdMaterialItemReceipt_RowDataBound" >
                    <Columns>
                        <asp:TemplateField HeaderText="WO #">
                            <ItemTemplate>
                                <asp:Label ID="txtPONum" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                    Width="40px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ARTICLE Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                    Width="70px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                    Width="120px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="LOT#">
                            <ItemTemplate>
                                <asp:Label ID="txtLotNo1" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="GRADE">
                            <ItemTemplate>
                                <asp:Label ID="txtGrade1" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Shade">
                            <ItemTemplate>
                                <asp:Label ID="txtShade" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                    Width="120px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Shade Family" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtshadeFamily" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Gross&nbsp;Wt.">
                            <ItemTemplate>
                                <asp:Label ID="txtGrossWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GROSS_WT") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tare&nbsp;Wt.">
                            <ItemTemplate>
                                <asp:Label ID="txtTareWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TARE_WT") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        
                         <asp:TemplateField HeaderText="Net Wt">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                                <asp:Label ID="txtTRN_QTY_1" runat="server" CssClass="LabelNo SmallFont" Visible="false"
                                    Text='<%# Bind("TRN_QTY_1") %>' Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Cops">
                            <ItemTemplate>
                                <asp:Label ID="txtNO_OF_UNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM Of Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Av. Wt">
                            <ItemTemplate>
                                <asp:Label ID="txtWEIGHT_OF_UNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Cartons">
                            <ItemTemplate>
                                <asp:Label ID="txtCartons" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CARTONS") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="B. Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtBasicRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BASIC_RATE") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtPORate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_RATE") %>'
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
                         <asp:TemplateField HeaderText="Cost Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtCostCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("COST_CENTER_CODE") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REMARKS") %>'
                                    Width="120px" ReadOnly="true"></asp:Label>
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
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="PI No" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbtpino" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lot&nbsp;No" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                     
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblLotNO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Grade">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                    </ItemTemplate>
                                              <FooterTemplate>
                                                    <asp:Label ID="flblBOMUOM" runat="server" CssClass="LabelNo SmallFont"  >Total:</asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Carton&nbsp;No">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbtcartonno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CARTON_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:Label ID="flbtcartonno" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cops">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblNoUnit" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Gross&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrossWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GROSS_WT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblGrossWt" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tare&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTareWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TARE_WT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate >
                                                    <asp:Label ID="flblTareWt" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                      <FooterTemplate>
                                                    <asp:Label ID="flblQTY" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="UOM">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bar&nbsp;Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBarcodeNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BARCODE_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Pallet">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPallet" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("IS_PALLET") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <FooterTemplate>
                                                    <asp:Label ID="flblPallet" runat="server" CssClass="LabelNo SmallFont" ></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Mfd" Visible="false">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                               
                                                <asp:TemplateField HeaderText="WeightofUnit">
                                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWeightofUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material Status" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtBOMArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
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
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                                /
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
<cc1:ValidatorCalloutExtender ID="vcMRNNo" runat="server" TargetControlID="rfv1">
</cc1:ValidatorCalloutExtender>
<cc1:ValidatorCalloutExtender ID="vcrv1" runat="server" TargetControlID="rv1">
</cc1:ValidatorCalloutExtender>
<asp:RangeValidator ID="rv1" runat="server" ControlToValidate="txtTRNNUMBer" Display="Dynamic"
    ErrorMessage="Only numeric value allowed" MaximumValue="1000000" MinimumValue="1"
    SetFocusOnError="True" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
<br />
<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtTRNNUMBer"
    Display="Dynamic" ErrorMessage="MRN number required" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
<cc1:CalendarExtender ID="cebill" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPartyBillDate">
</cc1:CalendarExtender>
<asp:RangeValidator ID="rvbill" runat="server" ControlToValidate="txtPartyBillDate"
    Display="Dynamic" ErrorMessage="Invalid Party Bill Date" SetFocusOnError="True"
    Type="Date" ValidationGroup="M1"></asp:RangeValidator>
<cc1:ValidatorCalloutExtender ID="vcbill" runat="server" TargetControlID="rvbill">
</cc1:ValidatorCalloutExtender>
<cc1:CalendarExtender ID="celr" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLRDate">
</cc1:CalendarExtender>
<asp:RangeValidator ID="rvlr" runat="server" ControlToValidate="txtLRDate" Display="Dynamic"
    ErrorMessage="Invalid LR Date" SetFocusOnError="True" Type="Date" ValidationGroup="M1"></asp:RangeValidator>
<cc1:ValidatorCalloutExtender ID="vclr" runat="server" TargetControlID="rvlr">
</cc1:ValidatorCalloutExtender>
</ContentTemplate></asp:UpdatePanel>
