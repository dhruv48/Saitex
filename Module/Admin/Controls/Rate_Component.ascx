<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rate_Component.ascx.cs" Inherits="Module_Admin_Controls_Rate_Component" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class=" tContentArial" width="100%">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center" class="TableHeader td">
                                <span class="titleheading"><strong>Rate Component Query</strong> </span>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" style="border-style: ridge; border-color: White;">
                        <tr>
                            <td align="right" valign="top" width="15%">
                                Compo Type
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlCompoType" runat="server" Width="140px" 
                                    CssClass="SmallFont BoldFont UPPERCASE" >
                                    <%--onselectedindexchanged="ddlCompoType_SelectedIndexChanged">--%>
                                   <%--<asp:ListItem>ALL</asp:ListItem>--%>
                                   <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>D</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            
                            <td align="right" valign="top" width="10%">
                                <asp:Button ID="btngetrecord" runat="server" Text="Get Record" Width="85px" Height="22px"
                                    OnClick="btngetrecord_Click1" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="left" width="50%">
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                                    <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                            </td>
                            <td align="left" valign="top" width="50%" cssclass="Label">
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                    <tr>
                    <td>
                    <asp:Panel ID="pnl1" ScrollBars="Both" runat="server">
                        <asp:GridView ID="grRateComponentQuery" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            PageSize="20" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                            Width="100%" OnPageIndexChanging="grRateComponentQuery_PageIndexChanging">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                            <asp:BoundField DataField="PRODUCT_TYPE" HeaderText="Product Type" />
                            <asp:BoundField DataField="COMPO_SL" HeaderText="CompoSL" />
                                <asp:BoundField DataField="COMPO_CODE" HeaderText="Compo Code" />
                                
                                <asp:BoundField DataField="COMPO_TYPE" HeaderText="Compo Type" />
                                <%--<asp:BoundField DataField="TUSER" HeaderText="Tuser" />
                                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                <asp:BoundField DataField="COMP_CODE" HeaderText="Comp Code" />--%>
                               <%-- <asp:BoundField DataField="BRANCH_CODE" HeaderText="Branch Code" />--%>
                                
                                <asp:BoundField DataField="LDGR_CODE" HeaderText="LDGR Code" />
                                <asp:BoundField DataField="LDGR_NAME" HeaderText="LDGR Name" />
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
