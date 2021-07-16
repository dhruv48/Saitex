<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobCardEntry_Dyeing.ascx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Controls_JobCardEntry_Dyeing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
        width: 100px;
    }
    .c2
    {
        
        width: 140px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        width: 190px;
    }
    .c5
    {
        margin-left: 4px;
        width: 320px;
    }
    .c6
    {
        margin-left: 4px;
        width: 150px;
    }
    .c7
    {   margin-left: 4px;
        width: 70px;
    }
    .c8
    {   margin-left: 4px;
        width: 200px;
    }
    .style1
    {
        height: 26px;
    }
</style>
<asp:UpdatePanel id="UpdatePanel1"  runat="server">
<ContentTemplate>
<table class="tContentArial" align="center" width="95%">
    <tr>
        <td valign="top" class="td" align="left" width="100%">
            <table align="left">
                <tr>
                    <td id="tdSave" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" Height="41" Width="48" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Update" Height="41" Width="48" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
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
        <td class="TableHeader" class="td" align="center" width="100%">
            <b class="titleheading">Job Card Entry</b>
        </td>
    </tr>
    <tr>
        <td class="td" valign="top" align="left" width="100%">
            <span style="color: #ff0000">You are in &nbsp;<asp:Label ID="lblMode" runat="server">
            </asp:Label>&nbsp;Mode</span>
        </td>
    </tr>
    <tr>
        <td class="td" align="left" width="100%">
            <table align="left" width="100%">
                <tr >
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblBatchCode" runat="server" CssClass="SmallFont" Text="Job Code"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtBatchCode" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                        <cc2:ComboBox ID="ddlBatchCode" runat="server" TabIndex="1" Width="100%" MenuWidth="600"
                            AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                            Height="200px" EnableVirtualScrolling="true" OnLoadingItems="ddlBatchCode_LoadingItems"
                            OnSelectedIndexChanged="ddlBatchCode_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    JC No</div>
                                <div class="header c1">
                                    JC Date</div>
                                <div class="header c1">
                                    Pa No</div>
                                <div class="header c2">
                                    Lot No
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("BATCH_CODE")%></div>
                                <div class="item c1">
                                    <%# Eval("BATCH_DATE")%></div>
                                <div class="item c2">
                                    <%# Eval("PA_NO")%></div>
                                <div class="item c1">
                                    <%# Eval("GREY_LOT_NO")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblBatchDate" runat="server" Text="Job Date"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtBatchDate" CssClass="TextBox SmallFont" Width="100%" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="ceBatchDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBatchDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblPaNo0" runat="server" Text="Order No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtOrderNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblPaNo" runat="server" Text="Select PA No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <cc2:ComboBox ID="ddlPaNo" runat="server" TabIndex="1" Width="100%" MenuWidth="1000"
                            AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                            Height="200px" EnableVirtualScrolling="true" OnLoadingItems="ddlPaNo_LoadingItems"
                            OnSelectedIndexChanged="ddlPaNo_SelectedIndexChanged" EmptyText="Select PA No" >
                            <HeaderTemplate>
                            <div class="header c8">
                                    Party Name</div>
                                     <div class="header c1">
                                    Shade Code</div>
                                <div class="header c1">
                                    Order NO</div>
                                <div class="header c2">
                                    Pa No</div>
                                    <div class="header c7">
                                    Issue NO
                                </div>
                                    
                                 <div class="header c3">
                                    Machine Code
                                </div>
                                <div class="header c7">
                                    Trn No
                                </div>
                                <div class="header c7">
                                    Qty
                                </div>
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                            <div class="item c8">
                                    <%# Eval("PRTY_NAME")%></div>
                                    <div class="item c1">
                                    <%# Eval("SHADE_CODE")%></div>
                                <div class="item c1">
                                    <%# Eval("CUST_REQ_NO")%></div>
                                <div class="item c2">
                                    <%# Eval("PI_NO")%></div>
                                    <div class="item c7">
                                    <%# Eval("ISSUE_NO")%></div>
                                    
                                 <div class="item c3">
                                    <%# Eval("MACHINE_CODE")%></div>
                                    
                                     <div class="item c7">
                                    <%# Eval("TRN_NUMB")%></div>
                                <div class="item c7">
                                    <%# Eval("TRN_QTY")%></div>
                                    
                               
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblPaNo1" runat="server" Text="PA No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtPaNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblLotNo" runat="server" Text="Cust Rq. No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtCustReqNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                
                 <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblParty" runat="server" Text="Party Name"></asp:Label>
                    </td>
                    <td class="style1" width="17%">
                        <asp:TextBox ID="txtParty" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="style1" colspan="2">
                        <asp:TextBox ID="txtPartDtl" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight"  width="17%">
                        <asp:Label ID="lblLotSize" runat="server" Text="Batch Qty"></asp:Label>
                    </td>
                    <td class="style1" width="15%">
                        <asp:TextBox ID="txtLotSize" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblArticle" runat="server" Text="Quality"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <asp:TextBox ID="txtArticle" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%" colspan="2" style="width: 34%">
                        <asp:TextBox ID="txtArticleDesc" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblShade" runat="server" Text="Shade"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtShade" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                 <td class="tdRight" width="17%">
                        <asp:Label ID="Label1" runat="server" Text="Issue Slip No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                      <asp:TextBox ID="txtIssueNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="Label2" runat="server" Text="Issue Slip Date"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                       <asp:TextBox ID="txtIssueDate" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server" ></asp:TextBox>
                    </td>
                   <td class="tdRight" width="17%">
                        &nbsp;<asp:Label ID="Label3" runat="server" Text="Trolly No/Crate No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="ddlTrolyNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                             runat="server" ReadOnly="true"></asp:TextBox>                             
                    </td>
                </tr>
                  <tr>
                <td class="tdRight" width="17%">
                     Jober:
                </td>
                <td class="tdLeft" width="17%">
                       <cc2:ComboBox ID="txtPartyCode1" runat="server"  EnableLoadOnDemand="true"
                            DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" 
                           EmptyText="Select Party" EnableVirtualScrolling="true"
                            Width="100%" MenuWidth="450px" Height="200px" 
                           OnLoadingItems="txtPartyCode_LoadingItems" 
                           onselectedindexchanged="txtPartyCode1_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
                            <HeaderTemplate>
                                <div class="header c7">
                                    Code</div>
                                <div class="header c4">
                                    Name</div>
                                    <div class="header c2">
                                    Category</div>
                                    
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c7">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                     <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("VENDOR_CAT_CODE") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                                           
                
                
                    
                            
                </td>
                <td class="tdLeft" width="17%" colspan="2">
                 <asp:TextBox ID="txtJbCode" CssClass="TextBox TextBoxDisplay SmallFont" Width="15%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                     <asp:TextBox ID="txtJbName" CssClass="TextBox TextBoxDisplay SmallFont" Width="80%"
                             ReadOnly="true" runat="server"></asp:TextBox>
                </td>
                <%--<td class="tdLeft" width="17%"></td>--%>
                <td class="tdRight" width="17%">
                 <asp:Label ID="lblMachineCode" runat="server" Text="Machine Code" ></asp:Label>
                </td>
                <td class="tdLeft" width="17%">
                <cc2:ComboBox ID="ddlMachine" runat="server"  CssClass="SmallFont" Width="100%"
                                    EmptyText="select..." EnableLoadOnDemand="true" 
                        EnableVirtualScrolling="true" TabIndex="11"
                                    Height="200px" MenuWidth="200px" 
                        OnLoadingItems="ddlMacCode_LoadingItems" onselectedindexchanged="ddlMachine_SelectedIndexChanged" AutoPostBack="true"
                                    >
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Mac Code</div>
                                        <div class="header c2">
                                            Mac Capacity</div>
                                       
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container3" runat="server" Text='<%# Eval("MACHINE_CAPACITY") %>' />
                                        </div>
                                    </ItemTemplate>
                                </cc2:ComboBox>
                </td>
                </tr>
                
                <tr>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblGreyLot" runat="server" Text="Grey Lot"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                        <cc2:ComboBox ID="ddlGreyLot" runat="server" TabIndex="1" Width="100%" MenuWidth="500"
                            AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                            Height="220px" EnableVirtualScrolling="true" OnLoadingItems="ddlGreyLot_LoadingItems"
                            OnSelectedIndexChanged="ddlGreyLot_SelectedIndexChanged" 
                            EmptyText="Select Lot" style="left: 0px; top: -11px">
                            <HeaderTemplate>
                                <div class="header c1">
                                  Grey Lot No</div>
                                <div class="header c3">
                                 Lab Dip No </div>
                                <div class="header c1">
                                   LR Option 
                                </div>
                               <div class="header c1">
                                   Shade No 
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("GREY_LOT_NO")%></div>
                                <div class="item c3">
                                    <%# Eval("LAB_DIP_NO")%></div>
                                <div class="item c1">
                                    <%# Eval("LR_OPTION")%></div>
                                  <div class="item c1">
                                    <%# Eval("SHADE_CODE")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight" width="17%">
                    
                    <asp:Label ID="lblMachineName" runat="server" Text="Machine Name"></asp:Label>
                    
                      
                     
                    </td>
                    <td class="tdLeft" width="17%">
                    
                    <asp:TextBox ID="txtMachineName" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    
                      
                    </td>
                    <td class="tdRight" width="17%">
                     
                     
                    </td>
                    <td class="tdLeft" width="15%">
                    <asp:TextBox ID="txtMachineCode" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                
              
                
                 <tr>
                 <td class="tdRight" width="17%">
                        <asp:Label ID="LblGreyLot1" runat="server" Text="Grey Lot No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                      <asp:TextBox ID="txtGreyLotNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:TextBox ID="txtTRNno" CssClass="TextBox TextBoxDisplay SmallFont" 
                                  ReadOnly="true" Visible="false" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblLabDipNo" runat="server" Text="Lab Dip No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                       <asp:TextBox ID="txtLabDipNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                   <td class="tdRight" width="17%">
                        &nbsp;<asp:Label ID="lblOption" runat="server" Text="OPTION"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtOption" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                             runat="server" ReadOnly="true"></asp:TextBox>                             
                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"  TargetControlID="txtSpring"   FilterType="Custom, Numbers" />
                    </td>
                </tr>
                
                <tr>
                 <td class="tdRight" width="17%">
                        <asp:Label ID="lblMachineVol" runat="server" Text="Machine Volume"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                      <asp:TextBox ID="txtMachineCap" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="17%">
                        <asp:Label ID="lblSpindle" runat="server" Text="No. Of Spindles"></asp:Label>
                    </td>
                    <td class="tdLeft" width="17%">
                       <asp:TextBox ID="txtMachineSpindle" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                            ReadOnly="true" runat="server" ></asp:TextBox>
                    </td>
                   <td class="tdRight" width="17%">
                        &nbsp;<asp:Label ID="lblSpring" runat="server" Text="No of Cheese"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:TextBox ID="txtSpring" CssClass="extBox TextBoxDisplay SmallFont" Width="100%"
                             runat="server" ReadOnly="true"></asp:TextBox>                             
                        <cc1:FilteredTextBoxExtender ID="FiltertxtQty" runat="server"  TargetControlID="txtSpring"   FilterType="Custom, Numbers" />
                    </td>
                </tr>
                
                <tr>
                <td class="tdRight" width="17%">
                <asp:Label ID="lblShadeNature" runat="server" Text="Shade Nature"></asp:Label>
                </td>
                <td class="tdLeft" width="17%">
                <asp:TextBox ID="txtShadeNature" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                             runat="server" ReadOnly="true"></asp:TextBox> 
                </td>
                <td class="tdRight" width="17%"> 
                  <asp:Label ID="lblPaperWt" runat="server" Text="Paper Tube Weight"></asp:Label>
                </td>
                <td class="tdLeft" width="17%">
                  <asp:TextBox ID="txtPaperTubeWt" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                             runat="server" ReadOnly="true"></asp:TextBox> 
                </td>
                 <td class="tdRight" width="17%"> 
                 <asp:Label ID="Label5" runat="server" Text="Paper Tube Size"></asp:Label>
                 </td>
                 <td class="tdLeft" width="17%">
                 <asp:TextBox ID="txtPaperTubeSize" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                             runat="server" ReadOnly="true"></asp:TextBox> 
                 </td>
                
                </tr>
               
                <tr>
                <td class="tdRight" width="17%">
                        <asp:Label ID="lblProcess" runat="server" Text="Select Process"></asp:Label>
                    </td>
                      <td class="tdLeft" width="17%">
                       <cc2:ComboBox ID="ddlProcessCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                        DataTextField="PROS_CODE" Width="100%" DataValueField="PROS_CODE" EnableLoadOnDemand="true"
                        MenuWidth="350px" OnLoadingItems="ddlProcessCode_LoadingItems" OnSelectedIndexChanged="ddlProcessCode_SelectedIndexChanged"
                        Height="200px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="9" EmptyText="Select Process"
                        Visible="true">
                            <HeaderTemplate>
                                <div class="header c5">
                                    Process Type
                                </div>
                                <div class="header c5">
                                    Pros Desc
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c5">
                               <%# Eval("PROS_CODE")%></div>
                                <div class="item c5">
                                    <%# Eval("PROS_DESC")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:TextBox ID="txtProcess" CssClass="TextBox TextBoxDisplay SmallFont" Width="150%"
                            ReadOnly="true" runat="server" Visible="false" ></asp:TextBox>
                      </td>
                    <td class="tdRight" width="17%">
                        &nbsp;<asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
                    </td>
                    <td class="tdLeft" colspan="1">
                        <asp:TextBox ID="txtRemarks" CssClass="TextBox SmallFont" Width="100%" runat="server"></asp:TextBox>
                    </td>
                     <td class="tdRight" width="17%">
                        &nbsp;<asp:Label ID="Label4" runat="server" Text="Tint No"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%">
                        <asp:DropDownList ID="ddlTint" CssClass="TextBox SmallFont" Width="100%"
                             runat="server" Enabled="false">
                            <asp:ListItem >YES</asp:ListItem>
                             <asp:ListItem>NO</asp:ListItem>
                                    
                                    </asp:DropDownList>                             
                    </td>
                </tr>
                
                
            </table>
            <tr id="tr1" runat="server">
        <td class="td" align="left" width="100%">
          <%--  <asp:Panel ID="Panel1" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">--%>
             <asp:GridView ID="grdDyesTrn" runat="server" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdDyesTrn_RowCommand"
                    ShowFooter="false" BorderWidth="1px" AutoGenerateColumns="False"
                    AllowSorting="True" Width="98%">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Sr." Visible="false">
                        <ItemTemplate>
                         <asp:Label ID="lblSr1" runat="server" CssClass=" tdRight Label SmallFont" Text='<%# Bind("SR_NO") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Lab Dip No" Visible="false">
                        <ItemTemplate>
                         <asp:Label ID="lblLabDipNo" runat="server" CssClass=" tdRight Label SmallFont" Text='<%# Bind("LAB_DIP_NO") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="LR Option" Visible="false">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblLrOption" runat="server" Text='<%# Bind("LR_OPTION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Grey Lot No" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblGreyLotNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("GREY_LOT_NO") %>'
                                ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shade No" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblShadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="DYE NAME" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="txtDYENAME" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DYE_NAME") %>'
                                ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dyes Desc">
                        <ItemTemplate>
                            <asp:Label ID="txtDYEDTL" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DYE_DTL") %>'
                                 ToolTip='<%# Bind("DYE_NAME") %>'></asp:Label>       
                        </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="% Of Dyes">
                            <HeaderStyle VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                            <asp:TextBox ID="txtDyesQTY" runat="server" ReadOnly ="true" CssClass="TextBox TextBoxDisplay SmallFont" Width="80px" Text='<%# Bind("DOSE") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                             <asp:Label ID="txtRATE" runat="server" CssClass=" tdRight Label SmallFont" Text='<%# Bind("RATE") %>'>
                             </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Batch Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLotQty" ReadOnly ="true" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Text='<%# Bind("LOT_SIZE") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Dyes Wt">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDyesWT" ReadOnly ="true" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Text='<%# Bind("TRN_QTY") %>'></asp:TextBox>
                                    
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Dyes KG">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDyesKG" ReadOnly ="true" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Text='<%# Eval("TRN_QTY_KG") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Dyes GM">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDyesGM" ReadOnly ="true" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Text='<%# Eval("TRN_QTY_GM" ) %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText="Dyes MGM">
                            <ItemTemplate>
                                <asp:TextBox ID="txtDyesMGM" ReadOnly ="true" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Text='<%# Bind("TRN_QTY_MGM") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                              <asp:LinkButton ID="lnkEdit" TabIndex="29" runat="server" Text="Edit" CommandName="JOBCreditEdit"
                                CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                            /
                            <asp:LinkButton ID="lnkDelete" TabIndex="29" runat="server" Text="Del" CommandName="JOBCreditDelete"
                                CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
           <%-- </asp:Panel>--%>
        </td>
    </tr>
        </td>
    </tr>
   <tr>
       <td id="Td1" align="left" class="td SmallFont" valign="top" width="100%" runat="server">
            <table width="98%">
                <tr bgcolor="#006699">
                    <td>
                        <span class="titleheading"><b>Process </b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Sub Process </b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Items </b></span>
                    </td>
                   
                    <td>
                        <span class="titleheading"><b> UOM </b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b> Qty </b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Lot Qty</b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Machine Volume</b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Item Qty</b></span>
                    </td>
                     <td>
                        <span class="titleheading"><b>Temp</b></span>
                    </td>
                     <td>
                        <span class="titleheading"><b>Hold Temp</b></span>
                    </td>
                    <td>
                        <span class="titleheading"><b>Remarks</b></span>
                    </td>
                    <td align="left" valign="top">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                <td valign="top" align="left" >
                        <asp:DropDownList ID="ddlChemicalBasis" CssClass="SmallFont" runat="server" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="top">
                        <asp:DropDownList ID="ddlDyeProcessCode" CssClass="SmallFont" runat="server" AppendDataBoundItems="true" Width="110px">  
                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="top">
                       <cc2:ComboBox ID="txtItemCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                        DataTextField="ITEM_CODE" DataValueField="ITEM_CODE" EmptyText="Find Item" EnableLoadOnDemand="true"
                                        EnableVirtualScrolling="true" Height="200px" MenuWidth="300px" OnLoadingItems="txtItemCode_LoadingItems"
                                        OnSelectedIndexChanged="txtItemCode_SelectedIndexChanged" TabIndex="7" Width="130px">
                        <HeaderTemplate>
                            <div class="header c1">
                                Code</div>
                            <div class="header c2">
                                DESCRIPTION</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ITEM_CODE") %>' />
                            </div>
                            <div class="item c2">
                                <asp:Literal ID="Container5" runat="server" Text='<%# Eval("ITEM_DESC") %>' />
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                    </cc2:ComboBox>
                    <asp:TextBox ID="txtICode" runat="server" Width="80px" CssClass="TextBox TextBoxDisplay SmallFont"
                               Font-Bold="False" ReadOnly="True"></asp:TextBox>
                    </td>
                    
                    <td align="left" valign="top">
                 <asp:DropDownList ID="ddlChemicalunit" CssClass="SmallFont" runat="server" Width="40px">
                        </asp:DropDownList>
                    </td>
                    <td align="left" valign="top">                      
                         <asp:TextBox ID="txtChemicalQuantity" runat="server" CssClass="TextBoxNo smallfont" AutoPostBack="true" OnTextChanged="txtChemicalQuantity_TextChanged"
                       TabIndex="24" Width="50px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="None"
                      ErrorMessage="Please Enter Reciepe Quantity" ValidationGroup="Chemical" ControlToValidate="txtChemicalQuantity"></asp:RequiredFieldValidator>
                        <br />
                        <asp:RangeValidator ID="RangeValidator20" runat="server" ErrorMessage="Please Enter Chemical Quantity in Numeric & Precision Should be 10 and Scale 4"
                            Display="None" Type="Double" ControlToValidate="txtChemicalQuantity" MinimumValue="0"
                            MaximumValue="9999999999.9999" ValidationGroup="Chemical"></asp:RangeValidator>
                    </td>
                    <td align="left" valign="top">
                   <asp:TextBox ID="txtItemQtykg" CssClass="TextBox TextBoxDisplay SmallFont" Width="70px" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtiMachineVolum" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true" Width="70px" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtiItemQty"  ReadOnly="true" CssClass="TextBox TextBoxDisplay SmallFont" Width="70px" runat="server"></asp:TextBox>
                    </td>
                    <td valign="top" align="left">
                    <asp:TextBox ID="txtTemp" runat="server" CssClass="SmallFont" TabIndex="12"
                            Width="50px"></asp:TextBox>
                    </td>
                    <td valign="top" align="left">
                    <asp:TextBox ID="txtHoldTemp" runat="server" CssClass="SmallFont" TabIndex="12"
                            Width="70px"></asp:TextBox>
                    </td>
                    <td valign="top" align="left">
                    <asp:TextBox ID="txtDyeRemarks" runat="server" CssClass="SmallFont" TabIndex="12"
                            Width="70px"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:Button ID="lbtnsavedetail" Text="Save" runat="server" TabIndex="17" OnClick="lbtnsavedetail_Click"
                            Width="60px" ValidationGroup="S1"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" colspan="11">
                        <asp:Label ID="lblProsDesc" runat="server" Text="Item Desc" CssClass="TextBoxNo"
                            Width="15%"></asp:Label>
                        <asp:TextBox ID="txtProcessDesc" CssClass="TextBox TextBoxDisplay SmallFont" Width="80%"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                        <asp:Button ID="lbtnCancel" Text="Cancel" runat="server" TabIndex="18" OnClick="lbtnCancel_Click1"
                            Width="60px"></asp:Button>
                    </td>
                </tr>
            </table>
            
        </td>
    </tr>
  <tr id="trGridView" runat="server">
        <td class="td" align="left" width="100%">
            <asp:Panel ID="pnlGrid" runat="server" Height="250px" ScrollBars="Vertical" Width="100%">
             <asp:GridView ID="grdProcessTrn" runat="server" CssClass="SmallFont" Font-Bold="False" OnRowCommand="grdProcessTrn_RowCommand"
                    ShowFooter="false" BorderWidth="1px" AutoGenerateColumns="False"
                    AllowSorting="True" Width="98%">
                    <Columns>
                        <asp:TemplateField HeaderText="Sr. No">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sr." Visible="false">
                        <ItemTemplate>
                         <asp:Label ID="lblSr" runat="server" CssClass=" tdRight Label SmallFont" Text='<%# Bind("SR_NO") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Process">
                        <ItemTemplate>
                         <asp:Label ID="lblBasic" runat="server" CssClass=" tdRight Label SmallFont" Text='<%# Bind("PARA_BASIS") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub Process">
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="txtDYE_PROCESS" runat="server" Text='<%# Bind("DYE_PROCESS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Item Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_CODE") %>'
                                ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item Desc">
                        <ItemTemplate>
                            <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'
                                 ToolTip='<%# Bind("ITEM_CODE") %>'></asp:Label>       
                        </ItemTemplate>
                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="QTY">
                            <HeaderStyle VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                            <asp:TextBox ID="txtQTY" runat="server" ReadOnly ="true" CssClass="TextBox TextBoxDisplay SmallFont" Width="80px" Text='<%# Bind("QTY") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate>
                             <asp:Label ID="txtUNIT" runat="server" CssClass=" tdRight Label SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'>
                             </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lot Qty">
                            <ItemTemplate>
                                <asp:TextBox ID="txtLotQty" ReadOnly ="true" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Text='<%# Bind("LOT_SIZE") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Machine Volumn">
                            <ItemTemplate>
                                <asp:TextBox ID="txtMachineVolumn" runat="server" Width="70px" ReadOnly ="true" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Text='<%# Bind("MACHINE_CAPACITY") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Item Qty(KG)">
                            <ItemTemplate>
                                <asp:TextBox ID="txtItemQty" ReadOnly ="true" runat="server" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Text='<%# Bind("TRN_QTY") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Temp">
                        <ItemTemplate>
                        <asp:Label ID="txtTemp" runat="server" CssClass=" tdRight Label SmallFont" Text='<%# Bind("TEMP") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Hold Temp">
                        <ItemTemplate>
                        <asp:Label ID="txtHoldTemp" runat="server" CssClass=" tdRight Label SmallFont" Text='<%# Bind("HOLD_TIME") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                        <asp:Label ID="txtDyeRemarks" runat="server" CssClass=" tdRight Label SmallFont" Text='<%# Bind("REMARKS") %>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                              <asp:LinkButton ID="lnkEdit" TabIndex="29" runat="server" Text="Edit" CommandName="JOBCreditEdit"
                                CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                            /
                            <asp:LinkButton ID="lnkDelete" TabIndex="29" runat="server" Text="Del" CommandName="JOBCreditDelete"
                                CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>