<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Compose.ascx.cs" Inherits="Module_Mail_Controls_Compose" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="obout_FileUpload" Namespace="OboutInc.FileUpload" TagPrefix="fup" %>

<script type="text/JavaScript">

function deleteClicked()
{
  var container = document .getElementById("inputsPlace");
  var inputElls = container.getElementsByTagName("input");
  var inputs    = [];

  for(var i=0; i < inputElls.length; i++)
  {
    inputs.push(inputElls[i]);
  }

  for(var i=0; i < inputs.length; i++)
  {
    var inputElement = inputs[i];
    if(inputElement.type=="checkbox" && inputElement.checked)
    {
      var number = inputElement.id.replace(/^check(\d+)/,"$1");
      var div    = document .getElementById("div"+number);

      div.parentNode.removeChild(div);
    }
  }
}

function addClicked()
{
  var container = document .getElementById("inputsPlace");
  var inputs    = container.getElementsByTagName("input");
  var number    = "1";

  for(var i=0; i < inputs.length; i++)
  {
    var inputElement = inputs[i];
    if(inputElement.type=="checkbox")
    {
      number = (parseInt(inputElement.id.replace(/^check(\d+)/,"$1"))+1).toString();
    }
  }

  var newDiv = document.createElement("DIV");
  newDiv.id  = "div"+number;
  container.appendChild(newDiv);

  var newCheck  = document.createElement("INPUT");
  newCheck.id   = "check"+number;
  newCheck.type = "checkbox";
  newDiv.appendChild(newCheck);

  var newInput  = document.createElement("INPUT");
  newInput.name = "myFile"+number;
  newInput.type = "file";
  newInput.style.marginLeft = "20px";
  newDiv.appendChild(newInput);
}
</script>

<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnSend" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/Mail/send.png"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnSend_Click"></asp:ImageButton>
                        <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                            ImageUrl="~/CommonImages/Mail/save.png" OnClick="imgbtnSave_Click"></asp:ImageButton><asp:ImageButton
                                ID="imgbtnCancel" Width="48" Height="41" runat="server" ToolTip="Cancel" ImageUrl="~/CommonImages/Mail/cancel.png"
                                OnClick="imgbtnCancel_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
        <%--  </td>
                   </td>
                    <td id="tdDelete" runat="server" align="left">
                    <td id="tdFind" runat="server" visible="false" align="left">--%>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">
                <asp:Label ID="lblComposeHeading" runat="server" Text="Compose Mail"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="center" width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="10%">
                        <asp:Label runat="server" ID="lblTo0" Text="From :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="90%">
                        <cc2:OboutDropDownList ID="ddlFrom" runat="server" Width="250px" MenuWidth="300px"
                            Height="150px">
                        </cc2:OboutDropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="10%">
                        <asp:Label runat="server" ID="lblTo" Text="To :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="90%">
                        <asp:TextBox ID="txtTo" Width="98%" CssClass="TextBox SmallFont" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="10%">
                    </td>
                    <td class="tdLeft" width="90%">
                        <asp:LinkButton ID="lbtnAddCC" runat="server" OnClick="lbtnAddCC_Click">Add CC</asp:LinkButton>
                        |
                        <asp:LinkButton ID="lbtnAddBCC" runat="server" OnClick="lbtnAddBCC_Click">Add BCC</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <table width="100%" runat="server" id="tblAddCC">
                <tr>
                    <td class="tdRight" width="10%">
                        <asp:Label runat="server" ID="lblCC" Text="Cc :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="90%">
                        <asp:TextBox ID="txtCC" Width="98%" CssClass="TextBox SmallFont" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%" runat="server" id="tblAddBCC">
                <tr>
                    <td class="tdRight" width="10%">
                        <asp:Label runat="server" ID="lblBCC" Text="Bcc :"></asp:Label>
                    </td>
                    <td class="tdLefst" width="90%">
                        <asp:TextBox ID="txtBCC" Width="98%" CssClass="TextBox SmallFont" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="10%">
                        <asp:Label runat="server" ID="lblSubject" Text="Subject :"></asp:Label>
                    </td>
                    <td class="tdLeft" width="90%">
                        <asp:TextBox ID="txtSubject" Width="98%" CssClass="TextBox SmallFont" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="10%">
                    </td>
                    <td class="tdLeft" width="90%">
                        <asp:LinkButton ID="lbtnAddAttachment" runat="server" OnClick="lbtnAddAttachment_Click">Attach File</asp:LinkButton>
                        <cc2:OboutButton runat="server" ID="add" Text="Add new file input" OnClientClick="addClicked(); return false;" />&nbsp;
                        <cc2:OboutButton runat="server" ID="del" Text="Delete checked inputs" OnClientClick="deleteClicked(); return false;" /><br />
                        <div id="inputsPlace">
                            <div id="div1">
                                <input type="checkbox" id="check1" /><input type="file" name="myFile1" style="margin-left: 20px;" />
                                <fup:FileUploadProgress
                                    ID="FileUploadProgress1" runat="server">
                                </fup:FileUploadProgress>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="100%">
                        <obout:Editor ID="txtBody" Submit="false" PreviewMode="true" runat="server">
                        </obout:Editor>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
