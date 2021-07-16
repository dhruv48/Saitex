<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WIPStockQueryForm.ascx.cs" Inherits="Module_Production_Controls_WIPStockQueryForm" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:UpdatePanel ID="UpdatePanel8971" runat="server">
    <ContentTemplate>--%>
        <table width="100%" class ="td tContentArial">
            <tr>
                <td width="100%">
                    <table >
                        <tr>
                        <td >
                                &nbsp;</td>
                            <td id="tdClear" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" onclick="imgbtnHelp_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class ="td">
                        <tr>
                            <td align="center" valign="top" class="tRowColorAdmin ">
                                <span class="titleheading">WIP Stock Query Form</span>
                            </td>
                        </tr>
                    </table>
                    <table width="75%" >
                        <tr>
                            <td align="right" >
                                Select&nbsp;Branch:
                            </td>
                            <td >
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" >
                                </asp:DropDownList>
                            </td>
                            <td align="right" >
                                Department: </td>
                            <td >
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                <td align="right" >
                                Lot&nbsp;No:</td>
                            <td >
            <asp:DropDownList ID="ddllotno" runat="server" DataTextField="LOT_NUMBER" DataValueField="LOT_NUMBER"
                Width="160px"  CssClass="tContentArial">
                <%-- OnSelectedIndexChanged="ddllotno_SelectedIndexChanged AutoPostBack="True"--%>
            </asp:DropDownList>
                            </td>
                            <td align="right" class="style4">
                                &nbsp;</td>
                            <td >
                                &nbsp;</td>
                            <td align="right" >
                                Denier: </td>
                            <td >
                                <asp:DropDownList ID="ddlDenier" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right" >
                            <asp:CheckBox ID="ChkStock" runat=server Text="Bal WIP" TextAlign="Right" Checked="true" />
                                </td>
                            <td >
                                </td>
                            
                             <td align="right" >
                                 </td>
                            <td >
                                </td>
                        </tr>
                        <caption>
                            <br />
                            <tr>
                                <td align="right" >
                                    From&nbsp;Date: </td>
                                <td >
                        <asp:TextBox ID="TxtFromDate" runat="server" TabIndex="6" Width="145px" CssClass="SmallFont"   ></asp:TextBox><%--OnTextChanged="TxtFromDate_TextChanged"--%>
              <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="TxtFromDate" PopupPosition="TopLeft"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>                
                                </td>
                                <td  align ="right">
                                    To&nbsp;Date: </td>
                                <td >
                        <asp:TextBox ID="TxtToDate" runat="server" TabIndex="7" Width="145px" CssClass="SmallFont"
                           ></asp:TextBox> <%--AutoPostBack="true"  OnTextChanged="TxtToDate_TextChanged" --%>
               <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="TxtToDate" Format="dd/MM/yyyy"
                            PopupPosition="TopLeft">
                        </cc1:CalendarExtender>                
                                
                                </td>
                                <td >
                                    Party: </td>
                                <td >
                                <asp:DropDownList ID="ddlpartycode" runat="server" AutoPostBack="True" 
                                    CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                                </td>
                                <td >
                                </td>
                                <td >
                                    </td>
                                <td >Product&nbsp;Type:</td>
                                <td>
                             
                                 <asp:DropDownList Width="160px" TabIndex="2" CssClass="SmallFont TextBox UpperCase"
                ID="ddlProductType" runat="server" AutoPostBack="True" AppendDataBoundItems="True">
               <asp:ListItem id="Select" Text="-----------Select-----------  "></asp:ListItem>
                <asp:ListItem id="TEXTURISING" Text="TEXTURISING"></asp:ListItem>
                <asp:ListItem id="TWISTING" Text="TWISTING"></asp:ListItem>
                <asp:ListItem id="DYENING" Text="DYEING"></asp:ListItem>
            </asp:DropDownList>
                                </td>
                                <td >
                                    <asp:Button ID="btnsave" runat="server" CssClass="AButton" Height="22px" 
                                        OnClick="btnsave_Click" Text="Get Record" Width="85px" />
                                </td>
                            </tr>
                            <caption>
                                <br />
                                <tr>
                                    <td colspan="4" width="50%">
                                        <b>Total&nbsp;Records: </b>
                                        <asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                                    </td>
                                    <td colspan="4" width="50%">
                                        <asp:UpdateProgress ID="UpdateProgress9587" runat="server">
                                            <ProgressTemplate>
                                                <h3>
                                                    Loading...</h3>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </caption>
                        </caption>
                    </table>
                    <table  width="100%" >
                        <tr>
                            <td align="left">
                                <asp:Panel ID="pnlShowHover" runat="server" ScrollBars="Auto" Width="100%">
                                    <asp:GridView ID="Grid1" runat="server" AutoGenerateColumns="False" 
                                        HeaderStyle-Font-Bold="true" Width="80%"
                                        AllowPaging="true" PageSize="20" CellPadding="3" ForeColor="#333333" GridLines="Both"
                                        BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small" 
                                        onpageindexchanging="Grid1_PageIndexChanging" >
                                       <%-- OnPageIndexChanging="Grid1_PageIndexChanging"--%>
                                       
                                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />

                                        <Columns>                   
                                                                                   
                                           <asp:BoundField DataField="BRANCH_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Name"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                           <asp:BoundField DataField="DEPT_NAME" HeaderStyle-HorizontalAlign="Left" HeaderText="Dept Name" Visible="false"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                          <%--  <asp:BoundField DataField="PRODUCT_TYPE" HeaderText="Product Type" />
                                            <asp:BoundField DataField="ORDER_NO" HeaderText="Order No" />
                                            <asp:BoundField DataField="PRTY_NAME" HeaderText="Party Name"/>--%>
                                            <asp:BoundField DataField="ARTICLE_DESC" HeaderText="Denier" />
                                           <%-- <asp:BoundField DataField="ORD_SHADE_CODE" HeaderText="Shade"/>--%>
                                            <asp:BoundField DataField="LOT_NUMBER" HeaderText="Lot No"/>
                                            <asp:BoundField DataField="LOT_QTY" HeaderText="Issue Qty"/>
                                            <asp:BoundField DataField="ISSUE_QTY" HeaderText="On Machine" />
                                            <asp:BoundField DataField="STOCK_QTY" HeaderText="Bal Qty" />
                                            <asp:BoundField DataField="STOCK_BAL_QTY" HeaderText="Cl. Bal"  Visible="false"/>
                                            <asp:BoundField DataField="USER_NAME" HeaderText="User Name" Visible="false"/>
                                          
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"/>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <%--</ContentTemplate>--%>