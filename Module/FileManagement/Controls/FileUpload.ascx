<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUpload.ascx.cs" Inherits="Module_FileManagement_Controls_FileUpload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
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
        width: 40px;
    }
    .c2
    {
        margin-left: 4px;
        width: 170px;
    }
    .c3
    {
        margin-left: 4px;
        width: 170px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
    }
    .c5
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

<script language="javascript" type="text/javascript">
function func1()
{
document.getElementById("imgPhoto").style.display="";
document.getElementById("imgPhoto").src=document.getElementById("ctl00_cphBody_tPhoto").value;
}
</script>

<table align="left" class="tContentArial">
    <tr>
        <td>
            <table width="100%" class="tContentArial" cellspacing="0" cellpadding="0" align="left">
                <tbody>
                    <tr>
                        <td align="left" class="td" colspan="3">
                            <table class="tContentArial" cellspacing="0" cellpadding="0" border="1">
                                <tbody>
                                    <tr>
                                        <td id="tdSave" align="left" width="48" runat="server">
                                            <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ValidationGroup="M1"
                                                ImageUrl="~/CommonImages/save.jpg" ToolTip="Save"></asp:ImageButton>
                                        </td>
                                        <td id="tdFind" runat="server" align="left" width="48">
                                            <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                                ToolTip="Find" Height="41" Width="48"></asp:ImageButton>
                                        </td>
                                        <td id="tdUpdate" align="left" width="48" runat="server">
                                            <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ValidationGroup="M1"
                                                ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48">
                                            </asp:ImageButton>
                                        </td>
                                        <td id="tdDelete" align="left" width="48" runat="server">
                                            <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ImageUrl="~/CommonImages/del6.png"
                                                ToolTip="Delete" Height="41" Width="48"></asp:ImageButton>
                                        </td>
                                        <td id="tdClear" runat="server" align="left" width="48">
                                            <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                                        </td>
                                        <td id="tdPrint" runat="server" align="left" width="48">
                                            <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                                        </td>
                                        <td id="tdExit" runat="server" align="left" width="48">
                                            <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                                        </td>
                                        <td id="tdHelp" runat="server" align="left" width="48">
                                            <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableHeader td" align="center" colspan="3">
                            <b class="titleheading">File Uploading</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" valign="top" align="left" colspan="3">
                            <span class="Mode">You are in
                                <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center" colspan="3">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ValidationGroup="M1" />
                            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                            </strong>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                            </strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td">
                            <table>
                                <tr>
                                    <td>
                                        *File Code
                                    </td>
                                    <td>
                                        <b>:</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFileCode" TabIndex="1" runat="server" ValidationGroup="M1" CssClass="TextBoxDisplay"
                                            MaxLength="15" TextMode="singleLine" ReadOnly="true" Width="190px"></asp:TextBox>
                                        <cc1:ComboBox ID="cmbFileCode" runat="server" EnableLoadOnDemand="True" CssClass="SmallFont"
                                            Width="197px" MenuWidth="350px" Height="200px" TabIndex="2" AutoPostBack="True"
                                            EmptyText="select file code" OnLoadingItems="cmbFileCode_LoadingItems" OnSelectedIndexChanged="cmbFileCode_SelectedIndexChanged"
                                            DataTextField="FILE_CODE" DataValueField="FILE_CODE">
                                            <HeaderTemplate>
                                                <div class="header c1">
                                                    Code</div>
                                                <div class="header c2">
                                                    File Name</div>
                                                <div class="header c4">
                                                    File Type</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c1">
                                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("FILE_CODE") %>' /></div>
                                                <div class="item c2">
                                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("FILE_NAME") %>' /></div>
                                                <div class="item c4">
                                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("FILE_TYPE") %>' /></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc1:ComboBox>
                                    </td>
                                    <td>
                                        *File Name
                                    </td>
                                    <td>
                                        <b>:</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFileName" TabIndex="3" runat="server" ValidationGroup="M1" CssClass="gCtrTxt UpperCase"
                                            MaxLength="50" TextMode="singleLine" Width="215px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFFileName" runat="server" ValidationGroup="M1" Display="None"
                                            ErrorMessage="Please enter File Name.." ControlToValidate="txtFileName" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        File Group
                                    </td>
                                    <td>
                                        <b>:</b>
                                    </td>
                                    <td>
                                        <cc1:ComboBox ID="cmbFileGroup" EmptyText="select file group" runat="server" Width="197px"
                                            TabIndex="4" Height="250px" MenuWidth="200px" DataTextField="MST_CODE" DataValueField="MST_CODE"
                                            OnLoadingItems="cmbFileGroup_LoadingItems">
                                            <HeaderTemplate>
                                                <div class="header c2">
                                                    File Group</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c2">
                                                    <%# Eval("MST_CODE")%></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc1:ComboBox>
                                    </td>
                                    <td>
                                        File Reference
                                    </td>
                                    <td>
                                        <b>:</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFileReference" TabIndex="5" runat="server" ValidationGroup="M1"
                                            CssClass="gCtrTxt UpperCase" MaxLength="50" TextMode="singleLine" Width="215px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFVFileReference" runat="server" ValidationGroup="M1"
                                            Display="None" ErrorMessage="Please enter File Reference.." ControlToValidate="txtFileReference"
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        *File Type
                                    </td>
                                    <td>
                                        <b>:</b>
                                    </td>
                                    <td>
                                        <cc1:ComboBox ID="cmbFileType" EmptyText="select file type" runat="server" Width="197px"
                                            TabIndex="6" Height="250px" MenuWidth="200px" DataTextField="MST_CODE" DataValueField="MST_CODE"
                                            OnLoadingItems="cmbFileType_LoadingItems">
                                            <HeaderTemplate>
                                                <div class="header c2">
                                                    File Type</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c2">
                                                    <%# Eval("MST_CODE")%></div>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc1:ComboBox>
                                    </td>
                                    <td>
                                        *Select File
                                    </td>
                                    <td>
                                        <b>:</b>
                                    </td>
                                    <td>
                                        <input id="tPhoto" class="gCtrHindi" tabindex="7" type="file" onchange="func1();"
                                            name="tPhoto" runat="server" />
                                        <asp:RequiredFieldValidator ID="RFtPhoto" runat="server" ValidationGroup="M1" Display="None"
                                            ErrorMessage="Please select file." ControlToValidate="tPhoto" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="trView" runat="server">
                                    <td>
                                        Click for view
                                    </td>
                                    <td>
                                        <b>:</b>
                                    </td>
                                    <td colspan="4">
                                        <asp:LinkButton ID="lbtnView" runat="server" TabIndex="8" OnClick="lbtnView_Click">Click here to view your file...</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Brief Description
                                    </td>
                                    <td>
                                        <b>:</b>
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="gCtrTxt" Width="505px"
                                            TabIndex="9" TextMode="multiLine" Rows="2" MaxLength="200" Height="55px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
</table>
