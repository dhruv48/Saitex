<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SWFinishGudIssueMicsc.ascx.cs"
    Inherits="Module_SewingThread_Controls_SWFinishGudIssueMicsc" %>
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
        width: 160px;
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
    .c7
    {
        margin-left: 4px;
        width: 300px;
    }
    .SmallFont
    {
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table class="tdMain" width="800px">
    <tr>
        <td class="td" width="100%">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="M1" Style="width: 48px" />
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
            <b class="titleheading">Sewing Thread Finish Goods Issue Misc.</b>
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
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label15" runat="server" CssClass="LabelNo SmallFont" Text="Challan Number : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtChallanNumber" runat="server" AutoPostBack="True" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            OnTextChanged="txtChallanNumber_TextChanged" TabIndex="1" ValidationGroup="M1"
                            Width="80px"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" DataTextField="TRN_NUMB"
                            DataValueField="TRN_NUMB" EnableLoadOnDemand="true" Height="200px" MenuWidth="500px"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                            Width="85px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    MRN #MRN #</div>
                                <div class="header c2">
                                    MRN DateMRN Date</div>
                                <div class="header c4">
                                    Department</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container4" runat="server" Text='<%# Eval("TRN_NUMB") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container6" runat="server" Text='<%# Eval("TRN_DATE") %>' />
                                </div>
                                <div class="item c4">
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
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label16" runat="server" CssClass="Label SmallFont" Text="Issue Date : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtIssueDate" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            TabIndex="2" ValidationGroup="M1" Width="80px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Issue Shift : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlIssueShift" runat="server" MenuWidth="200px" TabIndex="2"
                            Width="90px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label1" runat="server" CssClass="Label SmallFont" Text="Department :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:DropDownList ID="txtDepartment" runat="server" AppendDataBoundItems="true" CssClass="SmallFont"
                            MenuWidth="200px" TabIndex="6" Width="90px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label7" runat="server" CssClass="LabelNo SmallFont" Text="Document Number :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="TextBoxNo SmallFont" TabIndex="14"
                            Width="80px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label8" runat="server" CssClass="Label SmallFont" Text="Doc. Date"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtDocDate" runat="server" CssClass="TextBox SmallFont" TabIndex="15"
                            Width="80px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label2" runat="server" CssClass="Label SmallFont" Text="Reprocess :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:DropDownList ID="ddlReprocess" runat="server" CssClass="TextBox" Width="90px">
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label9" runat="server" CssClass="Label SmallFont" Text="Vehicle/Lorry No. :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="TextBox SmallFont" TabIndex="16"
                            Width="80px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        &nbsp;
                    </td>
                    <td class="tdLeft" width="15%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label14" runat="server" CssClass="Label SmallFont" Text="Remarks :"></asp:Label>
                    </td>
                    <td class="tdLeft" colspan="5">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" TabIndex="21"
                            Width="99%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="99%">
                <tr bgcolor="#336699" class="SmallFont titleheading">
                    <td width="15%">
                        SW Code
                    </td>
                    <td width="8%">
                        Adj.Recpt
                    </td>
                    <td width="9%">
                        Total Unit
                    </td>
                    <td width="9%">
                        Weight Of Unit
                    </td>
                    <td width="9%" align="right">
                        Quantity
                    </td>
                    <td width="9%" align="right">
                        Rate
                    </td>
                    <td width="9%">
                        &nbsp;Cost Code
                    </td>
                    <td width="9%">
                        Mac Code
                    </td>
                    <td width="15%">
                        Remarks
                    </td>
                    <td width="8%">
                    </td>
                </tr>
                <tr>
                    <td width="15%">
                        <cc2:ComboBox ID="txtICODE" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="800px" EnableVirtualScrolling="true"
                            OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                            Width="99%">
                            <HeaderTemplate>
                                <div class="header c3">
                                    Yarn Code</div>
                                <div class="header c7">
                                    Description</div>
                                <div class="header c3">
                                    Shade</div>
                                <div class="header c2 rAlign">
                                    Bal Apr Qty</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c3">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_CODE") %>' />
                                </div>
                                <div class="item c7">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("YARN_DESC") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("SHADE_CODE") %>' />
                                </div>
                                <div class="item c2 rAlign">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("REMQTY") %>' />
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
                    <td width="8%">
                        <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                            Text="Adj.Recpt" Width="99%" />
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtQtyUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="99%" OnTextChanged="txtQtyUnit_TextChanged"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtQtyWeight" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="99%"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            OnTextChanged="txtQTY_TextChanged1" ReadOnly="True" Width="99%"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="99%"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtCostCode" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtMacCode" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td width="92%" colspan="9">
                        Code :
                        <asp:TextBox ID="txtYarnCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="150px" Font-Size="X-Small"></asp:TextBox>
                        &nbsp;Description :
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="200px"></asp:TextBox>
                        &nbsp; Shade:<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true"></asp:TextBox>
                        &nbsp;UOM :
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="70px"></asp:TextBox>
                        <asp:TextBox ID="txtMaxQty" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="1px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtAmount0" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="1px" Visible="False"></asp:TextBox>
                    </td>
                    <td width="8%">
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
                    CssClass="SmallFont" OnRowCommand="grdMaterialItemIssue_RowCommand" OnRowDataBound="grdMaterialItemIssue_RowDataBound"
                    ShowFooter="false" Width="99%">
                    <Columns>
                        <asp:TemplateField HeaderText="SW.Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("YARN_CODE") %>' Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("YARN_DESC") %>' Width="120px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade">
                            <ItemTemplate>
                                <asp:Label ID="txtSHADE_CODE" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("SHADE_CODE") %>' Width="120px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No of Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtQtyUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                    ReadOnly="True" Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM of Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtQtyUOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'
                                    ReadOnly="True" Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Weight Of Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtQtyWeight" runat="server" CssClass="Label SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                    ReadOnly="True" Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkunige" runat="server" CssClass="Label SmallFont" CommandArgument='<%# Bind("UNIQUEID") %>'
                                    Text='<%# Bind("TRN_QTY") %>' Width="60px"></asp:LinkButton>
                                <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                    BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left" Width="350px" ScrollBars="Auto">
                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="YEAR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE" runat="server" CssClass="SmallFont Label" Text='<%# Bind("YEAR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="COMPANY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE14" runat="server" CssClass="SmallFont Label" Text='<%# Bind("COMP_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BRANCH CODE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE15" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BRANCH_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TRN TYPE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE20" runat="server" CssClass="SmallFont Label" Text='<%# Bind("TRN_TYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO COMPANY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE21" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PO_COMP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO BRANCH">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE22" runat="server" CssClass="SmallFont Label" Text='<%#Bind("PO_BRANCH") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO TYPE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE23" runat="server" CssClass="SmallFont Label" Text='<%#Bind("PO_TYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO NUMBER">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE24" runat="server" CssClass="SmallFont Label" Text='<%#Bind("PO_NUMB") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="YARN CODE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE25" runat="server" CssClass="SmallFont Label" Text='<%#Bind("YARN_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SHADE CODE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE26" runat="server" CssClass="SmallFont Label" Text='<%#Bind("SHADE_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FINAL RATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE27" runat="server" CssClass="SmallFont Label" Text='<%#Bind("FINAL_RATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LOT NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE28" runat="server" CssClass="SmallFont Label" Text='<%#Bind("LOT_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO OF UNIT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE29" runat="server" CssClass="SmallFont Label" Text='<%#Bind("NO_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM OF UNIT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE30" runat="server" CssClass="SmallFont Label" Text='<%#Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WEIGHT OF UNIT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE31" runat="server" CssClass="SmallFont Label" Text='<%#Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PI NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE32" runat="server" CssClass="SmallFont Label" Text='<%#Bind("PI_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ISSUE_QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE33" runat="server" CssClass="SmallFont Label" Text='<%#Bind("ISSUE_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                    </asp:GridView>
                                </asp:Panel>
                                <cc1:HoverMenuExtender ID="hmerecadj" runat="server" TargetControlID="lnkunige" PopupControlID="pnlBOM"
                                    PopupPosition="Top">
                                </cc1:HoverMenuExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtRate" runat="server" CssClass="LabelNo SmallFont" ReadOnly="True"
                                    Text='<%# Bind("BASIC_RATE") %>' Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                    Text='<%# Bind("AMOUNT") %>' Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost Code">
                            <ItemTemplate>
                                <asp:Label ID="txtCostCode" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("COST_CODE") %>' Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mac Code">
                            <ItemTemplate>
                                <asp:Label ID="txtMacCode" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("MAC_CODE") %>' Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("REMARKS") %>' Width="120px"></asp:Label>
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
<cc1:CalendarExtender ID="ceIssueDate" runat="server" TargetControlID="txtIssueDate"
    Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<asp:RangeValidator ID="rv1" runat="server" ControlToValidate="txtChallanNumber"
    Display="None" ErrorMessage="Only numeric value allowed" MaximumValue="1000000"
    MinimumValue="1" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
<cc1:ValidatorCalloutExtender ID="vcrv1" runat="server" TargetControlID="rv1">
</cc1:ValidatorCalloutExtender>
<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtChallanNumber"
    Display="None" ErrorMessage="MRN number required" ValidationGroup="M1"></asp:RequiredFieldValidator>
<cc1:CalendarExtender ID="ceDoc" runat="server" TargetControlID="txtDocDate" Format="dd/MM/yyyy">
</cc1:CalendarExtender>
</ContentTemplate>
</asp:UpdatePanel> 