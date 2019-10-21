<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmDatosClienteActulizar.aspx.cs" Inherits="EscenariosQnta.wfrmDatosClienteActulizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
            
    </style>
    <script type="text/javascript">
        var accordionItems = new Array();

        function init() {

            // Grab the accordion items from the page
            var divs = document.getElementsByTagName('div');
            for (var i = 0; i < divs.length; i++) {
                if (divs[i].className == 'accordionItem') accordionItems.push(divs[i]);
            }

            // Assign onclick events to the accordion item headings
            for (var i = 0; i < accordionItems.length; i++) {
                var h2 = getFirstChildWithTagName(accordionItems[i], 'H2');
                h2.onclick = toggleItem;
            }

            // Hide all accordion item bodies except the first
            for (var i = 1; i < accordionItems.length; i++) {
                accordionItems[i].className = 'accordionItem hide';
            }
        }


        function toggleItem() {
            var itemClass = this.parentNode.className;

            // Hide all items
            for (var i = 0; i < accordionItems.length; i++) {
                accordionItems[i].className = 'accordionItem hide';
            }

            // Show this item if it was previously hidden
            if (itemClass == 'accordionItem hide') {
                this.parentNode.className = 'accordionItem';

            }
        }

        function getFirstChildWithTagName(element, tagName) {
            for (var i = 0; i < element.childNodes.length; i++) {
                if (element.childNodes[i].nodeName == tagName) return element.childNodes[i];
            }
        }

        // **** Valida Letras *****//
        function isAlphabetKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97 || charCode == 32)) {

                return true;
            }
            return false;

        }

        //**** Valida Decimales ****//
       function isDecimalKey(e, field) {
            key = e.keyCode ? e.keyCode : e.which
            // backspace
            if (key == 8) return true
            // 0-9
            if (key > 47 && key < 58) {
                if (field.value == "") return true
                regexp = /.[0-9]{}$/
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

        }}

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--===================== Content ======================-->
   <div id="html">
        <div id="portfolio" >
            <div class="container_13"></div>  
        </div>       
        <div id="stars"></div>
        <div id="stars2"></div>
        <div id="srars3"></div>
            <div class="containerTitlePage ">
                <div class="titlePage">
                    CLIENTE
                </div>
            </div>
        </div>
        <div id="skills" >
  <div class="container_12">
  <div style="text-align:right;">
  <asp:Button runat="server" ID="btnRegresar" Text="<< Regresar" CssClass="btn" 
          onclick="btnRegresar_Click" />
  </div>

  DATOS GENERALES
  <div style="width:auto; border:2px Solid #4a1414;"></div>
  <div class="contenPanel">
      <table>
        <tr>            
           <td>Nombre / Razon Social: </td>  
           <td>Denominacion</td>         
           <td>Giro: </td>
        </tr>
        <tr>
            <td class="td"><asp:TextBox ID="txtNombre" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>                        
            <td class="td"><asp:TextBox ID="txtDenominacion" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>  
            <td class="td"><asp:TextBox ID="txtGiro" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>                
        </tr>
          <tr>           
           <td>Calle Num: </td>
           <td>Colonia:</td>
           <td>Delegacion: </td>
        </tr>
        <tr>
            <td class="td"><asp:TextBox ID="txtCalle" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>            
            <td class="td"><asp:TextBox ID="txtColonia" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>
            <td class="td"><asp:TextBox ID="txtDelegacion" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>                
        </tr>
          <tr>           
           <td>Entidad: </td>
           <td>CP:</td>
           <td>Pais: </td>
        </tr>
        <tr>
            <td class="td"><asp:DropDownList ID="ddlEntidad" runat="server" CssClass="cssDropdown" required></asp:DropDownList> </td>
            <td class="td"><asp:TextBox ID="txtCP" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>
            <td class="td"><asp:TextBox ID="txtPais" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>                
        </tr>
        <tr>   
            <td>Telefono:</td>                
            <td>Nombre Contacto:</td>                    
            <td>Correo Contacto:</td>
                        
        </tr>
        <tr>
            <td class="td"><asp:TextBox ID="txtTelefono" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>
            <td class="td"><asp:TextBox ID="txtNombreContacto" runat="server" Text="" CssClass="textbox" required></asp:TextBox></td>
            <td class="td"><asp:TextBox ID="txtCorreoContacto" runat="server" Text="" CssClass="textbox" required></asp:TextBox> </td>
                        
        </tr>    
        <tr>
          <td>Ejecutivo QNTA:</td> 
          <td>Notas:</td>        
        </tr>
        <tr>
            <td class="td"><asp:DropDownList ID="ddlEjecutivo" runat="server" CssClass="cssDropdown" required></asp:DropDownList> </td>
            <td class="td"><asp:TextBox ID="txtNotas" runat="server" Text="" CssClass="textbox"></asp:TextBox> </td>
        </tr>
       </table>
    </div>
    <div>
       DATOS NOMINA
       <div style="width:auto; border:2px Solid #4a1414;"></div>
       <div class="contenPanel">

       <body>
        <div class="accordionItem">
            <h2 >Tipo de Personal:</h2>
            <div>
                <asp:CheckBoxList ID="chkTipoPersonal" runat="server" CssClass="chkBox"></asp:CheckBoxList>
            </div>
        </div>
        <div class="accordionItem">
            <h2 >Periocidad Nomina:</h2>
            <div>
                    <asp:CheckBoxList ID="chkPeriocidadNomina" runat="server" CssClass="chkBox"></asp:CheckBoxList>                   
              </div>
        </div>
         <div class="accordionItem">
            <h2 >Prima de Riesgo:</h2>
            <div>
                <asp:CheckBoxList ID="chkPrimaRiesgo" runat="server" CssClass="chkBox"></asp:CheckBoxList>
              </div>
        </div>
         <div class="accordionItem">
            <h2 >Tipo Nomina Actual:</h2>
            <div>
                <asp:CheckBoxList ID="chkTipoNomina" runat="server" CssClass="chkBox"></asp:CheckBoxList>
              </div>
        </div>
        </body>

        </div>   
        </div>
        <div>
            <asp:Button ID="btnGuardar" Text="Guardar" runat="server"  CssClass="btn" 
                onclick="btnGuardar_Click"/>
        </div>
        <div>
            <asp:GridView runat="server" ID="grvEmpresa" CssClass="mGrid"></asp:GridView>
        </div>
    </div>
    </div>
    </div>
</asp:Content>
