<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PurchaseOrderQuery.ascx.cs"
    Inherits="Module_Inventory_Queries_WebUserControl" %>
<%@ Register Src="../../../CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table width="100%" class="td tContentArial">
    <tr>
        <td class="td tContentArial">
            <table>
                <tr>
                    <td id="tdClear" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                    </td>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center" valign="top" class="TableHeader">
                        <span class="titleheading">Purchase Order Querys</span>
                    </td>
                </tr>
            </table>
            <table class="td tContentArial">
                <tr>
                    <td align="right" valign="top">
                        Party Code :
                    </td>
                    <td align="left" valign="top">
                        <uc1:PartyCodeLOV ID="ddlprtycode" runat="server" TabIndex="4" Width="75px" />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        To :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="TxtTdate" runat="server" TabIndex="4" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        Order Date :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="TxtFdate" runat="server" TabIndex="5" CssClass="TextBox"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td align="left" valign="top">
                        <asp:Button ID="btnview" runat="server" Text="Show" OnClick="btnview_Click1" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <b>
                            <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                            <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="panal15" runat="server" ScrollBars="Both" Width="1050px" Height="300px">
                            <asp:GridView ID="purchaseOrderGrid" runat="server" Width="300%" Font-Size="10px"
                                AutoGenerateColumns="False" CellPadding="4" AllowPaging="false" GridLines="None"
                                ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Company Code">
                                        <ItemTemplate>
                                            <asp:Label ID="txtcomp_code" runat="server" CssClass="Label" Text='<%# Bind("COMP_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Branch Code">
                                        <ItemTemplate>
                                            <asp:Label ID="txtbranch_code" runat="server" CssClass="Label" Text='<%# Bind("BRANCH_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Po Numb">
                                        <ItemTemplate>
                                            <asp:Label ID="txtpo_numb" runat="server" CssClass="Label" Text='<%# Bind("PO_NUMB") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Type">
                                        <ItemTemplate>
                                            <asp:Label ID="txtpo_type" runat="server" CssClass="Label" Text='<%# Bind("PO_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Po Nature">
                                        <ItemTemplate>
                                            <asp:Label ID="txtpo_nature" runat="server" CssClass="Label" Text='<%# Bind("PO_NATURE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PO Date">
                                        <ItemTemplate>
                                            <asp:Label ID="txtpo_dt" runat="server" CssClass="Label" Text='<%# Bind("PO_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Party Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txtparty_name" runat="server" CssClass="Label" Text='<%# Bind("PRTY_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delevery Date">
                                        <ItemTemplate>
                                            <asp:Label ID="txtdelv_dt" runat="server" CssClass="Label" Text='<%# Bind("DEL_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pay Term">
                                        <ItemTemplate>
                                            <asp:Label ID="txtpay_term" runat="server" CssClass="Label" Text='<%# Bind("PAY_TERM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirmation">
                                        <ItemTemplate>
                                            <asp:Label ID="txtconf_flag" runat="server" CssClass="Label" Text='<%# Bind("CONF_FLAG") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Po Status">
                                        <ItemTemplate>
                                            <asp:Label ID="txtpo_status" runat="server" CssClass="Label" Text='<%# Bind("PO_STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transport Code">
                                        <ItemTemplate>
                                            <asp:Label ID="txttrsp_code" runat="server" CssClass="Label" Text='<%# Bind("TRSP_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delevery Branch">
                                        <ItemTemplate>
                                            <asp:Label ID="txtdelv_branch" runat="server" CssClass="Label" Text='<%# Bind("DELV_BRANCH") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Desp Mode">
                                        <ItemTemplate>
                                            <asp:Label ID="txtdesp_code" runat="server" CssClass="Label" Text='<%# Bind("DESP_MODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Authorised">
                                        <ItemTemplate>
                                            <asp:Label ID="txtauth" runat="server" CssClass="Label" Text='<%# Bind("AUTH") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Authorised By">
                                        <ItemTemplate>
                                            <asp:Label ID="txtauth_by" runat="server" CssClass="Label" Text='<%# Bind("AUTH_BY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Authorised Date">
                                        <ItemTemplate>
                                            <asp:Label ID="txtauth_dt" runat="server" CssClass="Label" Text='<%# Bind("AUTH_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Advance Percentage">
                                        <ItemTemplate>
                                            <asp:Label ID="txtadv_per" runat="server" CssClass="Label" Text='<%# Bind("ADV_PRCNT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Final Ammount">
                                        <ItemTemplate>
                                            <asp:Label ID="txtfinal_amt" runat="server" CssClass="Label" Text='<%# Bind("FINAL_AMT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transportor Name">
                                        <ItemTemplate>
                                            <asp:Label ID="txtparty_name" runat="server" CssClass="Label" Text='<%# Bind("TRSP_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<cc1:CalendarExtender ID="cetsub_FDT" Format="dd/MM/yyyy" TargetControlID="TxtFdate"
    runat="server">
</cc1:CalendarExtender>
<cc1:CalendarExtender ID="cetsub_TODT" Format="dd/MM/yyyy" TargetControlID="TxtTdate"
    runat="server">
</cc1:CalendarExtender>
