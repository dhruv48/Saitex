 <%@ Control Language="C#" AutoEventWireup="true" CodeFile="CRForYarnSpining.ascx.cs" 
     Inherits="Module_OrderDevelopment_CustomerRequest_Controls_CRForYarnSpining" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Src="~/CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/LOV/ApproveLRLOV.ascx" TagName="ApproveLRLOV" TagPrefix="uc2" %>

<style type="text/css">
    .item
    {
	    position: relative !important;
	    display: -moz-inline-stack;
	    display: inline-block;
	    zoom: 1; overflow: hidden; white-space: nowrap;
	    
	}
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
	    	margin-left: 2px;
	    	width: 500px;
		}	
	.c3
	    {
	    	width: 150px;
		}	
	.d1
	    {
	    	width: 180px;
		}	
	.d2
	    {
	    	margin-left: 4px;
	    	width: 180px;
		}	
	.d3
	    {
	    	margin-left: 4px;
	    	width: 180px;
		}	
	.d4
	    {
	    	margin-left: 4px;
	    	width: 180px;
		}	
</style>
<style type="text/css">
    .AutoExtender
        {
	        font-family: Verdana, Helvetica, Sans-Serif;
	        font-size: .8em;
	        font-weight: normal;
	        line-height: 20px;
	        padding: 10px;
	        background-color: White;
	        margin-left: 10px;
	    }
	 .AutoExtenderList
	    {
	    	border-bottom: dotted 1px #006699;
	    	cursor: pointer;
	    	color: Maroon;
	 	}   
	 .AutoExtenderHighlight
	    {
	    	color: White;
	    	background-color: #006699;
	    	cursor: pointer;
	 	}	
	 #divwidth
	    {
	    	width: 200px !important;
	 	}	
	 #divwidth div
	    {
	    	width: 200px !important;
	 	}	
	 .SmallFont
	 {}	
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table align="left" class="tContentArial" width="100%" >
<tr>
<td align="left"  valign="top" width="100%">
<table class="td">
<tr>
<td id="tdSave" runat="server">

    <asp:ImageButton ID="imgbtnSave" runat="server" 
        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" ValidationGroup="CR" 
        onclick="imgbtnSave_Click" />
</td>
<td id="tdUpdate" runat="server">

    <asp:ImageButton ID="imgbtnUpdate" runat="server" 
        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" ValidationGroup="CR" 
        onclick="imgbtnUpdate_Click" />
</td>
<td id="tdDelete" runat="server">

    <asp:ImageButton ID="imgbtnDelete" runat="server" 
        ImageUrl="~/CommonImages/del6.png" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"  
        ToolTip="Delete" ValidationGroup="M1" Width="48px" onclick="imgbtnDelete_Click" />
</td>
<td id="tdFind" runat="server">

    <asp:ImageButton ID="imgbtnFind" runat="server" 
        ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" 
        onclick="imgbtnFind_Click" />
</td>
<td id="tdPrint" runat="server">

    <asp:ImageButton ID="imgbtnPrint" runat="server" 
        ImageUrl="~/CommonImages/link_print.png" ToolTip="Print" 
        onclick="imgbtnPrint_Click" />
</td>
<td id="tdClear" runat="server">

    <asp:ImageButton ID="imgbtnClear" runat="server" 
        ImageUrl="~/CommonImages/clear.jpg" ToolTip="Clear" 
        onclick="imgbtnClear_Click" />
</td>
<td>

    <asp:ImageButton ID="imgbtnExit" runat="server" 
        ImageUrl="~/CommonImages/link_exit.png" ToolTip="Exit" 
        onclick="imgbtnExit_Click" />
</td>
<td style="font-style: italic">
  <asp:ImageButton ID="imgbtnHelp" runat="server" 
          ImageUrl="~/CommonImages/link_help.png" ToolTip="Help" />
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="center" class="TableHeader td" width=100%>
<span class="titleheading"><b>Sales Contract For Yarn Spining</b></span>
</td>
</tr>
<tr>
<td align="left" class="td" valign="top" width="100%">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="CR" />
                    </span>
                </td>
</tr>
<tr>
<td width=100% class="td">
<table width=100%  class="td">
<tr  class="td">

 <td align="right" width="17%" >
                                Business Type :
                            </td>
                            <td align="left" width="17%" >
                                <asp:DropDownList Width="120px" CssClass="SmallFont TextBox UpperCase" ID="ddlBusinessType" 
                                    runat="server" OnSelectedIndexChanged="ddlBusinessType_SelectedIndexChanged"
                                    AppendDataBoundItems="True" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DDLBusinessType"
                                    Display="None" ErrorMessage="Please Select Business Type" InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
<td align="right" width="17%">
CR Type:
</td>
<td align="left" width="17%">
                       <asp:DropDownList Width="120px" TabIndex="2" CssClass="SmallFont TextBox UpperCase"
                                    ID="ddlOrderType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLOrderType"
                                    Display="None" ErrorMessage="Please Select Order Type " InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                            </td>
                            
                            
                             <td align="right" width="17%">
                                Delivery Mode:
                            </td>
                            <td align="left" width="15%">
                                <asp:DropDownList Width="120px" TabIndex="3" CssClass="SmallFont TextBox UpperCase"
                                    ID="ddlDeliveryMode" runat="server">
                                </asp:DropDownList>
                            </td>



                                 
                            
                      
                            </tr>
                            <tr >
                            
                             <td align="right" width="17%">
                                CR No :
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox ID="txtOrderNo" TabIndex="4" runat="server" Width="120px" CssClass="TextBox TextBoxDisplay"
                                    ValidationGroup="M1" ReadOnly="True"></asp:TextBox>
                            <cc2:ComboBox ID="cmbOrderNo" TabIndex="4" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="ORDER_NO" DataValueField="Combined" EnableLoadOnDemand="True"
                                    MenuWidth="800" OnLoadingItems="cmbOrderNo_LoadingItems" OnSelectedIndexChanged="cmbOrderNo_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" OpenOnFocus="true" Width="120px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            CR No</div>
                                        <div class="header c1">
                                            CR Location</div>
                                        <div class="header c1">
                                            CR Type</div>
                                        <div class="header c1">
                                            CR date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("ORDER_NO")%></div>
                                        <div class="item c1">
                                            <%# Eval("ORDER_CAT")%></div>
                                        <div class="item c1">
                                            <%# Eval("ORDER_TYPE")%></div>
                                        <div class="item c1">
                                            <%# Eval("ORDER_DATE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>

                            
                            
                            <td align="right" width="17%">
                                CR Date :
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtDate" runat="server" TabIndex="5" Width="120px" MaxLength="10"
                                    CssClass="SmallFont TextBoxDisplay UpperCase" ValidationGroup="M1"></asp:TextBox>
                            </td>
                           
                           
                           <td align="right" width="17%">
CR Location:
</td>
<td align="left" width="17%">
<asp:TextBox ID="txtCrLocation" runat="server" ReadOnly="true" TabIndex="6" Width="120px"
                            CssClass="SmallFont TextBox UpperCase TextBoxDisplay" ValidationGroup="M1"></asp:TextBox>
                            <br />
</td>
                           
                           
                        </tr>
                        
                        
        <tr  >
                            
                <td align="right" width="17%">
                                <asp:Label ID="lblCurrencyCode" runat="server" Text="Currency Code :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td align="left" width="17%">
                                <asp:DropDownList ID="ddlCurrencyCode" runat="server"  CssClass="SmallFont TextBox UpperCase"
                                  Width="120px" onselectedindexchanged="ddlCurrencyCode_SelectedIndexChanged" TabIndex="7">
                                </asp:DropDownList>
                            </td>
                            <td align="right" width="17%">
                                <asp:Label ID="lblConversionRate" runat="server" Text="Conversion Rate :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtConversionRate" runat="server" Width="120px" MaxLength="4"
                                    CssClass="SmallFont TextBox UpperCase" TabIndex="8"></asp:TextBox>
                            </td>              

                             <td align="right" width="17%">
                                <asp:Label ID="lblShipment" runat="server" Text="Shipment :" CssClass="SmallFont" ></asp:Label>
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtShipment" runat="server" Width="120px" MaxLength="250"
                                    CssClass="SmallFont TextBox UpperCase" TabIndex="9"></asp:TextBox>
                            </td>
                            
                           
                           

                           
                           
                        </tr>
                        
                        
                       
                        
                        <tr >
                            <td align="right" width="17%">
                                Billing Mode: </td>
                            <td align="left" width="17%">
                        <asp:DropDownList Width="120px" CssClass="SmallFont TextBox UpperCase" ID="ddlBillingMode"
                            runat="server" AppendDataBoundItems="True" TabIndex="10" AutoPostBack="True" OnSelectedIndexChanged="ddlBillingMode_SelectedIndexChanged">
                        </asp:DropDownList>
                            </td>
                            <td align="right" width="17%">
                        Agent:
                    </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtAgent" runat="server" CssClass="SmallFont TextBox" 
                                    ValidationGroup="M1" Width="115px" TabIndex="11" MaxLength="45"></asp:TextBox>
                                <div id="divwidth">
                                </div>
                                <cc3:AutoCompleteExtender ID="aceAgent" runat="server" 
                                    BehaviorID="autoComplete" CompletionInterval="1000" 
                                    CompletionListCssClass="AutoExtender" CompletionListElementID="divwidth" 
                                    CompletionListHighlightedItemCssClass="AutoExtenderHighlight" 
                                    CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="12" 
                                    EnableCaching="true" Enabled="true" MinimumPrefixLength="1" 
                                    ServiceMethod="GetAgentForCRSWLabDip" ServicePath="~/AutoComplete.asmx" 
                                    TargetControlID="txtAgent" UseContextKey="true" >
                                </cc3:AutoCompleteExtender>
                            </td>
                            <td align="right" width="17%">
                                &nbsp;</td>
                            <td align="left" width="15%">
                                <asp:Button ID="btnOther" runat="server" TabIndex="12" Text="Show Other" OnClick="btnOther_Click"
                                    Width="80px" />
                            </td>
                        </tr>
                        <tr id="trOther" runat="server" visible="false">
                            <td class="tdRight" colspan="6"  >
                                <asp:Panel ID="pnlOtherDTL" runat="server" BackColor="LightGray" BorderStyle="Ridge"
                                    BorderWidth="5px">
                                    <table>
                                        <tr>
                                          <td align="right" CssClass = "SmallFont">
                                                Pre - Carriage By :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPreCarriage" runat="server" CssClass="TextBox SmallFont"
                                                    TabIndex="13" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" CssClass = "SmallFont">
                                               Place of receipt :
                                            </td>
                                            <td align="left" >
                                                <asp:TextBox ID="txtPlaceToReceipt" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="14" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" CssClass = "SmallFont">
                                               Port of Loading :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPortofLoading" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="15" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" CssClass = "SmallFont">
                                               Port of Discharge :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtPortOfDischarge" runat="server" 
                                                    CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="16" Width="80px"></asp:TextBox>
                                            </td>
                                            
                                            <td align="right" CssClass = "SmallFont">
                                               Mark & Nos. Container No. :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtNoOfContainer" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="17" Width="80px"></asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                           <td align="right" CssClass = "SmallFont">
                                               Exporter's Ref:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtnoofpackages" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="18" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" CssClass = "SmallFont">
                                               SC/ NO. :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLCNo" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="19" Width="80px"></asp:TextBox>
                                            </td>
                                          
                                            <td align="right" CssClass = "SmallFont">
                                               SC/NO. Date :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLCDate" runat="server" CssClass="TextBox SmallFont" TabIndex="20"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                           <td align="right" CssClass = "SmallFont">
                                                Vessel No. :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtvesselNo" runat="server" CssClass="TextBox SmallFont"
                                                    TabIndex="21" Width="80px"></asp:TextBox>
                                            </td>
                                            
                                             <td align="right" CssClass = "SmallFont">
                                               Buyer's Date :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextBox1" runat="server" CssClass="TextBox SmallFont" TabIndex="22"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                           <%-- <td align="right" CssClass = "SmallFont">
                                                Sale Agaist :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleAgainst" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="28" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                            <tr>
                                           
                                            <td align="right" CssClass = "SmallFont">
                                               Freight Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtFreight" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                                    MaxLength="18" OnTextChanged="txtFreight_TextChanged" TabIndex="31" ValidationGroup="M1"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                             
                                            <td align="right" CssClass = "SmallFont">
                                                Insurance Amount:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInsurance" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                                    MaxLength="18" OnTextChanged="txtInsurance_TextChanged" TabIndex="32" ValidationGroup="M1"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                        
                                           
                                            <td align="right" CssClass = "SmallFont">
                                                Sale TAX Type:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlSaleTax" runat="server" CssClass="SmallFont" TabIndex="44"
                                                    Width="80">
                                                    <asp:ListItem Selected="True" Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Text="VAT" Value="VAT"></asp:ListItem>
                                                    <asp:ListItem Text="CST" Value="CST"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" CssClass = "SmallFont">
                                                Sale TAX Rate :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleTAXRate" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="12"
                                                    OnTextChanged="txtSaleTAXRate_TextChanged" TabIndex="45" ValidationGroup="M1"
                                                    Width="80px" AutoPostBack="True"></asp:TextBox>
                                            </td>
                                              
                                            <td align="right" CssClass = "SmallFont">
                                                Sale TAX Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleTAXAmt" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                    MaxLength="18" ReadOnly="True" TabIndex="46" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                           </tr>--%>
                                        <tr>   
                                             <td align="right" CssClass = "SmallFont">
                                               No & kind Of Packages:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtpackages" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="23" Width="80px"></asp:TextBox>
                                            </td>
                                             
                                            <td align="right" CssClass = "SmallFont">
                                               Final Destination
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDestination" runat="server" CssClass="TextBox SmallFont" MaxLength="75"
                                                    TabIndex="24" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" CssClass = "SmallFont">
                                               Country of Origin Of Goods.
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TxtofOG" runat="server" CssClass="TextBox SmallFont" MaxLength="75"
                                                    TabIndex="25" Width="80px"></asp:TextBox>
                                            </td>
                                             <td align="right" CssClass = "SmallFont">
                                               Country of Final Destination.
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TxtofFD" runat="server" CssClass="TextBox SmallFont" MaxLength="75"
                                                    TabIndex="2" Width="80px"></asp:TextBox>
                                            </td>
                                             <td align="right" CssClass = "SmallFont">
                                               Delevery:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDelevery" runat="server" CssClass="TextBox SmallFont" MaxLength="100"
                                                    TabIndex="27" Width="80px" Text="WITHIN 4-5 DAYS."></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td align="center" colspan="8">
                                                <asp:Button ID="btncncelpack" runat="server" OnClick="btncncelpack_Click" TabIndex="28"
                                                    Text="Hide Others" ValidationGroup="M1" Width="100px" />
                                                    </td>
                                           <%-- <td align="center" colspan="8">
                                                <asp:Button ID="btncncelpack" runat="server" OnClick="btncncelpack_Click" TabIndex="48"
                                                    Text="Hide Others" ValidationGroup="M1" Width="100px" />
                                                <asp:RangeValidator ID="RVFreight" runat="server" ControlToValidate="txtFreight"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Freight" MaximumValue="9999999"
                                                    MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVInsurance" runat="server" ControlToValidate="txtInsurance"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Insurance" MaximumValue="9999999"
                                                    MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVSaleTAXRate" runat="server" ControlToValidate="txtSaleTAXRate"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Sale TAX Rate" MaximumValue="100"
                                                    MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVSaleTAXAmt" runat="server" ControlToValidate="txtSaleTAXAmt"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Sale TAX Amount" MaximumValue="9999999"
                                                    MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                               --%>
                                  </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        </td>
                        </tr>
                        
                        
                        
                        
                        
                        <tr >
                            <td align="right" class="" width="17%">
                                Customer Name :
                            </td>
                            <td align="left"  colspan="4">
                                <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" DataTextField="PRTY_CODE"
                            DataValueField="Address" EmptyText="Select Party" EnableLoadOnDemand="true" Height="200px"
                            MenuWidth="800px" OnLoadingItems="cmbPartyCode_LoadingItems" OnSelectedIndexChanged="cmbPartyCode_SelectedIndexChanged"
                            TabIndex="29" Width="120px" EnableVirtualScrolling="true" >
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c3">
                                    NAME</div>
                                <div class="header c2">
                                    Address</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("Address") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        
                      <%--  </td>--%>
                           <%-- <td align="left" colspan="4" class="tdLeft" width="66%">--%>
                                <asp:TextBox ID="txtPartyCode" TabIndex="14" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ReadOnly="true" ValidationGroup="M1" Width="150px"></asp:TextBox>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                                    ReadOnly="true" TabIndex="15" ValidationGroup="M1" Width="450px"></asp:TextBox>
                          </td>
                        </tr>
                        <tr >
                            <td align="right" width="17%">
                               <%-- &nbsp;Direct Billing :--%>Tolerance : 
                            </td>
                            <td align="left" width="17%" id=tdDirectBilling runat=server >
                                <asp:TextBox ID="txtDirectBilling" runat="server" TabIndex="30" Width="120px" CssClass="SmallFont TextBox UpperCase"
                                    ValidationGroup="M1" MaxLength="45" Visible=false></asp:TextBox>
                                    <asp:TextBox ID="txtTolerance" runat="server" TabIndex="30" Width="120px" CssClass="SmallFont TextBox UpperCase"
                                    ValidationGroup="M1" MaxLength="45"></asp:TextBox>
                            </td>
                            <td align="right" width="17%">
                                Document No:
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtCustomerReffNo" TabIndex="31" runat="server" Width="120px" CssClass="SmallFont TextBox UpperCase"
                                    ValidationGroup="M1" MaxLength="24"></asp:TextBox>
                            </td>
                            <td align="right" width="17%">
                                Document Date :
                            </td>
                            <td align="left" width="15%">
                                <asp:TextBox ID="TxtDocumentDate" runat="server" TabIndex="32" Width="120px" CssClass="SmallFont TextBox UpperCase"
                                    ValidationGroup="M1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvod" runat="server" ControlToValidate="TxtDocumentDate"
                                    Display="Dynamic" ErrorMessage="*Please select the Document Date" Font-Bold="False" ValidationGroup="CR"></asp:RequiredFieldValidator>
                                <cc3:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                    MaskType="Date" TargetControlID="TxtDocumentDate" PromptCharacter="_">
                                </cc3:MaskedEditExtender>
                                <cc3:CalendarExtender ID="CE1" Format="dd/MM/yyyy" runat="server" TargetControlID="TxtDocumentDate">
                                </cc3:CalendarExtender>
                            </td>
                        </tr>
                        
                                           <tr  >
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlPaymentMode" runat="server"  MenuWidth="200px"
                                    CssClass="SmallFont" Width="120px" 
                                    onselectedindexchanged="ddlPaymentMode_SelectedIndexChanged" TabIndex="33">
                                    <asp:ListItem Text="DD" Value="DD"></asp:ListItem>
                                    <asp:ListItem Text="CHEQUE" Value="CHEQUE"></asp:ListItem>
                                    <asp:ListItem Text="CASH" Value="CASH"></asp:ListItem>
                                      <asp:ListItem Text="RTGS" Value="RTGS"></asp:ListItem>
                                    <asp:ListItem Text="NEFT" Value="NEFT"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblPaymentTerm" runat="server" Text="Payment Term :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="64%" colspan=4>
                                <asp:TextBox ID="txtPaymentTerm" runat="server" CssClass="TextBox SmallFont" Width="100%"
                                    MaxLength="450" TabIndex="34"></asp:TextBox>
                            </td>
                        </tr>
                           <tr  >
                            <td class="tdRight" width="12%">
                                <asp:Label ID="lblBillTo" runat="server" Text="Bill To :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="88%" colspan="6">
                                <asp:TextBox ID="txtBillTo" runat="server" CssClass="TextBox SmallFont" Width="100%"
                                    MaxLength="450" TabIndex="35"></asp:TextBox>
                            </td>
                        </tr>
                        
                           <tr  >
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label1" runat="server" Text="Delevered To:" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="88%" colspan="6">
                                <asp:TextBox ID="txtDeleveredTo" runat="server" CssClass="TextBox SmallFont" Width="100%"
                                    MaxLength="450" TabIndex="36"></asp:TextBox>
                            </td>
                        </tr>
                           <tr  >
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label2" runat="server" Text="Tax Against :" CssClass="SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="88%" colspan="6">
                                <asp:TextBox ID="txtTaxAgainst" runat="server" CssClass="TextBox SmallFont" Width="100%"
                                    MaxLength="450" TabIndex="37"></asp:TextBox>
                            </td>
                        </tr>
                        <tr  >
                            <td align="right" width="17%">
                                &nbsp;Remarks:
                            </td>
                            <td align="left" width="83%" colspan="5">
                                &nbsp;<asp:TextBox ID="txtMstRemarks" runat="server" TabIndex="38" CssClass="SmallFont TextBox UpperCase"
                                    ValidationGroup="M1" Width="99.50%" MaxLength="450"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%" >
                        <tr>
                            <td class="td" width="100%">
                                <table width="100%" class="td">
                                    <tr bgcolor="#006699">
                                        <td width="20%" align="center">
                                            <span class="titleheading"><b>Gray&nbsp;Yarn&nbsp;Code</b></span>
                                        </td>
                                        <td width="20%" align="center">
                                            <span class="titleheading"><b>Shade&nbsp;Family/Shade&nbsp;Code</b></span>
                                        </td>
                                        <%--<td width="10%">
                                            <span class="titleheading"><b>Matching Reff</b></span>
                                        </td>--%>
                                        <td class="tdRight" width="10%" align="center">
                                            <span class="titleheading"><b>Qty</b></span>
                                        </td>
                                        <td width="10%" align="center">
                                            <span class="titleheading"><b>Sale&nbsp;Rate/Unit</b></span>
                                        </td>
                                         <td width="10%" align="center">
                                            <span class="titleheading"><b>Tax/Disc.</b></span>
                                        </td>
                                          <td width="10%" align="center">
                                            <span class="titleheading"><b>Final&nbsp;Rate</b></span>
                                        </td>
                                        <td width="10%" align="center">
                                            <span class="titleheading"><b>Total&nbsp;Cost</b></span>
                                        </td>
                                        <td width="10%" align="center">
                                            <span class="titleheading"><b>Reqn&nbsp;Date</b></span>
                                        </td>
                                        <td width="20%" align="center">
                                            <span class="titleheading"><b>Remarks</b></span>
                                        </td>
                                        <td width="20%">
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" width="20%">
                                            <cc2:ComboBox ID="cmbArticleNo" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="FIBER_CODE" DataValueField="Combined" EnableLoadOnDemand="True"
                                                MenuWidth="800" OnLoadingItems="cmbArticleNo_LoadingItems" OnSelectedIndexChanged="cmbArticleNo_SelectedIndexChanged"
                                                EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="39" Visible="true"
                                                Width="100%" Height="200px">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        Gray Fiber Code</div>
                                                    <div class="header c2">
                                                        Gray Fiber Desc</div>
                                                    <div class="header c3">
                                                        Fiber Cat</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <%# Eval("FIBER_CODE") %></div>
                                                    <div class="item c2">
                                                        <%# Eval("FIBER_DESC") %></div>
                                                    <div class="item c3">
                                                        <%# Eval("FIBER_CAT")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>
                                        </td>
                                        <td align="left" valign="top" width="20%">
                                        
                                        <asp:DropDownList ID="ddlYarnShade" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="24" Width="127" onselectedindexchanged="ddlYarnShade_SelectedIndexChanged" AutoPostBack="true" 
                                                   >
                                                </asp:DropDownList>
                                             <%--<cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="Combined" EnableLoadOnDemand="True"
                                                MenuWidth="400" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="16"
                                                Height="200px" Visible="true" Width="100%" OnLoadingItems="cmbShade_LoadingItems"
                                                OnSelectedIndexChanged="cmbShade_SelectedIndexChanged">
                                                <HeaderTemplate>
                                                    
                                                    <div class="header d2">
                                                        Shade Family Name</div>
                                                    
                                                    <div class="header d4">
                                                        Shade Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    
                                                    <div class="item d2">
                                                        <%# Eval("SHADE_FAMILY_NAME")%></div>
                                                    
                                                    <div class="item d4">
                                                        <%# Eval("SHADE_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>--%>
                                        </td>
                                        <%--<td align="left" valign="top" width="10%">
                                            
                                        </td>--%>
                                        <td align="left" valign="top" width="10%">
                                            <asp:TextBox ID="txtNoofUnit" runat="server" CssClass="SmallFont TextBoxNo" Width="95%"
                                                TabIndex="40" OnTextChanged="txtNoofUnit_TextChanged" MaxLength="8"></asp:TextBox>
                                                <cc3:FilteredTextBoxExtender ID="FiltertxtNoofUnit" runat="server"  TargetControlID="txtNoofUnit"   FilterType="Custom, Numbers" ValidChars="."/>
                                                <uc2:ApproveLRLOV ID="txtMatchingReff" Width="100%" TabIndex="18" runat="server"  Visible="false" />
                                        </td>
                                        <td align="left" valign="top" width="10%">
                                        <asp:TextBox ID="txtSaleRate" runat="server"  CssClass="SmallFont TextBoxNo" 
                                                MaxLength="6"  OnTextChanged="txtsalerate_TextChanged"   AutoPostBack="True" 
                                                TabIndex="40"></asp:TextBox>
                                        <cc3:FilteredTextBoxExtender ID="FilteredTexttxtSaleRate" runat="server"  TargetControlID="txtSaleRate"   FilterType="Custom, Numbers" ValidChars="."/>
                                            <asp:DropDownList CssClass="SmallFont TextBox UpperCase" TabIndex="41" ID="txtEndUse"
                                                runat="server" AppendDataBoundItems="true" Width="100%" Visible="false">
                                            </asp:DropDownList>
                                        </td>
                                         <td >
                                <asp:Button ID="btnDisc" runat="server" Text="Disc/Taxes" OnClick="btnDisc_Click"
                                    CssClass="SmallFont " Width="100%" TabIndex="42" />
                                        <%--<td align="left" valign="top" width="8%">--%>
                                 </td>
                                   <td>
                                <asp:TextBox ID="txtfinal" runat="server" CssClass="SmallFont TextboxNo" OnTextChanged="txtfinal_TextChanged"
                                    Width="100%"></asp:TextBox>
                            </td>
                            <td>
                                        <asp:TextBox ID="txtTotalCost" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            Width="80px" ontextchanged="txtTotalCost_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </td>
                                
                                        <td align="left" valign="top" width="10%">
                                    <asp:TextBox ID="txtReqDate" runat="server" TabIndex="44" Width="60px" 
                                                CssClass="SmallFont TextBox UpperCase"></asp:TextBox>
                                          </td>
                                        <td align="left" valign="top" width="20%" rowspan="2">
                                            <asp:TextBox ID="txtRemarks" runat="server" TabIndex="45" CssClass="SmallFont" 
                                                Width="95%" Height="40px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" width="10%">
                                            <asp:Button ID="btnSTSave" runat="server" CssClass="SmallFont" TabIndex="46" OnClick="btnSTSave_Click"
                                                Text=" Save "  />
                                          
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" colspan="6" width="90%">
                                            Gray Yarn Code :                   Gray Yarn Code :<asp:TextBox ID="txtItemCodeLabel" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                                Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                            Shade Family :<asp:TextBox ID="txtShadeFamilyName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="100px"></asp:TextBox>
                                            &nbsp;Shade Code :<asp:TextBox ID="txtShadeName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="120px"></asp:TextBox>
                                            <asp:TextBox ID="ddlShadeCode" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Visible="false" Width="1px"></asp:TextBox>
                                        </td>
                                        <td align = "right" colspan="5">
                                            <asp:Button ID="btnSTCancel" runat="server" CssClass="SmallFont" TabIndex="47" OnClick="btnSTCancel_Click"
                                                Text="Cancel" width ="50px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <table width="100%">
                                    <tr id="tr4" runat="server">
                                        <td id="Td4" runat="server" align="left" class="td" width="100%">
                                            <asp:GridView ID="GridSpinningThread" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                CssClass="SmallFont" Font-Bold="False" OnRowCommand="GridSpinningThread_RowCommand"
                                                Width="100%" TabIndex="39">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Article No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtArticleNo" runat="server" Font-Bold="true" Text='<%# Bind("ARTICLE_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="Shade Family">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtShadeFamily" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_FAMILY_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shade Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtShade" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>                                                                                                 
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtQuantity" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                                Text='<%# Bind("QUANTITY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Case/Box">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtNoOfCones" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                                Text='<%# Bind("CR_UNIT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="Sale&nbsp;Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtSale" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SALE_RATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Final Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtfRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("FINAL_RATE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTotalCost" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TOTAL_COST") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Use."  Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtEndUse" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("END_USE_NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Req Date.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtReqDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REQ_DATE" , "{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtRemarks" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                                Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                                CommandName="EditItem" Text="Edit" />
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                                CommandName="DelItem" Text="Delete" />
                                                            <asp:Label ID="txtMatchingReff" runat="server" CssClass="LabelNo SmallFont" 
                                                                Text='<%# Bind("MATCHING_REFF") %>' Visible="false"></asp:Label>
                                                        
                                                        </ItemTemplate>

<HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"></HeaderStyle>

                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                                <RowStyle CssClass="SmallFont" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Label ID="lblUNIT_WT" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="lblNETBOX_WT" runat="server" Visible="false" Text=""></asp:Label>
        <asp:Label ID="lblNETCART_WT" runat="server" Visible="false" Text=""></asp:Label>
 <p>
     &nbsp;</p>
<cc3:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
    TargetControlID="txtReqDate">
</cc3:MaskedEditExtender>
<cc3:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
    TargetControlID="txtReqDate">
</cc3:CalendarExtender>
 <%--<cc3:CalendarExtender ID="ceIssueDate" runat="server" TargetControlID="txtIssueDate"
            Format="dd/MM/yyyy">
        </cc3:CalendarExtender>
        <cc3:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtIssueDate" PromptCharacter="_">
        </cc3:MaskedEditExtender>
        <asp:RangeValidator ID="rv1" runat="server" ControlToValidate="txtChallanNumber"
            Display="None" ErrorMessage="Only numeric value allowed" MaximumValue="1000000"
            MinimumValue="1" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
        <cc3:ValidatorCalloutExtender ID="vcrv1" runat="server" TargetControlID="rv1">
        </cc3:ValidatorCalloutExtender>--%>
      <%--  <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtChallanNumber"
            Display="None" ErrorMessage="MRN number required" ValidationGroup="M1"></asp:RequiredFieldValidator>--%>
        <cc3:CalendarExtender ID="ceDoc" runat="server" TargetControlID="TextBox1" Format="dd/MM/yyyy">
        </cc3:CalendarExtender>
        <cc3:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="TextBox1" PromptCharacter="_">
        </cc3:MaskedEditExtender>
        <%--<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
            ShowSummary="false" ValidationGroup="M1" />--%>
        <cc3:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtLCDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc3:CalendarExtender>
        <cc3:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtLCDate" PromptCharacter="_">
        </cc3:MaskedEditExtender>
        <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtExciseDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtExciseDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>--%>
     <%--   <cc3:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtLRDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc3:CalendarExtender>--%>
        <%--<cc3:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtLRDate" PromptCharacter="_">
        </cc3:MaskedEditExtender>--%>
 <%--</ContentTemplate>
</asp:UpdatePanel>--%>
</ContentTemplate>
</asp:UpdatePanel>