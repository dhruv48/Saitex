<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="IndentagainstBom.aspx.cs" Inherits="Module_Fiber_Pages_IndentagainstBom" %>

<%@ Register src="../Controls/FiberIndentBom.ascx" tagname="FiberIndentBom" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FiberIndentBom ID="FiberIndentBom1" runat="server" />
</asp:Content>

