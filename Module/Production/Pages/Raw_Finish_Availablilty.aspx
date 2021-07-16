<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Raw_Finish_Availablilty.aspx.cs" Inherits="Module_Production_Pages_Raw_Finish_Availablilty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Raw Material Requirement</title>
     <link rel="stylesheet" type="text/css" href="~/StyleSheet/CommonStyle.css" />
         <script language="javascript" type="text/javascript">

             function gridWindowClose() {                 
                 window.opener.document.forms[0].submit();
                 window.close();
             }           
     
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div >


 <table width="100%"  class="tContentArial">
   <tr>
        <td align="left" valign="top" class="td" width="100%" colspan="2">
      

            <table align="left">
                <tr>
                   
                    
                   <td >
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td >
                        <asp:ImageButton ID="imgbtnExport" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnExport_Click"></asp:ImageButton>&nbsp;
                    </td>
                  
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                   
                </tr>
            </table>
            
        </td>
       
    </tr>
    <tr>
        
        <td align="center" class="TableHeader td" width="50%" >
            <b class="titleheading"> Finish Stock Details</b>
        </td>
        <td align="center" class="TableHeader td" width="50%" >
            <b class="titleheading"> Raw Stock Details</b>
        </td>
    </tr>
    
    <tr>
        <td align="center" class="td" width="50%" valign="top" >
          <asp:Panel ID="pnl1" runat="server"  ScrollBars="Vertical" Height="550px">
                  <asp:GridView ID="grdFinishDetails" runat="server"             
                      AutoGenerateColumns="false"         width="90%" ShowFooter="true" 
                      onrowdatabound="grdFinishDetails_RowDataBound"        >
             
                <Columns>                        
                  
                    
                    <asp:TemplateField HeaderText="Artical&nbsp;Details" >               
                        <ItemTemplate>
                            <asp:Label ID="lblArticalCode" runat="server" CssClass="SmallFont"  Text='<%# Bind("ARTICAL_DESC") %>'   ToolTip='<%# Bind("ARTICAL_CODE") %>' 
                                ></asp:Label>
                        </ItemTemplate> 
                         <FooterTemplate>
                        <asp:Label ID="lblTOTALArticalCode" runat="server" CssClass="SmallFont"     Font-Bold="true"  Text="Total:"
                                ></asp:Label>
                        </FooterTemplate>                       
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade" >               
                        <ItemTemplate>
                            <asp:Label ID="lblShadeCode" runat="server" CssClass="SmallFont"  Text='<%# Bind("SHADE_CODE") %>' 
                                ></asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total&nbsp;Required&nbsp;Qty" >               
                        <ItemTemplate>
                            <asp:Label ID="lblREQ_QTY" runat="server" CssClass="SmallFont"  Text='<%# Bind("TOTAL_REQ_QTY") %>'   Font-Bold="true" 
                                ></asp:Label>
                        </ItemTemplate>    
                        <FooterTemplate>
                        <asp:Label ID="lblTOTAL_REQ_QTY" runat="server" CssClass="SmallFont"     Font-Bold="true" 
                                ></asp:Label>
                        </FooterTemplate>                   
                    </asp:TemplateField>
                       
                    <asp:TemplateField HeaderText="Total&nbsp;Balance&nbsp;Qty" >               
                        <ItemTemplate>
                            <asp:Label ID="lblBAL_QTY" runat="server" CssClass="SmallFont"  Text='<%# Bind("TOTAL_BAL_QTY") %>'  ForeColor="Blue"  Font-Bold="true" 
                                ></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>      
                         <asp:Label ID="lblTOTAL_BAL_QTY" runat="server" CssClass="SmallFont"    Font-Bold="true" 
                                ></asp:Label>  
                                 </FooterTemplate>                      
                    </asp:TemplateField>
                       
                    <asp:TemplateField HeaderText="Total&nbsp;Stock&nbsp;Qty" >               
                        <ItemTemplate>
                            <asp:Label ID="lblSTOCK_QTY" runat="server" CssClass="SmallFont"  Text='<%# Bind("TOTAL_STOCK_QTY") %>'  
                                ForeColor="Green"  Font-Bold="true"></asp:Label>
                        </ItemTemplate>      
                         <FooterTemplate>
                        <asp:Label ID="lblTOTAL_STOCK_QTY" runat="server" CssClass="SmallFont"     Font-Bold="true" 
                                ></asp:Label>
                        </FooterTemplate>                  
                    </asp:TemplateField>
                  
                   </Columns>
                  
             </asp:GridView>
              </asp:Panel>
        </td>
        <td  align="center" class="td" width="50%" valign="top">
         <asp:Panel ID="Panel2" runat="server"  ScrollBars="Vertical" Height="550px">
           <asp:GridView ID="grdRawDetails" runat="server"             
                AutoGenerateColumns="false"   ShowFooter="true"       width="90%" 
                onrowdatabound="grdRawDetails_RowDataBound"          >
             
                <Columns>                        
                  
                    
                    <asp:TemplateField HeaderText="Artical&nbsp;Details" >               
                        <ItemTemplate>
                            <asp:Label ID="lblArticalCode" runat="server" CssClass="SmallFont"  Text='<%# Bind("ARTICAL_DESC") %>'   ToolTip='<%# Bind("ARTICAL_CODE") %>' 
                                ></asp:Label>
                        </ItemTemplate>  
                        <FooterTemplate>
                        <asp:Label ID="lblTOTALArticalCode" runat="server" CssClass="SmallFont"     Font-Bold="true"  Text="Total:"
                                ></asp:Label>
                        </FooterTemplate>                         
                    </asp:TemplateField>
                       
                    <asp:TemplateField HeaderText="Total&nbsp;Required&nbsp;Qty" >               
                        <ItemTemplate>
                            <asp:Label ID="lblREQ_QTY" runat="server" CssClass="SmallFont"  Text='<%# Bind("TOTAL_REQ_QTY") %>'     Font-Bold="true" 
                                ></asp:Label>
                        </ItemTemplate>     
                         <FooterTemplate>
                        <asp:Label ID="lblTOTAL_REQ_QTY" runat="server" CssClass="SmallFont"     Font-Bold="true" 
                                ></asp:Label>
                        </FooterTemplate>                       
                    </asp:TemplateField>
                       
                    <asp:TemplateField HeaderText="Total&nbsp;Balance&nbsp;Qty" >               
                        <ItemTemplate>
                            <asp:Label ID="lblBAL_QTY" runat="server" CssClass="SmallFont"  Text='<%# Bind("TOTAL_BAL_QTY") %>'  ForeColor="Blue"  Font-Bold="true"  
                                ></asp:Label>
                        </ItemTemplate>   
                         <FooterTemplate>      
                         <asp:Label ID="lblTOTAL_BAL_QTY" runat="server" CssClass="SmallFont"     Font-Bold="true" 
                                ></asp:Label>  
                                 </FooterTemplate>                       
                    </asp:TemplateField>
                       
                    <asp:TemplateField HeaderText="Total&nbsp;Stock&nbsp;Qty" >               
                        <ItemTemplate>
                            <asp:Label ID="lblSTOCK_QTY" runat="server" CssClass="SmallFont"  Text='<%# Bind("TOTAL_STOCK_QTY") %>'   ForeColor="Green"  Font-Bold="true" 
                                ></asp:Label>
                        </ItemTemplate> 
                         <FooterTemplate>
                        <asp:Label ID="lblTOTAL_STOCK_QTY" runat="server" CssClass="SmallFont"     Font-Bold="true" 
                                ></asp:Label>
                        </FooterTemplate>                           
                    </asp:TemplateField>
                  
                   </Columns>
                  
             </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>

    </div>
    </form>
</body>
</html>
