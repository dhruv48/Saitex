<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OrderTracking.aspx.cs" Inherits="Module_OrderDevelopment_Pages_OrderTracking" %>
<%@ Register Src="../../../CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>




<%--<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">

 
 


</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">






            <table class="td tContentArial" width="95%">
            <tr>
                <td class="td tContentArial">
                    <table>
                        <tr>
                           
                            <td id="td2" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                           
                            <td id="td4" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="td5" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/CommonImages/export.png" OnClick="imgExcel_Click"
                                    ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                            </td>

                             </td>
                            <td id="td6" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"
                                    ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="M1" />
                    <asp:Label ID="Label1" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="Label2" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading"><b>Order Tracking & JobCard Analysis</b></span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table width="100%">
                        <tr>
                           
                            <td align="right" valign="top">
                                Party Code :
                            </td>
                            <td align="left" valign="top">
                                <uc1:partycodelov ID="ddlprtycode" runat="server" TabIndex="4" Width="75px" />
                            </td>
                            <td align="right" valign="top">
                                Quality :
                            </td>
                            <td align="left" valign="top">
                                <cc2:ComboBox ID="ddlArticle" runat="server" CssClass="smallfont" EnableLoadOnDemand="True" 
                                    DataTextField="YARN_CODE" DataValueField="YARN_DESC" EmptyText="Select Article"
                                    MenuWidth="400px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="11"
                                    Visible="true" Height="200px" Width="98%" OnLoadingItems="ddlArticle_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Article Code
                                        </div>
                                        <div class="header c2">
                                            Description
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("YARN_CODE") %>
                                        </div>
                                        <div class="item c2">
                                            <%# Eval("YARN_DESC") %>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>


                            <td align="right" valign="top">
                                Shade No. :
                            </td>
                            <td align="left" valign="top">
                               <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="SHADE_FAMILY_CODE" DataValueField="SHADE_CODE" EnableLoadOnDemand="True"
                                    MenuWidth="300px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="16" 
                                    Height="200px" Visible="true" Width="98%" OnLoadingItems="cmbShade_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Shade Family Name
                                        </div>
                                        <div class="header c2">
                                            Shade Code
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("SHADE_FAMILY_CODE")%>
                                        </div>
                                        <div class="item c2">
                                            <%# Eval("SHADE_CODE")%>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>

                            <%-- <td align="right" valign="top">
                               Batch Card No. :
                            </td>
                            <td align="right" valign="top">
                                <asp:DropDownList ID="ddlBatchCard" runat="server"text="Select BatchCard"></asp:DropDownList>
                            </td>--%>

                           




                        </tr>


                        <tr>
                             <td align="right" valign="top">
                               Job Card No. :
                            </td>
                           <td align="left" valign="top">
                                <asp:TextBox ID="txt_BatchCode" runat="server" CssClass="TextBox" TabIndex="1"></asp:TextBox>
                                
                            </td>

                             <td align="right" valign="top">
                               Order No. :
                            </td>
                           <td align="left" valign="top">
                                <asp:TextBox ID="txtOrderNo" runat="server" CssClass="TextBox" TabIndex="1"></asp:TextBox>
                                    
                            </td>

                            <td align="right" valign="top">
                               Report Category. :
                            </td>
                           <td align="left" valign="top">
                                
                               <asp:DropDownList ID="ddl_category" runat="server" >
                                   <asp:ListItem Text="Select Category" Value="0"/>
                                   <asp:ListItem Text="Order Tracking" Value="1"/>
                                   <asp:ListItem Text="JobCard Analysis" Value="2"/>
                               </asp:DropDownList>
                                <asp:Button ID="Button1" runat="server" CssClass="SmallFont" OnClick="Button1_Click"
                                    Text="Show" />
                            </td>

                            </tr>
                      
            
            <tr>
                <td align="left">
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto">
                        <span class="titleheading"><b>
                          
                        </b></span>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

                         <tr>
        <td class="td tContentArial" colspan="8">
           <%-- <asp:Panel ID="pnlShowHover" runat="server" Height="350px" ScrollBars="Auto" Width="98%">--%>
                <asp:GridView ID="Grid_OrderTracking" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CellPadding="3" CssClass="smallfont" EmptyDataText="No Record Found"
                    Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" PageSize="25"
                     OnPageIndexChanging="Grid_OrderTracking_PageIndexChanging">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                        <asp:TemplateField HeaderText="SR.NO." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                   
                        <asp:TemplateField HeaderText="YEAR">
                            <ItemTemplate>
                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("YEAR")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ORDER NO">
                            <ItemTemplate>
                                <asp:Label ID="lblOrder" runat="server" Text='<%#Eval("ORDER_NO")%>' ToolTip='<%#Eval("ORDER_NO")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="PI NO ">
                            <ItemTemplate>
                                <asp:Label ID="lblPi_No" runat="server" Text='<%#Eval("PI_NO")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                             </asp:TemplateField>
                              <asp:TemplateField HeaderText="ORDER DATE ">
                            <ItemTemplate>
                                <asp:Label ID="lblOrderDate" runat="server" Text='<%#Eval("ORDER_DATE")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="2%" />
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="PRTY NAME      ">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblPartyName" runat="server" Font-Bold="true" Text='<%#Eval("PRTY_NAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="true" HorizontalAlign="Left" Width="2%" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="QUALITY      ">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuality" runat="server" Font-Bold="true" Text='<%#Eval("PARTY_ARTICLE_DESC") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                        </asp:TemplateField>
                       
                             <asp:TemplateField HeaderText="SHADE CODE">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblShade" runat="server" Font-Bold="true" Text='<%#Eval("SHADE_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QUANTITY">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("QUANTITY") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QTY APPROVED.">
                            <ItemStyle HorizontalAlign="Right" BackColor="#99FF66" Font-Bold="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblOpnBal" runat="server" Text='<%#Eval("QTY_APPROVED","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PLAN_QTY">
                            <ItemTemplate>
                                  <asp:Label ID="lblPlanQty" runat="server" Text='<%#Eval("PLAN_QTY","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="JOB_CARD_NO.">
                            <ItemTemplate>
                              

                                 <asp:Label ID="lblJobCardNo" runat="server" Text='<%#Eval("JOB_CARD_NO","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BATCH DATE.">
                            <ItemStyle HorizontalAlign="Right"  Font-Bold="true" />
                            <ItemTemplate>
                                
                                 <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("BATCH_DATE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="JOB QTY">
                            <ItemStyle HorizontalAlign="Right"  Font-Bold="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblJobQty" runat="server" Text='<%#Eval("JOB_QTY","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="JOB CARD STATUS">
                            <ItemTemplate>
                              
                                  <asp:Label ID="lblJobCardStatus" runat="server" Text='<%#Eval("JOB_CARD_STATUS")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DYEING QTY">
                            <ItemTemplate>
                               
                                 <asp:Label ID="lblDyeingQty" runat="server" Text='<%#Eval("DYEING_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="QC FLAG">
                            <ItemStyle HorizontalAlign="Right" BackColor="#99CCFF" Font-Bold="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblQcFlag" runat="server" Text='<%#Eval("QC_FLAG","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HYDRO FLAG">
                            <ItemStyle HorizontalAlign="Right" BackColor="#99CCFF" />
                            <ItemTemplate>
                                <asp:Label ID="lblHydroFlag" runat="server" Text='<%#Eval("HYDRO_FLAG")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="ISS QTY">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lblIssQty" runat="server" Text='<%#Eval("ISS_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="PCW QTY">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lblPcwQty" runat="server" Text='<%#Eval("PCW_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="DISPATCHED QTY">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lblDispatchQty" runat="server" Text='<%#Eval("DISPATCHED_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="INVOICE QTY">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceQty" runat="server" Text='<%#Eval("INVOICE_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="FINAL RATE">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lblFinalRate" runat="server" Text='<%#Eval("FINAL_RATE")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>

                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                </asp:GridView>

            <asp:GridView ID="GridJobCardAnalysis" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" CellPadding="3" CssClass="smallfont" EmptyDataText="No Record Found"
                    Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" PageSize="25"
                     OnPageIndexChanging="Grid_OrderTracking_PageIndexChanging">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                        <asp:TemplateField HeaderText="SR.NO." ItemStyle-Width="1%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                   
                          <asp:TemplateField HeaderText="JOB_CARD_NO.">
                            <ItemTemplate>
                              

                                 <asp:Label ID="lblJobCardNo" runat="server" Text='<%#Eval("JOB_CARD_NO","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="JOB CARD DATE">
                            <ItemStyle HorizontalAlign="Right"  Font-Bold="true" />
                            <ItemTemplate>
                                
                                 <asp:Label ID="lblJobCardDate" runat="server" Text='<%#Eval("JOB_CARD_DATE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                       
                         <asp:TemplateField HeaderText="ORDER NO">
                            <ItemTemplate>
                                <asp:Label ID="lblOrder" runat="server" Text='<%#Eval("ORDER_NO")%>' ToolTip='<%#Eval("ORDER_NO")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                        </asp:TemplateField>
                          
                            
                      
                        <asp:TemplateField HeaderText="PRTY NAME      ">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblPartyName" runat="server" Font-Bold="true" Text='<%#Eval("PRTY_NAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="true" HorizontalAlign="Left" Width="2%" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="QUALITY      ">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuality" runat="server" Font-Bold="true" Text='<%#Eval("QUALITY") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                        </asp:TemplateField>
                       
                             <asp:TemplateField HeaderText="SHADE CODE">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblShade" runat="server" Font-Bold="true" Text='<%#Eval("SHADE_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                        </asp:TemplateField>
                      <%--  <asp:TemplateField HeaderText="QUANTITY">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("QUANTITY") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                        </asp:TemplateField>--%>
                         <asp:TemplateField HeaderText="LOT NO">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblLotNo" runat="server" Text='<%#Eval("LOT_NO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                        </asp:TemplateField>
                     
                         <asp:TemplateField HeaderText="JOBER NAME">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblJoberName" runat="server" Text='<%#Eval("JOBER_NAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="1%" />
                        </asp:TemplateField>
                     
                          <asp:TemplateField HeaderText="SHADE CODE">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblShade" runat="server" Font-Bold="true" Text='<%#Eval("SHADE_CODE") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="BATCH NO">
                            <ItemStyle HorizontalAlign="Left" />
                            <ItemTemplate>
                                <asp:Label ID="lblBatchNo" runat="server" Font-Bold="true" Text='<%#Eval("BATCH_NO") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="JOB QTY">
                            <ItemStyle HorizontalAlign="Right"  Font-Bold="true" />
                            <ItemTemplate>
                                <asp:Label ID="lblJobQty" runat="server" Text='<%#Eval("QTY","{0:N3}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="CHEESES">
                            <ItemTemplate>
                              
                                  <asp:Label ID="lblCheeses" runat="server" Text='<%#Eval("CHEESES")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="JOB CARD STATUS">
                            <ItemTemplate>
                              
                                  <asp:Label ID="lblJobCardStatus" runat="server" Text='<%#Eval("JOB_CARD_STATUS")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DYEING QTY">
                            <ItemTemplate>
                               
                                 <asp:Label ID="lblDyeingQty" runat="server" Text='<%#Eval("PROD_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                       
                        

                         <asp:TemplateField HeaderText="ISS QTY">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lblIssQty" runat="server" Text='<%#Eval("ISS_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="PCW QTY">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lblPcwQty" runat="server" Text='<%#Eval("PCW_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="DISPATCHED QTY">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lblDispatchQty" runat="server" Text='<%#Eval("DISPATCHED_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="INVOICE QTY">
                            <ItemStyle HorizontalAlign="Right" BackColor="#CCCCCC" />
                            <ItemTemplate>
                                <asp:Label ID="lblInvoiceQty" runat="server" Text='<%#Eval("INVOICE_QTY")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                        </asp:TemplateField>
                       

                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                        VerticalAlign="Middle" />
                </asp:GridView>

           <%-- </asp:Panel>--%>
        </td>
    </tr>


        </table>
      <%--  <cc1:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="TxtFdate"
            runat="server">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="TxtTdate"
            runat="server">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="TxtTdate" PromptCharacter="_">
        </cc1:MaskedEditExtender>--%>



            </table>
</asp:Content>



     