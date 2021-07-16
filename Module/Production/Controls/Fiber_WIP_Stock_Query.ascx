<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_WIP_Stock_Query.ascx.cs" Inherits="Module_Production_Controls_Fiber_WIP_Stock_Query" %>

<link href="../../../StyleSheet/style.css" rel="stylesheet" type="text/css" />
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<table class="td tContentArial"  align="center" width="45%">
    <tr>
        <td valign="top" class="td" align="left"  >
            <table align="left">
                <tr>
                    <td id="tdPrint" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Width="48" OnClick="imgbtnPrint_Click" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                   
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center">
            <span class="titleheading">
                WIP Stock Query </span>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
    <td>
    <table>
    <tr>
        <td align="right">
            Order No :
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderNo" runat="server" DataTextField="ORDER_NO" DataValueField="ORDER_NO"
                Width="128px" AutoPostBack="True" CssClass="tContentArial" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            DepartMent :
        </td>
        <td>
            <asp:DropDownList ID="ddlDepartMent" runat="server" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE"
                OnSelectedIndexChanged="ddlDepartMent_SelectedIndexChanged" Width="128px" AutoPostBack="True"
                CssClass="tContentArial">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            Branch. :
        </td>
        <td>
            <asp:DropDownList ID="ddlbranch" runat="server" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged"
                DataTextField="BRANCH_NAME" DataValueField="BRANCH_CODE" Width="128px" AutoPostBack="True"
                CssClass="tContentArial">
            </asp:DropDownList>
        </td>
      
        <td align="right">
            Process No.:
        </td>
        <td>
            <asp:DropDownList ID="ddlprocessno" runat="server" DataTextField="PROS_DESC" DataValueField="PROS_CODE"
                OnSelectedIndexChanged="ddlprocessno_SelectedIndexChanged" Width="128px" AutoPostBack="True"
                CssClass="tContentArial">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td  colspan="4">
            <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <table>
                <tr>
                    <td  class="td tContentArial" >
                        <asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Both">
                            <asp:GridView ID="GridYRN_STOCK" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="180%"
                                 ForeColor="#333333" GridLines="None" HeaderStyle-Wrap="true"
                                Font-Size="10px" OnPageIndexChanging="GridYRN_STOCK_PageIndexChanging" PageSize="15">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="ARTICLE_CODE" HeaderText="ARTICLE CODE" HeaderStyle-HorizontalAlign="Justify" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" />
                                    <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                    <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT"  HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                     <asp:BoundField DataField="ORDER_NO" HeaderText="ORDER NO" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify" />
                                     <asp:BoundField DataField="BATCH_NO" HeaderText="BATCH NO" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify" />
                                    <asp:BoundField DataField="LOT_NUMBER" HeaderText="LOT NUMBER" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify"  HeaderStyle-HorizontalAlign="Justify"/>
                                     <asp:BoundField DataField="BATCHCARD_NO" HeaderText="BATCHCARD NO" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify" />
                                    <asp:BoundField DataField="PROS_CODE" HeaderText="PROS CODE" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify"  HeaderStyle-HorizontalAlign="Justify"/>
                                     <asp:BoundField DataField="PROS_DATE" HeaderText="PROS DATE" DataFormatString="{0:MM-dd-yyyy}" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify"  HeaderStyle-HorizontalAlign="Justify"/>
                                    <asp:BoundField DataField="PROS_DESC" HeaderText="PROS DESC" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                    <asp:BoundField DataField="PROS_ROOT" HeaderText="PROS ROOT"  HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                   
                                    <asp:BoundField DataField="TRN_DATE" HeaderText="TRN DATE" DataFormatString="{0:MM-dd-yyyy}" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                    
                                    <asp:BoundField DataField="BIN_LOCT" HeaderText="BIN LOCT" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify" />
                                    <asp:BoundField DataField="LOT_QTY" HeaderText="LOT QTY" ItemStyle-HorizontalAlign="Justify"
                                        DataFormatString="{0:0.00}" HeaderStyle-Width="17px" ItemStyle-Width="17px"  HeaderStyle-HorizontalAlign="Justify" />
                                    <asp:BoundField DataField="STOCK_QTY" HeaderText="STOCK QTY" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"
                                        DataFormatString="{0:0.00}" />
                                    <asp:BoundField DataField="LENGTH" HeaderText="LENGTH" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify" />
                                    <asp:BoundField DataField="UN_CONF_QTY" HeaderText="UNCONF QTY" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"
                                        DataFormatString="{0:0.00}" />
                                    <asp:BoundField DataField="FR_DEPT_CODE" HeaderText="FR DEPTCODE" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify" />
                                    <asp:BoundField DataField="TO_DEPT_CODE" HeaderText="TO DEPTCODE" HeaderStyle-Width="17px" ItemStyle-Width="17px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                    <asp:BoundField DataField="QTY_PACK" HeaderText="QTYPACK" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify"  HeaderStyle-HorizontalAlign="Justify"/>
                                       <asp:BoundField DataField="DIFF_STOCK" HeaderText="DIFF STOCK" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                    <asp:BoundField DataField="LOT_STD_GLM" HeaderText="LOT STDGLM" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                    <asp:BoundField DataField="WIP_SEQ_NO" HeaderText="WIP SEQNO" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                    <asp:BoundField DataField="NO_OF_UNIT" HeaderText="NO OFUNIT" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"
                                        DataFormatString="{0:0.00}" />
                                    <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM OFUNIT" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"
                                        DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="WEIGHT OFUNIT" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"
                                        DataFormatString="{0:0.00}" />
                                         <asp:BoundField DataField="TDATE" HeaderText="TDATE" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify" />
                                    <asp:BoundField DataField="TUSER" HeaderText="TUSER" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                  
                                    
                                         <asp:BoundField DataField="STATUS" HeaderText="STATUS" HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                   
                                    <asp:BoundField DataField="REMARKS" HeaderText="REMARKS"  HeaderStyle-Width="17px" ItemStyle-Width="23px" ItemStyle-HorizontalAlign="Justify" HeaderStyle-HorizontalAlign="Justify"/>
                                 
                                    
                                    
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
    </td>
    </tr>
    
    
    
    
</table>
