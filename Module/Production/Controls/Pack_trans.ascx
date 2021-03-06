<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pack_trans.ascx.cs" Inherits="Module_Production_Controls_Pack_trans" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../../../../javascript/jquery-1.4.1.min.js" type="text/javascript"></script>

<script src="../../../../javascript/ScrollableGrid.js" type="text/javascript"></script>

<script type="text/javascript">
    //    $(document).ready(function() {
    //        jQuery('table').Scrollable(400, 800);
    //    });

    $(document).ready(function() {
        $('#<%=grdFGTrans.ClientID %>').Scrollable();
    }
)
</script>

<style type="text/css">
    .HideControl
    {
        visibility: hidden;
    }
    .pager span
    {
        color: #009900;
        font-weight: bold;
        font-size: 16pt;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial" width="100%">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
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
                    <b class="titleheading">Packed Goods transfer to FG</b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <asp:Panel ID="pnlTree" runat="server" ScrollBars="Both" Height="400px" Width="970px">
                        <asp:GridView ID="grdFGTrans" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                            Width="100%" OnRowCommand="grdFGTrans_RowCommand" AllowPaging="True" PagerStyle-CssClass="pager"
                            OnPageIndexChanging="grdFGTrans_PageIndexChanging" PageSize="12">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Packing ID">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkTransfer" runat="server" OnCheckedChanged="chkTransfer_CheckedChanged"
                                            AutoPostBack="True" ToolTip='<%# Bind("PACKING_ID") %>' Text='<%# Bind("PACKING_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Pck.Date" DataField="PACKING_DATE" />
                                <asp:BoundField HeaderText="pck.Cat." DataField="PACKING_CAT" />
                                <asp:BoundField HeaderText="Order No" DataField="ORDER_NO" />
                                <asp:BoundField HeaderText="PA No" DataField="PI_NO" />
                                <asp:BoundField HeaderText="Lot No" DataField="LOT_ID_NO" />
                                <asp:BoundField HeaderText="Article" DataField="ARTICAL_CODE" />
                                <asp:BoundField HeaderText="Shade" DataField="SHADE_CODE" />
                                <asp:BoundField HeaderText="Pck.Lot No" DataField="PACK_LOT_NO" />
                                <asp:BoundField HeaderText="Units" DataField="NO_OF_UNIT" ItemStyle-CssClass="tdRight" />
                                <asp:BoundField HeaderText="UOM of Unit" DataField="UOM_OF_UNIT" />
                                <asp:BoundField HeaderText="Unit Weight" DataField="WEIGHT_OF_UNIT" ItemStyle-CssClass="tdRight" />
                                <asp:BoundField HeaderText="Production(KG.)" DataField="PRODUCTION" ItemStyle-CssClass="tdRight" />
                            </Columns>
                            <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
