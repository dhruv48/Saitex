<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POIndentAdjustment.aspx.cs"
    Inherits="Inventory_POIndentAdjustment" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjust Meterial Indent</title>
    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

    function GetRowValue(val,TextBoxId)
    {           
        window.opener.document.getElementById(TextBoxId).value=val;   
        window.opener.document.forms[0].submit();      
        window.close();
    }
    </script>

</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="tContentArial" style="background-color: #afcae4">
                    <tr>
                        <td class="TableHeader td tdCenter">
                            <span class="titleheading">Indent Adjustment</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            Adjust Indent For Item :
                            <asp:Label ID="lblAdjustItemIndentCode" CssClass="SmallFont Label" runat="server"></asp:Label>
                        </td>
                        <asp:Label ID="lblAdjustIndentItemCode" runat="server"></asp:Label></tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Label ID="lblIndentAdjustmentError" runat="server" CssClass="SmallFont Label"
                                Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Panel ID="pnlIndentAdjustment" runat="server" ScrollBars="Vertical" Height="200px"
                                BackColor="#afcae4">
                                <asp:GridView ID="grdIndentAdjustment" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" OnRowCommand="grdIndentAdjustment_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Indent Year" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustIndentYear" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("IND_YEAR") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Indent Branch">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBranch" CssClass="SmallFont Label" runat="server" ToolTip='<%# Bind("BRANCH_CODE") %>'
                                                    Text='<%# Bind("BRANCH_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Indent Number" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustIndentNuber" CssClass="SmallFont LabelNo" runat="server"
                                                    Text='<%# Bind("IND_NUMB") %>'></asp:Label>
                                                <asp:Label ID="lblAPPR_QTY" CssClass="SmallFont LabelNo" runat="server" ToolTip='<%# Bind("Ind_Type") %>'
                                                    Text='<%# Bind("APPR_QTY") %>' Visible="False"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjustRemQty" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("REMQTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="POADJUST_QTY" HeaderText="Adjusted QTY" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" />
                                        <asp:TemplateField HeaderText="Adjust QTY">
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAdjustedIndentQTY" CssClass="SmallFont LabelNo" runat="server"
                                                    Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAdjustedIndentQTY" runat="server" Text="0" Width="50px" AutoPostBack="True"
                                                    CssClass="SmallFont TextBoxNo" OnTextChanged="txtAdjustedIndentQTY_TextChanged1"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAdjustedIndentQTY"
                                                    ErrorMessage="Only numeric value allowed" Display="Dynamic" SetFocusOnError="true"
                                                    MaximumValue="999999999" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAdjAll" runat="server" CommandArgument='<%# Bind("IND_NUMB") %>'
                                                    CommandName="AdjAll" Text="Adjust All" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle SmallFont" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                                    <PagerStyle CssClass="PagerStyle" />
                                    <HeaderStyle CssClass="HeaderStyle GrdHeader" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Button ID="btnAdjustIndentItem" runat="server" Text="Adjust Item Indent" OnClick="btnAdjustIndentItem_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
