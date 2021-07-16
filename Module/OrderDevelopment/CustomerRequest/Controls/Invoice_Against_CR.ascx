<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Invoice_Against_CR.ascx.cs"
    Inherits="Module_OrderDevelopment_CustomerRequest_Controls_Invoice_Against_CR" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
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
        margin-left: 8px;
        width: 100px;
    }
    .c3
    {
        margin-left: 8px;
        width: 100px;
    }
    .c4
    {
        margin-left: 8px;
        width: 100px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
    .panelclass
    {
        border-color: Red;
        border-style: solid;
        border-width: 2;
        background-color: Gray;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial" width="80%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                    ValidationGroup="M1" TabIndex="22" OnClick="imgbtnSave_Click" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" TabIndex="23"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    TabIndex="24" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    TabIndex="25" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    TabIndex="26" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                    TabIndex="27"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><b>Invoice</b></span>
                </td>
            </tr>
            <tr>
                <td class="td" align="left" valign="top">
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label8" runat="server" Text="Invoice Type:" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="SmallFont" Width="150px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="Sale Work" Value="SALEWORK"></asp:ListItem>
                                    <asp:ListItem Text="Job Work" Value="JOBWORK"></asp:ListItem>
                                    <asp:ListItem Text="FROM STOCK" Value="FROM STOCK"></asp:ListItem>
                                </asp:DropDownList>
                                <td class="tdRight" width="15%">
                                    <asp:Label ID="Label6" runat="server" Text="Challan No. : " CssClass="Label tdRight SmallFont"
                                        Width="100%"></asp:Label>
                                </td>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                                    OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="80%" Height="200px"
                                    EmptyText="Select Challan No" MenuWidth="350px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Challan No #</div>
                                        <div class="header c2">
                                            Challan Date</div>
                                        <div class="header c4">
                                            Department</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container6" Text='<%# Eval("TRN_DATE","{0:dd/MM/yyyy}") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Literal5" Text='<%# Eval("DEPT_NAME") %>' />
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
                                <asp:Label ID="Label16" runat="server" Text="Invoice Date : " CssClass="Label tdRight SmallFont"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtInvoiceDate" runat="server" TabIndex="2" ValidationGroup="M1"
                                    Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                <cc3:CalendarExtender ID="ceInvoiceDate" runat="server" TargetControlID="txtInvoiceDate"
                                    Format="dd/MM/yyyy">
                                </cc3:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label12" runat="server" Text="Buyer's PO No :" CssClass="LabelNo SmallFont"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtBuyerorder" runat="server" TabIndex="14" Width="99%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label15" runat="server" Text="Invoice No : " CssClass="LabelNo SmallFont"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtInvoiceNumber" runat="server" ValidationGroup="M1" Width="77%"
                                    TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                                <cc2:ComboBox ID="ddlInvoiceNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlInvoiceNumber_LoadingItems" DataTextField="INVOICE_NUMB" DataValueField="INVOICE_NUMB"
                                    OnSelectedIndexChanged="ddlInvoiceNumber_SelectedIndexChanged" Width="80%" Height="200px"
                                    EmptyText="Select Invoice No" MenuWidth="500px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Invoice No #</div>
                                        <div class="header c2">
                                            Invoice Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("INVOICE_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container6" Text='<%# Eval("INVOICE_DATE","{0:dd/MM/yyyy}") %>' />
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
                                <asp:Label ID="Label10" runat="server" CssClass="Label SmallFont " Text="Invoice Time : "></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">

                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="tdRight"" " width="15%">
                                <asp:Label ID="Label4" runat="server" CssClass="LabelNo" Text="Party Detail :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="cmbParty" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="cmbParty_LoadingItems" DataTextField="PRTY_CODE" DataValueField="PRTY_NAME"
                                    OnSelectedIndexChanged="cmbParty_SelectedIndexChanged" Width="99%" Height="200px"
                                    MenuWidth="550px" EnableVirtualScrolling="true" EmptyText="Select Party">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c6">
                                            NAME</div>
                                        <%-- <div class="header c4">
                                            Address</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c6">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        <%-- <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("Address") %>' /></div>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" colspan="2">
                                <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="25%"></asp:TextBox>
                                <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="85%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label7" runat="server" CssClass="Label SmallFont " Text="Inv. Removal Date: "></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtDateOfRemoval" runat="server" TabIndex="2" ValidationGroup="M1"
                                    Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                <cc3:CalendarExtender ID="ceDateOfRemoval" runat="server" TargetControlID="txtDateOfRemoval"
                                    Format="dd/MM/yyyy">
                                </cc3:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="tdRight" width="15%">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged" Width="99%"
                                    Height="200px" MenuWidth="550px" EnableVirtualScrolling="true" EmptyText="Select Transporter">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c4">
                                            NAME</div>
                                        <%-- <div class="header c4">
                                            Address</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                        <%-- <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("Address") %>' /></div>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="70%" colspan="2">
                                <asp:TextBox ID="lblTransporterCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="25%"></asp:TextBox>
                                <asp:TextBox ID="txtTransporterAddress" TabIndex="5" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="85%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label11" runat="server" CssClass="Label SmallFont " Text="Eway Bill No. : "></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                   <asp:TextBox ID="txtEwayBillNo" runat="server" TabIndex="16" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="tdRight" width="15%">
                                <asp:Label ID="Label2" runat="server" CssClass="LabelNo" Text="Agent Code :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="txtConsigneeCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtConsigneeCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EmptyText="Select Consignee" OnSelectedIndexChanged="txtConsigneeCode_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="99%" MenuWidth="800px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c4">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="70%" colspan="2">
                                <asp:TextBox ID="lblConsigneeCode" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="25%"></asp:TextBox>
                                <asp:TextBox ID="txtConsigneeAddress" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="85%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label9" runat="server" Text="Vehicle No :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label5" runat="server" Text="LR No :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtLRNumber" runat="server" TabIndex="15" Width="99%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label3" runat="server" CssClass="Label SmallFont " Text="Shift : "
                                    Visible="false"></asp:Label>
                            </td>
                            <td class="tdLeft" width="10%">
                                <asp:DropDownList ID="ddlIssueShift" runat="server" TabIndex="2" Width="150px" Visible="false">
                                </asp:DropDownList>
                            </td>
                            <%--<td class="tdRight" width="15%">
                                <asp:Label ID="Label6" runat="server" Text="LR Date :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>--%>
                            <%--       <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="99%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="85%" TabIndex="21" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>--%>
                            <td>
                                <asp:TextBox ID="txtPINO" runat="server" Visible="false"></asp:TextBox>
                               <asp:TextBox ID="txtChallanNo" runat="server" Visible="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            <td width="10%">
                                Yarn&nbsp;Details
                            </td>
                            <td width="10%">
                                Description
                            </td>
                            <td align="right" width="10%">
                                Lot&nbsp;Number
                            </td>
                            <td align="right" width="10%">
                                Shade
                            </td>
                            <td align="right" width="10%">
                                D/C&nbsp;No
                            </td>
                            <td align="right" width="10%">
                                No&nbsp;of&nbsp;Carton
                            </td>
                            <td align="right" width="10%">
                                Total&nbsp;Qty(In&nbsp;Kgs)
                            </td>
                            <td align="right" width="10%">
                                Rate
                            </td>
                            <td align="right" width="10%">
                                Assessable&nbsp;Value
                            </td>
                            <td width="8%">
                            </td>
                        </tr>
                        <tr>
                            <td width="10%">
                                <cc2:ComboBox ID="ddlYarnDetails" runat="server" CssClass="SmallFont" AutoPostBack="TRUE"
                                    EnableLoadOnDemand="true" MenuWidth="750px" Width="99%" OnLoadingItems="ddlYarnDetails_LoadingItems"
                                    OnSelectedIndexChanged="ddlYarnDetails_SelectedIndexChanged" Height="200px" EmptyText="Select Details">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            DC No</div>
                                        <div class="header c2">
                                            Yarn
                                        </div>
                                        <div class="header c2">
                                            Shade</div>
                                        <div class="header c2">
                                            Packages</div>
                                        <div class="header c2">
                                            Qty</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("TRN_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("YARN_DESC") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal8" Text='<%# Eval("SHADE_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("NO_OF_UNIT") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("TRN_QTY") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:Label ID="lblYarnCode" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtYarnDesc" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtLotNo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont "
                                    Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtShade" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtDCNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtNoOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="100%"></asp:TextBox>
                            </td>
                            <td width="10%" runat="server">
                                <asp:TextBox ID="txtRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%" align="center">
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save" Width="55px" />
                                <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                                    Width="55px" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <asp:GridView ID="grdInvoice" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                        Width="99%" ShowFooter="false" OnRowCommand="grdInvoice_RowCommand" OnRowDataBound="grdInvoice_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="SR&nbsp;No">
                                <ItemTemplate>
                                    <asp:Label ID="lblSR_NO" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UNIQUEID") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Yarn Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblYARN_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                        ToolTip='<%# Bind("YARN_CODE") %>' Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lot&nbsp;No">
                                <ItemTemplate>
                                    <asp:Label ID="lblLOT_NO" runat="server" CssClass="Label SmallFont" Text='<%# Bind("LOT_NO") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shade">
                                <ItemTemplate>
                                    <asp:Label ID="lblSHADE_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="D/C&nbsp;No">
                                <ItemTemplate>
                                    <asp:Label ID="lblDOC_NO" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DOC_NO") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cartons">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblNO_OF_UNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'       Width="100px" ></asp:Label>--%>
                                    <asp:LinkButton ID="lblNO_OF_UNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:LinkButton>
                                    <asp:Panel ID="pnlrecadj" runat="server" CssClass="panelclass">
                                        <asp:GridView ID="grdRecAdj" runat="server" AutoGenerateColumns="False" CssClass="SmallFont">
                                            <Columns>
                                                <asp:BoundField HeaderText="Lot&nbsp;No" DataField="LOT_NO" />
                                                <asp:BoundField HeaderText="Grade" DataField="GRADE" />
                                                <asp:BoundField HeaderText="Carton&nbsp;No" DataField="CARTON_NO" />
                                                <asp:BoundField HeaderText="Issued&nbsp;Qty" DataField="ISSUE_QTY" />
                                            </Columns>
                                            <RowStyle CssClass="SmallFont" />
                                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <cc3:HoverMenuExtender ID="hvrmnrecadj" runat="server" PopDelay="500" PopupControlID="pnlrecadj"
                                        TargetControlID="lblNO_OF_UNIT" PopupPosition="Left">
                                    </cc3:HoverMenuExtender>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total&nbsp;Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblQUANTITY" runat="server" CssClass="Label SmallFont" Text='<%# Bind("QUANTITY") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblRATE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("RATE") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAMOUNT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("AMOUNT") %>'
                                        Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tax Details">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnTrnNumb" Text="Tax Details" runat="server" CssClass="LabelNo SmallFont"></asp:LinkButton>
                                    <asp:Panel ID="pnlTaxDetails" runat="server" Width="30%" CssClass="panelclass">
                                        <asp:GridView ID="grdTaxDetails" runat="server" Width="100%" AutoGenerateColumns="False"
                                            CssClass="SmallFont">
                                            <Columns>
                                                <asp:BoundField HeaderText="Component Name" DataField="COMPO_CODE" />
                                                <asp:BoundField HeaderText="Component Name" DataField="TAX_RATE" />
                                                <asp:BoundField HeaderText="Component Name" DataField="TAX_AMOUNT" />
                                                <%--<asp:TemplateField HeaderText="Component Name" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComponentCode" runat="server" CssClass="SmallFont Label" Text='<%# Bind("COMPO_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" runat="server" CssClass="SmallFont Label" Text='<%# Bind("TAX_RATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" CssClass="SmallFont Label" Text='<%# Bind("TAX_AMOUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <RowStyle CssClass="SmallFont" />
                                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <cc3:HoverMenuExtender ID="hvrTaxDetails" runat="server" PopDelay="500" PopupControlID="pnlTaxDetails"
                                        TargetControlID="lnkbtnTrnNumb" PopupPosition="Left">
                                    </cc3:HoverMenuExtender>
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
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
