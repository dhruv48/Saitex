<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Lot_Movemement_Conf.ascx.cs"
    Inherits="Module_OrderDevelopment_Controls_Lot_Movemement_Conf" %>
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
                            ImageUrl="~/CommonImages/link_find.png" onclick="imgbtnFindTop_Click"></asp:ImageButton>
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
            <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
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
                    AutoGenerateColumns="False" OnRowDataBound="grdLOT_MOV_CONF_RowDataBound" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Entry No" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblEntryNo" runat="server" Text='<%# Bind("ENTRY_NO") %>'></asp:Label>
                                <asp:Label ID="lblEntryType" runat="server" Text='<%# Bind("ENTRY_TYPE") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                          
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Entry Date" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("ENTRY_DATE") %>'></asp:Label>
                            </ItemTemplate>
                        
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lot No" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblLotNo" runat="server" Text='<%# Bind("LOT_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                          
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order No" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblOrderNo" runat="server" Text='<%# Bind("FR_ORDERNO") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%# Bind("FR_MOVE_QTY") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process Code" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblProcessCode" runat="server" Text='<%# Bind("FR_PROS_CODE") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="From Department" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblFromDept" runat="server" Text='<%# Bind("FR_DEPT_CODE") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Department" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblToDept" runat="server" Text='<%# Bind("TO_DEPT_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="To Location" Visible="false" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblToLacation" runat="server" Text='<%# Bind("TO_BRANCH_CODE") %>'></asp:Label>
                            </ItemTemplate>
                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Batch No" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblBatchNo" runat="server" Text='<%# Bind("TO_BATCH_NO") %>'></asp:Label>
                            </ItemTemplate>
                         
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Confirm" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkConf" runat="server" AutoPostBack="false" OnCheckedChanged="chkConf_CheckedChanged" />
                            </ItemTemplate>
                          
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reject" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkReject" runat="server" AutoPostBack="false" OnCheckedChanged="chkReject_CheckedChanged" />
                            </ItemTemplate>
                          
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" Width="100%" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>
