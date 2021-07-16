<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FIBER_LotWiseRecipeEntry.ascx.cs" Inherits="Module_OrderDevelopment_LabDip_Controls_FIBER_LotWiseRecipeEntry" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
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
                width: 100px;
            }
            .c2
            {
               
                width: 120px;
            }
            .c3
            {
               
                width:80px;
            }
            .c4
            {
                margin-left: 4px;
                width: 350px;
            }
            .c5
            {
                margin-left: 4px;
                width: 100px;
            }
        </style>
        <style type="text/css">
            .AutoExtender
            {
                font-family: Verdana, Helvetica, sans-serif;
                font-size: .8em;
                font-weight: normal;
                border: solid 1px #006699;
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
                width: 150px !important;
            }
            #divwidth div
            {
                width: 150px !important;
            }
        </style>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
        <table class="tContentArial">
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnNew" runat="server" Height="41px" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="M1" Width="48px" 
                                     />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41px" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" 
                                    Width="48px" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41px" ImageUrl="~/CommonImages/del6.png"
                                    OnClick="imgbtnDelete_Click" ToolTip="Delete" Width="48px" 
                                     />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Height="41px" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" ToolTip="Find" Width="48px" 
                                     />
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41px" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48px" 
                                     />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41px" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48px" 
                                     />
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41px" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48px" 
                                     />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41px" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48px" 
                                     />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">Lot Wise Recipe Entry</span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="M1" 
                         />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage" 
                        ></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError" 
                        ></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td class="tdRight">
                                Customer Req. No. :
                            </td>
                            <td class="tdLeft">
                                <cc2:ComboBox ID="ddlOrderNo" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    EnableLoadOnDemand="True" DataTextField="ORDER_NO" DataValueField="ORDER_NO"
                                    EmptyText="Select Shade no" MenuWidth="700px" OnLoadingItems="ddlOrderNo_LoadingItems"
                                    OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged" 
                                    EnableVirtualScrolling="True" TabIndex="11" Height="200px" Width="130px">
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Shade No</div>
                                        <div class="header c2">
                                            CR.NO</div>
                                        <div class="header c3">
                                            Quality Code</div>
                                        <div class="header c4">
                                            Quality Desc
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("SHADE_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("ORDER_NO")%></div>
                                        <div class="item c3">
                                            <%# Eval("ARTICAL_NO")%></div>
                                        <div class="item c4">
                                            <%# Eval("ARTICAL_DESC")%></div>
                                    </ItemTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight">
                               Shade No:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtShadeNo" runat="server" CssClass="TextBox TextBoxDisplay" Width="100px"
                                    ReadOnly="True" TabIndex="2"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                               Cr. Date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="TextBox TextBoxDisplay" Width="100px"
                                    ReadOnly="True" TabIndex="2"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                Branch:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtBranch" runat="server" CssClass="TextBox TextBoxDisplay" Width="100px"
                                    TabIndex="3" ReadOnly="True" ></asp:TextBox>
                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="1px" TabIndex="4" ReadOnly="True" Visible="False" 
                                   ></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                Entry Date :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtSubmissionDate" runat="server" CssClass="TextBox" Width="100px"
                                    TabIndex="5" ></asp:TextBox>
                            </td>
                        </tr>
                        
                         <tr>
                            <td class="tdRight">
                                Customer :
                            </td>
                            <td class="tdLeft" colspan="3">
                                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="TextBox TextBoxDisplay"
                                    ReadOnly="True" Width="100px" 
                                  ></asp:TextBox>
                                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="TextBox TextBoxDisplay"
                                    ReadOnly="True" Width="240px"  
                                   ></asp:TextBox>
                            </td>
                            <td class="tdLeft">
                                Shade Code :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtShadeCode" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="100px" TabIndex="4" ReadOnly="True"></asp:TextBox>
                            </td>
                             <td class="tdRight">
                                Shade Category :
                            </td>
                            <td class="tdLeft" >
                                 <asp:DropDownList ID="ddlShadeCat" runat="server" AppendDataBoundItems="True"
                                         CssClass="SmallFont "   Width="100px">
                                         <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                           <asp:ListItem Value="Sensitive">Sensitive</asp:ListItem>
                                        </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                Nature Shade :
                            </td>
                            <td class="tdLeft" >
                                <asp:DropDownList ID="ddlNatureShade" runat="server" AppendDataBoundItems="True"
                                         CssClass="SmallFont "   Width="100px">
                                         <asp:ListItem Value="Light">Light</asp:ListItem>
                                         <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                           <asp:ListItem Value="Dark">Dark</asp:ListItem>
                                        </asp:DropDownList>
                            </td>
                        </tr>
                        
                         <tr>
                            <td class="tdRight">
                                Quality Desc:
                            </td>
                            <td class="tdLeft" colspan="7">
                                <asp:TextBox ID="txtQualityCode" runat="server" CssClass="TextBox TextBoxDisplay"
                                    ReadOnly="True" Width="100px" TabIndex="6" 
                                  ></asp:TextBox>
                                <asp:TextBox ID="txtQualityDesc" runat="server" CssClass="TextBox TextBoxDisplay"
                                    ReadOnly="True" Width="650px" TabIndex="7" 
                                   ></asp:TextBox>
                            </td>
                        </tr>
                       
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" valign="top" width="100%">
                    <table>
                        <tr>
                            <td>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr bgcolor="#336699" class="titleheading">
                                        <td>
                                            Lab Dip No.
                                        </td>
                                        <td>
                                            Option
                                        </td>
                                        <td>
                                            Quality Code
                                        </td>
                                        <td>
                                           Artical No.
                                        </td>                                         
                                        <td>
                                           Shade Family
                                        </td>                                                                               
                                        <td>
                                           Party Desc
                                        </td>
                                        <td>
                                        Base Lot NO.
                                        </td>
                                        <td id="trQuality" runat="server">
                                            Quality
                                        </td>
                                        <td>
                                            Recipe
                                        </td>
                                        <td>
                                            Recipe Cost
                                        </td>
                                        <td>
                                            Depth
                                        </td>
                                        <td>
                                        Remarks
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlLRNo" runat="server" Width="100px" TabIndex="8" CssClass="SmallFont"
                                                DataValueField="LAB_DIP_NO" DataTextField="LAB_DIP_NO" AutoPostBack="True" 
                                                OnSelectedIndexChanged="ddlLRNo_SelectedIndexChanged" Height="16px" 
                                                >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlOption" runat="server" Width="40px" TabIndex="9" CssClass="SmallFont"
                                                AutoPostBack="True" 
                                                OnSelectedIndexChanged="ddlOption_SelectedIndexChanged" 
                                               >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtArticalNo" runat="server" TabIndex="10" CssClass="SmallFont TextBoxDisplay"
                                                Width="100px" ReadOnly="True"></asp:TextBox>                                                                                       
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtOrderRefNo" runat="server" TabIndex="10" CssClass="SmallFont TextBoxDisplay"
                                                Width="100px" ReadOnly="True"></asp:TextBox>                                             
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlShadeFamily" runat="server" Width="100px" TabIndex="13"
                                                AutoPostBack="True" CssClass="SmallFont" 
                                                DataValueField="SHADE_FAMILY_CODE" DataTextField="SHADE_FAMILY_CODE"
                                                OnSelectedIndexChanged="ddlShadeFamily_SelectedIndexChanged" 
                                              >
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                         
                                       
                                            <asp:TextBox ID="ddlShade" CssClass="SmallFont TextBox UpperCase" runat="server"
                                                ValidationGroup="M1" Width="100px" 
                                               ></asp:TextBox>
                                             <div id="divwidth">
                                             </div>
                                             <cc4:AutoCompleteExtender ID="aceShade" runat="server" TargetControlID="ddlShade"
                                                BehaviorID="autoComplete" EnableCaching="true" CompletionSetCount="12" CompletionListCssClass="AutoExtender"
                                                CompletionListItemCssClass="AutoExtenderList" CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                CompletionListElementID="divwidth" Enabled="true" CompletionInterval="100" UseContextKey="true"
                                                MinimumPrefixLength="1" ServiceMethod="GetShadeCodeThroughShadeFamily" ServicePath="~/AutoComplete.asmx">
                                            </cc4:AutoCompleteExtender>
                                          
                                        </td>
                                        
                                        <td >
                          <cc2:ComboBox ID="txtLotNo" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtLotNo_LoadingItems" DataTextField="LOT_NO" DataValueField="FIBER_CODE"
                           AutoPostBack="True"   EmptyText="Lot No"  OnSelectedIndexChanged ="txtLotNo_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="110px" MenuWidth="300px" Height="200px"  TabIndex="28">
                            <HeaderTemplate>
                                <div class="header c5">
                                    Lot No</div>
                                <div class="header c5">
                                    Fiber Code</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("LOT_NO") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("FIBER_CODE") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                 </td>
                                        <td id="trQuality1" runat="server">
                                            <asp:TextBox ID="txtQuality" runat="server" TabIndex="15" CssClass="SmallFont TextBoxDisplay"
                                                Width="80px" ReadOnly="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDyeName" runat="server" Text="Dye & Dose" Width="90px" TabIndex="16"
                                                ValidationGroup="M1" OnClick="btnDyeName_Click" 
                                                ></asp:Button>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRecipeCost" runat="server" TabIndex="17" CssClass="TextBoxNo TextBoxDisplay"
                                                Width="70px" OnTextChanged="txtRecipeCost_TextChanged" ReadOnly="True" 
                                                ></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDepth" runat="server" TabIndex="18" 
                                                CssClass="TextBoxNo TextBoxDisplay" Width="70px" 
                                                OnTextChanged="txtDepth_TextChanged" ReadOnly="True" 
                                                ></asp:TextBox>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtRemarks" runat="server" TabIndex="18" CssClass="TextBoxNo TextBoxDisplay" Width="110px"></asp:TextBox>

                                        </td>
                                        <td>
                                        <asp:Button ID="btnSaveDetail" runat="server" Text="Save"
                                                Width="60px" OnClick="btnSaveDetail_Click" TabIndex="19" 
                                                ValidationGroup="M1" >
                                            </asp:Button>
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="60px" OnClick="btnCancel_Click"
                                                TabIndex="20" ></asp:Button>
                                        </td>                                      
                                       
                                     
                                    </tr>
                                    
                              <tr>
                           <%-- <td class="tdRight">
                                Quality Description :
                            </td>--%>
                            <%--<td class="tdLeft" colspan="7">
                                <asp:TextBox ID="txtQualityDesc" runat="server" CssClass="TextBox TextBoxDisplay"
                                    ReadOnly="True" Width="650px" TabIndex="7" 
                                   ></asp:TextBox>
                            </td>--%>
                        </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdLabDipSubmission" runat="server" AutoGenerateColumns="False"
                                    CssClass="SmallFont" Width="1100px" TabIndex="21" 
                                    OnRowCommand="grdLabDipSubmission_RowCommand" 
                                    
                                    onrowdatabound="grdLabDipSubmission_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Lab Dip No." 
                                           >
                                            <ItemTemplate>
                                                <asp:Label ID="lblLRNo" CssClass="LabelNo" runat="server" 
                                                    Text='<%# Bind("LAB_DIP_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Option" 
                                           >
                                            <ItemTemplate>
                                                <asp:Label ID="lblOption" runat="server" Text='<%# Bind("LR_OPTION") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quality Code" 
                                          >
                                            <ItemTemplate>
                                                <asp:Label ID="lblarticalNo" runat="server" Text='<%# Bind("ARTICAL_NO") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="130px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quality Description" 
                                          >
                                            <ItemTemplate>
                                                <asp:Label ID="lblQualityDesc" runat="server" Text='<%# Bind("ARTICAL_DESC") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="130px" />
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Shade Family Code" Visible="False" 
                                           >
                                            <ItemTemplate>
                                                <asp:Label ID="lblShadeFamilyCode" runat="server" Text='<%# Bind("SHADE_FAMILY_CODE_OLD") %>'
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shade Family" 
                                           >
                                            <ItemTemplate>
                                                <asp:Label ID="lblShadeFamilyName" runat="server" Text='<%# Bind("SHADE_FAMILY_NAME") %>'
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shade Code" Visible="False" 
                                            >
                                            <ItemTemplate>
                                                <asp:Label ID="lblShadeCode" runat="server" Text='<%# Bind("SHADE_CODE_OLD") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Party Desc">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShadeName" runat="server" Text='<%# Bind("SHADE_NAME_OLD") %>' 
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Base Lot No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGreyLot" runat="server" Text='<%# Bind("GREY_LOT_NO") %>' 
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Quality" Visible="False" 
                                           >
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuality" runat="server" Text='<%# Bind("QUALITY") %>' 
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recipe Cost" 
                                            >
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecipeCost" runat="server" Text='<%# Bind("TOTAL_RECIPE_COSE") %>'
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Depth Code" Visible="False" 
                                            >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepthCode" runat="server" Text='<%# Bind("DEPTH") %>' 
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Depth" 
                                           >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepthName" runat="server" Text='<%# Bind("DEPTH_NAME") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ORDER_REF_NO" HeaderText="By&nbsp;Reff. Np" />
                                        <asp:TemplateField HeaderText="Remarks" 
                                            >
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnViewPOTRN" runat="server" Text="View Details"></asp:LinkButton>
                            <asp:Panel ID="pnlPOTRN" runat="server"  BackColor="Beige" BorderWidth="2px">
                                <asp:GridView ID="grdPOTRNAprove" runat="server" AutoGenerateColumns="False" Width="500px"
                                    CssClass="SmallFont" >
                                    <Columns>
                                    <asp:BoundField DataField="LR_OPTION" HeaderText="LR&nbsp;Option">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DYE_NAME" HeaderText="Dye&nbsp;Name">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item&nbsp;Description">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RATE" HeaderText="Rate">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DOSE" HeaderText="Dose&nbsp;%" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RECIPE_COST" HeaderText="Recipe&nbsp;Cost" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                </asp:GridView>
                            </asp:Panel>
                            <cc4:HoverMenuExtender ID="hmePOTRN" runat="server" PopupPosition="Left" PopupControlID="pnlPOTRN"
                                TargetControlID="lbtnViewPOTRN">
                            </cc4:HoverMenuExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandArgument='<%#bind("UNIQUE_ID") %>'
                                                    CommandName="EditTRN" Text="Edit" TabIndex="22" 
                                                     />
                                                <asp:Button ID="btnDelete" runat="server" CommandArgument='<%#bind("UNIQUE_ID") %>'
                                                    CommandName="DeleteTRN" Text="Delete" TabIndex="23" 
                                                   />
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle VerticalAlign="Top" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtSubmissionDate"
            PopupPosition="TopRight" OnClientDateSelectionChanged="checkDate" 
            Format="dd/MM/yyyy" Enabled="True">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtSubmissionDate" 
            CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
            CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
        </cc4:MaskedEditExtender>
    </ContentTemplate>
    </asp:UpdatePanel>  