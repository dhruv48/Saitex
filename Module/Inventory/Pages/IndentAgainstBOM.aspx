<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="IndentAgainstBOM.aspx.cs" Inherits="Module_Inventory_Pages_IndentAgainstBOM"
    Title="Untitled Page" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">

    <script type="text/javascript" language="javascript">
   
    function Calculation(val)
    { 
     var name=val;                                                               
     document.getElementById('ctl00_cphBody_txtAmount').value=(Math.round(parseFloat(document.getElementById('ctl00_cphBody_txtOpeningRate').value)*(parseFloat(document.getElementById('ctl00_cphBody_txtRequestQty').value)))).toFixed(2) ;
   }
    function GetRemarkFocus()
    {
     document.getElementById('ctl00_cphBody_txtRemark').focus();
  
    }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
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
            width: 80px;
        }
        .style1
        {
            border: .05em ridge #C1D3FB;
            height: 48px;
        }
        .style3
        {
            width: 268435456px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="tContentArial" align="center" width="95%">
                <tr>
                    <td valign="top" class="td" align="left" width="100%">
                        <table align="left">
                            <tr>
                                <td id="tdSave" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server"
                                        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>
                                <td id="tdUpdate" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate"  OnClick="imgbtnUpdate_Click" runat="server"
                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>
                                <td id="tdDelete" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnDelete"  OnClick="imgbtnDelete_Click1" runat="server"
                                        ImageUrl="~/CommonImages/del6.png" ToolTip="Delete" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnFind"  OnClick="imgbtnFind_Click" runat="server"
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
                        <cc1:CalendarExtender ID="c2" runat="server" TargetControlID="txtRequiredBefore">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="TableHeader" class="td" align="center" width="100%">
                        <b class="titleheading">Indent Against BOM</b>
                    </td>
                </tr>
                <tr>
                    <td class="style1" valign="top" align="left" width="100%">
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
                                    <asp:TextBox ID="txtIndentNumber" TabIndex="1" runat="server" Width="150px" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                        Font-Bold="True" AutoPostBack="True" OnTextChanged="txtIndentNumber_TextChanged"></asp:TextBox>
                                    <cc2:ComboBox ID="ddlIndentNumber" runat="server" TabIndex="1" Width="150px" MenuWidth="300px"
                                        AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                                        Height="200px" OnLoadingItems="ddlIndentNumber_LoadingItems" OnSelectedIndexChanged="ddlIndentNumber_SelectedIndexChanged">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Indent #</div>
                                            <div class="header c2">
                                                Indent Date</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("IND_NUMB") %></div>
                                            <div class="item c2">
                                                <%# Eval("IND_DATE") %></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td class="tdRight" width="15%">
                                    <asp:Label ID="Label112" runat="server" Text="Indent Date :" CssClass="Label "></asp:Label>
                                </td>
                                <td class="tdLeft" width="15%">
                                    <asp:TextBox ID="txtIndentDate" TabIndex="2" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                                <td class="tdRight" width="15%">
                                    <asp:Label ID="Label113" runat="server" Text="Department :" CssClass="Label " Font-Bold="False"></asp:Label>
                                </td>
                                <td class="tdLeft" width="25%">
                                    <asp:TextBox ID="txtDepartment" TabIndex="3" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                        Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right" width="15%" >
                                    <asp:Label ID="Label114" runat="server" Text="Prepared By :" CssClass="Label "></asp:Label>
                                </td>
                                <td valign="top" align="left" width="15%" >
                                    <asp:TextBox ID="txtPreparedBy" TabIndex="4" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="True"></asp:TextBox>
                                </td>
                                <td valign="top" align="right" width="15%" >
                                    <asp:Label ID="Label115" runat="server" Text="Required Before :" CssClass="Label "></asp:Label>
                                </td>
                                <td valign="top" align="left" colspan="1" width="15%" >
                                    <asp:TextBox ID="txtRequiredBefore" TabIndex="5" ReadOnly="false" runat="server"
                                        Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                                </td>
                                <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label3" runat="server" Text="Location :" CssClass="Label "></asp:Label>
                                </td>
                                <td >
                                  <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Width="150px"
                                             TabIndex="6" >   </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="right" width="15%">
                                    <asp:Label ID="Label116" runat="server" Text="Comment :" CssClass="Label "></asp:Label>
                                </td>
                                <td valign="top" align="left" colspan="3" width="15%">
                                    <asp:TextBox ID="txtCommentComment" TabIndex="8" runat="server" Width="95%" CssClass="TextBox SmallFont"
                                        ></asp:TextBox>
                                </td>
                                <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label4" runat="server" Text="Store :" CssClass="Label "></asp:Label>
                                </td>
                                 <td>
                                 
<asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Width="150px"
                                        TabIndex="7" >
                                    </asp:DropDownList>
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
                                    <span class="titleheading"><b>Item</b></span>
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
                                    Adj BOM
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
                                    <asp:TextBox ID="txtItemDesc" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                        ReadOnly="true" Width="100px"></asp:TextBox>
                                    <br />
                                    <cc2:ComboBox ID="txtItemCode" TabIndex="9" runat="server" AutoPostBack="True" CssClass="smallfont"
                                        DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" EnableLoadOnDemand="True"
                                        MenuWidth="350px" OnLoadingItems="Item_LOV_LoadingItems" OnSelectedIndexChanged="Item_LOV_SelectedIndexChanged"
                                        OpenOnFocus="true"  Visible="true" Width="100px" Height="200px">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                ITEM CODE</div>
                                            <div class="header c2">
                                                ITEM DESCRIPTION</div>
                                            <div class="header c3">
                                                TYPE</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("ITEM_CODE") %></div>
                                            <div class="item c2">
                                                <%# Eval("ITEM_DESC") %></div>
                                            <div class="item c3">
                                                <%# Eval("ITEM_TYPE") %></div>
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
                                    <asp:TextBox ID="txtCurrentStock" TabIndex="10" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                        Width="60px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtMinStockLevel" TabIndex="11" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                        Width="60px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtOpeningRate" runat="server" TabIndex="12" ReadOnly="true" onChange="javascript:Calculation(this.id)"
                                        CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="60px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Button ID="btnAdjBOM"  TabIndex="13" Text="Adj." Width="40px" runat="server" OnClick="btnAdjBOM_Click">
                                    </asp:Button>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRequestQty" TabIndex="14" runat="server" onkeyup="javascript:Calculation(this.id)"
                                        CssClass="TextBoxNo SmallFont" Width="70px"  OnTextChanged="txtRequestQty_TextChanged1"
                                        ReadOnly="True"></asp:TextBox>
                                    <asp:RangeValidator ID="r1" runat="server" ControlToValidate="txtRequestQty" ErrorMessage="*Enter Quantity"
                                        MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                        Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtUnitName"  TabIndex="15" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                                        Width="35px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtAmount" TabIndex="15" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                        Width="80px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRemark" TabIndex="16" runat="server" CssClass="TextBox SmallFont" Width="60px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Button ID="lbtnsavedetail" Text="Save" runat="server" TabIndex="17" OnClick="lbtnsavedetail_Click">
                                    </asp:Button>
                                    <asp:Button ID="lbtnCancel" Text="Cancel" runat="server" TabIndex="18" OnClick="lbtnCancel_Click1">
                                    </asp:Button>
                                    <asp:Label ID="lblMin_Procure_days" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trGridView" runat="server">
                    <td class="td" align="left" width="100%">
                        <asp:Panel ID="pnlGrid" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdIndentDetail" runat="server" CssClass="SmallFont" Font-Bold="False"
                                        OnRowCommand="grdIndentDetail_RowCommand" ShowFooter="True" BorderWidth="1px"
                                        AutoGenerateColumns="False" AllowSorting="True" Width="98%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="3%" />
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Code">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtItemCode" Font-Bold="true" runat="server" Text='<%# Bind("ITEM_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Description">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtItemDesc" runat="server" Text='<%# Bind("ITEM_DESC") %>' CssClass="Label SmallFont"></asp:Label>
                                                    <asp:TextBox ID="txtIndentDetailNumber" runat="server" Text='<%# Bind("IndentDetailNumber") %>'
                                                        Width="50px" Visible="False" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Stock">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="txtCurrentStock" runat="server" Text='<%# Bind("currentStock") %>'
                                                        CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Min.Level">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtMinStockLevel" runat="server" Text='<%# Bind("MIN_STOCK_LVL") %>'
                                                        CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening Rate">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtOpeningRate" runat="server" Text='<%# Bind("OP_RATE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="txtRequestQty" runat="server" Font-Bold-Bold="true" Text='<%# Bind("RQST_QTY") %>'
                                                        CssClass="LabelNo SmallFont"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM">
                                                <FooterTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="3%"></ItemStyle>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="txtUnitName" runat="server" Text='<%# Bind("VC_UNITNAME") %>' CssClass="Label SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="txtFooterAmount" runat="server" Text='<%# Bind("Amount") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <FooterStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtAmount" runat="server" Font-Bold="true" Text='<%# Bind("Amount") %>'
                                                        CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRemark" runat="server" Text='<%# Bind("DPT_REMARK") %>' CssClass="TextBox SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="120px">
                                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Button ID="lnkEdit" Text="Edit" runat="server" CommandName="indentEdit"
                                                        TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'>
                                                    </asp:Button>
                                                    <asp:Button ID="lnkDelete" runat="server" Text="Delete" CommandName="indentDelete"
                                                        TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'>
                                                    </asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                    <asp:Label ID="Label2" runat="server" Text="Amounts In Words :"></asp:Label>
                                    <asp:Label ID="lblAmountInWords" runat="server"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtIndentNumber" EventName="TextChanged"></asp:AsyncPostBackTrigger>
                                    <asp:AsyncPostBackTrigger ControlID="grdIndentDetail" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtIndentNumber" EventName="TextChanged"></asp:AsyncPostBackTrigger>
            <asp:AsyncPostBackTrigger ControlID="imgbtnUpdate" EventName="Click"></asp:AsyncPostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
