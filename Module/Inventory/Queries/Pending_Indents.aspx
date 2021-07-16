<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="Pending_Indents.aspx.cs" Inherits="Module_Inventory_Queries_Pending_Indents" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
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
        width: 150px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>--%>
        
<table  cellpadding="3" cellspacing="0" width="100%" class=" td tContentArial ">
<tr>
                            <td align="left" colspan="8" width="100%">
                                <table class="tContentArial" cellspacing="0" width="10%" cellpadding="0" border="0"
                                    align="left">
                                    <tbody>
                                        <tr>
                                                                                    
                                            <td width="41">
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                                    Width="41" Height="41" onclick="imgbtnPrint_Click"  ></asp:ImageButton>
                                            </td>
                                              <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" ></asp:ImageButton>&nbsp;</td> 
                                            <td width="41">
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                                    Width="41" Height="41" onclick="imgbtnExit_Click" ></asp:ImageButton>
                                            </td>
                                            
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
            <td width="100%" align="center" class="TableHeader" colspan="8"><b class="titleheading">Pending Indent Details </b> </td>
         </tr>
<tr >
<td align="right"  width="10%" >
                From Date :
               </td>
        <td  align ="left" width: 10%" ID="txtDate1" >
            <asp:TextBox ID="txtDate1" runat="server" CssClass ="TextBox"></asp:TextBox>
              <cc1:CalendarExtender ID="TxttxtDate1" runat="server" Format="dd/MM/yyyy"  
              TargetControlID="txtDate1">
             </cc1:CalendarExtender>
             
             <cc1:MaskedEditExtender ID="MaskedEdittxtDate1" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtDate1">
                        </cc1:MaskedEditExtender>
        </td>
    <td align="right" width: 10%">
                To Date :
               </td>
        <td align ="left" width: 10%">
            <asp:TextBox ID="txtDate2" runat="server" CssClass ="TextBox"></asp:TextBox>
              <cc1:CalendarExtender ID="TxtIndentDate1" runat="server" Format="dd/MM/yyyy" 
              TargetControlID="txtDate2">
             </cc1:CalendarExtender>
             
               <cc1:MaskedEditExtender ID="MaskedEdittxtDate2" runat="server" Mask="99/99/9999"
                            MaskType="Date" PromptCharacter="_" TargetControlID="txtDate2">
                        </cc1:MaskedEditExtender>
        </td>
    <td align="right" width="10%">
    Branch:
    </td>
    <td align ="left" width="15%">
    <asp:DropDownList ID="ddlBranch" runat="server" Width="160px" CssClass="gCtrTxt"  AutoPostBack="true"
            onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                            </asp:DropDownList>
    </td>
    <td  align="right" width="15%" >
                Department :
            </td>
            <td align ="left" width: 15%">
                <asp:DropDownList ID="ddlDepartment" runat="server" Width="160px" CssClass="gCtrTxt"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
   
                             
</tr>
<tr>
<td  align="right" width="15%">
             Year :
        </td>
        <td align ="left" width="15%">
            <asp:DropDownList ID="ddlYear" runat="server" Width="160px" CssClass="gCtrTxt" AutoPostBack="true"
                OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
<td  align="right" width="10%" >
              Item :
            </td>         
    <td align ="left" width: 15%">
                                    <cc2:ComboBox ID="txtICODE" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" MenuWidth="650px" EnableVirtualScrolling="true"
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtICODE_LoadingItems"
                        EmptyText="------------All----------" AutoPostBack="True" 
                                        onselectedindexchanged="txtICODE_SelectedIndexChanged">
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                        <ItemTemplate>
                            <div class="item d1">
                                <%# Eval("ITEM_CODE")%></div>
                            <div class="item d2">
                                <%# Eval("ITEM_DESC") %></div>
                            <div class="item d3">
                                <%# Eval("ITEM_TYPE")%></div>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <div class="header d1">
                                ITEM CODE</div>
                            <div class="header d2">
                                ITEM DESCRIPTION</div>
                            <div class="header d3">
                                TYPE</div>
                        </HeaderTemplate>
                    </cc2:ComboBox>

                                </td>
   <td align="right" width="10%" >
        Status :
    </td>
    <td  align ="left" width="15%" >
    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" Width="160px" CssClass="gCtrTxt" 
            ValidationGroup="M1" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="1" Text="All"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Pending"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Not Approved"></asp:ListItem>
                            </asp:DropDownList>
    </td>
    <td  align="right" width="15%" >
                Location :
            </td>
            <td align ="left" width: 15%">
                <asp:DropDownList ID="ddllocation" runat="server" Width="160px" CssClass="gCtrTxt"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
    <td></td>
    
    
</tr>
<tr>
<td  align="right" width="15%" >
                Store :
            </td>
            <td align ="left" width: 15%">
                <asp:DropDownList ID="ddlstore" runat="server" Width="160px" CssClass="gCtrTxt"
                    AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align =center>
    <asp:Button ID="btnSubmit"  runat="server" CssClass="AButton"  Text=" Get Record "
            onclick="btnSubmit_Click" />
    </td>
</tr>

             <tr>
             <td colspan="8" width="100%" >
                            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </td></tr>
         
         <tr>
            <td  colspan="8">
            <asp:GridView ID="gvReportDisplayGrid"   runat="server"  
            AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" Font-Size="x-Small" 
            CellPadding="3"   GridLines="Both" Width="100%" ForeColor="#333333" 
            CssClass = "smallfont"   
                    PagerStyle-HorizontalAlign="Left" onpageindexchanging="gvReportDisplayGrid_PageIndexChanging"                   
                    EmptyDataText="No Record Found" 
                    PageSize="15">  
                    <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                    <Columns>                   
                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="2%">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch Name">
                            <ItemTemplate>
                                <asp:Label ID="lblBranchName" Text='<%#Eval("BRANCH_NAME")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDeptName" Text='<%#Eval("DEPT_NAME")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:Label ID="lbllocation" Text='<%#Eval("LOCATION")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Store">
                            <ItemTemplate>
                                <asp:Label ID="lblstore" Text='<%#Eval("STORE")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Indent No.">
                            <ItemTemplate>
                                <asp:Label ID="lblDeptName" Text='<%#Eval("IND_NUMB") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Indent Date">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("INDENT_DATE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="2%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Description">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("ITEM_DESC") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approval Date">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblApprovaldate" Text='<%#Eval("APPROVAL_DATE") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="2%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved By">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblApproveby" Text='<%#Eval("APPROVED_BY") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Indent Qty.">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("QTY_INDENTED") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="2%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved Qty.">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("QTY_APPROVED") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="2%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adjusted Qty.">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("QTY_ADJUSTED") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="2%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending Qty.">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("QTY_PENDING") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="2%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending Since (Days)">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="lblDOJ" Text='<%#Eval("PENDING_DAYS") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="2%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                    </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle 
            HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" 
            ForeColor="White" Font-Bold="True" /> 
                </asp:GridView>
             </td>
        </tr>
    </table>    
<%-- </ContentTemplate>
</asp:UpdatePanel>
--%>
</asp:Content>

