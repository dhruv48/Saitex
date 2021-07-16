<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReturnAgstPO.ascx.cs"
    Inherits="Module_Inventory_Controls_ReturnAgstPO" %>
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
    .rAlign
    {
        text-align: right;
    }
    .style1
    {
        height: 47px;
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
                                    ImageUrl="~/CommonImages/clear.png"></asp:ImageButton>
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
                    <b class="titleheading">Item Return Against Purchase Order</b>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="100%">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label15" runat="server" Text="Challan Number : " CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td class="style1" width="15%">
                                <asp:TextBox ID="txtChallanNumber" runat="server" ValidationGroup="M1" Width="150px"
                                    TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" AutoPostBack="True"
                                    OnTextChanged="txtChallanNumber_TextChanged"></asp:TextBox>
                                <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                                    OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="150px" Height="200px"
                                    MenuWidth="500px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            MRN #</div>
                                        <div class="header c2">
                                            MRN Date</div>
                                        <div class="header c4">
                                            Department</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container6" Text='<%# Eval("TRN_DATE") %>' />
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
                                <asp:Label ID="Label16" runat="server" Text="Return Date : " CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="style1" width="15%">
                                <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="150px"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Return Shift : "></asp:Label>
                            </td>
                            <td class="style1" width="25%">
                                <asp:DropDownList ID="ddlIssueShift" Width="150px" runat="server" TabIndex="2" CssClass=" SmallFont">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label1" runat="server" Text="Party :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="150px" MenuWidth="800px" Height="200px">
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
                            <td class="tdRight" width="15%">
                                <asp:TextBox ID="txtPartyCode1" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdLeft" colspan="1" style="width: 15%">
                                <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="15%">
                                <asp:Label ID="Label2" runat="server" CssClass="Label SmallFont" Text="Location : "></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:DropDownList ID="ddlLocation" Width="150px" runat="server" TabIndex="2" CssClass=" SmallFont">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label7" runat="server" Text="Document Number :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtDocNo" runat="server" TabIndex="14" Width="150px" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label8" runat="server" Text="Doc. Date" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtDocDate" runat="server" TabIndex="15" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label9" runat="server" Text="Store :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="25%">
                                 <asp:DropDownList ID="ddlStore" Width="150px" runat="server" TabIndex="2" CssClass=" SmallFont">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="15%">
                                <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" colspan="3" valign="top" width="55%">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="90%" TabIndex="21" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="15%">
                                <asp:Label ID="Label3" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="98%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            <td>
                                Item Code
                            </td>
                            <td>
                                Adj Recpt
                            </td>
                            <td>
                                Qty
                            </td>
                            <td>
                                Rate
                            </td>
                            <td>
                                Amount
                            </td>
                            <td>
                              <%--  Cost Code--%>
                            </td>
                            <td>
                              <%--  Mac Code--%>
                            </td>
                            <td>
                                Remarks
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc2:ComboBox ID="txtICODE" runat="server" CssClass="SmallFont" EmptyText="select..."
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="100px"
                                    EnableVirtualScrolling="true" OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                                    Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            PO Numb</div>
                                        <div class="header c2">
                                            Item Code</div>
                                        <div class="header c4">
                                            Description</div>
                                        <div class="header c2 rAlign">
                                            Bal Apr Qty</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("PO_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ITEM_CODE") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ITEM_DESC") %>' />
                                        </div>
                                        <div class="item c2 rAlign">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("REMQTY") %>' />
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
                                <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                                    Text="Adj.Recpt." Width="65px" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="77px" OnTextChanged="txtQTY_TextChanged1" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="75px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="73px"></asp:TextBox>
                            </td>
                            <td  >
                                <asp:DropDownList ID="ddlCostCode" visible="false" CssClass="SmallFont" runat="server" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                            <td >
                                <cc2:ComboBox ID="ddlMacCode" runat="server" visible="false" CssClass="SmallFont" EmptyText="select..."
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="650px" Width="50px"
                                    EnableVirtualScrolling="true" OnLoadingItems="ddlMacCode_LoadingItems" OnSelectedIndexChanged="ddlMacCode_SelectedIndexChanged"
                                    Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Mac Code</div>
                                        <div class="header c2">
                                            Mac Group</div>
                                        <div class="header c3 ">
                                            Mac Segement</div>
                                        <div class="header c3">
                                            Mac Type</div>
                                        <div class="header c3 ">
                                            Mac Section</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MACHINE_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("MACHINE_GROUP") %>' />
                                        </div>
                                        <div class="item c3 ">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("MACHINE_SEGMENT") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("MACHINE_TYPE") %>' />
                                        </div>
                                        <div class="item c3 ">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("MACHINE_SEC") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:TextBox ID="txtMacCode" runat="server" visible="false" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="77px" MaxLength="6" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="138px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save"  Width="60px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <span lang="en-us">PO Numb:<asp:TextBox ID="txtPO_NUMB" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px"></asp:TextBox>
                                    Item Code/ Desc:<asp:TextBox ID="txtItemCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="100px"></asp:TextBox>
                                    /<asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="150px"></asp:TextBox>
                                    &nbsp; UOM:
                                    <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="60px"></asp:TextBox>
                                </span>
                                <asp:TextBox ID="txtQtyUnit" Visible="false"   runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="1px"></asp:TextBox>
                                <asp:TextBox ID="txtQtyWeight"  runat="server" Visible="false" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="1px"></asp:TextBox>
                                <asp:Label ID="lblPO_BRANCH" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPO_TYPE" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPO_COMP" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPO_Year" runat="server" Visible="False"></asp:Label>
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
                        <asp:GridView ID="grdMaterialItemIssue" runat="server" AutoGenerateColumns="False"
                            CssClass="SmallFont" Width="99%" ShowFooter="false" OnRowCommand="grdMaterialItemIssue_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="PO Numb">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPONUMB" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="I.Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_CODE") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'
                                            ReadOnly="True" Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No of Unit" Visible="false" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQtyUNIT" runat="server" CssClass="Label SmallFont" Visible="false" Text='<%# Bind("NO_OF_UNIT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM of Unit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQtyUOM" runat="server" CssClass="Label SmallFont" Visible="false" Text='<%# Bind("UOM_OF_UNIT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight Of Unit" HeaderStyle-HorizontalAlign="Right" Visible="false"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQtyWeight" runat="server" CssClass="Label SmallFont" Visible="false" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("BASIC_RATE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("AMOUNT") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="txtCostCode" runat="server" Visible="false" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("COST_CODE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mac Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="txtMacCode" runat="server" Visible="false" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("MAC_CODE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDetRemarks" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("REMARKS") %>' Width="120px"></asp:Label>
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
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="ceIssueDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtIssueDate">
        </cc1:CalendarExtender>
        <asp:RangeValidator ID="rv1" runat="server" ControlToValidate="txtChallanNumber"
            Display="None" ErrorMessage="Only numeric value allowed" MaximumValue="1000000"
            MinimumValue="1" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
        <cc1:ValidatorCalloutExtender ID="vcrv1" runat="server" TargetControlID="rv1">
        </cc1:ValidatorCalloutExtender>
        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtChallanNumber"
            Display="None" ErrorMessage="MRN number required" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <cc1:CalendarExtender ID="ceDoc" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDocDate">
        </cc1:CalendarExtender>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>