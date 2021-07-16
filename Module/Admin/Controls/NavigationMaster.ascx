<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavigationMaster.ascx.cs"
    Inherits="Module_Admin_Controls_NavigationMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Src="AddChildModule.ascx" TagName="AddChildModule" TagPrefix="uc1" %>
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
        width: 150px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .tContentArial
    {
        margin-left: 0px;
    }
</style>

<script language="javascript" type="text/javascript">
    function func1() {
        document.getElementById("imgPhoto").style.display = "";
        document.getElementById("imgPhoto").src = document.getElementById("ctl00_cphBody_NavigationMaster1_tPhoto").value;
    }
</script>

<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table align="left" class="tContentArial" width="100%">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table style="text-align: left" class="tContentArial" cellspacing="0" cellpadding="0">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" ValidationGroup="M1" OnClick="imgbtnSave_Click" OnClientClick="if (!confirm('Are you want to Save?')) { return false; }">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Width="48" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Update" ValidationGroup="M1" OnClick="imgbtnUpdate_Click" OnClientClick="if (!confirm('Are you Want to Update?')) { return false; }">
                        </asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" TabIndex="7" OnClick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" ValidationGroup="M1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                            TabIndex="6" OnClick="imgbtnDelete_Click2"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you sure Want To Clear ?')) { return false; }">
                        </asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClientClick="if (!confirm('Are you want to print?')) { return false; }">
                        </asp:ImageButton>
                    </td>
                    <td width="48">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you sure to Exit?')) { return false; }">
                        </asp:ImageButton>
                    </td>
                    <td style="width: 48px">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="tRowColorAdmin">Add Module Navigation</b>
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
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <table>
                <tr style="font-weight: bold">
                    <td valign="top" align="right">
                        *Parent Module Name <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:DropDownList ID="ddlParenModuleName" TabIndex="1" runat="server" Width="255px"
                            AutoPostBack="true" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlParenModuleName_SelectedIndexChanged1"
                            CssClass="SmallFont">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="M1"
                            ErrorMessage="*" Display="dynamic" ControlToValidate="ddlParenModuleName"></asp:RequiredFieldValidator>
                        <cc1:ComboBox ID="cmbNavigation" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="cmbNavigation_LoadingItems" DataTextField="NAV_NAME" DataValueField="NAV_ID"
                            Width="255px" MenuWidth="500px" Height="200px" CssClass="SmallFont" TabIndex="1"
                            EmptyText="Find Navigation" OnSelectedIndexChanged="cmbNavigation_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Parent Module Name</div>
                                <div class="header c2">
                                    Child Module Name</div>
                                <div class="header c2">
                                    Navigation Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MDL_NAME") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("CHILD_MDL_NAME") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("NAV_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc1:ComboBox>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td valign="top" align="right">
                        *Child Module Name <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:DropDownList ID="ddlChildModuleName" TabIndex="2" runat="server" Width="255px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlChildModuleName_SelectedIndexChanged"
                            CssClass="SmallFont">
                        </asp:DropDownList>
                        <strong></strong>
                    </td>
                </tr>
                <tr style="font-weight: bold; color: #000000">
                    <td valign="top" align="right">
                        *Navigation Name <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:TextBox ID="txtNavigationName" TabIndex="3" runat="server" Width="250px" CssClass="SmallFont"
                            MaxLength="100" OnTextChanged="txtNavigationName_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="M1"
                            ErrorMessage="*" Display="dynamic" ControlToValidate="txtNavigationName"></asp:RequiredFieldValidator>
                        <cc1:AutoCompleteExtender TargetControlID="txtNavigationName" CompletionInterval="1000"
                            ID="AutoCompleteExtender1" UseContextKey="true" MinimumPrefixLength="2" ServiceMethod="GetNavByChildModule"
                            ServicePath="~/AutoComplete.asmx" runat="server">
                        </cc1:AutoCompleteExtender>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td valign="top" align="right">
                        <strong>*Navigation URL </strong><b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:TextBox ID="txtNavigationUrl" TabIndex="3" runat="server" Width="250px" CssClass="SmallFont"
                            MaxLength="250"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="M1"
                            ErrorMessage="*" Display="dynamic" ControlToValidate="txtNavigationUrl"></asp:RequiredFieldValidator>
                        <asp:LinkButton ID="lnkTree" runat="server" Text="Select Navigation" OnClick="lnkTree_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td valign="top" align="right">
                        Module Images <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <input id="tPhoto" class="gCtrHindi SmallFont" tabindex="4" type="file" onchange="func1();"
                            name="tPhoto" runat="server" />
                        <img style="display: none" id="imgPhoto" class="gCtrHindi" height="30" width="30"
                            border="0" />
                        <asp:Image ID="imgNav" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right">
                        *Display Order <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:TextBox ID="txtDisplayOrder" TabIndex="3" runat="server" Width="89px" CssClass="SmallFont"
                            MaxLength="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="M1"
                            ErrorMessage="*" Display="dynamic" ControlToValidate="txtDisplayOrder"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ValidationGroup="M1" ErrorMessage="Pls enter numeric value between 1 to 100"
                            Display="dynamic" ControlToValidate="txtDisplayOrder" Type="Integer" MinimumValue="1"
                            MaximumValue="100"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right">
                        Select Tab <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:DropDownList ID="ddlTabMaster" runat="server" AppendDataBoundItems="True" CssClass="SmallFont">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right">
                        Remarks <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:TextBox ID="txtRemarks" TabIndex="6" runat="server" Width="450px" CssClass="SmallFont"
                            MaxLength="250" TextMode="multiLine" Rows="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 18%" valign="top" align="right">
                        Status <b>:</b>
                    </td>
                    <td valign="top" align="left">
                        <asp:CheckBox ID="chk_Status" TabIndex="7" runat="server" CssClass="SmallFont"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" colspan="2">
                        <asp:GridView ID="gvNavigation" runat="server" AllowPaging="true" PageSize="10" AllowSorting="true"
                            AutoGenerateColumns="false" OnSelectedIndexChanged="gvNavigation_SelectedIndexChanged"
                            OnRowCommand="gvNavigation_RowCommand1" OnPageIndexChanging="gvNavigation_PageIndexChanging1"
                            Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Image" ItemStyle-VerticalAlign="top" ItemStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <img src="../ShowImage.aspx?NAV_ID=<%# Eval("NAV_ID") %> &amp;ilen=<%# Eval("POSTED_LENGTH") %>"
                                            width="20px" height="20px" alt="" />
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
                                <asp:BoundField DataField="NAV_NAME" HeaderText="Navigation Name" ItemStyle-VerticalAlign="top"
                                    ItemStyle-HorizontalAlign="left">
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NAV_URL" HeaderText="Navigation URL" ItemStyle-VerticalAlign="top"
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
                                        <asp:ImageButton ID="ImgEdit" CommandArgument='<%# Eval("NAV_ID") %>' CommandName="RecordEdit"
                                            ImageUrl="~/CommonImages/edit1.jpg" runat="server" CssClass="ContolStyle"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="left" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="right" colspan="2">
                        <asp:Panel ID="pnlTreeview" runat="server" BackColor="White" BorderColor="Blue" BorderStyle="Groove"
                            BorderWidth="1px" HorizontalAlign="Left" ScrollBars="Both" Width="600px" Height="300px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblsaitex" runat="server" Text="Saitex"></asp:Label>
                                        <asp:TreeView ID="TreeView1" runat="server" Font-Size="Small" NodeWrap="True" ShowLines="True"
                                            ToolTip="Click on " OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" Width="500px">
                                        </asp:TreeView>
                                        <asp:Button ID="Button1" CssClass="SmallFont" runat="server" Text="Close Me" OnClick="Button1_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <cc1:ModalPopupExtender ID="mpeNavigation" BackgroundCssClass="modalBackground" PopupControlID="pnlTreeview"
                    DropShadow="true" runat="server" TargetControlID="lnkTree">
                </cc1:ModalPopupExtender>
            </table>
        </td>
    </tr>
</table>
 <%--</ContentTemplate>
</asp:UpdatePanel>--%>