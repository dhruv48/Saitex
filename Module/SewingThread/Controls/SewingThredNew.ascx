<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SewingThredNew.ascx.cs" Inherits="Module_Sewing_Thread_Controls_SewingThredNew" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<table align="left" class="tContentArial">
    <tr>
        <td class="td">
            <table align="left">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" 
                            ToolTip="Save" ValidationGroup="s" ImageUrl="~/CommonImages/save.jpg" 
                            TabIndex="10" onclick="imgbtnSave_Click"  />
                    </td> 
                     <td id="tdUpdate" runat="server">
                          <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ValidationGroup="M1"
                                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48">
                          </asp:ImageButton>
                     </td>
                     <td id="tdFind" runat="server" >
                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                        Width="48" Height="41" TabIndex="7" OnClick="imgbtnFind_Click"></asp:ImageButton>
                     </td>
                     <td id="tdDelete"  runat="server">
                       <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ImageUrl="~/CommonImages/del6.png"
                                                 onclientclick="if (!confirm('Are you want to Delete ?')) { return false; }"       ToolTip="Delete" Height="41" Width="48"></asp:ImageButton>
                       </td>                 
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" 
                            ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg" 
                            onclick="imgbtnClear_Click" ></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png" ></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" 
                            ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png" 
                            onclick="imgbtnExit_Click" ></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center">
            <b class="titleheading">Sewing Thread</b>
        </td>
    </tr>    
    <tr>
        <td>
            <table class="td">
            <tr>
                <td class="tdRight">Artical No:</td>
                <td colspan="3"><asp:TextBox ID="TxtArticalCode" runat="server" Width="80px" CssClass="TextBoxNo"  ReadOnly="True"></asp:TextBox>
                <asp:DropDownList ID="DDLArticalCode" runat="server" Width="162px" Visible="false"  
                        AutoPostBack="True" CssClass="tContentArial" 
                        onselectedindexchanged="DDLArticalCode_SelectedIndexChanged">
                        </asp:DropDownList>
                
                </td>
                <td></td>
                <td></td>
            </tr>
                <tr>
                    <td class="tdRight">
                        Unit:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLUnit" runat="server" Width="162px" CssClass="tContentArial">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Length-Mtr :
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtLenght" runat="server" CssClass="TextBoxNo" onkeyup="pricevalidate(this);"  Width="160px"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        TKT No.:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtTktNo" runat="server" CssClass="TextBoxNo" onkeyup="pricevalidate(this);"  Width="160px" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        Ply/Count :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLPly" runat="server"   CssClass="tContentArial" Width="162px"> </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Tex Size:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtTexSize" runat="server" CssClass="TextBoxNo" onkeyup="pricevalidate(this);"  Width="160px"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        TPI:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtTPI" runat="server" CssClass="TextBox" Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        Twist :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLTwist" CssClass="tContentArial" runat="server" Width="162px" >                          
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Quality :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLQuality" runat="server" Width="162px" CssClass="tContentArial">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Brand :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDlBrand" runat="server" Width="162px" CssClass="tContentArial">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        End Use :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLEndUse" runat="server" Width="162px" AppendDataBoundItems="False"
                            CssClass="tContentArial">
                        </asp:DropDownList>
                    </td>                     
                    <td class="tdRight">
                        Colour Of Unit :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLColor" runat="server" Width="162px" AppendDataBoundItems="False"
                            CssClass="tContentArial">
                        </asp:DropDownList>
                    </td>  
                    <td class="tdRight">Unit Wt.:</td>             
                    <td class="tdLeft"> <asp:TextBox ID="TxtUnitWt" CssClass="TextBoxNo" onkeyup="pricevalidate(this);"  runat="server" Width="160px"></asp:TextBox></td>  
                </tr>
                <tr>                   
                     <td class="tdRight">Unit-Size(inch):</td>             
                    <td class="tdLeft"><asp:TextBox ID="TxtUnitSize" CssClass="TextBoxNo" onkeyup="pricevalidate(this);"  runat="server" Width="160px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
  
</table>