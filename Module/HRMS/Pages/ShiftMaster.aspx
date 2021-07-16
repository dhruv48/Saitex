<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="ShiftMaster.aspx.cs" Inherits="Module_HRMS_Pages_ShiftMaster" Title="Shift Master" %>
    

<%@ Register src="../Controls/ShiftMaster.ascx" tagname="ShiftMaster" tagprefix="uc2" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">

                                    

    <uc2:ShiftMaster ID="ShiftMaster1" runat="server" />

                                    

</asp:Content>

