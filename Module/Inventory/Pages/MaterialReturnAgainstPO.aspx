<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MaterialReturnAgainstPO.aspx.cs" Inherits="Module_Inventory_Pages_MaterialReturnAgainstPO" Title="Untitled Page" %>
<%@ Register src="../Controls/ReturnAgstPO.ascx" tagname="ReturnAgstPO" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReturnAgstPO ID="ReturnAgstPO1" runat="server" />
</asp:Content>

