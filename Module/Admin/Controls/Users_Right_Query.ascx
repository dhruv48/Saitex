<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Users_Right_Query.ascx.cs" Inherits="Module_Admin_Controls_Users_Right_Query" %>


<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" class=" td tContentArial" >
            <tr>
                <td>
                    <table width="100px">
                        <tr>
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
                    <table  >
                        <tr>
                            <td align="center" class="TableHeader td">
                                <span class="titleheading"><strong>User Right Query</strong> </span>
                            </td>
                        </tr>
                    </table>
                    <table style="border-style: ridge; border-color: White;">
                        <tr>
                            
                            
                            <td align="right" valign="top" width="100%">
                                User Name
                            </td>
                            <td align="left" valign="top" width="100%">
                                <asp:DropDownList ID="ddlUserName" runat="server" Width="140px" 
                                    CssClass="SmallFont BoldFont" 
                                    >
                                </asp:DropDownList>
                            </td>
                             <td align="right" valign="top" width="100">
                                <asp:Button ID="btngetrecord" runat="server" Text="Get Record" Width="100" Height="22"
                                    OnClick="btngetrecord_Click1" />
                            </td>
                        </tr>
                    </table>
                    <table >
                        <tr>
                            <td align="left" width="50%">
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                                    <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                            </td>
                            <td align="left" valign="top" width="50%" cssclass="Label">
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server">
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
                      <asp:GridView ID="grUser_Right_Query" runat="server" AutoGenerateColumns="False" 
                            AllowPaging="True" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" Width="100%"  > 
                          
                       
                     
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="COMP_NAME" HeaderText="User company" />
                                <asp:BoundField DataField="BRANCH_NAME" HeaderText="User Branch" />
                                <%--<asp:BoundField DataField="DEPT_NAME" HeaderText="User dept" />--%>
                             
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    
                    </td><td>
                       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            AllowPaging="True" AllowSorting="True" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont" VAlign="Top"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" Width="100%"  > 
                          
                       
                     
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                
                                <asp:BoundField DataField="DEPT_NAME" HeaderText="User dept" />
                             
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td></tr>
                      
                        
                     
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
