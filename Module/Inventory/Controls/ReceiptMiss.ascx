<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReceiptMiss.ascx.cs" Inherits="Inventory_Controls_ReceiptMiss" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
</style>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
      
    function Calculation(val)
    {                                                                
     document.getElementById('ctl00_cphBody_ReceiptCredit1_txtAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtFinalRate').value)*(parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtQTY').value))).toFixed(4) ;
   }
</script>

<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
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
        width: 350px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        width: 80px;
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
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
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
                    <b class="titleheading">Material Receiving Cash Party(Without PO).</b>
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
                                    ReadOnly="true" ValidationGroup="M1"  Width="150px"></asp:TextBox>
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
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label16" runat="server" CssClass="Label SmallFont" Text="M.R.N. Date : "></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtMRNDate" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" ValidationGroup="M1"  Width="150px"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Receipt Shift : "></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlReceiptShift" runat="server" CssClass="SmallFont" TabIndex="1"  Width="150px">
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
                                            <asp:Literal ID="Container9" runat="server" Text='<%# Eval("GATE_TYPE") %>' />
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
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <%--        <asp:TextBox ID="txtGateEntryNo" runat="server" TabIndex="17" Width="80px" CssClass="TextBoxNo SmallFont"></asp:TextBox> --%>
                                <asp:TextBox ID="txtGateEntryNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label11" runat="server" CssClass="Label SmallFont" Text="Gate Entry Date :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtGateEntryDate" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true"  Width="150px"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label9" runat="server" CssClass="Label SmallFont" Text="Vehicle/Lorry No. :"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true"  Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label4" runat="server" CssClass="Label SmallFont" Text="Party Challan Number :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtPartyChallanNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true"  Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label5" runat="server" CssClass="Label SmallFont" Text="Party Challan Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtPartyChallanDate" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true"  Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label6" runat="server" CssClass="Label SmallFont" Text="Department :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtDepartment" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"  Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label19" runat="server" CssClass="LabelNo SmallFont" Text="Party Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"  Width="150px"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Width="150px"
                                        TabIndex="2" Font-Bold="True">
                                    </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label18" runat="server" CssClass="LabelNo SmallFont" Text="Transporter Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="lblTransporterCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"  Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label24" runat="server" CssClass="LabelNo SmallFont" Text="Transporter Details :"></asp:Label>
                            </td>
                            <td class="tdLeft" colspan="1" style="width: 32%">
                                <asp:TextBox ID="txtTransporterAddress" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"  Width="150px"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="17%">
                                <asp:Label ID="Label25" runat="server" CssClass="Label SmallFont" Text="Store :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                  <asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Width="150px"
                                        TabIndex="2" Font-Bold="True">
                                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelNo SmallFont" Text="Party Bill Number :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtPartyBillNo" runat="server" CssClass="TextBoxNo SmallFont" TabIndex="3"
                                     Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label2" runat="server" CssClass="Label SmallFont" Text="Party Bill Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtPartyBillDate" runat="server" CssClass="TextBox SmallFont" TabIndex="4"
                                     Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label3" runat="server" CssClass="Label SmallFont" Text="Party Bill Amount :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtPartyBillAmount" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                    OnTextChanged="txtPartyBillAmount_TextChanged" TabIndex="5"  Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label12" runat="server" CssClass="Label SmallFont" Text="Form Type :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtFormType" runat="server" CssClass="TextBox SmallFont" MaxLength="15"
                                    TabIndex="6"  Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label13" runat="server" CssClass="Label SmallFont" Text="Form Ref No. :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtFormRefNo" runat="server" CssClass="TextBox SmallFont" MaxLength="15"
                                    TabIndex="7"  Width="150px"></asp:TextBox>
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
                                    TabIndex="8"  Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label8" runat="server" CssClass="Label SmallFont" Text="L.R. Date :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtLRDate" runat="server" CssClass="TextBox SmallFont" TabIndex="9"
                                    Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label14" runat="server" CssClass="Label SmallFont" Text="Remarks :"></asp:Label>
                            </td>
                            <td class="tdLeft" colspan="5">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" MaxLength="200"
                                    TabIndex="10" Width="99%"></asp:TextBox>
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
                                Item Code
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
                                Remarks Remarks
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">
                                <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    EnableLoadOnDemand="True" DataTextField="ITEM_CODE" DataValueField="Combined"
                                    MenuWidth="800px" OnLoadingItems="Item_LOV_LoadingItems" OnSelectedIndexChanged="Item_LOV_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="11" Visible="true"
                                    Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            ITEM CODE</div>
                                        <div class="header c2">
                                            ITEM DESCRIPTION</div>
                                        <div class="header c4">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("ITEM_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("ITEM_DESC") %></div>
                                        <div class="item c4">
                                            <%# Eval("ITEM_TYPE")%></div>
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
                                <asp:Button ID="btnSubDetail" runat="server" Font-Size="8pt" OnClick="btnSubDetail_Click1"
                                    TabIndex="12" Text="Sub-Detail" Width="60px" />
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtQTY" runat="server" TabIndex="13" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    OnTextChanged="txtQTY_TextChanged" Width="60px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtNoOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="50px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtUOm" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="50px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtWeightOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="60px" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:TextBox ID="txtFinalRate" runat="server" CssClass="TextBoxNo SmallFont" Width="60px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDetRemarks" runat="server" TabIndex="15" CssClass="TextBox SmallFont"
                                    MaxLength="200" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save" Width="60px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" cssclass="LabelNo SmallFont tdLeft">
                                <asp:Label ID="Label20" runat="server" CssClass="Label SmallFont" Text="Item Code/Desc :"></asp:Label>
                                <asp:TextBox ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="140px"></asp:TextBox>
                                <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="360px"></asp:TextBox>
                                &nbsp;<asp:Label ID="Label22" runat="server" CssClass="Label SmallFont" Text="UOM :"></asp:Label>
                                <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px"></asp:TextBox>
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
                        <asp:GridView ID="grdMaterialItemReceipt" runat="server" AutoGenerateColumns="False"
                            CssClass="SmallFont" OnRowCommand="grdMaterialItemReceipt_RowCommand" OnRowDataBound="grdMaterialItemReceipt_RowDataBound"
                            ShowFooter="false" Width="99%">
                            <Columns>
                                <asp:TemplateField HeaderText="PO #" Visible="false" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPONum" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                            Text='<%# Bind("PO_NUMB") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                            Text='<%# Bind("ITEM_CODE") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                            Text='<%# Bind("ITEM_DESC") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                            Text='<%# Bind("UOM") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of Mfg." Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDOM" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                            Text='<%# Bind("DATE_OF_MFG", "{0:dd-MMM-yyyy}") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                            Text='<%# Bind("TRN_QTY") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NoOfUnit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtNoOfUnit" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                            Text='<%# Bind("NO_OF_UNIT") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UomOfUnit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUomOfUnit" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                            Text='<%# Bind("UOM_OF_UNIT") %>' Width="50px"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="WeightOfUnit" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtWeightOfUnit" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                            Text='<%# Bind("WEIGHT_OF_UNIT") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="B. Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtBasicRate" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                            Text='<%# Bind("BASIC_RATE") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="F. Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFinalRate" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                            Text='<%# Bind("FINAL_RATE") %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                            Text='<%# Bind("AMOUNT") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--    <asp:TemplateField HeaderText="Cost Code">
                            <ItemTemplate>
                                <asp:Label ID="txtCostCode" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                    Text='<%# Bind("COST_CODE") %>' Width="70px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> <asp:TemplateField HeaderText="Q.C.">
                            <ItemTemplate>
                                <asp:Label ID="txtQC" runat="server" CssClass="Label SmallFont" ReadOnly="true" Text='<%# Bind("QCFLAG") %>'
                                    Width="40px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" ReadOnly="true"
                                            Text='<%# Bind("REMARKS") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SubTran">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkunige" runat="server" CommandArgument='<%# Bind("UNIQUEID") %>'
                                            CssClass="Label SmallFont" Text="View Sub Tran">
                                        </asp:LinkButton>
                                        <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                            BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                                            <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdW_SIDE" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Of Unit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdW_SIDE14" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdW_SIDE15" runat="server" CssClass="SmallFont Label" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Weight Of Unit" HeaderStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdW_SIDE20" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdARTICLE_CODE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grade">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdUOM" runat="server" CssClass="SmallFont Label" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lot No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdBASIS" runat="server" CssClass="SmallFont Label" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date of Manufacturing">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgrdVALUE_QTY" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("DATE_OF_MFG","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnlBOM" PopupPosition="Left"
                                            TargetControlID="lnkunige">
                                        </cc1:HoverMenuExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("UNIQUEID") %>'
                                            CommandName="EditItem" Text="Edit"></asp:LinkButton>
                                        /
                                        <asp:LinkButton ID="lnkbtnDel" runat="server" CommandArgument='<%# Bind("UNIQUEID") %>'
                                            CommandName="DelItem" Text="Delete"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="SmallFont" />
                            <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                        </asp:GridView>
                        <asp:Label ID="lblPO_BRANCH" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblPO_TYPE" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblPO_COMP" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblPO_NUMB" runat="server" Visible="false"></asp:Label>
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
   <%-- </ContentTemplate>
</asp:UpdatePanel>--%>