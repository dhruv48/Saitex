<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yrn_Issue_Without_Order.ascx.cs" Inherits="Module_Yarn_SalesWork_Controls_Yrn_Issue_Without_Order" %>
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
                                    ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1" 
                                    Style="width: 48px; height: 41px;">
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
                    <b class="titleheading">Yarn Issue Against Production</b>
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
                                    MenuWidth="400px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Ch No #</div>
                                        <div class="header c2">
                                            Ch Date</div>
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
                                <asp:DropDownList ID="ddlIssueShift" Width="98%" runat="server" TabIndex="1">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <%--<td class="tdRight" width="17%">
                                <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Lot No.:"></asp:Label>
                            </td>--%>
                           <%-- <td class="tdLeft" width="17%">--%>
                                <%--<asp:TextBox ID="TxtLotIdNo" runat="server" TabIndex="14" Width="98%" CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                              <asp:DropDownList ID="ddlLotNo" Width="98%" runat="server" Enabled="false">
                              </asp:DropDownList>--%>
                           <%-- </td>--%>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label1" runat="server" Text="Department :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:DropDownList ID="txtDepartment" CssClass="SmallFont" AppendDataBoundItems="true"
                                    Width="98%" runat="server" TabIndex="2">
                                    <asp:ListItem Selected="True" Value="TW0001">Twisting 1st Floor</asp:ListItem>
                                     <asp:ListItem  Value="TW0002">Twisting 2nd Floor</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label2" runat="server" Text="Reprocess :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:DropDownList ID="ddlReprocess" Width="98%" runat="server" CssClass="TextBox" TabIndex="3">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                             <td class="tdRight" width="17%">
                                <asp:Label ID="Label4" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="6" Width="98%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label7" runat="server" Text="Document Number :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtDocNo" runat="server" TabIndex="4" Width="98%" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label8" runat="server" Text="Doc. Date :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                <asp:TextBox ID="txtDocDate" runat="server" TabIndex="5" Width="98%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label9" runat="server" Text="Location :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                               <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Font-Size="9" TabIndex="2"
                Width="150px">
            </asp:DropDownList>  
                            </td>
                        </tr>
                        <tr>  <td align="right" valign="top" width="17%">
                        
                        <asp:Label ID="Labe44" runat="server" Text="Lot No. :" CssClass="Label SmallFont" ></asp:Label>
                        </td>
                         <td align="left" valign="top" width="17%">
                     <%--   <asp:CheckBoxList ID="ChkList" runat="server" Width="272px" Visible="false"></asp:CheckBoxList>--%>
                             <asp:TextBox ID="TxtLotIdNo" runat="server" TabIndex="14" Width="90%" CssClass="SmallFont" Visible="false" ></asp:TextBox>
                               <%--<asp:DropDownList ID="ddlLotNo"  TabIndex="14" Width="93%" CssClass="SmallFont"
                             runat="server" onselectedindexchanged="ddlLotNo_SelectedIndexChanged" >
                              </asp:DropDownList>--%>
                              <cc2:ComboBox ID="ddlLotNo" runat="server" AutoPostBack="True" CssClass="SmallFont"
                DataTextField="LOT_NO" DataValueField="TRN_DATA" EmptyText="Select Lot No" EnableLoadOnDemand="true"
                Height="200px" MenuWidth="500px" TabIndex="9" EnableVirtualScrolling="true"
                OnLoadingItems="ddlLotNo_LoadingItems"  OnSelectedIndexChanged="ddlLotNo_SelectedIndexChanged">
                <HeaderTemplate>
                    <div class="header c2">
                        Lot No</div>
                  <div class="header c1">
                         Code</div>
                           <div class="header c4">
                         Desc</div>
                       
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c2">
                        <asp:Literal ID="ltr2" runat="server" Text='<%# Eval("LOT_NO")%>'></asp:Literal>
                    </div>
                   <div class="item c1">
                        <asp:Literal ID="Literal10" runat="server" Text='<%# Eval("YARN_CODE")%>'></asp:Literal>
                    </div>
                      <div class="item c4">
                        <asp:Literal ID="Literal11" runat="server" Text='<%# Eval("YARN_DESC")%>'></asp:Literal>
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
                             oncheckedchanged="chkLot_CheckedChanged"  Visible="false"/>
                        </td>
                            <td align="right" valign="top" width="17%">
                                <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left"  valign="top" width="17%">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="98%" TabIndex="7" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                               <td class="tdRight" width="17%">
                                <asp:Label ID="Label5" runat="server" Text="Store :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                               <asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Font-Size="9" TabIndex="3"
                Width="150px">
            </asp:DropDownList> 
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
                                Lot No
                            </td>
                            <td width="8%">
                                Adj Rec
                            </td>
                            <td width="8%">
                                Unit Weight
                            </td>
                            <td width="8%">
                                No of Unit
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
                              <asp:TextBox ID="txtPANo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="110px"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                                    Width="98%" Text="Adj. Rec." TabIndex="9" />
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
                                <asp:DropDownList ID="ddlCostCode" Width="100%" runat="server" AppendDataBoundItems="True" TabIndex="10"  CssClass="SmallFont">
                                </asp:DropDownList>
                            </td>
                            <td width="15%">
                        
                                <asp:TextBox ID="txtMacCode" Width="30%" runat="server" CssClass="TextBox SmallFont"
                                    MaxLength="6" TabIndex="12">NA</asp:TextBox>
                                          <%--<cc2:ComboBox ID="cmbMachineGroup" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="MACHINE_GROUP" DataValueField="MACHINE_GROUP"  Width="50%" MenuWidth="200px"
                                    Height="200px" CssClass="SmallFont" TabIndex="11" EmptyText="Select Machine Group"
                                    OnLoadingItems="cmbMachineGroup_LoadingItems" OnSelectedIndexChanged="ddlMacCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Machine Group
                                        </div>
                                      
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MACHINE_GROUP") %>' /></div>
                                     
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>--%>
                                <cc2:ComboBox ID="ddlMacCode" runat="server" AutoPostBack="True" CssClass="SmallFont" 
                                    EmptyText="select..." EnableLoadOnDemand="true" EnableVirtualScrolling="true" TabIndex="11"
                                    Height="200px" MenuWidth="200px" OnLoadingItems="ddlMacCode_LoadingItems" OnSelectedIndexChanged="ddlMacCode_SelectedIndexChanged"
                                    Width="45%">
                                    <HeaderTemplate>
                                        <div class="header c5">
                                            Mac Code</div>
                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c5">
                                            <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                             -
                                    <asp:Literal ID="Literal9" runat="server" Text='<%# Eval("OLD_MACHINE_NAME") %>' />
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
                            <td width="13%">
                                <asp:TextBox ID="txtDetRemarks" Width="100%" runat="server" CssClass="TextBox SmallFont" TabIndex="13"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Width="98%" Text="Save" TabIndex="14"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9" width="92%">
                                
                                Code/Desc: &nbsp;<asp:TextBox ID="txtYarnCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="90px"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="150px"></asp:TextBox>
                                 
                                Shade:<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="100px" Text=""></asp:TextBox>
                                UOM:<asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="40px"></asp:TextBox>
                                Bal.Qty:<asp:TextBox ID="txtBalQty" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="40px"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                                    Width="98%" Text="Cancel" TabIndex="15" />
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
                                <asp:TemplateField HeaderText="Tex.LotNo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPICode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PI_NO") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shade">
                                    <ItemTemplate>
                                        <asp:Label ID="txtShadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
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
<%--    </ContentTemplate>
</asp:UpdatePanel>
--%>