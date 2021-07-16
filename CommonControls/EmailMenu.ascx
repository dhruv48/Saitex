<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailMenu.ascx.cs" Inherits="CommonControls_EmailMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="navigate">
    <ul>
        <li>
            <asp:LinkButton ID="lbtnMailNew" PostBackUrl="~/Module/Mail/Pages/NewMail.aspx" runat="server">New Mail</asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnMailInbox" PostBackUrl="~/Module/Mail/Pages/Inbox.aspx" runat="server">Inbox</asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnMailSent" runat="server">Sent</asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnMailOutBox" runat="server">OutBox</asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnMailFolder" runat="server">Folders</asp:LinkButton>
            <cc1:HoverMenuExtender ID="hmeFolder" PopupControlID="pnlFolderList" TargetControlID="lbtnMailFolder"
                PopupPosition="Left" runat="server">
            </cc1:HoverMenuExtender>
            <asp:Panel ID="pnlFolderList" BackColor="White" BorderWidth="1px" BorderColor="Gray"
                BorderStyle="Solid" runat="server">
            </asp:Panel>
        </li>
        <li>
            <asp:LinkButton ID="lbtnMailSetting" PostBackUrl="~/Module/Mail/Pages/Setting.aspx"
                runat="server">Setting</asp:LinkButton>
        </li>
        <li>
            <asp:LinkButton ID="lbtnMailExit" runat="server" OnClick="lbtnMailExit_Click">Exit</asp:LinkButton>
        </li>
    </ul>
</div>
