<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yrn_Issue_Agnst_PA12.ascx.cs" Inherits="Module_Yarn_SalesWork_Controls_Yrn_Issue_Agnst_PA12" %>
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
        width: 170px;
    }
    .c6
    {
        margin-left: 2px;
        width: 110px;
    }
    .SmallFont
    {}
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
                    <b class="titleheading">Yarn Issue Agains PA</b>
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
                                <asp:Label ID="Label15" runat="server" Text="Issue Slip No :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtChallanNumber" runat="server" ValidationGroup="M1" Width="98%"
                                    TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" AutoPostBack="True"
                                    OnTextChanged="txtChallanNumber_TextChanged"></asp:TextBox>
                                <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                                    OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="98%" Height="200px" EnableVirtualScrolling="true"
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
                                <asp:Label ID="Label3" runat="server" CssClass="Label SmallFont" Text="Issue Shift :" Visible="false"></asp:Label>
                                
                                <asp:Label ID="Label2" runat="server" Text="Reprocess :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:DropDownList ID="ddlIssueShift" Width="98%" runat="server" TabIndex="1" Visible="false">
                                </asp:DropDownList>
                                
                                <asp:DropDownList ID="ddlReprocess" Width="98%" runat="server" CssClass="TextBox" TabIndex="3">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
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
                                <asp:Label ID="Label7" runat="server" Text="Document Number :" CssClass="LabelNo SmallFont" Visible="false"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="txtDocNo" runat="server" TabIndex="4" Width="98%" CssClass="TextBoxNo SmallFont" Visible="false"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label8" runat="server" Text="Doc. Date :" CssClass="Label SmallFont" Visible="false"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtDocDate" runat="server" TabIndex="5" Width="98%" CssClass="TextBox SmallFont" Visible="false"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="17%">
                                
                            </td>
                            <td align="left" valign="top" width="15%">
                                
                            </td>
                        </tr>
                        <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label19" runat="server" Text="Party Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="PRTY_NAME"
                            OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged" Width="98%" MenuWidth="450px"
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
                    <td align="right" valign="top" colspan="4">
                        <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="11%" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" Width="85%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                            
                        
                        <tr>
                            <td class="tdRight" width="17%">
                                
                                
                                <asp:Label ID="Label1" runat="server" Text="Department :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                
        
        <asp:DropDownList ID="txtDepartment" CssClass="SmallFont" AppendDataBoundItems="true"
                                    Width="98%" runat="server" TabIndex="2">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="17%">
                                
                                <asp:Label ID="Label4" runat="server" Text="Trolly and Crates No. :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" width="17%">
                                
        
        <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="6" Width="98%" CssClass="TextBox SmallFont"></asp:TextBox>
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
                        
                        <asp:Label ID="Labe44" runat="server" Text="Lot No.:" CssClass="Label SmallFont" ></asp:Label>
                        </td>
                         <td align="left" valign="top" width="17%">
                     <%--   <asp:CheckBoxList ID="ChkList" runat="server" Width="272px" Visible="false"></asp:CheckBoxList>--%>
                             <asp:TextBox ID="TxtLotIdNo" runat="server" TabIndex="14" Width="90%" CssClass="SmallFont" Visible="false" ></asp:TextBox>
                               <asp:DropDownList ID="ddlLotNo"  TabIndex="14" Width="93%" CssClass="SmallFont"
                             runat="server" onselectedindexchanged="ddlLotNo_SelectedIndexChanged" >
                              </asp:DropDownList>
                              <asp:CheckBox ID="chkLot" runat="server" Checked="false" Text=""  AutoPostBack="true" Width="5px"
                             oncheckedchanged="chkLot_CheckedChanged" />
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
                        <tr>
                        <td class="tdRight" width="17%"> <asp:Label ID="Label13" runat="server" Text="Paper Tube:" CssClass="Label SmallFont"></asp:Label> </td>
                        <td align="left" valign="top" width="15%">
                        <cc2:ComboBox ID="txtFormRefNo" runat="server" AutoPostBack="True" CssClass="smallfont"
                                            EnableLoadOnDemand="True" DataTextField="ITEM_DESC" 
                            DataValueField="PAPER_TUBE" MenuWidth="500"
                                            OnLoadingItems="txtFormRefNo_LoadingItems"  EmptyText="Select Paper Tube"
                                            EnableVirtualScrolling="true" OpenOnFocus="true" 
                            TabIndex="4" Width="210px"      Height="200px" 
                            onselectedindexchanged="txtFormRefNo_SelectedIndexChanged">
                                            <HeaderTemplate>
                                                <div class="header c2">
                                                    CODE</div>
                                                <div class="header c3">
                                                    DESCRIPTION</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c2">
                                                    <%# Eval("ITEM_CODE") %></div>
                                                <div class="item c3">
                                                    <%# Eval("ITEM_DESC") %></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox></td>
                                         <td align="right" valign="top" width="17%">
                                <asp:Label ID="lblpaperWeight" runat="server" Text="Paper Tube Weight" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left"  valign="top" width="17%">
                                <asp:TextBox ID="txtPaperTubeWeight" runat="server" Width="98%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        
                         <td align="right" valign="top" width="17%">
                                <asp:Label ID="lblPaperSize" runat="server" Text="Paper Tube Size" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td align="left"  valign="top" width="17%">
                                <asp:TextBox ID="txtPaperSize" runat="server" Width="98%"  CssClass="TextBox SmallFont"></asp:TextBox>
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
                                Unit Weight
                            </td>
                            <td width="8%">
                                No of Cheese
                            </td>
                            <td width="8%">
                                Gross Weight
                            </td>
                            <td width="8%">
                                Tare Weight
                            </td>
                            <td width="8%">
                               Net Qty
                            </td>
                            <td width="8%" >
                                Rate
                            </td>
                            <td width="8%" >
                                Amount
                            </td>
                            <td width="8%">
                                Cost Code
                            </td>
                            <td width="8%">
                                Mac Code
                            </td>
                            <td width="13%">
                                Tint
                            </td>
                           <%-- <td width="13%">
                                Trolly/Crates No.
                            </td>--%>
                            <td width="8%">
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                <cc2:ComboBox ID="DDLPiNo" runat="server" CssClass="SmallFont" EmptyText="select.." TabIndex="8"
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="1460px" OnLoadingItems="DDLPiNo_LoadingItems"
                                    EnableVirtualScrolling="true" Width="100%" OnSelectedIndexChanged="DDLPiNo_SelectedIndexChanged"
                                    Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c6">
                                            PA No</div>
                                             <div class="header c6">
                                            Order No</div>
                                        <div class="header c1">
                                            Shade</div>
                                       <%-- <div class="header c1" visible="false">
                                            Yarn Code</div>--%>
                                       <%-- <div class="header c4">
                                          Yarn Desc</div>--%>
                                           <div class="header c5">
                                          Yarn Display Desc</div>
                                           <div class="header c2">
                                            GreyLotNo</div>  
                                       
                                        <%-- <div class="header c1" visible="false" >
                                            Shade F#</div>    --%>
                                         <div class="header c2">
                                            Cheeses</div>
                                        <div class="header c2">
                                            Required Qty</div>
                                         <div class="header c2">
                                            Balance Qty</div>
                                            <div class="header c2">
                                            Machine Code</div>
                                            <div class="header c2">
                                            Planned Date</div> 
                                             <div class="header c5">
                                            Jober Name</div>
                                            <div class="header c5">
                                            Remark</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c6">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PI_NO") %>' />
                                        </div>
                                        <div class="item c6">
                                            <asp:Literal runat="server" ID="Literal14" Text='<%# Eval("ORDER_NO") %>' />
                                        </div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("SHADE_CODE") %>' />
                                        </div>
                                        <%--<div class="item c1">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("BASE_ARTICAL_CODE") %>' />
                                        </div>--%>
                                       <%-- <div class="item c4">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("BASE_ARTICAL_DESC") %>' />
                                        </div>--%>
                                         <div class="item c5">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("ASS_YARN_DESC") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal10" Text='<%# Eval("GREY_LOT_NO") %>' />
                                        </div>
                                        
                                        
                                        <%--<div class="item c1">
                                            <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("BASE_SHADE_FAMILY") %>' />
                                        </div>--%>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal11" Text='<%# Eval("CONS") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal12" Text='<%# Eval("QTY") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("QTY_REM") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal13" Text='<%# Eval("MACHINE_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal15" Text='<%# Eval("PLANNING_DATE","{0:dd/MM/yyyy}") %>' />
                                        </div>
                                         <div class="item c5">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("PRTY_NAME") %>' />
                                        </div>
                                        <div class="item c5">
                                            <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("REMARKS") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td width="8%">
                                <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                                    Width="98%" Text="Adj. Rec." TabIndex="9" />
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="txtunitweight" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="100%" ReadOnly="false"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="txtnoofunit" runat="server" CssClass="TextBoxNo  SmallFont"
                                    Width="100%" ReadOnly="false"></asp:TextBox>
                            </td>
                            <td width="8%">
                                    <asp:TextBox ID="txtGrossWeight" runat="server" CssClass="TextBoxNo  SmallFont"
                                    Width="100%"  ></asp:TextBox></td>
                            <td width="8%">
                                   <asp:TextBox ID="txtTareWeight" runat="server" CssClass="TextBoxNo  SmallFont"
                                    Width="100%" ontextchanged="txtTareWeight_TextChanged" AutoPostBack="true"  ></asp:TextBox></td>
                            <td width="8%">
                                  <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo  SmallFont" AutoPostBack="true"
                                    Width="100%" OnTextChanged="txtQTY_TextChanged1" ></asp:TextBox>
                                  <td width="8%">   <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="100%" ReadOnly="true" 
                                     ></asp:TextBox>
                                     </td>
                                  
                            </td>
                            <td width="8%">
                               <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="100%" ReadOnly="true"  
                                    ></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:DropDownList ID="ddlCostCode" Width="100%" runat="server" AppendDataBoundItems="True" TabIndex="10">
                                </asp:DropDownList>
                            </td>
                            <td width="15%">
                                <cc2:ComboBox ID="ddlMacCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    EmptyText="select..." EnableLoadOnDemand="true" EnableVirtualScrolling="true" TabIndex="11"
                                    Height="200px" MenuWidth="720px" OnLoadingItems="ddlMacCode_LoadingItems" OnSelectedIndexChanged="ddlMacCode_SelectedIndexChanged"
                                    Width="30%" Visible="false">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Mac Code</div>
                                        <div class="header c2">
                                            Mac Capacity</div>
                                        <div class="header c3 ">
                                            Mac Qty</div>
                                        <div class="header c3">
                                            Mac Cons</div>
                                        <div class="header c2 ">
                                            Mac For Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container3" runat="server" Text='<%# Eval("MACHINE_CAPACITY") %>' />
                                        </div>
                                        <div class="item c3 ">
                                            <asp:Literal ID="Literal6" runat="server" Text='<%# Eval("QTY") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("CONS") %>' />
                                        </div>
                                        <div class="item c3 ">
                                            <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("PLANNING_DATE") %>' />
                                        </div>
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
                            <asp:DropDownList ID="ddlTint" runat="server" ontextchanged="ddlTint_TextChanged" AutoPostBack="true" TabIndex="13">
                            <asp:ListItem Text="YES"  Selected="True">YES</asp:ListItem>
                            <asp:ListItem Text="NO">NO</asp:ListItem>
                            
                            </asp:DropDownList>
                                <asp:TextBox ID="txtDetRemarks" Width="99%" runat="server" CssClass="TextBox SmallFont"  Visible="false"></asp:TextBox>
                            </td>
                             <%--<td class="tdLeft">
                    
                                            <asp:Button ID="btnTrolly" runat="server" Text="Trolly/Crates No." CssClass="SmallFont "
                                                                    Width="100%" OnClick="btnTrolly_Click" />
                                        
                                        </td>--%>
                            
                            <td width="8%">
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Width="98%" Text="Save" TabIndex="14"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10" width="92%">
                                PA No:<asp:TextBox ID="txtPANo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="110px"></asp:TextBox>
                                Code/Desc: &nbsp;<asp:TextBox ID="txtYarnCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="50px"></asp:TextBox>
                                &nbsp;<asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="250px"></asp:TextBox>
                                Shade:<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="70px" Text="N/A"></asp:TextBox>
                                    Shade Family:<asp:TextBox ID="txtShadeFamily" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="70px" Text="N/A"></asp:TextBox>
                                UOM:<asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="40px"></asp:TextBox>
                                Bal.Qty:<asp:TextBox ID="txtBalQty" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="40px"></asp:TextBox>
                                   Supplier Code:<asp:TextBox ID="txtJober" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="50px"></asp:TextBox>
                                    Supplier Name:<asp:TextBox ID="txtJoberNme" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="250px"></asp:TextBox>
                                    <asp:TextBox ID="txtMachineTrnno" runat="server" Visible="false" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="10px"></asp:TextBox>
                            </td>
                            <td></td><td></td>
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
                                
                                  <asp:TemplateField HeaderText="No of Cheese">
                                    <ItemTemplate>
                                        <asp:Label ID="txtCHEESE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Gross Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGrossWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GROSS_WT") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                
                                 <asp:TemplateField HeaderText="Tare Weight">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTareWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TARE_WT") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                
                                
                                <asp:TemplateField HeaderText="Net Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                            ReadOnly="True"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="txtRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("BASIC_RATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" Visible="false">
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
                                <asp:TemplateField HeaderText="Tint">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDetRemarks" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("REMARKS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jober">
                                    <ItemTemplate>
                                        <asp:Label ID="txtJober" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("JOBER") %>'></asp:Label>
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