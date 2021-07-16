<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StockTransfer.ascx.cs"
    Inherits="Module_Inventory_Controls_StockTransfer" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Src="../../../CommonControls/LOV/DepartmentLOV.ascx" TagName="DepartmentLOV"
    TagPrefix="uc1" %>
<%@ Register Src="../../../CommonControls/LOV/CompanyLov.ascx" TagName="CompanyLov"
    TagPrefix="uc2" %>
<%@ Register Src="../../../CommonControls/LOV/BranchLov.ascx" TagName="BranchLov"
    TagPrefix="uc3" %>
<%@ Register Src="../../../CommonControls/LOV/ShiftLov.ascx" TagName="ShiftLov" TagPrefix="uc4" %>

<script type="text/javascript" language="javascript">
   
    function Calculation(val)
    { 
     var name=val;                                                               
     document.getElementById('ctl00_cphBody_StockTransfer1_txtAmount').value=(Math.round(parseFloat(document.getElementById('ctl00_cphBody_StockTransfer1_txtQTY').value)*(parseFloat(document.getElementById('ctl00_cphBody_StockTransfer1_txtBasicRate').value)))).toFixed(2) ;
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
            <b class="titleheading">Material Stock Transfer</b>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft" width="100%">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                &nbsp;Mode</span>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label20" runat="server" Text="Source Company : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtIssueCompany" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label21" runat="server" Text="Source Branch : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtIssueBranch" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label22" runat="server" Text="Source Dept :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="20%">
                        <asp:TextBox ID="txtIssueDepartment" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                        <%--  <uc1:DepartmentLOV ID="ddlIssueDepartment" runat="server" />--%>
                    </td>
                </tr>
                 <tr>
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label2" runat="server" Text="Source Location : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                         <asp:DropDownList ID="ddlFromLocation" CssClass="SmallFont" AppendDataBoundItems="true"
                                    Width="150px" runat="server" TabIndex="6">
                                </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label3" runat="server" Text="Source Store : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlFromStore" CssClass="SmallFont" AppendDataBoundItems="true"
                                    Width="150px" runat="server" TabIndex="6">
                                </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label18" runat="server" Text="Destination Company : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="20%">
                      <uc2:CompanyLov ID="ddlReceiptCompany" runat="server" />
                    </td>
                </tr>
               
                
                <tr>
                   
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label19" runat="server" Text="Destination Branch : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <uc3:BranchLov ID="ddlReceiptBranch" runat="server" />
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label1" runat="server" Text="Destination Dept :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="20%">
                        <uc1:DepartmentLOV ID="ddlReceiptDepartment" runat="server" />
                    </td>
                     <td class="tdRight" width="15%">
                          <asp:Label ID="Label4" runat="server" Text="Destination Location :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                         <asp:DropDownList ID="ddlToLocation" CssClass="SmallFont" AppendDataBoundItems="true"
                                    Width="150px" runat="server" TabIndex="6">
                                </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label23" runat="server" Text="Issue Numb :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtIssueNumb" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="150px" CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"    OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="150px" Height="200px"   MenuWidth="800px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Issue #</div>
                                <div class="header c3">
                                    Issue Company</div>
                                <div class="header c3">
                                    Issue Branch</div>
                                <div class="header c1">
                                    Receipt #</div>
                                <div class="header c3">
                                    Receipt Company</div>
                                <div class="header c3">
                                    Receipt Branch</div>
                                <div class="header c5">
                                    Transaction Date</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container4" Text='<%# Eval("ISS_TRN_NUMB") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container6" Text='<%# Eval("ISS_COMP") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal5" Text='<%# Eval("ISS_BRANCH") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("TRN_NUMB") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("COMP_CODE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("BRANCH_CODE") %>' />
                                </div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Literal6" Text='<%# Eval("BRANCH_CODE") %>' />
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
                        <asp:Label ID="Label24" runat="server" Text="Receive Numb :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtReceiveNumb" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="150px" CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                          <asp:Label ID="Label5" runat="server" Text="Destination Store :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                         <asp:DropDownList ID="ddlToStore" CssClass="SmallFont" AppendDataBoundItems="true"
                                    Width="150px" runat="server" TabIndex="6">
                                </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label25" runat="server" Text="Transporter :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            EnableVirtualScrolling="true" OnLoadingItems="txtTransporterCode_LoadingItems"
                            DataTextField="PRTY_CODE" DataValueField="Address" OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged"
                            Width="150px" Height="200px" MenuWidth="450px">
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
                        <asp:TextBox ID="lblTransporterCode" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdLeft" colspan="1">
                        <asp:TextBox ID="txtTransporter" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                     <td class="tdRight" width="15%">
                        &nbsp;
                        <asp:Label ID="Label16" runat="server" Text="Transfer Date : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="20%">
                        <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="150px"
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label26" runat="server" CssClass="Label SmallFont" Text="Form Type : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtFormType" runat="server" ValidationGroup="M1"
                            Width="150px" CssClass="TextBox TextBoxDisplayu SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label27" runat="server" CssClass="Label SmallFont" Text="Form No : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtFormNo" runat="server" ValidationGroup="M1" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Transfer Shift : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="20%">
                        <uc4:ShiftLov ID="ddlShift" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label7" runat="server" Text="Document Number :  " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtDocNo" runat="server" TabIndex="14" Width="150px" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label8" runat="server" Text="Doc. Date : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtDocDate" runat="server" TabIndex="15" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No. : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="20%">
                        <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" width="20%" class="tdRight">
                        <asp:Label ID="Label14" runat="server" Text="Remarks : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" colspan="5" valign="top" width="80%">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="98%" TabIndex="21" 
                            CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="98%" cellpadding="0" cellspacing="0">
                <tr bgcolor="#336699" class="SmallFont titleheading">
                    <td width="20%" align="left" valign="top">
                        Item Code
                    </td>
                    <td width="10%" align="left" valign="top">
                        UOM
                    </td>
                    <td width="10%" align="center" valign="top">
                        Adj. Rec.
                    </td>
                    <td align="right" valign="top" width="10%">
                        Qty
                    </td>
                    <td align="right" valign="top" width="10%">
                        Rate
                    </td>
                    <td align="right" valign="top" width="10%">
                        Amount
                    </td>
                    <td width="20%" align="center" valign="top">
                        Remarks
                    </td>
                    <td width="10%" align="left" valign="top">
                    </td>
                </tr>
                <tr>
                    <td width="20%" align="left" valign="top">
                        <cc2:ComboBox ID="txtICODE" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="48%" OnLoadingItems="cmbPOITEM_LoadingItems"
                            EnableVirtualScrolling="true" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Item Code</div>
                                <div class="header c4">
                                    Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ITEM_CODE") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ITEM_DESC") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:TextBox ID="txtItemCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="45%"></asp:TextBox>
                    </td>
                    <td width="10%" align="left" valign="top">
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="99%"></asp:TextBox>
                    </td>
                    <td width="10%" align="center" valign="top">
                        <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                            Text="adj. Rec." Width="55px" />
                    </td>
                    <td align="right" width="10%" align="right" valign="top">
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="98%" onkeyup="javascript:Calculation(this.id)" AutoPostBack="True" OnTextChanged="txtQTY_TextChanged"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td align="right" width="10%" align="right" valign="top">
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="98%"></asp:TextBox>
                    </td>
                    <td align="right" width="10%" align="right" valign="top">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="98%"></asp:TextBox>
                    </td>
                    <td width="20%" align="left" valign="top">
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="98%"></asp:TextBox>
                    </td>
                    <td width="10%" rowspan="2" align="left" valign="top">
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" />
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                            Text="Cancel" />
                    </td>
                </tr>
                <tr>
                    <td colspan="7" align="left" valign="top">
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="99%"></asp:TextBox>
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
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_CODE") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                    Text='<%# Bind("BASIC_RATE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont"
                                    Text='<%# Bind("AMOUNT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                    Text='<%# Bind("REMARKS") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                                /
                                <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>
<cc1:CalendarExtender ID="ceDoc" runat="server" TargetControlID="txtDocDate" Format="dd/MM/yyyy">
</cc1:CalendarExtender>
