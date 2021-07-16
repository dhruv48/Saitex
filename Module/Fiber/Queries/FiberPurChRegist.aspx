<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberPurChRegist.aspx.cs" Inherits="Module_Fiber_Queries_FiberPurChRegist" %>

<%@ Register src="../Controls/Fiber_Purchase_Reg.ascx" tagname="Fiber_Purchase_Reg" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Fiber_Purchase_Reg ID="Fiber_Purchase_Reg1" runat="server" />
</asp:Content>

