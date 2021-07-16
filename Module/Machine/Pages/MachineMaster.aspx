<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MachineMaster.aspx.cs" Inherits="Module_Machine_Pages_MachineMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/Machine_Master.ascx" tagname="Machine_Master" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Machine_Master ID="Machine_Master1" runat="server" />
</asp:Content>

