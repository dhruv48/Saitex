<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Telephonebill.ascx.cs" Inherits="Module_HRMS_Controls_Telephonebill" %>

<script language="javascript">
 function SelectAllCheckboxes(spanChk){
    var oItem = spanChk.children;
   var theBox= (spanChk.type=="checkbox") ? 
        spanChk : spanChk.children.item[0];
   xState=theBox.checked;
   elm=theBox.form.elements;
   for(i=0;i<elm.length;i++)
     if(elm[i].type=="checkbox" && 
              elm[i].id!=theBox.id)
     {
          if(elm[i].checked!=xState)
         elm[i].click();
     }
 }
</script>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table class="td tContent">
<tr>
        <td align="Right" class="td" colspan = "4">
            <table align="left">
                            <tr>
                                <td id="tdSave" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" TabIndex="9" OnClick="imgbtnSave_Click" runat="server"
                                        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>
                                <td id="tdUpdate" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate" TabIndex="9" OnClick="imgbtnUpdate_Click" runat="server"
                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>                               
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnFind" TabIndex="9" OnClick="imgbtnFind_Click" runat="server"
                                        ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" Height="41" Width="48">
                                    </asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                        ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                        ToolTip="Clear" Height="41" Width="48" onclick="imgbtnClear_Click"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                        ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan = "4">
            <span class="titleheading">Employee Telephone Bills</span>
            </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="4">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
   <tr id="trFindingRecord" >
        <td valign="top" class="td" align="center"  colspan="4">
            <table width="60%">
                     <tr><td class="tdRight">Month :</td><td class="tdLeft">  
                            <asp:DropDownList ID="DDLOpenMonth" Width="150px" 
                                 CssClass="SmallFont TextBox UpperCase"  runat="server" AutoPostBack="True" 
                                 onselectedindexchanged="DDLOpenMonth_SelectedIndexChanged">
                             <asp:ListItem Value="">------------Select-------------</asp:ListItem>
                                    <asp:ListItem Value="1"> January </asp:ListItem>
                                    <asp:ListItem Value="2"> February </asp:ListItem>
                                    <asp:ListItem Value="3"> March </asp:ListItem>
                                    <asp:ListItem Value="4"> April </asp:ListItem>
                                    <asp:ListItem Value="5"> May </asp:ListItem>
                                    <asp:ListItem Value="6"> June </asp:ListItem>
                                    <asp:ListItem Value="7"> July </asp:ListItem>
                                    <asp:ListItem Value="8"> August </asp:ListItem>
                                    <asp:ListItem Value="9"> September </asp:ListItem>
                                    <asp:ListItem Value="10"> October </asp:ListItem>
                                    <asp:ListItem Value="11"> November </asp:ListItem>
                                    <asp:ListItem Value="12"> December </asp:ListItem>
                            </asp:DropDownList>
                        </td><td class="tdRight">Year :</td><td class="tdLeft">    
                        <asp:DropDownList ID="DDLOpenYear" Width="150px" 
                             CssClass="SmallFont TextBox UpperCase" runat="server" AutoPostBack="True" 
                             onselectedindexchanged="DDLOpenYear_SelectedIndexChanged"> </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="TrSerach1" runat="server"  visible="false" >
                    <td class="tdRight">Department :</td><td class="tdLeft">
                    <asp:DropDownList ID="ddldepartment" runat="server" class="tdLeft" 
                                AutoPostBack="True" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE" 
                                Width="150px" onselectedindexchanged="ddldepartment_SelectedIndexChanged" CssClass="SmallFont TextBox UpperCase">
                    </asp:DropDownList>
                  
                    </td><td class="tdRight">Branch :</td><td class="tdLeft">
                            <asp:DropDownList ID="DDLBranch" runat="server" Width="150px" 
                                AutoPostBack="True" onselectedindexchanged="DDLBranch_SelectedIndexChanged" CssClass="SmallFont TextBox UpperCase">
                            </asp:DropDownList>
                            </td>             
                            </tr>
                 <tr id="TrSerach2" runat="server"  visible="false"><td class="tdRight">Designation :</td>
                 <td class="tdLeft"><asp:DropDownList ID="DDLDesign" runat="server" class="tdLeft" 
                         AutoPostBack="True" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE" 
                         Width="150px" onselectedindexchanged="DDLDesign_SelectedIndexChanged" CssClass="SmallFont TextBox UpperCase" >
                    </asp:DropDownList></td>
                 <td class="tdRight">Employee :</td>
                 <td class="tdLeft">           
                    <asp:DropDownList ID="ddlemployee" runat="server" 
                                onselectedindexchanged="ddlemployee_SelectedIndexChanged" 
                                AutoPostBack="True" DataTextField="EMPLOYEENAME" DataValueField="EMP_CODE" 
                                Width="150px" CssClass="SmallFont TextBox UpperCase">
                            </asp:DropDownList>
       </td></tr>
            </table>
        </td>
   </tr>
   
<tr><td colspan="4" class = "td">Telephone Record :-</td></tr>
<tr>
<td colspan="4">
    <asp:GridView ID="GVTelephoneRecord" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True" Font-Size="8pt" PageSize="15" CellPadding="3" GridLines="None" Width="100%" ForeColor="#333333" 
        CssClass = "smallfont" onpageindexchanging="GVTelephoneRecord_PageIndexChanging" >       
        <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="EMP CODE" HeaderStyle-Width="50px"   ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="50px">
             <ItemTemplate>
                    <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                </ItemTemplate>                

<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" Width="50px"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField HeaderText="EMPLOYEE" HeaderStyle-HorizontalAlign="Left" > <ItemTemplate>
                                    <asp:Label ID="lblempname" Width="140px" runat="server" Text='<%# Eval("EMPLOYEENAME") %>'></asp:Label>
              </ItemTemplate>            

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="DEPARTMENT"> <ItemTemplate>
                    <asp:Label ID="lbldepartment" runat="server" Text='<%# Eval("DEPT_NAME") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="DESIGNATION"> <ItemTemplate>
                    <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("DESIG_NAME") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField>              
            <asp:TemplateField HeaderText="MOBILE NO">
             <ItemTemplate>
                 <asp:TextBox ID="txtmob" runat="server" Text='<%# Eval("TELEPHONE_NO") %>' Enabled="false"  CssClass="TextBoxNo SmallFont" Width ="70px"></asp:TextBox>
                </ItemTemplate>                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TERIF PLAN">
             <ItemTemplate>
                 <asp:TextBox ID="txtterif" runat="server" Text='<%# Eval("TERIFF_PLAN") %>'  Enabled="false" CssClass="TextBoxNo SmallFont" Width ="70px"></asp:TextBox>
                </ItemTemplate>                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MOBILE LIMIT">
             <ItemTemplate>
                   <asp:TextBox ID="txtmoblimit" runat="server" Text='<%# Eval("TELEPHONE_LIMIT") %>' Enabled="false" CssClass="SmallFont TextBoxNo" onKeyPress="pricevalidate(this);" Width ="60px"></asp:TextBox>
                </ItemTemplate>           
            </asp:TemplateField>
             <asp:TemplateField HeaderText="BILL NO">
             <ItemTemplate>
                 <asp:TextBox ID="txttelid" runat="server" Text='<%# Eval("BILL_NO") %>' TabIndex="1" CssClass="TextBoxNo SmallFont" Width ="40px"></asp:TextBox>
                </ItemTemplate>                
            </asp:TemplateField> 
             <asp:TemplateField HeaderText="BILL AMT.">
             <ItemTemplate>
                 <asp:TextBox ID="TxtBillAmt" runat="server" Text='<%# Eval("BILL_AMOUNT") %>' TabIndex="2"
                     CssClass="TextBoxNo SmallFont" Width ="40px" AutoPostBack="True" 
                     ontextchanged="TxtBillAmt_TextChanged"></asp:TextBox>
                </ItemTemplate>                
            </asp:TemplateField> 
             <asp:TemplateField HeaderText="PERSONAL CALL">
             <ItemTemplate>
                   <asp:TextBox ID="txtprsncall" runat="server" 
                       Text='<%# Eval("PERSONAL_CALL_AMOUNT") %>' CssClass="TextBoxNo SmallFont" TabIndex="3"
                       onKeyPress="pricevalidate(this);" Width ="60px" AutoPostBack="True" 
                       ontextchanged="txtprsncall_TextChanged"></asp:TextBox>
                </ItemTemplate>           
            </asp:TemplateField>  
             <asp:TemplateField HeaderText="OFFICIAL CALL">
             <ItemTemplate>
                    <asp:TextBox ID="txtofficailcall" runat="server" 
                        Text='<%# Eval("OFFICIAL_CALL_AMOUNT") %>' CssClass="TextBoxNo SmallFont" TabIndex="4"
                        onKeyPress="pricevalidate(this);" Width ="60px" AutoPostBack="True" 
                        ontextchanged="txtofficailcall_TextChanged"></asp:TextBox>
                </ItemTemplate>             
            </asp:TemplateField>         
                     
             <asp:TemplateField HeaderText="Adj." >
             <HeaderTemplate><input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" type="checkbox" /></HeaderTemplate>            
             <ItemTemplate>
                 <asp:CheckBox ID="ChkAdjust" AutoPostBack="true" CssClass="TextBoxNo SmallFont" TabIndex="5" Width="20px" runat="server" oncheckedchanged="ChkAdjust_CheckedChanged" />
             </ItemTemplate>             
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Adjust Amount">               
               <ItemTemplate>
                    <asp:TextBox ID="TxtAdjustAmt" runat="server" Text='<%# Eval("ADJUSTMENT_AMOUNT") %>' TabIndex="6"
                        Enabled="false"  CssClass="TextBoxNo SmallFont" 
                        onKeyPress="pricevalidate(this);" Width ="60px" AutoPostBack="True" 
                        ontextchanged="TxtAdjustAmt_TextChanged"></asp:TextBox>
                </ItemTemplate>               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="AMOUNT PAYBLE">
               <ItemTemplate>
                    <asp:TextBox ID="txtamtpayble" runat="server" Text='<%# Eval("EMP_DEDUCTION_AMOUNT") %>'  Enabled="false"  CssClass="TextBoxNo SmallFont" onKeyPress="pricevalidate(this);" Width ="60px"></asp:TextBox>
                </ItemTemplate>               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="REMARKS">
                <ItemTemplate>
                    <asp:TextBox ID="txtremarks" runat="server" Text='<%# Eval("REMARKS") %>' TabIndex="7" CssClass="TextBox SmallFont" Width ="60px" ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField Visible="false">
                <ItemTemplate> 
                 <asp:Label ID="LblMobileID" runat="server" Text='<%# Eval("TELEPHONE_NO") %>' /> 
                    <asp:Label ID="LblYear" runat="server" Text='<%# Eval("BILL_YEAR") %>' />
                    <asp:Label ID="LblMonth" runat="server" Text='<%# Eval("BILL_MONTH") %>' />  
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
            
    </asp:GridView>
    
    </td>
  </tr>

</table>
</ContentTemplate>
</asp:UpdatePanel>