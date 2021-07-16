<%@ Page Language="C#"  MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Lot_Making_Form.aspx.cs" Inherits="Module_Production_Pages_Lot_Making_Form" ValidateRequest="false" %>


<%@ Register Src="~/Module/Production/Controls/Lot_Making_Form.ascx" TagName="Lot_Making_Form" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc1:Lot_Making_Form id="Lot_Making_Form1" runat = "server"></uc1:Lot_Making_Form>
</asp:Content>

