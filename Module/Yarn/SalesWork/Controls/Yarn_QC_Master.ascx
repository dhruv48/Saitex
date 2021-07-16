<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_QC_Master.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Yarn_QC_Master" %>
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
                                                OnClick="imgbtnSave_Click" TabIndex="17" />
                                        </td>
                                        <td id="tdUpdate" runat="server">
                                            <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                                OnClick="imgbtnUpdate_Click" TabIndex="17"></asp:ImageButton>
                                        </td>
                                        <td id="tdFind" runat="server">
                                            <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                                OnClick="imgbtnFind_Click" TabIndex="18"></asp:ImageButton>
                                        </td>
                                        <td id="tdPrint" runat="server">
                                            <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                                OnClick="imgbtnPrint_Click" TabIndex="19"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnList" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/list.jpg"
                                                OnClick="imgbtnList_Click" TabIndex="20"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                                OnClick="imgbtnClear_Click" TabIndex="21"></asp:ImageButton>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                                OnClick="imgbtnExit_Click" TabIndex="22"></asp:ImageButton>
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
                                <span class="titleheading"><b>Yarn QC Standard Master</b></span>
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
                            <td width="100%" class="td">
                                <table width="100%">
                                    <tr>
                                        <td width="17%" class="tdRight style2">
                                            <asp:Label ID="Label15" runat="server" Text="QC Number : " CssClass="LabelNo SmallFont"></asp:Label>
                                        </td>
                                        <td width="20%" class="tdLeft">
                                            <b>
                                                <asp:TextBox ID="txtTRNNUMBer" runat="server" ValidationGroup="M1" Width="180px"
                                                    TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true" Font-Bold="True"></asp:TextBox>
                                                <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                                    OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="combined"
                                                    EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                                                    Width="180px" Height="200px" MenuWidth="500px" TabIndex="1">
                                                    <HeaderTemplate>
                                                        <div class="header c1">
                                                            MRN #</div>
                                                        <div class="header c2">
                                                            Yarn Category</div>
                                                        <div class="header c2">
                                                            Yarn Code</div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' />
                                                        </div>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container5" Text='<%# Eval("YARN_CATEGORY") %>' />
                                                        </div>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container6" Text='<%# Eval("YARN_CODE") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                        out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc2:ComboBox>
                                            </b>
                                        </td>
                                        <td width="14%" class="tdRight style2">
                                            <asp:Label ID="Label16" runat="server" Text="Inward Type : " CssClass="Label SmallFont"></asp:Label>
                                        </td>
                                        <td width="17%" class="tdLeft">
                                            <b>
                                                <asp:DropDownList ID="ddlInwardType" runat="server" AutoPostBack="True" Width="150px"
                                                    CssClass="SmallFont" OnSelectedIndexChanged="ddlInwardType_SelectedIndexChanged"
                                                    TabIndex="2">
                                                </asp:DropDownList>
                                            </b>
                                        </td>
                                        <td width="17%" class="tdRight style2">
                                            <asp:Label ID="Label17" runat="server" Text="Yarn Category : " CssClass="Label SmallFont"></asp:Label>
                                        </td>
                                        <td width="17%" class="tdLeft">
                                            <b>
                                                <asp:DropDownList ID="ddlYarnCat" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="3" Width="150px" OnSelectedIndexChanged="ddlYarnCat_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="17%" class="tdRight">
                                            <asp:Label ID="Label10" runat="server" Text="Yarn Description :" CssClass="LabelNo SmallFont"></asp:Label>
                                        </td>
                                        <td width="20%" class="tdLeft">
                                            <cc2:ComboBox ID="ddlyarncode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                                DataTextField="YARN_CODE" DataValueField="Y_COUNT" EmptyText="Find Yarn Code"
                                                EnableLoadOnDemand="true" EnableVirtualScrolling="true" Height="200px" MenuWidth="800"
                                                OnLoadingItems="ddlyarncode_LoadingItems" OnSelectedIndexChanged="ddlyarncode_SelectedIndexChanged1"
                                                TabIndex="4" Width="100px">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        YARN CODE</div>
                                                    <div class="header c2">
                                                        YARN Description</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <asp:Literal ID="Container1" runat="server" Text='<%# Eval("YARN_CODE") %>' /></div>
                                                    <div class="item c2">
                                                        <asp:Literal ID="Container2" runat="server" Text='<%# Eval("YARN_DESC") %>' /></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>
                                            <asp:TextBox ID="txtYarnCode" runat="server" Width="80px" ReadOnly="true" Visible="true"
                                                CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                        </td>
                                        <td width="14%" class="tdRight">
                                            <asp:Label ID="Label11" runat="server" Text="Nominal Count :" CssClass="Label SmallFont"></asp:Label>
                                        </td>
                                        <td width="17%" class="tdLeft">
                                            <asp:TextBox ID="txtNominalCount" runat="server" TabIndex="5" Width="150px" ReadOnly="true"
                                                CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                        </td>
                                        <td width="17%" class="tdRight">
                                            <asp:Label ID="Label9" runat="server" Text="Total Imperfection:" CssClass="Label SmallFont"></asp:Label>
                                        </td>
                                        <td width="17%" class="tdLeft">
                                            <asp:TextBox ID="txtTotalImperfection" runat="server" TabIndex="6" Width="150px"
                                                ReadOnly="true" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="td" valign="top" width="100%">
                                <table width="100%">
                                    <tr bgcolor="#336699" class="SmallFont titleheading">
                                        <td class="HeaderRow">
                                            Std&nbsp;Type
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
                                        <td class="HeaderRow" id="tdheadUom" runat="server">
                                            UOM
                                        </td>
                                        <td class="HeaderRow">
                                            Remarks
                                        </td>
                                        <td class="HeaderRow">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:DropDownList runat="server" ID="ddlStdType" Width="100px" CssClass="Smallfont"
                                                TabIndex="7" AutoPostBack="True" OnSelectedIndexChanged="ddlStdType_SelectedIndexChanged" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtTolerance" runat="server" TabIndex="8" CssClass="TextBoxNo tdRight SmallFont"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList runat="server" ID="ddltoleranceType" Width="100px" CssClass="Smallfont"
                                                TabIndex="9" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList runat="server" ID="ddltolerancerange" Width="100px" CssClass="Smallfont"
                                                TabIndex="10" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtMaxValue" runat="server" TabIndex="11" CssClass="TextBoxNo tdRight SmallFont"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtMinValue" runat="server" TabIndex="12" CssClass="TextBoxNo tdRight SmallFont"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top" id="tdUom" runat="server">
                                            <cc2:ComboBox runat="server" ID="ddlUOM" Width="150px" CssClass="Smallfont" Height="180px"
                                                EmptyText="Select UOM..." EnableLoadOnDemand="true" OnLoadingItems="ddlUOM_LoadingItems"
                                                TabIndex="13" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtRemarks" runat="server" TabIndex="14" CssClass="TextBox SmallFont"
                                                Width="300px" MaxLength="200"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Button ID="btnsaveTRNDetail" runat="server" TabIndex="15" CssClass="SmallFont"
                                                Text="Add" Width="60px" OnClick="btnsaveTRNDetail_Click" ValidationGroup="M1" />
                                            <asp:Button ID="btnCancel" runat="server" TabIndex="16" CssClass="SmallFont" Text="Cancel"
                                                Width="60px" OnClick="btnCancel_Click" />
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
                                                    <asp:Label ID="lblTRN_NUMB" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SR_NO") %>'></asp:Label>
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
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                                        CommandArgument='<%# Bind("SR_NO") %>'></asp:LinkButton>
                                                    /
                                                    <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                                        CommandArgument='<%# Bind("SR_NO") %>'></asp:LinkButton>
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
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlStdType" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="btnsaveTRNDetail" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="None" runat="server"
    ErrorMessage="*Std type required" CssClass="RequiredField" ControlToValidate="ddlStdType"
    InitialValue="-1" ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Display="None" runat="server"
    ErrorMessage="*Tolerance required" CssClass="RequiredField" ControlToValidate="txtTolerance"
    ValidationGroup="M1"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTolerance"
    ValidationExpression="(^-?0\.[0-9]*[1-9]+[0-9]*$)|(^-?[1-9]+[0-9]*((\.[0-9]*[1-9]+[0-9]*$)|(\.[0-9]+)))|(^-?[1-9]+[0-9]*$)|(^0$){1}"
    ErrorMessage="Invalid No" ValidationGroup="M1">
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
<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMinValue"
    ValidationExpression="([0-9])[0-9]*[.]?[0-9]*" ErrorMessage="Invalid No" ValidationGroup="M1">
</asp:RegularExpressionValidator>