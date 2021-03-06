<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Material_Issue_For_Dept.ascx.cs" Inherits="Module_Inventory_Controls_Material_Issue_For_Dept" %>
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
        width: 300px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
    .style1
    {
        height: 26px;
    }
    .SmallFont {
        top: 0px;
        left: 0px;
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
                    <b class="titleheading">Material Issue For Department</b>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="100%">
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td SmallFont">
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
                                <asp:Label ID="Label16" runat="server" Text="Issue Date : " CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="150px"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="LotId No: "></asp:Label>
                            </td>
                            <td class="tdLeft" width="35%">
                                <asp:TextBox ID="TxtLotIdNo" runat="server" TabIndex="3" onkeyup="pricevalidate(this);"
                                    Width="150px" CssClass="TextBoxNo SmallFont"></asp:TextBox>
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
                                <asp:Label ID="Label1" runat="server" Text="From Location :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="style1" width="15%">
                                <asp:DropDownList ID="ddlFromLocation" CssClass="SmallFont" AppendDataBoundItems="true"
                                    Width="150px" runat="server" TabIndex="4">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label3" runat="server" CssClass="Label SmallFont" Text="Issue Shift : "></asp:Label>
                            </td>
                            <td class="style1" width="15%">
                               <asp:DropDownList ID="ddlIssueShift" Width="150px" MenuWidth="200px" runat="server"
                                    TabIndex="5">
                                </asp:DropDownList> 
                            </td>
                            <td class="tdRight" width="15%">
                            <asp:Label ID="Label5" runat="server" Text="To Deparment :" CssClass="Label SmallFont"></asp:Label>
                                
                            </td>
                            <td class="style1" width="25%">
                            <asp:DropDownList ID="txtDepartment" CssClass="SmallFont" AppendDataBoundItems="true"
                                    Width="150px" runat="server" TabIndex="6">
                                </asp:DropDownList>
                                
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label4" runat="server" Text="From Store :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:DropDownList ID="ddlFromStore" CssClass="SmallFont" AppendDataBoundItems="true"
                                    Width="150px" runat="server" TabIndex="7">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="15%">
                               <asp:Label ID="Label2" runat="server" Text="Reprocess :" CssClass="Label SmallFont"></asp:Label> 
                            </td>
                            <td class="tdLeft" width="15%">
                               <asp:DropDownList ID="ddlReprocess" TabIndex="8" Width="150px" runat="server" CssClass="SmallFont">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                </asp:DropDownList> 
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label6" runat="server" CssClass="Label SmallFont" Text="To Location : "></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:DropDownList ID="ddlToLocation" Width="150px" MenuWidth="200px" runat="server" CssClass="SmallFont"
                                    TabIndex="9">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label7" runat="server" Text="Document Number :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtDocNo" runat="server" TabIndex="10" Width="150px" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label8" runat="server" Text="Doc. Date" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtDocDate" runat="server" TabIndex="11" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="15%">
                                <asp:Label ID="Label10" runat="server" Text="To Store :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="25%">
                                <asp:DropDownList ID="ddlToStore" Width="150px" MenuWidth="200px" runat="server" CssClass="SmallFont"
                                    TabIndex="12">
                                </asp:DropDownList>
                            </td>
                           
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="15%">
                                <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" colspan="3" valign="top" width="85%">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="94%" TabIndex="13" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="15%">
                                <asp:Label ID="Label9" runat="server" Text="Sub Dept. Details :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="25%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="14" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="98%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                          <td id =td2 runat="server" visible ="false">
                                PA No
                            </td>
                            <td>
                                Item Code
                            </td>
                            <td>
                                Adj Rcpt
                            </td>
                            <td>
                                Qty
                            </td>
                            <td>
                                Rate
                            </td>
                            <td id =td5 runat="server" visible ="false">
                                Cost Code
                            </td>
                            <td id =td6 runat="server" visible ="false">
                                Mac Code
                            </td>
                            <td>
                                Remarks
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td id =td1 runat="server" visible ="false">
                                <cc2:ComboBox ID="ddlPaNo" runat="server" AutoPostBack="True" CssClass="SmallFont" visible ="false"
                                    DataTextField="PI_NO" DataValueField="PI_NO" EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                                    Height="200px" MenuWidth="850px" OnLoadingItems="ddlPaNo_LoadingItems" OnSelectedIndexChanged="ddlPaNo_SelectedIndexChanged"
                                    TabIndex="15" Width="100px">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Pa Type</div>
                                        <div class="header c3">
                                            Pa No</div>
                                        <div class="header c3">
                                            Article Code
                                        </div>
                                        <div class="header c4">
                                            Article Desc</div>
                                        <div class="header c3">
                                            Shade</div>
                                        <div class="header c3">
                                            Party Code</div>
                                        <div class="header c4">
                                            Party Desc</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("PI_TYPE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Container5" runat="server" Text='<%# Eval("PI_NO") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Container6" runat="server" Text='<%# Eval("ARTICAL_CODE") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("ARTICAL_DESC") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Literal6" runat="server" Text='<%# Eval("SHADE_CODE") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
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
                                <cc2:ComboBox ID="ddlItemCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    DataTextField="ITEM_CODE" DataValueField="ITEM_DATA" EnableLoadOnDemand="true"
                                    EnableVirtualScrolling="true" MenuWidth="550px" OnLoadingItems="ddlItemCode_LoadingItems"
                                    OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged" TabIndex="16" Width="100px">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Code</div>
                                        <div class="header c4">
                                            DESCRIPTION</div>
                                        <div class="header c1">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ITEM_CODE") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal ID="Container5" runat="server" Text='<%# Eval("ITEM_DESC") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal ID="Container6" runat="server" Text='<%# Eval("ITEM_TYPE") %>' />
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
                                <asp:Button ID="btnAdjRec" TabIndex="17" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                                    Text="Adj.Recpt." Width="65px" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtQTY" TabIndex="18" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="100px" OnTextChanged="txtQTY_TextChanged1" ReadOnly="True" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBasicRate" TabIndex="19" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="100px" ontextchanged="txtBasicRate_TextChanged"></asp:TextBox>
                            </td>
                            <td id =td3 runat="server" visible ="false">
                                <asp:DropDownList ID="ddlCostCode" CssClass="SmallFont" runat="server" AppendDataBoundItems="True" Visible ="false">
                                </asp:DropDownList>
                            </td>
                            <td id =td4 runat="server" visible ="false">
                                <cc2:ComboBox ID="ddlMacCode" runat="server" CssClass="SmallFont" EmptyText="select..." Visible ="false"
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
                                <asp:TextBox ID="txtMacCode" runat="server" CssClass="TextBox SmallFont" Width="50px" Visible = "False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDetRemarks" TabIndex="20" runat="server" CssClass="TextBox SmallFont" Width="150px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnsaveTRNDetail" TabIndex="21" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save" Width="60px" />
                           
                                <asp:Button ID="btnTRNCancel" TabIndex="22" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                                    Text="Cancel" Width="60px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <asp:TextBox ID="txtPaNo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont" Visible ="false"
                                    ReadOnly="true" Width="120px"></asp:TextBox>
                                &nbsp;Item Code/Desc:<asp:TextBox ID="txtItemCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="120px"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="150px"></asp:TextBox>
                                UOM:<asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtQtyUnit" Visible="false" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="15px"></asp:TextBox>
                                <asp:TextBox ID="txtQtyWeight" runat="server" Visible="false" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="75px"></asp:TextBox>
                                Amount:<asp:TextBox ID="txtAmount0" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="73px"></asp:TextBox>
                                <asp:TextBox ID="txtBalQty" runat="server" CssClass="TextBox TextBoxDisplay SmallFont" Visible ="false"
                                    ReadOnly="true" Width="40px"></asp:TextBox>
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
                               <%-- <asp:TemplateField HeaderText="Pa No.">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPaNumb" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PI_NO") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="I.Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_CODE") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'
                                            ReadOnly="True"></asp:Label>
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
                                        <asp:Label ID="txtAmount" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("AMOUNT") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <%--  <asp:TemplateField HeaderText="Cost Code">
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
   <%-- </ContentTemplate>
</asp:UpdatePanel>--%>