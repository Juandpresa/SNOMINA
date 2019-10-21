<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmSubsidioEmpleado.aspx.cs" Inherits="EscenariosQnta.wfrmSubsEmp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
    <script language="Javascript" type="text/javascript">
        function isAlphabetKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97 || charCode == 32)) {

                return true;
            }
            return false;

        }

        function isDecimalKey(e, field) {
            key = e.keyCode ? e.keyCode : e.which
            // backspace
            if (key == 8) return true
            // 0-9
            if (key > 47 && key < 58) {
                if (field.value == "") return true
                regexp = /.[0-9]{4}$/
                return !(regexp.test(field.value))
            }

            // .
            if (key == 46) {
                if (field.value == "") return false
                regexp = /^[0-9]+$/
                return regexp.test(field.value)
            }
            // other key
            return false



        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <div id="html">
        <!--===================== Content ======================-->
        <div id="portfolio" >
  <div class="container_13">    
  </div>
  
</div>
        
            <div id="stars">
            </div>
            <div id="stars2">
            </div>
            <div id="srars3">
            </div>
            <div class="containerTitlePage">
                <div class="titlePage">
                        SUBSIDIO EMPLEADO</div>
            </div>
        </div>
        <div id="skills" >
  <div class="container_12">
  
  DATOS GENERALES
  <div style="width:auto; border:2px Solid #4a1414;"></div>
  <div class="contenPanel">
        <table>
            <tr> 
                <td>Periodo:</td>
                <td class="td"><asp:DropDownList ID="ddlPeriodo" runat="server" 
                        CssClass="cssDropdown" AutoPostBack="true" 
                        onselectedindexchanged="ddlPeriodo_SelectedIndexChanged" ></asp:DropDownList> </td>                                                
            </tr>              
        </table>                
    </div>
        <div>
           
        </div>
        <div style="height:50px;"></div>
        <div>

<asp:GridView ID="gvSudsidio" runat="server" CssClass="mGrid" AutoGenerateColumns = "false"  
 AllowPaging="true" 
                 PageSize="50"  ShowFooter="true" 
                onrowcancelingedit="gvSudsidio_RowCancelingEdit" 
                onrowediting="gvSudsidio_RowEditing" onrowupdating="gvSudsidio_RowUpdating" >
<Columns>
<asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "Id" Visible="false">
    <ItemTemplate>
        <asp:Label ID="lblId" runat="server"
        Text='<%# Eval("Id")%>'></asp:Label>
    </ItemTemplate>   
</asp:TemplateField>
<asp:TemplateField  HeaderText = "Limite Inferior">
    <ItemTemplate>
        <asp:Label ID="lblLimInf" runat="server"
                Text='<%# Eval("LimInf")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtLimInf" runat="server"
            Text='<%# Eval("LimInf")%>' onkeypress="return isDecimalKey(event)"></asp:TextBox>
    </EditItemTemplate>   
    <FooterTemplate>
    <asp:TextBox ID="txtLimInf" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
    </FooterTemplate> 
</asp:TemplateField>
<asp:TemplateField   HeaderText = "Limite Superior">
    <ItemTemplate>
        <asp:Label ID="lblLimSup" runat="server"
                Text='<%# Eval("LimSup")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtLimSup" runat="server"
            Text='<%# Eval("LimSup")%>' onkeypress="return isDecimalKey(event)"></asp:TextBox>
    </EditItemTemplate>  
    <FooterTemplate>
    <asp:TextBox ID="txtLimSup" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
    </FooterTemplate>   
</asp:TemplateField>
<asp:TemplateField   HeaderText = "Subsidio">
    <ItemTemplate>
        <asp:Label ID="lblSubsidio" runat="server"
                Text='<%# Eval("Subsidio")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtSubsidio" runat="server"
            Text='<%# Eval("Subsidio")%>' onkeypress="return isDecimalKey(event)"></asp:TextBox>
    </EditItemTemplate>
    <FooterTemplate>
    <asp:TextBox ID="txtSubsidio" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
    </FooterTemplate>     
</asp:TemplateField>

<asp:TemplateField>   
    <FooterTemplate>
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click"></asp:Button>
    </FooterTemplate>
</asp:TemplateField>
<asp:CommandField  ShowEditButton="True" >
    <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
    </asp:CommandField>
</Columns>
<AlternatingRowStyle BackColor="#EDECEC"  />
</asp:GridView>
        </div>
    </div>
    </div>
    </div>
</asp:Content>

