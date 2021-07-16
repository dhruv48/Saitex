<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BlowRoom.ascx.cs" Inherits="Module_Production_Controls_BlowRoom" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100;
    }
    .c4
    {
        margin-left: 4px;
        width: 150;
    }
</style>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="YM"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">
                <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont"> Blow Room and mixing Form</asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="17%">
                        Machine Code
                    </td>
                    <td class="tdLeft" width="17%">
                        <cc2:ComboBox ID="ddlMachineCode" runat="server" DataTextField="MACHINE_CODE" DataValueField="MACHINE_DATA"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="750px" OnLoadingItems="ddlMachineCode_LoadingItems"
                            OnSelectedIndexChanged="ddlMachineCode_SelectedIndexChanged" Width="98%" AutoPostBack="True">
                            <HeaderTemplate>
                                <div class="header c4">
                                    Machine Code</div>
                                <div class="header c5">
                                    Machine Group</div>
                                <div class="header c2">
                                    Machine Make</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c4">
                                    <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                </div>
                                <div class="item c5">
                                    <asp:Literal ID="Container10" runat="server" Text='<%# Eval("MACHINE_GROUP") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container11" runat="server" Text='<%# Eval("MACHINE_MAKE") %>' />
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
                    <td class="tdLeft" colspan="4" width="66%">
                        <asp:TextBox ID="txtMachineDesc" runat="server" Width="99%" ReadOnly="true" CssClass="TextBoxDisplay SmallFont"
                            Font-Size="XX-Small"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        Process Code
                    </td>
                    <td class="tdLeft" width="17%">
                        <cc2:ComboBox ID="ddlProsCode" runat="server" DataTextField="PROS_CODE" DataValueField="PROS_DESC"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="750px" OnLoadingItems="ddlProsCode_LoadingItems"
                            OnSelectedIndexChanged="ddlProsCode_SelectedIndexChanged" Width="98%" AutoPostBack="True">
                            <HeaderTemplate>
                                <div class="header c4">
                                    Process Code</div>
                                <div class="header c5">
                                    Process Description</div>
                                <div class="header c2">
                                    Product Type</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c4">
                                    <asp:Literal ID="Literal4" runat="server" Text='<%# Eval("PROS_CODE") %>' />
                                </div>
                                <div class="item c5">
                                    <asp:Literal ID="Container10" runat="server" Text='<%# Eval("PROS_DESC") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container11" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>' />
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
                    <td class="tdLeft" colspan="4" width="66%">
                        <asp:TextBox ID="txtProsDesc" runat="server" Width="99%" ReadOnly="true" CssClass="TextBoxDisplay SmallFont"
                            Font-Size="XX-Small"></asp:TextBox>
                        <asp:Label ID="lblProsCode" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        Process Id No
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtProsIdNo" runat="server" ReadOnly="true" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Font-Size="XX-Small" Width="99%"></asp:TextBox>
                        <cc2:ComboBox ID="ddlProsIdNo" runat="server" DataTextField="PROS_ID_NO" DataValueField="PROS_DATA"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="750px" OnLoadingItems="ddlProsIdNo_LoadingItems"
                            OnSelectedIndexChanged="ddlProsIdNo_SelectedIndexChanged" Width="99%" AutoPostBack="True"
                            Font-Size="XX-Small">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Process Id No</div>
                                <div class="header c2">
                                    Process Code</div>
                                <div class="header c3">
                                    Machine Code</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Literal5" runat="server" Text='<%# Eval("PROS_ID_NO") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container12" runat="server" Text='<%# Eval("PROS_CODE") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container13" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:RangeValidator ID="RangeValidator13" runat="server" ControlToValidate="txtProsIdNo"
                            Display="None" ErrorMessage="Please Enter Process Id No in Numeric" MaximumValue="999999999"
                            MinimumValue="0" Type="Integer" ValidationGroup="YM"></asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ControlToValidate="txtProsIdNo"
                            Display="None" ErrorMessage="Please Enter Pros Id No" SetFocusOnError="True"
                            ValidationGroup="YM"></asp:RequiredFieldValidator>
                    </td>
                    <td class="tdRight" width="17%">
                        Entry Date
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtEntryDate" runat="server" ReadOnly="true" CssClass="TextBoxDisplay SmallFont"
                           Width="99%" Font-Size="XX-Small"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        Shift
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlShift" Width="99%" runat="server" CssClass="SmallFont" 
                            Font-Size="XX-Small">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        Loading Time
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtLoadingDate" runat="server" AutoPostBack="True" OnTextChanged="txtLoadingDate_TextChanged"
                            CssClass="SmallFont" Font-Size="XX-Small" Width="99%"></asp:TextBox>
                        <cc1:MaskedEditExtender TargetControlID="txtLoadingDate" ID="meeLoadingDate" runat="server"
                            Mask="99/99/9999 99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="DateTime" CultureName="en-CA" AcceptAMPM="True" />
                        <cc1:MaskedEditValidator ID="mevLoadingDate" runat="server" ControlExtender="meeLoadingDate"
                            ControlToValidate="txtLoadingDate" IsValidEmpty="false" InvalidValueMessage="Invalid Loading Date"
                            Display="Dynamic" ValidationGroup="YM" />
                    </td>
                    <td class="tdRight" width="17%">
                        Unloading Time
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtUnLoadingDate" runat="server" AutoPostBack="True" OnTextChanged="txtUnLoadingDate_TextChanged"
                            CssClass="SmallFont" Font-Size="XX-Small" Width="99%"></asp:TextBox>
                        <cc1:MaskedEditExtender TargetControlID="txtUnLoadingDate" ID="meeUnLoadingDate"
                            runat="server" Mask="99/99/9999 99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="DateTime" CultureName="en-CA" AcceptAMPM="true" />
                        <cc1:MaskedEditValidator ID="mevUnLoadingDate" runat="server" ControlExtender="meeUnLoadingDate"
                            ControlToValidate="txtUnLoadingDate" IsValidEmpty="false" InvalidValueMessage="Invalid Un-Loading Date"
                            Display="Dynamic" ValidationGroup="YM" />
                    </td>
                    <td class="tdRight" width="17%">
                        Process Time
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtProcessTime" runat="server" ReadOnly="true" CssClass="TextBoxNo TextBoxDisplay"
                            Font-Size="XX-Small" Width="80%"></asp:TextBox>
                        Min.
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        Machine Stopage
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtMachineStopage" runat="server" AutoPostBack="True" OnTextChanged="txtMachineStopage_TextChanged"
                            CssClass="SmallFont"  Font-Size="XX-Small" Width="80px">0</asp:TextBox>Min.<asp:LinkButton ID="lbtnmacStopDetail" runat="server" OnClick="lbtnmacStopDetail_Click"
                            Text="Mac Stopage"  Font-Size="XX-Small"></asp:LinkButton>
                    </td>
                    <td class="tdRight" width="17%">
                        Operator
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtOperator" runat="server" Font-Size="XX-Small" Width="99%" CssClass="SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        Supervisor
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtSupervisor" runat="server" Font-Size="XX-Small" Width="99%" CssClass="SmallFont"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        Remarks
                    </td>
                    <td class="tdLeft" colspan="5" width="83%">
                        <asp:TextBox ID="txtRemarks" runat="server" Font-Size="XX-Small" Width="100%" CssClass="SmallFont"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr></tr>
    <tr>
        <td align="left" class="td" width="100%">
            Lot Detail
            <table width="100%">
                <tr>
                    <td class="tdRight" width="100%">
                        <table width="100%">
                            <tr bgcolor="#336699" class="SmallFont titleheading">
                                <td width="12%" class="tdLeft">
                                     Batch No
                                </td>
                                <td width="12%" class="tdLeft">
                                    PA No
                                </td>
                                <td width="15%" class="tdLeft">
                                    Order Description
                                </td>
                                <td width="15%" class="tdLeft">
                                    Grey Yarn Article Code
                                </td>
                                <%--   <td width="15%" class="tdLeft">
                                    Dyed Lot No
                                </td>--%>
                                <td>
                                    Pattern No.
                                </td>
                                <td width="8%" class="tdLeft">
                                    Load No Of Unit.
                                </td>
                                <td width="8%" class="tdLeft">
                                    Load UOM.
                                </td>
                                <td width="8%" class="tdLeft">
                                    Load Weight Of Unit.
                                </td>
                                <td width="8%" class="tdLeft">
                                    Load Qty.
                                </td>
                                <td width="8%" class="tdLeft">
                                    UnLoad No Of Unit.
                                </td>
                                <td width="8%" class="tdLeft">
                                    UnLoad UOM.
                                </td>
                                <td width="8%" class="tdLeft">
                                    UnLoad Weight Of Unit.
                                </td>
                                <td width="8%" class="tdRight">
                                    UnLoad Qty.
                                </td>
                                <td width="10%" class="tdLeft">
                                    To Location
                                </td>
                                <td width="8%" class="tdLeft">
                                    To Batch No
                                </td>
                                <%--      <td width="8%" class="tdRight">
                                    Loading Date Time
                                </td>
                                <td width="8%" class="tdLeft">
                                    Unloading Date Time
                                </td>--%>
                                <td width="11%" class="tdLeft">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="10%" class="tdLeft">
                                    <cc2:ComboBox ID="ddlLotNo" runat="server" DataTextField="LOT_NUMBER" DataValueField="LOT_DATA"
                                        EnableLoadOnDemand="true" Height="200px" MenuWidth="850px" OnLoadingItems="ddlLotNo_LoadingItems"
                                        OnSelectedIndexChanged="ddlLotNo_SelectedIndexChanged" Width="99%" AutoPostBack="True">
                                        <HeaderTemplate>
                                            <div class="header c4">
                                                Batch No</div>
                                            <div class="header c4">
                                                PA No</div>
                                            <div class="header c2">
                                                Department</div>
                                            <div class="header c2">
                                                Process Code</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c4">
                                                <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("LOT_NUMBER") %>' />
                                            </div>
                                            <div class="item c4">
                                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("ORDER_NO") %>' />
                                            </div>
                                            <div class="item c2">
                                                <asp:Literal ID="Container3" runat="server" Text='<%# Eval("DEPT_CODE") %>' />
                                            </div>
                                            <div class="item c2">
                                                <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("PROS_CODE") %>' />
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
                                <td width="12%" class="tdLeft">
                                    <asp:TextBox ID="txtLotQty" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtMaxLoadQty" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Visible="false"></asp:TextBox>
                                    <asp:TextBox ID="txtOrderNo" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Wrap="true"></asp:TextBox>
                                </td>
                                <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtOrderDescription" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Wrap="true"></asp:TextBox>
                                </td>
                                <td width="14%" class="tdLeft">
                                    <asp:TextBox ID="txtGreyArticleCode" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True" Wrap="true"></asp:TextBox>
                                    <asp:TextBox ID="txtDyedLot" runat="server" CssClass=" TextBox SmallFont" Width="99%"
                                        Wrap="true" Visible="false"></asp:TextBox>
                                    <asp:Label ID="lblPROS_CODE" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblBIN_LOCT" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblBATCH_NO" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                                <%--   <td width="14%" class="tdLeft">
                                
                                </td>--%>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtPattern" runat="server" CssClass=" TextBox SmallFont" Width="99%"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtLoadNoOfUnit" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"
                                        AutoPostBack="True" OnTextChanged="txtLoadNoOfUnit_TextChanged" Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:DropDownList ID="ddlLoadUOM" runat="server" Width="99%">
                                    </asp:DropDownList>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtLoadWeightOfUnit" runat="server" CssClass="TextBoxNo SmallFont"
                                        Width="99%" AutoPostBack="True" OnTextChanged="txtLoadWeightOfUnit_TextChanged"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtLoadQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                        Width="99%" ReadOnly="true" Wrap="true"></asp:TextBox>
                                        <asp:RangeValidator ID="r1" runat="server" ControlToValidate="txtLoadQty" ErrorMessage="*Enter Quantity"
                                MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtUnloadNoOfUnit" runat="server" CssClass="TextBoxNo SmallFont"
                                        Width="99%" AutoPostBack="True" OnTextChanged="txtUnloadNoOfUnit_TextChanged"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:DropDownList ID="ddlUnloadUOM" runat="server" Width="99%">
                                    </asp:DropDownList>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtUnloadWeightOfUnit" runat="server" CssClass="TextBoxNo SmallFont"
                                        Width="99%" AutoPostBack="True" OnTextChanged="txtUnloadWeightOfUnit_TextChanged"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdRight">
                                    <asp:TextBox ID="txtUnloadQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                        Width="99%" ReadOnly="true" Wrap="true"></asp:TextBox>
                                        <asp:RangeValidator ID="r2" runat="server" ControlToValidate="txtUnloadQty" ErrorMessage="*Enter Quantity"
                                MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtToLocation" runat="server" CssClass="TextBox SmallFont" Width="99%"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtToBatchNo" runat="server" CssClass="TextBox SmallFont" Width="99%"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <%-- <td width="8%" class="tdLeft">
                                    <asp:TextBox ID="txtLoadingDateTime" runat="server" CssClass="TextBox SmallFont"
                                        Width="99%"></asp:TextBox>
                                </td>
                                <td width="8%" class="tdLeft">
                                    <asp:TextBox ID="txtUnLoadingDateTime" runat="server" CssClass="TextBox SmallFont"
                                        Width="99%"></asp:TextBox>
                                </td>--%>
                                <td width="14%" class="tdLeft">
                                    <asp:Button ID="btnsaveLotDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveLotDetail_Click"
                                        Text="Save" ValidationGroup="T1" Width="50px" />
                                    <asp:Button ID="btnCancelLotDetail" runat="server" CssClass="SmallFont" OnClick="btnCancelLotDetail_Click"
                                        Text="Cancel" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <asp:Panel ID="pnlLotDetail" runat="server" Width="100%" Height="200px" ScrollBars="Auto">
                            <asp:GridView ID="grdLotDetail" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                OnRowCommand="grdLotDetail_RowCommand" Width="98%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Lot Number">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLotNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NUMBER") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PA No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ORDER_NO") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderDetail" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ORDER_DESC") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Dyed Lot No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDyedLotNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DYED_LOT_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Grey Yarn Article Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblARTICLE_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ARTICLE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pattern">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadNoOfUnit1" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PATTERN_NO") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Load No Of Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadNoOfUnit2" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_NO_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Load UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_UOM_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Load Weight Of Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadWeightOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_WEIGHT_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Load Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_QTY") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UnLoad No Of Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnLoadNoOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNLOAD_NO_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UnLoad UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnLoadUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNLOAD_UOM_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UnLoad Weight Of Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnLoadWeightOfUnit" runat="server" CssClass="LabelNo SmallFont"
                                                Text='<%# Bind("UNLOAD_WEIGHT_OF_UNIT") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UnLoad Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunLoadQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNLOAD_QTY") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunLoadQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TO_LOCATION") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Batch No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunLoadQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TO_BATCH_NO") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Loading Date Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunLoadQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_DATE") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Un Loading Date Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunLoadQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNLOAD_DATE") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_TRN") %>'
                                                CommandName="EditLotDetail" Text="Edit"></asp:LinkButton>/
                                            <asp:LinkButton ID="lnkbtnDel" runat="server" CommandArgument='<%# Bind("UNIQUE_TRN") %>'
                                                CommandName="DelLotDetail" Text="Delete"></asp:LinkButton></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                <RowStyle CssClass="SmallFont" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
  
</table>
