<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserAuthMenu.ascx.cs" Inherits="Admin_UserControls_UserAuthMenu" %>
<asp:TreeView ID="trvMenu" runat="server">
    <Nodes>
        <asp:TreeNode NavigateUrl="~/Admin/UserMaster.aspx" Text="User Master" Value="User Master">
        </asp:TreeNode>
        <asp:TreeNode NavigateUrl="~/Admin/CompanyMaster.aspx" Text="Company Master" Value="Company Master">
        </asp:TreeNode>
        <asp:TreeNode NavigateUrl="~/Admin/BranchMaster.aspx" Text="Branch Master" Value="Branch Master">
        </asp:TreeNode>
        <asp:TreeNode NavigateUrl="~/Admin/DepartmentMaster.aspx" Text="Department Master"
            Value="Department Master"></asp:TreeNode>
        <asp:TreeNode NavigateUrl="~/Admin/AddModule.aspx" Text="Module Master" Value="Module Master">
            <asp:TreeNode NavigateUrl="~/Admin/AddModule.aspx" Text="Add Module" Value="Add Module">
            </asp:TreeNode>
            <asp:TreeNode NavigateUrl="~/Admin/AddChildMenu.aspx" Text="Child Module" Value="Child Module">
            </asp:TreeNode>
            <asp:TreeNode NavigateUrl="~/Admin/AddNavigation.aspx" Text="Navigation" Value="Navigation">
            </asp:TreeNode>
        </asp:TreeNode>
        <asp:TreeNode NavigateUrl="~/Admin/UserNavigationRight.aspx" Text="User Navigation Right"
            Value="User Navigation Right"></asp:TreeNode>
        <asp:TreeNode NavigateUrl="~/Admin/AuthorisedToUser.aspx" Text="User Authorisation"
            Value="User Authorisation"></asp:TreeNode>
    </Nodes>
</asp:TreeView>
