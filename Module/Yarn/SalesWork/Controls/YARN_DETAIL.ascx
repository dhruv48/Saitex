<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YARN_DETAIL.ascx.cs" Inherits="Module_Yarn_SalesWork_Controls_YARN_DETAIL" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
  <style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 200px;
    }
    .c2
    {
        margin-left: 4px;
        width: 300px;
    }
    .c3
    {
        width: 200px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 120px;
    }
    .d3
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 120px;
    }
</style>
  
    
    
<table align="left" class=" td tContentArial" width="100%">
    <tr>
        <td class="td">
           
            <table >
                <tr>
              
              
              
                    <td>
                        <asp:ImageButton ID="imgbtnExport" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnExport_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                    
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
         
           <table width="100%">
                <tr>
                    <td align="center" class="TableHeader td" colspan="8">
                        <span class="titleheading"><strong>Quality Detail Stock Report</strong> </span>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Branch:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                            <td align="right">
                                Year:
                            </td>
                            <td>
                                <%--<asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px">--%>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="gCtrTxt " Font-Size="9" 
                                    Width="160px" AutoPostBack="True" 
                                    onselectedindexchanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                From Date:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    OnTextChanged="TxtFromDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td align="right">
                                To Date:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    OnTextChanged="TxtToDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
                            </td>
                 
                  
                    
                   
                </tr>
                <tr>
                  <td align="right">
                        Quality Type :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlyarntype" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Party :
                    </td>
                    <td >
                        <asp:DropDownList ID="ddlpartycode" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        Shade Code :
                    </td>
                    <td align="left" valign="top" class="style8">
                        <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="SHADE_NAME" EnableLoadOnDemand="True"
                                                MenuWidth="300" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="16"
                                                Height="200px" Visible="true" Width="150px" OnLoadingItems="cmbShade_LoadingItems"
                                                >
                                                <HeaderTemplate>                                                  
                                                    <div class="header d2">
                                                        Shade Family Name</div>                                                  
                                                    <div class="header d4">
                                                        Shade Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                   
                                                    <div class="item d2">
                                                        <%# Eval("SHADE_FAMILY_NAME")%></div>                                                    
                                                    <div class="item d4">
                                                        <%# Eval("SHADE_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
 </cc2:ComboBox>
                    </td>
                <td style="text-align: right"  >
             Quality Code :</td>
           <td >
               <asp:TextBox ID="TxtYarnCode" runat="server" Width="158px"></asp:TextBox>
           </td>
       
                </tr>
                <tr>
                    <td align="right">
                        Quality Catagory :
                     </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlyarncat" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
              <td align="right">
                        Location :
                    </td>
                    <td class="tdLeft">
                         <asp:DropDownList ID="ddlLocation" runat="server" CssClass="gCtrTxt " Font-Size="9"  Width="150px">  </asp:DropDownList></td>
                    <td align="right">
                        Store :
                    </td>
                    <td align="left" valign="top" class="style8">
                       <asp:DropDownList ID="ddlStore" runat="server" CssClass="gCtrTxt " Font-Size="9"
                Width="150px">
            </asp:DropDownList></td>
                <td style="text-align: right"  >
            </td>
           <td >
              <asp:Button ID="btngetdata" runat="server" Text="Get Data" OnClick="btngetdata_Click" CssClass="AButton" />
           </td>
             
                </tr>
                
                 <tr >
                            <td align="left">
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                                    <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                            </td>
                            </tr>
                </table>
                  
                 
                        <tr>
                            <td>
                                <asp:GridView ID="GrdYarnDetail" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                                    EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                                    PageSize="10" Width="100%" OnPageIndexChanging="GrdYarnDetail_PageIndexChanging">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                    <Columns>
                                        <asp:BoundField DataField="YEAR" HeaderText="Year" />
                                        <asp:BoundField DataField="TRN_TYPE" HeaderText="Trn Type" />
                                        <asp:BoundField DataField="TRN_NUMB" HeaderText="Trn No" />
                                        <asp:BoundField DataField="TRN_DATE" HeaderText="Trn Date" />
                                        <asp:BoundField DataField="YARN_CODE" HeaderText="Quality Code" />
                                        <asp:BoundField DataField="YARN_DESC" HeaderText="Quality Desc" />
                                        <asp:BoundField DataField="YARN_CAT" HeaderText="Quality Catagory" />
                                        <asp:BoundField DataField="YARN_TYPE" HeaderText="Quality Type" />
                                         <asp:BoundField DataField="SHADE_FAMILY" HeaderText="Shade Family" />
                                        <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade Code" />
                                        <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                                        <asp:BoundField DataField="STORE" HeaderText="STORE" />
                                        <asp:BoundField DataField="LOT_NO" HeaderText="Lot Number" />
                                        <asp:BoundField DataField="GRADE" HeaderText="Grade" />
                                        <asp:BoundField DataField="BALQTY" HeaderText="Balance Qty" />
                                        <asp:BoundField DataField="FINAL_RATE" HeaderText="Final Rate" />
                                        <asp:BoundField DataField="BALVAL" HeaderText="Balance Value" />
                                        <asp:BoundField DataField="PRTY_NAME" HeaderText="Party Name" DataFormatString="{0:dd-MM-yyyy}" />
                                        <asp:BoundField DataField="PO_NUMB" HeaderText="PO No" />
                                        <asp:BoundField DataField="PO_TYPE" HeaderText="PO Type" />
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />
                                </asp:GridView>
                            </td>
                        </tr>
                   
                 <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="TxtFromDate">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="TxtToDate">
                    </cc1:CalendarExtender>
         </td>
   
    </tr>
  
</table>
