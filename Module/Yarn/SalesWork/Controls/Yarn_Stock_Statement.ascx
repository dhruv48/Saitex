<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_Stock_Statement.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Yarn_Stock_Statement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
\
<asp:UpdatePanel ID="UpdatePanel99" runat="server">
    <ContentTemplate>
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
                        </tr>
                    </table>
                    <table width="100%" class="td tContentArial">
                        <tr>
                            <td align="center" valign="top" class="TableHeader">
                                <span class="titleheading">Yarn Stock Statement</span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class="td tContentArial">
                        <tr>
                            <td class="tdRight">
                                Financial Year:
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLFinancialYear" Width="150px" runat="server" AutoPostBack="True"
                                    CssClass="SmallFont TextBox UpperCase">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                From date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="TxtStartDate" Width="150px" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                To Date:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="TxtEndDate" CssClass="SmallFont TextBox UpperCase" Width="150px"
                                    runat="server" AutoPostBack="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                Yarn Code:
                            </td>
                            <td class="tdLeft">
                                <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="YARN_CODE" DataValueField="YARN_CODE" EnableLoadOnDemand="true"
                                    MenuWidth="660" OnLoadingItems="Item_LOV_LoadingItems" EnableVirtualScrolling="true"
                                    OpenOnFocus="true" TabIndex="9" Visible="true" Height="200px" OnSelectedIndexChanged="txtItemCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            YARN CODE</div>
                                        <div class="header c5">
                                            YARN DESCRIPTION</div>
                                        <div class="header c6">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("YARN_CODE") %></div>
                                        <div class="item c5">
                                            <%# Eval("YARN_DESC") %></div>
                                        <div class="item c6">
                                            <%# Eval("YARN_TYPE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight">
                                Catagory:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlcatgory" runat="server" Width="150px" CssClass="SmallFont TextBox UpperCase">
                                </asp:DropDownList>
                            </td>
                            <td class="tdLeft">
                                <asp:Button ID="btnview" runat="server" Text="View" Width="100px" Height="25px" OnClick="btnview_Click" />
                            </td>
                        </tr>
                    </table>
              <tr> <td colspan="8" width="100%" class="TdBackVir">
                            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td></tr>
                    <table width="100%">
                        <tr>
                            <td>
                            <asp:Panel ID="Panel1" runat="server"  Height="440px"
                    ScrollBars="Auto" Width="950px">
                                <asp:GridView ID="grdyrnstock" runat="server" AutoGenerateColumns="False" Width="1050px"
                                    Height="75px" GridLines="Both" AllowPaging="false" BorderStyle="Inset" Font-Size="11px"
                                    CellPadding="4" ForeColor="#333333">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB"  />
                                    <Columns>
                                        <asp:BoundField DataField="COMP_CODE" HeaderText="Company Code" />
                                        <asp:BoundField DataField="COMP_NAME" HeaderText="Company Name" />
                                        <asp:BoundField DataField="USER_NAME" HeaderText="User Name" />
                                        <asp:BoundField DataField="BRANCH_CODE" HeaderText="Branch Code" />
                                        <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn Code" />
                                        <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn Description" />
                                        <asp:BoundField DataField="YARN_MAKE" HeaderText="Yarn Make" />
                                        <asp:BoundField DataField="OP_BAL_STOCK" HeaderText="Opening Stock" />
                                        <asp:BoundField DataField="CL_BAL_STOCK" HeaderText="Closing Stock" />
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
        <cc1:CalendarExtender ID="cetsub_FDT" Format="dd/MM/yyyy" TargetControlID="TxtStartDate"
            runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="cetsub_TODT" Format="dd/MM/yyyy" TargetControlID="TxtEndDate"
            runat="server">
        </cc1:CalendarExtender>
    </ContentTemplate>
</asp:UpdatePanel>
