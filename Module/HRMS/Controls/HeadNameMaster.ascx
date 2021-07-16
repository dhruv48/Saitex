<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeadNameMaster.ascx.cs" Inherits="Module_HRMS_Controls_HeadNameMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
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
        width: 50px;
    }
    .c2
    {
        margin-left: 4px;
        width: 90px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
    <table class="tContentArial">
       <tr>
         <td class="td">
                 <table>
                        <td id="tdSave"  runat="server">
                            <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                Width="48px" Height="41px" ValidationGroup="M1" OnClick="imgbtnSave_Click1"></asp:ImageButton>
                        </td>
                        <td id="tdUpdate" runat="server">
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                Width="48" Height="41" ValidationGroup="M1" onclick="imgbtnUpdate_Click" ></asp:ImageButton>
                        </td>
                         <td id="tdFind" runat="server" valign="top">
                            <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                Width="48" Height="41" TabIndex="7" onclick="imgbtnFind_Click" ></asp:ImageButton>
                        </td>
                        <td id="tdDelete" runat="server">
                            <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                Width="48" Height="41" 
                                OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')" onclick="imgbtnDelete_Click"
                                ></asp:ImageButton>
                        </td>
                        <td id="tdClear" runat="server">
                            <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                Width="48" Height="41" 
                                OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')" onclick="imgbtnClear_Click"
                               ></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                Width="48" Height="41" onclick="imgbtnPrint_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                Width="48" Height="41" onclick="imgbtnExit_Click" ></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                Width="48" Height="41"></asp:ImageButton>
                        </td>
                    </table>
        </td>
    </tr>
         <tr>
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Head Master</span>
        </td>
    </tr>
       <tr>
        <td class="td">
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="M1" />
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
        <tr>
        <td class="td">
            <table>
                <td>
                    <tr>
                        <td align="right"  valign="top">
                            *Head Name
                        </td>
                        <td align="center"  valign="top">
                            <b>:</b>
                        </td>
                        <td align="left"  valign="top">
                            <asp:TextBox ID="txtHeadName" runat="server" Width="150px" ValidationGroup="M1" 
                                CssClass="UpperCase"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHeadName"
                                ErrorMessage="Enter Head" ValidationGroup="M1"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" >
                            Status</td>
                        <td align="center" valign="top" >
                            <strong>:</strong></td>
                        <td align="left" valign="top" >
                            <asp:CheckBox ID="chkActive" runat="server" TabIndex="20" /></td>
                 
                    </tr>
                </td>
            </table>
        </td>
    </tr>
        <tr>
        <td class="td">
            <table>
                <td align="left">
                <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" 
                PageSize="5" AutoGenerateColumns="False" OnSelect="Grid1_Select" AutoPostBackOnSelect="True" >
                <Columns>
                    <cc2:Column DataField="HEAD_ID" HeaderText="HeadId"  width="100px" Align="center">
                    </cc2:Column>
                    <cc2:Column DataField="HEAD_NAME" HeaderText="Head Name" width="200px">
                    </cc2:Column>
                     </Columns>
            </cc2:Grid>
            </td>
            </table>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>


