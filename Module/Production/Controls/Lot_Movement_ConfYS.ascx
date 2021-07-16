<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Lot_Movement_ConfYS.ascx.cs" Inherits="Module_Production_Controls_Lot_Movement_ConfYS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" visible="false" align="left">
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
            <b class="titleheading">
                <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont">Lot 
            Movement Confirmation</asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode</span></td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;:&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:Panel ID="pnlLotMov" runat="server" Width="100%" Height="500px" ScrollBars="Both">
                <asp:GridView ID="grdLOT_MOV_CONF" CssClass="SmallFont" runat="server" AllowSorting="True"
                    AutoGenerateColumns="False" OnRowDataBound="grdLOT_MOV_CONF_RowDataBound" Width="98%">
                    <Columns>
                        <asp:TemplateField HeaderText="Entry Dept.">
                            <ItemTemplate>
                                <asp:Label ID="lblDeptCode" runat="server" Text='<%# Bind("DEPT_CODE") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblDeptName" runat="server" Text='<%# Bind("DEPT_NAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Entry No">
                            <ItemTemplate>
                                <asp:Label ID="lblEntryNo" runat="server" Text='<%# Bind("ENTRY_NO") %>'></asp:Label>
                                <asp:Label ID="lblEntryType" runat="server" Text='<%# Bind("ENTRY_TYPE") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Entry Date">
                            <ItemTemplate>
                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("ENTRY_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lot No">
                            <ItemTemplate>
                                <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LOT_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order No">
                            <ItemTemplate>
                                <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("FR_ORDERNO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%# Bind("FR_MOVE_QTY") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdRight" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process Code">
                            <ItemTemplate>
                                <asp:Label ID="lblProcessCode" runat="server" Text='<%# Bind("FR_PROS_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From Department">
                            <ItemTemplate>
                                <asp:Label ID="lblFromDept" runat="server" Text='<%# Bind("FR_DEPT_CODE") %>' Visible="false"></asp:Label>
                               <asp:Label ID="lblFromDeptName" runat="server" Text='<%# Bind("FR_DEPT_NAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Department">
                            <ItemTemplate>
                                <asp:Label ID="lblToDept" runat="server" Text='<%# Bind("TO_DEPT_CODE") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblToDeptName" runat="server" Text='<%# Bind("TO_DEPT_NAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Location" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblToLacation" runat="server" Text='<%# Bind("TO_BRANCH_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Batch No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblBatchNo" runat="server" Text='<%# Bind("TO_BATCH_NO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="SmallFont tdLeft" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Confirm">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkConf" runat="server"  OnCheckedChanged="chkConf_CheckedChanged" /><%--AutoPostBack="true"--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reject" Visible="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkReject" runat="server" AutoPostBack="true" OnCheckedChanged="chkReject_CheckedChanged" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" Width="5%"/>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" Width="98%" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>
