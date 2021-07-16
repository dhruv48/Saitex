<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestImage.aspx.cs" Inherits="TestImage" %>

<%@ Register Src="Module/Admin/Controls/ImageControl.ascx" TagName="ImageControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
        my_window = null;

        function popuponclick(val) {
            my_window = window.open(val, '_blank', 'toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,minimizable=no,dialog=yes,menubar=no,width=800,height=450,left=100,top=200');
            my_window.focus();
        }
        // Added By Rajesh for Ledger Master popup (01 Nov 2011)
        function popuponclickLedger(val) {
            my_window = window.open(val, '_blank', 'location=no, titlebar=no,toolbar=no,statusbar=no,directories=no,status=no,scrollbars=yes,resizable=no,minimizable=no,dialog=yes,menubar=no,width=420,height=450,left=100,top=200');
            my_window.focus();
        }
        function check() {
            if (my_window && !my_window.closed)
                my_window.focus();
        }
        function checkBackDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select a Back date  than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnGetImage" runat="server" OnClick="btnGetImage_Click" Text="Get Image" />
        <br />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/CommonImages/ImageResizer/No_Image.jpg" />
        <asp:Button ID="btnSetImage" runat="server" OnClick="btnSetImage_Click" Width="0px" />
    </div>
    </form>
</body>
</html>
