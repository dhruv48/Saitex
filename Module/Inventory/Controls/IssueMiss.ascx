<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IssueMiss.ascx.cs" Inherits="Module_Inventory_Controls_IssueMiss" %>
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
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">    <ContentTemplate>--%>
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
            <b class="titleheading">Material Issue Misc.</b>
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
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtChallanNumber" runat="server" ValidationGroup="M1" Width="150px"
                            TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" AutoPostBack="True"
                            OnTextChanged="txtChallanNumber_TextChanged"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            EnableVirtualScrolling="true" OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB"
                            DataValueField="TRN_NUMB" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                            Width="150px" Height="200px" MenuWidth="500px">
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
                        <asp:Label ID="Label16" runat="server" Text="Issue Date : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="150px"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                     <td class="tdRight" width="15%">
                        <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Shift : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:DropDownList ID="ddlIssueShift" Width="150px" runat="server" TabIndex="2" CssClass=" SmallFont">
                        </asp:DropDownList>
                    </td>
                  
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label4" runat="server" Text="From Location :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlFromLocation" CssClass="SmallFont" AppendDataBoundItems="true"
                            Width="150px" runat="server" TabIndex="6">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label2" runat="server" Text="Reprocess :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlReprocess" Width="150px" runat="server" CssClass="TextBox">
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                      <td class="tdRight" width="15%">
                        <asp:Label ID="Label3" runat="server" Text="To Department : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="25%">
                           <asp:DropDownList ID="txtDepartment" CssClass="SmallFont" AppendDataBoundItems="true"
                            Width="150px" runat="server" TabIndex="6">
                        </asp:DropDownList>
                    </td>
                   
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label5" runat="server" Text="From Store :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlFromStore" Width="150px" runat="server" CssClass="TextBox">                           
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                      <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No.:" CssClass="Label SmallFont"></asp:Label>  
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                     <td class="tdRight" width="15%">
                        <asp:Label ID="Label10" runat="server" CssClass="Label SmallFont" Text="To Location : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:DropDownList ID="ddlToLocation" Width="150px" runat="server" TabIndex="2" CssClass=" SmallFont">
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
                        <asp:Label ID="Label6" runat="server" CssClass="Label SmallFont" Text="To Store: "></asp:Label>
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:DropDownList ID="ddlToStore" Width="150px" runat="server" TabIndex="2" CssClass=" SmallFont">
                        </asp:DropDownList>
                    </td>
                   
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" colspan="5" valign="top" width="85%">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="85%" TabIndex="21" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        
                    </td>
                    <td align="left" valign="top" width="25%">
                        
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
                        Cost Code
                    </td>
                    <td>
                        Mac Code
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
                                    Item Code</div>
                                <div class="header c4">
                                    Description</div>
                                <div class="header c2 rAlign">
                                    Bal Apr Qty</div>
                            </HeaderTemplate>
                            <ItemTemplate>
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
                    <td>
                        <asp:DropDownList ID="ddlCostCode" CssClass="SmallFont" runat="server" AppendDataBoundItems="True">
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txtCostCode" runat="server" CssClass="TextBox SmallFont" Width="76px"></asp:TextBox>--%>
                    </td>
                    <td>
                        <cc2:ComboBox ID="ddlMacCode" runat="server" CssClass="SmallFont" EmptyText="select..."
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
                        <asp:TextBox ID="txtMacCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="77px" MaxLength="6" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="138px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" Style="width: 42px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <span lang="en-us">Item Code/ Desc:<asp:TextBox ID="txtItemCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="100px"></asp:TextBox>
                            /<asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                ReadOnly="true" Width="150px"></asp:TextBox>
                            &nbsp; UOM:
                            <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                ReadOnly="true" Width="60px"></asp:TextBox>
                            &nbsp;</span>
                        <asp:TextBox ID="txtQtyUnit" Visible="false" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="75px"></asp:TextBox>
                        <asp:TextBox ID="txtQtyWeight" runat="server" Visible="false" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="75px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                            Text="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                <asp:GridView ID="grdMaterialItemIssue" runat="server" AutoGenerateColumns="False"
                    CssClass="SmallFont" Width="99%" ShowFooter="false" 
                    OnRowCommand="grdMaterialItemIssue_RowCommand" 
                    onrowdatabound="grdMaterialItemIssue_RowDataBound">
                    <Columns>
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
                        <asp:TemplateField HeaderText="No of Unit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
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
                        <asp:TemplateField HeaderText="Weight Of Unit" HeaderStyle-HorizontalAlign="Right"
                            ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="txtQtyWeight" runat="server" CssClass="Label SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
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
                                <cc1:HoverMenuExtender ID="hmeRecAdj" runat="server" PopupControlID="pnlRecAdj" TargetControlID="txtQTY"
                                    PopupPosition="Left">
                                </cc1:HoverMenuExtender>
                                <asp:Panel ID="pnlRecAdj" runat="server" BackColor="#C5E7F1" BorderColor="Desktop"
                                    BorderStyle="Solid" BorderWidth="5px" HorizontalAlign="Left">
                                    <asp:GridView ID="grdRecAdj" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Rec Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecYear" runat="server" CssClass="SmallFont Label" Text='<%# Bind("YEAR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rec Comp">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecComp" runat="server" CssClass="SmallFont Label" Text='<%# Bind("COMP_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rec Branch">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecBranch" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BRANCH_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rec Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecType" runat="server" CssClass="SmallFont Label" Text='<%# Bind("TRN_TYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rec No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecNumb" runat="server" CssClass="SmallFont Label" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Comp">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecPoComp" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PO_COMP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Branch">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecPoBranch" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PO_BRANCH") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecPoType" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PO_TYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PO Numb">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecPoNumb" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PO_NUMB") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issue Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIssueQty" runat="server" CssClass="SmallFont Label" Text='<%# Bind("ISSUE_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PA No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRecPaNo" runat="server" CssClass="SmallFont Label" Text='<%# Bind("PI_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                </asp:Panel>
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
                        <asp:TemplateField HeaderText="Cost Code">
                            <ItemTemplate>
                                <asp:Label ID="txtCostCode" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                    Text='<%# Bind("COST_CODE") %>' Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mac Code">
                            <ItemTemplate>
                                <asp:Label ID="txtMacCode" runat="server" ReadOnly="True" CssClass="Label SmallFont"
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
                <asp:Label ID="lblPO_BRANCH" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblPO_TYPE" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblPO_COMP" runat="server" Visible="false"></asp:Label>
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
<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtIssueDate"
    Display="None" ErrorMessage="Issue Date Required" ValidationGroup="M1"></asp:RequiredFieldValidator>
<%--  </ContentTemplate>
</asp:UpdatePanel>--%>