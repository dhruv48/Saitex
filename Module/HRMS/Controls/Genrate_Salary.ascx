<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Genrate_Salary.ascx.cs"
    Inherits="Module_HRMS_Controls_Genrate_Salary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    .modalPopup
    {
        background-color: #696969;
        filter: alpha(opacity=40);
        opacity: 0.7;
        xindex: -1;
    }
</style>

<script language="javascript" type="text/javascript">
      //
var prm = Sys.WebForms.PageRequestManager.getInstance();
//Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
prm.add_beginRequest(BeginRequestHandler);
// Raised after an asynchronous postback is finished and control has been returned to the browser.
prm.add_endRequest(EndRequestHandler);
function BeginRequestHandler(sender, args) 
{
    //Shows the modal popup - the update progress
    var popup = $find('<%= modalPopup.ClientID %>');
    if (popup != null) 
    {
    popup.show();
    }
}

function EndRequestHandler(sender, args) {
    //Hide the modal popup - the update progress
    var popup = $find('<%= modalPopup.ClientID %>');
    if (popup != null) {
        popup.hide();
    }
} 
</script>

<asp:UpdateProgress ID="UpdateProgress" runat="server">
    <ProgressTemplate>
        <asp:Image ID="Image1" ImageUrl="~/CommonImages/waiting.gif" AlternateText="Processing"
            runat="server" />
    </ProgressTemplate>
</asp:UpdateProgress>
<cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
    PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" BehaviorID="pload" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
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
                <td class="TableHeader td" colspan="8" align="center">
                    <span class="titleheading">Genrate Salary</span>
                </td>
            </tr>
            <tr>
                <td align="right" class="TdBackVir">
                    Salary Year
                </td>
                <td align="left" class="TdBackVir">
                    <asp:TextBox ID="TxtSalYear" CssClass="gCtrTxt" Width="150px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="TdBackVir">
                    Salary Month
                </td>
                <td align="left" class="TdBackVir">
                    <asp:TextBox ID="TxtSalMonth" CssClass="gCtrTxt" Width="150px" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td align="right" class="TdBackVir">
                    Branch
                </td>
                <td align="left" class="TdBackVir">
                    <asp:DropDownList ID="ddlBranch" runat="server" Width="150px" CssClass="gCtrTxt">
                    </asp:DropDownList>
                </td>
                <td align="right" class="TdBackVir">
                    Deaprtment
                </td>
                <td align="left" class="TdBackVir">
                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="150px" CssClass="gCtrTxt">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" class="TdBackVir">
                    Cadder
                </td>
                <td align="left" class="TdBackVir">
                    <asp:DropDownList ID="DDLCader" Width="150px" CssClass="gCtrTxt" runat="server">
                        <asp:ListItem Value="0">----------SELECT---------</asp:ListItem>
                        <asp:ListItem Value="STAFF">STAFF</asp:ListItem>
                        <asp:ListItem Value="WORKMEN">WORKMEN</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" class="TdBackVir">
                    Employee
                </td>
                <td align="left" class="TdBackVir">
                    <obout:ComboBox runat="server" ID="ddlEmployee" EnableVirtualScrolling="true" Width="150px"
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
                <td align="right" class="TdBackVir">
                </td>
                <td align="left" class="TdBackVir">
                    <asp:Button ID="btnSalaryGenerate" Text="Create Salary" OnClientClick="ShowModalPopup('pload');"
                        CssClass="AButton" runat="server" Width="125" ValidationGroup="M1" OnClick="btnSalaryGenerate_Click" />
                </td>
            </tr>
        </table>
  </ContentTemplate>
</asp:UpdatePanel>
