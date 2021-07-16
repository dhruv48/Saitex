<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_Indent.ascx.cs" Inherits="Module_Yarn_SalesWork_Controls_Yarn_Indent" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<script type="text/javascript" language="javascript">

    function GetClick(ButtonId) {
        document.getElementById(ButtonId).click();
    }
    function Calculation(val) { 
        var name = val;
        document.getElementById('ctl00_cphBody_Yarn_Indent1_txtAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_Yarn_Indent1_txtOpeningRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_Yarn_Indent1_txtRequestQty').value))).toFixed(3);
    }
    function GetRemarkFocus() {
        document.getElementById('ctl00_cphBody_Yarn_Indent1_txtRemark').focus();

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
        width: 200px;
    }
    .c2
    {
        margin-left: 4px;
        width: 300px;
    }
    .c3
    {
        width: 200px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 120px;
    }
    .d3
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 120px;
    }
</style>
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
        width: 150px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        width: 100px;
    }
    .c5
    {
        margin-left: 4px;
        width: 250px;
    }
    .c6
    {
        margin-left: 4px;
        width: 120px;
    }
    .c7
    {
        margin-left: 4px;
        width: 70px;
    }
    
    .style1
    {
        width: 19%;
    }
    .style2
    {
        width: 4%;
    }
    .style3
    {
        width: 6%;
    }
    .style4
    {
        width: 5%;
    }
    .style6
    {
        width: 12%;
    }
    .style7
    {
        width: 26%;
    }
    .style8
    {
        width: 9%;
    }
    </style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
        <table class="tContentArial" align="center" width="95%">
            <tr>
                <td valign="top" class="td" align="left" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdSave" valign="top" align="center" runat="server">
                                <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                    ToolTip="Save" Height="41" Width="48" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td id="tdUpdate" valign="top" align="center" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                    ToolTip="Update" Height="41" Width="48" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" valign="top" align="center" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click1" runat="server" ImageUrl="~/CommonImages/del6.png"
                                    ToolTip="Delete" Height="41" Width="48" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                    ToolTip="Find" Height="41" Width="48"></asp:ImageButton>
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
                    <%--<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtRequiredBefore"
                            Display="None" ErrorMessage="Invalid Date" Type="Date" ValidationGroup="M1"></asp:RangeValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="TableHeader" class="td" align="center" width="100%">
                    <b class="titleheading">Yarn Indenting Form (General)</b>
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
                            <td  valign="top" align="right" width="15%">
                                <asp:Label ID="Label111" runat="server" Text="Indent Number :" CssClass="Label "
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td  width="15%">
                                <asp:TextBox ID="txtIndentNumber" TabIndex="1" runat="server" Width="150px" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Font-Bold="True" AutoPostBack="True" OnTextChanged="txtIndentNumber_TextChanged"></asp:TextBox>
                                <cc2:ComboBox ID="ddlIndentNumber" runat="server" TabIndex="1" Width="150px" MenuWidth="800"
                                    AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                                    Height="200px" OnLoadingItems="ddlIndentNumber_LoadingItems" OnSelectedIndexChanged="ddlIndentNumber_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Indent #</div>
                                        <div class="header c2">
                                            Indent Date</div>
                                        <div class="header c3">
                                            DEPT NAME
                                        </div>
                                        <div class="header c3">
                                            PREPAID BY
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("IND_NUMB") %></div>
                                        <div class="item c2">
                                            <%# Eval("IND_DATE") %></div>
                                        <div class="item c3">
                                            <%# Eval("DEPT_NAME")%></div>
                                        <div class="item c4">
                                            <%# Eval("USER_NAME")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td  valign="top" align="right" width="15%">
                                <asp:Label ID="Label112" Font-Bold="True" runat="server" Text="Indent Date :" CssClass="Label "></asp:Label>
                            </td>
                            <td  width="15%">
                                <asp:TextBox ID="txtIndentDate" TabIndex="2" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td  valign="top" align="right" width="15%">
                                <asp:Label ID="Label113" Font-Bold="True" runat="server" Text="Department :" CssClass="Label "></asp:Label>
                            </td>
                            <td  width="25%">
                                <asp:TextBox ID="txtDepartment" TabIndex="3" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Font-Bold="False" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label3" Text="Indent Branch:" class="Label " runat="server"></asp:Label>
                            </td>
                            <td class="tdLeft " width="15%">
                                <asp:TextBox ID="txtIndentBranch" TabIndex="4" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label4" Text="Indent Type:" class="Label " runat="server"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="TextBox1" TabIndex="5" Text="GEN" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label115" runat="server" Text="Required Before :" CssClass="Label "></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtRequiredBefore" TabIndex="6" runat="server" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%" >
                                <asp:Label ID="Label114" runat="server" Text="Prepared By :" CssClass="Label "></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%" >
                                <asp:TextBox ID="txtPreparedBy" TabIndex="7" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td valign="top" align="right" width="15%" >
                                <asp:Label ID="Label6" runat="server" Text="Location :" CssClass="Label "></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%" >
                               <asp:DropDownList ID="ddlLocation" runat="server" TabIndex="8" CssClass="SmallFont" Font-Size="9"
                Width="150px">
            </asp:DropDownList>
                            </td>
                             <td valign="top" align="right" width="15%" >
                                <asp:Label ID="Label5" runat="server" Text="Store Location :" CssClass="Label "></asp:Label>
                            </td>
                            <td valign="top" align="left"  width="15%" >
                                <asp:DropDownList ID="ddlStore" runat="server" TabIndex="9" CssClass="SmallFont" Font-Size="9"
                Width="150px">
            </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label116" runat="server" Text="Comment :" CssClass="Label "></asp:Label>
                            </td>
                            <td valign="top" align="left" colspan="5" width="85%">
                                <asp:TextBox ID="txtCommentComment" TabIndex="10" MaxLength="199" runat="server" Width="85%"
                                    CssClass="TextBox SmallFont" TextMode="SingleLine" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" valign="top" width="100%">
                    <table width="98%">
                        <tr bgcolor="#006699">
                            <td >
                                <span class="titleheading"><b>Yarn Code </b></span>
                            </td>
                            <td >
                                <span class="titleheading"><b>ShadeCode </b></span>
                            </td>
                             <td >
                        <span class="titleheading"><b> Lot No </b></span>
                    </td>
                    <td >
                        <span class="titleheading"><b>Grade </b></span>
                    </td>
                            <td >
                                <span class="titleheading"><b>UOM </b></span>
                            </td>
                            <td >
                                <span class="titleheading"><b>Current Stock </b></span>
                            </td>
                            <%-- <td class="tdRight SmallFont">
                                <span class="titleheading"><b>Min Stock </b></span>
                            </td>--%>
                            <td >
                                <span class="titleheading"><b>Last Po Rate </b></span>
                            </td>
                            <td >
                                <span class="titleheading"><b>Indent Qty</b></span>
                            </td>
                            <td >
                                <span class="titleheading"><b>Amount </b></span>
                            </td>
                            <td >
                                <span class="titleheading"><b>Yarn Spec./Remarkes</b></span>
                            </td>
                            <td align="left" valign="top" width="10%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" >
                                <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="YARN_CODE" DataValueField="TRN" EnableLoadOnDemand="true"
                                    MenuWidth="650" OnLoadingItems="Item_LOV_LoadingItems" OnSelectedIndexChanged="Item_LOV_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="9" Visible="true"
                                    Height="200px" Width="45%">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            YARN CODE</div>
                                           <div class="header c5">
                                           CURRENT YARN DESCRIPTION</div>
                                        <div class="header c7">
                                            HSN CODE</div>
                                        <div class="header c6">
                                            Dyed/Non Dyed</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("ASS_YARN_CODE")%></div>
                                             <div class="item c5">
                                            <%# Eval("ASS_YARN_DESC")%></div>
                                            <div class="item c7">
                                            <%# Eval("HSN_CODE") %></div>
                                        <div class="item c6">
                                            <%# Eval("YARN_TYPE")%></div>
                                            
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:TextBox ID="txtItemCodeLabel" runat="server" TabIndex="10"  Width="45%" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Font-Bold="False" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" >
                           <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="SHADE_NAME" EnableLoadOnDemand="True"
                                                MenuWidth="350" EnableVirtualScrolling="true" 
                                    OpenOnFocus="true" TabIndex="16"
                                                Height="200px" Visible="true" Width="120px" 
                                    OnLoadingItems="cmbShade_LoadingItems" onselectedindexchanged="cmbShade_SelectedIndexChanged"
                                                >
                                                <HeaderTemplate>                                                  
                                                    <div class="header d2">
                                                        Shade Family Name</div>                                                  
                                                    <div class="header d4">
                                                        Shade Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                   
                                                    <div class="item d2">
                                                        <%# Eval("SHADE_FAMILY_NAME")%></div>                                                    
                                                    <div class="item d4">
                                                        <%# Eval("SHADE_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>
                                            
                            </td>
                            
                      <td align="left" valign="top" >
                      <cc2:ComboBox ID="txtLotNo" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtLotNo_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Lot No" 
                            EnableVirtualScrolling="true" Width="80px" MenuWidth="300px" Height="200px" >
                            <HeaderTemplate>
                                <div class="header d2">
                                    Lot No</div>
                                <div class="header c2">
                                    Desc</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item d2">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' /></div>
                                <div class="item c2">
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
                     <td align="left" valign="top" >
                        <cc2:ComboBox ID="txtGrade" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtGrade_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Grade" 
                            EnableVirtualScrolling="true" Width="60px" MenuWidth="100px" Height="200px"  TabIndex="29">
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
                            <td align="left" valign="top" >
                                <asp:TextBox ID="txtUnitName" TabIndex="11" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                                    Width="99%" Text="KGS"></asp:TextBox>
                                <td align="right" valign="top" >
                                    <asp:TextBox ID="txtCurrentStock" TabIndex="12" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                        Width="99%"></asp:TextBox>
                                </td>
                                <%--  <td align="right" valign="top">
                                <asp:TextBox ID="txtMinStockLevel" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                    Width="60px"></asp:TextBox>
                            </td>--%>
                                <td align="right" valign="top" >
                                    <asp:TextBox ID="txtOpeningRate" TabIndex="13" runat="server" ReadOnly="true" onChange="javascript:Calculation(this.id)"
                                        CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="99%"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" >
                                    <asp:TextBox ID="txtRequestQty" runat="server" TabIndex="14" onkeyup="javascript:Calculation(this.id)"
                                        CssClass="TextBoxNo SmallFont" Width="99%" MaxLength="6"></asp:TextBox>
                                    <asp:RangeValidator ID="r1" runat="server" ControlToValidate="txtRequestQty" ErrorMessage=" Please Enter numeric only"
                                        MaximumValue="999999.99" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                        Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                                        
                                </td>
                            </td>
                            <td align="right" valign="top" >
                                <asp:TextBox ID="txtAmount" TabIndex="15" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                    Width="99%"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" >
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox SmallFont" Width="99%"
                                    MaxLength="75" TabIndex="16"></asp:TextBox>
                            </td>
                            <td align="left" valign="top"  width="10%">
                                <asp:Button ID="lbtnsavedetail" Text="Add" runat="server" TabIndex="17" OnClick="lbtnsavedetail_Click"   CssClass="SmallFont" Width="45%">
                                </asp:Button>
                                <asp:Button ID="lbtnCancel" Text="Cancel" runat="server" TabIndex="18" OnClick="lbtnCancel_Click1"  CssClass="SmallFont" Width="45%">
                                </asp:Button>
                                
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="9">
                                <asp:TextBox ID="txtItemDesc" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                                    Width="250"></asp:TextBox> HSN Code<asp:TextBox ID="txtHSNCODE" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="120px"></asp:TextBox>
                                ShadeName/Code:<asp:TextBox ID="txtShadeFamilyName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="100px" ></asp:TextBox>
                                            <asp:TextBox ID="txtShadeName" runat="server" CssClass="TextBoxDisplay SmallFont"
                                                ReadOnly="true" Width="120px"></asp:TextBox>
                                                
                                                <asp:Label ID="lblMin_Procure_days" runat="server" Visible="false"></asp:Label>
                                                
                                                <cc1:FilteredTextBoxExtender ID="FiltertxtRequestQty" runat="server"  TargetControlID="txtRequestQty"   FilterType="Custom, Numbers" ValidChars="."/>
                                              
                                              
                                             
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trGridView" runat="server">
                <td class="td" align="left" width="100%">
                    <asp:Panel ID="pnlGrid" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">
                        <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                        <asp:GridView ID="grdIndentDetail" runat="server" CssClass="SmallFont" Font-Bold="False"
                            OnRowCommand="grdIndentDetail_RowCommand" ShowFooter="True" BorderWidth="0px"
                            AutoGenerateColumns="False" AllowSorting="True" Width="98%">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="3%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn Code">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtItemCode" Font-Bold="true" runat="server" Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn Description">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtItemDesc" runat="server" Text='<%# Bind("YARN_DESC") %>' CssClass="Label SmallFont"></asp:Label>
                                        <asp:TextBox ID="txtIndentDetailNumber" runat="server" Text='<%# Bind("IndentDetailNumber") %>'
                                            Width="50px" Visible="False" Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Lot No">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="7%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtLotNo1" runat="server" Text='<%# Bind("LOT_NO") %>' CssClass="Label SmallFont"></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="GRADE">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtGrade1" runat="server" Text='<%# Bind("GRADE") %>' CssClass="Label SmallFont"></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="HSN Code">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtHSNCODE" runat="server" Text='<%# Bind("HSN_CODE") %>' CssClass="Label SmallFont"></asp:Label>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                  <asp:TemplateField HeaderText="Shade Family">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="7%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtShadeFamily" runat="server" Text='<%# Bind("SHADE_FAMILY") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shade">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="7%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtShadeCode" runat="server" Text='<%# Bind("SHADE_CODE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="3%"></ItemStyle>
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="txtUnitName" runat="server" Text='<%# Bind("VC_UNITNAME") %>' CssClass="Label SmallFont"></asp:Label>
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
                                <%-- <asp:TemplateField HeaderText="Min.Level">
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="txtMinStockLevel" runat="server" Text='<%# Bind("MIN_STOCK_LVL") %>'
                                                    CssClass="LabelNo SmallFont"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Last Po Rate">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtOpeningRate" runat="server" Text='<%# Bind("OP_RATE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Indent Qty">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtRequestQty" runat="server" Font-Bold="true" Text='<%# Bind("RQST_QTY") %>'
                                            CssClass="LabelNo SmallFont" AutoPostBack="True" OnTextChanged="txtRequestQty_TextChanged"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Total"></asp:Label>
                                    </FooterTemplate>
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
                                <asp:TemplateField HeaderText="Yarn Spec./Remarkes">
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="txtRemark" runat="server" Text='<%# Bind("DPT_REMARK") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="120px">
                                    <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Button ID="lnkEdit" Text="Edit" runat="server" CommandName="indentEdit" TabIndex="12"
                                            CommandArgument='<%# Eval("UniqueId") %>' CssClass="SmallFont" Width="50px"></asp:Button>
                                            <asp:Button ID="lnkDelete"
                                                runat="server" Text="Delete" CommandName="indentDelete" TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>' CssClass="SmallFont" Width="50px">
                                            </asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="SmallFont" />
                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                        </asp:GridView>
                        <asp:Label ID="Label2" runat="server" Text="Amounts In Words :"></asp:Label>
                        <asp:Label ID="lblAmountInWords" runat="server"></asp:Label>
                        <%--   </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtIndentNumber" EventName="TextChanged"></asp:AsyncPostBackTrigger>
                                <asp:AsyncPostBackTrigger ControlID="grdIndentDetail" EventName="RowCommand"></asp:AsyncPostBackTrigger>
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </asp:Panel>
                    <asp:Button ID="Button1" runat="server" Text="" Width="1pt" OnClick="Button1_Click" />
                </td>
            </tr>
        </table>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>