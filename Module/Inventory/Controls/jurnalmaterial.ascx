<%@ Control Language="C#" AutoEventWireup="true" CodeFile="jurnalmaterial.ascx.cs" Inherits="Module_Inventory_Controls_jurnalmaterial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table class="td tContentArial" width = "950px">
<tr>
        <td align="left"  class="td" colspan = "8">
            <table align="left">
                <tr>               
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" onclick="imgbtnPrint_Click"   />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click"  ></asp:ImageButton>
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
        <td class="TableHeader td" align="center" colspan = "8">
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Transaction Query Form &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
 
 
 <tr>
 
           <td align = "right">Tran  Type :</td><td>
        <asp:DropDownList ID="ddltrn" runat="server" DataTextField="TRN_TYPE" 
            DataValueField="TRN_TYPE" 
             Width="160px" 
                AutoPostBack="True" CssClass="tContentArial" 
               onselectedindexchanged="ddltrn_SelectedIndexChanged1">
               
        </asp:DropDownList>
        </td><td align = "right">Party:</td><td>
<asp:DropDownList ID="ddlpartycode" runat="server" DataTextField="PRTY_NAME" 
                DataValueField="PRTY_CODE" 
                Width="160px" 
                AutoPostBack="True" CssClass="tContentArial" 
               onselectedindexchanged="ddlpartycode_SelectedIndexChanged">
         
            </asp:DropDownList>
            </td>
  
  
   <td  align = "right">Form Date :</td>
 <td>
     <asp:TextBox ID="txtformdate" runat="server" AutoPostBack="True" Width="127px" 
         CssClass="tContentArial" ontextchanged="txtformdate_TextChanged" 
        ></asp:TextBox>
     <cc4:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
         TargetControlID="txtformdate" PopupPosition="TopLeft">
                        </cc4:CalendarExtender>
            </td>
 <td align = "right">To Date :</td>
 <td><asp:TextBox ID="txtTodate" runat="server" AutoPostBack="True" Width="127px" 
         CssClass="tContentArial" ontextchanged="txtTodate_TextChanged" 
        ></asp:TextBox>
     <cc4:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
         TargetControlID="txtTodate" PopupPosition="TopLeft">
                        </cc4:CalendarExtender></td>
  </tr>
  

 
 

  <tr>
             <td colspan="8" width="100%" class="TdBackVir">
                            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td></tr>
  <table>
   <td colspan="5"  class = "td tContentArial">
   
  <asp:Panel ID="pnlShowHover" runat="server" Width="950px" BackColor="Beige" BorderWidth="2px"
                                ScrollBars="Auto">
      <asp:GridView ID="Gridrecevingmaterial" runat="server" 
    AutoGenerateColumns="False" 
                 Width="250%"  HeaderStyle-Wrap = "true" Font-Size="X-Small" 
          CellPadding="4" ForeColor="#333333" 
    GridLines="None" onrowdatabound="Gridrecevingmaterial_RowDataBound" 
          AllowPaging="True" PageSize="15" 
          onpageindexchanging="Gridrecevingmaterial_PageIndexChanging"  >
          <FooterStyle BackColor="#507CD1" Font-Bold="True" 
              ForeColor="White" />
          <RowStyle BackColor="#EFF3FB" />
          <Columns>
              <asp:TemplateField HeaderText="TRN NUMBER" Visible="TRUE" HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign = "Right">
                  <ItemTemplate>
                      <asp:Label ID="lbltrnnumber" runat="server" Text='<%# Bind("TRN_NUMB") %>' 
                                        CssClass="Label smallfont"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle Wrap="true" VerticalAlign="Top" />
              </asp:TemplateField>
              <asp:TemplateField HeaderText="TRN TYPE" Visible="TRUE">
                  <ItemTemplate>
                      <asp:Label ID="lbltrntype" runat="server" Text='<%# Bind("TRN_TYPE") %>' 
                                        CssClass="Label smallfont"></asp:Label>
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
              </asp:TemplateField>
              <asp:BoundField DataField="YEAR" HeaderText="YEAR "  Visible = "false" />
              <asp:BoundField DataField="COMP_CODE" HeaderText="COMP_CODE" 
                  Visible = "false"  />
              <asp:BoundField DataField="BRANCH_CODE" HeaderText="BRANCH_CODE" 
                  Visible = "false"  />
              <asp:BoundField DataField="COMP_NAME" HeaderText="COMPANY "  
                  Visible = "false" />
              <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH"  
                  Visible = "false" />
              <asp:BoundField DataField="TRN_DATE" HeaderText="TRN DATE" 
                  DataFormatString = {0:dd-MM-yyyy}  />
              <asp:BoundField DataField="GATE_NUMB" HeaderText="GATE NUMBER" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right" />
              <asp:BoundField DataField="GATE_DATE" HeaderText="GATE DATE" 
                  DataFormatString = {0:dd-MM-yyyy} />
              <asp:BoundField DataField="PRTY_CH_NUMB" HeaderText="PRTY CHALAN NUMBER" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right"/>
              <asp:BoundField DataField="PRTY_CH_DATE" HeaderText="PRTY CHALAN DATE" 
                  DataFormatString = {0:dd-MM-yyyy}/>
              <asp:BoundField DataField="PRTY_CODE" HeaderText="PRTY_CODE" 
                  Visible = "false" />
              <asp:BoundField DataField="LR_NUMB" HeaderText="LR NUMB" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right"/>
              <asp:BoundField DataField="LR_DATE" HeaderText="LR DATE" 
                  DataFormatString = {0:dd-MM-yyyy} />
              <asp:BoundField DataField="TRSP_CODE" HeaderText="TRSP CODE" />
              <asp:BoundField DataField="RCOMMENT" HeaderText="COMMENT" />
              <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT NAME" />
              <asp:BoundField DataField="GATE_OUT_NUMB" HeaderText="GATE OUT NUMBER"/>
              <asp:BoundField DataField="SHIFT" HeaderText="SHIFT"  />
              <asp:BoundField DataField="FORM_TYPE" HeaderText="FORM TYPE" />
              <asp:BoundField DataField="FORM_NUMB" HeaderText="FORM NUMB" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right"/>
              <asp:BoundField DataField="GATE_PASS_TYPE" HeaderText="GATE PASS TYPE" />
              <asp:BoundField DataField="STATUS" HeaderText="STATUS"  Visible = "false" />
              <asp:BoundField DataField="DEL_STATUS" HeaderText="DEL STATUS"  
                  Visible = "false" />
              <asp:BoundField DataField="TDATE" HeaderText="TDATE" 
                  DataFormatString = {0:dd-MM-yyyy}  Visible = "false"/>
              <asp:BoundField DataField="TUSER" HeaderText="TUSER"  Visible = "false"/>
              <asp:BoundField DataField="TDATE" HeaderText="TDATE"  
                  DataFormatString = {0:dd-MM-yyyy}  Visible = "false"/>
              <asp:BoundField DataField="REPROCESS"  HeaderText="REPROCESS" 
                  Visible = "false"/>
              <asp:BoundField DataField="CONF_FLAG" HeaderText="CONF FLAG" />
              <asp:BoundField DataField="CONF_BY" HeaderText="CONF BY" />
              <asp:BoundField DataField="BILL_TYPE" HeaderText="BILL TYPE" />
              <asp:BoundField DataField="BILL_NUMB" HeaderText="BILL NUMB" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right"/>
              <asp:BoundField DataField="BILL_YEAR" HeaderText="BILL YEAR" />
              <asp:BoundField DataField="LORY_NUMB" HeaderText="LORRY NUMB" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right" />
              <asp:BoundField DataField="REPROCESS" HeaderText="REPROCESS" Visible = "false"/>
              <asp:BoundField DataField="CONF_FLAG" HeaderText="CONF FLAG"  
                  Visible = "false" />
              <asp:BoundField DataField="CONF_BY" HeaderText="CONF BY"  Visible = "false" />
              <asp:BoundField DataField="CONF_DATE" HeaderText="CONF DATE" 
                  DataFormatString = {0:dd-MM-yyyy} />
              <asp:TemplateField HeaderText="Show Details">
                  <ItemTemplate>
                      <asp:Panel ID="pnlView" runat="server">
                          <asp:LinkButton ID="lbtnView" runat="server" Text="View Detail" 
                                            CssClass="Label"></asp:LinkButton>
                      </asp:Panel>
                      <asp:Panel ID="pnlShowHover" runat="server" Width="820px" BackColor="Beige" BorderWidth="2px"
                                         ScrollBars="Auto">
                          <asp:GridView ID="Grid2" runat="server" CssClass="SmallFont"  Width="300%"
                                            AutoGenerateColumns="False" >
                              <Columns >
                                  <asp:BoundField DataField="TRN_NUMB" HeaderText="TRN NUMBER" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right">
                                      <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" 
                                                        VerticalAlign="Top" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="YEAR" HeaderText="YEAR"  />
                                  <asp:BoundField DataField="COMP_NAME" HeaderText="COMPANY NAME" 
                                      Visible = "false" />
                                  <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH NAME" 
                                      Visible = "false"  />
                                  <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left" />
                                  <asp:BoundField DataField="TRN_NUMB" HeaderText="TRN NUMB" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right" />
                                  <asp:BoundField DataField="PO_COMP_CODE" HeaderText="PO COMP CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"
                                      Visible = "false" />
                                  <asp:BoundField DataField="PO_BRANCH" HeaderText="PO BRANCH" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"
                                      Visible = "false" />
                                  <asp:BoundField DataField="PO_TYPE" HeaderText="PO TYPE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"/>
                                  <asp:BoundField DataField="PO_NUMB" HeaderText="PO NUMB" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right" />
                                  <asp:BoundField DataField="ITEM_CODE" HeaderText="ITEM CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"/>
                                  <asp:BoundField DataField="TRN_QTY" HeaderText="TRN QTY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right"/>
                                  <asp:BoundField DataField="TRN_QTY_ADJ" HeaderText="TRN QTY ADJ" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right"/>
                                  <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                  <asp:BoundField DataField="FINAL_RATE" HeaderText="FINAL RATE" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right" />
                                  <asp:BoundField DataField="MAC_CODE" HeaderText="MAC CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left" />
                                  <asp:BoundField DataField="COST_CENTER_CODE" HeaderText="COST CENTER CODE" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"/>
                                  <asp:BoundField DataField="QCFLAG" HeaderText="QCFLAG" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"/>
                                  <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"/>
                                  <asp:BoundField DataField="LAB_CONF_DT" HeaderText="LAB CONF DT"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"
                                      Visible = "false" />
                                  <asp:BoundField DataField="LAB_SAMP_COLL_DT" HeaderText="LAB SAMP COLL DT"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"
                                      Visible = "false" />
                                  <asp:BoundField DataField="QC_REP_NO" HeaderText="QC REP NO"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right"
                                      Visible = "false" />
                                  <asp:BoundField DataField="MODVAT_AMT" HeaderText="MODVAT AMT"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right"
                                      Visible = "false" />
                                  <asp:BoundField DataField="TRN_REJ_QTY" HeaderText="TRN REJ QTY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right" />
                                  <asp:BoundField DataField="CONE_NO" HeaderText="CONE NO" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right" />
                                  <asp:BoundField DataField="AVG_CONE_WT" HeaderText="AVG CONE WT" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"/>
                                  <asp:BoundField DataField="ISS_REF" HeaderText="ISS REF" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"/>
                                  <asp:BoundField DataField="ISS_QTY" HeaderText="ISS QTY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right"/>
                                  <asp:BoundField DataField="DATE_OF_MFG" HeaderText="DATE OFMFG"  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"
                                      DataFormatString = {0:dd-MM-yyyy}/>
                                  <asp:BoundField DataField="STATUS" HeaderText="STATUS" Visible = "false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"/>
                                  <asp:BoundField DataField="RTN_QTY" HeaderText="RTN QTY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign = "Right" />
                                  <asp:BoundField DataField="PO_YEAR" HeaderText="PO YEAR" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign ="Left"/>
                              </Columns>
                              <RowStyle CssClass="SmallFont" Width="98%" />
                              <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                          </asp:GridView>
                      </asp:Panel>
                      <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="pnlView"
                                        PopupControlID="pnlShowHover" PopupPosition="Left" 
                          PopDelay="10">
                      </cc1:HoverMenuExtender>
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
              </asp:TemplateField>
          </Columns>
          <PagerStyle BackColor="#2461BF" ForeColor="White" 
              HorizontalAlign="Center" />
          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" 
              ForeColor="#333333" />
          <HeaderStyle BackColor="#507CD1" Font-Bold="True" 
              ForeColor="White" />
          <EditRowStyle BackColor="#2461BF" />
          <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
                                   
   </asp:Panel>
        </td>
 </table>
 </tr> 

 </table>