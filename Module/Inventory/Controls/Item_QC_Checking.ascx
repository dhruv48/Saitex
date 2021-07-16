<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item_QC_Checking.ascx.cs"
    Inherits="Module_Inventory_Controls_Item_QC_Checking" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="td1" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                            ImageUrl="~/CommonImages/addnew.png" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
                    </td>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            TabIndex="3" OnClick="imgbtnSave_Click" />
                    </td>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" TabIndex="4" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1">
                        </asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            OnClick="imgbtnFind_Click" TabIndex="5"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" TabIndex="6">
                        </asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnList" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/list.jpg"
                            OnClick="imgbtnList_Click" TabIndex="7"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" TabIndex="8">
                        </asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" TabIndex="9">
                        </asp:ImageButton>
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
            <b class="titleheading">Item QC Checking</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="center" width="100%" class="td">
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
            <asp:GridView ID="gvMaterialReceiptApproval" CssClass="SmallFont" runat="server"
                AllowSorting="True" AutoGenerateColumns="False" OnRowDataBound="gvMaterialReceiptApproval_RowDataBound"
                Width="95%">
                <Columns>
                    <asp:TemplateField HeaderText="Sr&nbsp;No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRN&nbsp;YEAR" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblQC_Year" runat="server" Text='<%# Bind("YEAR") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblTRN_YEAR" runat="server" Text='<%# Bind("TRN_YEAR") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRN&nbsp;No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblTRN_NUMB" runat="server" CssClass="LabelNo" ToolTip='<%# Bind("TRN_NUMB") %>'
                                Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblQC_NUMB" runat="server" CssClass="Label smallfont" Text='<%#Eval("QC_NUMB")%>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="TRN_DATE" HeaderText="MRN&nbsp;Date" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PARTY_DATA" HeaderText="Party" HeaderStyle-HorizontalAlign="Left">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Item&nbsp;Desc" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblITEM_CODE" runat="server" CssClass="Label smallfont" Text='<%#Eval("ITEM_CODE")%>'
                                Visible="false"></asp:Label>
                            <asp:Label ID="lblITEM_Desc" runat="server" CssClass="Label smallfont" Text='<%#Eval("ITEM_DESC")%>'
                                ToolTip='<%#Eval("ITEM_CODE")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Std&nbsp;Type" HeaderStyle-HorizontalAlign="Left"
                        ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblSTD_TYPE" runat="server" CssClass="Label smallfont" Text='<%#Eval("STD_TYPE")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="label smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Max&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMAX_VALUE" runat="server" CssClass="LabelNo smallfont" Text='<%#Eval("MAX_VALUE")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Min&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMIN_VALUE" runat="server" CssClass="LabelNo smallfont" Text='<%#Eval("MIN_VALUE")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QC&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="updatepanel11" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtQCValue" runat="server" CssClass="TextBoxNo smallfont" OnTextChanged="txtQCValue_TextChanged"
                                        AutoPostBack="true" Text='<%#Eval("QC_VALUE")%>' TabIndex="1" Width="90%"></asp:TextBox></ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQCValue"
                                ValidationExpression="(^-?0\.[0-9]*[1-9]+[0-9]*$)|(^-?[1-9]+[0-9]*((\.[0-9]*[1-9]+[0-9]*$)|(\.[0-9]+)))|(^-?[1-9]+[0-9]*$)|(^0$){1}"
                                ErrorMessage="Invalid No">
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox smallfont" MaxLength="200"
                                Width="90%" Text='<%#Eval("QC_REMARKS")%>' TabIndex="2"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle CssClass="label smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Result" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Image ID="imgSuccess" runat="server" ImageUrl="../../../CommonImages/green.png"
                                Height="20px" Width="20px" AlternateText="Pass" ToolTip="Pass" />
                            <asp:Image ID="imgFail" runat="server" ImageUrl="../../../CommonImages/red.png" Height="22px"
                                Width="22px" AlternateText="Fail" ToolTip="Fail" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Add&nbsp;New">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approved&nbsp;Result" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblQC_Approved_Result" runat="server" CssClass="LabelNo smallfont"
                                Text='<%#Eval("QC_Approved_Result")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEdit" runat="server" OnCheckedChanged="chkEdit_CheckedChanged"
                                AutoPostBack="true" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <%--   <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>--%>
                </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
</table>
