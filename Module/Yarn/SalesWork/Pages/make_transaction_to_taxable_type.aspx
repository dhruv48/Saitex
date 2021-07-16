<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master" CodeFile="make_transaction_to_taxable_type.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_make_transaction_to_taxable_type" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">
    <%--<asp:UpdatePanel ID="UpdatePanel4531" runat="server">--%>
    <ContentTemplate>

<table width="100%"  class="tContentArial" align="left">
     
     <tr  width="100%">
     <td class="td" colspan="9">
     <table>
      <tr>
                        <td id="tdUpdate" runat="server" align="left">
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                Width="48" Height="41" ValidationGroup="M1" onclick="imgbtnUpdate_Click" ></asp:ImageButton>
                        </td>
                        
                        <td id="tdFind" runat="server" visible="false" align="left">
                            <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                ImageUrl="~/CommonImages/link_print.png" onclick="imgbtnPrint_Click1" ></asp:ImageButton>&nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                ImageUrl="~/CommonImages/clear.jpg" onclick="imgbtnClear_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                ImageUrl="~/CommonImages/link_exit.png" onclick="imgbtnExit_Click" ></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                ImageUrl="~/CommonImages/link_help.png" ></asp:ImageButton>
                        <asp:ImageButton ID="ImageButton1" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                        </td>
                    </tr>
     </table>
     
     </td>
     </tr>
    <tr>
            <td align="center" class="TableHeader td" width="100%" colspan="9">
                <b class="titleheading">All Confirm Yarn Receiving List</b>
            </td>
        </tr>
        
        <tr>
            <td align="center" width="100%" class="style1" colspan="9">
                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td  >
            
                Taxation Type:
                 </td  >
                  <td  >
                <asp:DropDownList ID="ddlExcisableType1" runat="server" 
                    CssClass="SmallFont" Width="120px">
                    <asp:ListItem Selected="True" Value="">--select--</asp:ListItem>
                    <asp:ListItem Value="NA">NA</asp:ListItem>
                    <asp:ListItem Value="Excisable">Excisable</asp:ListItem>
                    <asp:ListItem Value="Exempted">Exempted</asp:ListItem>
                </asp:DropDownList>
                </td>
                <td>
                Branch Name:  </td  >
                  <td  > <asp:TextBox ID="txtbranch" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
               Party Name:  </td  >
                  <td  > <asp:TextBox ID="txtpartyname" runat="server" Width="120px"></asp:TextBox>
                     </td>                       
                <td>
                  GRN No: </td  >
                  <td  > <asp:TextBox ID="txtyGRNNo" runat="server" Width="120px"></asp:TextBox>
                                            
                </td>
                </tr>
                <tr>
                <td>
                 Yarn Desc:  </td  >
                  <td  ><asp:TextBox ID="txtyarndesc" runat="server" Width="120px"></asp:TextBox>
                </td>
                <td>
                Date From: </td  >
                  <td  > <asp:TextBox ID="txtdatefrom" runat="server" 
                    Width="120px"></asp:TextBox>
                 <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdatefrom"></cc1:CalendarExtender>
                 </td>
                 <td>
                Date To: </td  >
                  <td  > <asp:TextBox ID="txtdateto" runat="server" Width="120px"></asp:TextBox>
                  <cc1:CalendarExtender ID="dateTo" runat="server" Format="dd/MM/yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>         
                      </td>
                      <td>
                      </td>
                      <td>
            <asp:Button ID="btngetdata" runat="server" Text="GetData" 
                    onclick="btngetdata_Click" />     
   
</td>
        </tr>
        <tr>
            <td align="left" class="td" width="100%" colspan="9">
                <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="9">
               <asp:Panel ID="pnlShowHover" runat="server" Width="99%" ScrollBars="Auto">
                <asp:GridView ID="gvPartyList" runat="server" AllowSorting="True" 
                       AutoGenerateColumns="False" HeaderStyle-Font-Bold="true"  Width="99%"
                                                    AllowPaging="true" PageSize="3" 
                       CellPadding="3" ForeColor="#333333" GridLines="Both"
                                                    BorderStyle="Ridge" Font-Names="Arial" 
                       Font-Size="X-Small" onpageindexchanging="gvPartyList_PageIndexChanging">
                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>
                      
                                             
                         <asp:TemplateField HeaderText="Year">
                            <ItemTemplate>
                         <asp:Label ID="lblYear" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>                                                             
                        </ItemTemplate>                           
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Company">
                            <ItemTemplate>
                                <asp:Label ID="lblCompany" runat="server" ToolTip='<%# Bind("COMP_CODE") %>' Text='<%# Bind("COMP_NAME") %>'></asp:Label>                                                               
                        </ItemTemplate>                           
                        </asp:TemplateField> 
                         <asp:TemplateField HeaderText="Branch">
                            <ItemTemplate>
                                <asp:Label ID="lblBranch" runat="server" ToolTip='<%# Bind("BRANCH_CODE") %>' Text='<%# Bind("BRANCH_NAME") %>'></asp:Label>                                                               
                        </ItemTemplate>                           
                        </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Party">
                            <ItemTemplate>
                                <asp:Label ID="lblParty" runat="server" ToolTip='<%# Bind("PRTY_CODE") %>' Text='<%# Bind("PRTY_NAME") %>'></asp:Label>                                                               
                        </ItemTemplate>                           
                        </asp:TemplateField> 
                          <asp:TemplateField HeaderText="MRN(GRN) No">
                            <ItemTemplate>
                                <asp:Label ID="lblMrn" runat="server" ToolTip='<%# Bind("TRN_TYPE") %>' Text='<%# Bind("TRN_NUMB") %>'></asp:Label>                                                               
                        </ItemTemplate>                           
                        </asp:TemplateField> 
                          <asp:TemplateField HeaderText="Yarn">
                            <ItemTemplate>
                                <asp:Label ID="lblYarn" runat="server" ToolTip='<%# Bind("YARN_CODE") %>' Text='<%# Bind("YARN_DESC") %>'></asp:Label>                                                               
                        </ItemTemplate>                           
                        </asp:TemplateField> 
                          <asp:BoundField DataField="TRN_QTY" HeaderText="Qty" />
                                      
                      
                        <asp:TemplateField HeaderText="Is Excisable/Exempted">
                        
                            <ItemTemplate>
                                <asp:DropDownList  ID="ddlExcisableType" runat="server" Width="100px" CssClass="SmallFont" >
                                <asp:ListItem Selected="True" Value="">NA</asp:ListItem>
                                <asp:ListItem Value="Excisable">Excisable</asp:ListItem>
                                <asp:ListItem Value="Exempted">Exempted</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkApproved" runat="server" />
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
     </tr>
     
     
                   
                </table>
        </ContentTemplate>
            
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="Head1">

    <style type="text/css">
        .style1
        {
            border: .05em ridge #C1D3FB;
            height: 21px;
        }
        .style2
        {
            width: 19%;
        }
    </style>

</asp:Content>

