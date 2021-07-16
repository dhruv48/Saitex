<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SewingIssueQuery.ascx.cs" Inherits="Module_SewingThread_Controls_SewingIssueQuery" %>
<table class="td tContentArial" width = "950px">
<tr>
        <td align="left"  class="td" colspan = "8">
            <table align="left">
                <tr>               
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" onclick="imgbtnPrint_Click" />
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
            <span class="titleheading">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sewing Thread&nbsp; Issue  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    
 <tr>
 
           <td align = "right" style="text-align: right">Transaction Type :</td><td>
    
                         <asp:DropDownList ID="DDLTrnType" Width="131px" 
                             CssClass="SmallFont TextBox UpperCase"  runat="server" AutoPostBack="True" 
                             onselectedindexchanged="DDLTrnType_SelectedIndexChanged1">
                                    <asp:ListItem Value="I"> Issue </asp:ListItem>
                                    <asp:ListItem Value="R" Selected="True"> Receive </asp:ListItem>                                   
                            </asp:DropDownList>
        </td>
         <td  >
              Sewing Thread:</td>
           <td >
        <asp:DropDownList ID="ddlitem" runat="server" DataTextField="YARN_CAT" 
            DataValueField="YARN_CODE" 
             Width="128px" 
             AutoPostBack="True" CssClass="tContentArial" 
                   onselectedindexchanged="ddlitem_SelectedIndexChanged1" >      
        </asp:DropDownList>
           </td>
           <td  align = "right">Transaction No.From:</td>
 <td colspan="3">
        <asp:TextBox ID="Txtfromno" runat="server" 
            ontextchanged="Txtfromno_TextChanged" AutoPostBack="True"></asp:TextBox>
        To<asp:TextBox ID="txttono" runat="server" ontextchanged="txttono_TextChanged" 
            AutoPostBack="True"></asp:TextBox>
            </td>
  </tr>

  <tr>
             <td colspan="8" width="100%" class="TdBackVir">
                            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td>
  </tr>
  
  <tr>
 
  <table>
  <tr>
   <td colspan="7"  class = "td tContentArial">
   
  <asp:Panel ID="pnlShowHover" runat="server" width = "950px" 
                                Height="350px" ScrollBars="Both">
      <asp:GridView ID="GridView1" runat="server" Font-Size="X-Small"  
          AutoGenerateColumns="False" CellPadding="3" 
          AllowPaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
          PageSize="15"  Width = "120%" BackColor="White" BorderColor="#999999" 
          BorderStyle="None" BorderWidth="1px">
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
              <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" 
                  HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left">
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
              <asp:BoundField DataField="TRN_NUMB" HeaderText="TRN NUMBER" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >                 
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="PO_NUMB" HeaderText="PO NUMB" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right" >                 
              <HeaderStyle HorizontalAlign="Right" />
              <ItemStyle HorizontalAlign="Right" />
              </asp:BoundField>
              <asp:BoundField DataField="YARN_CODE" HeaderText="SEWING THREAD CODE"  
                  HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left" >
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
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
              <asp:BoundField DataField="ISS_TRN_TYPE" HeaderText="ISS TRN TYPE" 
                  HeaderStyle-HorizontalAlign ="Left" ItemStyle-HorizontalAlign ="Left" >                 
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle HorizontalAlign="Left" />
              </asp:BoundField>
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
              <asp:BoundField DataField="LOT_NO" HeaderText="LOT NO" 
                  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign ="Right"   >
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
   
       
        </tr>
 </table>
 </td>
</tr>
 </table>
