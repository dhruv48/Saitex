<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PromotionIncrement.aspx.cs" Inherits="Module_HRMS_Pages_PromotionIncrement" Title="Untitled Page" %>

<%@ Register src="../Controls/PromotionIncrement.ascx" tagname="PromotionIncrement" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <p>
        <uc1:PromotionIncrement ID="PromotionIncrement1" runat="server" />
    </p>
</asp:Content>

