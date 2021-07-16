<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Production.ascx.cs" Inherits="Module_Production_Controls_Production" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
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
                <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont">Production Entry Form</asp:Label></b>
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
                    <td class="tdRight" width="20%">
                        Machine Code
                    </td>
                    <td class="tdLeft" width="20%">
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
                    <td class="tdLeft" width="60%">
                        <asp:TextBox ID="txtMachineDesc" runat="server" Width="99%" ReadOnly="true" CssClass="TextBoxDisplay"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="20%">
                        Process Code
                    </td>
                    <td class="tdLeft" width="20%">
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
                    <td class="tdLeft" width="60%">
                        <asp:TextBox ID="txtProsDesc" runat="server" Width="99%" ReadOnly="true" CssClass="TextBoxDisplay"></asp:TextBox>
                        <asp:Label ID="lblProsCode" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="20%">
                        Process Id No
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtProsIdNo" runat="server" ReadOnly="true" CssClass="TextBoxNo TextBoxDisplay"></asp:TextBox>
                        <cc2:ComboBox ID="ddlProsIdNo" runat="server" DataTextField="PROS_ID_NO" DataValueField="PROS_DATA"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="750px" OnLoadingItems="ddlProsIdNo_LoadingItems"
                            OnSelectedIndexChanged="ddlProsIdNo_SelectedIndexChanged" Width="150px" AutoPostBack="True">
                            <HeaderTemplate>
                                <div class="header c4">
                                    Process Id No</div>
                                <div class="header c5">
                                    Process Code</div>
                                <div class="header c2">
                                    Machine Code</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c4">
                                    <asp:Literal ID="Literal5" runat="server" Text='<%# Eval("PROS_ID_NO") %>' />
                                </div>
                                <div class="item c5">
                                    <asp:Literal ID="Container12" runat="server" Text='<%# Eval("PROS_CODE") %>' />
                                </div>
                                <div class="item c2">
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
                    </td>
                    <td class="tdRight" width="15%">
                        Entry Date
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtEntryDate" runat="server" ReadOnly="true" CssClass="TextBoxDisplay"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        Shift
                    </td>
                    <td class="tdLeft" width="20%">
                        <cc3:OboutDropDownList ID="ddlShift" runat="server">
                        </cc3:OboutDropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="20%">
                        Loading Time
                    </td>
                    <td class="tdLeft" width="20%">
                        <asp:TextBox ID="txtLoadingDate" runat="server" AutoPostBack="True" OnTextChanged="txtLoadingDate_TextChanged"></asp:TextBox>
                        <cc1:MaskedEditExtender TargetControlID="txtLoadingDate" ID="meeLoadingDate" runat="server"
                            Mask="99/99/9999 99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="DateTime" CultureName="en-CA" AcceptAMPM="True" />
                        <cc1:MaskedEditValidator ID="mevLoadingDate" runat="server" ControlExtender="meeLoadingDate"
                            ControlToValidate="txtLoadingDate" IsValidEmpty="false" InvalidValueMessage="Invalid Loading Date"
                            Display="Dynamic" />
                    </td>
                    <td class="tdRight" width="20%">
                        Unloading Time
                    </td>
                    <td class="tdLeft" width="40%">
                        <asp:TextBox ID="txtUnLoadingDate" runat="server" AutoPostBack="True" OnTextChanged="txtUnLoadingDate_TextChanged"></asp:TextBox>
                        <cc1:MaskedEditExtender TargetControlID="txtUnLoadingDate" ID="meeUnLoadingDate"
                            runat="server" Mask="99/99/9999 99:99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="DateTime" CultureName="en-CA" AcceptAMPM="true" />
                        <cc1:MaskedEditValidator ID="mevUnLoadingDate" runat="server" ControlExtender="meeUnLoadingDate"
                            ControlToValidate="txtUnLoadingDate" IsValidEmpty="false" InvalidValueMessage="Invalid Un-Loading Date"
                            Display="Dynamic" />
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="20%">
                        Machine Stopage
                    </td>
                    <td class="tdLeft" width="20%">
                        <asp:TextBox ID="txtMachineStopage" runat="server" AutoPostBack="True" OnTextChanged="txtMachineStopage_TextChanged"></asp:TextBox>Minutes
                    </td>
                    <td class="tdRight" width="20%">
                        Process Time
                    </td>
                    <td class="tdLeft" width="40%">
                        <asp:TextBox ID="txtProcessTime" runat="server" ReadOnly="true" CssClass="TextBoxNo TextBoxDisplay"></asp:TextBox>
                        Minutes
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="20%">
                        Operator
                    </td>
                    <td class="tdLeft" width="20%">
                        <asp:TextBox ID="txtOperator" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="20%">
                        Supervisor
                    </td>
                    <td class="tdLeft" width="40%">
                        <asp:TextBox ID="txtSupervisor" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="20%">
                        Remarks
                    </td>
                    <td class="tdLeft" width="80%">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            Lot Detail
            <table width="100%">
                <tr>
                    <td class="tdRight" width="100%">
                        <table width="100%">
                            <tr bgcolor="#336699" class="SmallFont titleheading">
                                <td width="12%" class="tdLeft">
                                    Lot No
                                </td>
                                <td width="12%" class="tdLeft">
                                    Order No
                                </td>
                                <td width="15%" class="tdLeft">
                                    Order Description
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
                                        EnableLoadOnDemand="true" Height="200px" MenuWidth="750px" OnLoadingItems="ddlLotNo_LoadingItems"
                                        OnSelectedIndexChanged="ddlLotNo_SelectedIndexChanged" Width="150px" AutoPostBack="True">
                                        <HeaderTemplate>
                                            <div class="header c4">
                                                Lot No</div>
                                            <div class="header c5">
                                                Order No</div>
                                            <div class="header c2">
                                                Process
                                            </div>
                                            <div class="header c3">
                                                Department</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c4">
                                                <asp:Literal ID="Literal2" runat="server" Text='<%# Eval("LOT_NUMBER") %>' />
                                            </div>
                                            <div class="item c5">
                                                <asp:Literal ID="Container1" runat="server" Text='<%# Eval("ORDER_NO") %>' />
                                            </div>
                                            <div class="item c2">
                                                <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PROS_CODE") %>' />
                                            </div>
                                            <div class="item c3">
                                                <asp:Literal ID="Container3" runat="server" Text='<%# Eval("DEPT_CODE") %>' />
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
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtLoadNoOfUnit" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"
                                        AutoPostBack="True" OnTextChanged="txtLoadNoOfUnit_TextChanged" Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <cc3:OboutDropDownList ID="ddlLoadUOM" runat="server" Width="99%">
                                    </cc3:OboutDropDownList>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtLoadWeightOfUnit" runat="server" CssClass="TextBoxNo SmallFont"
                                        Width="99%" AutoPostBack="True" OnTextChanged="txtLoadWeightOfUnit_TextChanged"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtLoadQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                        Width="99%" ReadOnly="true" Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtUnloadNoOfUnit" runat="server" CssClass="TextBoxNo SmallFont"
                                        Width="99%" AutoPostBack="True" OnTextChanged="txtUnloadNoOfUnit_TextChanged"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <cc3:OboutDropDownList ID="ddlUnloadUOM" runat="server" Width="99%">
                                    </cc3:OboutDropDownList>
                                </td>
                                <td width="5%" class="tdLeft">
                                    <asp:TextBox ID="txtUnloadWeightOfUnit" runat="server" CssClass="TextBoxNo SmallFont"
                                        Width="99%" AutoPostBack="True" OnTextChanged="txtUnloadWeightOfUnit_TextChanged"
                                        Wrap="true"></asp:TextBox>
                                </td>
                                <td width="5%" class="tdRight">
                                    <asp:TextBox ID="txtUnloadQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                        Width="99%" ReadOnly="true" Wrap="true"></asp:TextBox>
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
                                    <asp:TemplateField HeaderText="Order No">
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
                                    <asp:TemplateField HeaderText="Load No Of Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLoadNoOfUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOAD_NO_OF_UNIT") %>'></asp:Label></ItemTemplate>
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
    <tr>
        <td align="left" class="td" width="100%">
            Color Chemical Detail
            <table width="100%">
                <tr>
                    <td class="tdRight" width="100%">
                        <table width="100%">
                            <tr bgcolor="#336699" class="SmallFont titleheading">
                                <td width="12%" class="tdLeft">
                                    Item Code
                                </td>
                                <td width="15%" class="tdLeft">
                                    Item Description
                                </td>
                                <td width="10%" class="tdLeft">
                                    Item Rate
                                </td>
                                <td width="10%" class="tdLeft">
                                    Item Qty
                                </td>
                                <td width="8%" class="tdRight">
                                    Basis
                                </td>
                                <td width="10%" class="tdLeft">
                                    Tube Qty
                                </td>
                                <td width="8%" class="tdLeft">
                                    Expr
                                </td>
                                <td width="8%" class="tdRight">
                                    Unit Qty
                                </td>
                                <td width="8%" class="tdLeft">
                                    Density
                                </td>
                                <td width="11%" class="tdLeft">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="12%" class="tdLeft">
                                    <cc2:ComboBox ID="ddlItem" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="ITEM_CODE" DataValueField="ITEM_DESC" EmptyText="Find Item" EnableLoadOnDemand="true"
                                        Height="200px" MenuWidth="350px" OnLoadingItems="ddlItem_LoadingItems" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"
                                        TabIndex="1" Width="100px">
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
                                                <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ITEM_CODE") %>' />
                                            </div>
                                            <div class="item c2">
                                                <asp:Literal ID="Container5" runat="server" Text='<%# Eval("ITEM_DESC") %>' />
                                            </div>
                                            <div class="item c3">
                                                <asp:Literal ID="Container6" runat="server" Text='<%# Eval("ITEM_TYPE") %>' />
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items - out of .
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                </td>
                                <td width="15%" class="tdLeft">
                                    <asp:TextBox ID="txtItemDesc" runat="server" CssClass="TextBoxDisplay TextBox SmallFont"
                                        Width="99%" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td width="10%" class="tdLeft">
                                    <asp:TextBox ID="txtItemRate" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"></asp:TextBox>
                                </td>
                                <td width="10%" class="tdLeft">
                                    <asp:TextBox ID="txtItemQty" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"></asp:TextBox>
                                </td>
                                <td width="8%" class="tdRight">
                                    <asp:TextBox ID="txtBasis" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                                </td>
                                <td width="10%" class="tdLeft">
                                    <asp:TextBox ID="txtTubeQty" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"></asp:TextBox>
                                </td>
                                <td width="8%" class="tdLeft">
                                    <asp:TextBox ID="txtExpr" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                                </td>
                                <td width="8%" class="tdLeft">
                                    <asp:TextBox ID="txtUnitQty" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"></asp:TextBox>
                                </td>
                                <td width="8%" class="tdLeft">
                                    <asp:TextBox ID="txtDensity" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                                </td>
                                <td width="11%" class="tdLeft">
                                    <asp:Button ID="btnSaveItemDetail" runat="server" CssClass="SmallFont" OnClick="btnSaveItemDetail_Click"
                                        Text="Save" ValidationGroup="T1" />
                                    <asp:Button ID="btnCancelItemDetail" runat="server" CssClass="SmallFont" OnClick="btnCancelItemDetail_Click"
                                        Text="Cancel" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="td" width="100%">
                        <asp:Panel ID="pnlItemDetail" runat="server" Width="100%" Height="200px" ScrollBars="Auto">
                            <asp:GridView ID="grdItemDetail" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                OnRowCommand="grdItemDetail_RowCommand" Width="98%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ITEM_CODE") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemDesc" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemRate" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_RATE") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ITEM_QTY") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Basis">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemBasis" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("BASIS") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tub Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemTubQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TUB_QTY") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expression">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemExpr" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("EXPR") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemUnitQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UNIT_QTY") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Density">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemDensity" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DENSITY") %>'></asp:Label></ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_CHEM") %>'
                                                CommandName="EditItemDetail" Text="Edit"></asp:LinkButton>/
                                            <asp:LinkButton ID="lnkbtnDel" runat="server" CommandArgument='<%# Bind("UNIQUE_CHEM") %>'
                                                CommandName="DelItemDetail" Text="Delete"></asp:LinkButton></ItemTemplate>
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
