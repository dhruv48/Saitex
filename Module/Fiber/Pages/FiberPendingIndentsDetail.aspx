<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FiberPendingIndentsDetail.aspx.cs" Inherits="Module_Fiber_Pages_FiberPendingIndent_Detail" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Innovative Pvt. Ltd.</title>
    <link href="../../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#afcae4"">
    <form id="Form2" runat="server">
    <table border="1" cellpadding="3" cellspacing="0" width="100%" class="tContentArial"
        style="background-color: #afcae4">
        <tr>
            <td width="100%" align="center" class="TableHeader" colspan="4">
                <b class="titleheading">Detail of Pending Indents for Selected Department </b>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="gvPendIndDetails" runat="server" AllowPaging="False" PagerSettings-Position="Bottom"
                    AutoGenerateColumns="False" PagerSettings-Mode="Numeric" PagerStyle-HorizontalAlign="Left"
                    OnPageIndexChanging="gvPendIndDetails_PageIndexChanging" EmptyDataText="No Data Found"
                    PageSize="15">
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="100px">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchName" Text='<%#Eval("DEPT_NAME")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Indent No.">
                            <ItemTemplate>
                                <asp:Label ID="lblDeptName" Text='<%#Eval("IND_NUMB") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Indent Date">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("INDENT_DATE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Description">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("FIBER_DESC") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approval Date">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("APPROVAL_DATE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Indent Qty.">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("QTY_INDENTED") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved Qty.">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("QTY_APPROVED") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adjusted Qty.">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("QTY_ADJUSTED") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending Qty.">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("QTY_PENDING") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending Since (Days)">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("PENDING_DAYS") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="RowStyle SmallFont" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <HeaderStyle CssClass="HeaderStyle GrdHeader" />
                </asp:GridView>
        </tr>
    </table>
    </form>
</body>
</html>
