<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WasteMasterQry.ascx.cs"
    Inherits="Module_Waste_Controls_WasteMasterQry" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel8971" runat="server">
    <ContentTemplate>
        <table width="100%" class ="td tContentArial">
            <tr>
                <td width="100%">
                    <table >
                        <tr>
                            <td id="tdClear" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" />
                            </td>
                        </tr>
                    </table>
                    <table width="80%" class ="td">
                        <tr>
                            <td align="center" valign="top" class="tRowColorAdmin " colspan="10">
                                <span class="titleheading">Waste Master Query</span>
                            </td>
                        </tr>
                    </table>
                    <table width="75%" >
                        <tr>
                            <td align="right">
                                Select&nbsp;Branch:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Department:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Category:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlItemCate" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <%--Item&nbsp;Type:--%>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" Visible=false>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnsave" runat="server" Text="Get Record" Width="85px" Height="22px"
                                    OnClick="btnsave_Click"  CssClass="AButton" />
                            </td>
                              <td></td>
                        </tr>
                      
                        <tr>
                            <td width="50%" colspan="4" >
                                <b>Total&nbsp;Records: </b>
                                <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                            </td>
                            <td width="50%" colspan="4">
                                <asp:UpdateProgress ID="UpdateProgress9587" runat="server">
                                    <ProgressTemplate>
                                        <h3>
                                            Loading...</h3>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                    <table  width="100%" >
                        <tr>
                            <td align="left">
                                <asp:Panel ID="pnlShowHover" runat="server" ScrollBars="Auto" Width="100%">
                                    <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Bold="true" Width="80%"
                                        AllowPaging="true" PageSize="12" CellPadding="3" ForeColor="#333333" GridLines="Both"
                                        BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" OnPageIndexChanging="Grid1_PageIndexChanging">
                                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />

                                        <Columns>
                                            <asp:BoundField DataField="BRANCH_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CAT_CODE" HeaderStyle-HorizontalAlign="Left" HeaderText="Cat. Code"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Code" Visible="true" />
                                            <asp:BoundField DataField="ITEM_TYPE" HeaderStyle-HorizontalAlign="Left" HeaderText="Item Type" Visible=false
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ITEM_DESC" HeaderStyle-HorizontalAlign="Left" HeaderText="Description"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ITEM_MAKE" HeaderStyle-HorizontalAlign="Left" HeaderText="Item Make" Visible=false
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderStyle-HorizontalAlign="Left" HeaderText="UOM"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="ITEM_STATUS" HeaderStyle-HorizontalAlign="Left" HeaderText="Item Status"
                                                ItemStyle-HorizontalAlign="Left" Visible="true">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="OP_BAL_STOCK" HeaderStyle-HorizontalAlign="Right" HeaderText="Opening Bal. St."
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OP_RATE" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Right"
                                                HeaderText="Opening Rate" ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MIN_STOCK_LVL" HeaderStyle-HorizontalAlign="Right" HeaderText="Min St. Level"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REODR_QTY" HeaderStyle-HorizontalAlign="Right" HeaderText="Reorder Qty"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EXPIRY_DAYS" HeaderStyle-HorizontalAlign="Right" HeaderText="Expire Days" Visible=false
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                           <%-- <asp:BoundField DataField="ITEM_STATUS" HeaderStyle-HorizontalAlign="Left" HeaderText="Item status"
                                                ItemStyle-HorizontalAlign="Left" Visible="true">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" />
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
