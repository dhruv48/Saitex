<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FabricStockDetail.ascx.cs" Inherits="Module_Fabric_FabricSaleWork_Controls_FabricStockDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table align="left" class=" td tContentArial" width="100%">
    <tr>
        <td class="td" colspan="8">
           
            <table >
                <tr>
              
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
                        <span class="titleheading"><strong>Fabric Stock Detail</strong> </span>
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
                  <td class="tdRight">
                        Fabric Type :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlfabrictype" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Party :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlpartycode" runat="server" CssClass="gCtrTxt"
                            Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Shade Code :
                    </td>
                    <td align="left" valign="top" class="style8">
                        <asp:DropDownList ID="ddlShadeCode" runat="server" CssClass="gCtrTxt" Width="160px">
                        </asp:DropDownList>
                    </td>
                <td style="text-align: right"  >
 Article Code :</td>
           <td >
               <asp:TextBox ID="TxtYarnCode" runat="server" Width="158px"></asp:TextBox>
           </td>
       
                </tr>
                <tr>
                    
             
               <td align="tdLeft">
                                <asp:Button ID="btngetdata" runat="server" Text="Get Data" OnClick="btngetdata_Click" />
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
                                <asp:GridView ID="GrdFabricDetail" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                                    EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left"
                                    PageSize="10" Width="100%" OnPageIndexChanging="GrdFabricDetail_PageIndexChanging">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                    <Columns>
                                        <asp:BoundField DataField="YEAR" HeaderText="Year" />
                                        <asp:BoundField DataField="TRN_TYPE" HeaderText="Trn Type" />
                                        <asp:BoundField DataField="TRN_NUMB" HeaderText="Trn No" />
                                        <asp:BoundField DataField="TRN_DATE" HeaderText="Trn Date" />
                                        <asp:BoundField DataField="FABR_CODE" HeaderText="Fabric Code" />
                                        <asp:BoundField DataField="FABR_TYPE" HeaderText="Fabric Type" />
                                        <asp:BoundField DataField="FABR_DESC" HeaderText="Fabric Desc" />
                                        <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade Code" />
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

