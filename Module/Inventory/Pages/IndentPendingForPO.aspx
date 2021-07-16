<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="IndentPendingForPO.aspx.cs" Inherits="Module_Inventory_Pages_IndentPendingForPO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
<script type="text/javascript" language="javascript">
    // Added By Rajesh for Printing Directly from Form (05 Jan 2012)
    function CallPrint(strid) {
        var prtContent = document.getElementById(strid);
        if (prtContent != null) {
            var WinPrint = window.open('', '', 'center=1,width=800,height=600,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            //WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
    }    
</script>
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
                        <%--<td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                ImageUrl="~/CommonImages/link_print.png" CssClass="AButton"></asp:ImageButton>&nbsp;
                        </td>--%>
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
                <b class="titleheading">Material Indent Pending For PO</b>
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
            
                <td align="left" Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="Label1" runat="server"></asp:Label></b>
                    <asp:Button ID="btnPrint" runat="Server" Text="Print Direct" TabIndex="7"  CssClass="AButton" />
                </td>
            </tr>
        <tr>
            <td align="left" class="td" width="100%">
            <div id="divPrint" >
                <asp:GridView ID="gvMaterialIndentApproval" runat="server" AllowSorting="True" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="DEPT_NAME" HeaderText="Dept Code">
                            <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                Width="50px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="LOCATION" HeaderText="Location">
                            <ItemStyle HorizontalAlign="Right" CssClass="labelNo smallfont" VerticalAlign="Top"
                                Width="50px" />
                        </asp:BoundField>
                         <asp:BoundField DataField="STORE" HeaderText="Store">
                            <ItemStyle HorizontalAlign="Right" CssClass="labelNo smallfont" VerticalAlign="Top"
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
                            <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item Description">
                            <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                Width="120px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="currentStock" HeaderText="Current Stock">
                            <ItemStyle HorizontalAlign="Right" CssClass="labelNo smallfont" VerticalAlign="Top"
                                Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RQST_QTY" HeaderText="Requested Qty">
                            <ItemStyle HorizontalAlign="Right" CssClass="labelNo smallfont" VerticalAlign="Top"
                                Width="50px" />
                        </asp:BoundField>
                        
                      
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
            
            </div>
            </td>
        </tr>
    </table>
   
</asp:Content>

