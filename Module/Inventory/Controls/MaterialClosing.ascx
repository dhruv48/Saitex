<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaterialClosing.ascx.cs"
    Inherits="Module_Inventory_Controls_MaterialClosing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../../CommonControls/LOV/DepartmentLOV.ascx" TagName="DepartmentLOV"
    TagPrefix="uc1" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    .style2
    {
        width: 149px;
    }
</style>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td">
            <table align="left" cellpadding="0" cellspacing="0">
                <tr>
                    <td id="tdUpdate" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click1" ToolTip="Update" ValidationGroup="M1" Width="48"
                            OnClientClick="if (!confirm('Are you sure to Update the record ?')) { return false; }" />
                    </td>
                    <td id="tdFind" runat="server" align="left" visible="false" width="48">
                        <asp:ImageButton ID="imgbtnFindTop" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" Width="48" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }"
                            OnClick="imgbtnFindTop_Click1" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" OnClientClick="if (!confirm('Are you sure to Exit ?')) { return false; }" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td">
            <b class="titleheading">Material Indent Closing</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                &nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td align="center" class="td">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center" class="td">
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td>
                        Department :
                    </td>
                    <td class="style2">
                        <uc1:DepartmentLOV ID="ddlDepartment" runat="server" />
                    </td>
                    <td align="right">
                        Indent No :
                    </td>
                    <td>
                        <asp:TextBox ID="txtIndNo" runat="server" OnTextChanged="txtIndNo_TextChanged" 
                            AutoPostBack="True"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td">
            <asp:GridView ID="gvYarnIndentClosing" runat="server" AllowPaging="true" 
                AllowSorting="True" AutoGenerateColumns="False"
                BorderWidth="1px" 
                onpageindexchanging="gvYarnIndentClosing_PageIndexChanging" >
                <Columns>
                    <%--<asp:BoundField DataField="YEAR" HeaderText="Year" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle CssClass="LabelNo SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="IND_TYPE" HeaderText="Indent Type">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DEPT_NAME" HeaderText="Dept Code">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Indent No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblInd_NUMB" runat="server" CssClass="LabelNo" Text='<%# Bind("IND_NUMB") %>'
                                ToolTip='<%# Bind("IND_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ITEM_CODE") %>' ToolTip='<%# Bind("IND_TYPE") %>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="LabelNo SmallFont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="40px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code">
                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="REQD_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Required Date"
                        HtmlEncode="False">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Description">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="currentStock" HeaderText="Current Stock">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RQST_QTY" HeaderText="Requested Qty" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle CssClass="LabelNo SmallFont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="APPR_QTY" HeaderText="Approved Qty" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PUR_ADJ_QTY" HeaderText="Adjusted Qty" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" ToolTip="Please Select for Closing" />
                        </ItemTemplate>
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                            Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="TextBox SmallFont " Text='<%# Bind("CONF_DATE") %>'
                                Width="55px" Height="16px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtConfirmDate">
                            </cc1:CalendarExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmBy" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                Text='<%# Bind("CONF_BY") %>' Width="70px" ReadOnly="true "></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" Width="100px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Left" />
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
            </asp:GridView>
        </td>
    </tr>
</table>
