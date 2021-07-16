<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="packyarncomb.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_packyarn" %>
<%@ Register src="../Controls/Yarn_Packcomb.ascx" tagname="Yarn_Packcomb" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Yarn_Packcomb ID="Yarn_Packcomb" runat="server" />
</asp:Content>


