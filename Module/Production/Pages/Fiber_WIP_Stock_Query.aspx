<%@ Page Language="C#"  MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Fiber_WIP_Stock_Query.aspx.cs" Inherits="Module_Production_Pages_Fiber_WIP_Stock_Query" %>

<%@ Register src="../Controls/Fiber_WIP_Stock_Query.ascx" tagname="Fiber_WIP_Stock_Query" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Fiber_WIP_Stock_Query ID="Fiber_WIP_Stock_Query1" runat="server" />
</asp:Content>
