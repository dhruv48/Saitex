<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Monthly_PF_ESI_Report.ascx.cs" Inherits="Module_HRMS_Controls_Monthly_PF_ESI_Report" %>
<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>

<script type="text/javascript">
        function NewWindow() 
        {
        document.forms[0].target = "_blank";
        }
</script>

<table class="td tContent" width="100%">
    <tr>
        <td colspan="4" class="td">
            <table class="tContent">
                <tr>
                    <td id="tdClear" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')"
                            TabIndex="8" ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                    </td>
                    <td id="tdExit" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" TabIndex="10" ToolTip="Exit" Width="48" />
                    </td>
                    <td id="tdHelp" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            TabIndex="11" ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="TableHeader td">
            <table border="0" width="100%">
                <tr>
                    <td align="center" width="100%">
                        <span class="titleheading"><b>Print Monthly PF/ESI Report</b></span></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" align="left" class="td">
            <table border="0" cellpadding="3" cellspacing="0" width="100%" class="tContentArial">
                <tr>
                    <td colspan="8" align="center" valign="top">
                        <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="10%" align="right" valign="top">
                        Year
                    </td>
                    <td width="30%" align="left" valign="top">
                        <asp:DropDownList ID="ddlYear" runat="server" Width="160px" CssClass="SmallFont">
                        </asp:DropDownList>
                        <br />
                    </td>
                    <td width="10%" align="right" valign="top">
                        Month
                    </td>
                    <td width="30%" align="left" valign="top">
                        <asp:DropDownList ID="ddlMonth" runat="server" Width="160px" CssClass="SmallFont">
                            <asp:ListItem Value="" Text="----------SELECT--------"></asp:ListItem>
                            <asp:ListItem Value="01" Text="January"></asp:ListItem>
                            <asp:ListItem Value="02" Text="February"></asp:ListItem>
                            <asp:ListItem Value="03" Text="March"></asp:ListItem>
                            <asp:ListItem Value="04" Text="April"></asp:ListItem>
                            <asp:ListItem Value="05" Text="May"></asp:ListItem>
                            <asp:ListItem Value="06" Text="June"></asp:ListItem>
                            <asp:ListItem Value="07" Text="July"></asp:ListItem>
                            <asp:ListItem Value="08" Text="August"></asp:ListItem>
                            <asp:ListItem Value="09" Text="September"></asp:ListItem>
                            <asp:ListItem Value="10" Text="October"></asp:ListItem>
                            <asp:ListItem Value="11" Text="November"></asp:ListItem>
                            <asp:ListItem Value="12" Text="December"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td width="10%" align="right" valign="top">
                        Department
                    </td>
                    <td width="30%" align="left" valign="top">
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="160px" CssClass="SmallFont">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Designation:
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLDesigination" Width="160px" CssClass="TextBox SmallFont"
                            runat="server">
                        </asp:DropDownList>
                    </td>
                    <td width="10%" align="right" valign="top">
                        Branch
                    </td>
                    <td width="30%" align="left" valign="top">
                        <asp:DropDownList ID="ddlBranch" runat="server" Width="160px" CssClass="SmallFont">
                        </asp:DropDownList>
                    </td>
                     <td align="right">
                        Cadder:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DDLCader" Width="160px" CssClass="SmallFont" runat="server">
                            <asp:ListItem Value="">--------SELECT-------</asp:ListItem>
                            <asp:ListItem Value="STAFF">STAFF</asp:ListItem>
                            <asp:ListItem Value="WORKMEN">WORKMEN</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                   
                </tr>
                <tr>
                    <td align="right">
                        Employee:
                    </td>
                    <td>
                        <obout:ComboBox runat="server" ID="ddlEmployee" EnableVirtualScrolling="true" Width="160px"
                            Height="200px" DataTextField="EMPLOYEENAME" CssClass="SmallFont TextBox UpperCase"
                            DataValueField="EMP_CODE" EnableLoadOnDemand="true" OnLoadingItems="ddlEmployee_LoadingItems"
                            AutoPostBack="True" MenuWidth="300px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Emp Code</div>
                                <div class="header c2">
                                    Employee Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("EMP_CODE")%></div>
                                <div class="item c2">
                                    <%# Eval("EMPLOYEENAME")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </obout:ComboBox>
                    </td>
                     <td align="right">
                        Report Type:
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DDLReportType" Width="160px" CssClass="SmallFont" runat="server">                           
                            <asp:ListItem Value="ESI">ESI</asp:ListItem>
                            <asp:ListItem Value="PF">PF</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="2" align="center" valign="top" style="height: 25px; text-align: left;">
                        <asp:Button ID="btnPrint" Text="Print All" runat="server" Width="75" OnClientClick="NewWindow();"
                            CssClass="AButton" OnClick="btnPrint_Click" />
                        <br />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>