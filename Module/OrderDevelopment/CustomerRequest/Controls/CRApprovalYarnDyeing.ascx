<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CRApprovalYarnDyeing.ascx.cs"    Inherits="Module_OrderDevelopment_CustomerRequest_Controls_CRApprovalYarnDyeing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial" width="100%">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" Visible="false" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
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
                    <b class="titleheading">Approval Form For Sale Booking</b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="td" width="100%">
                   <%-- <asp:Panel ID="pnlTree" runat="server" ScrollBars="Both" Height="300px" Width="1300px">--%>
                        <asp:GridView ID="gvCustomerApproval" runat="server" AutoGenerateColumns="False"
                            CssClass="SmallFont" Width="100%" OnRowDataBound="gvCustomerApproval_RowDataBound1"
                            OnRowCommand="gvCustomerApproval_RowCommand" OnSelectedIndexChanged="gvCustomerApproval_SelectedIndexChanged"
                            OnSelectedIndexChanging="gvCustomerApproval_SelectedIndexChanging">
                            <RowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREMUNIT" CssClass=" SmallFont" runat="server" Text='<%# Eval("REMUNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFINAL_AMOUNTS" CssClass=" SmallFont" runat="server" Text='<%# Eval("FINAL_AMOUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPARTY_ARTICLE_DESC" CssClass=" SmallFont" runat="server" Text='<%# Eval("PARTY_ARTICLE_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFINAL_RATE" CssClass=" SmallFont" runat="server" Text='<%# Eval("FINAL_RATE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPAYMENT_MODE" CssClass=" SmallFont" runat="server" Text='<%# Eval("PAYMENT_MODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPAYMENT_TERMS" CssClass=" SmallFont" runat="server" Text='<%# Eval("PAYMENT_TERMS") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQUANTITY_QTY" CssClass=" SmallFont" runat="server" Text='<%# Eval("QUANTITY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGRADE" CssClass=" SmallFont" runat="server" Text='<%# Eval("GRADE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                
                                
                                
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblqualitydesc" CssClass=" SmallFont" runat="server" Text='<%# Eval("YARN_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="EMAIL" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEMAIL" CssClass=" SmallFont" runat="server" Text='<%# Eval("EMAIL") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="PHONE" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPHONE" CssClass=" SmallFont" runat="server" Text='<%# Eval("PHONE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="INVOICE_NO_OF_UNIT" CssClass=" SmallFont" runat="server" Text='<%# Eval("INVOICE_NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConfFlag" CssClass=" SmallFont" runat="server" Text='<%# Eval("CONF_FLAG") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr  >
                                                <td colspan="2" width="100%">
                                                    Branch Name
                                                </td>
                                            </tr>
                                            <tr >
                                                <td width="80%">
                                                    <asp:TextBox ID="txthBranchName" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhBranchName" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBRANCH_NAME" CssClass=" SmallFont" runat="server" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBRANCH_CODE" CssClass=" SmallFont" runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="CR Date">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td width="80%">
                                                    Order Date
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhCRDate" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    <asp:TextBox ID="txthCRDate" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_DATE" runat="server" CssClass="label smallfont" Text='<%# Bind("ORDER_DATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Cust No">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Order No
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnhCustNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txthCustNo" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblCUSTNO" runat="server" Text='<%# Bind("CUSTNO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Party Name......">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Party Name
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthPartyName" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhPartyName" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRTY_Name" runat="server" ToolTip='<%# Bind("PRTY_CODE") %>' Text='<%# Bind("PRTY_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                   
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompCode" runat="server" Text='<%# Eval("COMP_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYEAR" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_TYPE" runat="server" Text='<%# Eval("ORDER_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_CAT" runat="server" Text='<%# Eval("ORDER_CAT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPRODUCT_TYPE" runat="server" Text='<%# Eval("PRODUCT_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req NoOfUnit"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBUSINESS_TYPE" runat="server" Text='<%# Eval("BUSINESS_TYPE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req NoOfUnit"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_NO" runat="server" Text='<%# Eval("custno") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Gray Yarn Detail">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Yarn Detail
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthGrayYarnDtl" Width="90px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhGrayYarnDtl" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblARTICLE_NO" runat="server" Text='<%# Eval("YARN_DESC") %>'  ToolTip='<%# Eval("ARTICLE_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSUBSTRATE" runat="server" Text='<%# Eval("SUBSTRATE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Req NoOfUnit" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCOUNT" runat="server" Text='<%# Eval("COUNT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shade Family">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Shade Family
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthShadeFamily" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhShadeFamily" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHADE_FAMILY_CODE" runat="server" Text='<%# Eval("SHADE_FAMILY_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Shade Code">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Shade Code
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthShadeCode" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhShadeCode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHADE_CODE" runat="server" Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Trans Price" Visible="false">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Tr. Price
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="80%">
                                                    <asp:TextBox ID="txthTransPrice" Width="35px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td width="20%">
                                                    <asp:ImageButton ID="btnhTransPrice" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                        Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="15px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTRANS_PRICE" runat="server" Text='<%# Eval("TRANS_PRICE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sale Rate" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                        <asp:Label ID="lblSALE_PRICE" runat="server" Text='<%# Eval("SALE_PRICE") %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Dis" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                        <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("DISCOUNT") %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Freight" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                        <asp:Label ID="lblFreight" runat="server" Text='<%# Eval("FREIGHT") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                
                                
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Net Rate"  ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                        <asp:Label ID="lblNetRate" runat="server" Text='<%# Eval("NET_RATE") %>'  Width="40px"></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="SGST" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                        <asp:Label ID="lblSGST" runat="server" Text='<%# Eval("SGST") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                        <asp:Label ID="lblCGST" runat="server" Text='<%# Eval("CGST") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                
                                
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" IGST " ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                        <asp:Label ID="lblIGST" runat="server" Text='<%# Eval("IGST") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText=" With Tax " ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                        <asp:Label ID="lblFinalRate" runat="server" Text='<%# Eval("FINAL_RATE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("REMARKS") %>' Width="70px"></asp:Label>
                                    </ItemTemplate>
                                
                                </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("CR_UNIT") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Qty." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                        <asp:Label ID="lblRequestedNoOfUnit" runat="server" Text='<%# Eval("NO_OF_UNIT") %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Apr.Qty">
                                    <ItemTemplate>
                                       <%-- <asp:TextBox ID="lblNoOfUnit" runat="server" AutoPostBack="True" Width="60px" CssClass="SmallFont TextBoxNo"
                                            OnTextChanged="lblNoOfUnit_TextChanged" Text='<%# Eval("BAL_INVOICE_NO_OF_UNIT") %>'  
                                            ValidationGroup="gg"></asp:TextBox>--%>
                                             <asp:TextBox ID="lblNoOfUnit" runat="server" AutoPostBack="True" Width="40px" CssClass="SmallFont TextBoxNo"
                                            OnTextChanged="lblNoOfUnit_TextChanged" Text='<%# Eval("NO_OF_UNIT") %>'  
                                            ValidationGroup="gg"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="lblNoOfUnit"
                                            Display="Dynamic" ErrorMessage="Please Enter Numeric" MaximumValue="99999999999"
                                            MinimumValue="0" Type="Double" ValidationGroup="gg"></asp:RangeValidator>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                
                                
                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Expected Dispatch Date" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblREQ_DATE" CssClass=" SmallFont" runat="server" Text='<%# Eval("REQ_DATE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve Order">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="True" OnCheckedChanged="chkApproved_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Apr Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAPP_NO_OF_UNIT" runat="server" Text='<%# Eval("APP_NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Invoiced Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINVOICE_NO_OF_UNIT" runat="server" Text='<%# Eval("INVOICE_NO_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req Qty" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRequestedQuantity" runat="server" Text='<%# Eval("QUANTITY") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Req WeighofUnit"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReqWeighofUnit" runat="server" Text='<%# Eval("WEIGHT_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Apr.Weight" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeighofUnit" runat="server" Text='<%# Eval("WEIGHT_OF_UNIT") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Closed CR" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkClosed" Enabled="false" runat="server" AutoPostBack="True" OnCheckedChanged="ChkClosed_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Apr.Weight" Visible="false">
                                    <ItemTemplate>
                                       <%-- <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QTY_APPROVED") %>'></asp:Label>--%>
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("NO_OF_UNIT") %>'></asp:Label>
                                        
                                    </ItemTemplate>
                                    
                                    
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approval Date" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConfirmDate" runat="server" Font-Size="Smaller" Width="60px"
                                            CssClass=" TextBox TextBoxDisplay SmallFont" ReadOnly="true" Text='<%# Bind("CONF_DATE") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved By">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConfirmBy" runat="server" Font-Size="Smaller" Width="60px" CssClass="TextBox TextBoxDisplay SmallFont"
                                            ReadOnly="true" Text='<%# Bind("CONF_BY") %>' ToolTip='<%# Bind("CONF_BY") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                        </asp:GridView>
                  <%--  </asp:Panel>--%>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
