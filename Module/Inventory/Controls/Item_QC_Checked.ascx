<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item_QC_Checked.ascx.cs"
    Inherits="Module_Inventory_Controls_Item_QC_Checked" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="td1" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                            ImageUrl="~/CommonImages/addnew.png" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
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
            <b class="titleheading">Item QC Already Checked</b>
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
                            <asp:Label ID="lblQC_NUMB" runat="server" CssClass="Label smallfont" Text='<%#Eval("QC_NUMB")%>'></asp:Label>
                        </ItemTemplate>
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
                            <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>' Visible="false"></asp:Label>
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
                            <asp:Label ID="lblQC_VALUE" runat="server" CssClass="Label smallfont" Text='<%#Eval("QC_VALUE")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="txtRemarks" runat="server" CssClass="Label smallfont" Text='<%#Eval("QC_REMARKS")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="label smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QC&nbsp;Result" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblQC_Result" runat="server" CssClass="Label smallfont" Text='<%#Eval("QC_Result")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="QC_DONE_BY" HeaderText="QC&nbsp;Done&nbsp;By" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QC_Approved_By" HeaderText="QC&nbsp;Approved&nbsp;By"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Approved&nbsp;Result" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblQC_Approved_Result" runat="server" CssClass="Label smallfont" Text='<%#Eval("QC_Approved_Result")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
</table>
