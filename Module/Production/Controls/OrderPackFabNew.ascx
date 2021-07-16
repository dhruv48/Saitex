<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderPackFabNew.ascx.cs"
    Inherits="Module_Production_Controls_OrderPackFabNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
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
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 130px;
    }
    .c4
    {
        margin-left: 4px;
        width: 130px;
    }
    .c5
    {
        margin-left: 4px;
        width: 130px;
    }
    .c6
    {
        margin-left: 4px;
        width: 130px;
    }
</style>
<table id="tblMain" runat="server" align="left" width="99%" class="tContentArial tablebox"
    style="font-size: x-small">
    <tr>
        <td class="td" width="100%">
            <table align="left" class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                            ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" Width="48" Height="41" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" ValidationGroup="M1">
                        </asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" width="100%">
            <b class="titleheading">Weaving Loomshed Production Entry</b></td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial"
                width="100%">
                <tr>
                    <td align="left" colspan="6" valign="top" width="100%">
                        <span class="Mode">
                            <asp:Label ID="lblMode" runat="server"></asp:Label></span>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center" colspan="6" width="100%">
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="false" ValidationGroup="M1" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                        </strong>
                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        *Packing ID :
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPackingID" runat="server" CssClass="TextBox UpperCase TextBoxDisplay SmallFont"
                            ValidationGroup="M1" MaxLength="25" TabIndex="1" ReadOnly="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFPackingID" runat="server" ControlToValidate="txtPackingID"
                            Display="None" ErrorMessage="Please Enter Packing ID" SetFocusOnError="True"
                            ValidationGroup="M1"></asp:RequiredFieldValidator>
                        <cc1:ComboBox ID="cmbPackingID" runat="server" Width="133px" Height="200px" AutoPostBack="True"
                            EnableLoadOnDemand="True" DataTextField="PACKING_ID" DataValueField="PACKING_ID"
                            TabIndex="2" MenuWidth="800px" OnLoadingItems="cmbPackingID_LoadingItems" EnableVirtualScrolling="true"
                            OnSelectedIndexChanged="cmbPackingID_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Pack ID</div>
                                <div class="header c3">
                                    PA Number</div>
                                <div class="header c5">
                                    Article</div>
                                <div class="header c5">
                                    Shade</div>
                                <div class="header c6">
                                    LOT ID</div>
                                <div class="header c6">
                                    Pack Date</div>
                                <div class="header c6">
                                    Fr Pros Code</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("PACKING_ID")%></div>
                                <div class="item c3">
                                    <%# Eval("PI_NO")%></div>
                                <div class="item c5">
                                    <%# Eval("ARTICAL_CODE")%></div>
                                <div class="item c5">
                                    <%# Eval("SHADE_CODE")%></div>
                                <div class="item c6">
                                    <%# Eval("LOT_ID_NO")%></div>
                                <div class="item c6">
                                    <%# Eval("PACKING_DATE")%></div>
                                <div class="item c6">
                                    <%# Eval("FR_PROS_CODE")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc1:ComboBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        *Packing Date :
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPackingDate" runat="server" CssClass="TextBox SmallFont" ValidationGroup="M1"
                            MaxLength="10" TabIndex="7"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFPackingDate" runat="server" ValidationGroup="M1"
                            Display="None" ErrorMessage="Please.. Enter Date Of Packing" ControlToValidate="txtPackingDate"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <cc4:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPackingDate"
                            OnClientDateSelectionChanged="checkDate" PopupPosition="TopLeft" Format="dd/MM/yyyy">
                        </cc4:CalendarExtender>
                        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="txtPackingDate" PromptCharacter="_">
                        </cc4:MaskedEditExtender>
                    </td>
                    <td align="right" valign="top" width="17%">
                        &nbsp; *Shift:
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:DropDownList ID="txtShift" runat="server" AppendDataBoundItems="true" DataTextField="SFT_NAME"
                            DataValueField="SFT_ID" CssClass="SmallFont" TabIndex="20" Width="133px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trRow1" runat="server">
                    <td align="right" valign="top" width="17%">
                        PA No./Lot Detail&nbsp;
                    </td>
                    <td align="left" valign="top" width="17%">
                        <cc1:ComboBox ID="ddlLotIdNo" runat="server" AutoPostBack="True" DataTextField="LOT_NUMBER"
                            DataValueField="LOT_NUMBER" EmptyText="select..." EnableLoadOnDemand="true" Height="200px"
                            MenuWidth="880px" OnLoadingItems="ddlLotIdNo_LoadingItems" OnSelectedIndexChanged="ddlLotIdNo_SelectedIndexChanged"
                            Width="133px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Lot #</div>
                                <div class="header c1">
                                    Department</div>
                                <div class="header c1">
                                    Order #</div>
                                <div class="header c3">
                                    Party</div>
                                <div class="header c3">
                                    Last Process Code</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("LOT_NUMBER") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("DEPT_CODE") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal ID="Literal3" runat="server" Text='<%# Eval("ORDER_NO") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("PARTY") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("PROS_CODE") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc1:ComboBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        *PA No :
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPANo" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ValidationGroup="M1" MaxLength="50" TabIndex="10" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        LOT No :
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtLOTIDNumber" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ValidationGroup="M1" MaxLength="50" TabIndex="10" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        Party Code
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="lblPartyCode" runat="server" Text='<%# Bind("PRTY_CODE") %>' CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Party Name
                    </td>
                    <td align="left" valign="top" colspan="3" style="width: 32%">
                        <asp:TextBox ID="lblPartyName" runat="server" Text='<%# Bind("PRTY_NAME") %>' CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        Main Order No
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ValidationGroup="M1" MaxLength="9" TabIndex="9" ReadOnly="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFOrderNumber" runat="server" ControlToValidate="txtOrderNumber"
                            Display="None" ErrorMessage="Please Enter Order Number" SetFocusOnError="True"
                            ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Business Type
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="lblBusinessType" runat="server" Text='<%# Bind("BUSINESS_TYPE") %>'
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Product Type
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="lblProductType" runat="server" Text='<%# Bind("PRODUCT_TYPE") %>'
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        From Process
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtFrProcessCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ValidationGroup="M1" MaxLength="10" TabIndex="11" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Order Category
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="lblOrderCategory" runat="server" Text='<%# Bind("ORDER_CAT") %>'
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        OrderType
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="lblOrderTypeS" runat="server" Text='<%# Bind("ORDER_TYPE") %>' CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        Order Article Code
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtArticleCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" ValidationGroup="M1" MaxLength="10" TabIndex="11"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFArticleCode" runat="server" ControlToValidate="txtArticleCode"
                            Display="None" ErrorMessage="Please Enter Article Code" SetFocusOnError="True"
                            ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Order Article Desc
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtArticleDesc" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" ValidationGroup="M1" MaxLength="10" TabIndex="11"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Order Shade
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtShadeCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ValidationGroup="M1" MaxLength="10" TabIndex="11" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="17%">
                        *Order Qty
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtOrderQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ValidationGroup="M1" MaxLength="9" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Packed Qty
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtPackingQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ValidationGroup="M1" MaxLength="50" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="17%">
                        Remaining Qty
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtRemainingQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ValidationGroup="M1" MaxLength="50" ReadOnly="true"></asp:TextBox>
                    </td>
                    <tr>
                        <td align="right" valign="top" width="17%">
                            Lot Qty.
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtLotQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                ValidationGroup="M1" MaxLength="9" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right" valign="top" width="17%">
                            Lot Packed Qty
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtLotPackQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                ValidationGroup="M1" MaxLength="9" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right" valign="top" width="17%">
                            Bal Lot Qty
                        </td>
                        <td align="left" valign="top" width="15%">
                            <asp:TextBox ID="txtLotRemQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                ValidationGroup="M1" MaxLength="9" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="17%">
                            Checked By :
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtCheckedBy" runat="server" CssClass="TextBox SmallFont" ValidationGroup="M1"
                                MaxLength="15" TabIndex="21"></asp:TextBox>
                        </td>
                        <td align="right" valign="top" width="17%">
                            *Packing Category :
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:DropDownList ID="ddlPackingCategory" runat="server" AppendDataBoundItems="true"
                                CssClass="SmallFont" TabIndex="6" Width="133px">
                                <asp:ListItem Text="PACKING CAT A" Value="PACKING CAT A"></asp:ListItem>
                                <asp:ListItem Text="PACKING CAT B" Value="PACKING CAT B"></asp:ListItem>
                                <asp:ListItem Text="PACKING CAT C" Value="PACKING CAT C"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFPackingCategory" runat="server" ControlToValidate="ddlPackingCategory"
                                Display="None" ErrorMessage="Please Select Order No" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="M1"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" valign="top" width="17%">
                            &nbsp; Packing Grade
                        </td>
                        <td align="left" valign="top" width="15%">
                            <asp:TextBox ID="txtPackGrade" runat="server" CssClass="TextBoxNo SmallFont " ValidationGroup="M1"
                                MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="17%">
                            *UOM Of Unit
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:DropDownList ID="ddlUOMOfUnit" runat="server" AppendDataBoundItems="true" CssClass="SmallFont"
                                TabIndex="18" Width="133px" AutoPostBack="True" OnSelectedIndexChanged="ddlUOMOfUnit_SelectedIndexChanged">
                                <asp:ListItem>BASE UNIT</asp:ListItem>
                                <asp:ListItem>BOX</asp:ListItem>
                                <asp:ListItem>CARTON</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFUOMOfUnit" runat="server" ControlToValidate="ddlUOMOfUnit"
                                Display="None" ErrorMessage="Please Select UOM Of Unit" InitialValue="0" SetFocusOnError="True"
                                ValidationGroup="M1"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" valign="top" width="17%">
                            *Weig`ht Per Unit :
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtWtPerCone" runat="server" CssClass="TextBoxNo SmallFont" ValidationGroup="M1"
                                MaxLength="100" TabIndex="13" OnTextChanged="txtWtPerCone_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFWtPerCone" runat="server" ControlToValidate="txtWtPerCone"
                                Display="None" ErrorMessage="Please Enter Weight Per Cone" SetFocusOnError="True"
                                ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RVWtPerCone" runat="server" ValidationGroup="M1" Display="Dynamic"
                                ControlToValidate="txtWtPerCone" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Weight Per Cone"
                                Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                        </td>
                        <td align="right" valign="top" width="17%">
                            *No Of Unit :
                        </td>
                        <td align="left" valign="top" width="15%">
                            <asp:TextBox ID="txtNoOfCone" runat="server" CssClass="TextBoxNo SmallFont" ValidationGroup="M1"
                                MaxLength="100" TabIndex="12" OnTextChanged="txtNoOfCone_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFNoOfCone" runat="server" ControlToValidate="txtNoOfCone"
                                Display="None" ErrorMessage="Please Enter Number Of Cone" SetFocusOnError="True"
                                ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RVNoOfCone" runat="server" ValidationGroup="M1" Display="Dynamic"
                                ControlToValidate="txtNoOfCone" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Number Of Cone"
                                Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="17%">
                            &nbsp;Loom No :
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtLoomNo" runat="server" CssClass="TextBox SmallFont" ValidationGroup="M1"
                                MaxLength="20" TabIndex="19"></asp:TextBox>
                        </td>
                        <td align="right" valign="top" width="17%">
                            Pack Lot No:
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtPackLotNo" runat="server" CssClass="TextBox SmallFont" ValidationGroup="M1"
                                MaxLength="15" TabIndex="21"></asp:TextBox>
                        </td>
                        <td align="right" valign="top" width="17%">
                            &nbsp;&nbsp; Run Speed :
                        </td>
                        <td align="left" valign="top" width="15%">
                            <asp:TextBox ID="txtRunSpeed" runat="server" CssClass="TextBox SmallFont" ValidationGroup="M1"
                                MaxLength="20" TabIndex="19"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="17%">
                            No Of
                            Bale :
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtBaleQty" runat="server" CssClass="TextBoxNo SmallFont" ValidationGroup="M1"
                                MaxLength="20" TabIndex="19"></asp:TextBox>
                            <asp:LinkButton ID="lbtnGetBale" runat="server" CssClass="SmallFont" 
                                onclick="lbtnGetBale_Click">Get Bale</asp:LinkButton>
                        </td>
                        <td align="right" valign="top" width="17%">
                            Pack Width :
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtPackWidth" runat="server" CssClass="TextBoxNo SmallFont" ValidationGroup="M1"
                                MaxLength="20" TabIndex="19"></asp:TextBox>
                        </td>
                        <td align="right" valign="top" width="17%">
                            *Pack Qty (In Mtrs) :</td>
                        <td align="left" valign="top" width="15%">
                            <asp:TextBox ID="txtPackQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                ValidationGroup="M1" MaxLength="20" TabIndex="19" ReadOnly="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFProduction" runat="server" ControlToValidate="txtPackQty"
                                Display="None" ErrorMessage="Please Enter Production" SetFocusOnError="True"
                                ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RVProduction" runat="server" ValidationGroup="M1" Display="None"
                                ControlToValidate="txtPackQty" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number in Production"
                                Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="17%">
                            Load Date :
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtLoadDate" runat="server" CssClass="TextBox SmallFont" ValidationGroup="M1"
                                MaxLength="20" TabIndex="19"></asp:TextBox>
                        </td>
                        <td align="right" valign="top" width="17%">
                            Unload Date :
                        </td>
                        <td align="left" valign="top" width="17%">
                            <asp:TextBox ID="txtUnloadDate" runat="server" CssClass="TextBox SmallFont" ValidationGroup="M1"
                                MaxLength="20" TabIndex="19"></asp:TextBox>
                        </td>
                        <td align="right" valign="top" width="17%">
                            Stop Time (In Min.):
                        </td>
                        <td align="left" valign="top" width="15%">
                            <asp:TextBox ID="txtStopTime" runat="server" CssClass="TextBoxNo SmallFont" ValidationGroup="M1"
                                MaxLength="20" TabIndex="19"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="17%">
                            Remarks :
                        </td>
                        <td align="left" valign="top" width="17%" colspan="5">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" ValidationGroup="M1"
                                MaxLength="225" TabIndex="22" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
            </table>
        </td>
    </tr>
</table>
