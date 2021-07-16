<%@ Page Language="C#"  MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="looseyarnpack1.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_looseyarnpack1" %>

<%@ Register src="../Controls/loose_yarnpack.ascx" tagname="loose_yarnpack" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:loose_yarnpack ID="loose_yarnpack1" runat="server" />
</asp:Content>


