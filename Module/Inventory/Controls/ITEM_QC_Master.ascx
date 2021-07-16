<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ITEM_QC_Master.ascx.cs"
    Inherits="Module_Inventory_Controls_ITEM_QC_Master" %>
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
    .d1
    {
        width: 100px;
    }
    .d2
    {
        margin-left: 4px;
        width: 350px;
    }
    .d3
    {
        width: 80px;
    }
    .HeaderRow
    {
        font-size: 8pt;
        font-weight: bold;
    }
    .Smallfont
    {
        font-size: 8pt;
    }
</style>

<script language="javascript" type="text/javascript">

    function CalculateAmount() {
        var STD, Tolerance, Max_Vlaue, Min_Value;

        STD = parseFloat(document.getElementById('<%=txtSTD.ClientID%>').value);


        Tolerance = parseFloat(document.getElementById('<%=txtTolerance.ClientID%>').value);


        var toleranceType = document.getElementById('<%=ddltoleranceType.ClientID%>');
        var vToleranceType = toleranceType.options[toleranceType.selectedIndex].innerHTML;

        var tolerancerange = document.getElementById('<%=ddltolerancerange.ClientID%>');
        var vtolerancerange = tolerancerange.options[tolerancerange.selectedIndex].innerHTML;

        if (isNaN(STD)) {
            STD = 0;
        }

        if (isNaN(Tolerance)) {
            Tolerance = 0;
        }

        if (vToleranceType == '%') {
            if (vtolerancerange == 'MAXIMUM') {
                Max_Vlaue = STD + Tolerance;
                Min_Value = 0;
            }
            else if (vtolerancerange == 'MINIMUM') {
                Min_Value = STD - Tolerance;
                Max_Vlaue = STD - Tolerance;
            }
            else if (vtolerancerange == 'MIN/MAX') {
                Max_Vlaue = STD + Tolerance;
                Min_Value = STD - Tolerance;
            }
        }
        else if (vToleranceType == 'VALUE') {
            if (vtolerancerange == 'MAXIMUM') {
                Max_Vlaue = STD + Tolerance;
                Min_Value = 0;
            }
            else if (vtolerancerange == 'MINIMUM') {
                Min_Value = STD - Tolerance;
                Max_Vlaue = STD - Tolerance;
            }
            else if (vtolerancerange == 'MIN/MAX') {
                Max_Vlaue = STD + Tolerance;
                Min_Value = STD - Tolerance;
            }
        }


        document.getElementById('<%=txtMaxValue.ClientID%>').value = Max_Vlaue.toString();
        document.getElementById('<%=txtMinValue.ClientID%>').value = Min_Value.toString();
    }
   
   
</script>

<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table width="95%">
    <tr>
        <td valign="top">
            <table align="left" class="tContentArial" width="100%">
                <tr>
                    <td valign="top" align="left" class="td">
                        <table>
                            <tr>
                                <td id="tdSave" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                        OnClick="imgbtnSave_Click" TabIndex="14" />
                                </td>
                                <td id="tdUpdate" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                        OnClick="imgbtnUpdate_Click" TabIndex="14"></asp:ImageButton>
                                </td>
                                <td id="tdFind" runat="server">
                                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                        OnClick="imgbtnFind_Click" TabIndex="15"></asp:ImageButton>
                                </td>
                                <td id="tdPrint" runat="server">
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                        OnClick="imgbtnPrint_Click" TabIndex="16"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnList" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/list.jpg"
                                        OnClick="imgbtnList_Click" TabIndex="17"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                        OnClick="imgbtnClear_Click" TabIndex="18"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                        OnClick="imgbtnExit_Click" TabIndex="19"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                        OnClick="imgbtnHelp_Click"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="TableHeader td" width="100%">
                        <span class="titleheading"><b>Item QC Standard Master</b></span>
                    </td>
                </tr>
                <tr>
                    <td class="td" align="left" valign="top" width="100%">
                        <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="M1" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <table>
                            <tr bgcolor="#336699" class="SmallFont titleheading">
                                <td class="HeaderRow">
                                    Item&nbsp;Category
                                </td>
                                <td class="HeaderRow">
                                    Item
                                </td>
                                <td class="HeaderRow">
                                    Std&nbsp;Value
                                </td>
                                <td class="HeaderRow">
                                    Std&nbsp;Type
                                </td>
                                <td class="HeaderRow">
                                    UOM
                                </td>
                                <td class="HeaderRow">
                                    Tolerance
                                </td>
                                <td class="HeaderRow">
                                    Tolerance&nbsp;Type
                                </td>
                                <td class="HeaderRow">
                                    Tolerance&nbsp;Range
                                </td>
                                <td class="HeaderRow">
                                    Max.&nbsp;Value
                                </td>
                                <td class="HeaderRow">
                                    Min.&nbsp;Value
                                </td>
                                <td class="HeaderRow">
                                    Remarks
                                </td>
                                <td class="HeaderRow">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                    <cc2:ComboBox runat="server" ID="ddlItemCategory" CssClass="SmallFont" MenuWidth="400px"
                                        EnableLoadOnDemand="True" Width="100px" Height="180px" EmptyText="Select Item Category..."
                                        OnLoadingItems="ddlItemCategory_LoadingItems" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged1"
                                        TabIndex="1">
                                        <HeaderTemplate>
                                            <div class="header d1">
                                                Code</div>
                                            <div class="header d1">
                                                Desc</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item d1">
                                                <%# Eval("MST_CODE")%></div>
                                            <div class="item d1">
                                                <%# Eval("MST_DESC") %></div>
                                        </ItemTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td align="left" valign="top">
                                    <cc2:ComboBox ID="ddlItemCode" runat="server" CssClass="SmallFont" AutoPostBack="true"
                                        TabIndex="2" Width="100px" EnableLoadOnDemand="True" DataTextField="ITEM_DESC"
                                        DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true" OpenOnFocus="true"
                                        Visible="true" Height="200px" EmptyTextSelect="Select Item" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged"
                                        OnLoadingItems="ddlItemCode_LoadingItems">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                 Code</div>
                                            <div class="header d2">
                                                 Desc</div>
                                            <div class="header d3">
                                                Type</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("ITEM_CODE")%></div>
                                            <div class="item d2">
                                                <%# Eval("ITEM_DESC") %></div>
                                            <div class="item d3">
                                                <%# Eval("ITEM_TYPE")%></div>
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
                                    <asp:TextBox ID="txtSTD" runat="server" TabIndex="3" CssClass="TextBoxNo tdRight SmallFont"
                                        Width="70px" onChange="javascript:CalculateAmount()"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList runat="server" ID="ddlStdType" Width="100px" CssClass="Smallfont"
                                        TabIndex="4" />
                                </td>
                                <td align="left" valign="top">
                                    <cc2:ComboBox runat="server" ID="ddlUOM" Width="100px" CssClass="Smallfont" Height="180px"
                                        EmptyText="Select UOM..." EnableLoadOnDemand="true" OnLoadingItems="ddlUOM_LoadingItems"
                                        TabIndex="5" />
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtTolerance" runat="server" TabIndex="6" CssClass="TextBoxNo tdRight SmallFont"
                                        Width="70px" onChange="javascript:CalculateAmount()"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList runat="server" ID="ddltoleranceType" Width="80px" CssClass="Smallfont"
                                        TabIndex="7" onChange="javascript:CalculateAmount()" />
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList runat="server" ID="ddltolerancerange" Width="100px" CssClass="Smallfont"
                                        TabIndex="8" onChange="javascript:CalculateAmount()" />
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtMaxValue" runat="server" TabIndex="9" CssClass="TextBoxNo tdRight SmallFont"
                                        Width="60px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtMinValue" runat="server" TabIndex="10" CssClass="TextBoxNo tdRight SmallFont"
                                        Width="60px"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRemarks" runat="server" TabIndex="11" CssClass="TextBox SmallFont"
                                        Width="158px" MaxLength="200"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:Button ID="btnsaveTRNDetail" runat="server" TabIndex="12" CssClass="SmallFont"
                                        Text="Add" Width="50px" OnClick="btnsaveTRNDetail_Click" ValidationGroup="M1" />
                                    <asp:Button ID="btnCancel" runat="server" TabIndex="13" CssClass="SmallFont" Text="Cancel"
                                        Width="50px" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="100%" class="td">
                        <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                            <asp:GridView ID="grdMaterialItemReceipt" Width="99%" runat="server" AutoGenerateColumns="False"
                                CssClass="SmallFont" ShowFooter="false" OnRowCommand="grdMaterialItemReceipt_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr&nbsp;No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTRN_NUMB" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item&nbsp;Category" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCategory" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_CATEGORY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item&nbsp;Code" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Std Value" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTD_VALUE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("STD_VALUE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Std Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTD_TYPE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("STD_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tolerance" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTOLERANCE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TOLERANCE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tolerance Type" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTOLERANCE_TYPE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TOLERANCE_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tolerance Range" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTOLERANCE_RANGE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TOLERANCE_RANGE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxValue" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MAX_VALUE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinValue" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MIN_VALUE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblREMARKS" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                                CommandArgument='<%# Bind("TRN_NUMB") %>'></asp:LinkButton>
                                            /
                                            <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                                CommandArgument='<%# Bind("TRN_NUMB") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="SmallFont" />
                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
    ErrorMessage="*Item Category Required" ControlToValidate="ddlItemCategory" ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="None" runat="server"
    ErrorMessage="*Std Value required" CssClass="RequiredField" ControlToValidate="txtSTD"
    ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtSTD"
    ValidationExpression="([0-9])[0-9]*[.]?[0-9]*" ErrorMessage="Invalid No" ValidationGroup="M1">
</asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="None" runat="server"
    ErrorMessage="*Std type required" CssClass="RequiredField" ControlToValidate="ddlStdType"
    InitialValue="-1" ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="None" runat="server"
    ErrorMessage="*Tolerance required" CssClass="RequiredField" ControlToValidate="txtTolerance"
    ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTolerance"
    ValidationExpression="([0-9])[0-9]*[.]?[0-9]*" ErrorMessage="Invalid No" ValidationGroup="M1">
</asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Display="None" runat="server"
    ErrorMessage="*Tolerance type required" CssClass="RequiredField" ControlToValidate="ddltoleranceType"
    InitialValue="-1" ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
    ErrorMessage="*Tolerance Range required" CssClass="RequiredField" ControlToValidate="ddltolerancerange"
    InitialValue="-1" ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
    ErrorMessage="*Max Value required" CssClass="RequiredField" ControlToValidate="txtMaxValue"
    ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMaxValue"
    ValidationExpression="([0-9])[0-9]*[.]?[0-9]*" ErrorMessage="Invalid No" ValidationGroup="M1">
</asp:RegularExpressionValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" Display="None" runat="server"
    ErrorMessage="*Min Value required" CssClass="RequiredField" ControlToValidate="txtMinValue"
    ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtMinValue"
    ValidationExpression="(^-?0\.[0-9]*[1-9]+[0-9]*$)|(^-?[1-9]+[0-9]*((\.[0-9]*[1-9]+[0-9]*$)|(\.[0-9]+)))|(^-?[1-9]+[0-9]*$)|(^0$){1}"
    ErrorMessage="Invalid No" ValidationGroup="M1" CssClass="RequiredField">
</asp:RegularExpressionValidator>
<%--</ContentTemplate>
</asp:UpdatePanel>
--%>