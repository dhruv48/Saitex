<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_Indent.ascx.cs" Inherits="Module_Fiber_Fiber_Indent1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<script type="text/javascript" language="javascript">
  
    function GetClick(ButtonId) {
        document.getElementById(ButtonId).click();
    }
    function Calculation(val) {
        var name = val;
        document.getElementById('ctl00_cphBody_Fabric_Indent1_txtAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_Fabric_Indent1_txtOpeningRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_Fabric_Indent1_txtRequestQty').value))).toFixed(3);
    }
    function GetRemarkFocus() {
        document.getElementById('ctl00_cphBody_txtRemark').focus();

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
        margin-left: 4px;
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
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
    .SmallFont {
        top: 0px;
        left: 0px;
    }
    .smallfont {
        top: 0px;
        left: 0px;
    }
</style>
<asp:UpdatePanel runat="server" id="UpdatePanel1">
    <ContentTemplate>
<table class="tContentArial" align="center" width="95%">
    <tr>
        <td valign="top" class="td" align="left" width="100%">
            <table align="left">
                <tr>
                    <td id="tdSave" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnSave" TabIndex="9" OnClick="imgbtnSave_Click" runat="server"
                            ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" ValidationGroup="M1">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" TabIndex="9" OnClick="imgbtnUpdate_Click" runat="server"
                            ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48" ValidationGroup="M1">
                        </asp:ImageButton>
                    </td>
                    <td id="tdDelete" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" TabIndex="9" OnClick="imgbtnDelete_Click1" runat="server"
                            ImageUrl="~/CommonImages/del6.png" ToolTip="Delete" Height="41" Width="48" ValidationGroup="M1">
                        </asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnFind" TabIndex="9" OnClick="imgbtnFind_Click" runat="server"
                            ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" Height="41" Width="48">
                        </asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <cc1:CalendarExtender ID="c2" runat="server" TargetControlID="txtRequiredBefore"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td class="TableHeader" class="td" align="center" width="100%">
            <b class="titleheading">Fiber Indent Entry</b>
        </td>
    </tr>
    <tr>
        <td class="td" valign="top" align="left" width="100%">
            <span style="color: #ff0000">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
            </asp:Label>&nbsp;Mode</span>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="M1" />
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label111" runat="server" Text="Indent Number :" CssClass="Label "
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtIndentNumber" TabIndex="1" runat="server" Width="70px" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Font-Bold="True" ontextchanged="txtIndentNumber_TextChanged"></asp:TextBox>
                        <cc2:ComboBox ID="ddlIndentNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            DataTextField="IND_NUMB" DataValueField='IND_NUMB' Width="80px" MenuWidth="350px"
                            Height="200px" CssClass="SmallFont" TabIndex="1" EmptyText="Find Indent Number"
                            OnLoadingItems="ddlIndentNumber_LoadingItems" 
                            OnSelectedIndexChanged="ddlIndentNumber_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Indent Number</div>
                                <div class="header c1">
                                    Indent Date</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("IND_NUMB") %>' /></div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("IND_DATE") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label112" runat="server" Text="Indent Date :" CssClass="Label "></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtIndentDate" TabIndex="2" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label113" runat="server" Text="Department :" CssClass="Label " Font-Bold="False"></asp:Label>
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:TextBox ID="txtDepartment" TabIndex="3" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                            Font-Bold="False" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label114" runat="server" Text="Prepared By :" CssClass="Label "></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:TextBox ID="txtPreparedBy" TabIndex="4" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True"></asp:TextBox>
                    </td>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label115" runat="server" Text="Required Before :" CssClass="Label "></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="3" width="55%">
                        <asp:TextBox ID="txtRequiredBefore" TabIndex="5" ReadOnly="false" runat="server"
                            Width="70px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label116" runat="server" Text="Comment :" CssClass="Label "></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="5" width="85%">
                        <asp:TextBox ID="txtComment" TabIndex="6" runat="server" Width="95%" CssClass="TextBox SmallFont" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" valign="top" width="100%">
            <table width="98%">
                <tr bgcolor="#006699">
                    <td class="tdLeft SmallFont">
                        <span class="titleheading"><b>Fiber Code </b></span>
                    </td>
                   <td class="tdLeft SmallFont">
                        <span class="titleheading"><b>Description </b></span>
                    </td>
                 <%--  <td   class="tdLeft SmallFont" >
                        <span class="titleheading"><b>Shade </b></span>
                    </td>--%>
                    <td id = "tdshad" class="tdRight SmallFont">
                        <span class="titleheading"><b>Stock </b></span>
                    </td>
                    <td class="tdRight SmallFont">
                        <span class="titleheading"><b>Min Stock </b></span>
                    </td>
                    <td class="tdRight SmallFont">
                        <span class="titleheading"><b>Rate </b></span>
                    </td>
                    <td class="tdRight SmallFont">
                        <span class="titleheading"><b>Quantity </b></span>
                    </td>
                    <td class="tdRight SmallFont">
                        <span class="titleheading"><b>UOM1 </b></span>
                    </td>
                      <td class="tdRight SmallFont">
                        <span class="titleheading"><b>UOM2 </b></span>
                    </td>
                      <td class="tdRight SmallFont">
                        <span class="titleheading"><b>kg/Bail </b></span>
                    </td>
                    <td class="tdRight SmallFont">
                        <span class="titleheading"><b>Amount </b></span>
                    </td>
                    <td class="tdRight SmallFont">
                        <span class="titleheading"><b>Remarks </b></span>
                    </td>
                    <td align="left" valign="top" class="SmallFont">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <cc2:ComboBox ID="cmbFabricCode" runat="server" AutoPostBack="true" CssClass="smallfont"
                            DataTextField="FIBER_CODE" DataValueField="FIBER_CODE" EnableLoadOnDemand="True"
                            MenuWidth="400px" OnLoadingItems="Item_LOV_LoadingItems" 
                            OpenOnFocus="true" TabIndex="7" Visible="true" Width="100px" 
                            Height="200px" onselectedindexchanged="cmbFabricCode_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                   FIBER CODE</div>
                                <div class="header c2">
                                    FIBER DESCRIPTION</div>
                                <div class="header c3">
                                    TYPE</div>
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
                        &nbsp;
                    </td>
                 <td align="left" valign="top">
                 <asp:TextBox ID="txtFabricDesc" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            Width="150px"  ></asp:TextBox>
                      </td>
                 <%--   <td align="left" valign="top">
                      <asp:DropDownList ID="cmbShade" runat="server" AutoPostBack="True"  Visible = "false" 
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                    </td>--%>
                    <td align="right" valign="top">
                        <asp:TextBox ID="txtOpeningStock" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            Width="60px"></asp:TextBox>
                            
                             
                    
                    </td>
                    <td align="right" valign="top">
                        <asp:TextBox ID="txtOpeningPartyStock" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            Width="60px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        <asp:TextBox ID="txtOpeningRate" runat="server" ReadOnly="true" onChange="javascript:Calculation(this.id)"
                            CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="60px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        <asp:TextBox ID="txtRequestQty" runat="server" onkeyup="javascript:Calculation(this.id)"
                            CssClass="TextBoxNo SmallFont" Width="70px" TabIndex="8" 
                            ontextchanged="txtRequestQty_TextChanged1" AutoPostBack="true" MaxLength="7"></asp:TextBox>
                        <asp:RangeValidator ID="r1" runat="server" ControlToValidate="txtRequestQty" ErrorMessage="*Enter Quantity"
                            MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                            Display="Dynamic" SetFocusOnError="True" ></asp:RangeValidator>
                            
                              <cc1:FilteredTextBoxExtender ID="FiltertxtRequestQty" runat="server"
                                                       TargetControlID="txtRequestQty"         
                                                       FilterType="Custom, Numbers" ValidChars="."
                                                            />
                            
                            
                            
                            
                    </td>
                    <td align="right" valign="top">
                        <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            Width="25px"></asp:TextBox>
                    </td>
                      <td align="right" valign="top">
                        <asp:TextBox ID="Txtuom2" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            Width="25px"></asp:TextBox>
                    </td>
                      <td align="right" valign="top">
                        <asp:TextBox ID="Txtkg_bail" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            Width="25px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            Width="80px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox SmallFont" Width="60px"
                            TabIndex="9" MaxLength="85"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <%--<cc3:OboutButton ID="lbtnsavedetail" Text="Save" runat="server" TabIndex="10" OnClick="lbtnsavedetail_Click">
                                </cc3:OboutButton>--%>
                        <asp:Button ID="lbtnsavedetail" runat="server" Text="Save" Height="22px" Width="60px" CssClass="SmallFont"
                            OnClick="lbtnsavedetail_Click" />
                        <%--   <cc3:OboutButton ID="lbtnCancel" Text="Cancel" runat="server" TabIndex="11" OnClick="lbtnCancel_Click1">
                        </cc3:OboutButton>--%>
                        <asp:Button ID="lbtnCancel" runat="server" Text="Cancel" Height="22px" Width="60px" CssClass="SmallFont"
                            OnClick="lbtnCancel_Click1" />
                        <asp:Label ID="lblMin_Procure_days" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trGridView" runat="server">
        <td class="td" align="left" width="100%">
            <asp:Panel ID="pnlGrid" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">
                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                <asp:GridView ID="grdIndentDetail" runat="server" CssClass="SmallFont" Font-Bold="False"
                    OnRowCommand="grdIndentDetail_RowCommand" ShowFooter="True" BorderWidth="1px"
                    AutoGenerateColumns="False" AllowSorting="True" Width="98%">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fiber Code">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtFabricCode" Font-Bold="true" runat="server" Text='<%# Bind("FIBER_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fiber Description">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtFabricDesc" runat="server" Text='<%# Bind("FIBER_DESC") %>' CssClass="Label SmallFont"></asp:Label>
                                <asp:TextBox ID="txtIndentDetailNumber" runat="server" Text='<%# Bind("IndentDetailNumber") %>'
                                    Width="50px" Visible="False" Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                <%--   <asp:TemplateField HeaderText="Shade Code" >
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtShadeCode" runat="server" Text='<%# Bind("SHADE_CODE") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Opening Stock">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="txtOpenmingStock" runat="server" Text='<%# Bind("OP_BAL_STOCK") %>'
                                    CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opening Party Stock">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtOpeningPartyStock" runat="server" Text='<%# Bind("OP_BAL_PRTY_STOK") %>'
                                    CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opening Rate">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtOpeningRate" runat="server" Text='<%# Bind("OP_RATE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtRequestQty" runat="server" Font-Bold="true" Text='<%# Bind("QTY") %>'
                                    CssClass="LabelNo SmallFont" AutoPostBack="True" OnTextChanged="txtRequestQty_TextChanged"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <FooterTemplate>
                                <%--<asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>--%>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="3%"></ItemStyle>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="txtUnitName" runat="server" Text='<%# Bind("UOM") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="UOM1">
                            <FooterTemplate>
                               <%-- <asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>--%>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="3%"></ItemStyle>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="txtUnitName1" runat="server" Text='<%# Bind("UOM1") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="UOM1">
                            <FooterTemplate>
                               <asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="3%"></ItemStyle>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="txtUnitBAIL" runat="server" Text='<%# Bind("UOM_BAIL") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <FooterTemplate>
                                <asp:Label ID="txtFooterAmount" runat="server" Text='<%# Bind("Amount") %>' CssClass="LabelNo SmallFont"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <FooterStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" Font-Bold="true" Text='<%# Bind("Amount") %>'
                                    CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtRemark" runat="server" Text='<%# Bind("REMARK") %>' CssClass="SmallFont"  ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="120px">
                            <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CommandName="indentEdit"
                                    TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'>                     
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" CommandName="indentDelete"
                                    TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'>                     
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
                <asp:Label ID="Label2" runat="server" Text="Amounts In Words :"></asp:Label>
                <asp:Label ID="lblAmountInWords" runat="server"></asp:Label>
                <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtIndentNumber" EventName="TextChanged"></asp:AsyncPostBackTrigger>
                                    <asp:AsyncPostBackTrigger ControlID="grdIndentDetail" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>--%>
            </asp:Panel>
            <asp:Button ID="Button1" runat="server" Text="" Width="1pt" OnClick="Button1_Click" Visible="false" CausesValidation="false" />
        </td>
    </tr>
</table>
            </ContentTemplate>
</asp:UpdatePanel>
