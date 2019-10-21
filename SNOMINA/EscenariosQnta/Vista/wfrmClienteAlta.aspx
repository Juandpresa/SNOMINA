<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmClienteAlta.aspx.cs" Inherits="EscenariosQnta.wfrmClienteAlta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-latest.js" type="text/jscript"></script>
    <script src="../js/jquery.maskedinput-1.3.1.min_.js" type="text/jscript"></script>
    <meta charset="utf-8" />
    <script type="text/javascript">

        $(function () {
            //Define your mask
            $('#ContentPlaceHolder1_txtTelefono').mask('(999) 999-9999');
        });

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


        // **** Valida Numeros *****//
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 57 && charCode >= 48)) {

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

        }

        //        function validate() {
        //            if (document.getElementById("ddlList").value == "") {
        //                alert("Please select value"); // prompt user
        //                document.getElementById("ddlList").focus(); //set focus back to control
        //                return false;
        //            }
        //        }


        // Funcion para validar campos vacios *Pendinete por revisar
        window.addEventListener("load", crearValidacion, false);
        function crearValidacion() {
            var btn = document.getElementById("btnEnviar");
            btn.addEventListener("click", validarDatos, false);
            function validarDatos(e) {
                if (validarCampo("txtNombre", "Ingresa el Nombre", e) == false) return false;
                if (validarCampo("cboCurso", "Seleccione el Curso", e) == false) return false;
                return true;
            }
            function validarCampo(idControl, mensaje, e) {
                var campo = document.getElementById(idControl);
                if (campo != null) {
                    if (campo.value == "") {
                        alert(mensaje);
                        campo.focus();
                        e.preventDefault();
                        return false;
                    }
                }
                else return false;
                return true;
            }
        }

       

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--===================== Content ======================-->
        <div id="html">
            <div id="portfolio">
                <div class="container_13">
                </div>
            </div>
            <div id="stars">
            </div>
            <div id="stars2">
            </div>
            <div id="srars3">
            </div>
            <div class="containerTitlePage ">
                <div class="titlePage">
                    <center>
                        CLIENTE ALTA
                    </center>
                </div>
            </div>
        </div>
        <div id="skills">
            <div class="container_12">
                DATOS GENERALES
                <div style="width: auto; border: 2px Solid #4a1414;">
                </div>
                <div class="contenPanel">
                    <table>
                        <tr>
                            <td>
                                Nombre / Razon Social:
                            </td>
                            <td>
                                Denominacion
                            </td>
                            <td>
                                Giro:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtNombre" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtDenominacion" runat="server" Text="" CssClass="textbox" ></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtGiro" runat="server" Text="" CssClass="textbox" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Calle Num:
                            </td>
                            <td>
                                Colonia:
                            </td>
                            <td>
                                Delegacion:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtCalle" runat="server" Text="" CssClass="textbox" ></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtColonia" runat="server" Text="" CssClass="textbox" ></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtDelegacion" runat="server" Text="" CssClass="textbox" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Entidad:
                            </td>
                            <td>
                                CP:
                            </td>
                            <td>
                                Pais:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtCP" runat="server" Text="" CssClass="textbox"  onkeypress="return isNumberKey(event)"
                                    MaxLength="5"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtPais" runat="server" Text="México" CssClass="textbox" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Telefono:
                            </td>
                            <td>
                                Nombre Contacto:
                            </td>
                            <td>
                                Correo Contacto:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="textbox" Text=""
                                    placeholder="(___) ___-____"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtNombreContacto" runat="server" Text="" CssClass="textbox" ></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtCorreoContacto" runat="server" Text="" CssClass="textbox" 
                                    placeholder="example@qnta.com"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ejecutivo QNTA:
                            </td>
                            <td>
                                Socio:
                            </td>
                            <td>
                                Asociado:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:DropDownList ID="ddlEjecutivo" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlSocio" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlAsociado" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Notas:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtNotas" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    DATOS NOMINA
                    <div style="width: auto; border: 2px Solid #4a1414;">
                    </div>
                    <div class="contenPanel">
                        <body>
                            <div class="accordionItem">
                                <h2>
                                    Tipo de Personal:</h2>
                                <div>
                                    <asp:CheckBoxList ID="chkTipoPersonal" runat="server" CssClass="chkBox">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="accordionItem">
                                <h2>
                                    Periocidad Nomina:</h2>
                                <div>
                                    <%--<asp:CheckBoxList ID="chkPeriocidadNomina" runat="server" CssClass="chkBox">
                                    </asp:CheckBoxList>--%>
                                    <asp:RadioButtonList ID="chkPeriocidadNomina" runat="server" CssClass="chkBox">
                                    </asp:RadioButtonList>
                                    <%-- <asp:TextBox runat="server" ID="txtOtro" Text="" Enabled="false" CssClass="textbox"></asp:TextBox>--%>
                                </div>
                            </div>
                            <div class="accordionItem">
                                <h2>
                                    Prima de Riesgo:</h2>
                                <div>
                                  <%--  <asp:CheckBoxList ID="chkPrimaRiesgo" runat="server" CssClass="chkBox">
                                    </asp:CheckBoxList>--%>
                                    <asp:RadioButtonList ID="chkPrimaRiesgo" runat="server" CssClass="chkBox">
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="accordionItem">
                                <h2>
                                    Nomina Actual:</h2>
                                <div>
                                    <asp:CheckBoxList ID="chkTipoNomina" runat="server" CssClass="chkBox">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                        </body>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>
               <%-- <div style="overflow: auto; height: 260px;">
                    <asp:GridView runat="server" ID="grvEmpresa" CssClass="mGrid">
                    </asp:GridView>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
