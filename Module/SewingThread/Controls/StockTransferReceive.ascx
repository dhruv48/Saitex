<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StockTransferReceive.ascx.cs"
    Inherits="Module_Sewing_Thread_Controls_StockTransferReceive" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">

    function Calculation(val) {
        document.getElementById('ctl00_cphBody_ReceiptCredit1_txtAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtFinalRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtQTY').value))).toFixed(3);
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
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
                                    ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1" Style="height: 41px">
                                </asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
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
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <b class="titleheading">Sewing Thread Stock Transfer Receiving</b>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" class="td" width="100%">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ValidationGroup="M1" />
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td SmallFont">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label15" runat="server" Text="M.R.N. Number : " CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
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
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label16" runat="server" Text="M.R.N. Date : " CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtMRNDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="80px"
                                    CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label17" runat="server" Text="Receipt Shift : " CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <cc3:OboutDropDownList ID="ddlReceiptShift" runat="server" TabIndex="2" Width="85px">
                                </cc3:OboutDropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label19" runat="server" Text="Issue Detail :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="ddlIssueDetail" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlIssueDetail_LoadingItems" DataTextField="TRN_NUMB" DataValueField="ISS_DATA"
                                    EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlIssueDetail_SelectedIndexChanged"
                                    Width="85px" MenuWidth="650px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Year</div>
                                        <div class="header c3">
                                            Comp</div>
                                        <div class="header c3">
                                            Branch</div>
                                        <div class="header c2">
                                            TRN Type</div>
                                        <div class="header c2">
                                            TRN No</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("YEAR") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("COMP_CODE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("BRANCH_CODE") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("TRN_TYPE") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("TRN_NUMB") %>' /></div>
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
                                <asp:Label ID="Label20" runat="server" Text="Issue Year :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtIssueYear" runat="server" TabIndex="18" Width="80px" ReadOnly="true"
                                    CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label23" runat="server" Text="Issue Company :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtIssueCompany" runat="server" TabIndex="18" Width="80px" ReadOnly="true"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label22" runat="server" Text="Issue Branch :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtIssueBranch" runat="server" TabIndex="18" Width="80px" ReadOnly="true"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label21" runat="server" Text="Issue TRN Type :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtIssueTRNType" runat="server" TabIndex="18" Width="80px" ReadOnly="true"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label24" runat="server" Text="Issue TRN Numb :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtIssueTRNNumb" runat="server" TabIndex="18" Width="80px" ReadOnly="true"
                                    CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
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
                                <asp:Label ID="Label18" runat="server" Text="Transporter Code :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged" Width="85px"
                                    Height="200px" MenuWidth="550px">
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
                            <td class="tdRight" width="15%">
                                <asp:TextBox ID="txtTransporter" TabIndex="5" runat="server" Width="100px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                            </td>
                            <td class="tdLeft" width="55%">
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
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label10" runat="server" Text="Gate Entry No. :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="ddlGateEntryNo" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlGateEntryNo_LoadingItems" DataTextField="GATE_NUMB" DataValueField="GATE_NUMB"
                                    OnSelectedIndexChanged="ddlGateEntryNo_SelectedIndexChanged" Width="85px" Height="200px"
                                    MenuWidth="450px">
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
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label25" runat="server" Text="Gate Entry No :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtGateEntryNo" runat="server" TabIndex="9" Width="80px" ReadOnly="true"
                                    CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label11" runat="server" Text="Gate Entry Date :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtGateEntryDate" runat="server" TabIndex="18" Width="80px" ReadOnly="true"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%" class="tdRight">
                                <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="15%" class="tdLeft">
                                <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="80px" ReadOnly="true"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td width="15%" class="tdRight">
                                <asp:Label ID="Label4" runat="server" Text="Party Challan No :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="15%" class="tdLeft">
                                <asp:TextBox ID="txtPartyChallanNo" runat="server" TabIndex="9" Width="80px" ReadOnly="true"
                                    CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td width="15%" class="tdRight">
                                <asp:Label ID="Label5" runat="server" Text="Party Challan Date :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td width="25%" class="tdLeft">
                                <asp:TextBox ID="txtPartyChallanDate" runat="server" TabIndex="10" Width="80px" ReadOnly="true"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label7" runat="server" Text="L.R. Number :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtLRNo" runat="server" TabIndex="14" Width="80px" CssClass="TextBoxNo SmallFont"
                                    MaxLength="15"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label8" runat="server" Text="L.R. Date :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%" colspan="3" style="width: 30%">
                                <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
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
                    <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                        <asp:GridView ID="grdMaterialItemReceipt" Width="99%" runat="server" AutoGenerateColumns="False"
                            CssClass="SmallFont" ShowFooter="false" OnRowDataBound="grdMaterialItemReceipt_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="ST Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                            Width="70px" ReadOnly="true"></asp:Label>
                                        <asp:Label ID="txtUNIQUEID" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UNIQUEID") %>'
                                            Width="70px" ReadOnly="true" Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                            Width="120px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shade Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtShadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                            Width="120px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No Of Unit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtNoOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Uom Of Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUomOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'
                                            Width="50px" ReadOnly="true">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight Of Unit" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtWeightOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtBasicRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BASIC_RATE") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtFinalRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("FINAL_RATE") %>'
                                            Width="50px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("AMOUNT") %>'
                                            Width="70px" ReadOnly="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SubTran">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkunige" runat="server" CssClass="Label SmallFont" Text="View Sub Tran"
                                            CommandArgument='<%# Bind("UNIQUEID") %>'>
                                        </asp:LinkButton>
                                        <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                            BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False" ShowFooter="true">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr No." HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                    Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtSrNo" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No Of Unit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                    FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtNoOfUnit1" runat="server" CssClass="SmallFont TextBoxNo" Text='<%# Bind("NO_OF_UNIT") %>'
                                                                            Width="60px" AutoPostBack="True" OnTextChanged="txtNoOfUnit1_TextChanged"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="txtTotalNoOfUnits" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Weight Of Unit" HeaderStyle-HorizontalAlign="Right"
                                                                    ItemStyle-HorizontalAlign="Right">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtWtOfUnit" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                                    FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgrdW_SIDE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="txtTotalQty" runat="server" Width="60px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="UOM">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgrdW_SIDE15" runat="server" CssClass="SmallFont Label" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Shade Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblShade" runat="server" CssClass="SmallFont Label" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
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
                                                                        <asp:Label ID="lblgrdVALUE_QTY" runat="server" CssClass="SmallFont LabelNo" Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle CssClass="SmallFont" />
                                                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="btnCloseSUB" runat="server" Text="Save And Close" OnClick="btnCloseSUB_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <cc1:ModalPopupExtender ID="mpeRed" BackgroundCssClass="modalBackground" PopupControlID="pnlBOM"
                                            DropShadow="true" runat="server" TargetControlID="lnkunige">
                                        </cc1:ModalPopupExtender>
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
        <asp:RequiredFieldValidator ControlToValidate="txtTRNNUMBer" ID="rfv1" runat="server"
            ValidationGroup="M1" ErrorMessage="MRN number required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <cc1:ValidatorCalloutExtender ID="vcMRNNo" TargetControlID="rfv1" runat="server">
        </cc1:ValidatorCalloutExtender>
        <asp:RangeValidator ID="rv1" ControlToValidate="txtTRNNUMBer" runat="server" ErrorMessage="Only numeric value allowed"
            Display="Dynamic" MaximumValue="1000000" MinimumValue="1" Type="Integer" ValidationGroup="M1"
            SetFocusOnError="True"></asp:RangeValidator>
        <cc1:ValidatorCalloutExtender ID="vcrv1" TargetControlID="rv1" runat="server">
        </cc1:ValidatorCalloutExtender>
        <asp:RangeValidator ID="rvlr" runat="server" ControlToValidate="txtLRDate" Display="Dynamic"
            ErrorMessage="Invalid LR Date" Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
        <cc1:ValidatorCalloutExtender ID="vclr" TargetControlID="rvlr" runat="server">
        </cc1:ValidatorCalloutExtender>
        <cc1:CalendarExtender ID="celr" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLRDate">
        </cc1:CalendarExtender>
 <%--   </ContentTemplate>
</asp:UpdatePanel>--%>
