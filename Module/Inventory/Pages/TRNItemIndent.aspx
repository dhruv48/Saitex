<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="TRNItemIndent.aspx.cs" Inherits="Module_Inventory_Pages_TRNItemIndent"
    Title="Item Indent" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Src="../Controls/Item_LOV.ascx" TagName="Item_LOV" TagPrefix="uc1" %>
<asp:Content ID="heead1" runat="server" ContentPlaceHolderID="Head1">

<script type="text/javascript" language="javascript">

    function GetClick(ButtonId) {
        document.getElementById(ButtonId).click();
    }
    function Calculation(val) {
        var name = val;
        document.getElementById('ctl00_cphBody_txtAmount').value = (Math.round(parseFloat(document.getElementById('ctl00_cphBody_txtOpeningRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_txtRequestQty').value)))).toFixed(2);
    }
    function GetRemarkFocus() {
        document.getElementById('ctl00_cphBody_txtRemark').focus();

    }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
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
            width: 200px;
        }
        .c3
        {
            margin-left: 4px;
            width: 100px;
        }
        .c4
        { 
            width: 150px;
        }
        .style1
        {
            color: #000000;
            font-weight: bold;
            background-color: #336799;
            font-family: Verdana,arial;
            text-decoration: none;
            font-size: 13px;
            text-align: center;
            height: 21px;
        }
        .style2
        {
            width: 60px;
        }
    </style>
   <%-- <asp:ScriptManager ID="src" runat="server"></asp:ScriptManager>--%>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table class="tContentArial" align="center" width="95%">
                <tr>
                    <td>
                        <table width="100%" >                       
                            <tr>
                                <td>
                                    <table width="100%" >
                                        <tr>
                                            <td valign="top" class="td" align="left" width="100%">
                                                <table align="left">
                                                    <tr>
                                                        <td id="tdSave" valign="top" align="center" runat="server">
                                                            <asp:ImageButton ID="imgbtnSave" TabIndex="12" OnClick="imgbtnSave_Click" runat="server"
                                                                ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" ValidationGroup="M1">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td id="tdUpdate" valign="top" align="center" runat="server">
                                                            <asp:ImageButton ID="imgbtnUpdate" TabIndex="12" OnClick="imgbtnUpdate_Click" runat="server"
                                                                ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48" ValidationGroup="M1">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td id="tdDelete" valign="top" align="center" runat="server">
                                                            <asp:ImageButton ID="imgbtnDelete" TabIndex="13" OnClick="imgbtnDelete_Click1" runat="server"
                                                                ImageUrl="~/CommonImages/del6.png" ToolTip="Delete" Height="41" Width="48" ValidationGroup="M1">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td valign="top" align="center">
                                                            <asp:ImageButton ID="imgbtnFind" TabIndex="13" OnClick="imgbtnFind_Click" runat="server"
                                                                ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" Height="41" Width="48">
                                                            </asp:ImageButton>
                                                        </td>
                                                        <td valign="top" align="center">
                                                            <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                                ToolTip="Print" Height="41" Width="48" TabIndex="14"></asp:ImageButton>
                                                        </td>
                                                        <td valign="top" align="center">
                                                            <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                                ToolTip="Clear" Height="41" Width="48" TabIndex="15"></asp:ImageButton>
                                                        </td>
                                                        <td valign="top" align="center">
                                                            <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                                ToolTip="Exit" Height="41" Width="48" TabIndex="16"></asp:ImageButton>
                                                        </td>
                                                        <td valign="top" align="center">
                                                            <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                                ToolTip="Help" Height="41" Width="48" TabIndex="17"></asp:ImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <cc1:CalendarExtender ID="c2" runat="server" TargetControlID="txtRequiredBefore"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIndentDate"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </td>                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1" class="td" align="center" width="100%">
                                    <b class="titleheading">Material Indent Entry</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" valign="top" align="left" width="100%">
                                    <span style="color: #ff0000">
                                        <asp:Label ID="lblMode" runat="server">
                                        </asp:Label></span>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="M1" />
                                </td>
                            </tr> 
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%" >
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
                                                        <div class="header c3">
                                                            Indent Date</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <%# Eval("IND_NUMB") %></div>
                                                        <div class="item c3">
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
                                                <asp:TextBox ID="txtIndentDate"  runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                                    ></asp:TextBox>
                                            </td>
                                            <td class="tdRight" width="15%">
                                                <asp:Label ID="Label113" runat="server" Text="Department :" CssClass="Label " Font-Bold="False"></asp:Label>
                                            </td>
                                            <td class="tdLeft" width="25%">
                                                <asp:TextBox ID="txtDepartment"  runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                                    Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="right" width="15%">
                                                <asp:Label ID="Label114" runat="server" Text="Prepared By :" CssClass="Label "></asp:Label>
                                            </td>
                                            <td valign="top" align="left" width="15%">
                                                <asp:TextBox ID="txtPreparedBy" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                                    ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td valign="top" align="right" width="15%">
                                            <asp:Label ID="Label3" runat="server" Text="Location:" CssClass="Label "></asp:Label>
                                            </td>
                                            <td valign="top" align="left" width="15%">
                                            
                                            <asp:DropDownList ID="ddlLocation" TabIndex="2" runat="server" CssClass="SmallFont" Width="150px"></asp:DropDownList>
                                        
                                        </td>
                                            <td valign="top" align="right" width="15%">
                                            <asp:Label ID="Label4" runat="server" Text="Store:" CssClass="Label "></asp:Label>
                                            </td>
                                            <td valign="top" align="left" width="25%">
                                            <asp:DropDownList ID="ddlStore" TabIndex="3" runat="server" CssClass="SmallFont" Width="150px">
                                    </asp:DropDownList></td>
                                        </tr>
                                         <tr>
                                            <td valign="top" align="right" width="15%">
                                                <asp:Label ID="Label5" runat="server" Text="Comment :" CssClass="Label "></asp:Label>
                                            </td>
                                            <td valign="top" align="left"  colspan="3">
                                                <asp:TextBox ID="txtCommentComment" TabIndex="4" runat="server" Width="95%" MaxLength="200"
                                                    CssClass="TextBox SmallFont"></asp:TextBox>
                                            </td>   
                                            
                                            <td valign="top" align="right" width="15%">
                                            <asp:Label ID="Label7" runat="server" Text="For:" CssClass="Label "></asp:Label>
                                            </td>
                                            <td valign="top" align="left" width="25%">
                                            <asp:DropDownList ID="ddlIndentFor" TabIndex="5" runat="server" CssClass="SmallFont" Width="150px">
                                            <asp:ListItem  Selected="True" Value="S">Store</asp:ListItem>
                                            <asp:ListItem Value="B">Break Down</asp:ListItem>
                                            <asp:ListItem Value="P">PCW Plant</asp:ListItem>
                                            <asp:ListItem Value="Pl">Ply</asp:ListItem>
                                            <asp:ListItem Value="D">Dyeing</asp:ListItem>
                                             <asp:ListItem Value="C">Dyes Chemical</asp:ListItem>
                                            <asp:ListItem Value="T">TFO</asp:ListItem>
                                            <asp:ListItem Value="S">Soft</asp:ListItem>
                                    </asp:DropDownList></td>
                                        </tr>
                                     </table>       
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
                                    <span class="titleheading"><b>Item Code </b></span>
                                </td>
                                <td class="style2">
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
                                <td class="tdRight SmallFont" style="width:70px" >
                                    <span class="titleheading"><b>Req. Before</b></span>
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
                                    <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" EmptyText="Find Item" EnableLoadOnDemand="true"
                                        EnableVirtualScrolling="true" Height="200px" MenuWidth="620px" OnLoadingItems="txtItemCode_LoadingItems"
                                        OnSelectedIndexChanged="txtItemCode_SelectedIndexChanged" TabIndex="6" Width="150px">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Code</div>
                                            <div class="header c2">
                                                DESCRIPTION</div>
                                            <div class="header c1">
                                                HSN Code</div>
                                            <div class="header c4">
                                                TYPE</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ITEM_CODE") %>' />
                                            </div>
                                            <div class="item c2">
                                                <asp:Literal ID="Container5" runat="server" Text='<%# Eval("ITEM_DESC") %>' />
                                            </div>
                                            <div class="item c1">
                                                <asp:Literal ID="Container7" runat="server" Text='<%# Eval("HSN_CODE") %>' />
                                            </div>
                                            <div class="item c4">
                                                <asp:Literal ID="Container6" runat="server" Text='<%# Eval("ITEM_TYPE") %>' />
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
                                <td align="right" valign="top" class="style2">
                                    <asp:TextBox ID="txtCurrentStock" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                        Width="60px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top">
                                    <asp:TextBox ID="txtMinStockLevel" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                        Width="60px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top">
                                    <asp:TextBox ID="txtOpeningRate" runat="server" ReadOnly="true" onChange="javascript:Calculation(this.id)"
                                        CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="60px"></asp:TextBox>
                                </td>
                                <td align="right" valign="top">
                                    <asp:TextBox ID="txtRequestQty" runat="server" onkeyup="javascript:Calculation(this.id)"
                                        CssClass="TextBoxNo SmallFont" Width="60px" TabIndex="7"></asp:TextBox>
                                    <asp:RangeValidator ID="r1" runat="server" ControlToValidate="txtRequestQty" ErrorMessage="*Enter Quantity"
                                        MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                        Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                                        Width="50px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRequiredBefore" runat="server" CssClass="TextBox SmallFont"
                                     Width="60px" TabIndex="8"></asp:TextBox>
                              <asp:RangeValidator ID="r2" runat="server" ControlToValidate="txtRequiredBefore" 
                                ErrorMessage="*Select Date" Type="Date"
                                        MaximumValue="9999/12/28" MinimumValue="2001/01/01" 
                                        ValidationGroup="reqdate"
                                        Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>   
                                </td>                                
                                <td align="right" valign="top">
                                    <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                        Width="60px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox SmallFont" MaxLength="900"
                                        Width="60px" TabIndex="9"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" rowspan="2">
                                    <asp:Button ID="lbtnsavedetail" Text="Save" runat="server" TabIndex="10" OnClick="lbtnsavedetail_Click" CssClass="SmallFont" Width="60px">
                                    </asp:Button>
                                    <asp:Button ID="lbtnCancel" Text="Cancel" runat="server" TabIndex="11" OnClick="lbtnCancel_Click1" CssClass="SmallFont" Width="60px">
                                    </asp:Button>
                                    <asp:Label ID="lblMin_Procure_days" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="9" valign="top">
                                <asp:TextBox ID="txtItemCodeLabel" runat="server" Width="80px" CssClass="TextBox TextBoxDisplay SmallFont"
                                        Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                        &nbsp;
                                    <asp:TextBox ID="txtItemDesc" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                        ReadOnly="true" Width="60%"></asp:TextBox>
                                        &nbsp;
                                        HSN CODE:<asp:TextBox ID="txtHSNCODE" runat="server" Width="80px" CssClass="TextBox TextBoxDisplay SmallFont"
                                        Font-Bold="False" ReadOnly="True"></asp:TextBox>
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
                                                    <asp:Label ID="txtItemCodeLable" Font-Bold="true" runat="server" Text='<%# Bind("ITEM_CODE") %>'></asp:Label>
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
                                            
                                            
                                              <asp:TemplateField HeaderText="HSN Code">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                   <asp:Label ID="txtHSNCODE" runat="server" Text='<%# Bind("HSN_CODE") %>' CssClass="Label SmallFont"></asp:Label>
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
                                                    <asp:Label ID="txtRequestQty" runat="server" Font-Bold="true" Text='<%# Bind("RQST_QTY") %>'
                                                        CssClass="LabelNo SmallFont" AutoPostBack="True" OnTextChanged="txtRequestQty_TextChanged"></asp:Label>
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
                                            
                                            <asp:TemplateField HeaderText="Required Before">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <FooterStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%"></ItemStyle>                                            
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRequiredBeforeLable" runat="server" Font-Bold="false" Text='<%# Bind("REQD_DATE","{0:dd/MM/yyyy}") %>'
                                                        CssClass="LabelNo SmallFont" ></asp:Label>
                                                </ItemTemplate>                                                
                                             </asp:TemplateField>
                                                                                         
                                            <asp:TemplateField HeaderText="Remarks">
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRemarkLable" runat="server" Text='<%# Bind("DPT_REMARK") %>' CssClass="SmallFont"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="120px">
                                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Button ID="lnkEdit" Text="Edit" runat="server" CommandName="indentEdit" TabIndex="12"
                                                        CommandArgument='<%# Eval("UniqueId") %>' CssClass="SmallFont" Width="55px"></asp:Button><asp:Button ID="lnkDelete"
                                                            runat="server" Text="Delete" CommandName="indentDelete" TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>' CssClass="SmallFont" Width="55px">
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
                        <asp:Button ID="Button1" runat="server" Text="" Width="1pt" OnClick="Button1_Click" />
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
