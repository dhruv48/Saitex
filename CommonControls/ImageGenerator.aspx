<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImageGenerator.aspx.cs" Inherits="CommonControls_ImageGenerator" %>

<%@ Register Assembly="CS.Web.UI.CropImage" Namespace="CS.Web.UI" TagPrefix="cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Image Generator</title>

    <script language="javascript" type="text/javascript">

        function GetImageUrl(imgurl, ImageControlId)
         {           
            
            //window.opener.document.getElementById(ImageControlId).click();
            var d = window.opener.document.getElementById(ImageControlId);
            d.click();
            alert("Do you want to save it.");   
            window.opener.document.forms[0].submit();
            window.close();
            //alert(ImageControlId);
           
        }
          
    </script>  
    
    
    

</head>
<body>
    <form id="form1" runat="server">
    <div>
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
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" />
        <br />
        <div style="margin-top: 5px; width: 100%; height: 400px; float: left;">
            <div style="overflow: auto; width: 49%; height: 400px; float: left; border: solid 1px #ff0000;">
                <asp:Image ID="imgOld" runat="server" ImageUrl="" />
                <cs:CropImage ID="CropImage1" runat="server" Visible="false" Image="imgOld" X="10"
                    Y="10" X2="50" Y2="50" />
            </div>
            <div style="margin-left: 5px; overflow: auto; width: 49%; height: 400px; float: left;
                border: solid 1px #ff0000;">
                <asp:Image ID="imgNew" runat="server"  /></div>
        </div>
    </div>
    <asp:Image ID="Image1" runat="server" />
    </form>
</body>
</html>
