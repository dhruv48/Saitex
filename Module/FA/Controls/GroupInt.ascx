<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupInt.ascx.cs" Inherits="Module_FA_Controls_GroupInt" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 180px;
    }
</style>

<script language="javascript" type="text/javascript">
 
function CheckBoxListSelect(cbControl, state)
{   
       var chkBoxList = document.getElementById(cbControl);
        var chkBoxCount= chkBoxList.getElementsByTagName("input");
        for(var i=0;i<chkBoxCount.length;i++)
        {
            chkBoxCount[i].checked = state;
        }
       
        return false; 
}
 
</script>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td align="center" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    Width="48" Height="41" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    Width="48" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                    Width="48" Height="41" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="td TableHeader" width="100%">
                    <asp:Label ID="lblFormHeading" CssClass="titleheading" runat="server" Text="Group Integration"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                </td>
            </tr>
            <tr>
                <td class="td" align="center">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="M1" />
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td align="center" class="td">
                                Select Company :
                                <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true" DataTextField="COMP_NAME"
                                    DataValueField="COMP_CODE" CssClass="SmallFont" TabIndex="1" Width="200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFCompany" runat="server" ControlToValidate="ddlCompany"
                                    Display="None" ErrorMessage="Please Select Company" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:CheckBoxList ID="chkListGroup" runat="server" RepeatColumns="4">
                                </asp:CheckBoxList>
                                Select <a id="A3" href="#" onclick="javascript: CheckBoxListSelect ('<%= chkListGroup.ClientID %>',true)">
                                    All</a> | <a id="A4" href="#" onclick="javascript: CheckBoxListSelect ('<%= chkListGroup.ClientID %>',false)">
                                        None</a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
