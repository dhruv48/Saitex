<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberItemStockAgeingQuery.aspx.cs" Inherits="Module_Fiber_Reports_FiberItemStockAgeingQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<ContentTemplate>
        <table class="td tContent" style="width: 945px">
            <tr>
                <td>
                    <table align="left">
                        <tr>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Height="41" ValidationGroup="M1" Width="48" OnClick="imgbtnPrint_Click">
                                </asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Height="41" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Height="41" Width="48" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Height="41" Width="48" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" colspan="5">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <span class="titleheading">Poy Stock Ageing</span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 945px">
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="Label1" runat="server" Text="Branch :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="SmallFont" Width="150px"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label3" runat="server" Text="Poy Code :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="SmallFont" Width="150px"
                                    TabIndex="2">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label4" runat="server" Text="Category Code :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlCatCode" runat="server" CssClass="SmallFont" Width="150px"
                                    TabIndex="3">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="Label5" runat="server" Text="Day 1 :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDay1" runat="server" CssClass="TextBoxNo" Width="147px" TabIndex="4"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label6" runat="server" Text="Day 2 :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDay2" runat="server" CssClass="TextBoxNo" Width="147px" TabIndex="5"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label7" runat="server" Text="Day 3 :" CssClass="Label"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtDay3" runat="server" CssClass="TextBoxNo" Width="147px" TabIndex="6"></asp:TextBox>
                            </td>
                        <td><asp:Button ID="txtGetRecord" runat="server" Text="GetRecord" 
                                onclick="txtGetRecord_Click" CssClass="AButton"></asp:Button> </td>
                        </tr>
                     
                    </table>
                </td>
            </tr>
          
            <tr>
                <td align="left" class="td" width="100%" valign="top">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                 
                </td>
            </tr>
            <tr><td colspan = "10" class ="tContent">
               
      
                               <asp:GridView ID="grdAgin" runat="server" 
                    AutoGenerateColumns="False" CssClass="SmallFont"
                                ShowFooter="true" CellPadding="4" ForeColor="#333333" 
                    BorderStyle="Ridge" GridLines="Both"
                                ont-Size="X-Small" AllowPaging="True" 
                    onpageindexchanging="grdAgin_PageIndexChanging" onrowdatabound="grdAgin_RowDataBound1" Width="100%">
                               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                <Columns>
                                    <asp:BoundField DataField="BRANCH_NAME" HeaderText="Branch"    HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left" />
                                     <asp:BoundField DataField="FIBER_CODE" HeaderText="Poy Code"  HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left" />                                     
                                     <asp:BoundField DataField="FIBER_DESC" HeaderText="Poy Description" HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left" />
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 1" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay1" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld1day" runat="server" Text='<%# Eval("d1day", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

                                     <FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 2" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay2" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld2day" runat="server" Text='<%# Eval("d2day", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

                                           <FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 3" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay3" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld3day" runat="server" Text='<%# Eval("d3day", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 4" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay4" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld4day" runat="server" Text='<%# Eval("d4Say", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Total Qty" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrTotQty" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTQTY" runat="server" Text='<%# Eval("TQTY", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 1 Value" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay1Value" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld1dayval" runat="server" Text='<%# Eval("d1dayval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 2 Value" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay2Value" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld2dayval" runat="server" Text='<%# Eval("d2dayval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 3 Value" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay3Value" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld3dayval" runat="server" Text='<%# Eval("d3dayval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Day 4 Value" FooterStyle-HorizontalAlign="Right"
                                        FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrDay4Value" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbld4sayval" runat="server" Text='<%# Eval("d4sayval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Total Qty Value"
                                        FooterStyle-HorizontalAlign="Right" FooterStyle-Font-Bold="true">
                                        <FooterTemplate>
                                            <asp:Label ID="lblFtrTotQtyValue" runat="server" CssClass="LabelNo"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbltqtyval" runat="server" Text='<%# Eval("tqtyval", "{0:f}") %>'></asp:Label>
                                        </ItemTemplate>

<FooterStyle HorizontalAlign="Right" Font-Bold="True"></FooterStyle>

                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Wrap="true" />
                                    </asp:TemplateField>
                                </Columns>
                                 <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                </td></tr>
        </table>
        <asp:RequiredFieldValidator ID="RFDay1" runat="server" ControlToValidate="txtDay1"
            Display="None" ErrorMessage="Please enter Day 1" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RVDay1" runat="server" ControlToValidate="txtDay1" Display="None"
            ErrorMessage="Please enter only numeric value that should be greater than zero"
            MaximumValue="99999" MinimumValue="1" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RFDay2" runat="server" ControlToValidate="txtDay2"
            Display="None" ErrorMessage="Please enter Day 2" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RVDay2" runat="server" ControlToValidate="txtDay2" Display="None"
            ErrorMessage="Please enter only numeric value that should be greater than zero"
            MaximumValue="99999" MinimumValue="1" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RFDay3" runat="server" ControlToValidate="txtDay3"
            Display="None" ErrorMessage="Please enter Day 3" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RVDay3" runat="server" ControlToValidate="txtDay3" Display="None"
            ErrorMessage="Please enter only numeric value that should be greater than zero"
            MaximumValue="99999" MinimumValue="1" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
    </ContentTemplate>
</asp:Content>

