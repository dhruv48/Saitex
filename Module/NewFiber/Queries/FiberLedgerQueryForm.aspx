<%@ Page Language="C#"  MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberLedgerQueryForm.aspx.cs" Inherits="Module_Fiber_Queries_FiberLedgerQueryForm" %>
<%@ Register src="../Controls/FiberLedgerQueryForm.ascx" tagname="FiberLedgerQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphBody" Runat="Server">
 <uc1:FiberLedgerQueryForm ID="FiberLedgerQueryForm1" runat="server" />
</asp:Content>


