<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModuleMaster.ascx.cs"
    Inherits="Module_Admin_Controls_ModuleMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

			function func1()
			{
				document.getElementById("imgPhoto").style.display="";
				document.getElementById("imgPhoto").src=document.getElementById("ctl00_cphBody_ModuleMaster1_tPhoto").value;
			}
			function GetRedirectPopUp()
			{
			document.getElementById('<%= lbtnRedMain.ClientID %>').click();
			}
			

</script>

<table align="left" cellpadding="0" cellspacing="0" class="tContentArial">
    <tr>
        <td align="right" class="td" style="text-align: left" valign="top">
            <table cellpadding="0" cellspacing="0" class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="M1" />
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
            <b class="tRowColorAdmin">Add Module</b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" style="height: 16px" valign="top">
            <span class="Mode">You are in
                <asp:Label ID="lblMode" runat="server">
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
                        *Module Name
                    </td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="78%">
                        <asp:TextBox ID="txtModuleName" runat="server" CssClass="gCtrTxt" MaxLength="100"
                            TabIndex="1" Width="230px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtModuleName"
                            Display="dynamic" ErrorMessage="*" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td align="right" valign="top" width="20%">
                        Module Images
                    </td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="78%">
                        <input id="tPhoto" runat="server" class="gCtrHindi" name="tPhoto" onchange="func1();"
                            tabindex="2" type="file" onclick="return tPhoto_onclick()" />
                        &nbsp;
                        <img id="imgPhoto" border="0" class="gCtrHindi" height="30" style="display: none"
                            width="30" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tPhoto"
                            Display="dynamic" ErrorMessage="*" ValidationGroup="M1"></asp:RequiredFieldValidator>
                        <asp:Image ID="imgNav" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="20%">
                        *Display Order
                    </td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="78%">
                        <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="TextBoxNo" MaxLength="3"
                            TabIndex="3" Width="50px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDisplayOrder"
                            Display="dynamic" ErrorMessage="*" ValidationGroup="M1"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDisplayOrder"
                            Display="dynamic" ErrorMessage="Pls enter numeric value between 1 to 100" MaximumValue="100"
                            MinimumValue="1" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="20%">
                        Remarks
                    </td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="78%">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="gCtrTxt" MaxLength="250" Rows="2"
                            TabIndex="4" Width="450px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="20%">
                        Status
                    </td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="78%">
                        &nbsp;<asp:CheckBox ID="chk_Status" runat="server" TabIndex="5" />
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
                        <asp:GridView ID="gvAddModule" runat="server" AllowPaging="true" AllowSorting="true"
                            AutoGenerateColumns="false" Width="100%" OnPageIndexChanging="gvAddModule_PageIndexChanging"
                            OnRowCommand="gvAddModule_RowCommand" PageSize="10">
                            <Columns>
                                  <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" 
                                        ItemStyle-Width="25px">
                                        <ItemTemplate>
                                             <%--       <%# Eval("POSTED_LENGTH")%>--%>
                                             <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>

<ItemStyle VerticalAlign="Top" Width="25px"></ItemStyle>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="center" ItemStyle-VerticalAlign="top"
                                    ItemStyle-Width="75px">
                                    <ItemTemplate>
                                        <img alt="" height="20px" src='../ShowImage.aspx?MDL_ID=<%# Eval("MDL_ID") %> &amp;ilen;=<%# Eval("POSTED_LENGTH") %>'
                                            width="20px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="75px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MDL_NAME" HeaderText="Module Name" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DISP_ODR" HeaderText="Display Order" ItemStyle-HorizontalAlign="center"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-VerticalAlign="top">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="top">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("MDL_ID") %>'
                                            CommandName="ImageEdit" CssClass="ContolStyle" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="top">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" CommandArgument='<%# Eval("MDL_ID") %>'
                                            CommandName="ImageDelete" CssClass="ContolStyle" ImageUrl="~/CommonImages/del6.png"
                                            OnClientClick="javascript:return window.confirm('Are you sure you want to delete this module?')" />
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
        </td>
    </tr>
</table>
<asp:LinkButton ID="lbtnRedMain" runat="server"></asp:LinkButton>
<asp:Panel ID="pnlRed" runat="server" BackColor="White" BorderColor="Blue" BorderStyle="Groove"
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
                    BorderWidth="1px" OnClick="btnRedirect_Click" Text="Redirect" Width="64px" />
                &nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Groove"
                    BorderWidth="1px" OnClick="btnCancelRed_Click" Text="Cancel" Width="62px" />
            </td>
        </tr>
    </table>
    <br />
    &nbsp; &nbsp;&nbsp;<cc1:ModalPopupExtender ID="mpeRed0" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="true" PopupControlID="pnlRed" TargetControlID="lbtnRedMain">
    </cc1:ModalPopupExtender>
</asp:Panel>
<cc1:ModalPopupExtender id="mpeRed" runat="server" backgroundcssclass="modalBackground"
    dropshadow="true" popupcontrolid="pnlRed" targetcontrolid="lbtnRedMain">
    </cc1:ModalPopupExtender>
