<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BatchCardEntryYS.ascx.cs" 
Inherits="Module_Prod_plan_Controls_BatchCardEntryYS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        margin-left: 4px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 120px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        width: 200px;
    }
    .c5
    {
        margin-left: 4px;
        width: 340px;
    }
    .c6
    {
        margin-left: 4px;
        width: 150px;
    }
</style>
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
        </td>
    </tr>
    <tr>
        <td class="TableHeader" class="td" align="center" width="100%">
            <b class="titleheading">Batch Card Entry</b>
        </td>
    </tr>
    <tr>
        <td class="td" valign="top" align="left" width="100%">
            <span style="color: #ff0000">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
            </asp:Label>&nbsp;Mode</span>
        </td>
    </tr>
    <tr>
        <td class="td" align="left" width="100%">
            <table align="left" width="100%">
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblBatchCode" runat="server" CssClass="SmallFont" Text="Batch Code"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtBatchCode" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                        <cc2:ComboBox ID="ddlBatchCode" runat="server" TabIndex="1" Width="100%" MenuWidth="800"
                            AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                            Height="200px" EnableVirtualScrolling="true" OnLoadingItems="ddlBatchCode_LoadingItems"
                            OnSelectedIndexChanged="ddlBatchCode_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Batch Code</div>
                                <div class="header c1">
                                    Batch Date</div>
                                <div class="header c1">
                                    Pa No</div>
                                <div class="header c2">
                                    Lot No
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("BATCH_CODE")%></div>
                                <div class="item c1">
                                    <%# Eval("BATCH_DATE")%></div>
                                <div class="item c2">
                                    <%# Eval("PA_NO")%></div>
                                <div class="item c1">
                                    <%# Eval("LOT_NUMBER")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblBatchDate" runat="server" Text="Batch Date"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtBatchDate" CssClass="TextBox SmallFont" Width="100%" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceBatchDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBatchDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblPaNo0" runat="server" Text="Order No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtOrderNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblPaNo" runat="server" Text="Select PA No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <cc2:ComboBox ID="ddlPaNo" runat="server" TabIndex="1" Width="100%" MenuWidth="800"
                            AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                            Height="200px" EnableVirtualScrolling="true" OnLoadingItems="ddlPaNo_LoadingItems"
                            OnSelectedIndexChanged="ddlPaNo_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Order No</div>
                                <div class="header c1">
                                    Pa No</div>
                                <div class="header c2">
                                    Lot No
                                </div>
                                <div class="header c1">
                                    Party Code
                                </div>
                                <div class="header c4">
                                    Party Name
                                </div>
                                <div class="header c1">
                                    Lot Qty
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("ORDER_NO")%></div>
                                <div class="item c1">
                                    <%# Eval("PI_NO")%></div>
                                <div class="item c2">
                                    <%# Eval("LOT_ID_NO")%></div>
                                <div class="item c1">
                                    <%# Eval("PRTY_CODE")%></div>
                                <div class="item c4">
                                    <%# Eval("PRTY_NAME")%></div>
                                <div class="item c1">
                                    <%# Eval("TRN_QTY")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblPaNo1" runat="server" Text="PA No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtPaNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblLotNo" runat="server" Text="Lot No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtLotNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblParty" runat="server" Text="Party"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtParty" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" colspan="2">
                        <asp:TextBox ID="txtPartDtl" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblLotSize" runat="server" Text="Lot Size"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtLotSize" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblArticle" runat="server" Text="Article"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtArticle" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%" colspan="2" style="width: 34%">
                        <asp:TextBox ID="txtArticleDesc" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblShade" runat="server" Text="Shade"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtShade" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        &nbsp;<asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                    </td>
                    <td class="tdLeft" colspan="5">
                        <asp:TextBox ID="txtRemarks" CssClass="TextBox SmallFont" Width="100%" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" valign="top" width="100%">
            <table width="98%">
                <tr bgcolor="#006699">
                    <td>
                        <span class="titleheading"><b>Select Process </b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Sr No </b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Department</b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Main Process </b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Process Code</b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Machine Group</b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Machine Code</b></span>
                    </td>
                    <td align="left" valign="top">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <cc2:ComboBox ID="ddlProcessCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                            DataTextField="PROS_CODE" Width="100%" DataValueField="PROS_CODE" EnableLoadOnDemand="true"
                            MenuWidth="860px" OnLoadingItems="ddlProcessCode_LoadingItems" OnSelectedIndexChanged="ddlProcessCode_SelectedIndexChanged"
                            Height="200px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="9"
                            Visible="true">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Department</div>
                                <div class="header c1">
                                    Main Process</div>
                                <div class="header c2">
                                    Pros Code
                                </div>
                                <div class="header c1">
                                    Pros Desc
                                </div>
                                <div class="header c4">
                                    Mac Group
                                </div>
                                <div class="header c1">
                                    Mac Code
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("DEPT_NAME")%></div>
                                <div class="item c1">
                                    <%# Eval("MAIN_PROCESS")%></div>
                                <div class="item c2">
                                    <%# Eval("PROS_CODE")%></div>
                                <div class="item c1">
                                    <%# Eval("PROS_DESC")%></div>
                                <div class="item c4">
                                    <%# Eval("MAC_GROUP_CODE")%></div>
                                <div class="item c1">
                                    <%# Eval("MACHINE_CODE")%></div>
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
                        <asp:TextBox ID="txtSrNo" CssClass="TextBoxNo SmallFont" Width="100%" runat="server"
                            ValidationGroup="S1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvSNo" runat="server" ControlToValidate="txtSrNo"
                            ErrorMessage="invalid Sr No" SetFocusOnError="True" ValidationGroup="S1" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rvSNo" runat="server" ControlToValidate="txtSrNo" ErrorMessage="Sr No out of range"
                            MaximumValue="10000" MinimumValue="1" SetFocusOnError="True" Type="Integer" ValidationGroup="S1"
                            Display="Dynamic"></asp:RangeValidator>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtDepartment" CssClass="TextBox TextBoxDisplay SmallFont" Width="45%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtDepartmentName" CssClass="TextBox TextBoxDisplay SmallFont" Width="48%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtMainProcess" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtProsCode" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtMacGroup" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtMacCode" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:Button ID="lbtnsavedetail" Text="Save" runat="server" TabIndex="17" OnClick="lbtnsavedetail_Click"
                            Width="60px" ValidationGroup="S1"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" colspan="7">
                        <asp:Label ID="lblProsDesc" runat="server" Text="Process Desc" CssClass="TextBoxNo"
                            Width="15%"></asp:Label>
                        <asp:TextBox ID="txtProcessDesc" CssClass="TextBox TextBoxDisplay SmallFont" Width="80%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:Button ID="lbtnCancel" Text="Cancel" runat="server" TabIndex="18" OnClick="lbtnCancel_Click1"
                            Width="60px"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trGridView" runat="server">
        <td class="td" align="left" width="100%">
            <asp:Panel ID="pnlGrid" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">
                <asp:GridView ID="grdBatchTrn" runat="server" CssClass="SmallFont" Font-Bold="False"
                    OnRowCommand="grdBatchTrn_RowCommand" ShowFooter="false" BorderWidth="1px" AutoGenerateColumns="False"
                    AllowSorting="True" Width="98%">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtSR_NO" runat="server" Text='<%# Bind("SR_NO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtDEPT_NAME" runat="server" Text='<%# Bind("DEPT_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Main Process">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtMAIN_PROCESS" runat="server" Text='<%# Bind("MAIN_PROCESS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process Code">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtPROS_CODE" runat="server" Text='<%# Bind("PROS_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process Desc">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtPROS_DESC" runat="server" Text='<%# Bind("PROS_DESC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Machine Group">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtMACHINE_GROUP" runat="server" Text='<%# Bind("MACHINE_GROUP") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Machine Code">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="txtMACHINE_CODE" runat="server" Text='<%# Bind("MACHINE_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Button ID="lnkDelete" runat="server" Text="Delete" CommandName="DelBatchTrn"
                                    CommandArgument='<%# Bind("PROS_CODE") %>' TabIndex="12"></asp:Button>
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
