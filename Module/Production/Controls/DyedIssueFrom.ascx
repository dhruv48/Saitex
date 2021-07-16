<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DyedIssueFrom.ascx.cs"
    Inherits="Module_Production_Controls_DyedIssueFrom" %>
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
        margin-left: 2px;
        width: 80px;
    }
    .c3
    {
        margin-left: 2px;
        width: 150px;
    }
    .c4
    {
        margin-left: 2px;
        width: 350px;
    }
    .c5
    {
        margin-left: 4px;
        width: 250px;
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
            <b class="titleheading">Yarn Issue Against Production(Dyeing to dept.)</b>
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
                            EnableVirtualScrolling="true" MenuWidth="600px">
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
                        <asp:DropDownList ID="ddlIssueShift" Width="150px" runat="server" TabIndex="1">
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
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label24" runat="server" Text="Receive Numb :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtReceiveNumb" ReadOnly="true" runat="server" ValidationGroup="M1"
                            Width="98%" CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label4" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="6" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label9" runat="server" Text=" from Location :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Font-Size="9"
                            TabIndex="2" Width="98%">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label7" runat="server" Text="Document Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtDocNo" runat="server" TabIndex="4" Width="98%" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label6" runat="server" Text="To Location :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlToLocation" CssClass="SmallFont" AppendDataBoundItems="true"
                            Width="150px" runat="server" TabIndex="6">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label5" runat="server" Text=" From Store :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Font-Size="9"
                            TabIndex="3" Width="98%">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label8" runat="server" Text="Doc. Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtDocDate" runat="server" TabIndex="5" Width="98%" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label10" runat="server" Text="To Store :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlToStore" CssClass="SmallFont" AppendDataBoundItems="true"
                            Width="150px" runat="server" TabIndex="6">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label19" runat="server" Text="Party Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="PRTY_NAME"
                            OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged" Width="98%" MenuWidth="450px" EnableVirtualScrolling="true"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c5">
                                    NAME</div>
                                <%-- <div class="header c4">
                                    Address</div>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container7" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container8" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                <%-- <div class="item c4">
                                    <asp:Literal runat="server" ID="Container9" Text='<%# Eval("PRTY_ADDRESS") %>' /></div>--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td align="right" valign="top" colspan="2">
                        <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="11%" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" Width="85%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="150px" TabIndex="7" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                
                
                <tr id="BATCH_NO" runat="server">
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Labe44" runat="server" Text="Batch No.:" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <%--   <asp:CheckBoxList ID="ChkList" runat="server" Width="272px" Visible="false"></asp:CheckBoxList>--%>
                        <asp:TextBox ID="TxtLotIdNo" runat="server" TabIndex="14" Width="90%" CssClass="SmallFont"
                            Visible="false"></asp:TextBox>
                        <asp:DropDownList ID="ddlLotNo" TabIndex="14" Width="93%" CssClass="SmallFont" runat="server"
                            OnSelectedIndexChanged="ddlLotNo_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkLot" runat="server" Checked="false" Text="" AutoPostBack="true"
                            Width="5px" OnCheckedChanged="chkLot_CheckedChanged" />
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label2" runat="server" Text="Reprocess :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlReprocess" Width="98%" runat="server" CssClass="TextBox"
                            TabIndex="3">
                            <asp:ListItem>No</asp:ListItem>
                            <asp:ListItem>Yes</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                
                <tr id="Quality">
                    <td align="right" valign="top" width="17%">
                      <asp:Label ID="Label11" runat="server" Text="Yarn Code:" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%" >
                    <cc2:ComboBox ID="cmbYarnCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtYarnCode_LoadingItems" DataTextField="YARN_CODE" DataValueField="YARN_DESC"
                            OnSelectedIndexChanged="txtYarnCode_SelectedIndexChanged" Width="98%" MenuWidth="450px" EnableVirtualScrolling="true"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                 Yarn Code</div>
                                <div class="header c5">
                                    Yarn Desc</div>
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container7" Text='<%# Eval("YARN_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container8" Text='<%# Eval("ASS_YARN_DESC") %>' /></div>
                               
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td align="left" valign="top" width="17%" colspan="2" >
                        <asp:TextBox ID="lblYarnCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="11%" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="lblYarnDesc" TabIndex="4" runat="server" Width="85%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    
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
                    <td visible="false">
                        Remaining Qty
                    </td>
                    <td>
                        Yarn&nbsp;Code
                    </td>
                    <td width="8%">
                        Adj Rec
                    </td>
                    <td width="8%">
                        Unit Weight
                    </td>
                    <td width="8%">
                        No of Cheese
                    </td>
                    <td width="8%">
                        Qty
                    </td>
                    <%--<td width="8%">
                                Rate
                            </td>
                            <td width="8%">
                                Cost Code
                            </td>--%>
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
                            TabIndex="8" AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="1250px"
                            OnLoadingItems="DDLPiNo_LoadingItems" EnableVirtualScrolling="true" Width="100%"
                            OnSelectedIndexChanged="DDLPiNo_SelectedIndexChanged" Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    JC No</div>
                                <div class="header c3">
                                    PA No</div>
                                <div class="header c1">
                                    GreyLotNo</div>
                                <div class="header c1">
                                    Yarn Code</div>
                                <div class="header c4">
                                    Yarn Desc</div>
                                <div class="header c1">
                                    Shade</div>
                                <div class="header c1">
                                    Shade F#</div>
                                <div class="header c1">
                                    Rem Qty</div>
                                <div class="header c2">
                                    Machine</div>
                                    <div class="header c2">
                                    QC Status</div>
                                   <%-- <div class="header c2">
                                    Hydro Status</div>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal11" Text='<%# Eval("PO_NUMB") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PI_NO") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal10" Text='<%# Eval("LOT_NO") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("BASE_ARTICAL_CODE") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("BASE_ARTICAL_DESC") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("BASE_SHADE_CODE") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("BASE_SHADE_FAMILY") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("QTY_REM") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal12" Text='<%# Eval("MACHINE_CODE") %>' />
                                </div>
                                 <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal6" Text='<%# Eval("QC_RELEASE") %>' />
                                </div>
                                 <%--<div class="item c2">
                                    <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("H_COMP_FLAG") %>' />
                                </div>--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBalQty" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="40px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtYarnCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                            Width="98%" Text="Adj. Rec." TabIndex="9" />
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="txtunitweight" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="false"
                            Width="100%" ></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="txtnoofunit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="100%" ReadOnly="false"></asp:TextBox>
                    </td>
                    <td width="8%">
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="100%" OnTextChanged="txtQTY_TextChanged1" ReadOnly="True"></asp:TextBox>
                    </td>
                    <asp:TextBox ID="txtBasicRate" runat="server" Visible="false" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                        Width="100%" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                        Width="100%" ReadOnly="true" Visible="false"></asp:TextBox>
                    <asp:DropDownList ID="ddlCostCode" Visible="false" Width="100%" runat="server" AppendDataBoundItems="True"
                        TabIndex="10">
                    </asp:DropDownList>
        </td>
        <td width="15%">
            <cc2:ComboBox ID="ddlMacCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                EmptyText="select..." EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                TabIndex="11" Height="200px" MenuWidth="180px" OnLoadingItems="ddlMacCode_LoadingItems"
                OnSelectedIndexChanged="ddlMacCode_SelectedIndexChanged" Width="30%">
                <HeaderTemplate>
                    <div class="header c3">
                        Machine</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c3">
                        <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                    </div>
                   <%-- <div class="item c2">
                        <asp:Literal ID="Container3" runat="server" Text='<%# Eval("OLD_MACHINE_NAME") %>' />
                    </div>--%>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>
            <asp:TextBox ID="txtMacCode" Width="50%" runat="server" CssClass="TextBox SmallFont"
                MaxLength="6" TabIndex="12"></asp:TextBox>
        </td>
        <td width="13%">
            <asp:TextBox ID="txtDetRemarks" Width="99%" runat="server" CssClass="TextBox SmallFont"
                TabIndex="13"></asp:TextBox>
        </td>
        <td width="8%">
            <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                Width="98%" Text="Save" TabIndex="14" />
        </td>
    </tr>
    <tr>
        <td colspan="9" width="92%">
            JC&nbsp;No:
            <asp:TextBox ID="txtPONumb" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                ReadOnly="true" Width="50px"></asp:TextBox>
            Lot&nbsp;No:
            <asp:TextBox ID="txtLotNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                ReadOnly="true" Width="80px"></asp:TextBox>
            PA No:<asp:TextBox ID="txtPANo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                ReadOnly="true" Width="110px"></asp:TextBox>
            Code/Desc: &nbsp; &nbsp;<asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                ReadOnly="true" Width="250px"></asp:TextBox>
            Shade:<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                ReadOnly="true" Width="70px" Text="N/A"></asp:TextBox>
            Shade Family:<asp:TextBox ID="txtShadeFamily" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                ReadOnly="true" Width="70px" Text="N/A"></asp:TextBox>
        </td>
        <td width="8%">
            <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                Width="98%" Text="Cancel" TabIndex="15" />
        </td>
    </tr>
</table>
</td> </tr>
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
                    <asp:TemplateField HeaderText="Shade Family">
                        <ItemTemplate>
                            <asp:Label ID="txtShadefam" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_FAMILY") %>'
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
                      <asp:TemplateField HeaderText="No Of Cheese ">
                        <ItemTemplate>
                            <asp:Label ID="txtCheese" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
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
</ContentTemplate>
</asp:UpdatePanel>