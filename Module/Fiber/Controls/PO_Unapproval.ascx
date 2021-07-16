<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PO_Unapproval.ascx.cs" Inherits="Module_Fiber_Controls_PO_Unapproval" %>

<script language="javascript" type="text/javascript">
    function divexpandcollapse(divname) {
        var div = document.getElementById(divname);
        var img = document.getElementById('img' + divname);

        if (div.style.display == "none") {
            div.style.display = "inline";
            img.src = "../../../CommonImages/minus.gif";
        } else {
            div.style.display = "none";
            img.src = "../../../CommonImages/plus.gif";
        }
    }
</script>

<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
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
                            ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading"><asp:Label ID="lblHeader" runat="server"></asp:Label></b>
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
        <td>
            <asp:GridView ID="grd_fib_po_app" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                PageSize="15" AllowSorting="True" DataKeyNames="PO_NUMB" CellPadding="0" GridLines="Both" BorderStyle="Ridge" CssClass="smallfont"
                EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                Width="100%" OnPageIndexChanging="grd_fib_po_app_PageIndexChanging" OnRowCommand="grd_fib_po_app_RowCommand"
                OnRowDataBound="grd_fib_po_app_RowDataBound">
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                <RowStyle BackColor="#EFF3FB" />
                <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <ItemTemplate>
                            <a href="JavaScript:divexpandcollapse('div<%# Eval("PO_NUMB") %>');">
                                <img id="imgdiv<%# Eval("PO_NUMB") %>" width="9px" border="0" src="../../../CommonImages/plus.gif" />
                            </a>
                        </ItemTemplate>
                        <ItemStyle Width="20px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PO_NUMB" HeaderText="PO NO">
                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" Wrap="true" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PO_TYPE" HeaderText="PO Type">
                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" Wrap="true" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PO_NATURE" HeaderText="PO NATURE" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PARTY_DATA" HeaderText="PARTY NAME">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DEL_DATE" HeaderText="DELEVERY DATE">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="UNAPPROVE">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CONF_DATE" HeaderText="UNAPPROVE DATE">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CONF_BY" HeaderText="UNAPPROVE BY">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="po No" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPO_NUMB" runat="server" Text='<%# Bind("PO_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblPO_type" runat="server" Text='<%# Bind("PO_TYPE") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <tr>
                                <td colspan="100%">
                                    <div id="div<%# Eval("PO_NUMB") %>" style="display: none; background-color: #DCDDCF;
                                        position: relative; left: 15px; overflow: auto">
                                        <asp:GridView ID="grdchild" runat="server" AutoGenerateColumns="false" BorderStyle="Double"
                                            BorderColor="#336799" GridLines="Both" Width="98%">
                                            <HeaderStyle BackColor="#336799" Font-Bold="true" ForeColor="White" />
                                            <RowStyle BackColor="#E1E1E1" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField DataField="FIBER_CODE" HeaderText="POY CODE" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="FIBER_DESC" HeaderText="POY DESC" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="UOM" HeaderText="UOM" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="ORD_QTY" HeaderText="ORD QTY" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="BASIC_RATE" HeaderText="BASIC RATE" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="FINAL_RATE" HeaderText="FINAL RATE" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="QUOTATION_NO" HeaderText="QUOTATION NO" HeaderStyle-HorizontalAlign="Left" />
                                                <asp:BoundField DataField="DEL_DATE" HeaderText="DEL DATE" HeaderStyle-HorizontalAlign="Left" />
                                                
                                                <%--<asp:BoundField DataField="COMMENTS" HeaderText="COMMENTS" HeaderStyle-HorizontalAlign="Left" />--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
            </asp:GridView>
        </td>
    </tr>
</table>
