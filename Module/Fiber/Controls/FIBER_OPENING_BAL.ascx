<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FIBER_OPENING_BAL.ascx.cs"
    Inherits="Module_Fiber_Controls_FIBER_OPENING_BAL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 250px;
    }
    .c4
    {
        margin-left: 4px;
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 100px;
    }
    .TextBoxNo
    {
        height: 22px;
    }
    .style2
    {
        width: 61px;
    }
</style>

<%-- <asp:UpdatePanel ID="uppnl" runat="server">
    <ContentTemplate>--%>
<table width="100%" style="border: 0px; border-style: inset;">
    <tr>
        <td align="left" class="td" valign="top">
            <table class="tContentArial">
                <tr>
                    <td>
                        <asp:Label ID="lblMode" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Update" ValidationGroup="M1" OnClick="imgbtnUpdate_Click" TabIndex="38" />
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/CommonImages/list.jpg"
                            ToolTip="Poy Master List" OnClick="imgbtnDelete_Click" TabIndex="39" />
                    </td>
                    <td id="tdFind" runat="server" visible="false">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" OnClick="imgbtnFind_Click" TabIndex="40" CausesValidation="false" />
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClick="imgbtnPrint_Click" TabIndex="41" CausesValidation="false" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Style="height: 41px" OnClick="imgbtnClear_Click" TabIndex="42"
                            CausesValidation="false" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" OnClick="imgbtnExit_Click" TabIndex="43" CausesValidation="false" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" TabIndex="56" CausesValidation="false" />
                    </td>
                </tr>
            </table>
            <table width="100%" class="tContentArial"  >
                <tr>
                    <td align="center" class="TableHeader td" valign="top">
                        <span class="titleheading"><b>P.O.Y. Opening Form</b></span>
                    </td>
                </tr>
                <tr>
                    <td class="td" align="left" width="50%">
                        <table class="tContentArial" >
                           
                            <tr>
                                <td align="right" valign="top">
                                    <font color="#ff0000">*</font>Poy Code:
                                </td>
                                <td align="left" valign="top">
                                <cc2:ComboBox ID="DDLFiberCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="FIBER_CODE" DataValueField="FIBER_DESC" EmptyText="Find Poy Code"
                                        EnableLoadOnDemand="true" Height="200px" MenuWidth="400" OnLoadingItems="DDLFiberCode_LoadingItems"
                                        OnSelectedIndexChanged="DDLFiberCode_SelectedIndexChanged" TabIndex="1" Width="125px"
                                        Visible="False">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                POY CODE</div>
                                            <div class="header c2">
                                                POY Description</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("FIBER_CODE") %>' /></div>
                                            <div class="item c2">
                                                <asp:Literal ID="Container2" runat="server" Text='<%# Eval("FIBER_DESC") %>' /></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                    <asp:TextBox ID="txtFiberCode" runat="server" CssClass="SmallFont TextBox UpperCase"
                                        Width="125px" TabIndex="1" ReadOnly="true" ></asp:TextBox>
                                    
                                    
                                </td>
                                <td align="right" valign="top">
                                    Poy&nbsp;Description:
                                </td>
                                <td align="left" valign="top" colspan="2">
                                    <asp:TextBox ID="txtdescription" runat="server" TabIndex="4" CssClass="SmallFont TextBox UpperCase"
                                        MaxLength="75" Width="250px" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField ID="txtOpeningBalanceStock" runat="server" />
                                </td>
                                
                            </tr>
                          
                           </table>
                    </td>
                </tr>
               
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td SmallFont" valign="top" width="100%">
            <table width="100%">
                <tr bgcolor="#006699">
                    <td align="left" class="tdLeft SmallFont" valign="top" width="150px">
                        <span class="titleheading"><b>Party&nbsp;Code</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Party&nbsp;Name</b></span>
                    </td>
                     <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Bill&nbsp;No</b></span>
                    </td>
                     <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Bill&nbsp;Dt</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Merge</b></span>
                    </td>
                    <td align="right" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Grade</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Pallet&nbsp;Code</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>C/P&nbsp;No</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Rate</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Qty</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>UOM</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Tubes</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                        <span class="titleheading"><b>Tube&nbsp;Wt.</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top" runat="server" visible="false">
                        <span class="titleheading"><b>Dat&nbsp;of&nbsp;Manufacturing</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top" runat="server" visible="false">
                        <span class="titleheading"><b>Material&nbsp;Status</b></span>
                    </td>
                    <td align="left" class="tdLeft SmallFont" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="95%" MenuWidth="400px" Height="200px" TabIndex="2">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c3">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c3">
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
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtPartyName" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="3"></asp:TextBox>
                    </td>
                      <td align="left" valign="top">
                        <asp:TextBox ID="txtBillNo" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="4"></asp:TextBox>
                    </td>
                      <td align="left" valign="top">
                        <asp:TextBox ID="txtBillDate" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="5"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                    <cc2:ComboBox ID="txtLotNo" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtLotNo_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Merge No"  
                            EnableVirtualScrolling="true" Width="120%" MenuWidth="300px" Height="200px"  TabIndex="6">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Merge No</div>
                                <div class="header c3">
                                    Desc</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MST_DESC") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        
                    </td>
                    <td align="right" valign="top">
                      <cc2:ComboBox ID="txtGrade" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtGrade_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Grade" 
                            EnableVirtualScrolling="true" Width="80%" MenuWidth="100px" Height="200px"  TabIndex="7">
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
                    <td align="left" valign="top">
                         <cc2:ComboBox ID="txtPalletCode" runat="server" EnableLoadOnDemand="true"
                            OnLoadingItems="txtPalletCode_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Pallet Code" 
                            EnableVirtualScrolling="true" Width="100%" MenuWidth="300px" Height="200px"  TabIndex="8">
                            <HeaderTemplate>
                                <div class="header c1">
                                   Pallet Code</div>
                                <div class="header c3">
                                   Pallet Desc</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MST_DESC") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtPalletNo" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="9"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtRate" class="SmallFont uppercase" Width="95%" runat="server"
                            MaxLength="14" TabIndex="10"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtQty" Width="95%" class="SmallFont" runat="server" AutoPostBack="True"
                            OnTextChanged="txtQty_TextChanged" MaxLength="9" TabIndex="11"></asp:TextBox>
                        <asp:Label ID="lblIssueQty" runat="server" Visible="false"></asp:Label>
                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>
                        <cc11:FilteredTextBoxExtender ID="FiltertxtQty" runat="server" TargetControlID="txtQty"
                            FilterType="Custom, Numbers" ValidChars="." />
                    </td>
                    <td align="left" valign="top">
                        <asp:DropDownList ID="ddlUOM" class="SmallFont" runat="server" Width="95%" >
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtNoofUnit" runat="server" class="SmallFont" Width="95%" AutoPostBack="True"
                            OnTextChanged="txtNoofUnit_TextChanged" MaxLength="5" TabIndex="12"></asp:TextBox>
                        <%--  <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtNoofUnit"
                                            Display="Dynamic" ErrorMessage="Please Enter No of Unit in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>
                        <cc11:FilteredTextBoxExtender ID="FilteredTextBoxtxtNoofUnit" runat="server" TargetControlID="txtNoofUnit"
                            FilterType="Custom, Numbers" ValidChars="." />
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtWeightofUnit" class="SmallFont" runat="server" Width="95%" Enabled="False"
                            ></asp:TextBox>
                        <%-- <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtWeightofUnit"
                                            Display="None" ErrorMessage="Please Enter Weight of Unit in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>
                    </td>
                    <td align="left" valign="top" runat="server" visible="false">
                        <asp:TextBox ID="txtDofMfd" class="SmallFont" Width="95%" runat="server" ></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDofMfd"
                                            Display="Dynamic" ErrorMessage="Please Enter Date Of Manufacutring" SetFocusOnError="True"
                                            ValidationGroup="YM"></asp:RequiredFieldValidator>--%>
                        <cc11:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                            TargetControlID="txtDofMfd">
                        </cc11:CalendarExtender>
                        <cc11:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtDofMfd">
                        </cc11:MaskedEditExtender>
                        <br />
                    </td>
                    <td align="left" valign="top" runat="server" visible="false">
                        <asp:DropDownList ID="ddlMaterialStatus" class="SmallFont" runat="server" Width="95%"
                            TabIndex="37">
                            <%--<asp:ListItem Value="0">------Select---------</asp:ListItem>--%>
                            <%--<asp:ListItem>UnCheck</asp:ListItem>
                                            <asp:ListItem>Extracted</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>--%>
                            <asp:ListItem>Approved</asp:ListItem>
                            <asp:ListItem>Rejected</asp:ListItem>
                            <asp:ListItem>Hold</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td align="left" valign="top">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="BtnBOMSave" class="SmallFont" runat="server" OnClick="BtnBOMSave_Click"
                                        Text="Add" ValidationGroup="YM" Width="70px" CausesValidation="false" TabIndex="13" />
                                </td>
                                <td>
                                    <asp:Button ID="BtnBOMCancel" runat="server" class="SmallFont" OnClick="BtnBOMCancel_Click"
                                        CausesValidation="false" Text="Cancel" Width="70px" TabIndex="14" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td SmallFont" valign="top" width="100%">
            <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand" ShowFooter="true"
                Width="100%" OnRowDataBound="grdsub_trn_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                        <ItemTemplate>
                            <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party Name">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblParty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PRTY_NAME") %>' ToolTip='<%# Bind("PRTY_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                   <asp:TemplateField HeaderText="Bill No">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lbtbillno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BILL_NUMB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bill Dt">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lbtBillDt" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BILL_DATE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Merge No">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lbtlotno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pallet Code">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblPalletCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PALLET_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            Total:
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="C/P No">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblCPNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PALLET_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Literal ID="totalPalletNo" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Rate">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("FINAL_RATE") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                           
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QTY">
                        <ItemTemplate>
                            <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <FooterTemplate>
                            <asp:Literal ID="totalQty" runat="server" />
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No of Tubes">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Literal ID="totalNoOfUnit" runat="server" />
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Weight of Tube">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblWeightofUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date of Mfd" Visible="false">
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DATE_OF_MFG","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Status" Visible="false">
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:Label ID="txtBOMArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issue Qty" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="txtIssueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ISS_QTY") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="lnkBOMEdit" class="SmallFont" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                Width="70px" CommandName="BOMEdit" TabIndex="12" Text="Edit" />
                            <asp:Button ID="lnkBOMDelete" class="SmallFont" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>'
                                Width="70px" CommandName="BOMDelete" OnClientClick="return confirm('Are you Sure want to delete this BOM Detail?');"
                                TabIndex="12" Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="RowStyle " />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <HeaderStyle BackColor="#336699" CssClass="SmallFont" ForeColor="White" />
            </asp:GridView>
        </td>
    </tr>
    
</table>
<cc11:CalendarExtender ID="CE1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBillDate">
                        </cc11:CalendarExtender>
<%--</ContentTemplate>
    </asp:UpdatePanel>--%>