<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LedgerMstPopUp.aspx.cs" Inherits="Module_FA_Pages_LedgerMstPopUp" %>

<%@ Register Src="../Controls/LedgerMstPopUp.ascx" TagName="LedgerMstPopUp" TagPrefix="uc1" %>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:LedgerMstPopUp ID="LedgerMstPopUp1" runat="server" />
    </div>
    </form>
</body>
</html>
