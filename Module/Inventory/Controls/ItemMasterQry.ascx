<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemMasterQry.ascx.cs"
    Inherits="Module_Inventory_Controls_ItemMasterQry" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel8971" runat="server">
    <ContentTemplate>
        <table width="100%" class ="td tContentArial">
            <tr>
                <td width="100%">
                    <table >
                        <tr>
                        <td >
                                <asp:ImageButton ID="imgBtnAddNew" runat="server" Height="41" ImageUrl="~/CommonImages/addnew.png"
                                    ToolTip="Add New" Width="48" onclick="imgBtnAddNew_Click"  />
                            </td>
                            <td id="tdClear" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class ="td">
                        <tr>
                            <td align="center" valign="top" class="tRowColorAdmin ">
                                <span class="titleheading">Item Master Query</span>
                            </td>
                        </tr>
                    </table>
                    <table width="75%" >
                        <tr>
                            <td align="right">
                                Select&nbsp;Branch:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Department:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Item&nbsp;Category:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlItemCate" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Item&nbsp;Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" onselectedindexchanged="ddlItemType_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            
                             <td align="right">
                                 &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <caption>
                            <br />
                            <tr>
                                <td align="right">
                                    Status
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSTATUS" runat="server" CssClass="gCtrTxt " 
                                        Font-Size="9" Width="160px" >
                                       
                                        
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    IS Excisable</td>
                                <td>
                                    <asp:DropDownList ID="ddlISEXCISABLE" runat="server" CssClass="gCtrTxt " 
                                        Font-Size="9" Width="160px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Consume
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCONSUME" runat="server" CssClass="gCtrTxt " 
                                        Font-Size="9" Width="160px" 
                                        onselectedindexchanged="ddlCONSUME_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    Approved/Rejected
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlApprovalStatus" runat="server" CssClass="gCtrTxt " 
                                        Font-Size="9" Width="160px" >
                                       <asp:ListItem Value="1" Selected="True">Approved</asp:ListItem>
                                       <asp:ListItem Value="3">Rejected</asp:ListItem>
                                        <asp:ListItem  Value="0">Pending</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                               
                                <td>
                                    <asp:Button ID="btnsave" runat="server" CssClass="AButton" Height="22px" 
                                        OnClick="btnsave_Click" Text="Get Record" Width="85px" />
                                </td>
                            </tr>
                            <caption>
                                <br />
                                <tr>
                                    <td colspan="4" width="50%">
                                        <b>Total&nbsp;Records: </b>
                                        <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="4" width="50%">
                                        <asp:UpdateProgress ID="UpdateProgress9587" runat="server">
                                            <ProgressTemplate>
                                                <h3>
                                                    Loading...</h3>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </caption>
                        </caption>
                    </table>
                    <table  width="100%" >
                        <tr>
                            <td align="left">
                                <asp:Panel ID="pnlShowHover" runat="server" ScrollBars="Auto" Width="100%">
                                    <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" 
                                        HeaderStyle-Font-Bold="true" Width="80%"
                                        AllowPaging="true" PageSize="12" CellPadding="3" ForeColor="#333333" GridLines="Both"
                                        BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" 
                                        OnPageIndexChanging="Grid1_PageIndexChanging">
                                       
                                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />

                                        <Columns>
                                            <asp:BoundField DataField="BRANCH_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CAT_CODE" HeaderStyle-HorizontalAlign="Left" HeaderText="Cat. Code"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ITEM_CODE" HeaderText="Item Code" Visible="true" />
                                            <asp:BoundField DataField="ITEM_TYPE" HeaderStyle-HorizontalAlign="Left" HeaderText="Item Type"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ITEM_DESC" HeaderStyle-HorizontalAlign="Left" HeaderText="Item Desc"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ITEM_MAKE" HeaderStyle-HorizontalAlign="Left" HeaderText="Item Make"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UOM" HeaderStyle-HorizontalAlign="Left" HeaderText="UOM"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="ITEM_STATUS" HeaderStyle-HorizontalAlign="Left" HeaderText="Item Status"
                                                ItemStyle-HorizontalAlign="Left" Visible="true">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="OP_BAL_STOCK" HeaderStyle-HorizontalAlign="Right" HeaderText="Opening Bal. St."
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OP_RATE" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Right"
                                                HeaderText="Opening Rate" ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MIN_STOCK_LVL" HeaderStyle-HorizontalAlign="Right" HeaderText="Min St. Level"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="REODR_QTY" HeaderStyle-HorizontalAlign="Right" HeaderText="Reorder Qty"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EXPIRY_DAYS" HeaderStyle-HorizontalAlign="Right" HeaderText="Expire Days"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="ITEM_SIZE" HeaderStyle-HorizontalAlign="Right" HeaderText="Item Size"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="WEIGHT" HeaderStyle-HorizontalAlign="Right" HeaderText="Weight"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="IS_EXCISABLE" HeaderStyle-HorizontalAlign="Right" HeaderText="IS Excisable"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TARIFF_HEADING" HeaderStyle-HorizontalAlign="Right" HeaderText="Tariff Heading"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            
                                             <asp:BoundField DataField="SALES_ITCHS_CODE" HeaderStyle-HorizontalAlign="Right" HeaderText="Sales ITCHS Code"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CUSTOM_ITCHS_CODE" HeaderStyle-HorizontalAlign="Right" HeaderText="Custom ITCHS Code"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>

                                             <asp:BoundField DataField="IS_MOVABLE" HeaderStyle-HorizontalAlign="Right" HeaderText="IS Movable"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="STATUS" HeaderStyle-HorizontalAlign="Right" HeaderText="Status"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
   
                                            
                                           <%-- <asp:BoundField DataField="ITEM_STATUS" HeaderStyle-HorizontalAlign="Left" HeaderText="Item status"
                                                ItemStyle-HorizontalAlign="Left" Visible="true">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
                                            <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" />
                                         <%-- <asp:BoundField DataField="CUSTOM_ITCHS_CODE" HeaderText="CUSTOMITCHSCODE" />--%>
                                         <asp:BoundField DataField="IS_APPROVED" HeaderText="Approved/Rejected" />
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
    <asp:PostBackTrigger  ControlID="imgBtnExportExcel"/>
    </Triggers>
    </asp:UpdatePanel>

