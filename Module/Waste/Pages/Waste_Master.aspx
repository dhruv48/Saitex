<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master"  AutoEventWireup="true" CodeFile="Waste_Master.aspx.cs" Inherits="Module_Waste_Pages_Waste_Master" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">

    <script language="javascript" type="text/javascript">
        function getRadiovalue() {
            alert("are you sure")
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
            width: 150px;
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
    </style>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
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
                                                    OnClick="imgbtnSave_Click" ValidationGroup="M1" />
                                            </td>
                                            <td id="tdUpdate" runat="server">
                                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                                    OnClick="imgbtnUpdate_Click" ValidationGroup="M1"></asp:ImageButton>
                                            </td>
                                            <td id="tdDelete" runat="server">
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                                    Enabled="false" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                                            </td>
                                            <td id="tdFind" runat="server">
                                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                                    OnClick="imgbtnFind_Click"></asp:ImageButton>
                                            </td>
                                            <td id="tdPrint" runat="server">
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                                    OnClick="imgbtnPrint_Click"></asp:ImageButton>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                                    OnClick="imgbtnClear_Click"></asp:ImageButton>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                                    OnClick="imgbtnExit_Click"></asp:ImageButton>
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
                                    <span class="titleheading"><b>Waste Master</b></span>
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
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" valign="top" style="font-weight: bold" width="20%" >
                                                *Waste Code :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtItemCode" runat="server" CssClass="TextBox UpperCase SmallFont"
                                                    Width="80px" MaxLength="10" ValidationGroup="M1" ReadOnly="True" TabIndex="0"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                                                    ErrorMessage="*Item Code Required" ControlToValidate="txtItemCode" CssClass="RequiredField"
                                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                                <%--<cc2:ComboBox ID="ddlItemCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                                    OnLoadingItems="ddlItemCode_LoadingItems" DataTextField="ITEM_DESC" DataValueField="ITEM_CODE"
                                                    Width="150px" MenuWidth="350px" Height="200px" CssClass="SmallFont" TabIndex="1"
                                                    EmptyText="Find Item" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged">
                                                    <HeaderTemplate>
                                                        <div class="header c1">
                                                            Code</div>
                                                        <div class="header c2">
                                                            DESCRIPTION</div>
                                                        <div class="header c3">
                                                            TYPE</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("ITEM_CODE") %>' /></div>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ITEM_DESC") %>' /></div>
                                                        <div class="item c3">
                                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ITEM_TYPE") %>' /></div>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                        out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc2:ComboBox>--%>
                                                <cc2:ComboBox ID="ddlItemCode" runat="server" CssClass="SmallFont" AutoPostBack="true"
                                                    Width="150px" EnableLoadOnDemand="True" DataTextField="ITEM_DESC" DataValueField="ITEM_CODE"
                                                    MenuWidth="650px" EnableVirtualScrolling="true" OpenOnFocus="true" Visible="true"
                                                    Height="200px" EmptyTextSelect="Select Item" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged"
                                                    OnLoadingItems="ddlItemCode_LoadingItems">
                                                    <HeaderTemplate>
                                                        <div class="header d1">
                                                            ITEM CODE</div>
                                                        <div class="header d2">
                                                            ITEM DESCRIPTION</div>
                                                        <div class="header d3">
                                                            TYPE</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item d1">
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtItemDesc"
                                                    CssClass="RequiredField" Display="None" ErrorMessage="*Item description Required"
                                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right" width="20%">
                Department :
            </td>
                                            <td width="30%">
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="150px">
                    <asp:ListItem Text="WASTE" Value="WASTES" Selected="True"> </asp:ListItem>
                </asp:DropDownList>
            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                Waste Description :
                                            </td>
                                            <td align="left" valign="top"  colspan = "3">
                                                <asp:TextBox ID="txtItemDesc" runat="server" CssClass="gCtrTxt SmallFont UpperCase"
                                                    Width="84%" TabIndex="1" MaxLength="340" Rows="2" AutoPostBack="false" 
                                                    OnTextChanged="txtItemDesc_TextChanged"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                *Waste Category :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <%--   &nbsp;<asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="tContentArial"
                                            Width="150px" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" TabIndex="5"
                                            AutoPostBack="True">
                                        </asp:DropDownList>--%>
                                                <cc2:ComboBox runat="server" ID="ddlItemCategory" CssClass="SmallFont" MenuWidth="250px"
                                                    Width="150px" Height="180px" EmptyText="Select Item Category..." EnableLoadOnDemand="true"
                                                    OnLoadingItems="ddlItemCategory_LoadingItems" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged1"
                                                    TabIndex="2" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                                    ErrorMessage="*Item Category Required" ControlToValidate="ddlItemCategory" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                UOM :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <%-- &nbsp;<asp:DropDownList ID="ddlUOM" CssClass="tContentArial" runat="server" Width="152px"
                                            OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" TabIndex="5">
                                        </asp:DropDownList>--%>
                                                <cc2:ComboBox runat="server" ID="ddlUOM" Width="150px" CssClass="smallfont" Height="180px"
                                                    EmptyText="Select UOM..." EnableLoadOnDemand="true" OnLoadingItems="ddlUOM_LoadingItems"
                                                    TabIndex="3" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                                                    ErrorMessage="*UOM required" CssClass="RequiredField" ControlToValidate="ddlUOM"
                                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="tr1" visible="false">
                                            <td align="right" valign="top" width="20%">
                                                Waste Type :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <%--  <asp:DropDownList ID="ddlItemType" runat="server" CssClass="tContentArial" Width="151px"
                                            TabIndex="6">
                                        </asp:DropDownList>--%>
                                                <cc2:ComboBox runat="server" ID="ddlItemType" CssClass="SmallFont" Width="150px"
                                                    Height="180px" EmptyText="Select Item Type..."  EnableLoadOnDemand="true" OnLoadingItems="ddlItemType_LoadingItems"
                                                    TabIndex="4" />
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                Associated Waste Code :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <cc2:ComboBox ID="txtAsocItemCode" runat="server" EnableLoadOnDemand="true" OnLoadingItems="txtAsocItemCode_LoadingItems"
                                                    CssClass="SmallFont" DataTextField="ITEM_DESC" DataValueField="ITEM_CODE" Width="150px"
                                                    MenuWidth="350px" Height="200px" TabIndex="5" EmptyText="Select Associated Item Code">
                                                    <HeaderTemplate>
                                                        <div class="header c1">
                                                            Code</div>
                                                        <div class="header c2">
                                                            DESCRIPTION</div>
                                                        <div class="header c3">
                                                            TYPE</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("ITEM_CODE") %>' /></div>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ITEM_DESC") %>' /></div>
                                                        <div class="item c3">
                                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ITEM_TYPE") %>' /></div>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                        out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc2:ComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr id="tr2" runat="server" visible="false">
                                            <td align="right" valign="top" width="20%">
                                                Item Make :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtItemMake" runat="server" CssClass="gCtrTxt SmallFont" Width="145px"
                                                    TabIndex="6" MaxLength="50" Text="0"></asp:TextBox>&nbsp;
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                Rack Code :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtrackCode" runat="server" CssClass="gCtrTxt SmallFont" Width="149px"
                                                    TabIndex="7" MaxLength="15" Text="0"></asp:TextBox><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                Minimum Stock Level :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtMinStockLevel" runat="server" CssClass="gCtrTxtNo SmallFont"
                                                    Width="80px" TabIndex="8" MaxLength="10" Text="0"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RangeValidator3" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter minimum stock numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtMinStockLevel" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                Reorder Level :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtReorderLevel" runat="server" CssClass="gCtrTxtNo SmallFont" Width="80px"
                                                    TabIndex="9" MaxLength="10" Text="0"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RangeValidator5" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter reorder level numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtReorderLevel" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                Maximum Stock Level:
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtMaxStockLevel0" runat="server" CssClass="gCtrTxtNo SmallFont"
                                                    Width="80px" TabIndex="8" MaxLength="10" Text="0"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator8" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter minimum stock numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtMaxStockLevel0" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>
                                            </td>
                                             <td align="right" valign="top" width="20%">
                                                Reorder Quantity :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtReorderQt" runat="server" CssClass="gCtrTxtNo SmallFont" Width="80px"
                                                    TabIndex="10" MaxLength="10" Text="0"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RangeValidator4" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter reorder quantity numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtReorderQt" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr visible="false" id="tr3" runat="server">
                                        <td align="right" valign="top" width="20%">
                                                Consumable/Non Consumable:
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:DropDownList ID="ddlConsumble" runat="server" CssClass="SmallFont" TabIndex="16">
                                                    <asp:ListItem Selected="True">Consumable</asp:ListItem>
                                                    <asp:ListItem>Non Consumable</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                           
                                            <td align="right" valign="top" width="20%">
                                                Expire Days :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtExpDay" runat="server" CssClass="gCtrTxtNo SmallFont" Width="80px"
                                                    TabIndex="11" MaxLength="10" Text="0"> </asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator7" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter expiry days numeric value between 0 to 999999"
                                                    Display="None" Type="Double" ControlToValidate="txtExpDay" MinimumValue="0" MaximumValue="999999"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                *Opening Balance Stock :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtOpBalStock" runat="server" CssClass="gCtrTxtNo SmallFont" Width="80px"
                                                    TabIndex="12" MaxLength="15" Text="0"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="RequiredField"
                                                    Display="None" runat="server" ErrorMessage="*Opening balance required" ControlToValidate="txtOpBalStock"
                                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter opening balance numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtOpBalStock" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                *Opening Rate :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtOpRate" runat="server" CssClass="gCtrTxt SmallFont" Width="80px"
                                                    TabIndex="13" MaxLength="50" Text="0"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator2" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter opening Rate numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtOpRate" MinimumValue="0" MaximumValue="9999999999"></asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="RequiredField"
                                                    Display="None" runat="server" ErrorMessage="*Opening Rate required" ControlToValidate="txtOpRate"
                                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%" id="tbl1" runat="server" visible="false">
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                Minimum Procure Day :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtMinProcDay" runat="server" CssClass="gCtrTxtNo SmallFont" Width="80px"
                                                    TabIndex="14" MaxLength="3" Text="0"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator6" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter minimum procure days numeric value between 1 to 10000"
                                                    Display="None" Type="Integer" ControlToValidate="txtMinProcDay" MinimumValue="0"
                                                    MaximumValue="10000"></asp:RangeValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                         <%--       Quality Checking Required :--%>
                                            </td>
                                            <td align="left" valign="top" width="30%" >
                                                <asp:RadioButtonList ID="rad_qc_req" runat="server" CssClass="SmallFont" RepeatColumns="2" Visible="false"
                                                    RepeatDirection="Horizontal" TabIndex="15" Height="11px">
                                                    <asp:ListItem Value="yes" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                Remarks :
                                            </td>
                                            <td align="left" valign="top" width="80%">
                                                <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" CssClass="gCtrTxt SmallFont"
                                                    Width="90%" TabIndex="16" MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                Waste Status :
                                            </td>
                                            <td align="left" valign="top" width="80%">
                                                <asp:DropDownList ID="ddlItemStatus" runat="server" CssClass="SmallFont" TabIndex="16">
                                                    <asp:ListItem Selected="True">Open</asp:ListItem>
                                                    <asp:ListItem>Close</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" align="left">
                        <table>
                            <tr>
                                <td>
                                    <asp:PlaceHolder ID="phGrid1" runat="server"></asp:PlaceHolder>
                                    <%--  <cc3:Grid ID="grdSuggestion" runat="server" AllowAddingRecords="false" FolderStyle="~/StyleSheet/black_glass"
                    AllowSorting="true" AllowRecordSelection="False" 
                    onrebind="grdSuggestion_Rebind">
                </cc3:Grid>--%>
                                    <%-- <asp:GridView ID="grdSuggestion" runat="server">
                </asp:GridView>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
