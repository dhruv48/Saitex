<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiberIssueAgnstPA1.ascx.cs"
    Inherits="Module_Fiber_Controls_FiberIssurAgnstPA1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; 
        *display:inline;
        overflow:hidden;
        white-space:nowrap;
        }
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
        width: 80px;
    }
</style>
<script type="text/javascript" src="../../../../javascript/jquery-1.4.1.min.js"></script>
<script src="../../../../javascript/jquery-ui.min.js" type = "text/javascript"></script>
<link href="../../../../javascript/jquery-ui.css" rel = "Stylesheet" type="text/css" />
<%--<script type="text/javascript">
    $(document).ready(function() {
    $("#<%=TxtLotIdNo.ClientID %>").autocomplete({
            source: function(request, response) {
                $.ajax({
                url: '<%=ResolveUrl("~/MOM.asmx/AutoLotFromYarnLotMakingMaster") %>',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function(data) {
                        response(data.d);                       
                    },
                    error: function(response) {
                        alert(response.responseText);
                    },
                    failure: function(response) {
                        alert(response.responseText);
                    }
                });
            },

            minLength: 1
        });

       
        
    });
</script>  --%>

<asp:UpdatePanel ID="upd" runat="server" >
<ContentTemplate>


<table class="tdMain" width="100%">
    <tr>
        <td class="td" width="100%">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1" Style="width: 48px; height: 41px;">
                        </asp:ImageButton>
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
            <b class="titleheading">Poy Issue Against PA (Production Advice)</b>
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
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label15" runat="server" Text="Challan Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtChallanNumber" runat="server" ValidationGroup="M1" Width="98%"
                            TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" AutoPostBack="True"
                            OnTextChanged="txtChallanNumber_TextChanged"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="98%" Height="200px"
                            MenuWidth="600px">
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
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label16" runat="server" Text="Issue Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="98%"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label3" runat="server" CssClass="Label SmallFont" Text="Issue Shift :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlIssueShift" Width="98%" runat="server" TabIndex="2">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                   <td class="tdRight" width="17%">
                                <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Lot No.:"></asp:Label>
                            </td>
                     <td class="tdLeft" width="17%">
                    <asp:TextBox ID="TxtLotIdNo" runat="server" TabIndex="14" Width="90%" CssClass="SmallFont" Visible="false" ></asp:TextBox>
                               <%--<asp:DropDownList ID="ddlLotNo"  TabIndex="14" Width="93%" CssClass="SmallFont"
                             runat="server" onselectedindexchanged="ddlLotNo_SelectedIndexChanged" >
                              </asp:DropDownList>--%>
                              
                              <cc2:ComboBox ID="ddlLotNo" runat="server" AutoPostBack="True" CssClass="SmallFont"
                DataTextField="LOT_NO" DataValueField="ASS_YARN_DESC" EmptyText="Select Lot No" EnableLoadOnDemand="true"
                Height="200px" MenuWidth="350px" TabIndex="9" EnableVirtualScrolling="true"
                OnLoadingItems="ddlLotNo_LoadingItems"  OnSelectedIndexChanged="ddlLotNo_SelectedIndexChanged">
                <HeaderTemplate>
                    <div class="header c6">
                        Lot No</div>
                  <div class="header c4">
                        Details</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c6">
                        <asp:Literal ID="ltr2" runat="server" Text='<%# Eval("LOT_NO")%>'></asp:Literal>
                    </div>
                   <div class="item c4">
                        <asp:Literal ID="Literal10" runat="server" Text='<%# Eval("ASS_YARN_DESC")%>'></asp:Literal>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>
            
                              <asp:CheckBox ID="chkLot" runat="server" Checked="true" Text=""  AutoPostBack="true" Width="5px"
                             oncheckedchanged="chkLot_CheckedChanged" />
                     </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label1" runat="server" Text="Department :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:DropDownList ID="txtDepartment" CssClass="SmallFont" AppendDataBoundItems="true"
                            Width="98%" runat="server" TabIndex="6">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label2" runat="server" Text="Reprocess :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlReprocess" Width="98%" runat="server" CssClass="TextBox">
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label7" runat="server" Text="Document Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtDocNo" runat="server" TabIndex="14" Width="98%" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label8" runat="server" Text="Doc. Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtDocDate" runat="server" TabIndex="15" Width="98%" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%" colspan="2" align="center">
                    <asp:Label ID="lblYarnDesc" runat="server" CssClass="SmallFont"></asp:Label>
                        <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont" Visible="false"></asp:Label>
                   <%-- </td>
                    <td align="left" valign="top" width="15%">--%>
                        <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="98%" CssClass="TextBox SmallFont" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" colspan="5" valign="top" width="83%">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="98%" TabIndex="21" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Labe44" runat="server" Text="Lot No. :" CssClass="Label SmallFont" Visible="false"></asp:Label>
                    </td>
                    <td align="left" colspan="2" valign="top" width="83%">
                        <asp:CheckBoxList ID="ChkList" runat="server" Width="272px" AutoPostBack="True" OnSelectedIndexChanged="ChkList_SelectedIndexChanged">
                        </asp:CheckBoxList>
                        
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="98%">
                <tr bgcolor="#336699" class="SmallFont titleheading">
                    <td width="15%">
                        PA No
                    </td>
                    <td width="8%">
                        Adj Rec
                    </td>
                    <td width="8%">
                        Tube Weight
                    </td>
                    <td width="8%">
                        No of Tubes
                    </td>
                    <td width="8%">
                        Qty
                    </td>
                    <td width="8%">
                        Rate
                    </td>
                    <td width="8%">
                        Cost Code
                    </td>
                    <td width="8%">
                        Mac Code
                    </td>
                    <td width="13%">
                        Remarks
                    </td>
                    <td width="8%">
                    </td>
                </tr>
                <tr>
                    <td width="15%">
                        <cc2:ComboBox ID="DDLPiNo" runat="server" CssClass="SmallFont" EmptyText="select.."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="850px" OnLoadingItems="DDLPiNo_LoadingItems"
                            EnableVirtualScrolling="true" Width="100%" OnSelectedIndexChanged="DDLPiNo_SelectedIndexChanged"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c3">
                                    PA No</div>
                                         <div class="header c4">
                                    Yarn Desc</div>
                                <div class="header c3">
                                    Poy Code</div>
                                <div class="header c4">
                                    Poy Desc</div>
                                <%--<div class="header c3">
                                            Shade</div>--%>
                                <div class="header c2">
                                    Rem Qty</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PI_NO") %>' />
                                </div>
                                 <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("ASS_ARTICAL_DESC") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("BASE_ARTICAL_CODE") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("BASE_ARTICAL_DESC") %>' />
                                </div>
                                <%-- <div class="item c3">
                                            <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("BASE_SHADE_CODE") %>' />
                                        </div>--%>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("QTY_REM") %>' />
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
                            Width="98%" Text="Adj. Rec." />
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="txtunitweight" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="100%" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="txtnoofunit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="100%" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="100%" OnTextChanged="txtQTY_TextChanged1" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="100%" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="100%" ReadOnly="true" Visible="false"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:DropDownList ID="ddlCostCode" Width="100%" runat="server" AppendDataBoundItems="True">
                        </asp:DropDownList>
                    </td>
                    <td width="15%">
                        <cc2:ComboBox ID="ddlMacCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            EmptyText="select..." EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                            Height="200px" MenuWidth="650px" OnLoadingItems="ddlMacCode_LoadingItems" OnSelectedIndexChanged="ddlMacCode_SelectedIndexChanged"
                            Width="25%">
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
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />-
                                    <asp:Literal ID="Literal9" runat="server" Text='<%# Eval("OLD_MACHINE_NAME") %>' />
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
                        <asp:TextBox ID="txtMacCode" Width="50%" runat="server" CssClass="TextBox SmallFont"
                            MaxLength="6">NA</asp:TextBox>
                    </td>
                    <td width="13%">
                        <asp:TextBox ID="txtDetRemarks" Width="100%" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Width="60px" Text="Save" />
                    </td>
                </tr>
                <tr>
                    <td colspan="9" width="92%">
                        PA No:<asp:TextBox ID="txtPANo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="110px"></asp:TextBox>
                        Code/Desc: &nbsp;<asp:TextBox ID="txtYarnCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="90px"></asp:TextBox>
                        &nbsp;<asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="150px"></asp:TextBox>
                        <%--  Shade:<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="100px"></asp:TextBox>--%>
                        Uom:<asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="40px"></asp:TextBox>
                          <%--  Uom2:--%><asp:TextBox ID="TxtUom1" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="40px" Visible="false"></asp:TextBox>
                            <%--Kg/Bail:--%><asp:TextBox ID="Txtkg_bail" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="40px" Visible="false"></asp:TextBox>
                        Bal.Qty:<asp:TextBox ID="txtBalQty" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="40px"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                            Width="60px" Text="Cancel" />
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
                        <asp:TemplateField HeaderText="PA.NO">
                            <ItemTemplate>
                                <asp:Label ID="lblPICode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PI_NO") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Poy Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("FIBER_CODE") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("FIBER_DESC") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Unit2" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT1" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM1") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Kg/Bail" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtkg_bail" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM_BAIL") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    ReadOnly="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                    Text='<%# Bind("BASIC_RATE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont"
                                    Text='<%# Bind("AMOUNT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost Code">
                            <ItemTemplate>
                                <asp:Label ID="txtCostCode" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                    Text='<%# Bind("COST_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mac Code">
                            <ItemTemplate>
                                <asp:Label ID="txtMacCode" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                    Text='<%# Bind("MAC_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                    Text='<%# Bind("REMARKS") %>'></asp:Label>
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
<cc1:CalendarExtender ID="ceIssueDate" runat="server" TargetControlID="txtIssueDate"
    Format="dd/MM/yyyy">
</cc1:CalendarExtender>

<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" TargetControlID="txtDocDate"></cc1:MaskedEditExtender>


<asp:RangeValidator ID="rv1" runat="server" ControlToValidate="txtChallanNumber"
    Display="None" ErrorMessage="Only numeric value allowed" MaximumValue="1000000"
    MinimumValue="1" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
<cc1:ValidatorCalloutExtender ID="vcrv1" runat="server" TargetControlID="rv1">
</cc1:ValidatorCalloutExtender>
<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtChallanNumber"
    Display="None" ErrorMessage="MRN number required" ValidationGroup="M1"></asp:RequiredFieldValidator>
<cc1:CalendarExtender ID="ceDoc" runat="server" TargetControlID="txtDocDate" Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
    ShowSummary="False" ValidationGroup="M1" />
    
    
    
    </ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="imgbtnSave" />
<asp:PostBackTrigger ControlID="imgbtnUpdate" />
<asp:PostBackTrigger ControlID="imgbtnExit" />
</Triggers>
</asp:UpdatePanel>
