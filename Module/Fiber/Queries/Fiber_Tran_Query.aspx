<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fiber_Tran_Query.aspx.cs" Inherits="Module_Fiber_Queries_Fiber_Tran_Query" %>


<%@ Register src="../Controls/Fiber_Tran_Query.ascx" tagname="Fiber_Tran_Query" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Fiber_Tran_Query ID="Fiber_Tran_Query1" runat="server" />
</asp:Content>
