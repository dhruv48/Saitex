<%@ Control Language="C#" AutoEventWireup="true" CodeFile="News_Master.ascx.cs" Inherits="Module_Admin_Controls_News_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
</style>

<script language="javascript" type="text/javascript">

		
</script>

<table align="left" cellpadding="0" cellspacing="0" class="tContentArial">
    <tr>
        <td align="right" class="td" style="text-align: left" valign="top">
            <table cellpadding="0" cellspacing="0" class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="NM" />
                    </td>
                    <td id="tdUpdate" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" Width="48"
                            OnClientClick="javascript:return window.confirm('Are you sure you want to Update this Form')" />
                    </td>
                    <td align="left" style="height: 46px" width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" OnClientClick="javascript:return window.confirm('Are you sure you want to Clear this Form')" />
                    </td>
                    <td align="left" style="width: 42px; height: 46px">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="43px" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48px" />
                    </td>
                    <td width="48">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" OnClientClick="javascript:return window.confirm('Are you sure you want to Exit from this Form')" />
                    </td>
                    <td style="width: 48px">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td">
            <b class="tRowColorAdmin">News Master</b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" style="height: 16px" valign="top">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
            </asp:Label>
                &nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr style="font-weight: bold">
                    <td align="right" valign="top" width="20%">
                        News Id
                    </td>
                    <td align="center" valign="top" width="2%">
                        &nbsp;
                    </td>
                    <td align="left" valign="top" width="78%">
                        <asp:TextBox ID="txtNewsId" runat="server" CssClass="SmallFont TextBoxNo TextBoxDisplay"
                            MaxLength="100" TabIndex="1" Width="230px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td align="right" valign="top" width="20%">
                        News Heading
                    </td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="78%">
                        <asp:TextBox ID="txtNewsHeading" runat="server" CssClass="SmallText" MaxLength="50"
                            TabIndex="1" Width="230px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Dear Please Enter News Heading"
                            ControlToValidate="txtNewsHeading" Display="None" ValidationGroup="NM"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="20%">
                        &nbsp;News Description
                    </td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="78%">
                        <obout:Editor ID="txtNewsBody" Submit="false" PreviewMode="true" runat="server">
                        </obout:Editor>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="20%">
                        Is Hot
                    </td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="78%">
                        <asp:CheckBox ID="chkIshot" runat="server" TabIndex="5" />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="20%">
                        Active Status
                    </td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="78%">
                        <asp:CheckBox ID="chk_Status" runat="server" TabIndex="5" />
                    </td>
                </tr>
                <tr>
                    <td id="tdbtn" runat="server" align="center" colspan="3" visible="false">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 100%" class="td">
            <table align="center" border="0" width="100%" cellpadding="0" cellspacing="0" class="tContentArial">
                <tr>
                    <td align="left" class="td">
                        <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label
                            ID="lblTotalRecord" runat="server"></asp:Label>
                        </b>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <br />
                        <asp:GridView ID="grdNews" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="grdNews_PageIndexChanging"
                            OnRowCommand="grdNews_RowCommand" OnRowDataBound="grdNews_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="NEWS_ID" HeaderText="S.No" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NEWS_HEAD" HeaderText=" News Heading" ItemStyle-HorizontalAlign="center"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NEWS_DESC" HeaderText="Descripiton" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Is Active">
                                    <ItemTemplate>
                                        <asp:Label ID="lblActive" runat="server" Text='<%# Bind("IS_ACTIVE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Is Hot">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHot" runat="server" Text='<%# Bind("IS_HOT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="top">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("NEWS_ID") %>'
                                            CommandName="ImageEdit" CssClass="ContolStyle" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="top">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="ImgDelete" runat="server" CommandArgument='<%# Eval("NEWS_ID") %>'
                                            CommandName="ImageDelete" CssClass="ContolStyle" Text="Delete" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this News?')" /></asp:LinkButton>
                                        <%--                                       <asp:ImageButton ID="ImgDelete" runat="server" CommandArgument='<%# Eval("NEWS_ID") %>'
                                            CommandName="ImageDelete" CssClass="ContolStyle" ImageUrl="~/CommonImages/del6.png"
                                            OnClientClick="javascript:return window.confirm('Are you sure you want to delete this module?')" />--%>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="left" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="right" valign="top" visible="false">
            <%--<table id="tdSearch" runat="server" border="1" cellpadding="0" cellspacing="0" 
                    class="tContentArial">
                    <tr>
                        <td align="center" style="width: 100%" valign="top">
                            <b>Search Pannel</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top">
                            Active/De-Active</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top">
                            Deleted/Not-Deleted</td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 100%" valign="top">
                            <asp:RadioButtonList ID="radDeletedNNotDelted" runat="server" RepeatColumns="1" 
                                RepeatDirection="Horizontal" RepeatLayout="flow" TabIndex="11">
                                <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                <asp:ListItem Value="0">Not-Deleted</asp:ListItem>
                                <asp:ListItem Value="1">Deleted</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 100%" valign="top">
                            <asp:Button ID="btnView" runat="server" CssClass="AButton" 
                                OnClick="btnView_Click" TabIndex="10" Text="View" Width="75px" />
                        </td>
                    </tr>
                </table>--%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Fields Are Mandatory"
                ShowMessageBox="True" ShowSummary="False" ValidationGroup="NM" />
        </td>
    </tr>
</table>
<%--<asp:LinkButton ID="lbtnRedMain" runat="server"></asp:LinkButton>--%>
<%--<asp:Panel ID="pnlRed" runat="server" BackColor="White" BorderColor="Blue" BorderStyle="Groove"
    BorderWidth="1px" Height="100px" HorizontalAlign="Center" Width="125px">
    <table style="width: 85px; height: 73px">
        <tr>
            <td style="height: 36px">
                <asp:Label ID="lblRedirect" runat="server" Height="31px" Width="175px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 22px">
                <asp:Button ID="Button1" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Groove"
                    BorderWidth="1px"  Text="Redirect" Width="64px" />
                &nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Groove"
                    BorderWidth="1px"  Text="Cancel" Width="62px" />
            </td>
        </tr>
    </table>
    <br />
    &nbsp; &nbsp;&nbsp;<cc1:ModalPopupExtender ID="mpeRed0" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="true" PopupControlID="pnlRed" TargetControlID="lbtnRedMain">
    </cc1:ModalPopupExtender>
</asp:Panel>--%>
<modalpopupextender id="mpeRed" runat="server" backgroundcssclass="modalBackground"
    dropshadow="true" popupcontrolid="pnlRed" targetcontrolid="lbtnRedMain">
    </modalpopupextender>
