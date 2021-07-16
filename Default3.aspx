<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblName" runat="server" Text="Name:" TabIndex="1" ToolTip="shiv"></asp:Label>
        <asp:TextBox ID="txtName" runat="server" ForeColor="#660033" MaxLength="100" 
            TabIndex="2" Width="500px"></asp:TextBox>
    
    </div>
  <asp:FileUpload ID="uploadPan" runat="server" CssClass="SmallFont" 
                                        Width="200px" />
    <asp:Button ID="btnShow" runat="server" onclick="btnShow_Click" Text="Show" />
  
    </form>
</body>
</html>
