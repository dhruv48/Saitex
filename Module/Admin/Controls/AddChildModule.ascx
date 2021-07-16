<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddChildModule.ascx.cs"
    Inherits="Module_Admin_Controls_AddChildModule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>

<script language="javascript" type="text/javascript">
			function func1()
			{
				document.getElementById("ctl00_cphBody_AddChildModule1_tPhoto").style.display="";
				document.getElementById("ctl00_cphBody_AddChildModule1_tPhoto").src=document.getElementById("ctl00_cphBody_AddChildModule1_tPhoto").value;
			}
</script>

<table cellpadding="0" cellspacing="0" border="0" class="tContentArial">
    <tr>
        <td align="center">
            <table class="tContentArial" cellspacing="0" cellpadding="0" align="center">
                <tbody>
                    <tr>
                        <td class="td" valign="top" align="left">
                            <table class="tContentArial" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td style="height: 46px" id="tdSave" width="48" runat="server">
                                            <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ValidationGroup="M1"
                                                ToolTip="Save" ImageUrl="~/CommonImages/save.jpg " OnClientClick="javascript:return window.confirm('Are you sure you want to Save')">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="height: 46px" id="tdUpdate" width="48" runat="server">
                                            <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" Width="48"
                                                Height="41" ValidationGroup="M1" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                                OnClientClick="javascript:return window.confirm('Are you sure you want to Update')">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="height: 46px" width="48">
                                            <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" Width="48"
                                                Height="41" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg" OnClientClick="javascript:return window.confirm('Are you sure you want to Clear')">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 48px; height: 46px">
                                            <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" Width="52px"
                                                Height="41" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"></asp:ImageButton>
                                        </td>
                                        <td width="48">
                                            <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                                                ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41" OnClientClick="javascript:return window.confirm('Are you sure you want to Exit')">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 48px">
                                            <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                                Width="48" Height="41"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="TableHeader td" align="center">
                            <b class="tRowColorAdmin">Add Child Module</b>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" valign="top" align="left" style="height: 16px">
                            <span class="Mode">
                                <asp:Label ID="lblMode" runat="server">
                                </asp:Label></span>
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
                                <tr>
                                    <td valign="top" align="right">
                                        *Parent Module Name
                                    </td>
                                    <td valign="top" align="center">
                                        <b>:</b>
                                    </td>
                                    <td valign="top" align="left" class="style1">
                                        <cc2:OboutDropDownList ID="ddlParenModuleName" TabIndex="1" runat="server" MenuWidth="250px"
                                            Width="155px">
                                        </cc2:OboutDropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="M1"
                                            ErrorMessage="*" Display="dynamic" ControlToValidate="ddlParenModuleName"></asp:RequiredFieldValidator>
                                    </td>
                                    <td valign="top" align="right">
                                        *Child Module Name
                                    </td>
                                    <td valign="top" align="center">
                                        <b>:</b>
                                    </td>
                                    <td valign="top" align="left" class="style2">
                                        <strong>
                                            <asp:TextBox ID="txtChildModuleName" TabIndex="2" runat="server" CssClass="gCtrTxt"
                                                MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                                    runat="server" ValidationGroup="M1" ErrorMessage="*" Display="dynamic" ControlToValidate="txtChildModuleName"></asp:RequiredFieldValidator></strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="M1"
                                            ErrorMessage="*" Display="dynamic" ControlToValidate="tPhoto"></asp:RequiredFieldValidator>
                                        Module Images
                                    </td>
                                    <td valign="top" align="center">
                                        <b>:</b>
                                    </td>
                                    <td valign="top" align="left" class="style1">
                                        <input id="tPhoto" class="gCtrHindi" tabindex="3" type="file" onchange="func1();"
                                            name="tPhoto" runat="server" />&nbsp;
                                    </td>
                                    <td valign="top" align="right">
                                        *Display Order
                                    </td>
                                    <td valign="top" align="center">
                                        <b>:</b>
                                    </td>
                                    <td valign="top" align="left" class="style2">
                                        <asp:TextBox ID="txtDisplayOrder" TabIndex="3" runat="server" CssClass="gCtrTxt"
                                            MaxLength="4"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                runat="server" ValidationGroup="M1" ErrorMessage="*" Display="dynamic" ControlToValidate="txtDisplayOrder"></asp:RequiredFieldValidator><asp:RangeValidator
                                                    ID="RangeValidator1" runat="server" ValidationGroup="M1" ErrorMessage="Pls enter numeric value between 1 to 100"
                                                    Display="dynamic" ControlToValidate="txtDisplayOrder" MaximumValue="100" MinimumValue="1"
                                                    Type="Integer"></asp:RangeValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        Remarks
                                    </td>
                                    <td valign="top" align="center">
                                        <b>:</b>
                                    </td>
                                    <td valign="top" align="left" colspan="4">
                                        <asp:TextBox ID="txtRemarks" TabIndex="5" runat="server" Width="496px" CssClass="gCtrTxt"
                                            MaxLength="250" Rows="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">
                                        Status
                                    </td>
                                    <td valign="top" align="center">
                                        <b>:</b>
                                    </td>
                                    <td valign="top" align="left" colspan="4">
                                        <asp:CheckBox ID="chk_Status" TabIndex="6" runat="server"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="6" id="tdbtn" runat="server" visible="false">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <table class="tContentArial" cellspacing="0" cellpadding="0" align="center" border="0">
                                <tbody>
                                    <tr>
                                        <td align="left" class="td">
                                            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="1" class="td">
                                            <asp:GridView ID="gvChildAddModule" runat="server" AllowPaging="true" PageSize="10"
                                                AllowSorting="true" AutoGenerateColumns="false" OnRowCommand="gvChildAddModule_RowCommand"
                                                OnPageIndexChanging="gvChildAddModule_PageIndexChanging" 
                                                CssClass="GridView">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S No." ItemStyle-VerticalAlign="top">
                                                        <ItemTemplate>
                                                          <%--  <%# Eval("POSTED_LENGTH")%>--%>
                                                          <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Image" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <img  alt="" height="20px" src='../ShowImage.aspx?CHILD_MDL_ID=<%# Eval("CHILD_MDL_ID") %> &amp;ilen=<%# Eval("POSTED_LENGTH") %>'
                                                                width="20px" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="MDL_NAME" HeaderText="Parent Module Name" ItemStyle-VerticalAlign="top"
                                                        ItemStyle-HorizontalAlign="left">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CHILD_MDL_NAME" HeaderText="Child Module Name" ItemStyle-VerticalAlign="top"
                                                        ItemStyle-HorizontalAlign="left">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DISP_ODR" HeaderText="Display Order" ItemStyle-VerticalAlign="top"
                                                        ItemStyle-HorizontalAlign="center">
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" ItemStyle-VerticalAlign="top"
                                                        ItemStyle-HorizontalAlign="left">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-VerticalAlign="top"
                                                        ItemStyle-HorizontalAlign="left">
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="top">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgEdit" CommandArgument='<%# Eval("CHILD_MDL_ID") %>' CommandName="RecordEdit"
                                                                ImageUrl="~/CommonImages/edit1.jpg" runat="server" CssClass="ContolStyle"></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="top">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgDelete" CommandArgument='<%# Eval("CHILD_MDL_ID") %>' CommandName="RecordDelete"
                                                                ImageUrl="~/CommonImages/del6.png" runat="server" CssClass="ContolStyle" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this child module?')" />
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="left" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
</table>
<asp:LinkButton ID="lbtnRedMain" runat="server"></asp:LinkButton>
<asp:Panel ID="pnlRed" runat="server" Height="100px" Width="125px" BackColor="White"
    BorderColor="Blue" BorderStyle="Groove" BorderWidth="1px" HorizontalAlign="Center">
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
    &nbsp; &nbsp;&nbsp;
</asp:Panel>
<cc1:ModalPopupExtender ID="mpeRed" BackgroundCssClass="modalBackground" PopupControlID="pnlRed"
    DropShadow="true" runat="server" TargetControlID="lbtnRedMain">
</cc1:ModalPopupExtender>
