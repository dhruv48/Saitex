<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiberMasterQuery.ascx.cs" Inherits="Module_Fiber_Controls_FiberMasterQuery" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class=" td tContentArial"  width = "100%">
            <tr>
                <td>
                    <table class=" td tContentArial"   >
                        <tr><td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" OnClick="imgbtnPrint_Click1" />
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
                                    ToolTip="Help" Width="48" onclick="imgbtnHelp_Click" />
                            </td>
                        </tr>
                    </table>
                    
                        <tr>
                            <td align="center" class="TableHeader td" colspan = "6" >
                                <span class="titleheading"><strong>Fiber Master Query</strong> </span>
                            </td>
                        </tr>
                   
                  
                        <tr>
                     <td align="right" valign="top" >
                                BRANCH&nbsp;NAME&nbsp; :
                            </td>
                            <td align="left" valign="top" >
                                <asp:DropDownList ID="ddlBranchCode" runat="server"
                                    CssClass="SmallFont BoldFont" 
                                    onselectedindexchanged="ddlBranchCode_SelectedIndexChanged" >
                                   </asp:DropDownList>
                                 <td align="right" valign="top" >
                                FIBER&nbsp;CATEGORY&nbsp; :
                            </td>
                           <td align="left" valign="top" >
                                <asp:DropDownList ID="ddlFiberCat" runat="server" 
                                    CssClass="SmallFont BoldFont" 
                                    onselectedindexchanged="ddlFiberCat_SelectedIndexChanged" >
                                    </asp:DropDownList>
                            </td>
                            
                            
                            
                             <td align="right" valign="top">
                                <asp:Button ID="Button1" runat="server" Text="Get Record" Width="118px" Height="25px"
                                    OnClick="btngetrecord_Click1" style="text-align: center" CssClass="AButton" />
                            </td>
                        </tr>
                  
                   
                        <tr>
                            <td align="left" >
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                                    <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                            </td>
                       <%--     <td align="left" valign="top" width="100%" cssclass="Label">
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>--%>
                        </tr>
                    
                   
                    <tr>
                    <td colspan= "7" >
                      <asp:GridView ID="grdFiberMasterQuery" runat="server" 
                            AutoGenerateColumns="False"  Width = "100%"
                            AllowPaging="True" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" onpageindexchanging="grUserMasterQuery_PageIndexChanging" 
                            > 
                           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns> 
                                                             
                                <asp:TemplateField HeaderText="Fiber Desc">
                                <ItemTemplate>
                                <asp:Label ID="lblFabCode"  runat="server" Text='<%#Eval("FIBER_DESC") %>' ToolTip='<%#Eval("FIBER_CODE") %>' ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                               <%-- <asp:BoundField DataField="FIBER_DESC" HeaderText="Fiber Description" />--%>
                                <asp:BoundField DataField="FIBER_CAT" HeaderText="Fiber Cat" />
                                <asp:BoundField DataField="SUB_FIBER_CAT" HeaderText="Sub Fiber Cat" />                               
                                <asp:BoundField DataField="LENGTH_TYPE" HeaderText="Length Type" />
                                <asp:BoundField DataField="LENGTH_VALUE" HeaderText="Length Value" />
                                <asp:BoundField DataField="LUSTURE" HeaderText="Lusture" />
                                <asp:BoundField DataField="DENIER" HeaderText="Denier" />
                                <asp:BoundField DataField="FANCY_EFFECT" HeaderText="Fancy Effect" />
                                <asp:BoundField DataField="TENACITY" HeaderText="Tenacity" />
                                 <asp:BoundField DataField="UOM_BAIL" HeaderText="KG/BALE" />
                                <asp:BoundField DataField="OPEN_RATE" HeaderText="Open Rate" />
                                <asp:BoundField DataField="MAXIMUM_STOCK" HeaderText="Maximum Stock" />
                                <asp:BoundField DataField="REMARK" HeaderText="Remarks" />
                                 <asp:BoundField DataField="PRTY_NAME" HeaderText="Party Name" />
                                  <asp:BoundField DataField="BRANCH_NAME" HeaderText="Branch" /> 
                                
                             
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
    </ContentTemplate>
</asp:UpdatePanel>