<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignationMaster.aspx.cs"  MasterPageFile ="~/CommonMaster/admin.master" 
Inherits="Module_HRMS_Pages_DesignationMaster" %>

<%@ Register src="~/Module/Admin/Controls/Designation.ascx"   tagname="Designation" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
 
    <uc1:Designation ID="Designation1" runat="server" />
 
</asp:Content>
