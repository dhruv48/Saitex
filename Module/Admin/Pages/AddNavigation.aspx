<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/CommonMaster/admin.master"
    AutoEventWireup="true" CodeFile="AddNavigation.aspx.cs" Inherits="Admin_AddNavigation"
    Title="Add Navigation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">

    <script language="javascript" type="text/javascript">
			function func1()
			{
				document.getElementById("imgPhoto").style.display="";
				document.getElementById("imgPhoto").src=document.getElementById("ctl00_cphBody_tPhoto").value;
			}
    </script>

    <table class="tContentArial" cellspacing="0" cellpadding="0" align="Left">
        <tbody>
            <tr>
                <td class="td" valign="top" align="left">
                    <table style="text-align: left" class="tContentArial" cellspacing="0" cellpadding="0">
                        <tbody>
                            <tr>
                                <td id="tdSave" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                        ToolTip="Save" ValidationGroup="M1"></asp:ImageButton>
                                </td>
                                <td id="tdUpdate" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" Width="48"
                                        Height="41" ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" Width="48"
                                        Height="41" ImageUrl="~/CommonImages/clear.jpg" ToolTip="Clear"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" Width="48"
                                        Height="41" ImageUrl="~/CommonImages/link_print.png" ToolTip="Print"></asp:ImageButton>
                                </td>
                                <td width="48">
                                    <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                                        ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41"></asp:ImageButton>
                                </td>
                                <td style="width: 48px">
                                    <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                                        ImageUrl="~/CommonImages/link_help.png" Width="48" Height="41"></asp:ImageButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <b class="tRowColorAdmin">Add Module Navigation</b>
                </td>
            </tr>
            <tr>
                <td class="td" valign="top" align="left" style="height: 16px">
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
                    </asp:Label>&nbsp;Mode </span>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr style="font-weight: bold">
                            <td valign="top" align="right">
                                *Parent Module Name <b>:</b></td>
                            <td valign="top" align="left">
                                <asp:DropDownList ID="ddlParenModuleName" TabIndex="1" runat="server" Width="255px"
                                    OnSelectedIndexChanged="ddlParenModuleName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="M1"
                                    ErrorMessage="*" Display="dynamic" ControlToValidate="ddlParenModuleName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr style="color: #000000">
                            <td valign="top" align="right">
                                *Child Module Name <b>:</b></td>
                            <td valign="top" align="left">
                                <asp:DropDownList ID="ddlChildModuleName" TabIndex="2" runat="server" Width="255px">
                                </asp:DropDownList><strong> </strong>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="M1"
                                    ErrorMessage="*" Display="dynamic" ControlToValidate="ddlChildModuleName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr style="font-weight: bold; color: #000000">
                            <td valign="top" align="right">
                                *Navigation Name <b>:</b></td>
                            <td valign="top" align="left">
                                <asp:TextBox ID="txtNavigationName" TabIndex="3" runat="server" Width="250px" CssClass="gCtrTxt"
                                    MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="M1"
                                    ErrorMessage="*" Display="dynamic" ControlToValidate="txtNavigationName"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr style="color: #000000">
                            <td valign="top" align="right">
                                <strong>*Navigation URL </strong><b>:</b></td>
                            <td style="font-weight: bold" valign="top" align="left">
                                <asp:TextBox ID="txtNavigationUrl" TabIndex="3" runat="server" Width="450px" CssClass="gCtrTxt"
                                    MaxLength="250"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="M1"
                                    ErrorMessage="*" Display="dynamic" ControlToValidate="txtNavigationUrl"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr style="color: #000000">
                            <td valign="top" align="right">
                                Module Images <b>:</b></td>
                            <td valign="top" align="left">
                                <input id="tPhoto" class="gCtrHindi" tabindex="4" type="file" onchange="func1();"
                                    name="tPhoto" runat="server" />
                                &nbsp;<img style="display: none" id="imgPhoto" class="gCtrHindi" height="30" width="30"
                                    border="0" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="M1"
                                    ErrorMessage="*" Display="dynamic" ControlToValidate="tPhoto"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                *Display Order <b>:</b></td>
                            <td valign="top" align="left">
                                <asp:TextBox ID="txtDisplayOrder" TabIndex="3" runat="server" Width="50px" CssClass="gCtrTxt"
                                    MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="M1"
                                    ErrorMessage="*" Display="dynamic" ControlToValidate="txtDisplayOrder"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ValidationGroup="M1" ErrorMessage="Pls enter numeric value between 1 to 100"
                                    Display="dynamic" ControlToValidate="txtDisplayOrder" Type="Integer" MinimumValue="1"
                                    MaximumValue="100"></asp:RangeValidator></td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                Select Tab :</td>
                            <td valign="top" align="left">
                                <asp:DropDownList ID="ddlTabMaster" runat="server" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right">
                                Remarks <b>:</b></td>
                            <td valign="top" align="left">
                                <asp:TextBox ID="txtRemarks" TabIndex="6" runat="server" Width="450px" CssClass="gCtrTxt"
                                    MaxLength="250" TextMode="multiLine" Rows="2"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 18%" valign="top" align="right">
                                Status <b>:</b></td>
                            <td valign="top" align="left">
                                &nbsp;<asp:CheckBox ID="chk_Status" TabIndex="7" runat="server"></asp:CheckBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <td class="td" align="center">
                <table cellpadding="0" cellspacing="0" border="0" align="center" class="tContentArial">
                    <tr>
                        <td align="left">
                            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvNavigation" runat="server" AllowPaging="true" PageSize="10" AllowSorting="true"
                                AutoGenerateColumns="false" OnRowCommand="gvNavigation_RowCommand" OnPageIndexChanging="gvNavigation_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Image" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="center">
                                        <ItemTemplate>
                                            <img src="./ShowImage.aspx?navigationModuleImageId=<%# Eval("IN_MODULENAVIGATIONID") %> &ilen=<%# Eval("in_PostedLength") %>"
                                                width="20px" height="20px" alt="" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="vc_ModuleName" HeaderText="Parent Module Name" ItemStyle-VerticalAlign="top"
                                        ItemStyle-HorizontalAlign="left" />
                                    <asp:BoundField DataField="VC_CHILDMODULENAME" HeaderText="Child Module Name" ItemStyle-VerticalAlign="top"
                                        ItemStyle-HorizontalAlign="left" />
                                    <asp:BoundField DataField="VC_NAVIGATIONNNAME" HeaderText="Navigation Name" ItemStyle-VerticalAlign="top"
                                        ItemStyle-HorizontalAlign="left" />
                                    <asp:BoundField DataField="VC_NAVIGATEURL" HeaderText="Navigation URL" ItemStyle-VerticalAlign="top"
                                        ItemStyle-HorizontalAlign="left" />
                                    <asp:BoundField DataField="in_displayOrder" HeaderText="Display Order" ItemStyle-VerticalAlign="top"
                                        ItemStyle-HorizontalAlign="center" />
                                    <asp:BoundField DataField="vc_Remarks" HeaderText="Remarks" ItemStyle-VerticalAlign="top"
                                        ItemStyle-HorizontalAlign="left" />
                                    <asp:BoundField DataField="activeDes" HeaderText="Status" ItemStyle-VerticalAlign="top"
                                        ItemStyle-HorizontalAlign="left" />
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="top">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgEdit" CommandArgument='<%# Eval("IN_MODULENAVIGATIONID") %>'
                                                CommandName="RecordEdit" ImageUrl="~/CommonImages/edit1.jpg" runat="server"
                                                CssClass="ContolStyle"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="top">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgDelete" CommandArgument='<%# Eval("IN_MODULENAVIGATIONID") %>'
                                                CommandName="RecordDelete" ImageUrl="~/CommonImages/del6.png" runat="server"
                                                CssClass="ContolStyle" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this navigation?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle HorizontalAlign="left" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
            <tr>
                <td align="left" valign="top" visible="false">
                    <table cellpadding="0" cellspacing="0" id="tdSerch" runat="server" border="1" width="175"
                        visible="false" class="tContentArial">
                        <tr>
                            <td align="center" width="100%" valign="top">
                                <b>Search Pannel</b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" valign="top">
                                Parent Module Name
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" valign="top">
                                <asp:DropDownList ID="ddlParenModuleNameRight" runat="server" Width="175px" TabIndex="11"
                                    OnSelectedIndexChanged="ddlParenModuleNameRight_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" valign="top">
                                Child Module Name
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" valign="top">
                                <asp:DropDownList ID="ddlChildModuleNameRight" runat="server" Width="175px" TabIndex="12">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" valign="top">
                                Active/De-Active</td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" valign="top">
                                <asp:RadioButtonList ID="radActiveDeActive" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                    RepeatLayout="flow" TabIndex="13">
                                    <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">De-Active</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" valign="top">
                                Deleted/Not-Deleted</td>
                        </tr>
                        <tr>
                            <td align="left" width="100%" valign="top">
                                <asp:RadioButtonList ID="radDeletedNNotDelted" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                    RepeatLayout="flow" TabIndex="14">
                                    <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                                    <asp:ListItem Value="0">Not-Deleted</asp:ListItem>
                                    <asp:ListItem Value="1">Deleted</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" width="100%" valign="top">
                                <asp:Button ID="btnView" runat="server" CssClass="AButton" Width="75px" Text="View"
                                    OnClick="btnView_Click" TabIndex="15" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <asp:LinkButton ID="lbtnRedMain" runat="server"></asp:LinkButton>
    <asp:Panel ID="pnlRed" runat="server" Height="100px" Width="125px" BackColor="White"
        BorderColor="Blue" BorderStyle="Groove" BorderWidth="1px" HorizontalAlign="Center">
        <table style="width: 85px; height: 73px">
            <tr>
                <td colspan="3" rowspan="2" style="height: 36px">
                    <asp:Label ID="lblRedirect" runat="server" Height="31px" Width="175px"></asp:Label></td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td colspan="3" style="height: 22px">
                    <asp:Button ID="Button1" runat="server" OnClick="btnRedirect_Click" Text="Redirect"
                        BackColor="White" BorderColor="Navy" BorderStyle="Groove" BorderWidth="1px" Width="64px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="Button2" runat="server" OnClick="btnCancelRed_Click" Text="Cancel"
                        BackColor="White" BorderColor="Navy" BorderStyle="Groove" BorderWidth="1px" Width="62px" /></td>
            </tr>
        </table>
        <br />
        &nbsp; &nbsp;&nbsp;
    </asp:Panel>
    <cc1:ModalPopupExtender ID="mpeRed" BackgroundCssClass="modalBackground" PopupControlID="pnlRed"
        DropShadow="true" runat="server" TargetControlID="lbtnRedMain">
    </cc1:ModalPopupExtender>
</asp:Content>
