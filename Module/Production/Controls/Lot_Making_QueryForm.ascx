<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Lot_Making_QueryForm.ascx.cs" Inherits="Module_Production_Controls_Lot_Making_QueryForm" %>

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
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 200px;
    }
    .d2
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 180px;
    }
    
    .divPrint1
    {
    	width:100px;
    	overflow:scroll;
    }
    
	.ob_iCboITCN
	{
		position: relative;
        display:-moz-inline-stack;
        display:inline-block;
        zoom:1;
        *display:inline;
        height: 21px;
        font-size: 10px;
    	padding: 0px;
	}
	
	.ob_iCboITCN .ob_iCboTL
	{
		background-position: 0px 0px;
	}
	
	.ob_iCboTL
	{
		position: absolute;
    	font-size: 1px;
    	height: 21px;
    	width: 7px;
    	left: 0px;
    	cursor: pointer;
    	
	}
	
	.ob_iCboITCN .ob_iCboTR
	{
		background-position: -7px 0px;
	}
	
	.ob_iCboTR
	{
		position: absolute;    	
    	font-size: 1px;
    	height: 21px;
		width: 26px;
		right: 0px;
		cursor: pointer;
		
	}
	
	.ob_iCboITCN .ob_iCboTC
	{
		background-position: 0px 0px;
	}
		
	.ob_iCboTC
	{
		height: 21px;
		line-height: 21px;
		margin-left: 7px;
		margin-right: 26px;
		position: relative;
		cursor: default;
		
	}
	
	.ob_iCboITCN .ob_iCboIE
	{
	    color: #2b4c61;
	}
	
	
	
	
	
	.ob_iCboIE_ET
	{
	    color: #5b676d !important;
	    font-style: italic !important;
	}
	
	.ob_iCboIE
	{
		width: 100%;
		position: absolute;
		left: 0px;
		right: 0px;
		top:0px;
		display: block;
    	background-color: transparent;
    	border: 0px;
    	margin: 0px;
    	padding: 0px;
    	margin-top: 4px !important;
    	font-family: Verdana !important;
		font-size: 10px !important;
		height: 13px !important;
	}
	
	</style>

<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table width="100%">
            <tr>
                <td>
                    <table align="left">
                        <tr>
                            <td id="tdPrint" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" class="TableHeader td">
                    <b class="titleheading">Lot Making Query Form</b>
                </td>
            </tr>
        </table>
        <fieldset>
            <table width="100%">
                <tr>
                    <td align="right" style="width: 12%;">
                        Branch :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="SmallFont" TabIndex="1"
                            Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 12%;">
                        Lot No :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:DropDownList ID="ddlLotNo" runat="server" CssClass="SmallFont" TabIndex="1"
                            Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 12%;">
                        Machine Name: </td>
                    <td align="left" style="width: 12%;">
            <cc2:ComboBox ID="ddlMacCode" runat="server"  CssClass="SmallFont"
                EmptyText="Select Machine" EnableLoadOnDemand="true" EnableVirtualScrolling="true"
                Height="200px" MenuWidth="350px" OnLoadingItems="ddlMacCode_LoadingItems">
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
                        <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
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
                    </td>
                    <td align="right" style="width: 12%;">
                        Finish Denier: </td>
                    <td align="left" style="width: 12%;">
                <cc2:ComboBox ID="cmbYarn" runat="server"  CssClass="SmallFont"
                            DataTextField="YARN_DESC" DataValueField="YARN_CODE" EmptyText="Find Yarn" EnableLoadOnDemand="true"
                            Height="200px" MenuWidth="350px" OnLoadingItems="cmbYarn_LoadingItems"
                            EnableVirtualScrolling="true" Width="150px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header d4">
                                    Description</div>
                                    
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container4" runat="server" Text='<%# Eval("YARN_CODE") %>' />
                                </div>
                                <div class="item d4">
                                    <asp:Literal ID="Container5" runat="server" Text='<%# Eval("YARN_DESC") %>' />
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
                    <td align="right" style="width: 12%;">
                        &nbsp;From Date:
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="TxtFromDate" runat="server" TabIndex="6" Width="145px" CssClass="SmallFont"  OnTextChanged="TxtFromDate_TextChanged" ></asp:TextBox>
                        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="TxtFromDate" PopupPosition="TopLeft"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <br />
                    </td>
                    <td align="right" style="width: 12%;">
                        To Date:
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="TxtToDate" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont"
                            AutoPostBack="true"  OnTextChanged="TxtToDate_TextChanged" ></asp:TextBox>
                        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="TxtToDate" Format="dd/MM/yyyy"
                            PopupPosition="TopLeft">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 12%;">
                        Lot Type: </td>
                    <td align="left" style="width: 12%;">
            <asp:DropDownList Width="150px" TabIndex="2" CssClass="SmallFont TextBox UpperCase"
                ID="ddlLotType" runat="server" AutoPostBack="True" AppendDataBoundItems="True">
                <asp:ListItem id="select0" Text="------Select------  "></asp:ListItem>
                <asp:ListItem id="Domastic" Text="Domastic"></asp:ListItem>
                <asp:ListItem id="Export" Text="Export"></asp:ListItem>
            </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 12%;">
                        Poy Denier / FIL:</td>
                    <td align="left" style="width: 12%;">
            <cc2:ComboBox ID="txtItemCode" runat="server"  CssClass="SmallFont"
                DataTextField="FIBER_DESC" DataValueField="FIBER_CODE" EmptyText="Select POY Denier/FIL"
                EnableLoadOnDemand="true" Height="200px" MenuWidth="450px" OnLoadingItems="txtItemCode_LoadingItems"
                TabIndex="16" EnableVirtualScrolling="true" AutoPostBack="true" 
                onselectedindexchanged="txtItemCode_SelectedIndexChanged">                
                <HeaderTemplate>
                    <div class="header c1">
                        Code</div>
                    <div class="header d4">
                        Description</div>               
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c1">
                        <asp:Literal ID="Container8" runat="server" Text='<%# Eval("FIBER_CODE") %>' />
                    </div>
                    <div class="item d4">
                        <asp:Literal ID="Container9" runat="server" Text='<%# Eval("FIBER_DESC") %>' />
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
                    <td align="left" style="width: 4%;">
                        &nbsp;</td>
                        
                </tr>
               
                <tr>
                    <td align="right" style="width: 12%;">
                        Purpose :</td>
                    <td align="left" style="width: 12%;">
                <asp:DropDownList Width="150px" TabIndex="2" CssClass="SmallFont TextBox UpperCase"
                    ID="ddlPurpose" runat="server" AutoPostBack="True" AppendDataBoundItems="True">
                    <asp:ListItem id="select" Text="------Select------" Value=""></asp:ListItem>
                    <asp:ListItem id="Dyeing" Text="Dyeing"></asp:ListItem>
                    <asp:ListItem id="Twisting" Text="Twisting"></asp:ListItem>
                    <asp:ListItem id="Market" Text="Market"></asp:ListItem>
                </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 12%;">
                        Merge No:</td>
                    <td align="left" style="width: 12%;">
            
            
            <cc2:ComboBox ID="cmbLotNo" runat="server"  CssClass="SmallFont"
                DataTextField="LOT_NO" DataValueField="LOT_NO" EmptyText="Select Merge No" EnableLoadOnDemand="true"
                Height="200px" MenuWidth="350px" OnLoadingItems="cmbLotNo_LoadingItems" TabIndex="16"
                EnableVirtualScrolling="true" Width="150px" >
                <HeaderTemplate>
                    <div class="header c1">
                        Merge No</div>
                    <div class="header d1">
                                            Party</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c1">
                        <asp:Literal ID="ltr1" runat="server" Text='<%# Eval("LOT_NO")%>'></asp:Literal>
                    </div>
                    <div class="item c1">
                    <%# Eval("PRTY_NAME")%></div>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>
                    </td>
                    <td align="right" style="width: 12%;">
                        Status:</td>
                    <td align="left" style="width: 12%;">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="SmallFont" TabIndex="8"
                            Width="150px">
                            <asp:ListItem Text="------ALL------" Value="" ></asp:ListItem>
                            <asp:ListItem Text="UNCONFIRMED" Value="0" ></asp:ListItem>
                            <asp:ListItem Text="CONFIRMED" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="CLOSED" Value="2"></asp:ListItem>
                            <asp:ListItem Text="UPDATED" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 12%;">
                        &nbsp;</td>
                    <td align="left" style="width: 12%;">
                        &nbsp;</td>
                    <td align="left" style="width: 4%;">
                        &nbsp;</td>
                        
                </tr>
               
            </table>
        </fieldset>
        <table width="100%">
            <tr>
                <td align="left" width="50%">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                        <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnShow" runat="Server" Text="Get Records" OnClick="btnShow_Click" />
                    </b>
                </td>
                <td align="left" valign="top" width="50%" cssclass="Label">
                    <b>
                        <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                            <ProgressTemplate>
                                Loading...</ProgressTemplate>
                        </asp:UpdateProgress>
                    </b>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="left" class="td" width="100%">
                    <div id="divPrint1" runat="server"  width="100px" style="overflow:scroll; position:relative;min-width:100px" >
                        <asp:GridView ID="gvLotMakingQuery" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" BorderStyle="Ridge" AllowSorting="True" CellPadding="3"
                            CssClass="smallfont" EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333"
                            PagerStyle-HorizontalAlign="Left" Width="100%" >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                            
                                <asp:BoundField DataField="BRANCH_NAME" HeaderText="Branch" />                          
                                <asp:BoundField DataField="LOT_NO" HeaderText="Lot No " />
                                <asp:BoundField DataField="LOT_TYPE" HeaderText="Lot Type" />
                                <asp:BoundField DataField="FINISHED_DENIER_DESC" HeaderText="Finished Denier" />    
                                <asp:BoundField DataField="POY_DESC" HeaderText="POY" />
                                <asp:BoundField DataField="MERGE_NO" HeaderText="Merge No" />
                                <asp:BoundField DataField="MACHINE_NAME" HeaderText="Machine Name" />                                                           
                                <asp:BoundField DataField="MACHINE_SPEED" HeaderText="Machine Speed" />
                                <asp:BoundField DataField="DR" HeaderText="DR" />
                                <asp:BoundField DataField="TKP" HeaderText="TKP" />
                                <asp:BoundField DataField="SOF" HeaderText="SOF" />
                                <asp:BoundField DataField="DY" HeaderText="DY" />
                                <asp:BoundField DataField="CPM" HeaderText="CPM" />
                                <asp:BoundField DataField="PH" HeaderText="PH" />
                                <asp:BoundField DataField="SH" HeaderText="SH" />
                                <asp:BoundField DataField="OIL_RPM" HeaderText="Oil Rpm" />
                                <asp:BoundField DataField="ROTO_PRESSURE" HeaderText="Roto Pressure" />
                                <asp:BoundField DataField="JET_NO" HeaderText="Jet No" />
                                <asp:BoundField DataField="DRY_DEN" HeaderText="Dry Den" />
                                <asp:BoundField DataField="ELG" HeaderText="ELG" />
                                <asp:BoundField DataField="GPD" HeaderText="GPD" />
                                <asp:BoundField DataField="OPU" HeaderText="OPU" />
                                <asp:BoundField DataField="OIL_DEN" HeaderText="Oil Den" />
                                <asp:BoundField DataField="DOFF_TIME" HeaderText="Doff Time" />
                                <asp:BoundField DataField="DOFF_WEIGHT" HeaderText="Doff Weight" />
                                <asp:BoundField DataField="PLT" HeaderText="PLT" />
                                <asp:BoundField DataField="TINT" HeaderText="TINT" />
                                <asp:BoundField DataField="RATIO_T1_H" HeaderText="Ratio T1" />
                                <asp:BoundField DataField="RATIO_T2_L" HeaderText="Ratio T2" />
                                <asp:BoundField DataField="PURPOSE" HeaderText="Purpose " />
                                <asp:BoundField DataField="HEAT_SETTING" HeaderText="Heat Setting" />
                                <asp:BoundField DataField="FINISHED_TYPE" HeaderText="Finished Type" />                                
                                <asp:BoundField DataField="TUSER_NAME" HeaderText="User" /> 
                                <asp:BoundField DataField="CONF_BY" HeaderText="Approved By" />  
                                <asp:BoundField DataField="STATUS1" HeaderText ="Status" />                    
                                  
                               
                               </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
   
        
                             