<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="GrayWeavingOrder.aspx.cs" Inherits="Module_OrderDevelopment_Pages_GrayWeavingOrder"
    Title="Untitled Page" %>

<%@ Register Src="~/Module/OrderDevelopment/Controls/OC_YARN_SPINING.ascx" TagName="OrderCap"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:OrderCap ID="OrderCap1" runat="server" />
</asp:Content>
