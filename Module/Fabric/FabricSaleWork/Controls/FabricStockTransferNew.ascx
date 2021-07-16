<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FabricStockTransferNew.ascx.cs" Inherits="Module_Fabric_FabricSaleWork_Controls_FabricStockTransferNew" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>

<script type="text/javascript" language="javascript">
   
    function Calculation(val)
    { 
     var name=val;                                                               
     document.getElementById('ctl00_cphBody_FabricStockTransfer1_txtAmount').value=(Math.round(parseFloat(document.getElementById('ctl00_cphBody_FabricStockTransfer1_txtQTY').value)*(parseFloat(document.getElementById('ctl00_cphBody_FabricStockTransfer1_txtBasicRate').value)))).toFixed(2) ;
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
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<table class="tContentArial" width="945px">
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
                  <%--  <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>--%>
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
            <b class="titleheading">Fabric Stock Transfer</b>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft" width="100%">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                &nbsp;Mode</span>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                <td></td>
                    <td class="tdLeft" colspan="4" width="100%">
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="85px" Height="200px"
                            MenuWidth="800px" EmptyText="Find">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Issue #</div>
                                <div class="header c3">
                                    Issue Company</div>
                                <div class="header c3">
                                    Issue Branch</div>
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container6" Text='<%# Eval("COMP_CODE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal5" Text='<%# Eval("BRANCH_CODE") %>' />
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
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label20" runat="server" Text="Source Company : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <cc3:OboutDropDownList ID="ddlSourceCompany" AppendDataBoundItems="true" AutoPostBack="true"
                            Width="150px" MenuWidth="250px" runat="server" CssClass="TextBox" 
                            OnSelectedIndexChanged="ddlSourceCompany_SelectedIndexChanged" 
                            Height="150px">
                        </cc3:OboutDropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label18" runat="server" Text="Destination Company : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <cc3:OboutDropDownList ID="ddlDestinationCompany" AppendDataBoundItems="true" 
                            AutoPostBack="true" Height="150px"
                            Width="150px" MenuWidth="250px" runat="server" CssClass="TextBox" 
                            OnSelectedIndexChanged="ddlDestinationCompany_SelectedIndexChanged">
                        </cc3:OboutDropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label21" runat="server" Text="Source Branch : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <cc3:OboutDropDownList ID="ddlSourceBranch" AppendDataBoundItems="true" 
                            Width="150px" Height="150px"
                            MenuWidth="250px" runat="server" CssClass="TextBox" AutoPostBack="True" 
                            OnSelectedIndexChanged="ddlSourceBranch_SelectedIndexChanged">
                            <asp:ListItem>Select</asp:ListItem>
                        </cc3:OboutDropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label19" runat="server" Text="Destination Branch : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <cc3:OboutDropDownList ID="ddlDestinationBranch" AppendDataBoundItems="true" 
                            Width="150px" Height="150px"
                            MenuWidth="250px" runat="server" CssClass="TextBox" AutoPostBack="True" 
                            OnSelectedIndexChanged="ddlDestinationBranch_SelectedIndexChanged">
                            <asp:ListItem>Select</asp:ListItem>
                        </cc3:OboutDropDownList>
                    </td>
                </tr>
                <%--<tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label15" runat="server" Text="Transfer Number : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtChallanNumber" runat="server" ValidationGroup="M1" Width="80px"
                            TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" AutoPostBack="True"
                            OnTextChanged="txtChallanNumber_TextChanged"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="85px" Height="200px"
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
                        &nbsp;</td>
                    <td class="tdLeft" width="35%">
                        &nbsp;</td>
                </tr>--%>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label22" runat="server" Text="Source Department :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <cc3:OboutDropDownList ID="ddlSourceDepartment" MenuWidth="250px" 
                            CssClass="SmallFont" Height="150px"
                            AppendDataBoundItems="true" Width="150px" runat="server" TabIndex="6">
                        </cc3:OboutDropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label1" runat="server" Text="Destination Department :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <cc3:OboutDropDownList ID="ddlDestinationDepartment" MenuWidth="250px" 
                            CssClass="SmallFont" Height="150px"
                            AppendDataBoundItems="true" Width="150px" runat="server" TabIndex="6">
                        </cc3:OboutDropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label23" runat="server" Text="Issue Numb :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtIssueNumb" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="80px" CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label24" runat="server" Text="Receipt Numb :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="35%">
                        <asp:TextBox ID="txtReceiptNumb" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="80px" CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                        <br />
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
                        <asp:Label ID="Label16" runat="server" Text="Transfer Date : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="80px"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Transfer Shift : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <cc3:OboutDropDownList ID="ddlIssueShift" Width="90px" MenuWidth="200px" runat="server"
                            TabIndex="2" Height="150px">
                        </cc3:OboutDropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        &nbsp;
                    </td>
                    <td class="tdLeft" width="25%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label7" runat="server" Text="Document Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtDocNo" runat="server" TabIndex="14" Width="80px" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label8" runat="server" Text="Doc. Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtDocDate" runat="server" TabIndex="15" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="80px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%" class="tdRight">
                        <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" colspan="5" valign="top" width="85%">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="98%" TabIndex="21" CssClass="TextBox SmallFont"></asp:TextBox>
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
                        Fabric Code
                    </td>
                    <td>
                        Description
                    </td>
                    <td>
                        UOM
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
                        Remarks
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc2:ComboBox ID="txtICODE" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="380px" Width="100px"
                            OnLoadingItems="txtICODE_LoadingItems" OnSelectedIndexChanged="txtICODE_SelectedIndexChanged"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Fabric Code</div>
                                <div class="header c4">
                                    Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("FABR_CODE") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("FABR_DESC") %>' />
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
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="150px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="40px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="50px"
                            onkeyup="javascript:Calculation(this.id)"  ReadOnly="true" 
                            ontextchanged="txtQTY_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="120px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" />
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                            Text="Cancel" />
                    </td>
                </tr>
                <tr>
                <td class="SmallFont titleheading">Shade Code</td>
                <td><asp:DropDownList ID="cmbShade" runat="server" AutoPostBack="True" 
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select"></asp:ListItem>
                                    </asp:DropDownList></td>
                                    <td>
<asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont"  Text="adj. Rec." Width="55px" 
                                            onclick="btnAdjRec_Click" />
                    
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
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("FABR_CODE") %>'
                                    ReadOnly="True" Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("FABR_DESC") %>'
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
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                    Text='<%# Bind("BASIC_RATE") %>' Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount1" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont"
                                    Text='<%# Bind("AMOUNT") %>' Width="60px"></asp:Label>
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
<cc1:CalendarExtender ID="ceIssueDate" runat="server" TargetControlID="txtIssueDate">
</cc1:CalendarExtender>
<cc1:CalendarExtender ID="ceDoc" runat="server" TargetControlID="txtDocDate">
</cc1:CalendarExtender>

