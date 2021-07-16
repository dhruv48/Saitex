<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvancedAdviceApproved.ascx.cs"
    Inherits="Module_FA_Controls_AdvancedAdviceApproved" %>
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
        width: 50px;
    }
    .c2
    {
        margin-left: 5px;
        width: 280px;
    }
    .c3
    {
        margin-left: 5px;
        width: 90px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFindTop_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <b class="titleheading">Advanced Advice Approval</b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="100%" class="td">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr id="trAdvice" runat="server" align="center">
                <td class="td">
                    Select Advanced Advice :
                    <cc2:ComboBox ID="cmbAdvancedAdvice" runat="server" Width="175px" Height="200px"
                        AutoPostBack="True" EnableLoadOnDemand="True" EmptyText="select Advice" DataTextField="ADV_NO"
                        DataValueField="ADV_NO" TabIndex="2" MenuWidth="600px" OnLoadingItems="cmbAdvancedAdvice_LoadingItems"
                        OnSelectedIndexChanged="cmbAdvancedAdvice_SelectedIndexChanged">
                        <HeaderTemplate>
                            <div class="header c3">
                                Advice No
                            </div>
                            <div class="header c3">
                                Advice Date</div>
                            <div class="header c2">
                                Party Name</div>
                            <div class="header c3">
                                Amount</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c3">
                                <%# Eval("ADV_NO")%></div>
                            <div class="item c3">
                                <%# Eval("ADV_DATE", "{0:dd-MM-yyyy}")%></div>
                            <div class="item c2">
                                <%# Eval("LDGR_NAME")%></div>
                            <div class="item c3">
                                <%# Eval("ADV_AMT")%></div>
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
            <tr id="trDate" runat="server">
                <td class="td" align="center">
                    <table style="width: 590px">
                        <tr>
                            <td>
                                Start Date :
                            </td>
                            <td>
                                <asp:TextBox ID="txtstartdate" runat="server" Width="80px"></asp:TextBox>
                            </td>
                            <td>
                                End Date :
                            </td>
                            <td>
                                <asp:TextBox ID="txtenddate" runat="server" Width="80px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trTotalRecord" runat="server">
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"
                        CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr id="trgrid" runat="server">
                <td align="left" class="td">
                    <asp:GridView ID="grdAdvancedAdviceApproval" CssClass="SmallFont" runat="server"
                        AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdAdvancedAdviceApproval_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Advice No">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdviceNo" runat="server" Text='<%# Bind("ADV_NO") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="LDGR_NAME" HeaderText="Ledger Name">
                                <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("ADV_AMT") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advice Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdviceDate" runat="server" Text='<%# Bind("ADV_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Description">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Approved">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkApproved" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtApprovedDate" runat="server" ReadOnly="true" Text='<%# Bind("APPR_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass=" TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved By">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtApprovedBy" runat="server" ReadOnly="true" Text='<%# Bind("APPR_BY") %>'
                                        ToolTip='<%# Bind("APPR_BY") %>' CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="SmallFont" Width="98%" />
                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtstartdate" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtenddate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtstartdate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtenddate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
