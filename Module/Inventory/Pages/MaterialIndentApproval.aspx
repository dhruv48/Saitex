<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="MaterialIndentApproval.aspx.cs" Inherits="Inventory_Pages_MaterialIndentApproval"
    Title="Material Indent Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <table width="100%" align="left" class="tContentArial">
        <tr>
            <td align="left" valign="top" class="td" width="100%">
                <table align="left">
                    <tr>
                        <td id="tdUpdate" runat="server" align="left">
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                        </td>
<%--                        <td id="tdDelete" runat="server" align="left">
                            <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                                ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>--%>
                        </td>
                        <td id="tdFind" runat="server" visible="false" align="left">
                            <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TableHeader td" width="100%">
                <b class="titleheading">Material Indent Approval</b>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" width="100%" class="style1">
                <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                </span>
            </td>
        </tr>
        <tr>
            <td align="center" width="100%" class="style1">
                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" class="td" width="100%">
                <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td align="left" class="td" width="100%">
                <asp:GridView ID="gvMaterialIndentApproval" runat="server" AllowSorting="True" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Dept Code">
                            <ItemStyle HorizontalAlign="Center" CssClass="label smallfont" VerticalAlign="Top"
                                Width="50px" />
                        </asp:BoundField>
                          <asp:BoundField DataField="IND_TYPE" HeaderText="Indent Type">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                        <asp:TemplateField HeaderText="Indent No">
                            <ItemTemplate>
                                <asp:Label ID="lblInd_NUMB" runat="server" ToolTip='<%# Bind("IND_NUMB") %>' Text='<%# Bind("IND_NUMB") %>'></asp:Label>
                                <asp:Label ID="lblItemCode" runat="server" ToolTip='<%# Bind("IND_TYPE") %>' Text='<%# Bind("ITEM_CODE") %>'
                                    Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                Width="40px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="REQD_DATE" HeaderText="Required Date" DataFormatString="{0:dd/MM/yyyy}"
                            HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Center" CssClass="label smallfont" VerticalAlign="Top"
                                Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Description">
                            <ItemStyle HorizontalAlign="Center" CssClass="label smallfont" VerticalAlign="Top"
                                Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HSN_CODE" HeaderText="HSN Code">
                            <ItemStyle HorizontalAlign="Center" CssClass="label smallfont" VerticalAlign="Top"
                                Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="currentStock" HeaderText="Current Stock">
                            <ItemStyle HorizontalAlign="Right" CssClass="labelNo smallfont" VerticalAlign="Top"
                                Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RQST_QTY" HeaderText="Requested Qty">
                            <ItemStyle HorizontalAlign="Right" CssClass="labelNo smallfont" VerticalAlign="Top"
                                Width="70px" />
                        </asp:BoundField>
                        <%-- <asp:BoundField DataField="APPR_QTY" HeaderText="Approved Qty">
                            <ItemStyle HorizontalAlign="Right" CssClass="labelNo smallfont" VerticalAlign="Top"
                                Width="50px" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Confirm">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkApproved" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" CssClass="label smallfont" VerticalAlign="Top"
                                Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtApprovedQty" runat="server" Width="50px" CssClass="TextBoxNo SmallFont"
                                    Text='<%# Bind("RQST_QTY") %>'></asp:TextBox>
                                <asp:Label ID="lblApprovedQty" runat="server" Visible="false" CssClass="TextBoxNo SmallFont"
                                    Text='<%# Bind("RQST_QTY") %>'></asp:Label>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtApprovedQty"
                                    Display="dynamic" Type="Double" MinimumValue="0" MaximumValue="100000" ErrorMessage="Pls enter b/w 0 to 100000"
                                    ValidationGroup="M1"></asp:RangeValidator>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Confirm Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtConfirmDate" runat="server" Text='<%# Bind("CONF_DATE") %>' Width="57px"
                                    CssClass="TextBox SmallFont TextBoxDisplay" ReadOnly="true"></asp:TextBox>
                                <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtConfirmDate">
                                </cc1:CalendarExtender>--%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Confirm By">
                            <ItemTemplate>
                                <asp:TextBox ID="txtConfirmBy" runat="server" Text='<%# Bind("CONF_BY") %>' Width="70px"
                                    CssClass="TextBox SmallFont TextBoxDisplay" ReadOnly="true"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server" Width="100px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <%--  <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="true" PopupControlID="Panel2" RepositionMode="RepositionOnWindowScroll"
        TargetControlID="imgbtnFindTop">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" BackColor="#336799">
        <table>
            <tr>
                <td >
                </td>
            </tr>
            <tr>
                <td align="left" >
                    <table width="600" align="left" class="tContentArial" border="1">
                        <tr>
                            <td align="center" colspan="6">
                                <span style="font-size: 13pt"><strong>Material Indent Approval Find</strong> </span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                ItemCode
                            </td>
                            <td>
                                <asp:TextBox ID="txtItemCodeFind" runat="server" Width="150px" Text="%"></asp:TextBox>
                            </td>
                            <td>
                                IndentNo
                            </td>
                            <td>
                                <asp:TextBox ID="txtIndentFind" runat="server" Width="150px" Text="%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <br />
                    <asp:GridView ID="gvMaterialFind" runat="server" AllowPaging="false" 
                        AutoGenerateColumns="False" ForeColor="WhiteSmoke" 
                        OnRowCommand="gvMaterialFind_RowCommand1" PagerSettings-Mode="Numeric" 
                        PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Left">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %> &nbsp;&nbsp;
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemCode" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkItemCode" runat="server" CausesValidation="false" 
                                        CommandArgument='<%# Eval("ITEM_CODE") %>' CommandName="" 
                                        OnClick="lnkItemCode_Click" Text='<%# Eval("ITEM_CODE") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IndentNumber" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkIndentNo" runat="server" CausesValidation="false" 
                                        CommandArgument='<%# Eval("ITEM_CODE") %>' CommandName="" 
                                        OnClick="lnkIndentNo_Click" Text='<%# Eval("IND_NUMB") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                </td>               
                <tr>
                    <td align="center" colspan="8" valign="top">
                        <asp:Button ID="btnGetGV" runat="server" CssClass="AButton" 
                            OnClick="btnGetGV_Click" TabIndex="19" Text="GetGV" ValidationGroup="GetGV" 
                            Width="75px" />
                    </td>
                </tr>
                
            </tr>
        </table>
    </asp:Panel>--%>
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="Head1">

    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>

</asp:Content>

