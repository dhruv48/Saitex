<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiberIndentBom.ascx.cs" Inherits="Module_Fiber_Controls_FiberIndentBom" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
   
<link href="../../../StyleSheet/style.css" rel="stylesheet" type="text/css" />
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function Calculation(val) {
            var name = val;
            
            document.getElementById('ctl00_cphBody_txtAmount').value = (Math.round(parseFloat(document.getElementById('ctl00_cphBody_txtOpeningRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_txtRequestQty').value)))).toFixed(2);
        }
        function GetRemarkFocus() {
            document.getElementById('ctl00_cphBody_txtRemark').focus();

        }
    </script>
    <style type="text/css">
    .item
   {
       display:-moz-inline-stack;
       position:relative  !important;
       overflow:hidden; white-space:nowrap;
       
    }
    .header
    {
     margin-left:4px;   
    }
    .c1
    {
        width:150px;
    }
    .c2
    {
        margin-left:2px;
        width:60px;
    }
    .c3
    {
        margin-left:2px;
        width:120px;
    }
    .c4
    {
        margin-left:2px;
        width:70px;
    }
    
    
        .style1
        {
            width: 176px;
        }
        .style2
        {
            width: 90px;
        }
        .style3
        {
            width: 58px;
        }
        .style4
        {
            width: 176px;
            height: 33px;
        }
        .style5
        {
            width: 90px;
            height: 33px;
        }
        .style6
        {
            width: 58px;
            height: 33px;
        }
        .style7
        {
            height: 33px;
        }
    </style>
    <%--<asp:UpdatePanel runat="server" ID="update1">
    <ContentTemplate>--%>
        <table align="center" width="95%" class="tContentArial">
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
                    
    </tr>
    
    </table>
    
    </td>
    </tr>
    <tr>
    <td class="td TableHeader" align="center" width="100%">
    <b class="titleheading"> Poy Indent Against BOM
    </b>
    </td>
    </tr>
    <tr>
    <td class="td" valign="top" align="left" width="100%">
    <span style="color:#ff0000">You are in &nbsp;
    <asp:Label ID="lblMode" runat="server"></asp:Label> 
    &nbsp;Mode
    </span>
    </td>
    </tr>
    <tr>
    <td class="td" width="100%">
    <table width="100%">
    <tr>
    <td class="tdRight Label" width="15%" style="font-weight:bold">
    Indent Number:
    </td>
    <td class="tdLeft" width="15%">
    <asp:TextBox ID="txtIndentNumber" runat="server" Width="150px" CssClass="TextBox TextBoxDisplay SmallFont" Font-Bold="true" AutoPostBack="true" OnTextChanged="txtIndentNumber_TextChanged"></asp:TextBox>
    <asp:DropDownList ID="ddlIndentNumber" runat="server" CssClass="SmallFont" AutoPostBack="true" OnSelectedIndexChanged="ddlIndentNumber_SelectedIndexChanged1" Width="150px"></asp:DropDownList>
    </td>
    <td class="tdRight Label" width="15%" style="font-weight:bold; word-spacing:normal;">
    Indent Date:
    </td>
    <td class="tdLeft" width="15%">
    <asp:TextBox ID="txtIndentDate" runat="server" Width="150px" ReadOnly="true" 
            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
    </td>
    <td class="tdRight Label" width="15%" style="font-weight:bold; word-spacing:normal;">
    Department:
    </td>
    <td class="tdLeft" width="15%">
    <asp:TextBox ID="txtDepartment" runat="server" Width="150px"  
            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td class="tdRight Label" width="15%" style="font-weight:bold; word-spacing:normal;">
    Prepared By:
    </td>
    <td class="tdLeft" width="15%">
    <asp:TextBox ID="txtPreparedBy" runat="server" Width="150px" 
            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
    </td>
    <td class="tdRight Label" width="15%" style=" font-weight:bold; word-spacing:normal;">
    Required Before:
    </td>
    <td class="tdLeft" width="15%">
    <asp:TextBox ID="txtRequiredBefore" runat="server" Width="150px" 
            CssClass="TextBox TextBoxDisplay SmallFont" ></asp:TextBox>
    </td>
    <td class="tdRight Label" width="15%" style="font-weight:bold; word-spacing:normal;">
    Comments:
    </td>
    <td>
    <asp:TextBox ID="txtComments" runat="server" Width="150px" 
            CssClass="TextBox  SmallFont" TextMode="MultiLine"></asp:TextBox> 
    </td>
    </tr>
    </table>
    
    </td>
    </tr>
    <tr>
    <td align="left" class="td" valign="top" width="100%">
    <table >
    <tr style="background-color:#006699;">
    <td  width="10%">
    <span class="titleheading">
    <b>Poy</b>
    </span>
    </td>
    <td width="10%">
    <span class="titleheading">
    <b>Rate</b>
    </span>
    </td>
    <td width="10%">
    <span class="titleheading">
     <b>Adj. BOM</b>    
    </span>
    </td>
    <td width="10%">
    <span class="titleheading">
     <b>Req. Date</b>    
    </span>
    </td>
    <td class="tdCenter SmallFont" width="10%">
    <span class="titleheading">
    <b>Quantity</b>
    </span>
    </td>
    <td class="tdCenter SmallFont" width="10%">
    <span class="titleheading">
    <b>UOM</b>
    </span>
    </td>
    <td class="tdCenter SmallFont" width="10%">
    <span class="titleheading">
    <b>Amount</b>
    </span>
    </td>
    <td class="tdCenter SmallFont" width="10%">
    <span class="titleheading">
    <b>Remarks</b>
    </span>
    </td>
    <td align="left" valign="top" class="SmallFont">
    &nbsp;
    </td>
    </tr>
    <tr>
    <td align="left" valign="top" >
    <cc2:ComboBox ID="ddlFiberCode" runat="server" AutoPostBack="true" EnableLoadOnDemand="true" OnLoadingItems="ddlFiberCode_LoadingItems" DataTextField="BASE_ARTICAL_CODE" DataValueField="BASE_SHADE_CODE" OnSelectedIndexChanged="ddlFiberCode_SelectedIndexChanged" EnableVirtualScrolling="true" Width="150px" MenuWidth="300px" Height="200px">
    <HeaderTemplate>
    <div class="header c2">
    PARTY
    </div>
    <div class="header c3">
    PA NO
    </div>
    <div class="header c4">
    CODE
    </div>
    <%--<div class="header c3">
    DESC
    </div>
    <div class="header c1">
    SHADE
    </div>--%>
    <%--<div class="header c1">
    REM QTY
    </div>--%>
    </HeaderTemplate>
    <ItemTemplate>
    <div class="item c2">
    <asp:Literal runat="server" ID="lblOrderNO" Text='<%#Eval("PRTY_CODE") %>'></asp:Literal>
    </div>
    <div class="item c3">
    <asp:Literal runat="server" ID="lblPaNo" Text='<%#Eval("PI_NO") %>'></asp:Literal>
    </div>
    <div class="item c4">
    <asp:Literal runat="server" ID="lblCOde" Text='<%#Eval("BASE_ARTICAL_CODE") %>'></asp:Literal>
    </div>
    </ItemTemplate>
    <FooterTemplate>
    Displaying Items
    <%#Container.ItemsCount> 0 ? "1":"0" %>-<%# Container.ItemsLoadedCount %>
    out 
    </FooterTemplate>
    </cc2:ComboBox>
    </td>
    <td align="right" valign="top" >
    <asp:TextBox ID="txtOpeningRate" runat="server" ReadOnly="true" CssClass="SmallFont TextBox TextBoxDisplay" 
    Width="90px" onchange="javascript:Calculation(this.id);"></asp:TextBox>
    </td>
    <td align="left" valign="top" >
    <asp:Button ID="btnAdjBom" runat="server" Text="Adj." OnClick="btnAdjBom_CLick" Width="60px" CssClass="SmallFont" />
    </td>
    <td align="left" valign="top" width="10%">
                                    <asp:TextBox ID="txtReqDate" runat="server" TabIndex="27" Width="90%" CssClass="SmallFont TextBox UpperCase"></asp:TextBox>
       </td>
    <td align="right" valign="top" width="10%" >
    <asp:TextBox ID="txtRequestQty" runat="server" CssClass="TextBox  SmallFont" Width="90%" ReadOnly="true" OnTextChanged="txtRequestQty_TextChanged" onkeyup="javascript:Calculation(this.id);"></asp:TextBox>
    
    <asp:RangeValidator ID="r1" runat="server" ControlToValidate="txtRequestQty" ErrorMessage="*"
                                MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator></td>
    <td align="left" valign="top" width="10%" >
     <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                                Width="90%"></asp:TextBox>
    </td>
    <td align="right" valign="top" width="10%" >
    <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                                Width="90%"></asp:TextBox>
    </td>
    <td align="right" valign="top" width="10%" >
     <asp:TextBox ID="txtRemark" runat="server" CssClass="TextBox SmallFont" Width="90%"
                                TabIndex="9"></asp:TextBox>
    </td>
    <td align="center" valign="top" >
    <asp:Button ID="lbtnsavedetail" Text="Add" runat="server" TabIndex="10" 
            OnClick="lbtnsavedetail_Click" Width="60px" CssClass="SmallFont">
                            </asp:Button>
    </td>
    </tr>
    <tr>
    <td align="left" colspan="8" valign="top" class="SmallFont">
    Poy Code/Desc:
    <asp:TextBox ID="txtFiberCode" runat="server" ReadOnly="true" CssClass="TextBox TextBoxDisplay SmallFont" Width="100px"></asp:TextBox>
    /<asp:TextBox ID="txtFiberDesc" runat="server" 
            CssClass="SmallFont TextBox TextBoxDisplay" ReadOnly="true" Width="200px"></asp:TextBox>  
        &nbsp;Stock:<asp:TextBox ID="txtCurrent" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"  Width="60px"></asp:TextBox>
        &nbsp;Min&nbsp;Stock:<asp:TextBox ID="txtMinStock" runat="server" ReadOnly="true"
                                CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="60px"></asp:TextBox>
    <asp:Label ID="lblMin_Procure_days" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblPINO" runat="server" Text="" Visible="false"></asp:Label>
    </td>
      <td colspan="4" align="center" valign="top">
  <asp:Button ID="lbtnCancel" Text="Cancel" runat="server" TabIndex="16" OnClick="lbtnCancel_Click1"
                                Width="60px" CssClass="SmallFont"></asp:Button>
      </td>  
    </tr>
    </table>
    
    </td>
    </tr>
    <tr id="trGridView" runat="server">
    <td class="td" align="left" valign="top" width="100%">
    <asp:Panel ID="pnlGrid" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">
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
                                    <asp:TemplateField HeaderText="Poy Code">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="txtItemCode" Font-Bold="true" runat="server" Text='<%# Bind("FIBER_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Poy&nbsp;Description">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="txtItemDesc" runat="server" Text='<%# Bind("FIBER_DESC") %>' CssClass="Label SmallFont"></asp:Label>
                                            <asp:TextBox ID="txtIndentDetailNumber" runat="server" Text='<%# Bind("IndentDetailNumber") %>'
                                                Width="50px" Visible="False" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PI_NO" Visible ="false" runat ="server">
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right"/>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPINO" runat="server" Text='<%# Bind("PI_NO") %>'
                                                CssClass="LabelNo SmallFont"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current&nbsp;Stock">
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
                                    <asp:TemplateField HeaderText="Opening&nbsp;Rate">
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="5%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningRate" runat="server" Text='<%# Bind("OP_RATE") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req&nbsp;Date.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtReqDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REQ_DATE" , "{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
                     
                    </asp:Panel>
    </td>
    </tr>
    <cc3:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
    Mask="99/99/9999" MaskType="Date" PromptCharacter="_" 
    TargetControlID="txtReqDate">
</cc3:MaskedEditExtender>
<cc3:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
    TargetControlID="txtReqDate">
</cc3:CalendarExtender>
    </table>
   <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>