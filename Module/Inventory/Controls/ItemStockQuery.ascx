<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemStockQuery.ascx.cs" Inherits="Module_Inventory_Controls_ItemStockQuery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <table>
     <tr>
    
    
        <td class="td">
            <table>
                <tr>
                    <td id="tdClear" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" onclick="imgbtnClear_Click"  />
                    </td>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click" ></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
     <tr>
        <td class="TableHeader td" align="center">
            <span class="titleheading"> Item Stock Query Form </span>
    </td>
    </tr>
     <tr>
        <td class="td">
            <table>
             <tr>
                    <td align="right" >
                      Department:
                    </td>
                    <td align="left" >
                        <asp:DropDownList ID="ddldepartment" runat="server" Height="20px" Font-Size="X-Small" 
                            Width="150px" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE">
                        </asp:DropDownList>
                    </td>
                    <td align="right" >
                          &nbsp;&nbsp;
                          Item :
                        </td>
                    <td align="left" style="margin-left: 120px" >
                        <asp:DropDownList ID="ddlitem" runat="server" Height="20px" Font-Size="X-Small" 
                            Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" >
                        &nbsp;&nbsp;
                        Item Type :
                    </td>
                    <td align="left" >
                        &nbsp;</td>
                  
                    <td align="center" >
                        <asp:DropDownList ID="ddlitemtype" runat="server"  
                            Width="150px" Height="20px" Font-Size="X-Small">
                        </asp:DropDownList>
                        
                    </td>
               <td><asp:Button ID="btnview" runat="server" Text="Show" onclick="btnview_Click" /></td>
                </tr>
            </table>
        </td>
    </tr>
   
     <tr>
        <td>
            <table>
              <tr>
                    <td >
                        <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Width="950px" Height="300px">
                            <asp:GridView ID="GridView1" runat="server"  ForeColor="#333333" 
                                GridLines="None" onrowdatabound="GridView1_RowDataBound" 
                                AutoGenerateColumns="false" Font-Size="10px"  Width="100%" 
                                CellPadding="4" AllowPaging="True" 
                                
    onpageindexchanging="GridView1_PageIndexChanging" PageSize="5">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="YTD ISSUE " 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblitemissue" runat="server" Text=""></asp:Label>
                                            <asp:LinkButton ID="linkissue" runat="server" CommandName="viewissue" 
                                                CssClass="Label SmallFont"  Text='<%# Eval("YTD_ISS") %>'>
                                            </asp:LinkButton>
                                            <asp:Panel ID="pnlitemissue" runat="server" BackColor="#C5E7F1" 
                                                BorderStyle="Ridge" BorderWidth="5px" ScrollBars = "Auto"  >
                                                <asp:GridView ID="grdvwitemissue" runat="server" AutoGenerateColumns="false" 
                                                    BorderColor="#C5E7F1">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="YEAR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtYEAR" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("YEAR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TRN_TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTRN_TYPE" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("TRN_TYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TRN_NUMB">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTRN_NUMB" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TRN_QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTRN_QTY" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtuom" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("UOM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TRN_QTY_ADJ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTRN_QTY_ADJ" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("TRN_QTY_ADJ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FINAL_RATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtFINAL_RATE" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("FINAL_RATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REMARKS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtREMARKS" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ISS_QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtISS_QTY" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("ISS_QTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DATE_OF_MFG">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtDATE_OF_MFG" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("DATE_OF_MFG", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ISS_REF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtISS_REF" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("ISS_REF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Button ID="btncancelyarnissue" runat="server" Text="Close" />
                                            </asp:Panel>
                                            <cc1:ModalPopupExtender ID="mpeyarnissue" runat="server" 
                                                BackgroundCssClass="modalBackground" DropShadow="true" 
                                                PopupControlID="pnlitemissue" TargetControlID="linkissue"  >
                                            </cc1:ModalPopupExtender>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="YTD RCPT " HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblytdrcpt" runat="server"></asp:Label>
                                            <asp:LinkButton ID="linkYTD_RCPT" runat="server" CommandName="viewreceive" CssClass="Label SmallFont"
                                               Text='<%# Eval("YTD_RCPT") %>' >
                                            </asp:LinkButton>
                                            <asp:Panel ID="pnlytdrcpt" runat="server" BorderStyle="Ridge" BorderWidth="5px" 
                                                BackColor="#C5E7F1">
                                                <asp:GridView ID="grdvwitemreceive" runat="server" BorderColor="#C5E7F1" 
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="YEAR">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtYEAR" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("YEAR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TRN NUMB">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTRN_NUMB" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TRN TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTRN_TYPE" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("TRN_TYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TRN QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTRN_QTY" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtuom" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("UOM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TRN QTY ADJ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTRN_QTY_ADJ" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("TRN_QTY_ADJ") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FINAL RATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtFINAL_RATE" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("FINAL_RATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REMARKS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtREMARKS" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ISS QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtISS_QTY" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("ISS_QTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DATE OF MFG">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtDATE_OF_MFG" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("DATE_OF_MFG" , "{0:dd/MM/yyyy}") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ISS_REF">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtISS_REF" runat="server" CssClass="Label" 
                                                                    Text='<%# Bind("ISS_REF") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Button ID="btncancelyarnreceive" runat="server" Text="Close" />
                                            </asp:Panel>
                                            <%-- <cc1:HoverMenuExtender ID="HoverMenuExtender2" runat="server" TargetControlID="linkYTD_RCPT"
                                                  PopupControlID="pnlytdrcpt" PopupPosition="Left" 
                                                  PopDelay="10">
                                            </cc1:HoverMenuExtender>--%>
                                            <cc1:ModalPopupExtender ID="mpeyarnreceive" runat="server" 
                                                PopupControlID="pnlytdrcpt" DropShadow="true"
                                                BackgroundCssClass="modalBackground" TargetControlID="linkYTD_RCPT" >
                                            </cc1:ModalPopupExtender>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="60px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="YEAR" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYEAR" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="COMP NAME" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCOMP_NAME" runat="server" Text='<%# Eval("COMP_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="BRANCH NAME" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBRANCH_NAME" runat="server" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="PRTY CODE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPRTY_CODE" runat="server" Text='<%# Eval("PRTY_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="ITEM CODE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITEM_CODE" runat="server" Text='<%# Eval("ITEM_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="ITEM DESC" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITEM_DESC" runat="server" Text='<%# Eval("ITEM_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="ITEM TYPE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITEM_TYPE" runat="server" Text='<%# Eval("ITEM_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="ITEM MAKE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITEM_MAKE" runat="server" Text='<%# Eval("ITEM_MAKE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="OP BAL STOCK" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOP_BAL_STOCK" runat="server" 
                                                Text='<%# Eval("OP_BAL_STOCK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="MIN STOCK LVL" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMIN_STOCK_LVL" runat="server" 
                                                Text='<%# Eval("MIN_STOCK_LVL") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="YTD ISS" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYTD_ISS" runat="server" Text='<%# Eval("YTD_ISS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="YTD RCPT" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYTD_RCPT" runat="server" Text='<%# Eval("YTD_RCPT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="OP QTY ADJ" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOP_QTY_ADJ" runat="server" Text='<%# Eval("OP_QTY_ADJ") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="OP RATE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOP_RATE" runat="server" Text='<%# Eval("OP_RATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="CURR RATE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCURR_RATE" runat="server" Text='<%# Eval("CURR_RATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="WT AVRG RATE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWT_AVRG_RATE" runat="server" 
                                                Text='<%# Eval("WT_AVRG_RATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="LAST PO RATE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLAST_PO_RATE" runat="server" 
                                                Text='<%# Eval("LAST_PO_RATE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="STOCK LAST UPDATE BY" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTOCK_LAST_UPDATE_BY" runat="server" 
                                                Text='<%# Eval("STOCK_LAST_UPDATE_BY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" 
                                        HeaderText="STOCK LAST UPDATE BY NAME" ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTOCK_LAST_UPDATE_BY_NAME" runat="server" 
                                                Text='<%# Eval("STOCK_LAST_UPDATE_BY_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="STOCK LAST UPDATE DATE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTOCK_LAST_UPDATE_DATE" runat="server" 
                                                Text='<%# Eval("STOCK_LAST_UPDATE_DATE" , "{0:dd/MM/yyyy}") %>'  ></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="REODR QTY" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblREODR_QTY" runat="server" Text='<%# Eval("REODR_QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="REODR LVL" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblREODR_LVL" runat="server" Text='<%# Eval("REODR_LVL") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="MIN PROCURE DAYS" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMIN_PROCURE_DAYS" runat="server" 
                                                Text='<%# Eval("MIN_PROCURE_DAYS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="EXPIRY DAYS" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEXPIRY_DAYS" runat="server" Text='<%# Eval("EXPIRY_DAYS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="QC REQUIRED" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQC_REQUIRED" runat="server" Text='<%# Eval("QC_REQUIRED") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="ITEM STATUS" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITEM_STATUS" runat="server" Text='<%# Eval("ITEM_STATUS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="ITEM REMARKS" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITEM_REMARKS" runat="server" 
                                                Text='<%# Eval("ITEM_REMARKS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="UOM" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="ASOC ITEM CODE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblASOC_ITEM_CODE" runat="server" 
                                                Text='<%# Eval("ASOC_ITEM_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="DEPT NAME" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDEPT_NAME" runat="server" Text='<%# Eval("DEPT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="RACK CODE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRACK_CODE" runat="server" Text='<%# Eval("RACK_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="CAT CODE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCAT_CODE" runat="server" Text='<%# Eval("CAT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="ITEM LAST UPDATED BY" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITEM_LAST_UPDATED_BY" runat="server" 
                                                Text='<%# Eval("ITEM_LAST_UPDATED_BY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" 
                                        HeaderText="ITEM LAST UPDATED BY NAME" ItemStyle-HorizontalAlign="Left" 
                                        ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITEM_LAST_UPDATED_BY_NAME" runat="server" 
                                                Text='<%# Eval("ITEM_LAST_UPDATED_BY_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="ITEM LAST UPDATED DATE" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITEM_LAST_UPDATED_DATE" runat="server" 
                                                Text='<%# Eval("ITEM_LAST_UPDATED_DATE", "{0:dd/MM/yyyy}") %>'  ></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="MAX STK LVL" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMAX_STK_LVL" runat="server" Text='<%# Eval("MAX_STK_LVL") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="60px" HeaderText="CONSUME" 
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCONSUME" runat="server" Text='<%# Eval("CONSUME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                       </asp:Panel>
                    </td>
            </tr>
            </table>
        </td>
    </tr> 
    
</table>
   

<%--</ContentTemplate>
</asp:UpdatePanel>--%>