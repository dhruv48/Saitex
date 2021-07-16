<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialIssueForDept.aspx.cs" Inherits="Module_Inventory_Pages_MaterialIssueForDept" %>

<%@ Register src="../Controls/Material_Issue_For_Dept.ascx" tagname="Material_Issue_For_Dept" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Material_Issue_For_Dept ID="Material_Issue_For_Dept" runat="server" />
</asp:Content>

