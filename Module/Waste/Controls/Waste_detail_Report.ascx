<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Waste_detail_Report.ascx.cs"
    Inherits="Module_Waste_Controls_Waste_detail_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel121" runat="server">
    <ContentTemplate>
        <table align="left" class="td tContentArial" width="100%">
            <tr>
                <td class="td" colspan="8">
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center" valign="top" style="font-size: 14px; font-weight: bold; font-family: Arial;
                                background-color: #336799; color: #ffffff;">
                                Waste Detail Report
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="center" valign="top" cssclass="Label">
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>
                        </tr>
                    </table>
                    <table >
                        <tr>
                            <td>
                                <asp:Panel ID="pnl1001" runat="server" ScrollBars="Both" Width="95%" Height="350px">
                                    <asp:GridView ID="GrdItemQuery" runat="server" AutoGenerateColumns="False" Font-Size="Smaller"
                                        Width="100%" AllowPaging="true" PageSize="15" 
                                        onpageindexchanging="GrdItemQuery_PageIndexChanging">
                                        <RowStyle BackColor="White" Font-Size="Smaller" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Year">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Year
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtyear" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="Btnyear" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblYEAR" runat="server" CssClass=" SmallFont" Text='<%# Eval("YEAR") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Item Code">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Code
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtitemcode" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="Btnitemcode" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEM_CODE" runat="server" CssClass=" SmallFont" Text='<%# Eval("ITEM_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Item Catagory">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                             Category
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtitemcat" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btnitemcat" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEM_CAT" runat="server" CssClass=" SmallFont" Text='<%# Eval("CAT_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Item Type" Visible="false">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Item Type
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtitemtype" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btnitemtype" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEM_TYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("ITEM_TYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Trn No">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Trn No
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txttrnno" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                               <cc1:FilteredTextBoxExtender ID="Filtertxttrnno" runat="server"  TargetControlID="txttrnno"   FilterType="Custom, Numbers" />
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btntrnno" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTRN_NO" runat="server" CssClass=" SmallFont" Text='<%# Eval("TRN_NUMB") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Trn Type">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Trn Type
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txttrntype" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btntrntype" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTRN_TYPE" runat="server" CssClass=" SmallFont" ToolTip='<%# Eval("TRN_TYPE") %>' Text='<%# Eval("TRN_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                      
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Lot No" Visible="false">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Lot No
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtlotno" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btnlot_no" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLOT_NO" runat="server" CssClass=" SmallFont" Text='<%# Eval("LOT_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Grade" Visible="false">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Grade
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtgrade" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btngrade" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGRADE" runat="server" CssClass=" SmallFont" Text='<%# Eval("GRADE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Bal Qty">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Bal Qty
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtbalqty" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                                 <cc1:FilteredTextBoxExtender ID="Filtertxtbalqty" runat="server"  TargetControlID="txtbalqty"   FilterType="Custom, Numbers" ValidChars="."/>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btnbal_qty" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBAL_QTY" runat="server" CssClass=" SmallFont" Text='<%# Eval("BAL_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Final Rate">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Final Rate
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtfinalrate" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="Filtertxtfinalrate" runat="server"  TargetControlID="txtfinalrate"   FilterType="Custom, Numbers" ValidChars="."/>
                                                           
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btnfinalrate" CssClass="SmallFont" runat="server" Text="Go"
                                                                    ImageUrl="~/CommonImages/Icons/Search.png" OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFINAL_RATE" runat="server" CssClass=" SmallFont" Text='<%# Eval("FINAL_RATE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Balance Value">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Balance Value
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtbalancevalue" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="Filtertxtbalancevalue" runat="server"  TargetControlID="txtbalancevalue"   FilterType="Custom, Numbers" ValidChars="."/>
                                                           
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btnbalance_value" CssClass="SmallFont" runat="server" Text="Go"
                                                                    ImageUrl="~/CommonImages/Icons/Search.png" OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBALANCE_VALUE" runat="server" CssClass=" SmallFont" Text='<%# Eval("BAL_VALUE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Party Name">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Party Name
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtprtyname" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btnprty_name" CssClass="SmallFont" runat="server" Text="Go"
                                                                    ImageUrl="~/CommonImages/Icons/Search.png" OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPRTY_NAME" runat="server" CssClass=" SmallFont" Text='<%# Eval("PRTY_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Po Type" Visible="false">
                                                <HeaderTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="left" valign="top" width="100%">
                                                                Po Type
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="80%">
                                                                <asp:TextBox ID="txtpotype" Width="70px" CssClass="SmallFont" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td width="20%">
                                                                <asp:ImageButton ID="btnpo_type" CssClass="SmallFont" runat="server" Text="Go" ImageUrl="~/CommonImages/Icons/Search.png"
                                                                    OnClick="Btnyear_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPOTYPE" runat="server" CssClass=" SmallFont" Text='<%# Eval("PO_TYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle CssClass=" label SmallFont" HorizontalAlign="Left" VerticalAlign="Top"
                                                    Wrap="true" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="AliceBlue" />
                                        <AlternatingRowStyle BackColor="AliceBlue" />
                                        <HeaderStyle BackColor="#336799" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
