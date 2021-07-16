<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="ItemMaster.aspx.cs" Inherits="Inventory_ItemMaster" Title="Item Master" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <script src="Scripts/jquery-3.1.1.min.js"></script>
    <script language="javascript" type="text/javascript">
        function getRadiovalue() {
            alert("are you sure")
        }
        function showhide() {
            var div = document.getElementById("newpost");
            if (div.style.display !== "none") {
                div.style.display = "none";
            }
            else {
                div.style.display = "block";
            }
        }

    </script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $('.image-link').viewbox({
                setTitle: true,
                margin: 20,
                resizeDuration: 300,
                openDuration: 200,
                closeDuration: 200,
                closeButton: true,
                navButtons: true,
                closeOnSideClick: true,
                nextOnContentClick: true
            });
        });
</script>
    <script>
        function myFunction() {
            var x = document.getElementById("myDIV");
            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
        }
</script>
    <style type="text/css">
        .panel {
            display: none;
        }

        .style1 {
            width: 620%;
        }

        .item {
            position: relative !important;
            display: -moz-inline-stack;
            display: inline-block;
            zoom: 1;
            *display: inline;
            overflow: hidden;
            white-space: nowrap;
        }

        .header {
            margin-left: 2px;
        }

        .c1 {
            width: 80px;
        }

        .c2 {
            margin-left: 4px;
            width: 100px;
        }

        .c3 {
            margin-left: 4px;
            width: 100px;
        }

        .d1 {
            width: 150px;
        }

        .d2 {
            margin-left: 4px;
            width: 350px;
        }

        .d3 {
            width: 80px;
        }
        .auto-style1 {
            height: 25px;
        }
        .auto-style2 {
            width: 82px;
        }
    </style>


    <script type="text/javascript" src="../../../javascript/jquery-1.4.1.min.js"></script>
    <script src="../../../javascript/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../../../javascript/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtItemDesc.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/MOM.asmx/GetItemDescription") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response(data.d);
                            //                        response($.map(data.d, function(item) {
                            //                            return {
                            //                                label: item.split('-')[0],
                            //                                val: item.split('-')[1]
                            //                            }
                            //                        }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                //            select: function(e, i) {
                //                $("#<%=hfDescriptionId.ClientID %>").val(i.item.val);
                //            },
                minLength: 1
            });
        });
    </script>
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
                                                    OnClick="imgbtnSave_Click" ValidationGroup="M1" TabIndex="26" />
                                            </td>
                                            <td id="tdUpdate" runat="server">
                                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                                    OnClick="imgbtnUpdate_Click" ValidationGroup="M1" TabIndex="37"></asp:ImageButton>
                                            </td>
                                            <td id="tdDelete" runat="server">
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                                    Enabled="false" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                                            </td>
                                            <td id="tdFind" runat="server">
                                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                                    OnClick="imgbtnFind_Click" TabIndex="38"></asp:ImageButton>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnList" runat="server" ToolTip="List"
                                                    ImageUrl="~/CommonImages/list.jpg" OnClick="imgBtnList_Click" TabIndex="39"></asp:ImageButton>
                                            </td>
                                            <td id="tdPrint" runat="server">
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                                    OnClick="imgbtnPrint_Click" TabIndex="40"></asp:ImageButton>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                                    OnClick="imgbtnClear_Click" TabIndex="41"></asp:ImageButton>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                                    OnClick="imgbtnExit_Click" TabIndex="42"></asp:ImageButton>
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
                                    <span class="titleheading"><b>Item Master</b></span>
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
                                            <td align="right" valign="top" style="font-weight: bold" width="20%">
                                                <font color="#ff0000">*</font>Item Code :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtItemCode" runat="server" CssClass="TextBox UpperCase SmallFont"
                                                    Width="145px" MaxLength="10" ValidationGroup="M1" ReadOnly="True" TabIndex="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server"
                                                    ErrorMessage="*Item Code Required" ControlToValidate="txtItemCode" CssClass="RequiredField"
                                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                                <cc2:ComboBox ID="ddlItemCode" runat="server" CssClass="SmallFont" AutoPostBack="true" TabIndex="1"
                                                    Width="150px" EnableLoadOnDemand="True" DataTextField="ITEM_DESC" DataValueField="ITEM_CODE"
                                                    MenuWidth="650px" EnableVirtualScrolling="true" OpenOnFocus="true" Visible="true"
                                                    Height="200px" EmptyTextSelect="Select Item" OnSelectedIndexChanged="ddlItemCode_SelectedIndexChanged"
                                                    OnLoadingItems="ddlItemCode_LoadingItems">
                                                    <HeaderTemplate>
                                                        <div class="header d1">
                                                            ITEM CODE
                                                        </div>
                                                        <div class="header d2">
                                                            ITEM DESCRIPTION
                                                        </div>
                                                        <div class="header d3">
                                                            TYPE
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item d1">
                                                            <%# Eval("ITEM_CODE")%>
                                                        </div>
                                                        <div class="item d2">
                                                            <%# Eval("ITEM_DESC") %>
                                                        </div>
                                                        <div class="item d3">
                                                            <%# Eval("ITEM_TYPE")%>
                                                        </div>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc2:ComboBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtItemDesc"
                                                    CssClass="RequiredField" Display="None" ErrorMessage="*Item description Required"
                                                    ValidationGroup="M1"></asp:RequiredFieldValidator>

                                                <asp:RadioButton ID="rdAuto" runat="server" Checked="true" Text="Auto" AutoPostBack="true"
                                                    OnCheckedChanged="rdAuto_CheckedChanged" CssClass="SmallFont" />
                                                <asp:RadioButton ID="rdManual" runat="server" Checked="false" Text="Manual" AutoPostBack="true"
                                                    OnCheckedChanged="rdManual_CheckedChanged" CssClass="SmallFont" />

                                            </td>
                                            <td align="right" width="20%">Department :
                                            </td>
                                            <td width="30%">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="8"
                                                    Width="150px" TabIndex="1">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">Item Description :
                                            </td>
                                            <td align="left" valign="top" colspan="3">
                                                <asp:TextBox ID="txtItemDesc" runat="server" CssClass="gCtrTxt SmallFont UpperCase"
                                                    Width="80%" TabIndex="2" MaxLength="340" Rows="2" AutoPostBack="false"
                                                    OnTextChanged="txtItemDesc_TextChanged"></asp:TextBox>
                                                <asp:HiddenField ID="hfDescriptionId" runat="server" />
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
                                                <font color="#ff0000">*</font>Item Category :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <%--   &nbsp;<asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="tContentArial"
                                            Width="150px" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" TabIndex="5"
                                            AutoPostBack="True">
                                        </asp:DropDownList>--%>
                                                <cc2:ComboBox runat="server" ID="ddlItemCategory" CssClass="SmallFont" MenuWidth="400px" EnableLoadOnDemand="True"
                                                    Width="150px" Height="180px" EmptyText="Select Item Category..."
                                                    OnLoadingItems="ddlItemCategory_LoadingItems" AutoPostBack="True" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged1"
                                                    TabIndex="3">
                                                    <HeaderTemplate>
                                                        <div class="header d1">
                                                            Code
                                                        </div>
                                                        <div class="header d1">
                                                            Desc
                                                        </div>

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item d1">
                                                            <%# Eval("MST_CODE")%>
                                                        </div>
                                                        <div class="item d1">
                                                            <%# Eval("MST_DESC") %>
                                                        </div>

                                                    </ItemTemplate>

                                                </cc2:ComboBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                                    ErrorMessage="*Item Category Required" ControlToValidate="ddlItemCategory" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                <font color="#ff0000">*</font>Item Subcategory:
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <cc2:ComboBox runat="server" ID="ddlsubcategory" CssClass="SmallFont" MenuWidth="280px" EnableLoadOnDemand="True"
                                                    Width="150px" Height="180px" EmptyText="Select Item SubCategory..."
                                                    OnLoadingItems="ddlsubcategory_LoadingItems" AutoPostBack="True" OnSelectedIndexChanged="ddlsubcategory_SelectedIndexChanged"
                                                    TabIndex="4">
                                                    <HeaderTemplate>
                                                        <div class="header d1">
                                                            Code
                                                        </div>
                                                        <div class="header d1">
                                                            Desc
                                                        </div>

                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item d1">
                                                            <%# Eval("MST_CODE")%>
                                                        </div>
                                                        <div class="item d1">
                                                            <%# Eval("MST_DESC") %>
                                                        </div>

                                                    </ItemTemplate>

                                                </cc2:ComboBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="None" runat="server"
                                                    ErrorMessage="*Item CategorySub Required" ControlToValidate="ddlItemCategory" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">Item Type :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <%--  <asp:DropDownList ID="ddlItemType" runat="server" CssClass="tContentArial" Width="151px"
                                            TabIndex="6">
                                        </asp:DropDownList>--%>
                                                <cc2:ComboBox runat="server" ID="ddlItemType" CssClass="SmallFont" Width="150px"
                                                    Height="180px" EmptyText="Select Item Type..." EnableLoadOnDemand="true" OnLoadingItems="ddlItemType_LoadingItems"
                                                    TabIndex="5" />
                                            </td>
                                            <td align="right" valign="top" width="20%">Associated Item Code :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <cc2:ComboBox ID="txtAsocItemCode" runat="server" EnableLoadOnDemand="true" OnLoadingItems="txtAsocItemCode_LoadingItems"
                                                    CssClass="SmallFont" DataTextField="ITEM_DESC" DataValueField="ITEM_CODE" Width="150px"
                                                    MenuWidth="350px" Height="200px" TabIndex="6" EmptyText="Select Associated Item Code">
                                                    <HeaderTemplate>
                                                        <div class="header c1">
                                                            Code
                                                        </div>
                                                        <div class="header c2">
                                                            DESCRIPTION
                                                        </div>
                                                        <div class="header c3">
                                                            TYPE
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("ITEM_CODE") %>' />
                                                        </div>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("ITEM_DESC") %>' />
                                                        </div>
                                                        <div class="item c3">
                                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("ITEM_TYPE") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
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
                                        <tr>
                                            <td align="right" valign="top" width="20%" visible="false">

                                                <font color="#ff0000">*</font>Minimum Stock Level :
                                            </td>
                                            <td align="left" valign="top" width="30%">


                                                <asp:TextBox ID="txtMinStockLevel" runat="server" CssClass="gCtrTxtNo SmallFont"
                                                    Width="145px" TabIndex="7" MaxLength="10"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RangeValidator3" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter minimum stock numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtMinStockLevel" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>

                                            </td>
                                            <td align="right" valign="top" width="20%">Rack Code :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtrackCode" runat="server" CssClass="gCtrTxt SmallFont" Width="40px"
                                                    TabIndex="8" MaxLength="15"></asp:TextBox>
                                                <asp:TextBox ID="txtrackCode2" runat="server" CssClass="gCtrTxt SmallFont" Width="40px"
                                                    TabIndex="9" MaxLength="50"></asp:TextBox>
                                                <asp:TextBox ID="txtrackCode3" runat="server" CssClass="gCtrTxt SmallFont" Width="40px"
                                                    TabIndex="10" MaxLength="50"></asp:TextBox><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                <font color="#ff0000">*</font>Maximum Stock Level:
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtMaxStockLevel0" runat="server" CssClass="gCtrTxtNo SmallFont"
                                                    Width="145px" TabIndex="11" MaxLength="10"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator8" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter minimum stock numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtMaxStockLevel0" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                <font color="#ff0000">*</font>Reorder Level :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtReorderLevel" runat="server" CssClass="gCtrTxtNo SmallFont" Width="145px"
                                                    TabIndex="12" MaxLength="10"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RangeValidator5" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter reorder level numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtReorderLevel" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">
                                                <font color="#ff0000">*</font>Reorder Quantity :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtReorderQt" runat="server" CssClass="gCtrTxtNo SmallFont" Width="145px"
                                                    TabIndex="13" MaxLength="10"></asp:TextBox><br />
                                                <asp:RangeValidator ID="RangeValidator4" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter reorder quantity numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtReorderQt" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                <font color="#ff0000">*</font>Minimum Procure Day :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtMinProcDay" runat="server" CssClass="gCtrTxtNo SmallFont" Width="145px"
                                                    TabIndex="14" MaxLength="3"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator6" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter minimum procure days numeric value between 1 to 10000"
                                                    Display="None" Type="Integer" ControlToValidate="txtMinProcDay" MinimumValue="0"
                                                    MaximumValue="10000"></asp:RangeValidator>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%" style="display: none">
                                                <%-- <font color="#ff0000">*</font>Opening Balance Stock :--%>
                                            </td>
                                            <td align="left" valign="top" width="30%" style="display: none">
                                                <asp:TextBox ID="txtOpBalStock" runat="server" CssClass="gCtrTxtNo SmallFont" Width="145px"
                                                    MaxLength="15" Visible="false"></asp:TextBox><br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="RequiredField"
                                                    Display="None" runat="server" ErrorMessage="*Opening balance required" ControlToValidate="txtOpBalStock"
                                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter opening balance numeric value between 0 to 100000"
                                                    Display="None" Type="Double" ControlToValidate="txtOpBalStock" MinimumValue="0"
                                                    MaximumValue="999999999"></asp:RangeValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                <font color="#ff0000">*</font>UOM :
                                            </td>
                                            <td align="left" valign="top" width="30%">

                                                <cc2:ComboBox runat="server" ID="ddlUOM" Width="150px" CssClass="smallfont" Height="180px"
                                                    EmptyText="Select UOM..." EnableLoadOnDemand="true" OnLoadingItems="ddlUOM_LoadingItems"
                                                    TabIndex="15" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                                                    ErrorMessage="*UOM required" CssClass="RequiredField" ControlToValidate="ddlUOM"
                                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                <font color="#ff0000">*</font>Expire Days :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtExpDay" runat="server" CssClass="gCtrTxtNo SmallFont" Width="145px"
                                                    TabIndex="16" MaxLength="10"></asp:TextBox>
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
                                            <td align="right" valign="top" width="20%">Consumable/Non Consumable:
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:DropDownList ID="ddlConsumble" runat="server" CssClass="SmallFont" TabIndex="17" Width="145px">
                                                    <asp:ListItem Selected="True">Consumable</asp:ListItem>
                                                    <asp:ListItem>Non Consumable</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                <font color="#ff0000">*</font>Opening Rate :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtOpRate" runat="server" CssClass="gCtrTxt SmallFont" Width="145px"
                                                    TabIndex="18" MaxLength="50"></asp:TextBox>
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
                                    <table width="100%">
                                        <tr>
                                            <td align="right" valign="top" width="20%">Item Size :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtItemSize" runat="server" CssClass="gCtrTxt SmallFont" Width="145px"
                                                    TabIndex="19" MaxLength="50"></asp:TextBox>&nbsp;
                                            </td>
                                            <td align="right" valign="top" width="20%">QC Required :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:RadioButtonList ID="rad_qc_req" runat="server" CssClass="SmallFont" RepeatColumns="4" Width="100px"
                                                    RepeatDirection="Horizontal" TabIndex="20" Height="11px">
                                                    <asp:ListItem Value="yes" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">Colors :
                                            </td>
                                            <td align="left" valign="top" width="30%">

                                                <asp:DropDownList ID="ddlItemMake" runat="server" CssClass="gCtrTxt SmallFont" Width="145px"
                                                    TabIndex="21" MaxLength="50" Visible="True">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" valign="top" width="20%">Weight :
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtWeight" runat="server" CssClass="gCtrTxt SmallFont" Width="145px"
                                                    TabIndex="22" MaxLength="15"></asp:TextBox><br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">Chemical/Dyes Cat:
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:RadioButtonList ID="rdIsExciable" runat="server" CssClass="SmallFont" RepeatColumns="4"
                                                    RepeatDirection="Horizontal" TabIndex="23" Height="11px"
                                                    RepeatLayout="Table" Width="100px"
                                                    OnSelectedIndexChanged="rdIsExciable_SelectedIndexChanged"
                                                    AutoPostBack="True" Visible="false">
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:DropDownList ID="ddlMovable" runat="server" CssClass="SmallFont" TabIndex="24" Width="145px">
                                                    <asp:ListItem Value="2" Selected="True">NA</asp:ListItem>
                                                    <asp:ListItem Value="1">Normal</asp:ListItem>
                                                    <asp:ListItem Value="0">Sensitive</asp:ListItem>
                                                </asp:DropDownList>


                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                <font color="#ff0000">*</font>HSN&nbsp;Code&nbsp;:
                                                <%--<font color="#ff0000">*</font>ITCHS&nbsp;Code&nbsp;Custom&nbsp;:--%>
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtHSNCODE" runat="server" CssClass="gCtrTxt SmallFont" Width="145px"
                                                    TabIndex="25" MaxLength="10"></asp:TextBox>
                                                <asp:TextBox ID="txtCustom_ITCHS" runat="server" Visible="false" CssClass="gCtrTxt SmallFont" Width="145px"
                                                    MaxLength="12"></asp:TextBox>
                                                <asp:RangeValidator ID="txtCustom_ITCHS_RangeValidator" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter custom itchs code."
                                                    Display="None" Type="Double" ControlToValidate="txtCustom_ITCHS" MinimumValue="00000000"
                                                    MaximumValue="99999999"></asp:RangeValidator>
                                                <cc1:FilteredTextBoxExtender ID="txtCustom_ITCHS_FilteredTextBoxExtender1" runat="server"
                                                    Enabled="True" TargetControlID="txtCustom_ITCHS" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">Branch :
                                            </td>
                                            <td align="left" valign="top" width="30%">

                                                <asp:DropDownList ID="ddlbranchUnit" runat="server" CssClass="gCtrTxt SmallFont" Width="145px"
                                                    TabIndex="25" MaxLength="50" Visible="True">
                                                     <asp:ListItem Selected="True">SPINING KASHI VISWANATH</asp:ListItem>
                                                    <asp:ListItem>FIBER KASHI VISWANATH</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                            <td align="right" valign="top" width="20%">Item Status :                                                                                      
                                                
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:DropDownList ID="ddlItemStatus" runat="server" CssClass="SmallFont" TabIndex="26" Width="145px">
                                                    <asp:ListItem Selected="True">Open</asp:ListItem>
                                                    <asp:ListItem>Close</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%"></td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtSales_ITCHS" runat="server" Visible="false" CssClass="gCtrTxt SmallFont" Width="145px"
                                                     MaxLength="8"></asp:TextBox>
                                                <asp:RangeValidator ID="txtSales_ITCHS_RangeValidator" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter sales itchs code."
                                                    Display="None" Type="Double" ControlToValidate="txtSales_ITCHS" MinimumValue="00000000"
                                                    MaximumValue="99999999"></asp:RangeValidator>
                                                <cc1:FilteredTextBoxExtender ID="txtSales_ITCHS_FilteredTextBoxExtender1" runat="server"
                                                    Enabled="True" TargetControlID="txtSales_ITCHS" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td align="right" valign="top" width="20%">
                                                <%-- Tariff&nbsp;Heading&nbsp;(Chapter&nbsp;No)&nbsp;:--%>
                                            </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtTariffHeading" runat="server" Visible="false" CssClass="gCtrTxt SmallFont" Width="145px"
                                                     MaxLength="8"></asp:TextBox>
                                                <asp:RangeValidator ID="txtTariffHeadingValidator" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter tariff heading."
                                                    Display="None" Type="Double" ControlToValidate="txtTariffHeading" MinimumValue="00000000"
                                                    MaximumValue="99999999"></asp:RangeValidator>
                                                <cc1:FilteredTextBoxExtender ID="txtTariffHeading_FilteredTextBoxExtender" runat="server"
                                                    Enabled="True" TargetControlID="txtTariffHeading" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" valign="top" visible="false" width="20%" class="auto-style1"><font color="#ff0000">*</font>Vendor(Multiple Selection) : </td>
                                            <td align="left" valign="top" width="30%" class="auto-style1">
                                                <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" CssClass="SmallFont" EmptyText="Select Vendor..." EnableLoadOnDemand="True" Height="180px" MenuWidth="400px" OnLoadingItems="txtPartyCode_LoadingItems" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged" TabIndex="27" Width="150px"
                                                    DataTextField="PRTY_CODE" DataValueField="Address">
                                                    <HeaderTemplate>
                                                        <div class="header d1">
                                                            Vendor Code
                                                        </div>
                                                        <div class="header d1">
                                                            Vendor Name
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item d1">
                                                            <%# Eval("PRTY_CODE")%>
                                                        </div>
                                                        <div class="item d1">
                                                            <%# Eval("PRTY_NAME") %>
                                                        </div>
                                                    </ItemTemplate>
                                                </cc2:ComboBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPartyCode" Display="None" ErrorMessage="*Vendor name" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right" valign="top" width="20%" class="auto-style1">Vendor Name : </td>
                                            <td align="left" valign="top" width="30%" class="auto-style1">
                                                <asp:TextBox ID="txtPartyName" runat="server" CssClass="gCtrTxt SmallFont" MaxLength="15" Width="145px"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">Serial No: </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtserial" runat="server" CssClass="gCtrTxt SmallFont" MaxLength="10" TabIndex="28" Width="145px"></asp:TextBox>
                                            </td>
                                            <td align="right" valign="top" width="20%">Part No : </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtpartno" runat="server" CssClass="gCtrTxt SmallFont" Width="145px" TabIndex="29"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">Catalogue No : </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtcatalog" runat="server" CssClass="gCtrTxt SmallFont" MaxLength="10" TabIndex="30" Width="145px"></asp:TextBox>
                                                <br />
                                            </td>
                                            <td align="right" valign="top" width="20%">Drawing No : </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtdrowing" runat="server" CssClass="gCtrTxt SmallFont" MaxLength="3" Width="145px" TabIndex="31"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" valign="top" width="20%">Other No : </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:TextBox ID="txtother" runat="server" CssClass="gCtrTxt SmallFont" MaxLength="15" Width="145px" TabIndex="32"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">

                                        <tr>
                                            <td align="right" valign="top" width="20%">Image: </td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="33" />
                                                <asp:Button ID="btnUpload" runat="server" Height="22px" OnClick="btnUpload_Click" Text="Upload" TabIndex="34"/>
                                            </td>
                                            <td align="right" valign="top" width="20%"></td>
                                            <td align="left" valign="top" width="30%">
                                                <asp:Label ID="lblPath" runat="server" align="left" Width="50px"></asp:Label>
                                                <a id="popupImg" href="../../../APP_IMAGES/No_Image.jpg" runat="server" class="image-link">
                                                <asp:Image ID="ItemImage" runat="server" ImageUrl="~/APP_IMAGES/No_Image.jpg" Width="50px" />
                                                </a></td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>

                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td align="right" valign="top" width="20%">Remarks : </td>
                                            <td align="left" valign="top" width="80%">
                                                <asp:TextBox ID="txtremarks" runat="server" CssClass="gCtrTxt SmallFont" MaxLength="100" TabIndex="35" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" align="left" class="auto-style2">
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
        <Triggers>
            <asp:PostBackTrigger ControlID="ddlItemCategory" />
             <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
