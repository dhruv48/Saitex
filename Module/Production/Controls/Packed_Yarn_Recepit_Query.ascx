<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Packed_Yarn_Recepit_Query.ascx.cs" Inherits="Module_Production_Controls_Packed_Yarn_Recepit_Query" %>


<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        height: 25px;
    }
    .style2
    {
        height: 25px;
        width: 4px;
    }
    .style3
    {
        width: 4px;
    }
</style>

<%--<asp:UpdatePanel ID="UpdatePanel8971" runat="server">
    <ContentTemplate>--%>
        <table width="100%" class ="td tContentArial">
            <tr>
                <td width="100%">
                    <table >
                        <tr>
                        <td >
                                &nbsp;</td>
                            <td id="tdClear" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" onclick="imgbtnHelp_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class ="td">
                        <tr>
                            <td align="center" valign="top" class="tRowColorAdmin ">
                                <span class="titleheading">Packed Yarn&nbsp; Query</span>
                            </td>
                        </tr>
                    </table>
                    <table width="75%" >
                        <tr>
                            <td align="right" class="style1">
                                Select&nbsp;Branch:
                            </td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" >
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="style2">
                                Department </td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                <td align="right" class="style2">
                                    &nbsp;</td>
                            <td class="style1">
                                &nbsp;</td>
                            <td align="right" class="style1">
                                Lot No </td>
                            <td class="style1">
                                <asp:DropDownList ID="ddlLotNo" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="style1">
                                &nbsp;</td>
                            <td class="style1">
                                &nbsp;</td>
                            <td align="right" class="style1">
                                </td>
                            <td class="style1">
                                </td>
                            
                             <td align="right" class="style1">
                                 </td>
                            <td class="style1">
                                </td>
                        </tr>
                        <caption>
                            <br />
                            <tr>
                                <td align="right">
                                    From Date </td>
                                <td>
            <asp:TextBox ID="txtformdate" runat="server" AutoPostBack="True" Width="126px" ></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtformdate" Format="dd-MM-yyyy">
                        </cc1:CalendarExtender>                    
                                </td>
                                <td class="style3">
                                    To Date </td>
                                <td>
            <asp:TextBox ID="txtTodate" runat="server" AutoPostBack="True" Width="126px" ></asp:TextBox>
               <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTodate" Format="dd-MM-yyyy">
                        </cc1:CalendarExtender>                  
                                
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
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
                                        BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" >
                                       <%-- OnPageIndexChanging="Grid1_PageIndexChanging"--%>
                                       
                                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />

                                        <Columns>
                                        
                                            <asp:BoundField DataField="BRANCH_NAME" HeaderText="Branch" />
                                        <%-- <asp:BoundField DataField="BRANCH_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="Dept Name"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>--%>
            
                                         <asp:BoundField DataField="YEAR" HeaderText="Year" />
                                            <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN Type" />
                                            <asp:BoundField DataField="PO_TYPE" HeaderText="PO Type" />
                                            <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn Code" />
                                           <asp:BoundField DataField="TRN_QTY" HeaderText="TRN Qty" />
                                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                            <asp:BoundField DataField="FINAL_RATE" HeaderText="Final Rate" />
                                          <asp:BoundField DataField="COST_CENTER_CODE" HeaderText="Cost center Code" />
                                            <asp:BoundField DataField="TUSER" HeaderText="TUSER" />
                                            <asp:BoundField DataField="PI_NO" HeaderText="PI No" />
                                         <asp:BoundField DataField="NO_OF_UNIT" HeaderText="No Of Unit" />
                                         <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM of Unit" />
                                         
                                          <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="Wt Of Unit" />
                                          <asp:BoundField DataField="BASIC_RATE" HeaderText="Basic rate" />
                                            <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade" />
                                            <asp:BoundField DataField="RGB" HeaderText="RGB" />
                                         <asp:BoundField DataField="LOCATION" HeaderText="Location " />
                                         <asp:BoundField DataField="LOT_NO" HeaderText="Lot No " />
                                           <asp:BoundField DataField="GRADE" HeaderText="Grade" />
                                          <asp:BoundField DataField="GROSS_WT" HeaderText="Gross Wt" />
                                            <asp:BoundField DataField="TARE_WT" HeaderText="TARE WT" />
                                            <asp:BoundField DataField="CARTONS" HeaderText="Cartons" />
                                         <asp:BoundField DataField="LOCATION" HeaderText="Location " />
                                         
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
    <%--</ContentTemplate>--%>