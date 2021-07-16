<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReceiptApproval.ascx.cs"
    Inherits="Module_Inventory_Controls_ReceiptApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" visible="false" align="left">
                        <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
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
            <b class="titleheading">Material Receipt Approval</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="center" width="100%" class="td">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:GridView ID="gvMaterialReceiptApproval" CssClass="SmallFont" runat="server"
                AllowSorting="True" AutoGenerateColumns="False" 
                OnRowDataBound="gvMaterialReceiptApproval_RowDataBound" Width="95%"   
                AllowPaging="true" PageSize="20" 
                onpageindexchanging="gvMaterialReceiptApproval_PageIndexChanging" >
                <Columns>
                    <asp:BoundField DataField="YEAR" HeaderText="YEAR" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right" Visible="false">
                        <ItemStyle HorizontalAlign="Left" CssClass="labelNo smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="MRN Date" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblTRN_DATE" runat="server" CssClass="LabelNo" ToolTip='<%# Bind("YEAR") %>'
                                Text='<%# Bind("TRN_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>                           
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="TRN_DESC" HeaderText="MRN Type">
                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="MRN No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblTRN_NUMB" runat="server" CssClass="LabelNo" ToolTip='<%# Bind("TRN_NUMB") %>'
                                Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>' ToolTip='<%# Bind("YEAR") %>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PARTY_DATA" HeaderText="Party">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRTY_CH_NUMB" HeaderText="Party Challan No" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GATE_NUMB" HeaderText="Gate Numb" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" ReadOnly="true" Text='<%# Bind("CONF_DATE") %>'
                                CssClass=" TextBox TextBoxDisplay SmallFont" Width="55px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmBy" runat="server" ReadOnly="true" Text='<%# Bind("CONF_BY") %>'
                                ToolTip='<%# Bind("CONF_BY") %>' CssClass="TextBox TextBoxDisplay SmallFont" Width="80px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnViewTRN" runat="server" Text="View Details"></asp:LinkButton>
                            <asp:Panel ID="pnlTRN" runat="server" Width="470px" BackColor="Beige" BorderWidth="2px"
                                Height="140px" ScrollBars="Auto">
                                <asp:GridView ID="grdTRN" runat="server" AutoGenerateColumns="False" Width="450px"
                                    CssClass="SmallFont" Height="140px">
                                    <Columns>
                                        <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Description">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HSN_CODE" HeaderText="HSN Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TRN_QTY" HeaderText="Qty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FINAL_RATE" HeaderText="Final Rate" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AMOUNT" HeaderText="Value" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REMARKS" HeaderText="Comments">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                </asp:GridView>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="hmeTRN" runat="server" PopupPosition="Left" PopupControlID="pnlTRN"
                                TargetControlID="lbtnViewTRN">
                            </cc1:HoverMenuExtender>
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
