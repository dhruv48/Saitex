<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Recipe_Entry.ascx.cs"
    Inherits="Module_OrderDevelopment_Controls_Recipe_Entry" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
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
        width: 140px;
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
   
    </style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table align="left" class="tContentArial" width="95%">
    <tr>
        <td valign="top" align="left" class="td" width="100%">
            <table>
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click" ValidationGroup="M1" TabIndex="17" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click" ValidationGroup="M1" TabIndex="18"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Enabled="false" OnClick="imgbtnDelete_Click" TabIndex="19"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            OnClick="imgbtnFind_Click" TabIndex="20"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" TabIndex="21"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" TabIndex="22"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" TabIndex="23"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" TabIndex="24"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <span class="titleheading"><b>Recipe Entry Form</b></span>
        </td>
    </tr>
    <tr>
        <td class="td" align="left" valign="top" width="100%">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="M1" Display="Dynamic" />
            </span>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label1" runat="server" CssClass="Label" Text="Recipe Number :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="30%">
                        <asp:TextBox ID="txtRecipeNumber" runat="server" CssClass="TextBox TextBoxDisplay" TabIndex="1"
                            ReadOnly="True"></asp:TextBox>
                        <cc2:ComboBox ID="cmbRecipeNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            DataTextField="RECIPE_NO" DataValueField="RECIPE_NO" Width="150px" MenuWidth="150px"
                            Height="200px" CssClass="SmallFont" TabIndex="1" EmptyText="Find Recipe No" OnLoadingItems="cmbRecipeNumber_LoadingItems"
                            OnSelectedIndexChanged="cmbRecipeNumber_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    RECIPE NO</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("RECIPE_NO") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRecipeNumber"
                            ErrorMessage="Field can't be Empty" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight" width="26%">
                        <asp:Label ID="Label2" runat="server" CssClass="Label" Text="Recipe Date :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="24%">
                        <asp:TextBox ID="txtRecipeDate" runat="server" Width="73%" CssClass="TextBox" TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRecipeDate"
                            ErrorMessage="Enter RecipeDate" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
                        <cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtRecipeDate" PopupPosition="TopLeft">
                        </cc4:CalendarExtender>
                        <%--<asp:RangeValidator ID="RangeValidator1" runat="server" 
                                        ControlToValidate="txtRecipeDate" Display="None" 
                                        ErrorMessage="Date can't be Forward Date" ValidationGroup="M1" 
                            Type="Date"></asp:RangeValidator>--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="20%">
                        &nbsp;<asp:Label ID="Label4" runat="server" CssClass="Label" Text="Process Code :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="10%">
                        <cc1:OboutDropDownList ID="ddlProcessCode" Width="100%" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlProcessCode_SelectedIndexChanged" TabIndex="3">
                            <asp:ListItem Text="P">Select</asp:ListItem>
                            <asp:ListItem Text="P1">P1</asp:ListItem>
                            <asp:ListItem Text="P2">P2</asp:ListItem>
                            <asp:ListItem Text="P3">P3</asp:ListItem>
                        </cc1:OboutDropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProcessCode"
                            ErrorMessage="Enter ProcessCode" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdLeft" width="30%">
                        <asp:TextBox ID="txtProcessCode" runat="server" CssClass="TextBox TextBoxDisplay"
                            ReadOnly="true" Width="100%"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="16%">
                        <asp:Label ID="Label5" runat="server" CssClass="Label" Text="Fabric Expr/Pickup :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="24%">
                        <asp:TextBox ID="txtFabricExpr" runat="server" Width="73%" CssClass="TextBox" TabIndex="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFabricExpr"
                            ErrorMessage="Enter FabricExpr" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="20%">
                        <asp:Label ID="Label6" runat="server" CssClass="Label" Text="Lab dip # :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="20%">
                        <cc2:ComboBox ID="cmbLabdipNo" runat="server" MenuWidth="350px" Width="100%" AutoPostBack="True"
                            OnSelectedIndexChanged="cmbLabdipNo_SelectedIndexChanged" TabIndex="5">
                        </cc2:ComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="cmbLabdipNo"
                            ErrorMessage="Enter LabDip No" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdLeft" colspan="2" width="60%">
                        <asp:TextBox ID="txtLabdipNo" runat="server" Width="89%" CssClass="TextBox TextBoxDisplay"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label12" runat="server" CssClass="Label" Text="Primary Light Source :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="20%">
                        <%--<cc1:OboutDropDownList ID="ddlPLS" Width="100%" runat="server" TabIndex="6">
                        </cc1:OboutDropDownList>--%>
                         <cc2:ComboBox ID="ddlPLS" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="MST_CODE" DataValueField="MST_CODE" EmptyText="Select" 
                                        Height="200px" MenuWidth="200px" 
                                         TabIndex="6" Width="150px" EnableLoadOnDemand="True" 
                            onloadingitems="ddlPLS_LoadingItems" >
                            
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Primary Light Source</div>
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal ID="Container4" runat="server" Text='<%# Eval("MST_CODE") %>' />
                                            </div>
                                            
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items - out of .
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlPLS"
                            ErrorMessage="Select PLS" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight" width="36%">
                        <asp:Label ID="Label13" runat="server" CssClass="Label" Text="Secondary Light Source :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="29%">
                      <%--  <cc1:OboutDropDownList ID="ddlSLS" Width="75%" runat="server" TabIndex="7">
                        </cc1:OboutDropDownList>--%>
                         <cc2:ComboBox ID="ddlSLS" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="MST_CODE" DataValueField="MST_CODE" EmptyText="Select" 
                                        Height="200px" MenuWidth="200px" 
                                         TabIndex="7" Width="150px" EnableLoadOnDemand="True" 
                            onloadingitems="ddlSLS_LoadingItems" >
                            
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Secondary Light Source</div>
                                            
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal ID="Container4" runat="server" Text='<%# Eval("MST_CODE") %>' />
                                            </div>
                                            
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items - out of .
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlSLS"
                            ErrorMessage="Select SLS" ValidationGroup="M1" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label14" runat="server" Text="Comment :"></asp:Label>
                    </td>
                    <td class="tdLeft" colspan="3" width="85%">
                        <asp:TextBox ID="txtComment" runat="server" Width="92%" CssClass="TextBox" TabIndex="7"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdLeft titleheading" width="100%">
                        <asp:Label ID="Label3" Font-Size="Medium" CssClass="SectionHeading" runat="server"
                            Text="Recipe Colour Details :" ForeColor="#CC0000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft">
                        <table width="94%">
                            <tr class="TableHeader">
                                <td>
                                    <asp:Label ID="Label15" runat="server" CssClass="Label titleheading" Text="Item Code"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label16" runat="server" CssClass="Label titleheading" Text="Description"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label17" runat="server" CssClass="Label titleheading" Text="Basis"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label18" runat="server" CssClass="Label titleheading" Text="Qty"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label19" runat="server" CssClass="Label titleheading" Text="Curr. Rate"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label20" runat="server" CssClass="Label titleheading" Text="Qty/KG"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label21" runat="server" CssClass="Label titleheading" Text="Value/ KG"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label22" runat="server" CssClass="Label titleheading" Text="Remarks"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLeft">
                                   
                                    <cc2:ComboBox ID="cmbItemCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" EmptyText="Select Item" EnableLoadOnDemand="true"
                                        Height="200px" MenuWidth="350px" OnLoadingItems="cmbItemCode_LoadingItems" OnSelectedIndexChanged="cmbItemCode_SelectedIndexChanged"
                                        OnTextChanged="cmbItemCode_TextChanged" TabIndex="1" Width="100px">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Code</div>
                                            <div class="header c2">
                                                DESCRIPTION</div>
                                          <%--  <div class="header c3">
                                                TYPE</div>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ITEM_CODE") %>' />
                                            </div>
                                            <div class="item c2">
                                                <asp:Literal ID="Container5" runat="server" Text='<%# Eval("ITEM_DESC") %>' />
                                            </div>
                                           <%-- <div class="item c3">
                                                <asp:Literal ID="Container6" runat="server" Text='<%# Eval("ITEM_TYPE") %>' />
                                            </div>--%>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items - out of .
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                    <%-- <cc1:OboutDropDownList ID="ddlItemCode" runat="server" Width="80px" 
                                        AutoPostBack="True" onselectedindexchanged="ddlItemCode_SelectedIndexChanged">
                                    </cc1:OboutDropDownList>--%>
                                </td>
                                <td class="tdLeft">
                                    <asp:TextBox ID="txtDesc" runat="server" CssClass="TextBox TextBoxDisplay" ReadOnly="true"
                                        TabIndex="9"></asp:TextBox>
                                </td>
                                <td class="tdLeft">
                                   <%-- <cc1:OboutDropDownList ID="ddlBasis" runat="server" Width="80px" TabIndex="10">
                                    </cc1:OboutDropDownList> --%>
                                     <cc2:ComboBox ID="ddlBasis" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="MST_CODE" DataValueField="MST_CODE" EmptyText="Select" EnableLoadOnDemand="true"
                                        Height="200px" MenuWidth="250px" 
                                         TabIndex="1" Width="100px" onloadingitems="ddlBasis_LoadingItems">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                Basis</div>
                                           
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <asp:Literal ID="Container4" runat="server" Text='<%# Eval("MST_CODE") %>' />
                                            </div>
                                          
                                        </ItemTemplate>
                                       
                                    </cc2:ComboBox>
                                    
                                </td>
                                <td class="tdLeft">
                                    <asp:TextBox ID="txtQty" CssClass="txtQty" runat="server" Width="60px" TabIndex="11"></asp:TextBox>
                                </td>
                                <td class="tdLeft">
                                    <asp:TextBox ID="txtCurrRate" CssClass="TextBox TextBoxDisplay" runat="server" Width="50px"
                                        ReadOnly="true" TabIndex="1"></asp:TextBox>
                                </td>
                                <td class="tdLeft">
                                    <asp:TextBox ID="txtQtyKg" CssClass="TextBox" runat="server" Width="50px" TabIndex="12"></asp:TextBox>
                                </td>
                                <td class="tdLeft">
                                    <asp:TextBox ID="txtValueKg" CssClass="TextBox" runat="server" Width="50px" TabIndex="14"></asp:TextBox>
                                </td>
                                <td class="tdLeft">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox" TabIndex="15"></asp:TextBox>
                                </td>
                                <td class="tdLeft">
                                    <cc1:OboutButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
                                        ValidationGroup="M2" TabIndex="16">
                                    </cc1:OboutButton>
                                    <cc1:OboutButton ID="btnCancel" runat="server" Text="Cancel" TabIndex="17" 
                                        onclick="btnCancel_Click">
                                    </cc1:OboutButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft">
                        <asp:Panel ID="Panel1" runat="server" Height="200px" ScrollBars="Vertical">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                Width="95%" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="RECIPE NO" Visible="false">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="LblRECIPENO" runat="server" Text='<%# Bind("RECIPE_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ITEM CODE">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="LblITEMCODE" runat="server" Text='<%# Bind("ITEM_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DESCRIPTION">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="LblDESCRIPTION" runat="server" Text='<%# Bind("ITEM_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BASIS">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="LblBASIS" runat="server" Text='<%# Bind("BASIS") %>' CssClass="Label SmallFont"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="LblQTY" runat="server" Text='<%# Bind("QTY") %>' CssClass="Label SmallFont"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY/KG" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="LblQTYKG" runat="server" Text='<%# Bind("QTY_KG") %>' CssClass="Label SmallFont"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="VALUE/KG" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="LblVALUEKG" runat="server" Text='<%# Bind("VALUE_KG") %>' CssClass="Label SmallFont"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="REMARKS" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="LblREMARKS" runat="server" Text='<%# Bind("REMARKS") %>' CssClass="Label SmallFont"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="150px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <cc1:OboutButton ID="OboutButton3" runat="server" CommandName="EditItem" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                FolderStyle="" Text="Edit">
                                            </cc1:OboutButton>
                                            <cc1:OboutButton ID="OboutButton4" runat="server" CommandName="DelItem" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                FolderStyle="" Text="Delete">
                                            </cc1:OboutButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="SmallFont" />
                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" ForeColor="White" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>