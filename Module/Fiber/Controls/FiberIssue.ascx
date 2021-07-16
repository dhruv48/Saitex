<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiberIssue.ascx.cs" Inherits="Module_Fiber_Controls_FiberIssue" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table class="td tContentArial" width = "950px">
    <tr>
        <td align="left"  class="td" colspan = "8">
            <table align="left">
                <tr>               
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" onclick="imgbtnPrint_Click" />
                    </td>
                    <td>  
<asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
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
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Poy&nbsp; Issue  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    
 <tr>
 
           <td align = "right" style="text-align: right">Transaction Type :</td>
           <td>
    
                         <asp:DropDownList ID="DDLTrnType"
                             CssClass="SmallFont TextBox UpperCase"  runat="server" AutoPostBack="True" 
                             onselectedindexchanged="DDLTrnType_SelectedIndexChanged1" Width="150px" >
                                    <asp:ListItem Value="I"  Selected="True"> Issue </asp:ListItem>
                                    <asp:ListItem Value="R"> Receive </asp:ListItem>                                   
                            </asp:DropDownList>
        </td> 
        <td  >         
         Poy:</td>
           <td >
        <asp:DropDownList ID="ddlitem" runat="server" DataTextField="FIBER_CODE" 
            DataValueField="FIBER_CAT" 
            Width="150px"
             AutoPostBack="True" CssClass="tContentArial" 
                   onselectedindexchanged="ddlitem_SelectedIndexChanged1" >      
        </asp:DropDownList>
           </td>
           <td  align = "right">Transaction No.From:</td>
 <td colspan="3">
        <asp:TextBox ID="Txtfromno" runat="server" 
            ontextchanged="Txtfromno_TextChanged" AutoPostBack="True" Width="150px"></asp:TextBox>
        To<asp:TextBox ID="txttono" runat="server" ontextchanged="txttono_TextChanged" 
            AutoPostBack="True" Width="150px"></asp:TextBox>
            </td>
  </tr>
  
   <tr>
    <td align = "right" style="text-align: right">
         Year:
    </td>
    <td>
         <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                            Width="150px"
             onselectedindexchanged="ddlYear_SelectedIndexChanged">
                        </asp:DropDownList>
    </td>
    <td>
         From Date:    
    </td>
    <td>
        <asp:TextBox ID="txtDate1" runat="server" 
                            Width="150px" ></asp:TextBox>
    </td>
    <td align = "right">
         To Date:    
    </td>
    <td>
          <asp:TextBox ID="txtDate2" runat="server" 
                           Width="150px"
             ></asp:TextBox>    
    </td>  
    <td colspan=2>
        <asp:Button ID="btnGetReport" runat="server" Text="Get Report" 
            onclick="btnGetReport_Click" CssClass="AButton"  />
    </td>  
 
 </tr>   

  <tr>
             <td colspan="8" width="100%" class="TdBackVir">
                            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td>
              
              </tr>
  
  <tr>
  <td colspan="8">
  <table>
   <td   class = "td tContentArial">
   
  <asp:Panel ID="pnlShowHover" runat="server" width = "95%" 
                                Height="350px" ScrollBars="Auto">
      <asp:GridView ID="GridView1" runat="server" Font-Size="X-Small"  
          AutoGenerateColumns="False" CellPadding="3" 
          AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
          PageSize="15"  Width = "120%" BackColor="White" BorderColor="#999999" 
          BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />


          <Columns>
              <asp:BoundField DataField="YEAR" HeaderText="YEAR" 
                  HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left">
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:BoundField DataField="COMP_CODE" HeaderText="COMP CODE" Visible = "false"/>
              <asp:BoundField DataField="BRANCH_CODE" HeaderText="BRANCH CODE"  Visible = "false"/>
              <asp:BoundField DataField="PO_COMP" HeaderText="PO COMP " Visible = "false"/>
              <asp:BoundField DataField="PO_BRANCH" HeaderText="PO BRANCH"  Visible = "false" />                
              <asp:BoundField DataField="PO_TYPE" HeaderText="PO TYPE" 
                  HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left" >
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
             
                <asp:TemplateField HeaderText="TRN TYPE">
              <ItemTemplate>
              <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Eval("TRN_DESC") %>' ToolTip='<%# Eval("TRN_TYPE") %>'></asp:Label>
              </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="TRN_NUMB" HeaderText="REC TRN NUMB" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >                 
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="PO_NUMB" HeaderText="REC PO NUMB" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >                 
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:TemplateField  HeaderText="POY CODE">
              <ItemTemplate >
              <asp:Label ID="lblFiberCode" runat="server" Text='<%# Eval("FIBER_DESC") %>' ToolTip='<%# Eval("FIBER_CODE") %>'></asp:Label>
              </ItemTemplate>
              </asp:TemplateField>            
              <asp:BoundField DataField="ISSUE_QTY" HeaderText="ISSUE QTY" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right">                  
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="FINAL_RATE" HeaderText="FINAL RATE" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >               
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="ISS_YEAR" HeaderText="ISS YEAR" Visible = "false" />
              <asp:BoundField DataField="ISS_COMP" HeaderText="ISS COMP" Visible = "false" />
              <asp:BoundField DataField="ISS_BRANCH" HeaderText="ISS BRANCH"  Visible = "false" />                 
           
                <asp:TemplateField HeaderText="ISS TRN TYPE">
              <ItemTemplate>
              <asp:Label ID="lblISSTRNTYPE" runat="server" Text='<%# Eval("ISS_TRN_DESC") %>' ToolTip='<%# Eval("ISS_TRN_TYPE") %>'></asp:Label>
              </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="ISS_TRN_NUMB" HeaderText="ISS TRN NUMB" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >                 
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="ISS_PO_COMP" HeaderText="ISS PO COMP" Visible = "false"/>
              <asp:BoundField DataField="ISS_PO_BRNCH" HeaderText="ISS PO BRNCH" Visible = "false" />
              <asp:BoundField DataField="ISS_PO_TYPE" HeaderText="ISS PO TYPE" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="ISS_PO_NUMB" HeaderText="ISS PO NUMB" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >     
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>             
          </Columns>
          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
      </asp:GridView>         
      <asp:GridView ID="grdIssue" runat="server" Font-Size="X-Small"  
          AutoGenerateColumns="False" CellPadding="3" 
          AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
          PageSize="15"  Width = "120%" BackColor="White" BorderColor="#999999" 
          BorderStyle="None" BorderWidth="1px">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />


          <Columns>
         
               <asp:BoundField DataField="ISS_YEAR" HeaderText="YEAR"  />
              <asp:BoundField DataField="ISS_TRN_NUMB" HeaderText="TRN&nbsp;NUMB" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >                 
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>  
               <asp:TemplateField HeaderText="TRN&nbsp;TYPE">
              <ItemTemplate>
              <asp:Label ID="lblISSTRNTYPE" runat="server" Text='<%# Eval("ISS_TRN_DESC") %>' ToolTip='<%# Eval("ISS_TRN_TYPE") %>'></asp:Label>
              </ItemTemplate>
              </asp:TemplateField>      
              <asp:BoundField DataField="TRN_DATE" HeaderText="DATE"  />      
             
              <asp:BoundField DataField="ISS_COMP" HeaderText="COMP" Visible = "false" />
              <asp:BoundField DataField="ISS_BRANCH" HeaderText="BRANCH"  Visible = "false" />                 
              <asp:BoundField DataField="ISS_PO_COMP" HeaderText="ISS PO COMP" Visible = "false"/>
              <asp:BoundField DataField="ISS_PO_BRNCH" HeaderText="ISS PO BRNCH" Visible = "false" />
              <asp:BoundField DataField="ISS_PO_TYPE" HeaderText="ISS PO TYPE"  Visible="false"
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right"  />
              </asp:BoundField>
              <asp:BoundField DataField="ISS_PO_NUMB" HeaderText="ISS PO NUMB"  Visible="false"
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >     
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>                
               <asp:BoundField DataField="LOT_NUMBER" HeaderText="LOT&nbsp;NO"  />                  
              <asp:TemplateField  HeaderText="POY&nbsp;CODE">
              <ItemTemplate >
              <asp:Label ID="lblFiberCode" runat="server" Text='<%# Eval("FIBER_DESC") %>' ToolTip='<%# Eval("FIBER_CODE") %>'></asp:Label>
              </ItemTemplate>
              </asp:TemplateField>            
              <asp:BoundField DataField="TRN_QTY" HeaderText="TOTAL&nbsp;ISSUE&nbsp;QTY" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right">                  
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
                  <asp:BoundField DataField="TOTAL_COPS" HeaderText="TOTAL&nbsp;COPS" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right">                  
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              
           
               <asp:BoundField DataField="MERGE_NO" HeaderText="MERGE" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right">                  
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              
               <asp:BoundField DataField="PALLET_CODE" HeaderText="PALLET&nbsp;CODE" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right">                  
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
             
              <asp:BoundField DataField="PALLET_NO" HeaderText="PALLET&nbsp;NO" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right">                  
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
             
                <asp:BoundField DataField="ISSUE_QTY" HeaderText="ISSUE&nbsp;QTY" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right">                  
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              
              <asp:BoundField DataField="NO_OF_UNIT" HeaderText="COPS" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right">                  
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              
                 <asp:BoundField DataField="MAC_CODE" HeaderText="MACHINE NO" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right">                  
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="COMP_CODE" HeaderText="REC COMP CODE" Visible = "false"/>
              <asp:BoundField DataField="BRANCH_CODE" HeaderText="REC BRANCH CODE"  Visible = "false"/>
              <asp:BoundField DataField="PO_COMP" HeaderText="REC PO COMP " Visible = "false"/>
              <asp:BoundField DataField="PO_BRANCH" HeaderText="REC PO BRANCH"  Visible = "false" />                
              <asp:BoundField DataField="PO_TYPE" HeaderText="REC&nbsp;PO&nbsp;TYPE" 
                  HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left" >
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
             <asp:BoundField DataField="YEAR" HeaderText="REC&nbsp;YEAR" 
                  HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left">
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
                <asp:TemplateField HeaderText="REC&nbsp;TRN&nbsp;TYPE">
              <ItemTemplate>
              <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Eval("TRN_DESC") %>' ToolTip='<%# Eval("TRN_TYPE") %>'></asp:Label>
              </ItemTemplate>
              </asp:TemplateField>
              <asp:BoundField DataField="TRN_NUMB" HeaderText="REC&nbsp;TRN&nbsp;NUMB" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >                 
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="PO_NUMB" HeaderText="PO&nbsp;NUMB" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >                 
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              
               <asp:BoundField DataField="FINAL_RATE" HeaderText="FINAL&nbsp;RATE" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >               
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
          </Columns>
          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
      </asp:GridView>                              
   </asp:Panel>
   <cc1:CalendarExtender ID="TxtIndentDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate1" >
                        </cc1:CalendarExtender>
                        
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtDate1">
                        </cc1:MaskedEditExtender>
                        <cc1:CalendarExtender ID="TxtIndentDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate2">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtDate2">
                        </cc1:MaskedEditExtender>
        </td>
 </table>
 </tr> 
</td>
 </table>