<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_Search_Pallet_Return.ascx.cs"
    Inherits="Module_Fiber_Controls_Fiber_Search_Pallet_Return" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table align="left" class=" td tContentArial" width="100%">
            <tr>
                <td>
                    <table class=" td tContentArial">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                                    ImageUrl="~/CommonImages/addnew.png" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" OnClick="imgbtnPrint_Click1" />
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/export.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" OnClick="imgbtnHelp_Click" />
                            </td>
                        </tr>
                    </table>
                    <tr>
                        <td align="center" class="TableHeader td">
                            <span class="titleheading"><strong>Search Pallet Return</strong> </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <b>
                                <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                                <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="grdsearchpalletreturn" runat="server" AutoGenerateColumns="False"
                                Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="3" BorderStyle="Ridge"
                                CssClass="smallfont" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                                OnPageIndexChanging="grdsearchpalletreturn_PageIndexChanging">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Challan Number">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                        Challan Number
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="80%">
                                                        <asp:TextBox ID="txtChallanNO" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                        
                                                    </td>
                                                    <td width="20%">
                                                        <asp:ImageButton ID="btnChallanNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblChallanNo" runat="server" Text='<%#Eval("PRTY_CH_NUMB") %>' ToolTip='<%#Eval("PRTY_CH_NUMB") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Challan Date">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                        Challan Date
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="80%">
                                                        <asp:TextBox ID="txtChallanDate" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="ceIssueDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChallanDate">
</cc1:CalendarExtender>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:ImageButton ID="btnChallanDate" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                    </td>
                                                </tr>
                                            </table>

                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblChallanDate" runat="server" Text='<%#Eval("PRTY_CH_DATE") %>' ToolTip='<%#Eval("PRTY_CH_DATE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Poy Sub Cat">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                        Party Name
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="80%">
                                                        <asp:TextBox ID="txtPartyCode" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:ImageButton ID="btnPartyCode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartyCode" runat="server" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("PRTY_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Merge">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                        Merge/LotNO 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="80%">
                                                        <asp:TextBox ID="txtMerge" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:ImageButton ID="btnMerge" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMerge" runat="server" Text='<%#Eval("MERGE_NO") %>' ToolTip='<%#Eval("MERGE_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="MRN NO">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                        Mrn No
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="80%">
                                                        <asp:TextBox ID="txtMRNNO" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:ImageButton ID="btnMRNNO" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMRNNO" runat="server" Text='<%#Eval("ISSUE_TRN_NUMB") %>' ToolTip='<%#Eval("ISSUE_TRN_NUMB") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="  Merge No" Visible ="false">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                       Trn type
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="80%">
                                                        <asp:TextBox ID="txtMergeNo" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:ImageButton ID="btnMergeNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMergeNo" runat="server" Text='<%#Eval("PALLET_CODE") %>' ToolTip='<%#Eval("PALLET_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="  Pallet Code">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                        Pallet Code
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="80%">
                                                        <asp:TextBox ID="txtPalletCode" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:ImageButton ID="btnPalletCode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPalletCode" runat="server" Text='<%#Eval("PALLET_CODE") %>' ToolTip='<%#Eval("PALLET_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No Of Pallet">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="2" width="100%">
                                                        Pallet No
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="80%">
                                                        <asp:TextBox ID="txtNoOfPallet" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:ImageButton ID="btnNoOfPallet" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoOfPallet" runat="server" Text='<%#Eval("PALLET_NO") %>' ToolTip='<%#Eval("PALLET_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </td>
            </tr>
        </table>
  
  <%--  </ContentTemplate>
</asp:UpdatePanel>
--%>