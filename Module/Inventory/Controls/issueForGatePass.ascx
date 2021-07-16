<%@ Control Language="C#" AutoEventWireup="true" CodeFile="issueForGatePass.ascx.cs" Inherits="Module_Inventory_Controls_issueForGatePass" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>

<script type="text/javascript" language="javascript">

    function Calculation(val) {
        var name = val;
        document.getElementById('ctl00_cphBody_ReturnMiss1_txtAmount').value = (Math.round(parseFloat(document.getElementById('ctl00_cphBody_ReturnMiss1_txtQTY').value) * (parseFloat(document.getElementById('ctl00_cphBody_ReturnMiss1_txtBasicRate').value)))).toFixed(2);
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
        width: 80px;
    }
    .rAlign
    {
        text-align: right;
    }
    .style1
    {
        height: 27px;
    }
    .style2
    {
        height: 47px;
    }
    .style3
    {
        height: 26px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
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
                    <b class="titleheading">Material Issue Gate Pass</b>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="100%">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td SmallFont">
                    <table width="100%">
                        <tr>
                            <td class="style2" width="15%" align="right">
                                <asp:Label ID="Label15" runat="server" Text="Challan Number : " CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td class="style2" width="15%">
                                <asp:TextBox ID="txtChallanNumber" runat="server" ValidationGroup="M1" Width="150px"
                                    TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" AutoPostBack="True"
                                    OnTextChanged="txtChallanNumber_TextChanged"></asp:TextBox>
                                <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                                    OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="150px" Height="200px"
                                    MenuWidth="500px" TabIndex="2">
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
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="style2" width="15%" align="right">
                                <asp:Label ID="Label16" runat="server" Text="Return Date : " CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="style2" width="15%">
                                <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="3" ValidationGroup="M1" Width="150px"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td class="style2" width="15%" align="right">
                            <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Return Shift : "></asp:Label>
                            </td>
                            <td class="style2" width="15%">
                            <asp:DropDownList ID="ddlIssueShift" runat="server" AppendDataBoundItems="true" Width="152px"
                                    TabIndex="11" CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                        </tr>
                           <tr>
                            <td valign="top" align="right" width="15%" class="style3">
                                <asp:Label ID="Label19" runat="server" Text="Party Code :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="10%" class="style3">
                                <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="cmbPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EmptyText="Select Vendor" OnSelectedIndexChanged="cmbPartyCode_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="150px" MenuWidth="500px" Height="200px"
                                    TabIndex="4">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Code</div>
                                        <div class="header c4">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="15%" class="style3">
                                <asp:TextBox ID="txtPartyCode" TabIndex="5" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                                
                            </td>
                            <td class="style3">
                              <asp:TextBox ID="txtPartyAddress" TabIndex="6" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                            
                             <td valign="top" align="right" class="style3">
                            <asp:Label ID="Label2" runat="server" Text="Returnable : " CssClass="Label SmallFont"></asp:Label>
                            </td>
                             <td class="style3">
                             <asp:DropDownList ID="ddlReturnable" runat="server" Width="152px" TabIndex="12">
                                    <asp:ListItem Text="Returnable" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Non Returnable" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%" class="style3">
                                <asp:Label ID="Label18" runat="server" Text="Transporter Code :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="10%" class="style3">
                                <cc2:ComboBox ID="cmbTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="cmbTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EnableVirtualScrolling="true" OnSelectedIndexChanged="cmbTransporterCode_SelectedIndexChanged"
                                    Width="150px" EmptyText="Select Transporter" MenuWidth="500px" Height="200px"
                                    TabIndex="7">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Code</div>
                                        <div class="header c4">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="15%" class="style3">
                                <asp:TextBox ID="txtTransporterCode" TabIndex="8" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                                
                            </td>
                            <td class="style3">
                             <asp:TextBox ID="txtTransporterName" TabIndex="9" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                            
                             <td align="right">
                            <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                             <td  align="left">
                             <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="15" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td class="tdRight" width="15%" align="right"> 
                                <asp:Label ID="Label1" runat="server" Text="Department :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                               
                                <asp:DropDownList ID="txtDepartment" runat="server" AppendDataBoundItems="true" Width="150px"
                                    TabIndex="10" CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="15%" align="right">
                                <asp:Label ID="Label5" runat="server" Text="Location :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                 <asp:DropDownList ID="ddlLocation" runat="server" AppendDataBoundItems="true" Width="150px"
                                    TabIndex="10" CssClass="SmallFont">
                                </asp:DropDownList>
                                
                            </td>
                            <td class="tdRight" width="15%" align="right">
                               <asp:Label ID="Label6" runat="server" Text="Store :" CssClass="Label SmallFont"></asp:Label> 
                            </td>
                            <td class="tdLeft" width="25%">
                                    <asp:DropDownList ID="ddlStore" runat="server" AppendDataBoundItems="true" Width="150px"
                                    TabIndex="10" CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1" width="15%" align="right">
                                <asp:Label ID="Label7" runat="server" Text="Document Number :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%" class="style1">
                                <asp:TextBox ID="txtDocNo" runat="server" TabIndex="13" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="style1" width="15%" align="right">
                                <asp:Label ID="Label8" runat="server" Text="Doc. Date" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%" class="style1">
                                <asp:TextBox ID="txtDocDate" runat="server" TabIndex="14" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="style1" width="15%" align="right">
                                <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="25%" class="style1">
                             <asp:TextBox ID="txtRemarks" runat="server" Width="150px" TabIndex="16" CssClass="TextBox SmallFont"></asp:TextBox>   
                            </td>
                        </tr>
                     
                    </table>
                </td>
            </tr>
            
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                       
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="95%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            <td>
                                Item Code
                            </td>
                            
                            <td align="right">
                                Qty
                            </td>
                            <td align="right">
                                Rate
                            </td>
                            <td align="right">
                                Amount
                            </td>
                           <td id="Td1" visible ="false" runat="server">
                                Cost Code
                            </td>
                            <td id="Td2" visible ="false" runat="server">
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
                                <cc2:ComboBox ID="cmbICODE" runat="server" CssClass="SmallFont" EmptyText="Select Item"
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="130px"
                                    EnableVirtualScrolling="true" OnLoadingItems="cmbICODE_LoadingItems" OnSelectedIndexChanged="cmbICODE_SelectedIndexChanged"
                                    Height="200px" TabIndex="17">
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
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("ITEM_CODE") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ITEM_DESC") %>' />
                                        </div>
                                        <div class="item c2 rAlign">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("REMQTY") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td >
                                
                            
                                <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo SmallFont" Width="90%"
                                    onkeyup="javascript:Calculation(this.id)" TabIndex="19"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                     Width="90%" TabIndex="20"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                     Width="90%" TabIndex="21"></asp:TextBox>
                            </td>
                            <td id="Td3" runat="server"  Visible = "false">
                                <asp:TextBox ID="txtCostCode" runat="server"  Visible = "false" CssClass="TextBox SmallFont" Width="50px"
                                    TabIndex="22"></asp:TextBox>
                            </td>
                            <td>
                                <cc2:ComboBox ID="ddlMacCode" runat="server" CssClass="SmallFont" EmptyText="select..."
                                    AutoPostBack="True" EnableLoadOnDemand="true"  Visible = "false" MenuWidth="650px" Width="50px"
                                    EnableVirtualScrolling="true" OnLoadingItems="ddlMacCode_LoadingItems" OnSelectedIndexChanged="ddlMacCode_SelectedIndexChanged"
                                    Height="200px" TabIndex="23">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Mac Code</div>
                                        <div class="header c2">
                                            Mac Group</div>
                                        <div class="header c3">
                                            Mac Segement</div>
                                        <div class="header c3">
                                            Mac Type</div>
                                        <div class="header c3">
                                            Mac Section</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MACHINE_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MACHINE_GROUP") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("MACHINE_SEGMENT") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("MACHINE_TYPE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container5" Text='<%# Eval("MACHINE_SEC") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:TextBox ID="txtMacCode" runat="server" Visible ="false" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="77px" MaxLength="24" ReadOnly="true"></asp:TextBox>
                            <%--</td>
                            <td>--%>
                                <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="90%"
                                    TabIndex="25"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save" TabIndex="26" Width="60px" />
                                <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                                    Text="Cancel" TabIndex="27" Width="60px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9">
                                <span lang="en-us">
                                    <asp:Label ID="Label3" runat="server" Text="Item Code/ Desc :" CssClass="Label SmallFont"></asp:Label><asp:TextBox
                                        ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"
                                        Width="70px"></asp:TextBox>
                                    /
                                    <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="180px"></asp:TextBox>
                                    &nbsp;
                                    <asp:Label ID="Label4" runat="server" Text="UOM :" CssClass="Label SmallFont"></asp:Label>
                                    <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="60px"></asp:TextBox>
                                    &nbsp;</span>
                                <asp:TextBox ID="txtQtyUnit" Visible="false" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="75px"></asp:TextBox>
                                <asp:TextBox ID="txtQtyWeight" runat="server" Visible="false" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="75px"></asp:TextBox>
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
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
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
                               <%-- <asp:TemplateField HeaderText="Cost Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtCostCode" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("COST_CODE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MAC Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtMacCode" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("MAC_CODE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDetRemarks" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("REMARKS") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                            CommandArgument='<%# Bind("UNIQUEID") %>' TabIndex="27"></asp:LinkButton>
                                        /
                                        <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                            CommandArgument='<%# Bind("UNIQUEID") %>' TabIndex="28"></asp:LinkButton>
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
        <cc1:CalendarExtender ID="ceDoc" runat="server" TargetControlID="txtDocDate" Format="dd/MM/yyyy"
            PopupPosition="TopRight">
        </cc1:CalendarExtender>
    </ContentTemplate>
</asp:UpdatePanel>