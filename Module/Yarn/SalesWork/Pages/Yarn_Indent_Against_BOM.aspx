<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/CommonMaster/admin.master"
    AutoEventWireup="true" CodeFile="Yarn_Indent_Against_BOM.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Yarn_Indent_Against_BOM"
    Title="Untitled Page" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Src="../../../../CommonControls/LOV/YarnCodeLov.ascx" TagName="YarnCodeLov"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">

    <script type="text/javascript" language="javascript"> 

        function Calculation(val) {
            var name = val;
            //document.getElementById('ctl00_cphBody_txtAmount').value = Number(document.getElementById('ctl00_cphBody_txtRequestQty').value) * Number(val);
          // document.getElementById('txtAmount').value = (Math.round(parseFloat(document.getElementById('txtOpeningRate').value) * (parseFloat(document.getElementById('txtRequestQty').value)))).toFixed(2);
           document.getElementById('ctl00_cphBody_txtAmount').value = (Math.round(parseFloat(document.getElementById('ctl00_cphBody_txtOpeningRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_txtRequestQty').value)))).toFixed(2);
        }
        function GetRemarkFocus() {
            document.getElementById('ctl00_cphBody_txtRemark').focus();

        }
        function calculateAmount(textcontrol) {
            var form1 = textcontrol.value;
            var form2 = document.getElementById('txtRequestQty').value;
            document.getElementById('txtAmount').value = Number(form1) * Number(form2);
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
            width: 120px;
        }
        .c3
        {
            margin-left: 4px;
            width: 200px;
        }
    </style>
   <%-- <asp:UpdatePanel ID="updatePanel1" runat="server">
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
                <cc1:CalendarExtender ID="c2" runat="server" TargetControlID="txtRequiredBefore">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="TableHeader td" align="center" width="100%">
                <b class="titleheading">Yarn Indent Against BOM</b>
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
                                Font-Bold="True" AutoPostBack="True" OnTextChanged="txtIndentNumber_TextChanged"></asp:TextBox>
                            <asp:DropDownList ID="ddlIndentNumber" runat="server" CssClass="SmallFont" AppendDataBoundItems="True"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlIndentNumber_SelectedIndexChanged1">
                            </asp:DropDownList>
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
                            <asp:TextBox ID="txtCommentComment" TabIndex="6" runat="server" Width="95%" CssClass="TextBox SmallFont"
                                TextMode="MultiLine" MaxLength="199"></asp:TextBox>
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
                            <span class="titleheading"><b>Yarn</b></span>
                        </td>
                        <td class="tdRight SmallFont">
                            <span class="titleheading"><b>Rate </b></span>
                        </td>
                        <td class="tdRight SmallFont">
                            <span class="titleheading"><b>Adj BOM</b></span>
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
                            <cc2:ComboBox ID="ddlYarnCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                OnLoadingItems="ddlYarnCode_LoadingItems" DataTextField="BASE_ARTICAL_CODE" DataValueField="BASE_SHADE_CODE"
                                OnSelectedIndexChanged="ddlYarnCode_SelectedIndexChanged" EnableVirtualScrolling="true"
                                Width="100%" MenuWidth="800px" Height="200px">
                                <HeaderTemplate>
                                    <div class="header c2">
                                        Order Party</div>
                                    <div class="header c2">
                                        PA No</div>
                                    <div class="header c1">
                                        Code</div>
                                    <div class="header c3">
                                        Desc</div>
                                    <div class="header c1">
                                        Shade</div>
                                    <div class="header c1">
                                        Rem Qty</div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="item c2">
                                        <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                    <div class="item c2">
                                        <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PI_NO") %>' /></div>
                                   <%-- <div class="item c1">
                                        <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("BASE_ARTICAL_CODE") %>' /></div>
                                    <div class="item c3">
                                        <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("BASE_ARTICAL_DESC") %>' /></div>
                                    <div class="item c1">
                                        <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("BASE_SHADE_CODE") %>' /></div>--%>
                                         <div class="item c1">
                                        <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("ARTICAL_CODE") %>' /></div>
                                    <div class="item c3">
                                        <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("ARTICAL_DESC") %>' /></div>
                                    <div class="item c1">
                                        <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("SHADE_CODE") %>' /></div>
                                    <div class="item c1">
                                        <asp:Literal runat="server" ID="Literal5" Text='<%# Eval("REM_QTY") %>' /></div>
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
                            <asp:TextBox ID="txtOpeningRate" runat="server" ReadOnly="true" onChange="javascript:Calculation(this.id);"
                                CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="60px"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            <asp:Button ID="btnAdjBOM" Text="Adj." Width="40px" runat="server" OnClick="btnAdjBOM_Click">
                            </asp:Button>
                        </td>
                        <td align="right" valign="top">
                            <asp:TextBox ID="txtRequestQty" runat="server" onBlur="javascript:Calculation(this.id);"
                                 Width="70px" TabIndex="8" OnTextChanged="txtRequestQty_TextChanged1" 
                                ReadOnly="True" CssClass="TextBox SmallFont TextBoxDisplay"></asp:TextBox>
                            <asp:RangeValidator ID="r1" runat="server" ControlToValidate="txtRequestQty" ErrorMessage="*Enter Quantity"
                                MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                                Width="35px"></asp:TextBox>
                        </td>
                        <td align="right" valign="top">
                            <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                Width="80px"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox SmallFont" Width="60px"
                                TabIndex="9" MaxLength="75"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                            <asp:Button ID="lbtnsavedetail" Text="Save" runat="server" TabIndex="10" OnClick="lbtnsavedetail_Click">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="7" valign="top">
                            Yarn Code/ Desc<asp:TextBox ID="txtYarnCode" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                ReadOnly="true" Width="100px"></asp:TextBox>
                            /<asp:TextBox ID="txtYarnDesc" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                ReadOnly="true" Width="180px"></asp:TextBox>
                            &nbsp;Shade:<asp:TextBox ID="txtShadeCode" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                ReadOnly="true" Width="100px"></asp:TextBox>
                            Stock:<asp:TextBox ID="txtCurrentStock0" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                Width="60px"></asp:TextBox>
                            &nbsp;Min Stock :<asp:TextBox ID="txtMinStockLevel0" runat="server" ReadOnly="true"
                                CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="60px"></asp:TextBox>
                            <asp:Label ID="lblMin_Procure_days" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:Button ID="lbtnCancel" Text=" Cancel " runat="server" TabIndex="11" OnClick="lbtnCancel_Click1"
                                Width="50px"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="trGridView" runat="server">
            <td class="td" align="left" width="100%">
                <asp:Panel ID="pnlGrid" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
                        <%--<ContentTemplate>--%>
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
                                    <asp:TemplateField HeaderText="Shade Code">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="7%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblShadeCode" runat="server" Text='<%# Bind("SHADE_CODE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Stock">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrentStock" runat="server" Text='<%# Bind("currentStock") %>'
                                                CssClass="LabelNo SmallFont"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min.Level">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinStockLevel" runat="server" Text='<%# Bind("MIN_STOCK_LVL") %>'
                                                CssClass="LabelNo SmallFont"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Rate">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningRate" runat="server" Text='<%# Bind("OP_RATE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblRequestQty" runat="server" Font-Bold-Bold="true" Text='<%# Bind("RQST_QTY") %>'
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
                                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("VC_UNITNAME") %>' CssClass="Label SmallFont"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFooterAmount" runat="server" Text='<%# Bind("Amount") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <FooterStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="8%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Font-Bold="true" Text='<%# Bind("Amount") %>'
                                                CssClass="LabelNo SmallFont"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="20%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("DPT_REMARK") %>' CssClass="TextBox SmallFont"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="120px">
                                        <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" CommandName="indentEdit" TabIndex="12"
                                                CommandArgument='<%# Eval("UniqueId") %>' ></asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="indentDelete"
                                                TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
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
            </td>
        </tr>
    </table>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>


