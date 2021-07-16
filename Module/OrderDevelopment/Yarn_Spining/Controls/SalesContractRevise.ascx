<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalesContractRevise.ascx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Controls_SalesContractRevise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<table align="left" class="tContentArial" width="100%">
<tr>
<td align="left" valign="top" class="td" width="100%">
<table align="left">
<tr>
<td id="tdUpdate" runat="server" align="left">
    &nbsp;<td id="tdFind" runat="server" visible="false" align="left">
        &nbsp;</td>
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
 <td id="td1" runat="server" align="left" >
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1" Visible="false"></asp:ImageButton>
                            </td>
</tr>
</table>

</td>
</tr>
<tr>
<td align="center" class="TableHeader td" width="100%">
<b class ="titleheading">
Sales Contract Revised </b>
</td>
</tr>
<tr>
<td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
<asp:GridView ID="gvCustomerApproval" runat="server" AutoGenerateColumns="false"  AllowPaging="true"
        CssClass="SmallFont" Width="100%" 
                        onpageindexchanging="gvCustomerApproval_PageIndexChanging1" 
                        onprerender="gvCustomerApproval_PreRender1" 
                        onrowdatabound="gvCustomerApproval_RowDataBound" >
<RowStyle BackColor="White" />
<Columns>                         
                            
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblConfFlag" CssClass=" SmallFont" runat="server" Text='<%# Eval("CONF_FLAG") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" width="100%">
                                                    Branch Name
                                                </td>
                                            </tr>
                                            <tr>
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
                                                    CR Date
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
                                                    Cust No
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
                                                <td colspan="2" width="50%">
                                                    Party 
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
                                        <asp:Label ID="lblPRTY_Name" runat="server" ToolTip='<%# Bind("PRTY_Name") %>' Text='<%# Bind("PRTY_CODE") %>'></asp:Label>
                                        &nbsp; &nbsp; &nbsp;
                                         <asp:Label ID="Label1" runat="server" ToolTip='<%# Bind("PRTY_CODE") %>' Text='<%# Bind("PRTY_Name") %>'></asp:Label>
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
                                <asp:TemplateField HeaderText="Reviesd">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApproved" runat="server" AutoPostBack="True" OnCheckedChanged="chkApproved_CheckedChanged" />
                                        <asp:Label ID="lblBOMRevised_FLAG" CssClass=" SmallFont" runat="server" Text='<%# Eval("ISREVISED") %>' Visible="false"></asp:Label>
                                   
                                    </ItemTemplate>
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>                                                        
                                                         
                                <asp:TemplateField HeaderText="Closed CR" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkClosed" Enabled="false" runat="server" AutoPostBack="True" OnCheckedChanged="ChkClosed_CheckedChanged" />
                                    </ItemTemplate>
                                    <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                                        Wrap="true" />
                                </asp:TemplateField>                        
                                <asp:TemplateField HeaderText="Confirm Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtConfirmDate" runat="server" Font-Size="Smaller" Width="60px"
                                            CssClass=" TextBox TextBoxDisplay SmallFont" ReadOnly="true" Text='<%# Bind("CONF_DATE") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Confirm By">
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
                </td>
</tr>
<tr>
<td align="left" class="td" width="100%">
    &nbsp;</td>
</tr>
</table>

