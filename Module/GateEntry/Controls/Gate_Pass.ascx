<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Gate_Pass.ascx.cs" Inherits="Module_GateEntry_Controls_Gate_Pass" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>

<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 8px;
        width: 100px;
    }
    .c3
    {
        margin-left: 8px;
        width: 100px;
    }
     .c4
    {
        margin-left: 8px;
        width: 100px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
   
    </style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
<table align="left" class="tContentArial" width="80%">
<tr>
<td>
    
            <table>
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                             ValidationGroup="M1"  TabIndex="22" onclick="imgbtnSave_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                             ValidationGroup="M1" onclick="imgbtnUpdate_Click" ></asp:ImageButton>
                    </td>
                   
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" 
                            ImageUrl="~/CommonImages/link_find.png" onclick="imgbtnFind_Click" TabIndex="23"
                            ></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" 
                            ImageUrl="~/CommonImages/link_print.png"  TabIndex="24" onclick="imgbtnPrint_Click"
                            ></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" 
                            ImageUrl="~/CommonImages/clear.jpg" TabIndex="25" onclick="imgbtnClear_Click"
                            ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" 
                            ImageUrl="~/CommonImages/link_exit.png"  TabIndex="26" onclick="imgbtnExit_Click"
                            ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" 
                            ImageUrl="~/CommonImages/link_help.png" TabIndex="27"
                            ></asp:ImageButton>
                    </td>
                </tr>
            </table>
       
 
</td> 
</tr>
   <tr>
        <td align="center" class="TableHeader td">
            <span class="titleheading"><b>Gate Pass</b></span>
        </td>
    </tr>
    <tr>
        <td class="td" align="left" valign="top">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                
            </span>
          
        
        </td>
    </tr> 
    <tr>
    <td>
    <table width="100%">
                        <tr>
                           <td class="tdRight" width="15%">                               
                                      <asp:Label ID="Label8" runat="server" Text="Gate Type:" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                          <%--<asp:DropDownList ID="ddlProductType" runat="server" CssClass="SmallFont"   
                                    Width="150px"  >
                                <asp:ListItem Selected="True" Text="Yarn" Value="Yarn"></asp:ListItem>         
                                <asp:ListItem  Text="POY" Value="POY"></asp:ListItem>       
                                <asp:ListItem  Text="Material" Value="Material"></asp:ListItem>                              
                               </asp:DropDownList>--%>
                               
                               <asp:DropDownList ID="ddlGateType" runat="server" CssClass="SmallFont"   
                                    Width="150px" AutoPostBack="true" 
                                    onselectedindexchanged="ddlGateType_SelectedIndexChanged" >
                                <asp:ListItem Selected="True" Text="Gate Pass" Value="GATEPASS"></asp:ListItem>                                
                               </asp:DropDownList>                 
                               </td>
                                                
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label15" runat="server" Text="Gate Pass No : " CssClass="LabelNo SmallFont"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtGateNumber" runat="server" ValidationGroup="M1" Width="80%"      TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" ></asp:TextBox>                                    
                                <cc2:ComboBox ID="ddlGateNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlGateNumber_LoadingItems" DataTextField="Gate_NUMB" DataValueField="Gate_NUMB"
                                    OnSelectedIndexChanged="ddlGateNumber_SelectedIndexChanged" Width="80%" Height="200px" EmptyText="Select Gate No"
                                    MenuWidth="500px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Gate No #</div>
                                        <div class="header c2">
                                            Gate Date</div>                                       
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("Gate_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container6" Text='<%# Eval("Gate_DATE","{0:dd/MM/yyyy}") %>' />                                        </div>
                                       
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
                                <asp:Label ID="Label16" runat="server" Text="Gate Pass Date : " CssClass="Label tdRight SmallFont"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtGateDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="150px"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                      <cc3:CalendarExtender ID="ceGateDate" runat="server" TargetControlID="txtGateDate"
            Format="dd/MM/yyyy">
        </cc3:CalendarExtender>
                            </td>
                           
                        </tr>
                        <tr>
                            <td valign="top" class="tdRight"" width="15%">
                                <asp:Label ID="Label4" runat="server" CssClass="LabelNo" Text="Party Detail :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="cmbParty" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="cmbParty_LoadingItems" DataTextField="PRTY_CODE" DataValueField="PRTY_NAME"
                                    OnSelectedIndexChanged="cmbParty_SelectedIndexChanged" Width="99%" Height="200px"
                                    MenuWidth="550px" EnableVirtualScrolling="true" EmptyText="Select Party">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c6">
                                            NAME</div>
                                       <%-- <div class="header c4">
                                            Address</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c6">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                       <%-- <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("Address") %>' /></div>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" colspan="2">
                                <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="15%"></asp:TextBox>
                                <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="75%"></asp:TextBox>
                            </td>
                            </td>
                                <td class="tdRight" width="15%">
                                <asp:Label ID="Label9" runat="server" Text="Vehicle No :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                              
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="tdRight" width="15%">
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
                                        <div class="header c4">
                                            NAME</div>
                                       <%-- <div class="header c4">
                                            Address</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                       <%-- <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("Address") %>' /></div>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="70%" colspan="2">
                                <asp:TextBox ID="lblTransporterCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="15%"></asp:TextBox>
                                <asp:TextBox ID="txtTransporterAddress" TabIndex="5" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="75%"></asp:TextBox></td>
                                      <td class="tdRight" width="15%">
                                <asp:Label ID="Label3" runat="server" Text="Remarks:" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtRemarks" runat="server" TabIndex="16" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                              
                            </td>
                           
                        </tr>
                       
                    </table>
    </td>
    </tr>
       <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            
                            <td width="10%">
                              Details
                            </td>
                             <td width="10%">
                               Description
                            </td>
                            <td align="right" width="10%">
                              Lot&nbsp;Number
                            </td>
                             <td align="right" width="10%" >
                             Grade
                            </td>
                            <td align="right" width="10%">
                              Shade
                            </td>
                            <td align="right" width="10%">
                               D/C&nbsp;No
                            </td>
                            <td align="right" width="10%">
                               D/C&nbsp;Date
                            </td>
                            <td align="right" width="10%">
                               No&nbsp;of&nbsp;Packages
                            </td>
                            <td align="right" width="10%" >
                             Total&nbsp;Qty(In&nbsp;Kgs)
                            </td>
                           
                            <td align="right" width="10%" >
                           Invoice&nbsp;No
                            </td>
                           
                            <td width="8%">
                            </td>
                        </tr>
                        <tr>
                            <td width="10%">
                                <cc2:ComboBox ID="ddlYarnDetails" runat="server" CssClass="SmallFont" AutoPostBack="True"
                                    EnableLoadOnDemand="true" MenuWidth="750px" Width="99%" OnLoadingItems="ddlYarnDetails_LoadingItems"
                                    OnSelectedIndexChanged="ddlYarnDetails_SelectedIndexChanged" Height="200px" EmptyText="Select Details">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            DC No</div>
                                        <div class="header c2"> 
                                            Yarn </div>
                                        <div class="header c2">
                                            Shade</div>                                       
                                        <div class="header c2">
                                            Packages</div>
                                        <div class="header c2">
                                            Qty</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("TRN_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("YARN_DESC") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal8" Text='<%# Eval("SHADE_CODE") %>' />                                        </div>
                                       
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("NO_OF_UNIT") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("TRN_QTY") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:Label ID="lblYarnCode" runat="server"  Visible="false"></asp:Label>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtYarnDesc" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                     Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtLotNo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont "
                                     Width="99%"></asp:TextBox>
                            </td>
                             <td width="10%" >
                                <asp:TextBox ID="txtGrade" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont" 
                                    Width="100%"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtShade" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                     Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%">
                                <asp:TextBox ID="txtDCNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="99%" ></asp:TextBox>
                            </td>
                               <td width="10%">
                                <asp:TextBox ID="txtDCDate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="99%" ></asp:TextBox>
                            </td>
                            <td width="10%" >
                                <asp:TextBox ID="txtNoOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="99%"
                                    ></asp:TextBox>
                            </td>
                                   <td width="10%">
                                <asp:TextBox ID="txtQty" runat="server" CssClass="TextBox TextBoxDisplay SmallFont" Width="99%"></asp:TextBox>
                            </td>
                            <td width="10%" runat="server">
                                <asp:TextBox ID="txtInvoiceNumb" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                     Width="99%"></asp:TextBox>
                            </td>
                    
                            <td width="10%" align="center">
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save" Width="55px" />
                                    <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click" Width="55px"
                                    Text="Cancel" />
                            </td>
                        </tr>
                   
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                 
                        <asp:GridView ID="grdGatePass" runat="server" AutoGenerateColumns="False"
                            CssClass="SmallFont" Width="99%" ShowFooter="false" 
                            OnRowCommand="grdGatePass_RowCommand">
                            <Columns>
                             <asp:TemplateField HeaderText="SR&nbsp;No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSR_NO" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UNIQUEID") %>'      Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Yarn Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYARN_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'  ToolTip='<%# Bind("YARN_DESC") %>'   Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Lot&nbsp;No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLOT_NO" runat="server" CssClass="Label SmallFont" Text='<%# Bind("LOT_NO") %>'       Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Lot&nbsp;No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGRADE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("GRADE") %>'       Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shade">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHADE_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'       Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="D/C&nbsp;No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOC_NO" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DOC_NO") %>'       Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="D/C&nbsp;Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOC_DATE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DOC_DATE","{0:dd/MM/yyyy}") %>'       Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Packages">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNO_OF_UNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'       Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                   <asp:TemplateField HeaderText="Total&nbsp;Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQUANTITY" runat="server" CssClass="Label SmallFont" Text='<%# Bind("QUANTITY") %>'       Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINVOICE_NUMB" runat="server" CssClass="Label SmallFont" Text='<%# Bind("INVOICE_NUMB") %>'       Width="100px"></asp:Label>
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
                     
                   
                </td>
            </tr>
       
</table>
</ContentTemplate></asp:UpdatePanel>