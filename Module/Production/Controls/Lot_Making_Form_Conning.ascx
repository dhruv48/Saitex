<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Lot_Making_Form_Conning.ascx.cs" Inherits="Module_Production_Controls_Lot_Making_Form_Conning"  %>
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
	    	
	    	width: 200px;
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
         TabIndex="49" onclick="imgbtnSave_Click1"  />
</td>
<td id="tdUpdate" runat="server" visible="false">

    <asp:ImageButton ID="imgbtnUpdate" runat="server" 
        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" ValidationGroup="CR" 
        onclick="imgbtnUpdate_Click" TabIndex="49"/>
</td>
<td id="tdDelete" runat="server" visible="false">

    <asp:ImageButton ID="imgbtnDelete" runat="server" 
        ImageUrl="~/CommonImages/del6.png" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"  
        ToolTip="Delete" ValidationGroup="M1" Width="48px" onclick="imgbtnDelete_Click" />
</td>
<td id="tdFind" runat="server">

    <asp:ImageButton ID="imgbtnFind" runat="server" 
        ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" 
        onclick="imgbtnFind_Click"  TabIndex="50"/>
</td>
<td id="tdPrint" runat="server">

    <asp:ImageButton ID="imgbtnPrint" runat="server" 
        ImageUrl="~/CommonImages/link_print.png" ToolTip="Print" 
        onclick="imgbtnPrint_Click" TabIndex="51"/>
</td>
<td id="tdClear" runat="server">

    <asp:ImageButton ID="imgbtnClear" runat="server" 
        ImageUrl="~/CommonImages/clear.jpg" ToolTip="Clear" 
        onclick="imgbtnClear_Click" TabIndex="52"/>
</td>
<td>

    <asp:ImageButton ID="imgbtnExit" runat="server" 
        ImageUrl="~/CommonImages/link_exit.png" ToolTip="Exit" 
        onclick="imgbtnExit_Click" TabIndex="53"/>
</td>
<td style="font-style: italic">
  <asp:ImageButton ID="imgbtnHelp" runat="server" 
          ImageUrl="~/CommonImages/link_help.png" ToolTip="Help" TabIndex="54"/>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td align="center" class="TableHeader td" width=100%>
    <span class="titleheading"><b>Lot Making Form </b> <asp:Label  ID="lblHeadingName" runat="server" ></asp:Label></span>
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
    <tr class="td">
        <td align="right">
            Tex/Tws Denier/FIL:
        </td>
        <td align="left">
            <cc2:ComboBox ID="txtItemCode" runat="server"  CssClass="SmallFont"
                DataTextField="YARN_DESC" DataValueField="YARN_CODE" EmptyText="Select Yarn Denier/FIL"
                EnableLoadOnDemand="true" Height="200px" MenuWidth="450px" OnLoadingItems="txtItemCode_LoadingItems"
                TabIndex="1"  AutoPostBack="true"  EnableVirtualScrolling="true"
                onselectedindexchanged="txtItemCode_SelectedIndexChanged">                
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
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="txtItemCode"
                Display="Dynamic" ErrorMessage="Pls enter Texturised Yarn." SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
          Tex/Tws Lot No:
        </td>
        <td align="left">
            
            
            <cc2:ComboBox ID="cmbLotNo" runat="server"  CssClass="SmallFont"
                DataTextField="LOT_NO" DataValueField="PRTY_NAME" EmptyText="Select Lot No" EnableLoadOnDemand="true"
                Height="200px" MenuWidth="350px" OnLoadingItems="cmbLotNo_LoadingItems" TabIndex="2" AutoPostBack="true"
                EnableVirtualScrolling="true" 
                onselectedindexchanged="cmbLotNo_SelectedIndexChanged">
                <HeaderTemplate>
                    <div class="header c1">
                        Lot No</div>
                    <div class="header d1">
                        Party
                     </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c1">
                        <asp:Literal ID="ltr1" runat="server" Text='<%# Eval("LOT_NO")%>'></asp:Literal>
                    </div>
                    <div class="item d1">
                    <%# Eval("PRTY_NAME")%></div>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="cmbLotNo"
                Display="Dynamic" ErrorMessage="Pls select Lot No." SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
           
        </td>
        <td align="right">
            T1:
        </td>
        <td align="left" width="15%">
            <asp:TextBox ID="txtt1_1" runat="server" Width="67px" MaxLength="3" CssClass="SmallFont TextBox UpperCase"
                TabIndex="5" Height="17px" AutoPostBack="true" OnTextChanged="txtt1_1_TextChanged"></asp:TextBox>
            /<asp:TextBox ID="txtt1_3" runat="server" Width="67px" MaxLength="3" CssClass="SmallFont TextBox UpperCase"
                TabIndex="6" Height="17px" AutoPostBack="true" OnTextChanged="txtt1_3_TextChanged"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            Machine Name:
        <td align="left">
            <cc2:ComboBox ID="ddlMacCode" runat="server"  CssClass="SmallFont"
                EmptyText="Select Machine" EnableLoadOnDemand="true" EnableVirtualScrolling="true" TabIndex="3"
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
        </td>
        <td align="right">
            Finished Denier:
        </td>
        <td align="left">            
                <cc2:ComboBox ID="cmbYarn" runat="server"  CssClass="SmallFont"
                            DataTextField="YARN_DESC" DataValueField="YARN_CODE" EmptyText="Find Finished Denier" EnableLoadOnDemand="true"
                            Height="200px" MenuWidth="350px" OnLoadingItems="cmbYarn_LoadingItems"
                            EnableVirtualScrolling="true" Width="150px"  TabIndex="4">
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
        <td align="right">
            T2:
        </td>
        <td align="left" width="17%">
            <asp:TextBox ID="txtt1_2" runat="server" Width="67px" MaxLength="3" CssClass="SmallFont TextBox UpperCase"
                AutoPostBack="true" TabIndex="7" Height="17px" OnTextChanged="txtt1_2_TextChanged"></asp:TextBox>
            /<asp:TextBox ID="txtt1_4" runat="server" Width="67px" MaxLength="3" CssClass="SmallFont TextBox UpperCase"
                TabIndex="8" Height="17px" OnTextChanged="txtt1_4_TextChanged" AutoPostBack="true"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            Lot No:
        </td>
        <td align="left">
            <asp:TextBox ID="txtLotNo" runat="server" Width="150px" MaxLength="20" CssClass="SmallFont TextBox UpperCase" onkeyup="javascript:this.value = this.value.toUpperCase();"
                TabIndex="9" Height="17px"></asp:TextBox>
            <cc2:ComboBox ID="cmbFindLotNo" runat="server" AutoPostBack="True" CssClass="SmallFont"
                DataTextField="LOT_NO" DataValueField="LOT_NO" EmptyText="Select Lot No" EnableLoadOnDemand="true"
                Height="200px" MenuWidth="170px" TabIndex="9" EnableVirtualScrolling="true"
                OnLoadingItems="cmbFindLotNo_LoadingItems" Visible="false" OnSelectedIndexChanged="cmbFindLotNo_SelectedIndexChanged">
                <HeaderTemplate>
                    <div class="header c1">
                        Lot No</div>
                  
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c1">
                        <asp:Literal ID="ltr2" runat="server" Text='<%# Eval("LOT_NO")%>'></asp:Literal>
                    </div>
                   
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtLotNo"
                Display="Dynamic" ErrorMessage="Pls enter Lot No." SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            Lot Type:
        </td>
        <td align="left">
            <asp:DropDownList Width="150px" TabIndex="10" CssClass="SmallFont TextBox UpperCase"
                ID="ddlLotType" runat="server" AutoPostBack="True" AppendDataBoundItems="True">
                <asp:ListItem id="select0" Value="">------Select------</asp:ListItem>
                <asp:ListItem id="Domastic" Value="Domastic">Domastic</asp:ListItem>
                <asp:ListItem id="Export" Value="Export">Export</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right">
            Ratio (%)
        </td>
        <td align="left" width="17%" class="style3">
            <asp:TextBox ID="txtRatio" runat="server" Width="67px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                 Height="17px"></asp:TextBox>
            /<asp:TextBox ID="txtRatio1" runat="server" Width="67px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                 Height="17px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            Machine Speed:
        </td>
        <td align="left">
            <asp:TextBox ID="txtMachineSpeed" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="11" Height="17px"></asp:TextBox>
        </td>
        <td align="right">
            Oil Rpm
        </td>
        <td align="left">
            <asp:TextBox ID="txtOilrpm" runat="server" Width="67px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="19" Height="17px"></asp:TextBox>
            <asp:TextBox ID="txtOilrpm0" runat="server" Width="67px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="20" Height="17px"></asp:TextBox>
        </td>
        <td align="right">
            Doff Time(hrs):
        </td>
        <td align="left" width="17%" class="style52">
            <asp:TextBox ID="txtDoffTime" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="28" Height="17px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            DR:
        </td>
        <td align="left">
            <asp:TextBox ID="txtdr" runat="server" Width="150px" MaxLength="8" CssClass="SmallFont TextBox UpperCase"
                TabIndex="12" Height="17px"></asp:TextBox>
            <cc3:FilteredTextBoxExtender ID="FilteredTexttxtdr" runat="server" TargetControlID="txtdr"
                FilterType="Custom, Numbers" ValidChars="." />
        </td>
        <td align="right">
            Roto Pressure:
        </td>
        <td align="left">
            <asp:TextBox ID="txtRotoPressure" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="21" Height="17px"></asp:TextBox>
        </td>
        <td align="right">
            Doff Weight(kg):
        </td>
        <td align="left" width="17%">
            <asp:TextBox ID="txtDoffWeight" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="29" Height="17px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            SOF:
        </td>
        <td align="left" class="style12">
            <asp:TextBox ID="txtsof" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="13" Height="17px"></asp:TextBox>
            <cc3:FilteredTextBoxExtender ID="FilteredTexttxtsof" runat="server" TargetControlID="txtsof"
                FilterType="Custom, Numbers" ValidChars="." />
        </td>
        <td align="right">
            &nbsp;Jet No:
        </td>
        <td align="left">
            <asp:TextBox ID="txtJetNo" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="22" Height="17px"></asp:TextBox>
        </td>
        <td align="right">
            PLT:
        </td>
        <td align="left" width="15%">
            <asp:TextBox ID="txtplt" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="30" Height="17px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            TKP :
        </td>
        <td align="left" class="style12">
            <asp:TextBox ID="txttkp" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="14" Height="17px"></asp:TextBox>
            <cc3:FilteredTextBoxExtender ID="FilteredTexttxttkp" runat="server" TargetControlID="txttkp"
                FilterType="Custom, Numbers" ValidChars="." />
        </td>
        <td align="right">
            Dry Den:
        </td>
        <td align="left">
            <asp:TextBox ID="txtdryden" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="23" Height="17px"></asp:TextBox>
            <cc3:FilteredTextBoxExtender ID="FilteredTexttxtdryden" runat="server" TargetControlID="txtdryden"
                FilterType="Custom, Numbers" ValidChars="." />
        </td>
        <td align="right">
            TINT:
        </td>
        <td align="left" width="15%">
            <asp:TextBox ID="txttint" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="31" Height="17px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            DY:
        </td>
        <td align="left">
            <asp:TextBox ID="txtdy" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="15" Height="17px"></asp:TextBox>
            <cc3:FilteredTextBoxExtender ID="FilteredTexttxtdy" runat="server" TargetControlID="txtdy"
                FilterType="Custom, Numbers" ValidChars="." />
        </td>
        <td align="right">
            ELG:
        </td>
        <td align="left">
            <asp:TextBox ID="txtelg" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="24" Height="17px"></asp:TextBox>
        </td>
        <td align="right">
            Heat Setting:
        </td>
        <td align="left" width="15%">
            <asp:TextBox ID="txtHeatSetting" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="32" Height="17px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            CPM:<td align="left">
                <asp:TextBox ID="txtcpm" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                    TabIndex="16" Height="17px"></asp:TextBox>
                <cc3:FilteredTextBoxExtender ID="FilteredTexttxtcpm" runat="server" TargetControlID="txtcpm"
                    FilterType="Custom, Numbers" ValidChars="." />
            </td>
            <td align="right">
                GPD(%):
            </td>
            <td align="left">
                <asp:TextBox ID="txtgpd" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                    TabIndex="25" Height="17px"></asp:TextBox>
                <cc3:FilteredTextBoxExtender ID="FilteredTexttxtgpd" runat="server" TargetControlID="txtgpd"
                    FilterType="Custom, Numbers" ValidChars="." />
            </td>
            <td align="right">
                Purpose
            </td>
            <td align="left" width="15%">
                <asp:DropDownList Width="150px" TabIndex="33" CssClass="SmallFont TextBox UpperCase"
                    ID="ddlPurpose" runat="server" AutoPostBack="True" AppendDataBoundItems="True" >
                    <asp:ListItem id="select" Text="------Select------" Value=""></asp:ListItem>
                    <asp:ListItem id="Dyeing" Text="Dyeing"></asp:ListItem>
                    <asp:ListItem id="Twisting" Text="Twisting"></asp:ListItem>
                    <asp:ListItem id="Market" Text="Market"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPurpose"
                    Display="Dynamic" ErrorMessage="Pls enter Denier" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
            </td>
    </tr>
    <tr>
        <td align="right">
            PH:
        </td>
        <td align="left">
            <asp:TextBox ID="txtph" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="17" Height="17px"></asp:TextBox>
            <cc3:FilteredTextBoxExtender ID="FilteredTexttxtph" runat="server" TargetControlID="txtph"
                FilterType="Custom, Numbers" ValidChars="." />
        </td>
        <td align="right">
            OPU:
        </td>
        <td align="left">
            <asp:TextBox ID="txtopu" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="26" Height="17px"></asp:TextBox>
            <cc3:FilteredTextBoxExtender ID="FilteredTexttxtopu" runat="server" TargetControlID="txtopu"
                FilterType="Custom, Numbers" ValidChars="." />
        </td>
       <td align="right">
                Finish Type
            </td>
            <td align="left" width="15%">
                <asp:DropDownList Width="150px" CssClass="SmallFont TextBox UpperCase"
                    ID="ddlFinishedType" runat="server" TabIndex="34" >
                     <asp:ListItem  Value="">--Select--</asp:ListItem>
                    <asp:ListItem  Value="Soft">Soft</asp:ListItem>
                    <asp:ListItem  Value="Hard">Hard</asp:ListItem>
                </asp:DropDownList>
    </tr>
    <tr>
        <td align="right">
            SH:
        </td>
        <td align="left">
            <asp:TextBox ID="txtsh" runat="server" Width="150px" MaxLength="10" CssClass="SmallFont TextBox UpperCase"
                TabIndex="18" Height="17px"></asp:TextBox>
            <cc3:FilteredTextBoxExtender ID="FilteredTexttxtsh" runat="server" TargetControlID="txtsh"
                FilterType="Custom, Numbers" ValidChars="." />
        </td>
        <td align="right">
            Oil Den:
        </td>
        <td align="left">
            <asp:TextBox ID="txtOilden" runat="server" Width="150px" MaxLength="20" CssClass="SmallFont TextBox UpperCase"
                TabIndex="27" Height="17px"></asp:TextBox>
        </td>
        <td align="right">
             Party: 
        </td>
        <td align="left" width="15%">
            <asp:Label ID="lblPartyName" runat="server"></asp:Label>
        </td>
    </tr>
</table>
                </td>
            </tr>
            </table>
     
</ContentTemplate>
</asp:UpdatePanel>
      