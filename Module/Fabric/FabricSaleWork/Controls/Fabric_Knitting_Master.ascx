<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fabric_Knitting_Master.ascx.cs" Inherits="Module_Fabric_FabricSaleWork_Controls_Fabric_Knitting_Master" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>
 <style type="text/css">
        .tdLeft
        {
            width: -110%;
        }
    </style>
    
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;
        }
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
</style>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td valign="top">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="left" class="td" valign="top">
                                    <table>
                                        <tr>
                                            <td id="tdSave" runat="server">
                                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                                    OnClick="imgbtnSave_Click" OnClientClick="if (!confirm('Are you sure to Save the record ?')) { return false; }"
                                                    ToolTip="Save" ValidationGroup="YM" TabIndex="51" />
                                            </td>
                                            <td id="tdUpdate" runat="server">
                                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                                    OnClick="imgbtnUpdate_Click" OnClientClick="if (!confirm('Are you sure to Update the record ?')) { return false; }"
                                                    ToolTip="Update" ValidationGroup="M1" />
                                            </td>
                                            <td id="tdDelete" runat="server">
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" Enabled="false" ImageUrl="~/CommonImages/del6.png"
                                                    OnClick="imgbtnDelete_Click" OnClientClick="if (!confirm('Are you sure to Delete the record ?')) { return false; }"
                                                    ToolTip="Delete" CausesValidation="false" />
                                            </td>
                                            <td id="tdFind" runat="server">
                                                <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                                    OnClick="imgbtnFind_Click1" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }"
                                                    ToolTip="Find" TabIndex="52" CausesValidation="false" />
                                            </td>
                                            <td id="tdPrint" runat="server">
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                    OnClick="imgbtnPrint_Click" OnClientClick="if (!confirm('Are you sure to Print the record ?')) { return false; }"
                                                    ToolTip="Print" TabIndex="53" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                    OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }"
                                                    ToolTip="Clear" TabIndex="54" CausesValidation="false"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                    OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }"
                                                    ToolTip="Exit" TabIndex="55" CausesValidation="false" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                    ToolTip="Help" TabIndex="56"  CausesValidation="false" 
                                                    onclick="imgbtnHelp_Click"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="TableHeader td">
                                    <span class="titleheading"><b>Fabric Knitting Master</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="td" valign="top">
                                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                                        &nbsp;Mode </span>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td class="tdRight" width="15%">
                                                Fabric Code*
                                            </td>
                                            <td width="15%" valign="top">
                                                <asp:TextBox ID="txtFabricCode" runat="server" CssClass="SmallFont TextBoxNo TextBoxDisplay"
                                                    ReadOnly="True" Width="125" TabIndex="1"></asp:TextBox>
                                             
                                        <cc2:ComboBox ID="ddlFabricCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                                    DataTextField="Combined" DataValueField="FABR_CODE" EmptyText="Find Fabric Code" EnableLoadOnDemand="true"
                                                    Height="200px" MenuWidth="700" OnLoadingItems="ddlFabricCode_LoadingItems" OnSelectedIndexChanged="ddlfabriccode_SelectedIndexChanged1"
                                                    TabIndex="2"  Width="150px" Visible="False" >
                                                    <HeaderTemplate>
                                                        <div class="header c1">
                                                            FABRIC CODE</div>
                                                        <div class="header c2">
                                                            FABRIC DESC</div>
                                                        <div class="header c2">
                                                            FABRIC TYPE</div>
                                                            <div class="header c2">
                                                            FABRIC CATEGORY</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <asp:Literal ID="Container1" runat="server" Text='<%# Eval("FABR_CODE") %>' /></div>
                                                        <div class="item c2">
                                                            <asp:Literal ID="Container2" runat="server" Text='<%# Eval("FABR_DESC") %>' /></div>
                                                            <div class="item c2">
                                                            <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("FABR_TYPE") %>' /></div>
                                                            <div class="item c2">
                                                            <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("FABR_CATEGORY") %>' /></div>
                                                        
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
                                              Fabric&nbsp;Type
                                            </td>
                                            <td width="15%" >
                                                <asp:DropDownList ID="ddlFabricType" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="2" Width="127">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdRight" width="15%">
                                                Fabric&nbsp;Category
                                            </td>
                                            <td width="15%">
                                                <asp:DropDownList ID="ddlFabricCategory" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="3" Width="127" OnSelectedIndexChanged="ddlfabricCategory_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                       </tr>
                                           
                                        <tr>
                                           
                                            
                                            <td class="tdRight" width="15%" valign="top">
                                               Fabric&nbsp;Sub&nbsp;Category
                                            </td>
                                            <td width="15%" valign="top">
                                                <asp:DropDownList ID="ddlFabricSubCategory" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                    CssClass="SmallFont" 
                                                    TabIndex="4" Width="125">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="tdRight" width="15%">
                                                 Fabric&nbsp;Description*
                                            </td>
                                            <td class="tdLeft" width="80%" valign="top" colspan="3" >
                                               <asp:TextBox ID="txtFabricRemarks" runat="server" Text="" MaxLength="100" TabIndex="5" Width="97%" ></asp:TextBox>
                                                
                                            </td>
                                           
                                        </tr>
                                        <tr>
                                            <td class="tdRight" width="15%">
                                            Gauge                                               
                                            </td>
                                            <td  width="15%">
                                              <asp:TextBox ID="txtGauge" runat="server" TabIndex="7" MaxLength="6" Width="125"> </asp:TextBox>
                                                  <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"  TargetControlID="txtGauge"   FilterType="Custom, Numbers" ValidChars="."/> 
                                            </td>
                                            <td class="tdRight" width="15%"> 
                                            Diameter
                                             </td>
                                            <td  width="15%">
                                             <asp:TextBox ID="txtDiameter" runat="server" TabIndex="8" MaxLength="6" Width="125"></asp:TextBox>
                                             <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"  TargetControlID="txtDiameter"   FilterType="Custom, Numbers" ValidChars="."/> 
                                            
                                            </td>
                                            <td class="tdRight" width="15%">
                                            Stitch&nbsp;Length
                                            </td>
                                            <td  width="15%">
                                             <asp:TextBox ID="txtStitchLenght" runat="server" TabIndex="9" MaxLength="6" Width="125"></asp:TextBox>
                                              <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"  TargetControlID="txtStitchLenght"   FilterType="Custom, Numbers" ValidChars="."/> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdRight" width="15%">
                                            GSM                                               
                                            </td>
                                            <td  width="15%">
                                             <asp:TextBox ID="txtGSM" runat="server" TabIndex="10" MaxLength="6" Width="125"></asp:TextBox>                                                                  
                                             <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"  TargetControlID="txtGauge"   FilterType="Custom, Numbers" ValidChars="."/> 
                                            </td>
                                            <td class="tdRight" width="15%"> 
                                            UOM
                                            </td>
                                            <td  width="15%">
                                            <asp:DropDownList ID="ddlUOM" runat="server" TabIndex="11"  Width="127"></asp:DropDownList>
                                            </td>
                                            <td class="tdRight" width="15%">
                                            Filament
                                            </td>
                                            <td  width="15%">
                                             <asp:DropDownList ID="ddlFilament" runat="server" TabIndex="12" Width="127" ></asp:DropDownList>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td class="tdRight" width="15%" valign="top">
                                                Opening&nbsp;Stock*
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtOpeningBalanceStock"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                                      <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"  TargetControlID="txtOpeningBalanceStock"   FilterType="Custom, Numbers" ValidChars="."/> 
                                            </td>
                                            <td class="tdLeft" width="15%">
                                                <asp:TextBox ID="txtOpeningBalanceStock" runat="server" 
                                                    MaxLength="6" TabIndex="13" Width="125"></asp:TextBox>
                                                
                                                
                                                <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtOpeningBalanceStock"
                                                    Display="None" ErrorMessage="Please Enter OpeningBalanceStock in Numeric &amp; Precision Should be 6 and Scale 3  "
                                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                                   
                                            </td>
                                            <td class="tdRight" width="15%" valign="top">
                                                Minimum&nbsp;Stock*
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtMimimumStock"
                                                   Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="tdleft" width="15%">
                                                <asp:TextBox ID="txtMimimumStock" runat="server"  MaxLength="6"
                                                    TabIndex="14" Width="125"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtMimimumStock"
                                                    Display="None" ErrorMessage="Please Enter Minimum Stock in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                                 <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"  TargetControlID="txtMimimumStock"   FilterType="Custom, Numbers" ValidChars="."/> 
                                                    
                                            </td>
                                            <td class="tdRight" width="15%" valign="top">
                                                Minimum&nbsp;Procure&nbsp;Days*
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtMinimumProcureDays"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                                    <cc11:FilteredTextBoxExtender ID="FiltertxtMinimumProcureDays" runat="server"  TargetControlID="txtMinimumProcureDays"   FilterType="Custom, Numbers" />
                                            </td>
                                            <td class="tdleft" width="25%">
                                                <asp:TextBox ID="txtMinimumProcureDays" runat="server" 
                                                    MaxLength="3" TabIndex="15" Width="125"></asp:TextBox>
                                                <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"  TargetControlID="txtMinimumProcureDays"   FilterType="Custom, Numbers" ValidChars="."/>  
                                                                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdRight" width="15" valign="top">
                                                Opening&nbsp;Rate*
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="txtOpeningRate"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" 
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:TextBox ID="txtOpeningRate" runat="server"  MaxLength="6"
                                                    TabIndex="16" Width="125"></asp:TextBox>
                                              
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtOpeningRate"
                                                    Display="None" ErrorMessage="Please Enter OpeningRate in Numeric &amp; Precision Should be 11 and Scale 4  "
                                                    MaximumValue="9999999999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                                  <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"  TargetControlID="txtOpeningRate"   FilterType="Custom, Numbers" ValidChars="."/>     

                                            </td>
                                            <td class="tdRight" width="15">
                                                Reorder&nbsp;Level
                                            </td>
                                            <td class="tdLeft" width="15">
                                                <asp:TextBox ID="txtRecorderLevel" runat="server" 
                                                    MaxLength="6" TabIndex="17" Width="125"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator14" runat="server" ControlToValidate="txtRecorderLevel"
                                                    Display="None" ErrorMessage="Please Enter Recorder Level in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                             <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"  TargetControlID="txtRecorderLevel"   FilterType="Custom, Numbers" ValidChars="."/> 
                                            </td>
                                            <td class="tdRight" width="15">
                                                Reorder&nbsp;Quantity
                                            </td>
                                            <td class="tdLeft" width="25">
                                                <asp:TextBox ID="txtRecorderQuantity" runat="server" 
                                                    MaxLength="6" TabIndex="18" Width="125"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator15" runat="server" ControlToValidate="txtRecorderQuantity"
                                                    Display="None" ErrorMessage="Please Enter Recorder Quantity in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                               <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"  TargetControlID="txtRecorderQuantity"   FilterType="Custom, Numbers" ValidChars="."/> 
                                                                                              </td>
                                        </tr>
                                        <tr>
                                            
                                            <td class="tdRight" width="15">
                                                Maximum Stock
                                            </td>
                                            <td class="tdLeft" width="15" colspan="2">
                                                <asp:TextBox ID="txtMaximumStock" runat="server"  MaxLength="8"
                                                    TabIndex="20" Width="125"></asp:TextBox>
                                                      <cc11:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server"  TargetControlID="txtMaximumStock"   FilterType="Custom, Numbers" ValidChars="."/> 
                                            </td>
                                            <td class="tdRight" width="15">
                                              
                                            </td>
                                            <td class="tdLeft" width="15">
                                               <%-- <asp:TextBox ID="txtFindDepCode" runat="server"  MaxLength="10"
                                                    TabIndex="19" Width="125"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtFindDepCode"
                                                    Display="None" ErrorMessage="Please Enter Fin Deb Code" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>--%>
                                                                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                         
                  

                          
                          </table>    
            
              
                                  
                                    <table width="98%">
                                    <tr>
                                    <td colspan="10">
                                      <b>Fabric Compostition....</b>
                                    </td>
                                    </tr>
                                        <tr bgcolor="#006699">
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Substrate&nbsp;Article(Type&nbsp;Of&nbsp;Blending) </b></span>
                                            </td>
                                           <%-- <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Count </b></span>
                                            </td>--%>
                                             <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Percentage</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Remarks</b></span>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" class="tdLeft">
                                               <%-- <asp:DropDownList ID="ddlBland2" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="21" Width="125" 
                                                    onselectedindexchanged="ddlBland2_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>--%>
                                                 <cc2:ComboBox runat="server" ID="ddlBland2" CssClass="SmallFont" MenuWidth="250px"
                                                    Width="125px" Height="180px" EmptyText="Select Yarn" EnableLoadOnDemand="true"
                                                    OnLoadingItems="ddlBland2_LoadingItems" AutoPostBack="True"
                                                    TabIndex="21" />
                                            </td>
                                            <%--<td align="left" valign="top" runat="server" visible ="false">
                                            <asp:TextBox ID="txtCount" runat="server" visible ="false" 
                                                    MaxLength="6" TabIndex="22" Width="100"></asp:TextBox>
                                                    &nbsp;&nbsp;
                                                  </td>--%>
                                            <td align="left" valign="top">
                                            <asp:TextBox ID="txtbland1percentage0" runat="server" 
                                                    MaxLength="3" TabIndex="23" Width="100"></asp:TextBox>
                                               <%-- <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtbland1percentage0"
                                                    Display="None" ErrorMessage="Bland1percentage Value Should not exceeds 100% Or Please Enter Numeric"
                                                    MaximumValue="100.000" MinimumValue="0" Type="Double" ValidationGroup="BB"></asp:RangeValidator>--%>
                                                    
                                                    <cc11:FilteredTextBoxExtender ID="Filtertxtbland1percentage0" runat="server"  TargetControlID="txtbland1percentage0"   FilterType="Custom, Numbers" ValidChars="."/>
                                          
                                              </td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txtBlandRemarks" runat="server" TabIndex="24"
                                                    Width="125" MaxLength="25"></asp:TextBox>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:Button ID="btmSave" runat="server" OnClick="btmSave_Click" Text="Save" ValidationGroup="BB"
                                                    TabIndex="25"  />
                                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                    TabIndex="26"  />
                                            </td>
                                        </tr>
                                          
                                        
                                        <tr>
                                            <td align="left" colspan="5" valign="top">
                                                <asp:GridView ID="grdBlandTran" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdBlandTran_RowCommand"
                                                    ShowFooter="True" Width="98%">
                                                    <Columns>
                                                    
                                                        <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" Width="25px" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Substrate" Visible="false" >
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtblendArtilce" runat="server" Text='<%# Bind("BlendArticle") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Substrate Desc">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtblendArtilceDesc" runat="server" Text='<%# Bind("BlendArticleDesc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                     
                                                        <asp:TemplateField HeaderText="Bland Percentage">
                                                            <FooterTemplate>
                                                                <asp:Label ID="lblBlandTotal" runat="server" Text=""></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtItemDesc" runat="server" CssClass="Label SmallFont" Text='<%# Bind("Percentage") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="40%" HeaderStyle-Width="40%" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemakrs" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("Remarks") %>'
                                                                    Width="50px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="lnkEdit" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                                    CommandName="indentEdit" TabIndex="12" Text="Edit" />
                                                                <asp:Button ID="lnkDelete" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                                    CommandName="indentDelete" OnClientClick="return confirm('Are you Sure want to delete this Bland?');"
                                                                    TabIndex="12" Text="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                    </Columns>
                                                     <RowStyle CssClass="RowStyle SmallFont" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                    <PagerStyle CssClass="PagerStyle" />
                                                    <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                                </asp:GridView>
                                                   
                            </tr>
                          
                          
                        </table>
                        
                                   <b>Base Article Detail....</b>
                                    <table width="98%">
                                        <tr bgcolor="#006699">
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Product Type</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Article Code</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Count </b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>UOM</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Basis</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Value Qty</b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                <span class="titleheading"><b>Wastage </b></span>
                                            </td>
                                            <td align="left" class="tdLeft SmallFont" valign="top">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlProductType" runat="server" AppendDataBoundItems="True"
                                                    AutoPostBack="True" CssClass="SmallFont" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged"
                                                    TabIndex="42" Width="120">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ControlToValidate="ddlProductType"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"  
                                                    ValidationGroup="BA"></asp:RequiredFieldValidator>
                                            </td>
                                            <td valign="top" class="tdLeft">
                                               <%-- <asp:DropDownList ID="ddlBland2" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="21" Width="125" 
                                                    onselectedindexchanged="ddlBland2_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>--%>
                                                 <cc2:ComboBox runat="server" ID="ddlBland21" CssClass="SmallFont" MenuWidth="250px"
                                                    Width="125px" Height="180px" EmptyText="Select Yarn" EnableLoadOnDemand="true"
                                                    OnLoadingItems="ddlBland21_LoadingItems" AutoPostBack="True" OnSelectedIndexChanged="ddlBland21_SelectedIndexChanged"
                                                    TabIndex="21" />
                                                    <asp:Label ID="lblYarnDesc" runat="server" Visible="false"></asp:Label>
                                            </td>
                                            <td id="Td1" align="left" valign="top">
                                            <asp:TextBox ID="txtCount" runat="server" 
                                                    MaxLength="6" TabIndex="22" Width="100"></asp:TextBox>
                                                    &nbsp;&nbsp;
                                                  </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlBaseUOM" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                   MaxLength="6" TabIndex="22">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ControlToValidate="ddlBaseUOM"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="BA"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlBaseBasis" runat="server"  CssClass="SmallFont" MaxLength="6"
                                                    TabIndex="22">
                                                </asp:DropDownList>
                                             <%--   AppendDataBoundItems="True"--%>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="ddlBaseBasis"
                                                    Display="Dynamic" ErrorMessage="*" InitialValue="0" SetFocusOnError="True"
                                                    ValidationGroup="BA"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="left" valign="top">
                                            
                                                <asp:TextBox ID="txtValueQty" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="6"
                                                    TabIndex="22" Width="100"></asp:TextBox>
                                                    
                                            <cc11:FilteredTextBoxExtender ID="FiltertxtValueQty" runat="server"      TargetControlID="txtValueQty"    FilterType="Custom, Numbers"      />
                                                <br />
                                                <asp:RangeValidator ID="RangeValidator17" runat="server" ControlToValidate="txtValueQty"
                                                    Display="None" ErrorMessage="Please Enter  Value Quantity in Numeric &amp; Precision Should be 7 and Scale 2   "
                                                    MaximumValue="9999999.99" MinimumValue="0" Type="Double" ValidationGroup="BA"></asp:RangeValidator>
                                            </td>
                                             <td id="Td2" align="left" valign="top" runat="server" >
                                            <asp:TextBox ID="TxtWastage" runat="server" 
                                                    MaxLength="6" TabIndex="22" Width="100"></asp:TextBox>
                                                    &nbsp;&nbsp;
                                                  </td>
                                            <td align="left" valign="top">
                                                <asp:Button ID="BtnBaseSave" runat="server" OnClick="BtnBaseSave_Click" Text="Save"
                                                    ValidationGroup="BB" TabIndex="25" />
                                                <asp:Button ID="BtnBaseCancel" runat="server" OnClick="BtnBaseCancel_Click" Text="Cancel"
                                                    TabIndex="26" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="8" valign="top">
                                                <asp:GridView ID="grdBaseArticleDetail" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                    BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdBaseArticleDetail_RowCommand"
                                                    ShowFooter="True" Width="98%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" Width="25px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ProductType">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtProductType" runat="server" Text='<%# Bind("ProductType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Article Code">
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtArticleCode" runat="server" CssClass="Label SmallFont" ToolTip='<%# Bind("ArticleCode") %>'  Text='<%# Bind("ArticleDesc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Count">
                                                            
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCount1" runat="server" CssClass="Label SmallFont" Text='<%# Bind("Count") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBaseUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Basis">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBasis" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("Basis") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value Qty">
                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ValueQty") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Wastage">
                                                            
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCount" runat="server" CssClass="Label SmallFont" Text='<%# Bind("Wastage") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                            <ItemTemplate>
                                                                <asp:Button ID="lnkEdit0" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                                    CommandName="BaseEdit" TabIndex="12" Text="Edit" />
                                                                <asp:Button ID="lnkDelete0" runat="server" CommandArgument='<%# Eval("UniqueId") %>'
                                                                    CommandName="BaseDelete" OnClientClick="return confirm('Are you Sure want to delete this Bland?');"
                                                                    TabIndex="12" Text="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <RowStyle CssClass="RowStyle SmallFont" />
                                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                                    <PagerStyle CssClass="PagerStyle" />
                                                    <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                                </asp:GridView>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table> 
                        
                        </td>
                        </tr>
                        </table>
     
        </ContentTemplate>
    </asp:UpdatePanel>