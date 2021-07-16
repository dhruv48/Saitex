<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Issue.ascx.cs" Inherits="Module_Yarn_SalesWork_Controls_Issue" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; overflow:hidden;white-space:nowrap;}
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
        margin-left: 2px;
        width: 80px;
    }
    .c3
    {
        margin-left: 2px;
        width: 160px;
    }
    .c4
    {
        margin-left: 2px;
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
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
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
            <b class="titleheading">Yarn Issue Misc.</b>
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
                            Width="150px"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" DataTextField="TRN_NUMB"
                            DataValueField="TRN_NUMB" EnableLoadOnDemand="true" Height="200px" MenuWidth="500px"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                             Width="150px">
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
                            TabIndex="2" ValidationGroup="M1"  Width="150px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Issue Shift : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlIssueShift" runat="server" MenuWidth="200px" TabIndex="1"
                             Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label1" runat="server" CssClass="Label SmallFont" Text="From Location :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                     <asp:DropDownList ID="ddlFromLocation" runat="server" AppendDataBoundItems="true" CssClass="SmallFont"
                            MenuWidth="200px" TabIndex="2"  Width="150px">
                        </asp:DropDownList>   
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label7" runat="server" CssClass="LabelNo SmallFont" Text="Document Number :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="TextBoxNo SmallFont" TabIndex="3"
                             Width="150px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                      <asp:Label ID="Label4" runat="server" CssClass="Label SmallFont" Text="To Department :"></asp:Label>  
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="txtDepartment" runat="server" AppendDataBoundItems="true" CssClass="SmallFont"
                            MenuWidth="200px" TabIndex="4"  Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                    <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label3" runat="server" CssClass="Label SmallFont" Text="From Store :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:DropDownList ID="ddlFromStore" runat="server" AppendDataBoundItems="true" CssClass="SmallFont"
                            MenuWidth="200px" TabIndex="5"  Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label8" runat="server" CssClass="Label SmallFont" Text="Doc. Date"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtDocDate" runat="server" CssClass="TextBox SmallFont" TabIndex="6"
                             Width="150px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label5" runat="server" CssClass="Label SmallFont" Text="To Location :"></asp:Label> 
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlToLocation" runat="server" AppendDataBoundItems="true" CssClass="SmallFont"
                            MenuWidth="200px" TabIndex="7"  Width="150px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label2" runat="server" CssClass="Label SmallFont" Text="Reprocess :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:DropDownList ID="ddlReprocess" runat="server" CssClass="TextBox"  Width="150px" TabIndex="8">
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label9" runat="server" CssClass="Label SmallFont" Text="Vehicle/Lorry No. :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="TextBox SmallFont" TabIndex="9"
                             Width="150px"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label6" runat="server" CssClass="Label SmallFont" Text="To Store :"></asp:Label>  
                    </td>
                    <td class="tdLeft" width="15%">
                       <asp:DropDownList ID="ddlToStore" runat="server" AppendDataBoundItems="true" CssClass="SmallFont"
                            MenuWidth="200px" TabIndex="10"  Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label14" runat="server" CssClass="Label SmallFont" Text="Remarks :"></asp:Label>
                    </td>
                    <td class="tdLeft" colspan="5">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" TabIndex="11"
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
                        Yarn Code
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
                                <div class="header c1">
                                    Yarn Code</div>
                                <div class="header c7">
                                    Description</div>
                                <div class="header c2">
                                    Shade Family </div>
                                     <div class="header c3">
                                    Shade</div>
                                <div class="header c1 rAlign">
                                    Bal Apr Qty</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_CODE") %>' />
                                </div>
                                <div class="item c7">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("YARN_DESC") %>' />
                                </div>
                                 <div class="item c2">
                                    <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("SHADE_FAMILY") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("SHADE_CODE") %>' />
                                </div>
                                <div class="item c1 rAlign">
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
                        <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click" TabIndex="12"
                            Text="Adj.Recpt" Width="99%" />
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtQtyUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="99%" OnTextChanged="txtQtyUnit_TextChanged" TabIndex="13"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtQtyWeight" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont" TabIndex="14"
                            ReadOnly="True" Width="99%"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            OnTextChanged="txtQTY_TextChanged1" TabIndex="15" ReadOnly="True" Width="99%"></asp:TextBox>
                    </td>
                    <td width="9%">
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="99%" TabIndex="16"></asp:TextBox>
                    </td>
                    <td width="9%">
                    <asp:DropDownList ID="ddlCostCode" CssClass="TextBox SmallFont" Width="99%" TabIndex="17"  runat="server" AppendDataBoundItems="True">
                        </asp:DropDownList>
                       
                    </td>
                    <td width="9%">
                     <cc2:ComboBox ID="ddlMacCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            EmptyText="select..." EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                            Height="200px" MenuWidth="650px" OnLoadingItems="ddlMacCode_LoadingItems" OnSelectedIndexChanged="ddlMacCode_SelectedIndexChanged"
                            Width="30%">
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
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("MACHINE_GROUP") %>' />
                                </div>
                                <div class="item c3 ">
                                    <asp:Literal ID="Literal6" runat="server" Text='<%# Eval("MACHINE_SEGMENT") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("MACHINE_TYPE") %>' />
                                </div>
                                <div class="item c3 ">
                                    <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("MACHINE_SEC") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>

                        <asp:TextBox ID="txtMacCode" runat="server" CssClass="TextBox SmallFont" Width="50%" TabIndex="18"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="99%" TabIndex="19"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Add" Width="60px" TabIndex="20" />
                    </td>
                </tr>
                <tr class="SmallFont titleheading">
                    <td width="92%" colspan="9">
                        Code :
                        <asp:TextBox ID="txtYarnCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="40px" Font-Size="X-Small"></asp:TextBox>
                        &nbsp;Description :
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="100px"></asp:TextBox>
                             &nbsp; Shade Family:<asp:TextBox ID="txtShadeFamily" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="100px"></asp:TextBox>
                        &nbsp; Shade:<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="100px"></asp:TextBox>
                        &nbsp;UOM :
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="30px"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                            Text="Cancel" Width="60px" TabIndex="21" />
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
                        <asp:TemplateField HeaderText="Yarn Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("YARN_CODE") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("YARN_DESC") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Shade Family">
                            <ItemTemplate>
                                <asp:Label ID="txtSHADE_FAMILY" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("SHADE_FAMILY") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade">
                            <ItemTemplate>
                                <asp:Label ID="txtSHADE_CODE" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("SHADE_CODE") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No of Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtQtyUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                    ReadOnly="True" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM of Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtQtyUOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'
                                    ReadOnly="True" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Weight Of Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtQtyWeight" runat="server" CssClass="Label SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                    ReadOnly="True" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" ReadOnly="True"
                                    Text='<%# Bind("TRN_QTY") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtRate" runat="server" CssClass="LabelNo SmallFont" ReadOnly="True"
                                    Text='<%# Bind("BASIC_RATE") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" ReadOnly="true"
                                    Text='<%# Bind("AMOUNT") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost Code">
                            <ItemTemplate>
                                <asp:Label ID="txtCostCode" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("COST_CODE") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mac Code">
                            <ItemTemplate>
                                <asp:Label ID="txtMacCode" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("MAC_CODE") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" ReadOnly="True"
                                    Text='<%# Bind("REMARKS") %>' ></asp:Label>
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
<%--</ContentTemplate>
</asp:UpdatePanel> --%>