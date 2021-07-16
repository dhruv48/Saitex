<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetUserMasterData.aspx.cs"
    Inherits="Module_Admin_Pages_GetUserMasterData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlUserName" BackColor="#AFCADA" runat="server" Height="350px">
            <table class="tContentArial">
                <tr>
                    <td>
                        <asp:Panel ID="Panel2" BackColor="#AFCADA" runat="server" Height="300px" ScrollBars="Horizontal">
                            <asp:GridView CssClass="tContentArial" ID="grdUserName" runat="server" AutoGenerateColumns="False"
                                Visible="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="User Code">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnUserCode" runat="server" Text='<%# Bind("USER_CODE") %>'
                                                CommandArgument='<%# Bind("USER_CODE") %>' CommandName="UserName"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnUserName" runat="server" Text='<%# Bind("USER_NAME") %>'
                                                CommandArgument='<%# Bind("USER_CODE") %>' CommandName="UserName"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;<asp:Button ID="btnCalcelCat" runat="server" Text="Cancel"  />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
