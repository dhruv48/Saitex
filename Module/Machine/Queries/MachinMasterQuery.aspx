<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="MachinMasterQuery.aspx.cs" Inherits="Module_Machine_Queries_MachinMasterQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
 <%@ register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc1" %>
<style type="text/css">
 .header
 {
    font-weight:bold;
    position:absolute;
    background-color:White;
  }
  </style>
    <table class="tContentArial" align="left" width="945px">
        <tr>
            <td class="td" align="left">
                <table>
                    <td id="tdPrint" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" TabIndex="9" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" TabIndex="10" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" valign="top">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" TabIndex="11"></asp:ImageButton>
                    </td>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top" class="tRowColorAdmin td">
                <span class="titleheading">Machine Master Report</span>
            </td>
        </tr>
        <tr>
            <td>
                <table class="td tContentArial" width="945px">
                    <tr>
                        <td align="right" valign="top">
                            Segment :
                        </td>
                        <td align="left" valign="top">
                            <asp:DropDownList ID="ddlSegment" runat="server" Width="100px" CssClass="tContentArial">
                            </asp:DropDownList>
                        </td>
                        <td align="right" valign="top">
                            Section :
                        </td>
                        <td align="left" valign="top">
                            <asp:DropDownList ID="ddlSection" runat="server" Width="100px" CssClass="tContentArial">
                            </asp:DropDownList>
                        </td>
                        <td align="right" valign="top">
                            Type :
                        </td>
                        <td align="left" valign="top">
                            <asp:DropDownList align="right" ID="ddlType" runat="server" Width="100px" CssClass="tContentArial">
                            </asp:DropDownList>
                        </td>
                        <td align="right" valign="top">
                            Group :
                        </td>
                        <td align="left" valign="top">
                            <asp:DropDownList ID="ddlGroup" runat="server" Width="100px" CssClass="tContentArial">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="GetRecord" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Total Records : </b>
                            <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <table>
                        <td class="td tContentArial">
                            <asp:Panel ID="pnlShowHover" runat="server" Width="950px" Height="420px" ScrollBars="Auto"  >
                                <asp:GridView ID="GridMachinMaster" runat="server" AutoGenerateColumns="False" CellPadding="4"  
                                    ForeColor="#333333" HeaderStyle-Wrap="true" Font-Size="X-Small" 
                                    PageSize="12" onrowcreated="GridMachinMaster_RowCreated" 
                                    AllowPaging="True" onpageindexchanging="GridMachinMaster_PageIndexChanging" >
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                    <Columns>
                                                                    
                                 
                                        
                                        <asp:TemplateField HeaderText="MACHINE GROUP"> 
                                     
                                        <ItemTemplate> 
                                            <asp:Label ID="Label1" runat="server" 
                                                 ToolTip='<%# Eval("MGROUP") %>'  
                                                 Text='<%# Eval("MACHINE_GROUP") %>'> 
                                            </asp:Label> 
                                        </ItemTemplate> 
                                            <ItemStyle BackColor="#FFFFCC" />
                                     </asp:TemplateField>  
                                      <asp:BoundField DataField="MACHINE_CODE" HeaderText="MACHINE CODE " 
                                           >  
                                        <ItemStyle BackColor="#FFFFCC" />
                                        </asp:BoundField>
                                       <asp:BoundField DataField="MACHINE_MAKE" HeaderText="MACHINE MAKE" 
                                            ItemStyle-BackColor="#FFFFCC" >                               
                                        <ItemStyle />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="MACHINE POWER" >
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("MACHINE_POWER") %>' ItemStyle-BackColor="#0099FF"> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_MACHINE_POWER") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#FFFFCC" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="YOM" HeaderText="YOM " 
                                            >
                                 
                                        <ItemStyle BackColor="#FFFFCC" />
                                        </asp:BoundField>
                                 
                                        <asp:TemplateField HeaderText="NO OF HEADS" ItemStyle-BackColor="#66FF66">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("NO_OF_HEADS") %>'> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_NO_OF_HEADS") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66FF66" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NO OF SPINDLES" ItemStyle-BackColor="#66FF66">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("NO_OF_SPINDLES") %>'> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_NO_OF_SPINDLES") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66FF66" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NO OF PACKAGES" ItemStyle-BackColor="#66FF66">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("NO_OF_PACKAGES") %>'> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_NO_OF_PACKAGES") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66FF66" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MAX PACKAGE" ItemStyle-BackColor="#66FF66">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("MAX_PACKAGE") %>'> 
                                                </asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_MAX_PACKAGE") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66FF66" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MACHINE SPEED" ItemStyle-BackColor="#66FF66">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("MACHINE_SPEED") %>'> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_SPEED") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66FF66" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MACHINE CAPACITY" ItemStyle-BackColor="#66FF66">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("MACHINE_CAPACITY") %>'> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_CAPACITY") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66FF66" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" COUNT PROD RATIO" ItemStyle-BackColor="#66FF66">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("COUNT_PROD_RATIO") %>'> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_COUNT_PROD_RATIO") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66FF66" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MANPOWER" ItemStyle-BackColor="#66CCFF">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("MANPOWER") %>'> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_MANPOWER") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66CCFF" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STEAM" ItemStyle-BackColor="#66CCFF">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("STEAM") %>'> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_STEAM") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66CCFF" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" SOFTWARE" ItemStyle-BackColor="#66CCFF">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SOFTWATER") %>'> 
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_SOFTWATER") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66CCFF" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AIR" ItemStyle-BackColor="#66CCFF">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("AIR") %>'> 
                         
                                                </asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("UOM_AIR") %>'> 
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="#66CCFF" />
                                        </asp:TemplateField>
                                      
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </table>
        </tr>
    </table>
    </td> </tr> </table>
</asp:Content>
