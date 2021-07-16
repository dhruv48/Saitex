<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Warp_Entry.ascx.cs" Inherits="Module_Production_Controls_Warp_Entry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%" class="tContentArial">
            <tr>
                <td>
                    <table class="tContentArial">
                        <tr>
                            <td id="tdSave" valign="top" align="center" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                    ToolTip="Save" Height="41" Width="48" ValidationGroup="M1" OnClick="imgbtnSave_Click">
                                </asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                    Width="48" Height="41" OnClick="imgbtnFind_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server" valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    Width="48" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    Width="48" Height="41" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                    Width="48" Height="41"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center" valign="top" style="background-color: #006699; color: White;">
                                <strong>Warping Entry Form</strong>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class="tContentArial" cellspacing="3">
                        <tr>
                            <td align="right" valign="top" width="15%">
                                Trn No:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txttrnno" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                <asp:DropDownList ID="ddltrn_no" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltrn_no_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Trn Date:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txttrn_date" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Shift:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlshift" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="15%">
                                Order No:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:DropDownList ID="ddlpinumber" runat="server" Width="135px" OnSelectedIndexChanged="ddlpinumber_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Business Type:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtbusinesstype" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Product Type:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtproduct_type" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="15%">
                                Mechine Id:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtmechineid" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Order Type:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtordertype" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                PI Type:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtpi_type" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" width="15%">
                                Beam Id:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtbeamID" runat="server" MaxLength="10" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Length:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtlength" runat="server" MaxLength="15" CssClass="TextBox SmallFont"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtreq_qty"
                                    ErrorMessage="Numeric Value only" MaximumValue="999999999" MinimumValue="1" Type="Double"
                                    ValidationGroup="reqqty" Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Broakge:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtbroakge" runat="server" MaxLength="15" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <fieldset>
                        <table width="100%" cellspacing="3" style="background-color: #E6E6E6;">
                            <tr>
                                <td align="right" valign="top" width="25%">
                                    <strong>Beam Loading Time</strong>
                                </td>
                                <td align="left" valign="top" width="25%">
                                    <asp:TextBox ID="txtbeamloading_time" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
                                </td>
                                <td align="right" valign="top" width="25%" cssclass="TextBox SmallFont">
                                    <strong>Beam Unloading Time</strong>
                                </td>
                                <td align="left" valign="top" width="25%">
                                    <asp:TextBox ID="txtbeamunloading_time" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <table width="100%">
                        <tr>
                            <td align="right" valign="top" width="15%">
                                Tip:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txttip" runat="server" MaxLength="10" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Creel:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtcreel" runat="server" MaxLength="10" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td align="right" valign="top" width="15%">
                                Operator:
                            </td>
                            <td align="left" valign="top" width="15%">
                                <asp:TextBox ID="txtoperator" runat="server" MaxLength="30" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Remark:
                            </td>
                            <td align="left" valign="top" colspan="4">
                                <asp:TextBox ID="txtremark" runat="server" Width="600px" MaxLength="200" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr bgcolor="#006699">
                            <td align="left" valign="top" width="10%">
                                <strong>Yarn Code</strong>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <strong>Yarn Desc</strong>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <strong>Shade Code</strong>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <strong>No Of End</strong>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <strong>Req Qty</strong>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <strong>Yarn Std</strong>
                            </td>
                            <td align="left" valign="top" width="10%" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" width="10%">
                                <asp:DropDownList ID="ddlyarn_code" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlyarn_code_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <asp:TextBox ID="txtyarn_desc" runat="server" Width="75px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <asp:DropDownList ID="ddlshadecode" runat="server" Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <asp:TextBox ID="txtno_of_end" runat="server" Width="75px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" width="11%">
                                <asp:TextBox ID="txtreq_qty" runat="server" Width="75px" CssClass="TextBox SmallFont"></asp:TextBox>
                                <asp:RangeValidator ID="r2" runat="server" ControlToValidate="txtreq_qty" ErrorMessage="*Enter Quantity"
                                    MaximumValue="999999999" MinimumValue="1" Type="Double" ValidationGroup="reqqty"
                                    Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <asp:TextBox ID="txtyarn_std" runat="server" Width="75px" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td align="left" valign="top" width="5%">
                                <asp:LinkButton ID="lnkbtnrecieve" runat="server" onclick="lnkbtnrecieve_Click">Issue Ref.
                                </asp:LinkButton>
                            </td>
                            <td align="left" valign="top" width="10%">
                                <asp:Button ID="lnkbtnsave" runat="server" Text="Save" OnClick="lnkbtnsave_Click1" />
                                <asp:Button ID="lnkbtncancel" runat="server" Text="Remove" OnClick="lnkbtncancel_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td class="td SmallFont">
                                <asp:GridView ID="WarpTrnDetail" TabIndex="16" runat="server" CssClass="SmallFont"
                                    AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" OnRowCommand="WarpTrnDetail_RowCommand">
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Yarn Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblyrncode" TabIndex="17" Width="80px" Font-Bold="true" CssClass="Label SmallFont"
                                                    runat="server" Text='<%# Bind("YARN_CODE") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Yarn Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblyrndesc" TabIndex="19" ReadOnly="true" runat="server" Width="120px"
                                                    CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shade">
                                            <ItemTemplate>
                                                <asp:Label ID="lblshade_code" TabIndex="22" CssClass="Label SmallFont" runat="server"
                                                    ReadOnly="true" Width="50px" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No Of End">
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblno_of_shade" TabIndex="23" runat="server" Width="50px" Text='<%# Bind("NO_OF_END") %>'
                                                    CssClass="LabelNo SmallFont" ReadOnly="true"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Req Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblyrn_req_qty" TabIndex="25" ReadOnly="true" runat="server" Text='<%# Bind("YRN_REQ_QTY") %>'
                                                    Width="70px" Font-Bold="true" CssClass="LabelNo SmallFont"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Yarn Std">
                                            <ItemTemplate>
                                                <asp:Label ID="lblyrn_std" TabIndex="26" ReadOnly="true" Width="70px" runat="server"
                                                    Text='<%# Bind("YARN_STD") %>' Font-Bold="true" CssClass="LabelNo SmallFont"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue Ref">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkissuref" runat="server" Text="Issue Ref"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="8%" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" TabIndex="29" runat="server" Text="Edit" CommandName="lnkEdit1"
                                                    CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                                                /
                                                <asp:LinkButton ID="lnkDelete" TabIndex="29" runat="server" Text="Del" CommandName="lnkDelete1"
                                                    CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
         <cc1:CalendarExtender ID="c2" runat="server" TargetControlID="txtbeamloading_time"
    Format="dd/MM/yyyy HH':'mm':'ss">
</cc1:CalendarExtender>
<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtbeamunloading_time"
    Format="dd/MM/yyyy HH':'mm':'ss">
</cc1:CalendarExtender>
<cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txttrn_date"
    Format="dd/MM/yyyy">
</cc1:CalendarExtender>

         </ContentTemplate>
</asp:UpdatePanel>
       
   
