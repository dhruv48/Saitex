<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PromotionIncrement.ascx.cs" Inherits="Module_HRMS_Controls_PromotionIncrement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="Combo" %>
 
 
 <style type="text/css">
     .style1
     {
         text-align: right;
     }
 </style>
 
 
 <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
--%>
<table id="tblDesgMainTable" runat="server" cellspacing="0" cellpadding="0" align="Left" class="tContentArial">
    <tr>
        <td align="Right" class="td">
            <table align="left">
                <tr>                                         
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="55px" Height="40px" onclick="imgbtnSave_Click" 
                            ValidationGroup="s"  >
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" onclick="imgbtnUpdate_Click" ValidationGroup="s"  >
                        </asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" onclick="imgbtnDelete_Click" 
                           ></asp:ImageButton>
                    </td>                                     
                   <td id="find" runat="server">
                        <asp:ImageButton ID="Imgbtnfind" runat="server" ToolTip="find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" onclick="Imgbtnfind_Click"  >
                        </asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" onclick="imgbtnClear_Click1" 
                           ></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" onclick="imgbtnPrint_Click"  ></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" onclick="imgbtnExit_Click" ></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" ></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
  
  
    <tr>
        <td align="center" class ="TableHeader td">
            <span class="titleheading">Promotion & Increment </span>
        </td>
    </tr>
  
    <tr>
        <td align="center" >
           
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="s" Height="36px" Width="195px" />
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlemployee"
                Display="None" ErrorMessage=" Please Select Employee . " 
                 SetFocusOnError="True" ValidationGroup="s"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddldesig"
                Display="None" ErrorMessage=" Please Select Designation ." 
                 SetFocusOnError="True" ValidationGroup="s"
               ></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtjoinsalary"
                Display="None" ErrorMessage=" Please Enter Join Time Salary ." SetFocusOnError="True" ValidationGroup="s"
                ></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcurrentsalary"
                Display="None" ErrorMessage=" Please Enter Current Salary . " SetFocusOnError="True"
                ValidationGroup="s" ></asp:RequiredFieldValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtcurrentrating"
                Display="None" ErrorMessage=" Please Enter Current Year Rating ." ValidationGroup="s" SetFocusOnError="True"
                ></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcurrentyearperform"
                Display="None" ErrorMessage=" Please Enter Current Year Performance ." SetFocusOnError="True" ValidationGroup="s"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtlastyearperform"
                Display="None" ErrorMessage=" Please Enter Last Year performance ." SetFocusOnError="True" 
                ValidationGroup="s"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtlastrating"
                Display="None" ErrorMessage=" Please Enter Last Year Rating Required ." ValidationGroup="s" SetFocusOnError="True"
              ></asp:RequiredFieldValidator>    
            </td>
    </tr>
    <tr>
        <td align="left" valign="top" class ="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>     
    <tr>
        <td class ="td">
            <table border="0" cellpadding="3" cellspacing="0" align="left" width="750">
                   <tr>
                        <td style="text-align: right" >              
                            Employee: 
                          
                           
                            </td>
                        <td style="text-align: left" >
                           
                            <%-- <Combo:ComboBox ID="cmbEmpCode" runat="server" Width="200px" 
                                EmptyText="------------Select------------" Height="150px" AutoPostBack="True" 
                                                TabIndex="1"  MenuWidth="300px" DataValueField="EMP_CODE" onselectedindexchanged="cmbEmpCode_SelectedIndexChanged1" 
                                                    >
                                                <HeaderTemplate>
                                                    <div class="header c3">
                                                        Code
                                                    </div>
                                                    <div class="header c4">
                                                        Emp Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c3">
                                                        <%# Eval("EMP_CODE")%></div>
                                                    <div class="item c4">
                                                        <%# Eval("EMPLOYEENAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </Combo:ComboBox>--%>
                            <asp:DropDownList ID="ddlemployee" runat="server" 
                                onselectedindexchanged="ddlemployee_SelectedIndexChanged" 
                                AutoPostBack="True" DataTextField="EMPLOYEENAME" DataValueField="EMP_CODE" 
                                Width="200px" >
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlfind" runat="server" 
                                 onselectedindexchanged="ddlfind_SelectedIndexChanged" AutoPostBack="True" 
                                DataTextField="EMPLOYEENAME" DataValueField="EMP_CODE" Width="200px">
                            </asp:DropDownList>
                          
                          
                        </td>
                        <td ></td>
                        <td >Promotion id :</td>
                        <td >
                            <asp:TextBox ID="txtproid" runat="server" Width="200px" TabIndex="2"></asp:TextBox>
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align: right" >Old Designation:</td>
                        <td >
                            <asp:TextBox ID="TxtDesignation" runat="server" Width="200px" TabIndex="3" 
                                CssClass="TextBox"></asp:TextBox>
                        </td>
                        <td >
                            &nbsp;</td>
                        <td style="text-align: justify" >
                            
                   
                            Promostion
                            &nbsp;Designation:</td>
                        <td >
                       <asp:DropDownList ID="ddldesig" runat="server" DataTextField="DESIG_NAME" 
                                DataValueField="DESIG_CODE" Width="200px" AutoPostBack="True">
                            </asp:DropDownList>
                     <%--  <Combo:ComboBox ID="cmbDisignation" runat="server" Width="200px" 
                                EmptyText="------------Select------------" Height="150px" AutoPostBack="True" 
                                                TabIndex="4"  MenuWidth="300px" DataValueField="DESIG_CODE" 
                                                    >
                                                <HeaderTemplate>
                                                    <div class="header c3">
                                                        CODE
                                                    </div>
                                                    <div class="header c4">
                                                        DESIGNATION </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c3">
                                                        <%# Eval("DESIG_CODE")%></div>
                                                    <div class="item c4">
                                                        <%# Eval("DESIG_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </Combo:ComboBox>--%>
                        </td>
                   </tr>
                   <tr>
                        <td style="text-align: right">
                       
                            Join Gross Salary :</td>
                        <td>
                            <asp:TextBox ID="txtjoinsalary" runat="server" Width="200px" 
                                onkeyup="pricevalidate(this);" TabIndex="5" CssClass="TextBoxNo"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                          
                            Current Salary: 
                            
                            
                            </td>
                            <td>
                            
                            <asp:TextBox ID="txtcurrentsalary" runat="server" Width="200px" 
                                    onkeyup="pricevalidate(this);" TabIndex="6" CssClass="TextBoxNo"></asp:TextBox>
                        </td>
                   </tr>
                   
                   <tr>
                   <td style="text-align: right"> 
                          
                           Current year overall Performance: 
                           </td>
                   <td>
                       <asp:TextBox ID="txtcurrentyearperform" runat="server" Width="200px" 
                           TabIndex="7" MaxLength="75" CssClass="TextBox"></asp:TextBox>
                           </td>
                   <td></td>
                   <td class="style1">
                          
                           Last year overall Performance:</td>
                   <td>
                       <asp:TextBox ID="txtlastyearperform" runat="server" Width="200px" TabIndex="8" 
                           MaxLength="75" CssClass="TextBox"></asp:TextBox>
                           </td>
                   </tr>
                       <tr>
                   <td style="text-align: right">Last year rating :</td>
                   <td>
                       <asp:TextBox ID="txtlastrating" runat="server" Width="200px" TabIndex="9" 
                           MaxLength="75" CssClass="TextBox"></asp:TextBox>
                           </td>
                   <td></td>
                   <td style="text-align: right">Current year rating : </td>
                   <td>
                       <asp:TextBox ID="txtcurrentrating" runat="server" 
                          Width="200px" TabIndex="10" MaxLength="75" CssClass="TextBox"></asp:TextBox>
                           </td>
                   </tr>
                       <tr>
                   <td style="text-align: right">Recomdation :</td>
                   <td>
                       <asp:TextBox ID="txtrecomdation" runat="server" TextMode="MultiLine" 
                           Width="200px" TabIndex="11" MaxLength="200" CssClass="TextBox"></asp:TextBox>
                           </td>
                   <td></td>
                   <td style="text-align: right">Remarks :</td>
                   <td>
                       <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Width="200px" 
                           TabIndex="12" MaxLength="200" CssClass="TextBox"></asp:TextBox>
                           </td>
                   </tr>
                       <tr>
                   <td colspan="5" align="center">
                       <asp:GridView ID="GridEmp" runat="server" AutoGenerateColumns="False" 
                           AllowPaging="True"  
                           onpageindexchanging="GridEmp_PageIndexChanging" PageSize="5" 
                           onrowcommand="GridEmp_RowCommand">                      
                           <Columns>                      
        <asp:BoundField DataField="EMP_CODE" HeaderText="Employee Code" />  
        <asp:BoundField DataField="PROMOTION_ID" HeaderText="Promotion id" />  
        <asp:BoundField DataField="EMPLOYEENAME" HeaderText="Employee Name" />  
        <asp:BoundField DataField="DEPT_NAME" HeaderText="Department" />  
        <asp:BoundField DataField="PROMOTION_DESIGs" HeaderText="Promotion Desig" />  
        <asp:BoundField DataField="JOIN_DT" HeaderText="Date Of Joining" DataFormatString ="{0:dd-MMMM-yyyy}"/>   
        <asp:BoundField DataField="DESIG_NAME" HeaderText="Designation" />  
        <asp:BoundField DataField="JOIN_GROSSSALARY" HeaderText="Join Gross Salary" />   
        <asp:BoundField DataField="CURRENT_GROSSSALARY" HeaderText="Current Gross Salary" />                                                                                   
        <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="top">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEdit" CommandArgument='<%# Eval(" PROMOTION_ID") %>' CommandName="RecordEdit"
                                                    ImageUrl="~/CommonImages/edit1.jpg" runat="server" CssClass="ContolStyle"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top"></ItemStyle>
                                        </asp:TemplateField>                 
                          </Columns>                    
                         
                       </asp:GridView>
                           </td>
                   </tr>
                       </table>
        </td>
    </tr>
  </table>
 </ContentTemplate>                   
                </asp:UpdatePanel>