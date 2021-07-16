<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="YarnPendingPo1.aspx.cs" Inherits="Module_Yarn_SalesWork_Queries_YarnPendingPo"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <table cellpadding="3" cellspacing="0" width="100%" class="tContentArial tablebox">
                <tr>
                    <td class="td" align="left" colspan="6" width="100%">
                        <table class="tContentArial" cellspacing="0" width="10%" cellpadding="0" border="0"
                            align="left">
                           
                                <tr>
                                    <%--<td class="td" width="41">
                                      <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                            Width="41" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                                    </td>--%>
                                    <td class="td" width="41">
                                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                            Width="41" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                                    </td>
                                </tr>
                            
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="10%">
                        Select Branch:
                    </td>
                    <td width="15%">
                        <asp:DropDownList ID="ddlBranch" runat="server" Width="160px" CssClass="gCtrTxt"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td width="10%">
                        Select Year :
                    </td>
                    <td width="10%">
                        <asp:DropDownList ID="ddlYear" runat="server" Width="100px" CssClass="gCtrTxt" AutoPostBack="true"
                            ValidationGroup="M1" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td width="15%">
                        Select Vendor :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlVendor" runat="server" Width="350px" CssClass="gCtrTxt"
                            AutoPostBack="true" ValidationGroup="M1" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="10%">
                        PoStatus:</td>
                    <td width="15%">
                        <asp:DropDownList ID="ddlPoStatus" runat="server" AutoPostBack="true" 
                            CssClass="gCtrTxt" OnSelectedIndexChanged="ddlPoStatus_SelectedIndexChanged" 
                            ValidationGroup="M1" Width="100px">
                            <asp:ListItem Value="1">Approved</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">Un-Approved</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="10%">
                        &nbsp;</td>
                    <td width="10%">
                        &nbsp;</td>
                    <td width="15%">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="td" colspan="6" width="100%">
                        <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="100%" align="center" class="TableHeader" colspan="6">
                        <b class="titleheading">Pending PO Details </b>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:GridView ID="gvReportDisplayGrid" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" AllowSorting="True" Font-Size="x-Small" CellPadding="3" GridLines="Both"
                            Width="100%" ForeColor="#333333" CssClass="smallfont" PagerStyle-HorizontalAlign="Left"
                            OnPageIndexChanging="gvReportDisplayGrid_PageIndexChanging" EmptyDataText="No Record Found"
                            PageSize="15">
                            <FooterStyle Width="100%" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="3%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Left" DataField="BRANCH_NAME"
                                    ItemStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="PO Type" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPOType" Text='<%#Eval("PO_TYPE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO No." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPOno" Text='<%#Eval("PO_NUMB") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PO Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPODate" Text='<%#Eval("PO_DATE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblVendor" Text='<%#Eval("PRTY_NAME") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPOconfdt" Text='<%#Eval("CONF_DATE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn Description" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblItemDesc" Text='<%#Eval("YARN_DESC") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="25%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblUOM" Text='<%#Eval("UOM") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty." HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblOrderQty" Text='<%#Eval("ORD_QTY") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Rate" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblFinalRate" Text='<%# Eval("FINAL_RATE","{0:N2}").ToString()%>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblAmount" Text='<%# Eval("AMOUNT","{0:N2}").ToString()%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pending Days" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="LblPDays" Text='<%#Eval("DAYS") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"
                                ForeColor="White" Font-Bold="True" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
