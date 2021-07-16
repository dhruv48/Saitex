<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item_QC_Checking_Approval.ascx.cs"
    Inherits="Module_Inventory_Controls_Item_QC_Checking_Approval" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
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
            <b class="titleheading">Item QC Checking Approval</b>
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
                    <asp:TemplateField HeaderText="QC&nbsp;YEAR" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblQC_Year" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QC&nbsp;No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblQC_NUMB" runat="server" CssClass="Label smallfont" Text='<%#Eval("QC_NUMB")%>'></asp:Label></ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="QC_DATE" HeaderText="QC&nbsp;Date" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="MRN&nbsp;YEAR" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblTRN_YEAR" runat="server" Text='<%# Bind("TRN_YEAR") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRN&nbsp;No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblTRN_NUMB" runat="server" CssClass="LabelNo" ToolTip='<%# Bind("TRN_NUMB") %>'
                                Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>' ToolTip='<%# Bind("YEAR") %>'
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
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Min&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblMIN_VALUE" runat="server" CssClass="LabelNo smallfont" Text='<%#Eval("MIN_VALUE")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QC&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblQC_VALUE" runat="server" CssClass="LabelNo smallfont" Text='<%#Eval("QC_VALUE")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Result" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblQC_Result" runat="server" CssClass="Label smallfont" Text='<%#Eval("QC_Result")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QC Confirm">
                        <ItemTemplate>
                            <asp:RadioButton ID="chkQCPass" runat="server" Text="Pass" GroupName="chk" />
                            <asp:RadioButton ID="chkQCFail" runat="server" Text="Fail" GroupName="chk" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox smallfont" MaxLength="500"
                                Width="90%" TextMode="MultiLine"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle CssClass="label smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
</table>
