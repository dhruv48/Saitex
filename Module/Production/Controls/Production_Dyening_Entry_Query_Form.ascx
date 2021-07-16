<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Production_Dyening_Entry_Query_Form.ascx.cs" Inherits="Module_Production_Controls_Production_Dyening_Entry_Query_Form" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc6" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
    }
    
    
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 80px;
    }
    .c4
    {
        margin-left: 4px;
        width: 300px;
    }
    
    .c5
    {
        margin-left: 4px;
        width: 390px;
    }
    .d1
    {
        width: 150px;
    }
    .d2
    {
        margin-left: 4px;
        width: 200px;
    }
    .d3
    {
        width: 100px;
    }
</style>
 
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
        <table align="left" class=" td tContentArial" width="100%">
            <tr>
                <td class="td" colspan="8">
                    <table>
                        <tr>
                            <td >
                                <%--<asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" Enabled="false"></asp:ImageButton>--%>
                            </td>
                             <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Excel Report"
                            ImageUrl="~/CommonImages/export.png"  TabIndex="8" 
                                     onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                    
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                     TabIndex="9" onclick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td align="center" valign="top" >
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48"  TabIndex="10" onclick="imgbtnExit_Click" />
                            </td>
                            <td>
                                <%--<asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" colspan="8">
                    <span class="titleheading"><strong>Production Dyening Entry Query Form</strong></span>
                </td>
            </tr>
            <tr>
             
                <td class="tdRight">
                    Year:
                </td>
                <td >
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                        Font-Size="8"  Width="160px" TabIndex="1">
                    </asp:DropDownList>
                </td>
                
               <td align="right" >
                    Prod Trn No:
                </td>
                <td >
                
                    <cc2:ComboBox ID="cmbTRNNO" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="TRN_NUMB" DataValueField="TRN_TYPE" MenuWidth="160px" EnableVirtualScrolling="true" 
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="cmbTRN_LoadingItems"
                        EmptyText="" TabIndex="2">
                        <HeaderTemplate>
                            <div class="header d1">
                                Prod Trn No</div>
                            
                            </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item d1">
                                <%# Eval("TRN_NUMB ")%></div>
                             
                        </ItemTemplate>
                        <%--<FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>--%>
                    </cc2:ComboBox>
                   
                </td>
                <td align="right">
                    Quality Code :
                </td>
                <td>
                    <cc2:ComboBox ID="txtYarn" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="ARTICLE_CODE" DataValueField="ARTICAL_DESC" MenuWidth="550px" EnableVirtualScrolling="true"
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtYCODE_LoadingItems"
                        EmptyText="" TabIndex="3">
                         <HeaderTemplate>
                            <div class="header c1">
                                Quality Code</div>
                            <div class="header d1">
                                Quality Desc</div>
                            </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <%# Eval("ARTICLE_CODE")%></div>
                                <div class="item d1">
                                <%# Eval("ARTICAL_DESC")%></div>
                             
                        </ItemTemplate>
                       <%-- <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>--%>
                    </cc2:ComboBox>
                </td>
            </tr>
            
            
            
            <tr>
              <td align="right">
                    
                    
                    Party Name:
                    
                    
                </td>
                <td >
                    
                    
                    
                    
                    <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="PRTY_CODE" DataValueField="PARTY_NAME" EmptyText="" Width="160px"
                                    MenuWidth="350px" Height="200px" TabIndex="4" OnLoadingItems="cmbPartyCode_LoadingItems"
                                     EnableVirtualScrolling="true">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header d2">
                                            NAME</div>
                                      
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item d2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PARTY_NAME") %>' /></div>
                                      
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>--%>
                                </cc2:ComboBox>
                    
                    
                    
                </td>
                
                
                
                 <td align="right">
                    
                    
                    Job Card No:
                    
                    
                </td>
                <td >
                    
                    
                    
                    
                    <cc2:ComboBox ID="cmbJobCard" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="BATCH_CODE" DataValueField="BATCH_CODE" EmptyText="" Width="160px"
                                    MenuWidth="160px" Height="200px" TabIndex="5" OnLoadingItems="cmbJobCode_LoadingItems"
                                     EnableVirtualScrolling="true">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Job Card</div>
                                        
                                      
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("BATCH_CODE") %>' /></div>
                                       
                                      
                                    </ItemTemplate>
                                    <%--<FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>--%>
                                </cc2:ComboBox>
                    
                    
                    
                </td>
                
            </tr>
            <tr>
            <td></td>
                <td style="font-size:large;" colspan="3">
                    <b>Total Records : 
                    <asp:Label ID="lblTotalRecord" runat="server" ></asp:Label></b>
                </td>
               <%-- <td align="right" colspan="3">
                    <asp:UpdateProgress ID="UpdateProgress431" runat="server">
                        <ProgressTemplate>
                            <h3>
                                Loading...
                            </h3>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>--%>
                <td colspan="4" align="center">
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click" CssClass="AButton"  TabIndex="6"/>
                </td>
                <td colspan="6">
                </td>
                 
            </tr>
            <tr>
                <td class="td tContentArial" colspan="8" >
                    <asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto" Height="300px" >
                       <asp:GridView ID="grdLabDipSubmission" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                     CssClass="SmallFont" Width="120%" TabIndex="7" BackColor="White" 
                               OnPageIndexChanging="Grid1_PageIndexChanging"  >
                                  
                                  
                                  
                                    <Columns >
                                      <asp:TemplateField HeaderText="Year" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblYear" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("YEAR") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Trn No" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblTrnNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("TRN_NUMB") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="Trn Date" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblTrnDate" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("TRN_DATE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="Job Card" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblJOBCARD" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("JOB_CARD_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="CUST REQ NO" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblCUSTNO" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("CUST_REQ_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="PA NO" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblPA" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("PA_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="Order Qty" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblTRNQTY" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("TRN_QTY") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Shade Code" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblShade_Code" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SHADE_CODE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="Party Name" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblPartyName" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("PARTY_NAME") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="350px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                       
                                         <asp:TemplateField HeaderText="Article Description" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblArticleDesc" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("ARTICAL_DESC") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="500px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        
                                         <asp:TemplateField HeaderText="Posting Date" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblPostingDate" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("POST_DATE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Lot No" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblLotNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("GREY_LOT_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="UOM" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblUOM" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("UOM_OF_UNIT") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="Machine Code" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblMacode" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("MACHINE_CODE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="Process" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblProcess" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("PROCESS") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    
                                    
                                    
                                    
                                    <asp:TemplateField HeaderText="Baatch No" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblBatchNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("BATCH_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    
                                    
                                     <asp:TemplateField HeaderText="Cortoon No" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblCortoon" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("CORTOON_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="COPS" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblCOPS" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("COPS") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    
                                      
                                      
                                      
                                       <asp:TemplateField HeaderText="Net Wt" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblNetQty" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("GROSS_WT") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      
                                      
                                      
                                      
                                       <asp:TemplateField HeaderText="Rejected Cops" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblRejCops" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("REJ_COPS") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      
                                      
                                      
                                      
                                      
                                      <asp:TemplateField HeaderText="Rejected Trn Qty" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblRejTrnQty" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("REJ_TRN_QTY") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      
                                      
                                      
                                      <asp:TemplateField HeaderText="Trolly No" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblTrolly" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("TROLLY_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      
                                      
                                      
                                      
                                      
                                       <asp:TemplateField HeaderText="Conferm by " >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblTrolly" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("CONF_BY") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      
                                      
                                       <asp:TemplateField HeaderText="Conferm Date " >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblConfDate" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("CONF_DATE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      
                                      
                                       <asp:TemplateField HeaderText="Remarks" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblRemarks" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("CONF_REMARKS") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      
                                      
                                      
                                    </Columns>
                                     
                                     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <RowStyle VerticalAlign="Top" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                                </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                   
                </td>
            </tr>
             </table>
       </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="imgBtnExportExcel" />
       </Triggers>
        </asp:UpdatePanel>