<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StockTransferIssueSW.ascx.cs"
    Inherits="Module_Sewing_Thread_Controls_StockTransferIssueSW" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;
        }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 120px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<table class="tdMain" width="900px">
    <tr>
        <td class="td" width="100%">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="M1" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" />
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/CommonImages/del6.png"
                            OnClick="imgbtnDelete_Click" ToolTip="Delete" />
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                            OnClick="imgbtnFind_Click" ToolTip="Find" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" ToolTip="Exit" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" ToolTip="Help" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Sewing Thread Stock Transfer Issue</b>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft" width="100%">
            <span class="Mode">&nbsp;You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                &nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label15" runat="server" CssClass="LabelNo SmallFont" Text="Challan Number : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtChallanNumber" runat="server" AutoPostBack="True" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            OnTextChanged="txtChallanNumber_TextChanged" TabIndex="1" ValidationGroup="M1"
                            Width="80px"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" DataTextField="TRN_NUMB"
                            DataValueField="TRN_NUMB" EnableLoadOnDemand="true" Height="200px" MenuWidth="500px"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                            Width="85px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    MRN #</div>
                                <div class="header c1">
                                    MRN Date</div>
                                <div class="header c3">
                                    Department</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container4" runat="server" Text='<%# Eval("TRN_NUMB") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Container6" runat="server" Text='<%# Eval("TRN_DATE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal5" runat="server" Text='<%# Eval("DEPT_NAME") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                DisplayingDisplaying items items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out
                                of out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label16" runat="server" CssClass="Label SmallFont" Text="Issue Date : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtIssueDate" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            TabIndex="2" ValidationGroup="M1" Width="80px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Issue Shift : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:DropDownList ID="ddlIssueShift" runat="server" MenuWidth="200px" TabIndex="2"
                            Width="90px" CssClass="SmallFont">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label1" runat="server" CssClass="Label SmallFont" Text="Department :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="txtDepartment" runat="server" AppendDataBoundItems="true" CssClass="SmallFont"
                            MenuWidth="200px" TabIndex="6" Width="90px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label7" runat="server" CssClass="LabelNo SmallFont" Text="Document Number :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="TextBoxNo SmallFont" TabIndex="14"
                            Width="80px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label8" runat="server" CssClass="Label SmallFont" Text="Doc. Date"></asp:Label>
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:TextBox ID="txtDocDate" runat="server" CssClass="TextBox SmallFont" TabIndex="15"
                            Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label2" runat="server" CssClass="Label SmallFont" Text="Reprocess :"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:DropDownList ID="ddlReprocess" runat="server" CssClass="TextBox" Width="90px">
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label9" runat="server" CssClass="Label SmallFont" Text="Vehicle/Lorry No. :"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="TextBox SmallFont" TabIndex="16"
                            Width="80px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label3" runat="server" CssClass="LabelNo SmallFont" Text="Receiving Branch:"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:DropDownList ID="ddlDelAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                            CssClass="SmallFont TextBox" OnSelectedIndexChanged="ddlDelAdd_SelectedIndexChanged"
                            Width="75px">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtDelAddress" runat="server" CssClass="SmallFont gCtrTxt" TabIndex="8"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label14" runat="server" CssClass="Label SmallFont" Text="Remarks :"></asp:Label>
                    </td>
                    <td align="left" colspan="5" valign="top" width="85%">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" TabIndex="21"
                            Width="98%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="98%">
                <tr bgcolor="#336699" class="SmallFont titleheading">
                    <td class="tdLeft" width="20%">
                        SW Code
                    </td>
                    <td class="tdLeft" width="10%">
                        Adj Rec
                    </td>
                    <td class="tdCenter" width="10%">
                        Unit Weight
                    </td>
                    <td class="tdRight" width="10%">
                        No Of Unit
                    </td>
                    <td class="tdRight" width="10%">
                        Qty
                    </td>
                    <td class="tdLeft" width="10%">
                        Cost Code
                    </td>
                    <td class="tdLeft" width="20%">
                        Remarks
                    </td>
                    <td class="tdLeft" width="10%">
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft" width="20%">
                        <cc2:ComboBox ID="txtICODE" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            EmptyText="select..." EnableLoadOnDemand="true" Height="200px" MenuWidth="880px"
                            OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                            Width="99%">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Sewing Thread Code</div>
                                <div class="header c4">
                                    Description</div>
                                <div class="header c2">
                                    Shade</div>
                                <div class="header c1">
                                    Qty</div>
                                <div class="header c1">
                                    Rem Qty</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_CODE") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("YARN_DESC") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("SHADE_CODE") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("TRN_QTY") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("REMQTY") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                DisplayingDisplaying items items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out
                                of out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdLeft" width="10%">
                        <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                            Text="adj. Rec." Width="99%" />
                    </td>
                    <td class="tdRight" width="10%">
                        <asp:TextBox ID="txtQtyWeight" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="99%"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="10%">
                        <asp:TextBox ID="txtQtyUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="99%"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="10%">
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            OnTextChanged="txtQTY_TextChanged1" ReadOnly="True" Width="99%"></asp:TextBox>
                    </td>
                    <td class="tdLeft" width="10%">
                        <asp:TextBox ID="txtCostCode" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                    </td>
                    <td class="tdLeft" width="20%">
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                    </td>
                    <td class="tdLeft" width="10%">
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7" class="tdLeft" width="90%">
                                                Code/Desc:<asp:TextBox ID="txtSWCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="80px"></asp:TextBox>
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="140px"></asp:TextBox>
                        &nbsp;Shade:<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="61px"></asp:TextBox>
                        &nbsp;UOM:<asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="55px"></asp:TextBox>
                        &nbsp;Rate:<asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="65px"></asp:TextBox>
                        &nbsp;Amount:<asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="80px"></asp:TextBox>
                    </td>
                    <td class="tdLeft" width="10%">
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
                <asp:GridView ID="grdMaterialItemIssue" runat="server" AutoGenerateColumns="False"
                    CssClass="SmallFont" OnRowCommand="grdMaterialItemIssue_RowCommand" ShowFooter="false"
                    Width="99%">
                    <Columns>
                        <asp:TemplateField HeaderText="SW Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("YARN_DESC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade">
                            <ItemTemplate>
                                <asp:Label ID="txtSHADECODE" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtNoofUnit" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Weight">
                            <ItemTemplate>
                                <asp:Label ID="txtUnitweight" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit UOM">
                            <ItemTemplate>
                                <asp:Label ID="txtUNITUOM" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" ReadOnly="True"
                                    Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtRate" runat="server" CssClass="LabelNo SmallFont" ReadOnly="True"
                                    Text='<%# Bind("BASIC_RATE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                    Text='<%# Bind("AMOUNT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost Code">
                            <ItemTemplate>
                                <asp:Label ID="txtCostCode" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("COST_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("REMARKS") %>'></asp:Label>
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
            </asp:Panel>
        </td>
    </tr>
</table>
<cc1:CalendarExtender ID="ceIssueDate" runat="server" TargetControlID="txtIssueDate">
</cc1:CalendarExtender>
<asp:RangeValidator ID="rv1" runat="server" ControlToValidate="txtChallanNumber"
    Display="None" ErrorMessage="Only numeric value allowed" MaximumValue="1000000"
    MinimumValue="1" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
<cc1:ValidatorCalloutExtender ID="vcrv1" runat="server" TargetControlID="rv1">
</cc1:ValidatorCalloutExtender>
<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtChallanNumber"
    Display="None" ErrorMessage="MRN number required" ValidationGroup="M1"></asp:RequiredFieldValidator>
<cc1:CalendarExtender ID="ceDoc" runat="server" TargetControlID="txtDocDate">
</cc1:CalendarExtender>
