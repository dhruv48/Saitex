<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageControl.ascx.cs"
    Inherits="Module_Admin_Controls_ImageControl" %>
<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>
<asp:FileUpload ID="fuUploader" runat="server" />
<asp:Button ID="btnPreview" runat="server" OnClick="btnPreview_Click" Text="Preview" />
<asp:CheckBox ID="chkCrop" AutoPostBack="true" Text="Crop" runat="server" OnCheckedChanged="chkCrop_CheckedChanged" />
<asp:CheckBox ID="chkRotate" AutoPostBack="true" Text="Rotate" runat="server" OnCheckedChanged="chkRotate_CheckedChanged"
    Visible="False" />
<asp:Button ID="btnCrop" runat="server" Visible="false" Text="Crop" OnClick="btnCrop_Click" />
H:<asp:TextBox ID="txtNewHeight" runat="server" Width="40px"></asp:TextBox>
W:<asp:TextBox ID="txtNewWidth" runat="server" Width="40px"></asp:TextBox>
<asp:Button ID="btnResize" runat="server" Text="Resize" OnClick="btnResize_Click" />
<asp:DropDownList ID="ddlRotate" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
    OnSelectedIndexChanged="ddlRotate_SelectedIndexChanged" Visible="False">
</asp:DropDownList>
<asp:Button ID="btnSave" runat="server" Text="Save" />
<br />
<div style="margin-top: 5px; width: 100%; height: 400px; float: left;">
    <div style="overflow: auto; width: 49%; height: 400px; float: left; border: solid 1px #ff0000;">
        <asp:Image ID="imgOld" runat="server" />
        <cs:CropImage ID="CropImage1" runat="server" Visible="false" Image="imgOld" X="10"
            Y="10" X2="50" Y2="50" />
    </div>
    <div style="margin-left: 5px; overflow: auto; width: 49%; height: 400px; float: left;
        border: solid 1px #ff0000;">
        <asp:Image ID="imgNew" runat="server" /></div>
</div>
