<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fabric_Indent.ascx.cs"
    Inherits="Module_Fabric_FabricSaleWork_Controls_Fabric_Indent" %>
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
        zoom: 1;
        *display:inline;
        overflow:hidden;
        white-space:nowrap;
    }
    .header
    {
        margin-left:2px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 2px;
        width: 100px;
    }
    .c3
    {
        margin-left: 2px;
        width: 80px;
    }
    .c4
    {
        margin-left: 2px;
        width:80px;
    }
</style>

<%--<asp:UpdatePanel ID="updatePanel" runat="server">
<ContentTemplate>--%>

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
            <b class="titleheading">Fabric Indent Entry</b>
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
                            DataTextField="IND_NUMB" DataValueField='IND_NUMB' Width="150px" MenuWidth="350px"
                            Height="200px" CssClass="SmallFont" TabIndex="1" EmptyText="Find Indent Number"
                            OnLoadingItems="ddlIndentNumber_LoadingItems" 
                            OnSelectedIndexChanged="ddlIndentNumber_SelectedIndexChanged" 
                            EnableVirtualScrolling="True">
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
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
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
                        <asp:TextBox ID="txtComment" TabIndex="6" runat="server" Width="95%" CssClass="TextBox SmallFont"></asp:TextBox>
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
                        <span class="titleheading"><b>Fabric Code </b></span>
                    </td>
                    <td class="tdLeft SmallFont">
                        <span class="titleheading"><b>Description </b></span>
                    </td>
                    <td class="tdLeft SmallFont">
                        <span class="titleheading"><b>Shade </b></span>
                    </td>
                    <td class="tdRight SmallFont">
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
                    <td class="tdLeft SmallFont">
                        <span class="titleheading"><b>UOM </b></span>
                    </td>
                    <td class="tdRight SmallFont">
                        <span class="titleheading"><b>Amount </b></span>
                    </td>
                    <td class="tdLeft SmallFont">
                        <span class="titleheading"><b>Remarks </b></span>
                    </td>
                    <td align="left" valign="top" class="SmallFont">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <cc2:ComboBox ID="cmbFabricCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                            DataTextField="FABR_CODE" DataValueField="FABR_CODE" EnableLoadOnDemand="True"
                            MenuWidth="500px" OnLoadingItems="Item_LOV_LoadingItems" OnSelectedIndexChanged="Item_LOV_SelectedIndexChanged"
                            OpenOnFocus="true" TabIndex="7" Visible="true" Width="100px" Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    FABRIC CODE</div>
                                <div class="header c2">
                                    FABRIC DESC</div>
                                <div class="header c3">
                                   FABRIC TYPE</div>
                                    <div class="header c4">
                                    FABRIC CAT</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("FABR_CODE") %></div>
                                <div class="item c2">
                                    <%# Eval("FABR_DESC") %></div>
                                <div class="item c3">
                                    <%# Eval("FABR_TYPE")%></div>
                                     <div class="item c4">
                                    <%# Eval("FABR_CATEGORY")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        &nbsp;
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtFabricDesc" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            Width="150px"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                      <asp:DropDownList ID="cmbShade" runat="server" 
                                        AppendDataBoundItems="True">
                                        <asp:ListItem Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtOpeningStock" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            Width="60px"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtOpeningPartyStock" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            Width="60px"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtOpeningRate" runat="server" ReadOnly="true" onChange="javascript:Calculation(this.id)"
                            CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="60px"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtRequestQty" runat="server" onkeyup="javascript:Calculation(this.id)"
                            CssClass="TextBoxNo SmallFont" Width="70px" TabIndex="8" MaxLength="6"></asp:TextBox>
                        <asp:RangeValidator ID="r1" runat="server" ControlToValidate="txtRequestQty" ErrorMessage="*Enter Quantity"
                            MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                            Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                   <cc1:FilteredTextBoxExtender ID="FiltertxtRate" runat="server"  TargetControlID="txtRequestQty"   FilterType="Custom, Numbers" ValidChars="."/>

                   
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            Width="25px"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            Width="80px"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox SmallFont" Width="80px"
                            TabIndex="9"></asp:TextBox>
                    </td>
                    <td align="center" valign="top">
                        
                        <asp:Button ID="lbtnsavedetail" runat="server" Text="Save" Height="21px" Width="51px"
                            OnClick="lbtnsavedetail_Click" />
                       
                        <asp:Button ID="lbtnCancel" runat="server" Text="Cancel" Height="21px" Width="51px"
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
               
                <asp:GridView ID="grdIndentDetail" runat="server" CssClass="SmallFont" Font-Bold="False"
                   ShowFooter="True" BorderWidth="1px"  
                    AutoGenerateColumns="False" AllowSorting="True" Width="98%" 
                    onrowcommand="grdIndentDetail_RowCommand1">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fabric Code">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtFabricCode" Font-Bold="true" runat="server" Text='<%# Bind("FABR_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fabric Description">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtFabricDesc" runat="server" Text='<%# Bind("FABR_DESC") %>' CssClass="Label SmallFont"></asp:Label>
                                <asp:TextBox ID="txtIndentDetailNumber" runat="server" Text='<%# Bind("IndentDetailNumber") %>'
                                    Width="50px" Visible="False" Enabled="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade Code">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtShadeCode" runat="server" Text='<%# Bind("SHADE_CODE") %>' CssClass="Label SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opening Stock">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="txtOpenmingStock" runat="server" Text='<%# Bind("OP_BAL_STOCK") %>'
                                    CssClass="LabelNo SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opening Party Stock" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtOpeningPartyStock" runat="server" Text='<%# Bind("OPBAL_PRTY_STOK") %>'
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
                        <asp:TemplateField HeaderText="UOM" Visible="false">
                            <FooterTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>
                            </FooterTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="3%"></ItemStyle>
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="txtUnitName" runat="server" Text='<%# Bind("UOM") %>' CssClass="Label SmallFont"></asp:Label>
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
                                <asp:Label ID="txtRemark" runat="server" Text='<%# Bind("REMARK") %>' CssClass="SmallFont"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField ItemStyle-Width="130px">
                                    <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Button ID="lnkEdit" Text="Edit" runat="server" CommandName="indentEdit" TabIndex="12"
                                            CommandArgument='<%# Eval("UniqueId") %>' Width="50px"></asp:Button>
                                            <asp:Button ID="lnkDelete"
                                                runat="server" Text="Delete" CommandName="indentDelete" TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>' Width="50px" >
                                            </asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
                <asp:Label ID="Label2" runat="server" Text="Amounts In Words :"></asp:Label>
                <asp:Label ID="lblAmountInWords" runat="server"></asp:Label>               
            </asp:Panel>
           
        </td>
    </tr>
</table>

<%--</ContentTemplate>
</asp:UpdatePanel>--%>