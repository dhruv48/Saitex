<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Waste_Sale.ascx.cs" Inherits="Module_Waste_Controls_Waste_Sale" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    .SmallFont
    {
        width: 42px;
    }
    .panelclass
    {
        border-color: Red;
        border-style: solid;
        border-width: 2;
        background-color: Gray;
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
                    <b class="titleheading">Invoice Against Customer Request To Party (Waste)</b>
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
                <td width="100%" class="td">
                    <table width="100%">
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label15" runat="server" Text="Invoice ID : " CssClass="LabelNo SmallFont"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtChallanNumber" runat="server" ValidationGroup="M1" Width="83px"
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
                                <asp:Label ID="Label16" runat="server" Text="Invoice Date : " CssClass="Label tdRight SmallFont"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="99%"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label3" runat="server" CssClass="Label SmallFont " Text="Shift : "></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:DropDownList ID="ddlIssueShift" Width="90px" runat="server" TabIndex="2">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <%--<asp:Label ID="Label17" runat="server" Text="Receiving Branch :" CssClass="LabelNo SmallFont"
                            Width="100%"></asp:Label>--%><asp:Label ID="Label7" runat="server" Text="Invoice No :"
                                CssClass="LabelNo SmallFont" Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <%--<asp:DropDownList ID="ddlDelAdd" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                            CssClass="SmallFont TextBox" OnSelectedIndexChanged="ddlDelAdd_SelectedIndexChanged"
                            Width="99%">
                        </asp:DropDownList>--%><asp:TextBox ID="txtInvoiceNo" runat="server" TabIndex="14" Width="99%"
                            CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label12" runat="server" Text="Buyer's Order No :"
                                CssClass="LabelNo SmallFont" Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtBuyerorder" runat="server" TabIndex="14" Width="99%"
                            CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label8" runat="server" Text="Buyer's Order Date :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                            <asp:TextBox ID="txtDocDate" runat="server" TabIndex="10" ValidationGroup="M1" Width="89%"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label4" runat="server" CssClass="LabelNo" Text="Party Detail :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="CmBParty" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="CmBParty_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    OnSelectedIndexChanged="CmBParty_SelectedIndexChanged" Width="99%" Height="200px"
                                    MenuWidth="550px" EnableVirtualScrolling="true" EmptyText="Select Party">
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
                            <td valign="top" align="left" colspan="4">
                                <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="10%"></asp:TextBox>
                                <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged" Width="99%"
                                    Height="200px" MenuWidth="550px" EnableVirtualScrolling="true" EmptyText="Select Transporter">
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
                            <td valign="top" align="left" width="70%" colspan="4">
                                <asp:TextBox ID="lblTransporterCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="10%"></asp:TextBox>
                                <asp:TextBox ID="txtTransporterAddress" TabIndex="5" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label2" runat="server" CssClass="LabelNo" Text="Consignee Code :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="txtConsigneeCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtConsigneeCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EmptyText="Select Consignee" OnSelectedIndexChanged="txtConsigneeCode_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="150px" MenuWidth="800px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c4">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="70%" colspan="4">
                                <asp:TextBox ID="lblConsigneeCode" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="10%"></asp:TextBox>
                                <asp:TextBox ID="txtConsigneeAddress" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="85%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label5" runat="server" Text="LR No :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtLRNumber" runat="server" TabIndex="15" Width="99%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label6" runat="server" Text="LR Date :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="99%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label9" runat="server" Text="Truck No :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="83px" CssClass="TextBox SmallFont"></asp:TextBox>
                                <asp:Button ID="btnOther" runat="server" TabIndex="17" Text="Show Other" OnClick="btnOther_Click"
                                    Width="100px" />
                            </td>
                        </tr>
                        <tr id="trOther" runat="server" visible="false">
                            <td  colspan="7" align ="center"><%--<%# Container.ItemsCount %>--%>
                                <asp:Panel ID="pnlOtherDTL" runat="server" BackColor="LightSteelBlue" BorderStyle="Ridge"
                                    BorderWidth="5px">
                                    <table>
                                        <tr>
                                        <td class="tdRight" width="15%">
                                         <asp:Label ID="Label18" runat="server" Text="other Reference(s) D/A NO. :" CssClass="Label SmallFont tdRight"
                                           Width="100%"></asp:Label>
                                           </td>

                                            <td align="left">
                                                <asp:TextBox ID="txtInsurancePolicyNo" runat="server" CssClass="TextBox SmallFont"
                                                    TabIndex="27" Width="80px"></asp:TextBox>
                                            </td>
                                             <td class="tdRight" width="15%">
                                         <asp:Label ID="Label19" runat="server" Text=" other Reference(s) Date :" CssClass="Label SmallFont tdRight"
                                           Width="100%"></asp:Label>
                                           </td>
                                           
                                            <td align="left">
                                                <asp:TextBox ID="txtLCDate" runat="server" CssClass="TextBox SmallFont" TabIndex="27"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                         <td class="tdRight" width="15%">
                                         <asp:Label ID="Label20" runat="server" Text=" buyer(other than consignee) :" CssClass="Label SmallFont tdRight"
                                           Width="100%"></asp:Label>
                                           </td>
                                          
                                            <td align="left">
                                               <asp:TextBox ID="txtbuyer" runat="server" CssClass="TextBox SmallFont"
                                                    TabIndex="27" Width="80px"></asp:TextBox>
                                            </td>
                                           
                                          <%--  <td align="right" runat ="server" visible ="false">
                                                Sale Against :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleAgainst" runat="server" visible ="false" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="28" Width="80px"></asp:TextBox>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                           <%-- <td align="right"runat ="server" visible ="false">
                                                Excise Notification No :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseNo" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="29" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Excise Notification Date :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseDate" runat="server" CssClass="TextBox SmallFont" TabIndex="30"
                                                    Width="80px"></asp:TextBox>
                                            </td>--%>
                                             <td class="tdRight" width="15%">
                                         <asp:Label ID="Label21" runat="server" Text=" Date & Time of preparation :" CssClass="Label SmallFont tdRight"
                                           Width="100%"></asp:Label>
                                           </td>
                                           
                                            <td align="left">
                                                <asp:TextBox ID="txtdateprp" runat="server" CssClass="TextBox SmallFont" TabIndex="27"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                              <td class="tdRight" width="15%">
                                         <asp:Label ID="Label22" runat="server" Text=" Date & Time removal :" CssClass="Label SmallFont tdRight"
                                           Width="100%"></asp:Label>
                                           </td>
                                           
                                           
                                            <td align="left">
                                                <asp:TextBox ID="txtdateremvl" runat="server" CssClass="TextBox SmallFont" TabIndex="27"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                              <td class="tdRight" width="15%">
                                         <asp:Label ID="Label23" runat="server" Text="Freight Amount :" CssClass="Label SmallFont tdRight"
                                           Width="100%"></asp:Label>
                                           </td>
                                           
                                            <td align="left">
                                                <asp:TextBox ID="txtFreight" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                                    MaxLength="18" OnTextChanged="txtFreight_TextChanged" TabIndex="31" ValidationGroup="M1"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                         
                                        </tr>
                                        <tr>
                                         <td class="tdRight" width="15%">
                                         <asp:Label ID="Label24" runat="server" Text="Goods Dispatch From:" CssClass="Label SmallFont tdRight"
                                           Width="100%"></asp:Label>
                                           </td>
                                          
                                            <td align="left">
                                                <asp:TextBox ID="txtgoodsdisptch" runat="server" CssClass="TextBox SmallFont" MaxLength="25"
                                                    TabIndex="47" Width="80px"></asp:TextBox>
                                            </td>
                                             <td class="tdRight" width="15%">
                                         <asp:Label ID="Label25" runat="server" Text=" Final Destination:" CssClass="Label SmallFont tdRight"
                                           Width="100%"></asp:Label>
                                           </td>
                                           
                                            <td align="left">
                                                <asp:TextBox ID="txtDestination" runat="server" CssClass="TextBox SmallFont" MaxLength="25"
                                                    TabIndex="47" Width="80px"></asp:TextBox>
                                            </td>
                                              <td class="tdRight" width="15%">
                                         <asp:Label ID="Label26" runat="server" Text=" E.D:" CssClass="Label SmallFont tdRight"
                                           Width="100%"></asp:Label>
                                           </td>
                                            
                                            <td align="left">
                                                <asp:TextBox ID="txtED" runat="server" AutoPostBack="True" OnTextChanged="txtED_TextChanged" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="28" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="6">
                                                <asp:Button ID="btncncelpack" runat="server" OnClick="btncncelpack_Click" TabIndex="48"
                                                    Text="Hide Others" ValidationGroup="M1" Width="100px" />
                                                <asp:RangeValidator ID="RVFreight" runat="server" ControlToValidate="txtFreight"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Freight" MaximumValue="9999999"
                                                    MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                             
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label10" runat="server" Text="Sub Total :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtSubTotal" runat="server" Width="120px" TabIndex="21" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label11" runat="server" Text=" Total Amount:" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtFinalTotal" runat="server" Width="120px" TabIndex="21" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                              <td width="15%" class="tdRight">
                                <asp:Label ID="Label17" runat="server" Text="Other Charges :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                           
                            <td width="15%" class="tdLeft">
                                <asp:Button ID="btnDisTaxAdjMST" runat="server" Text="Dis/Tax" Width="100px"
                                    TabIndex="17" CssClass="SmallFont" OnClick="btnDisTaxAdjMST_Click" />
                            </td>
                           
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" colspan="3">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="96%" TabIndex="21" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                             <td width="15%" class="tdRight">
                                <asp:Label ID="Label13" runat="server" Text="Grand Total :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td width="15%" class="tdLeft">
                                <asp:TextBox ID="txtPartyBillAmount" runat="server" TabIndex="8" Width="99px" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                    AutoPostBack="True"  ReadOnly="True"></asp:TextBox> <%--<%# Container.ItemsLoadedCount %>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
       
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            <td width="15%">
                                CR No
                            </td>
                          <%--  <td width="10%">
                                Packed Detail
                            </td>--%>
                            <td align="right" width="8%">
                                Qty Unit
                            </td>
                            <td align="right" width="8%">
                                Uom Of Unit
                            </td>
                            <td align="right" width="8%">
                                 Bal Unit
                            </td>
                            <td align="right" width="8%">
                                Total_stock
                            </td>
                            <td align="right" width="10%">
                                Basic Rate
                            </td>
                           <%-- <td align="right" width="10%">
                                Disc/Tax
                            </td>--%>
                            <td align="right" width="10%">
                                Qty
                            </td>
                            <td align="right" width="10%">
                                Amount
                            </td>
                            <td width="15%">
                                Remarks
                            </td>
                            <td width="8%">
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                <cc2:ComboBox ID="DDLPiNo" runat="server" CssClass="SmallFont" AutoPostBack="True"
                                    EnableLoadOnDemand="true" MenuWidth="800px" Width="99%" OnLoadingItems="DDLPiNo_LoadingItems"
                                    OnSelectedIndexChanged="DDLPiNo_SelectedIndexChanged" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            CR No</div>
                                        <div class="header c2">
                                            Article Code</div>
                                        <div class="header c2">
                                            Party</div>
                                        <div class="header c5">
                                            Apr.Unit</div>
                                        <div class="header c2">
                                            Bal.Unit</div>
                                        <div class="header c5">
                                          Total Stock  </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("CUSTNO") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("ARTICLE_NO") %>' />
                                        </div>
                                      <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal8" Text='<%# Eval("PRTY_DATA") %>' />
                                        </div>
                                        <div class="item c5">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("APP_NO_OF_UNIT") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("REM_INVOICE_NO_OF_UNIT") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("TOTAL_STOCK") %>' />
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
                         <%--   <td width="10%">
                                <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                                    Text="Adj.Detail.." Width="75px" Visible ="false" />
                            </td>--%>
                            <td width="8%">
                                <asp:TextBox ID="txtMaxQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="99%"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont "
                                    ReadOnly="True" Width="99%"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="txtQtyWeight" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="99%"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Width="99%" OnTextChanged="txtQTY_TextChanged1" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"
                                    AutoPostBack="True" OnTextChanged="txtBasicRate_TextChanged"></asp:TextBox>
                            </td>
                            <%--<td width="10%">
                                <asp:Button ID="btnDisc" runat="server" Text="Disc/Taxes" OnClick="btnDisc_Click"
                                    CssClass="SmallFont " Width="100%" Visible ="false" />
                            </td>--%>
                            <td width="10%"  runat="server">
                                <asp:TextBox ID="txtfinal" runat="server" CssClass="SmallFont Textbox" OnTextChanged="txtfinal_TextChanged"
                                    Width="100%" AutoPostBack="true" ></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="99%" ontextchanged="txtAmount_TextChanged"></asp:TextBox>
                            </td>
                            <td width="15%">
                                <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save" />
                            <%--</td>
                             <td width="8%">--%>
                                <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                                    Text="Cancel" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="11" width="92%">
                                 <asp:Label ID="Label27" runat="server" Text="C.R No:" CssClass="Label SmallFont tdRight"
                                           Width="10%"></asp:Label><asp:TextBox ID="txtPA_NO" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="80px"></asp:TextBox>
                                  <asp:Label ID="Label28" runat="server" Text="ArticleNo/Desc.:" CssClass="Label SmallFont tdRight"
                                           Width="10%"></asp:Label><asp:TextBox ID="txtArticleCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="120px"></asp:TextBox>
                                <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="100px"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Visible ="false"  Width="100px"></asp:TextBox>
                                
                                <asp:TextBox ID="txtQTYUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px" Visible ="false"></asp:TextBox>
                                <asp:TextBox ID="txtMaxQtyUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px" Visible ="false"></asp:TextBox>
                            </td>
                           
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                        <asp:GridView ID="grdMaterialItemIssue" runat="server" AutoGenerateColumns="False"
                            CssClass="SmallFont" Width="99%" ShowFooter="false" OnRowCommand="grdMaterialItemIssue_RowCommand"
                            OnRowDataBound="grdMaterialItemIssue_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="CR No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPICode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PI_NO") %>'
                                            ReadOnly="True" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_CODE") %>'
                                            ReadOnly="True" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'
                                            ReadOnly="True" Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Shade">
                                    <ItemTemplate>
                                        <asp:Label ID="txtSHADE_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                            ReadOnly="True" Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Qty Unit">
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
                                <asp:TemplateField HeaderText="Bal Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQtyWeight" runat="server" CssClass="Label SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Total Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TOTAL_STOCK") %>' ></asp:Label>
                                      <%--  <asp:Panel ID="pnlrecadj" runat="server" CssClass="panelclass">
                                            <asp:GridView ID="grdRecAdj" runat="server" AutoGenerateColumns="False" CssClass="SmallFont">
                                                <Columns>
                                                    <asp:BoundField HeaderText="MRN Type" DataField="TRN_TYPE" />
                                                    <asp:BoundField HeaderText="MRN Numb" DataField="TRN_NUMB" />
                                                    <asp:BoundField HeaderText="PO Comp" DataField="PO_COMP" />
                                                    <asp:BoundField HeaderText="PO Branch" DataField="PO_BRANCH" />
                                                    <asp:BoundField HeaderText="PO Type" DataField="PO_TYPE" />
                                                    <asp:BoundField HeaderText="PO Numb" DataField="PO_NUMB" />
                                                    <asp:BoundField HeaderText="Article" DataField="YARN_CODE" />
                                                    <asp:BoundField HeaderText="Shade" DataField="SHADE_CODE" />
                                                    <asp:BoundField HeaderText="Rec PA No" DataField="PI_NO" />
                                                    <asp:BoundField HeaderText="Lot No" DataField="LOT_NO" />
                                                    <asp:BoundField HeaderText="Issued Qty" DataField="ISSUE_QTY" />
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                            </asp:GridView>
                                        </asp:Panel>--%>
                                       <cc1:HoverMenuExtender ID="hvrmnrecadj" runat="server" PopDelay="500" PopupControlID="pnlrecadj"
                                            TargetControlID="txtQTY" PopupPosition="Left">
                                        </cc1:HoverMenuExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("BASIC_RATE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="txtfRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("TRN_QTY") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont"
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
        <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtIssueDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        <asp:RangeValidator ID="rv1" runat="server" ControlToValidate="txtChallanNumber"
            Display="None" ErrorMessage="Only numeric value allowed" MaximumValue="1000000"
            MinimumValue="1" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
        <cc1:ValidatorCalloutExtender ID="vcrv1" runat="server" TargetControlID="rv1">
        </cc1:ValidatorCalloutExtender>
        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtChallanNumber"
            Display="None" ErrorMessage="MRN number required" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <cc1:CalendarExtender ID="ceDoc" runat="server" TargetControlID="txtDocDate" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtDocDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        
           <cc1:CalendarExtender ID="CE3" runat="server" TargetControlID="txtdateprp" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtdateprp" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        
           <cc1:CalendarExtender ID="CE4" runat="server" TargetControlID="txtdateremvl" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtdateremvl" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="false" ValidationGroup="M1" />
        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtLCDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtLCDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
       <%-- <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExciseDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtExciseDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>--%>
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtLRDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtLRDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
<%--    </ContentTemplate>
</asp:UpdatePanel>
 --%>