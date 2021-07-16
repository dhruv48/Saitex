<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReceiptQ.aspx.cs" Inherits="Module_Waste_Queries_ReceiptQ" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Receipt Details</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#afcae4"">
    <form id="Form1" runat="server">
    <table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContentArial"
        style="background-color: #afcae4">
        <tr>
            <td width="30%">
                Branch :
                <asp:Label ID="lblBranch" Font-Bold="true" runat="server"></asp:Label>
            </td>
            <td width="20%">
                Year :
                <asp:Label ID="lblYear" Font-Bold="true" runat="server"></asp:Label>
            </td>
            <td width="50%">
                 Description :
                <asp:Label ID="lblItemDesc" Font-Bold="true" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="100%" align="center" class="TableHeader" colspan="4">
                <b class="titleheading">Receipt Details</b>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvReceiptDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    AllowSorting="True" Font-Size="x-Small" CellPadding="3" GridLines="Both" Width="100%"
                    ForeColor="#333333" CssClass="smallfont" PagerStyle-HorizontalAlign="Left" OnPageIndexChanging="gvReceiptDetails_PageIndexChanging"
                    EmptyDataText="No Record Found" PageSize="15">
                    <FooterStyle Width="100%" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="100px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transaction Type">
                            <ItemTemplate>
                                <asp:Label ID="lblTtype" Text='<%#Eval("TRN_DESC")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Item Description">
                            <ItemTemplate>
                                <asp:Label ID="lblItemDesc" Text='<%#Eval("ITEM_DESC")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Trans. No.">
                            <ItemTemplate>
                                <asp:Label ID="lblTransno" Text='<%#Eval("TRN_NO") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trans. Date">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblTDate" Text='<%#Eval("MRN_DATE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GE. No." Visible=false>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblGENo" Text='<%#Eval("GATE_NUMB") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GE. Date" Visible=false>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblGEDate" Text='<%#Eval("GATE_DATE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendor Name">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblVendor" Text='<%#Eval("PRTY_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Name">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblUSER_NAME" Text='<%#Eval("USER_NAME") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Challan No.">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblChallanno" Text='<%#Eval("PRTY_CH_NUMB") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Challan Date">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblChallanDt" Text='<%#Eval("PRTY_CH_DATE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO NO." Visible=false>
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblPONO" Text='<%#Eval("PO_NUMB") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblUOM" Text='<%#Eval("UOM") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QTY">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblQTY" Text='<%#Eval("MRN_QTY") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblRate" Text='<%#Eval("FINAL_RATE","{0:N2}").ToString() %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblAmount" Text='<%#Eval("AMOUNT","{0:N2}").ToString() %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"
                        ForeColor="White" Font-Bold="True" />
                </asp:GridView>
        </tr>
    </table>
    </form>
</body>
</html>
