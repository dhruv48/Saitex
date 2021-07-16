<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="SewingThread.aspx.cs" Inherits="Module_Swing_Thread_Page_SwingThread"
    Title="Untitled Page" %>

<%@ Register src="../Controls/SewingThreadMst.ascx" tagname="SewingThreadMst" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:SewingThreadMst ID="SewingThreadMst1" runat="server" />
</asp:Content>
