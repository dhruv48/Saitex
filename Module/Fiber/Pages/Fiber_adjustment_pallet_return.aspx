<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fiber_adjustment_pallet_return.aspx.cs"
    Inherits="Module_Fiber_Pages_Fiber_adjustment_pallet_return" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Adjust Pallet Return</title>

    <script language="javascript" type="text/javascript">

        function GetRowValue(val, TextBoxId) {
            window.opener.document.getElementById(TextBoxId).value = val;
            window.opener.document.forms[0].submit();
            window.close();
        }

    </script>

    <link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 39px;
            height: 20px;
        }
        .style2
        {
            height: 20px;
        }
    </style>
</head>
<body style="background-color: #afcae4;">
    <form id="form1" runat="server" style="background-color: #afcae4;">
    <div>
        <cc1:ToolkitScriptManager ID="ToolKitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="tContentArial" style="background-color: #afcae4;">
                    <tr>
                        <td class="TableHeader td tdCenter">
                            <span class="titleheading">Adjust Pallet For Return </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">                         
                            <asp:Label ID="lblAdjITEMBOMCode" CssClass="SmallFont Label" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Label ID="lblAdjBomError" runat="server" CssClass="SmallFont Label" Font-Bold="false"
                                ForeColor="Red"></asp:Label>
                        </td>
                        <td class="tdCenter td SmallFont">
                            </td>
                    </tr>
                    <tr>
                        <td class="SmallFont td tdCenter">
                            <asp:Panel ID="pnlAdjpalletreturn" runat="server" ScrollBars="Vertical" Height="200px"
                                BackColor="#afcae4">
                                <asp:GridView ID="grdAdjpalletreturn" runat="server" AutoGenerateColumns="False"   
                                    ShowFooter="True" onrowdatabound="grdAdjpalletreturn_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="MRN&nbsp;NUMBER" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMRN_NUMBER" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#336799" ForeColor="White" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MRN&nbsp;CODE" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMRN_CODE" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("TRN_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#336799" ForeColor="White" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="MERGE&nbsp;NO" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMERGE_NO" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("MERGE_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#336799" ForeColor="White" />
                                        </asp:TemplateField>
                                         
                                        <asp:TemplateField HeaderText="PALLET&nbsp;CODE" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPALLET_CODE" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("PALLET_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#336799" ForeColor="White" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PALLET&nbsp;NUMBER" HeaderStyle-BackColor="#336799" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPALLET_NUMBER" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("PALLET_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#336799" ForeColor="White" />                                       
                                            <FooterTemplate>
                                                <asp:Label ID="lblpal" CssClass="SmallFont LabelNo" runat="server" Text ="Total"  Width="50px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Return" HeaderStyle-BackColor="#336799">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkpalletnoApproved" runat="server"  AutoPostBack ="true"           oncheckedchanged="chkpalletnoApproved_CheckedChanged" />
                                                      <asp:Label ID="lblPalletNoApproved" CssClass="SmallFont LabelNo" runat="server"  Text='<%# Bind("IS_RETURNED") %>'  Width="50px"  Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#336799" ForeColor="White" />
                                            <FooterTemplate >
                                                <asp:Label ID="txtTotalAdjQTY" CssClass="SmallFont LabelNo" runat="server" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdCenter td SmallFont">
                            <asp:Button ID="btnAdjpalletreturnItem" runat="server" Text="Adjust pallet to return"
                                OnClick="btnAdjpalletreturnItem_Click" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
