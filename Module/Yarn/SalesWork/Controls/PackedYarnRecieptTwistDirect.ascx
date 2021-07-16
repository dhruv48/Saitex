<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PackedYarnRecieptTwistDirect.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_PackedYarnRecieptTwistDirect" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">

    function Calculation(val) {
        document.getElementById('ctl00_cphBody_ReceiptCredit1_txtAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtFinalRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtQTY').value))).toFixed(4);
    }
   
     
</script>

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
        width: 120px;
    }
    .c3
    {
        margin-left: 4px;
        width: 300px;
    }
    .c4
    {
        margin-left: 2px;
        width: 220px;
    }
    .c5
    {
        width: 120px;
    }
    .style2
    {
        height: 25px;
    }
    .style3
    {
        width: 17%;
    }
    .style4
    {
        height: 25px;
        width: 17%;
    }
    .style5
    {
        height: 23px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<table class="tdMain" width="100%">
    <tr>
        <td width="100%" class="td">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="gg" Style="height: 41px">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png" Enabled="false"></asp:ImageButton>
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
            <b class="titleheading">Yarn Packing Against Production</b>
        </td>
    </tr>
    <tr>
        <td valign="top" align="left" class="td" width="100%">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label15" runat="server" Text="Packing Slip Number : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtTRNNUMBer" runat="server" ValidationGroup="M1" Width="150px" TabIndex="1"
                            CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="150px" Height="200px"
                            MenuWidth="300px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Packing ID #</div>
                                <div class="header c2">
                                    Packing Date Date</div>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container6" Text='<%# Eval("TRN_DATE","{0:dd/MM/yyyy}") %>' /></div>                               
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                     <td align="right" valign="top" class="style3">
                        <asp:Label ID="Label17" runat="server" Text="Shift : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:DropDownList ID="ddlReceiptShift" CssClass="SmallFont" runat="server" TabIndex="1" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label16" runat="server" Text="Packing Date : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtMRNDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="150px"
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="false"></asp:TextBox>
                             <cc1:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtMRNDate"></cc1:CalendarExtender>
                    </td>
                   
                </tr>
                <tr id=trGate runat="server" visible="false">
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label10" runat="server" Text="Gate Entry No. :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <cc2:ComboBox ID="ddlGateEntryNo" runat="server" AutoPostBack="True" DataTextField="GATE_NUMB"
                            DataValueField="GATE_NUMB" EnableLoadOnDemand="true" Height="200px" MenuWidth="800"
                            OnLoadingItems="ddlGateEntryNo_LoadingItems" OnSelectedIndexChanged="ddlGateEntryNo_SelectedIndexChanged"
                            Width="75px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Gate No</div>
                                <div class="header c2">
                                    Gate Date</div>
                                <div class="header c1">
                                    Gate Type</div>
                                <div class="header c2">
                                    Party Code</div>
                                <div class="header c3">
                                    Party Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("GATE_NUMB") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container8" runat="server" Text='<%# Eval("GATE_DATE") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Container9" runat="server" Text='<%# Eval("GATE_TYPE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal9" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <%--        <asp:TextBox ID="txtGateEntryNo" runat="server" TabIndex="17" Width="80px" CssClass="TextBoxNo SmallFont"></asp:TextBox> --%>
                        <asp:TextBox ID="txtGateEntryNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="70px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label11" runat="server" Text="Gate Entry Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtGateEntryDate" runat="server"  Width="150px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            
                    </td>
                    <td align="right" valign="top" class="style3">
                        <asp:Label ID="Label9" runat="server" Text="Vehicle/Lorry No. :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtVehicleNo" runat="server"  Width="150px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr id=trPartyChallan runat="server" visible="false">
                    <td align="right" valign="top" width="17%" class="style2">
                        <asp:Label ID="Label4" runat="server" Text="Party Challan Number :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%" class="style2">
                        <asp:TextBox ID="txtPartyChallanNo" runat="server"  Width="150px" ReadOnly="true"
                            CssClass="TextBoxNo TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%" class="style2">
                        <asp:Label ID="Label5" runat="server" Text="Party Challan Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%" class="style2">
                        <asp:TextBox ID="txtPartyChallanDate" runat="server"  Width="150px" ReadOnly="true"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" class="style3">
                        <asp:Label ID="Label18" runat="server" Text="Transporter Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="lblTransporterCode"  runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr id=trParty runat="server" visible="false">
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label19" runat="server" Text="Party Code :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="lblPartyCode"  runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="150px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Party Details :
                    </td>
                    <td align="left" valign="top" colspan="1" style="width: 32%">
                        <asp:TextBox ID="txtPartyAddress"  runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                        
                    <td align="right" valign="top" class="style3">
                        Transporter Details :
                    </td>
                    <td align="left" valign="top" colspan="1" style="width: 32%">
                        <asp:TextBox ID="txtTransporterAddress"  runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                  
                      <td align="right" valign="top" width="17%" class="style2">
                         <asp:Label ID="Label22" runat="server" Text="Packing:" CssClass="Label SmallFont" ></asp:Label>
                     <asp:Label ID="Label6" runat="server" Text="Department :" CssClass="Label SmallFont" Visible="false"></asp:Label>
                 
                    </td>
                    <td align="left" valign="top" width="15%" class="style2">
                                            <asp:DropDownList ID="ddlPackingType" runat="server" CssClass="SmallFont" 
                                                Font-Size="8"    Width="150px" 
                                                 >   
                                             <asp:ListItem Selected="True" Text="In Carton" Value="RYS33"></asp:ListItem>
                                             <asp:ListItem  Text="In Carrate" Value="RYS34"></asp:ListItem>
                                             </asp:DropDownList>
                    
                        <asp:TextBox ID="txtDepartment" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Visible="false"></asp:TextBox>
                    </td>
<td align="right" valign="top" width="17%" class="style2">
                        <asp:Label ID="Label20" runat="server" Text="Location :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%" class="style2">
                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Font-Size="8"    Width="150px" TabIndex="2">    </asp:DropDownList>
                    </td>


<td align="right" valign="top" class="style4">
                        <asp:Label ID="Label21" runat="server" Text="Store :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%" class="style2">
                         <asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Font-Size="8"    Width="150px" TabIndex="3">   </asp:DropDownList>
                    </td>
                </tr>
                <tr id=trPartyBill runat="server" visible="false">
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label1" runat="server" Text="Party Bill Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPartyBillNo" runat="server" TabIndex="4"  Width="150px" CssClass="TextBox SmallFont"
                            OnTextChanged="txtPartyBillNo_TextChanged" AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label2" runat="server" Text="Party Bill Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPartyBillDate" runat="server" TabIndex="5" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" class="style3">
                        <asp:Label ID="Label3" runat="server" Text="Party Bill Amount :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtTotalPartyAmt" runat="server" TabIndex="6" Width="150px" CssClass="TextBoxNo SmallFont"
                            AutoPostBack="True" OnTextChanged="txtPartyBillAmount_TextChanged" MaxLength="6"></asp:TextBox>
                            
                            <cc1:FilteredTextBoxExtender ID="FiltertxtTotalPartyAmt" runat="server"  TargetControlID="txtTotalPartyAmt"   FilterType="Custom, Numbers" ValidChars="." />
                        <asp:TextBox ID="txtPartyBillAmount" runat="server" TabIndex="6" Width="150px" CssClass="TextBoxNo SmallFont"
                            AutoPostBack="True" OnTextChanged="txtPartyBillAmount_TextChanged" MaxLength="6"></asp:TextBox>
                            
                            
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label12" runat="server" Text="Carton:" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <%--<asp:TextBox ID="txtFormType" runat="server" TabIndex="4" Width="150px" CssClass="TextBox SmallFont"
                            MaxLength="15" ></asp:TextBox>--%>
                               <cc2:ComboBox ID="txtFormType" runat="server" AutoPostBack="True" CssClass="smallfont"
                                            EnableLoadOnDemand="True" DataTextField="ITEM_DESC" 
                            DataValueField="ITEM_CODE" MenuWidth="500"
                                            OnLoadingItems="txtFormType_LoadingItems"  EmptyText="Select Carton"
                                            EnableVirtualScrolling="true" OpenOnFocus="true" 
                            TabIndex="4" Width="150px"      Height="200px" 
                            onselectedindexchanged="txtFormType_SelectedIndexChanged">
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
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>

                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label13" runat="server" Text="Paper Tube:" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                       <%-- <asp:TextBox ID="txtFormRefNo" runat="server" TabIndex="5" Width="150px" CssClass="TextBox SmallFont"
                            MaxLength="15"></asp:TextBox>--%>
                              <cc2:ComboBox ID="txtFormRefNo" runat="server" AutoPostBack="True" CssClass="smallfont"
                                            EnableLoadOnDemand="True" DataTextField="ITEM_DESC" 
                            DataValueField="ITEM_CODE" MenuWidth="500"
                                            OnLoadingItems="txtFormRefNo_LoadingItems"  EmptyText="Select Paper Tube"
                                            EnableVirtualScrolling="true" OpenOnFocus="true" 
                            TabIndex="4" Width="150px"      Height="200px" 
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
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>


                                        
                                        
                                        <asp:HiddenField ID="hdnCartonWt" runat="server" />
                                        <asp:HiddenField ID="hdnPaperTubeWt" runat="server"  />
                    </td>
                    <td align="right" valign="top" class="style3">
                       <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label> <%--Total Amount:--%>
                    </td>
                    <td align="left" valign="top" width="15%">
                      <asp:TextBox ID="txtRemarks" runat="server"  Width="150px" TabIndex="6" CssClass="TextBox SmallFont"
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trLr" runat="server" visible="false">
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label7" runat="server" Text="L.R. Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtLRNo" runat="server" TabIndex="10" Width="150px" CssClass="TextBoxNo SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label8" runat="server" Text="L.R. Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtLRDate" runat="server" TabIndex="11" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEdittxtLRDate" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtLRDate">
                        </cc1:MaskedEditExtender>

                    </td>
                    <td align="right" valign="top" class="style3">
                        Other Charges:
                    </td>
                    <td align="left" valign="top" width="15%">
                      <asp:TextBox ID="txtTotalAmount" runat="server" TabIndex="9" Width="150px" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                            ReadOnly="true"></asp:TextBox>
                        <asp:Button ID="btnOtherCharges" class="SmallFont" runat="server" Text="Other Charges" 
                            OnClick="btnOtherCharges_Click" Width="100px"/>
                    </td>
                </tr>
               
               </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr bgcolor="#336699" class="SmallFont titleheading">
                    <td  >
                      Details
                    </td>
                    <td visible="false"  >
                       Lot&nbsp;No
                    </td>
                    <td  >
                      Tx.LotNo
                    </td>
                    <td  >
                      Grade
                    </td>
                     <td  >
                       Gross&nbsp;Wt
                    </td>
                    <td  >
                       Tare&nbsp;Wt
                    </td>
                    <td  >
                        Net&nbsp;Wt.
                    </td>
                    <td  >
                       No&nbsp;of&nbsp;Cops&nbsp;
                    </td>
                    <td  >
                        Avg&nbsp;Wt
                    </td>
                   
                    
                    <td  >
                      Cartons
                    </td>
                    
                    
                    <td  >
                        Finish Type 
                    </td>
                   
                    <td  >
                      Remarks
                    </td>
                    <td >
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<cc2:ComboBox ID="cmbPOITEM" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="1150px"  Width="80px"
                            OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                            Height="200px" OnPreRender="cmbPOITEM_PreRender" TabIndex="7">
                            <HeaderTemplate>
                       
                                <div class="header c5">
                                    PI No.</div>
                                    <div class="header c2">
                                    Lot No.</div>
                                <div class="header c2">
                                    Yarn Code</div>
                                <div class="header c3">
                                    Description</div>
                                
                                     <div class="header c2">
                                    Shade </div>
                                <div class="header c1">
                                    Quantity</div>
                                <div class="header c1">
                                    Qty Packed</div>
                                <div class="header c1">
                                    Qty. Rem.</div>
                                <div class="header c5">
                                    Machine</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                        
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PI_NO") %>' /></div>
                                    <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal10" Text='<%# Eval("TO_BATCH_NO") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PATTERN_NO") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ORDER_DESC") %>' /></div>
                                
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Literal6" Text='<%# Eval("SHADE_CODE") %>' /></div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("PRD_QTY") %>' /></div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("QTY_PACKED") %>' /></div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("QTY_REM") %>' /></div>     
                                     <div class="item c5">
                                    <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("MACHINE_CODE") %>' /></div>                                       
                                    
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>--%>
                        
                           <cc2:ComboBox ID="cmbPOITEM" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="750px"  Width="80px" EnableVirtualScrolling="true"
                            OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                            Height="200px" OnPreRender="cmbPOITEM_PreRender" TabIndex="7">
                            <HeaderTemplate>                          
                              
                                    <div class="header c2">
                                   Tw.Lot No</div>
                                     <div class="header c2">
                                    Tx.Lot No</div>
                                <div class="header c2">
                                     Code</div>
                                <div class="header c4">
                                    Description</div>                              
                                  
                                <div class="header c1">
                                    Qty. Rem.</div>
                             
                            </HeaderTemplate>
                            <ItemTemplate>                           
                             
                                    <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal10" Text='<%# Eval("LOT_NUMBER") %>' /></div>
                                       <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("MERGE_NO") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ARTICLE_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ARTICLE_DESC") %>' /></div>  
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("QTY_REM") %>' /></div>     
                                                                      
                                    
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>

                        
                    </td>
                    <td >
                          <asp:TextBox ID="txtLotNo" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="80px"></asp:TextBox>
                       <%--<asp:DropDownList ID="ddlLotNo"  Width="80px" CssClass="SmallFont"
                             runat="server"  >
                              </asp:DropDownList>--%>
                           <%--   <cc2:ComboBox ID="ddlLotNo" runat="server" AutoPostBack="True" CssClass="SmallFont"
                DataTextField="LOT_NO" DataValueField="LOT_NO" EmptyText="Select Lot No" EnableLoadOnDemand="true"
                Height="200px" MenuWidth="350px"  EnableVirtualScrolling="true"
                OnLoadingItems="ddlLotNo_LoadingItems"   Width="80px">
                <HeaderTemplate>
                    <div class="header c2">
                        Lot No</div>
                  <div class="header c4">
                        Details</div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c2">
                        <asp:Literal ID="ltr2" runat="server" Text='<%# Eval("LOT_NO")%>'></asp:Literal>
                    </div>
                   <div class="item c4">
                        <asp:Literal ID="Literal10" runat="server" Text='<%# Eval("ASS_YARN_DESC")%>'></asp:Literal>
                    </div>
                      <div class="item c4">
                        <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("YARN_DESC")%>'></asp:Literal>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>--%>
                    </td> <td>
                    <asp:TextBox ID="txtPINO" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="90px"></asp:TextBox>
                        
                    </td>
                    <td>
                        <cc2:ComboBox ID="txtGrade" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtGrade_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Grade" 
                            EnableVirtualScrolling="true" Width="50px" MenuWidth="100px" Height="200px"  TabIndex="8">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Grade</div>
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' />
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
                        <asp:TextBox ID="txtGrossWt" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTareWt" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont" TabIndex="16"
                            Width="60px" ReadOnly="true" OnTextChanged="txtQTY_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont" TabIndex="17"
                            ReadOnly="true" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtWeightOfUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont" TabIndex="18"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                    </td>
                    
                   
                    <td>
                        <asp:TextBox ID="txtCartons" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                    </td>
                   
                    <td>
                       
                            <asp:DropDownList Width="60px" CssClass="SmallFont" TabIndex="9"
                    ID="ddlFinishedType" runat="server"  >                    
                    <asp:ListItem  Value="Soft">Soft</asp:ListItem>
                    <asp:ListItem  Value="Hard">Hard</asp:ListItem>
                </asp:DropDownList>
                    </td>
                    
                     <td>
                      <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="120px" TabIndex="11"
                            MaxLength="200" ></asp:TextBox>
                    
                        <asp:Button ID="btnDis" Width="60px" CssClass="SmallFont" runat="server" Text="Dis/Tax" Font-Size="8pt"
                            OnClick="btnDis_Click" Visible="false" />
                            <asp:CheckBox ID="chk_QCFlag" runat="server" CssClass="SmallFont" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" TabIndex="12" OnClick="btnsaveTRNDetail_Click"
                            Text="Add" Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="12" class="SmallFont titleheading" >
                        
                      <%--  PO&nbsp;No:--%>
                        <asp:TextBox ID="txtPONumb" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px" Visible="false"></asp:TextBox>
                           Remaining Qty
                       <asp:TextBox ID="lblMaxQTY" runat="server" ReadOnly="true" CssClass="TextBoxDisplay TextBox SmallFont" TabIndex="15"
                            Width="60px" OnTextChanged="lblMaxQTY_TextChanged"></asp:TextBox>
                                <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="30px"></asp:TextBox>
                       
                        
                        &nbsp;Yarn Description :<asp:TextBox ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="70px" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="245px"></asp:TextBox>                                          
                           <%-- &nbsp; Shade Family:--%><asp:TextBox ID="txtShadeFamily" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px" Visible="false"></asp:TextBox>
                        &nbsp; Shade:<asp:TextBox ID="txtShadeCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="60px"></asp:TextBox>
                            
                        <asp:TextBox ID="txtUOm" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px" Visible="false"></asp:TextBox>
                             <cc2:ComboBox ID="cmbMachineGroup" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="MACHINE_GROUP" DataValueField="MACHINE_GROUP"  Width="130px" MenuWidth="200px"
                                    Height="200px" CssClass="SmallFont"  EmptyText="Machine Group"
                                    OnLoadingItems="cmbMachineGroup_LoadingItems" >
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
                                </cc2:ComboBox>
                  <%--          <cc2:ComboBox ID="ddlMachineCode" runat="server" DataTextField="MACHINE_CODE" DataValueField="MACHINE_CODE"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="180px" OnLoadingItems="ddlMachineCode_LoadingItems" EmptyText="Machines"
                            Width="130px" AutoPostBack="True">
                            <HeaderTemplate>
                                <div class="header c3">
                                    Machine Code</div>                              
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c3">
                                    <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />-
                                    <asp:Literal ID="Literal9" runat="server" Text='<%# Eval("OLD_MACHINE_NAME") %>' />
                                </div>                             
                                
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>--%>
                        
                        
                        <cc2:ComboBox ID="cmbOrderNo" runat="server"
                DataTextField="ORDER_NO" DataValueField="ORDER_NO" EmptyText="Order No" EnableLoadOnDemand="true"
                Height="200px" MenuWidth="700px"  EnableVirtualScrolling="true" Width="130px"
                OnLoadingItems="cmbOrderNo_LoadingItems"   >
                <HeaderTemplate>
                    <div class="header c5">
                       Order No
                        </div>         
                        <div class="header c1">
                        Yarn
                        </div>    
                        <div class="header c4">
                       Desc
                        </div>    
                        <div class="header c1">
                       Shade
                        </div>    
                        <div class="header c1">
                       Ord Qty
                        </div>    
                        <div class="header c1">
                        Prod Qty
                        </div>             
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="item c5">
                        <asp:Literal ID="ltr2" runat="server" Text='<%# Eval("ORDER_NO")%>'></asp:Literal>
                    </div>
                    <div class="item c1">
                        <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("ASS_ARTICAL_CODE")%>'></asp:Literal>
                    </div>
                    <div class="item c4">
                        <asp:Literal ID="Literal6" runat="server" Text='<%# Eval("ASS_ARTICAL_DESC")%>'></asp:Literal>
                    </div>
                    <div class="item c1">
                        <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("SHADE_CODE")%>'></asp:Literal>
                    </div>
                    <div class="item c1">
                        <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("REQ_QTY")%>'></asp:Literal>
                    </div>
                    <div class="item c1">
                        <asp:Literal ID="Literal9" runat="server" Text='<%# Eval("PRODUCTION_QTY")%>'></asp:Literal>
                    </div>
                    
                   
                </ItemTemplate>
                <FooterTemplate>
                    Displaying items
                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                    out of
                    <%# Container.ItemsCount %>.
                </FooterTemplate>
            </cc2:ComboBox>
                            &nbsp;<asp:Button ID="btnSubDetail" TabIndex="9" runat="server" Font-Size="8pt" Text="Cartons Detail" CssClass="SmallFont"
                            Width="100px" OnClick="btnSubDetail_Click1" /> 
                           
                           
                    </td>
                    <td>
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click" TabIndex="13"
                            Text="Cancel"  Width="60px"/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                <asp:GridView ID="grdMaterialItemReceipt" Width="99%" runat="server" AutoGenerateColumns="False"
                    CssClass="SmallFont" ShowFooter="false" OnRowCommand="grdMaterialItemReceipt_RowCommand"
                    OnRowDataBound="grdMaterialItemReceipt_RowDataBound">
                    <Columns>
                     <asp:TemplateField HeaderText="PO No#" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtPoNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LOT#">
                            <ItemTemplate>
                                <asp:Label ID="txtLotNo1" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PI_NO#">
                            <ItemTemplate>
                                <asp:Label ID="txtPI_NO1" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_NO") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="GRADE">
                            <ItemTemplate>
                                <asp:Label ID="txtGrade1" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Yarn Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>' ToolTip='<%# Bind("YARN_CODE") %>'
                                     >
                                </asp:Label>
                              
                                        
                                 
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Shade Family" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtshadeFamily" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_FAMILY") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade Code">
                            <ItemTemplate>
                                <asp:Label ID="txtshadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                              
                        <asp:TemplateField HeaderText="Gross&nbsp;Wt.">
                            <ItemTemplate>
                                <asp:Label ID="txtGrossWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GROSS_WT") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tare&nbsp;Wt.">
                            <ItemTemplate>
                                <asp:Label ID="txtTareWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TARE_WT") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Net&nbsp;Wt">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="No&nbsp;of&nbsp;Cops&nbsp;">
                            <ItemTemplate>
                                <asp:Label ID="txtNoOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="Avg.Wt.">
                            <ItemTemplate>
                                <asp:Label ID="txtWeightOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Cartons">
                            <ItemTemplate>
                                <asp:Label ID="txtCartons" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CARTONS") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REMARKS") %>'
                                     ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q.C." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtQC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("QCFLAG") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Finish Type" >
                            <ItemTemplate>
                                <asp:Label ID="txtFinishType" runat="server" CssClass="Label SmallFont" Text='<%# Bind("FINISH_TYPE") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Machine">
                            <ItemTemplate>
                                <asp:Label ID="txtMachine" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MAC_CODE") %>'
                                    ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SubTran">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkunige" runat="server" CssClass="Label SmallFont" Text="View"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'>
                                </asp:LinkButton>
                                <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid"
                                    BorderWidth="5px" HorizontalAlign="Left">
                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False" ShowFooter="true">
                                        <Columns>                                          
                                                  
                                                <asp:TemplateField HeaderText="Carton&nbsp;No">                                                   
                                                    <ItemTemplate>
                                                       <asp:Label ID="lbtcartonno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CARTON_NO") %>'></asp:Label>
                                                    </ItemTemplate>   
                                                     <FooterTemplate>
                                                     <asp:Label ID="lbtFcartonno" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true" Text="Total:"></asp:Label>
                                                   
                                                    </FooterTemplate>                                                  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cops">                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <FooterTemplate>
                                                     <asp:Label ID="lblFNoUnit" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"></asp:Label>
                                                   
                                                    </FooterTemplate>                                                  
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Gross&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrossWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GROSS_WT") %>'></asp:Label>
                                                    </ItemTemplate>    
                                                     <FooterTemplate>
                                                     <asp:Label ID="lblFGrossWt" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"></asp:Label>
                                                   
                                                    </FooterTemplate>                                                   
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tare&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTareWt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TARE_WT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                     <asp:Label ID="lblFTareWt" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"></asp:Label>
                                                   
                                                    </FooterTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net&nbsp;Wt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                     <asp:Label ID="lblFQTY" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"></asp:Label>
                                                   
                                                    </FooterTemplate>                                                   
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="UOM">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bar&nbsp;Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBarcodeNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BARCODE_NO") %>'></asp:Label>
                                                    </ItemTemplate>       
                                                    <FooterTemplate>
                                                     <asp:Label ID="lblFBarcodeNo" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"></asp:Label>
                                                   
                                                    </FooterTemplate>                                              
                                                </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                </asp:Panel>
                                <cc1:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnlBOM" TargetControlID="lnkunige"
                                    PopupPosition="Left">
                                </cc1:HoverMenuExtender>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>/
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
<cc1:ValidatorCalloutExtender ID="vcMRNNo" TargetControlID="rfv1" runat="server">
</cc1:ValidatorCalloutExtender>
<cc1:ValidatorCalloutExtender ID="vcrv1" TargetControlID="rv1" runat="server">
</cc1:ValidatorCalloutExtender>
<asp:RangeValidator ID="rv1" ControlToValidate="txtTRNNUMBer" runat="server" ErrorMessage="Only numeric value allowed"
    Display="Dynamic" MaximumValue="1000000" MinimumValue="1" Type="Integer" ValidationGroup="M1"
    SetFocusOnError="True"></asp:RangeValidator>
 <asp:RequiredFieldValidator ControlToValidate="txtTRNNUMBer"
        ID="rfv1" runat="server" ValidationGroup="M1" ErrorMessage="MRN number required"
        Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>

<cc1:CalendarExtender ID="cebill" runat="server" TargetControlID="txtPartyBillDate"
    Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<%--<asp:RangeValidator ID="rvbill" runat="server" ControlToValidate="txtPartyBillDate"
    Display="Dynamic" ErrorMessage="" Type="Date" ValidationGroup="M1"
    SetFocusOnError="True"></asp:RangeValidator>
    
    <cc1:ValidatorCalloutExtender ID="vcbill"
        TargetControlID="rvbill" runat="server">
    </cc1:ValidatorCalloutExtender>--%>
<cc1:CalendarExtender ID="celr" runat="server" TargetControlID="txtLRDate" Format="dd/MM/yyyy">
</cc1:CalendarExtender>
<%--<asp:RangeValidator ID="rvlr" runat="server" ControlToValidate="txtLRDate" Display="Dynamic"
    ErrorMessage="Invalid LR Date" Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
<cc1:ValidatorCalloutExtender ID="vclr" TargetControlID="rvlr" runat="server">
</cc1:ValidatorCalloutExtender>--%>

<%--</ContentTemplate>
</asp:UpdatePanel>--%>