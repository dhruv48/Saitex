<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YarnRecepitParty.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_YarnRecepitParty" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
</style>

<script type="text/javascript" language="javascript">

    function Calculation(val) {
        document.getElementById('ctl00_cphBody_ReceiptCredit1_txtAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtFinalRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtQTY').value))).toFixed(4);
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
        margin-left: 4px;
    }
    .c1
    {
        width: 200px;
    }
    .c2
    {
        margin-left: 2px;
        width: 400px;
    }
    .c3
    {
        width: 200px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 2px;
        width: 120px;
    }
    .d3
    {
        margin-left: 2px;
        width: 180px;
    }
    .d4
    {
        margin-left: 2px;
        width: 180px;
    }
</style>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 250px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 340px;
    }
    .c6
    {
        margin-left: 4px;
        width: 150px;
    }
    .style1
    {
        width: 19%;
    }
    </style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="tdMain tContentArial" width="900px">
            <tr>
                <td class="td" width="100%">
                    <table class="tContentArial">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnSave_Click" TabIndex="17" Style="height: 41px" ToolTip="Save"
                                    ValidationGroup="gg" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Enabled="false" ImageUrl="~/CommonImages/del6.png"
                                    OnClick="imgbtnDelete_Click" ToolTip="Delete" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" TabIndex="18" ToolTip="Find" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" TabIndex="19" ToolTip="Clear" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" TabIndex="20" ToolTip="Print" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" TabIndex="21" ToolTip="Exit" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" TabIndex="22" ToolTip="Help" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <b class="titleheading">Yarn Receiving Against Party.</b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" valign="top" width="100%">
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                        &nbsp;Mode </span>
                </td>
            </tr>
            <tr>
                <td class="td SmallFont" width="100%">
                    <table width="100%">
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label15" runat="server" CssClass="LabelNo SmallFont" Text="M.R.N. Number : "></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtTRNNUMBer" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" DataTextField="TRN_NUMB"
                                    DataValueField="TRN_NUMB" EnableLoadOnDemand="true" Height="200px" MenuWidth="700"
                                    OnLoadingItems="ddlTRNNumber_LoadingItems" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                                    Width="150px">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            MRN #</div>
                                        <div class="header c4">
                                            MRN Date</div>
                                        <div class="header c4">
                                            Party Code</div>
                                        <div class="header c2">
                                            Party Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("TRN_NUMB") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal ID="Container6" runat="server" Text='<%# Eval("TRN_DATE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal ID="Literal5" runat="server" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label16" runat="server" CssClass="Label SmallFont" Text="M.R.N. Date : "></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtMRNDate" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" ValidationGroup="M1" Width="150px"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Receipt Shift : "></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlReceiptShift" runat="server" CssClass="SmallFont" TabIndex="1" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label10" runat="server" CssClass="LabelNo SmallFont" Text="Gate Entry No. :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <cc2:ComboBox ID="ddlGateEntryNo" runat="server" AutoPostBack="True" DataTextField="GATE_NUMB"
                                    DataValueField="GATE_NUMB" EnableLoadOnDemand="true" Height="200px" MenuWidth="800px"
                                    OnLoadingItems="ddlGateEntryNo_LoadingItems" OnSelectedIndexChanged="ddlGateEntryNo_SelectedIndexChanged"
                                    Width="80px">
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
                                            <asp:Literal ID="Container7" runat="server" Text='<%# Eval("GATE_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container8" runat="server" Text='<%# Eval("GATE_DATE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Container9" runat="server" Visible="false" Text='<%# Eval("GATE_TYPE") %>' />
                                            <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("ITEM_TYPE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal ID="Literal9" runat="server" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("TRSP_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal ID="Literal10" runat="server" Text='<%# Eval("TRSP_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <%--        <asp:TextBox ID="txtGateEntryNo" runat="server" TabIndex="17" Width="80px" CssClass="TextBoxNo SmallFont"></asp:TextBox> --%>
                                <asp:TextBox ID="txtGateEntryNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="65px"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label11" runat="server" CssClass="Label SmallFont" Text="Gate Entry Date :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtGateEntryDate" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="150px"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label9" runat="server" CssClass="Label SmallFont" Text="Vehicle/Lorry No. :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label4" runat="server" CssClass="Label SmallFont" Text="Party Challan Number :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtPartyChallanNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label5" runat="server" CssClass="Label SmallFont" Text="Party Challan Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtPartyChallanDate" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label6" runat="server" CssClass="Label SmallFont" Text="Department :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label19" runat="server" CssClass="LabelNo SmallFont" Text="Party Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label23" runat="server" CssClass="LabelNo SmallFont" Text="Party Details :"></asp:Label>
                            </td>
                            <td class="tdLeft" colspan="1" style="width: 32%">
                                <asp:TextBox ID="txtPartyAddress" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="17%">
                                <asp:Label ID="Label21" runat="server" CssClass="Label SmallFont" Text="Location :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                      <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Font-Size="9" TabIndex="2"
                Width="150px">
            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label18" runat="server" CssClass="LabelNo SmallFont" Text="Transporter Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="lblTransporterCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label24" runat="server" CssClass="LabelNo SmallFont" Text="Transporter Details :"></asp:Label>
                            </td>
                            <td class="tdLeft" colspan="1" style="width: 32%">
                                <asp:TextBox ID="txtTransporterAddress" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="17%">
                                <asp:Label ID="Label25" runat="server" CssClass="Label SmallFont" Text="Store :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                         
            
            <asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Font-Size="9" TabIndex="3"
                Width="150px">
            </asp:DropDownList>
            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelNo SmallFont" Text="Party Bill Number :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtPartyBillNo" runat="server" CssClass="TextBox SmallFont" TabIndex="4"
                                    Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label2" runat="server" CssClass="Label SmallFont" Text="Party Bill Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtPartyBillDate" runat="server" CssClass="TextBox SmallFont" TabIndex="5"
                                    Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label3" runat="server" CssClass="Label SmallFont" Text="Party Bill Amount :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtPartyBillAmount" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                    OnTextChanged="txtPartyBillAmount_TextChanged" TabIndex="6" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label12" runat="server" CssClass="Label SmallFont" Text="Form Type :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtFormType" runat="server" CssClass="TextBox SmallFont" MaxLength="15"
                                    TabIndex="7" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label13" runat="server" CssClass="Label SmallFont" Text="Form Ref No. :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtFormRefNo" runat="server" CssClass="TextBox SmallFont" MaxLength="15"
                                    TabIndex="8" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="17%" colspan="2" rowspan="2" style="width: 32%">
                                <span id="spnAInW" runat="server" style="font-size: 8pt; height: 9pt;">Amount in words...</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label7" runat="server" CssClass="LabelNo SmallFont" Text="L.R. Number :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtLRNo" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="15"
                                    TabIndex="9" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label8" runat="server" CssClass="Label SmallFont" Text="L.R. Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtLRDate" runat="server" CssClass="TextBox SmallFont" TabIndex="10"
                                    Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label14" runat="server" CssClass="Label SmallFont" Text="Remarks :"></asp:Label>
                            </td>
                            <td class="tdLeft" colspan="3">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" MaxLength="200"
                                    TabIndex="11" Width="99%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label26" runat="server" CssClass="Label SmallFont" Text="Spinner :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                   <cc2:ComboBox ID="txtSpinnerCode" TabIndex="12" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtSpinner_LoadingItems" DataTextField="PRTY_NAME" DataValueField="PRTY_CODE"
                            EmptyText="Select Spinner" 
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="400px" Height="200px">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Code</div>
                                <div class="header c3">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" width="100%">
                    <table width="100%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                           <td class="style1 tdLeft">
                                Quality Code
                            </td>
                            <td class="style1 tdLeft">
                                Shade
                            </td> <td class="style1 tdLeft">
                        Lot No
                    </td>
                    <td class="style1 tdLeft">
                        Grade
                    </td>
                            <td class="style1 tdLeft">
                                Detail
                            </td>
                            <td class="style1 tdRight">
                                Qty
                            </td>
                            <td class="style1 tdRight">
                                NoOfUnit
                            </td>
                            <td class="style1 tdLeft">
                                UOM
                            </td>
                            <td class="style1 tdRight">
                                Weight Of Unit
                            </td>
                            <td class="style1 tdRight">
                                Rate
                            </td>
                            <td class="style1 tdLeft">
                                Remarks
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr> 
                          <td class="tdLeft">
                                <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    EnableLoadOnDemand="True" DataTextField="YARN_CODE" DataValueField="Combined"
                                    MenuWidth="600px" OnLoadingItems="Item_LOV_LoadingItems" OnSelectedIndexChanged="Item_LOV_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="12" Visible="true"
                                    Height="200px" Width="120px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Quality Code</div>
                                        <div class="header c2">
                                            Quality Description</div>
                                        <div class="header c4">
                                           Quality Type</div>
                                        <%--<div class="header c3">
                                    COLOUR</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("ASS_YARN_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("ASS_YARN_DESC")%></div>
                                        <div class="item c4">
                                            <%# Eval("YARN_TYPE")%></div>
                                        <%--  <div class="item c3">
                                    <%# Eval("COLOUR")%></div>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdLeft">                                
                               <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="SHADE_NAME" EnableLoadOnDemand="True"
                                                MenuWidth="300" EnableVirtualScrolling="true" 
                                    OpenOnFocus="true" TabIndex="13"
                                                Height="200px" Visible="true" Width="100px" 
                                    OnLoadingItems="cmbShade_LoadingItems"  onselectedindexchanged="cmbShade_SelectedIndexChanged"
                                                >
                                                <HeaderTemplate>                                                  
                                                    <div class="header d2">
                                                        Shade Family Name</div>                                                  
                                                    <div class="header d4">
                                                        Shade Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                   
                                                    <div class="item d2">
                                                        <%# Eval("SHADE_FAMILY_NAME")%></div>                                                    
                                                    <div class="item d4">
                                                        <%# Eval("SHADE_NAME")%></div>
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
                            EmptyText="Merge No" 
                            EnableVirtualScrolling="true" Width="100px" MenuWidth="300px" Height="200px"  TabIndex="28">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Merge No</div>
                                <div class="header c3">
                                    Desc</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' /></div>
                                <div class="item c3">
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
                    
                          
                            <td class="tdLeft">
                                <asp:Button ID="btnSubDetail" runat="server" Font-Size="8pt" OnClick="btnSubDetail_Click1"
                                    TabIndex="14" Text="Sub Details" Width="65px" />
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtQTY" runat="server" TabIndex="15" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" OnTextChanged="txtQTY_TextChanged" Width="60px"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtNoOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="50px"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtUOm" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="50px"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtWeightOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtFinalRate" runat="server" CssClass="TextBoxNo SmallFont" Width="60px" TabIndex="16"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDetRemarks" runat="server" TabIndex="17" CssClass="TextBox SmallFont"
                                    MaxLength="200" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click" TabIndex="17"
                                    Text="Add" Width="60px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11" cssclass="LabelNo SmallFont tdLeft">
                                <asp:Label ID="Label20" runat="server" CssClass="Label SmallFont" Text="Quality Code/Desc :"></asp:Label>
                                <asp:TextBox ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="50px"></asp:TextBox>
                                <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="200px"></asp:TextBox>
                                &nbsp;<asp:Label ID="Label22" runat="server" CssClass="Label SmallFont" Text="UOM :"></asp:Label>
                                <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="40px"></asp:TextBox>
                              
                                ShadeFamily/Shade:<asp:TextBox ID="txtShadeFamilyName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="100px"></asp:TextBox>
                                            <asp:TextBox ID="txtShadeName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="120px"></asp:TextBox>
                                                <asp:HiddenField ID="txtGrossWt" runat="server" />
                             <asp:HiddenField ID="txtTareWt" runat="server" />
                              <asp:HiddenField ID="txtCartons" runat="server" />
                               <asp:HiddenField ID="lblPO_NUMB" runat="server" />
                              <asp:HiddenField ID="txtNoOfPallet" runat="server"   />
                            </td>
                            <td class="tdLeft">
                                <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                                    Text="Cancel" Width="60px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" width="100%">
                    <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                <asp:GridView ID="grdMaterialItemReceipt" Width="99%" runat="server" AutoGenerateColumns="False"
                    CssClass="SmallFont" ShowFooter="false" OnRowCommand="grdMaterialItemReceipt_RowCommand"
                    OnRowDataBound="grdMaterialItemReceipt_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="PO #" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtPONum" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                     ReadOnly="true"></asp:Label>
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
                        <asp:TemplateField HeaderText="Quality Code">
                            <ItemTemplate>
                                <asp:LinkButton ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                     ReadOnly="true">
                                </asp:LinkButton>
                                <cc1:HoverMenuExtender ID="hvrYarn" runat="server" TargetControlID="txtICODE" PopupControlID="pnlyarndtl"
                                    PopupPosition="Bottom" PopDelay="500">
                                </cc1:HoverMenuExtender>
                                <asp:Panel ID="pnlyarndtl" runat="server" BackColor="Red">
                                    <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                        ReadOnly="true"></asp:Label></asp:Panel>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Shade Family">
                            <ItemTemplate>
                                <asp:Label ID="txtshadeFamily" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_FAMILY") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade Code">
                            <ItemTemplate>
                                <asp:Label ID="txtshadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date of Mfg." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtDOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DATE_OF_MFG", "{0:dd-MMM-yyyy}") %>'
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
                        <asp:TemplateField HeaderText="Net&nbsp;Wt">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cops">
                            <ItemTemplate>
                                <asp:Label ID="txtNoOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="Avg.Wt.">
                            <ItemTemplate>
                                <asp:Label ID="txtWeightOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                    ReadOnly="true"></asp:Label>
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
                        <asp:TemplateField HeaderText="Cost Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtCostCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("COST_CENTER_CODE") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REMARKS") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q.C." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtQC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("QCFLAG") %>'
                                    ReadOnly="true"></asp:Label>
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
        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtTRNNUMBer"
            Display="Dynamic" ErrorMessage="MRN number required" SetFocusOnError="True" ValidationGroup="M1">
        </asp:RequiredFieldValidator>
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
</ContentTemplate>
</asp:UpdatePanel>