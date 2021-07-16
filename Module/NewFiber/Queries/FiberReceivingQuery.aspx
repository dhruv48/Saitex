<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master"  AutoEventWireup="true" CodeFile="FiberReceivingQuery.aspx.cs" Inherits="Module_Fiber_Queries_FiberReceivingQuery" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <style type="text/css">
        .item
        {
            position: relative !important;
            display: -moz-inline-stack;
            display: inline-block;
            zoom: 1; display:inline;overflow:hidden;white-space:nowrap;}
        .header
        {
            margin-left: 2px;
        }
        .c1
        {
            width: 150px;
        }
        .c2
        {
            margin-left: 4px;
            width: 150px;
        }
        .c3
        {
            margin-left: 4px;
            width: 150px;
        }
        .c4
        {
            width: 150px;
        }
        .c5
        {
            margin-left: 4px;
            width: 340px;
        }
        .c6
        {
            margin-left: 4px;
            width: 100px;
        }
    </style>
    <table class="td tContentArial">
        <tr>
            <td colspan="8">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <%--<td>
                                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" 
                                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" 
                                            ToolTip="Print" Width="48" />
                                    </td>--%>
                                   <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" OnClick="imgbtnClear_Click1" />
                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                            OnClick="imgbtnExit_Click" ToolTip="Exit" />
                                    </td>
                                  
                                    <td>
                                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                            OnClick="imgbtnHelp_Click" ToolTip="Help" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TableHeader tdinset td " colspan="15">
                <span class="titleheading">Fiber Recieving Query Form</span>
            </td>
        </tr>
        <tr>
            <td align="right">
                FIBER CODE :
            </td>
            <td>
                <cc2:ComboBox ID="ddlYarn" runat="server" AutoPostBack="True" CssClass="smallfont"
                    DataTextField="FIBER_CODE" DataValueField="FIBER_CODE" EnableLoadOnDemand="true"
                    MenuWidth="660" OnLoadingItems="Item_LOV_LoadingItems" EnableVirtualScrolling="true"
                    OpenOnFocus="true" TabIndex="9" Visible="true" Height="200px" EmptyText="---------All--------">
                    <HeaderTemplate>
                        <div class="header c4">
                            FIBER CODE</div>
                        <div class="header c5">
                            FIBER DESCRIPTION</div>
                        <div class="header c6">
                            TYPE</div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="item c4">
                            <%# Eval("FIBER_CODE")%></div>
                        <div class="item c5">
                            <%# Eval("FIBER_DESC")%></div>
                        <div class="item c6">
                            <%# Eval("FIBER_CAT")%></div>
                    </ItemTemplate>
                    <FooterTemplate>
                        Displaying items
                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                        out of
                        <%# Container.ItemsCount %>.
                    </FooterTemplate>
                </cc2:ComboBox>
            </td>
            <td align="right">
                PARTY :
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlParty" CssClass="SmallFont TextBox" runat="server"
                    Width="150px">
                </asp:DropDownList>
            </td>
            <td align="right">
                FROM TRN NO.:
            </td>
            <td align="left">
                <asp:TextBox ID="txtFromTrnNo" CssClass="SmallFont TextBox" runat="server" 
                    Width="75px"></asp:TextBox>
            </td>
            <td align="right">
                TO TRN NO.:
            </td>
            <td align="left">
                <asp:TextBox ID="txttoTrnNo" CssClass="SmallFont TextBox" runat="server"
                    Width="75px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;FROM TRN DATE:
            </td>
            <td align="left">
                <asp:TextBox ID="txtTrnFromDate" CssClass="SmallFont TextBox" runat="server" 
                    Width="75px"></asp:TextBox>
            </td>
            <td align="right">
                TO TRN DATE:
            </td>
            <td align="left">
                <asp:TextBox ID="txttoTrndate" CssClass="SmallFont TextBox" runat="server" AutoPostBack="true"
                    Width="75px"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <asp:Button ID="btnGetRecord" runat="server" Text="GetRecord" OnClick="btnGetRecord_Click" CssClass="AButton" />
            </td>
        </tr>
        <tr>
            <td colspan="8" class=" td tContentArial">
                <b>TOTAL RECORD : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td " colspan="15">
                <asp:Panel ID="pnlShowHover" runat="server" Width="950px" Height="350px"
                    ScrollBars="Auto">
                  
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BorderStyle="Ridge"
                        AllowSorting="True" CellPadding="3" CssClass="smallfont" EmptyDataText="No Record Found"
                        Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" 
                        Width="150%" BorderColor="#CCCCCC" 
                        onpageindexchanging="GridView1_PageIndexChanging" 
                         PageSize="14" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound"  BackColor="White" 
                            BorderWidth="1px"> 
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="YEAR" HeaderText="YEAR" />
                            <asp:TemplateField HeaderText="COMPANY" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompCode1" Text='<%#Eval("COMP_CODE") %>' ToolTip='<%#Eval("COMP_NAME") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
             
                                    <HeaderStyle HorizontalAlign="Left" />
             
                                </asp:TemplateField>
                               
 <asp:TemplateField HeaderText="BRANCH" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranch" Text='<%#Eval("BRANCH_NAME") %>' ToolTip='<%#Eval("BRANCH_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
             
                                    <HeaderStyle HorizontalAlign="Left" />
             
                                </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="FIBER" HeaderStyle-HorizontalAlign="Left" Visible="true" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblfIBER" Text='<%#Eval("FIBER_DESC") %>' ToolTip='<%#Eval("FIBER_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
           <asp:TemplateField HeaderText="PARTY NAME" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParty" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("PRTY_CODE") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
           </asp:TemplateField>
           <%--<asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" />--%>
           <asp:BoundField DataField="TRN_Date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="TRN DATE" />
           
                            
                            <asp:TemplateField HeaderText="TRN NUMB" HeaderStyle-HorizontalAlign="Left" Visible="true" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrnnumb" Text='<%#Eval("TRN_NUMB") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
           </asp:TemplateField>
           
                            <asp:BoundField DataField="TRN_QTY" DataFormatString="{0:0.00}" HeaderText="TRN QTY"  
                                ItemStyle-HorizontalAlign="Right" ControlStyle-ForeColor="#3399FF" ControlStyle-BackColor="Blue" >
                                <ControlStyle BackColor="Blue" ForeColor="#3399FF" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TRN_QTY_ADJ" HeaderText="TRN QTY ADJ" />
                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                            <asp:BoundField DataField="BASIC_RATE" HeaderText="BASIC RATE" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#0099CC">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FINAL_RATE" DataFormatString="{0:0.00}" HeaderText="FINAL RATE"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#669999">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MAC_CODE" HeaderText="MAC CODE" />
                           <%-- <asp:BoundField DataField="PI_NO" HeaderText="PI NO" />--%>
                            <asp:TemplateField HeaderText="PI NO"  HeaderStyle-HorizontalAlign="Left"    Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrnpino" Text='<%#Eval("PI_NO") %>' runat="server" ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="NO_OF_UNIT" HeaderText="NO OF UNIT" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM OF UNIT" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WEIGHT_OF_UNIT" DataFormatString="{0:0.00}" HeaderText="WEIGHT OF UNIT"
                                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="LR_NUMB" HeaderText="LR NUMB" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="SubTran" >
                            <ItemTemplate>
                            <asp:Label ID="LblFinalRate" Text="View Sub Tran"
                                            runat="server" ForeColor="#3333FF"></asp:Label>
                            <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid"
                                    BorderWidth="5px" HorizontalAlign="Left">
                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                        <asp:BoundField DataField="LOT_NO" HeaderText="LOT NO." HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="GRADE" HeaderText="GRADE" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TRN_QTY" HeaderText="QTY" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NO_OF_UNIT" HeaderText="NO. UNIT" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="WEIGHT OF UNIT" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="DATE_OF_MFG" HeaderText="DATE OF MANUFACTURE" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                        </asp:BoundField>
                                                       <asp:BoundField DataField="MATERIAL_STATUS" HeaderText="STATUS" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                  </asp:BoundField>
                                            
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                </asp:Panel>
                                <cc1:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnlBOM" TargetControlID="LblFinalRate"
                                    PopupPosition="Left">
                                </cc1:HoverMenuExtender>
                             </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="15">
                <cc1:CalendarExtender ID="CE1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTrnFromDate">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txtTrnFromDate" PromptCharacter="_">
                </cc1:MaskedEditExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender12" runat="server" Mask="99/99/9999"
                    MaskType="Date" TargetControlID="txttoTrndate" PromptCharacter="_">
                </cc1:MaskedEditExtender>
                <cc1:CalendarExtender ID="CE12" Format="dd/MM/yyyy" runat="server" TargetControlID="txttoTrndate">
                </cc1:CalendarExtender>
            </td>
        </tr>
    </table>
</asp:Content>
