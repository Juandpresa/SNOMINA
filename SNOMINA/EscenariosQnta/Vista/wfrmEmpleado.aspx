<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmEmpleado.aspx.cs" Inherits="EscenariosQnta.wfrmEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- CSS -->
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-datepicker.css" rel="stylesheet" type="text/css" />
    <!-- JS -->
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="../js/bootstrap.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".datepicker").datepicker({ format: 'yyyy/mm/dd', autoclose: true, todayBtn: 'linked' });

            $('#<%=chkAntiguedad.ClientID%>').click(function () {
                $(".panelAntiguedad").show();
                if (this.checked == false) {
                    $(".panelAntiguedad").hide();
                }
            });

            $('#btnAgregarBanco').click(function () {
                AgregarBanco();
                Limpiar();
            });

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

        //**** Valida Decimales ****//
        function isDecimalKey(e, field) {
            key = e.keyCode ? e.keyCode : e.which
            // backspace
            if (key == 8) return true
            // 0-9
            if ((key >= 47 && key <= 58)) {
                if (field.val == "") return true

                if (field.value.indexOf(".") != -1) {
                    //Decimales
                    regexp = /.[0-9]{5}$/
                }
                else {
                    //Enteros
                    regexp = /.[0-9]{}$/
                }

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

        function showContent() {
            var porcentaje = document.getElementById('ContentPlaceHolder1_divPorcentaje');
            var fijo = document.getElementById('ContentPlaceHolder1_divFijo');
            var reside = document.getElementById('ContentPlaceHolder1_cresidencia');

            var rb = document.getElementById("<%=rbtTipoEsquema.ClientID%>");
            var inputs = rb.getElementsByTagName('input');
            var flag = false;
            var selected;
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked) {
                    selected = inputs[i];
                    flag = true;
                    break;
                }
            }

            if (selected.value == "1") {
                //alert(selected.value);
                porcentaje.style.display = 'block';
                fijo.style.display = 'none';
                document.getElementById('ContentPlaceHolder1_txtSueldoBruto').value = 0;
                document.getElementById('ContentPlaceHolder1_txtSueldoNeto').value = 0;
                document.getElementById('ContentPlaceHolder1_txtSueldoHonorarios').value = 0;
                document.getElementById('ContentPlaceHolder1_txtSueldoTN').value = 0;
                document.getElementById('ContentPlaceHolder1_txtSueldoEZWallet').value = 0;
                //Card´s
                cresidencia.style.display = 'block';
                cdlaborales.style.display = 'block';
                cpago.style.display = 'block';
                cdcomplemento.style.display = 'none';
            }
            else {
                //alert('Please select an option');
                porcentaje.style.display = 'none';
                fijo.style.display = 'block';
                document.getElementById('ContentPlaceHolder1_txtNomina').value = 0;
                document.getElementById('ContentPlaceHolder1_txtAsimilados').value = 0;
                document.getElementById('ContentPlaceHolder1_txtHonorarios').value = 0;
                document.getElementById('ContentPlaceHolder1_txtTN').value = 0;
                document.getElementById('ContentPlaceHolder1_txtEZWallet').value = 0;
                document.getElementById('ContentPlaceHolder1_txtSueldo').value = 0;
                //Card´s
                cresidencia.style.display = 'block';
                cdlaborales.style.display = 'block';
                cpago.style.display = 'block';
                cdcomplemento.style.display = 'block';
            }
        }

        // document.getElementById("<%=rbtTipoEsquema.ClientID%>").required = true;

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="html">
        <div id="portfolio">
            <div class="container_13">
            </div>
        </div>

        <div class="containerTitlePage ">
            <div class="titlePage">
                EMPLEADO
            </div>
        </div>
    </div>
    <div id="skills">
        <%--ACORDION PARA LAS SECCIONES--%>
        <div class="accordion container" id="accordionExample">
            <div class="card">
                <div class="card-header" id="heading1">
                    <h2 class="mb-0">
                        <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapse1" aria-expanded="true" aria-controls="collapse1">
                            DATOS GENERALES
                        </button>
                    </h2>
                </div>

                <div id="collapse1" class="collapse show" aria-labelledby="heading1" data-parent="#accordionExample">
                    <div class="card-body">
                        <div class="container_12 container">
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                            <div class="contenPanel">
                                <table>
                                    <tr>
                                        <td>Empleadora:
                                        </td>
                                        <td>Cliente:
                                        </td>
                                        <td>Clave Empleado:
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlEmpleadora" runat="server" CssClass="cssDropdown" OnSelectedIndexChanged="ddlEmpleadora_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="cssDropdown" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <%--<td class="td">
                                <asp:DropDownList ID="ddlEscenario" runat="server" CssClass="cssDropdown" OnSelectedIndexChanged="ddlEscenario_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>--%>
                                        <td class="td">
                                            <asp:TextBox ID="txtIdentificador" runat="server" Text="" CssClass="textboxid" ReadOnly="true"></asp:TextBox>
                                            <asp:TextBox ID="txtClave" runat="server" Text="" CssClass="textboxcl" required="required"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Nombre:
                                        </td>
                                        <td>Apellido Paterno:
                                        </td>
                                        <td>Apellido Materno:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox ID="txtNombre" runat="server" Text="" CssClass="textbox" required="required"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtPaterno" runat="server" Text="" CssClass="textbox" required="required"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtMaterno" runat="server" Text="" CssClass="textbox" required="required"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Sexo:
                                        </td>
                                        <td>Fecha Nacimiento:
                                        </td>
                                        <td>Curp:
                                        </td>
                                    </tr>
                                    <tr>

                                        <td class="td">
                                            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtFechaNacimiento" runat="server" Text="" CssClass="datepicker"
                                                placeholder="dd/mm/yyyy" required="required"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtCurp" runat="server" Text="" CssClass="textbox" required="required" Style="text-transform: uppercase"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RFC:
                                        </td>
                                        <td>Correo:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox ID="txtRfc" runat="server" Text="" CssClass="textbox" required="required" Style="text-transform: uppercase"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtCorreo" runat="server" Text="" CssClass="textbox" required="required"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Telefono Movil:
                                        </td>
                                        <td>Telefono Fijo: 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox ID="txtTelefonoMovil" runat="server" Text="" CssClass="textbox" required="required" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtTelefonoLocal" runat="server" Text="" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <div style="width: auto; border: 2px Solid #4a1414;"></div>

                        </div>

                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header" id="heading2">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse2" aria-expanded="false" aria-controls="collapse2">
                            TIPO DE ALTA
                        </button>
                    </h2>
                </div>
                <div id="collapse2" class="collapse" aria-labelledby="heading2" data-parent="#accordionExample">
                    <div class="card-body">
                        <div class="container_12 container">
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                            <div class="contenPanel">
                                <table>
                                    <tr>
                                        <td class="td">
                                            <asp:RadioButtonList runat="server" ID="rbtTipoEsquema" CssClass="chkBox" onchange="javascript:showContent()"
                                                CausesValidation="True">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" id="cresidencia" style="display: none">
                <div class="card-header" id="heading3">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse3" aria-expanded="false" aria-controls="collapse3">
                            DATOS DE RESIDENCIA
                        </button>
                    </h2>
                </div>
                <div id="collapse3" class="collapse" aria-labelledby="heading3" data-parent="#accordionExample">
                    <div class="card-body">
                        <div class="container_12 container">
                            <div style="visibilitywidth: auto; border: 2px Solid #4a1414;">
                            </div>
                            <div class="contenPanel">
                                <table>
                                    <tr>
                                        <td>Calle:
                                        </td>
                                        <td>Número:
                                        </td>
                                        <td>Colonia:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox ID="txtCalle" runat="server" Text="" CssClass="textbox" required="true"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtNumero" runat="server" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtColonia" runat="server" Text="" CssClass="textbox" required="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Estado:
                                        </td>
                                        <td>Ciudad/Alcaldia:
                                        </td>
                                        <td>CP:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtCiudadDel" runat="server" Text="" CssClass="textbox" required="true"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtCP" runat="server" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" id="cdlaborales" style="display: none">
                <div class="card-header" id="heading4">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse4" aria-expanded="false" aria-controls="collapse4">
                            DATOS LABORALES
                        </button>
                    </h2>
                </div>
                <div id="collapse4" class="collapse" aria-labelledby="heading4" data-parent="#accordionExample">
                    <div class="card-body">
                        <div class="container_12 container">
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                            <div class="contenPanel">
                                <table>
                                    <tr>
                                        <td>Puesto:
                                        </td>
                                        <td>Descripcion Puesto:
                                        </td>
                                        <td>Fecha Ingreso:
                                        </td>
                                    </tr>
                                    <tr>

                                        <td class="td">
                                            <asp:DropDownList ID="ddlClasificacionEmpleado" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtFechaIngreso" runat="server" Text="" CssClass="datepicker" required="required"
                                                placeholder="dd/mm/yyyy"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--<td>Prestacion:
                                        </td>--%>
                                        <td>Tipo de Contrato:
                                        </td>
                                        <td>Empleado de Construccion:
                                        </td>
<%--                                        <td>Ubicacion Laboral:
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <%--<td class="td">
                                            <asp:DropDownList ID="ddlPrestacion" runat="server" CssClass="cssDropdown" required="required">
                                            </asp:DropDownList>
                                        </td>--%>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlContrato" runat="server" CssClass="cssDropdown" required="required">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkObra" runat="server" CssClass="chkBox" />
                                        </td>
<%--                                        <td class="td">
                                            <asp:TextBox ID="txtUbicacionLaboral" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                                        </td>--%>
                                    </tr>

                                </table>
                            </div>
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" id="cpago" style="display: none">
                <div class="card-header" id="heading5">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse5" aria-expanded="false" aria-controls="collapse5">
                            DATOS DE PAGO
                        </button>
                    </h2>
                </div>
                <div id="collapse5" class="collapse" aria-labelledby="heading5" data-parent="#accordionExample">
                    <div class="card-body">
                        <div class="container_12 container">
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>

                            <div class="contenPanel">
                                <table>
                                    <tr>
                                        <td>Banco:
                                        </td>
                                        <td>Cuenta:
                                        </td>
                                        <td>CLABE:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlBanco" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtCuenta" runat="server" CssClass="textbox" FilterType="Numbers" onkeypress="return isDecimalKey(event, this);">
                                            </asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtClabe" runat="server" CssClass="textbox" FilterType="Numbers" onkeypress="return isDecimalKey(event, this);">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>No. Tarjeta:
                                        </td>
                                        <td>Sueldo Neto:
                                        </td>
                                        <td>Periodo de Pago:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox ID="txtTarjeta" runat="server" CssClass="textbox" onkeypress="return isDecimalKey(event, this);">
                                            </asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtSueldoNeto" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlPeriodoPago" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div>
                                    <button type="button" class="btn btn-success" id="btnAgregarBanco">Agregar</button>
                                    <br />
                                    <br />
                                </div>

                                <table id="tblBancos" class="table table-bordered" runat="server">
                                    <thead>
                                        <tr>
                                            <th>Banco</th>
                                            <th>Cuenta</th>
                                            <th>CLABE</th>
                                            <th>No. Tarjeta</th>
                                            <th style="visibility: hidden">IdBanco:
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>

                            </div>
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card" id="cdcomplemento" style="display: none">
                <div class="card-header" id="heading6">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse6" aria-expanded="false" aria-controls="collapse6">
                            DATOS COMPLEMENTARIOS
                        </button>
                    </h2>
                </div>
                <div id="collapse6" class="collapse" aria-labelledby="heading6" data-parent="#accordionExample">
                    <div class="card-body">
                        <div class="container_12 container">
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                            <div class="contenPanel">
                                <table>
                                    <tr>
                                        <td>Grado de Estudio:
                                        </td>
                                        <td>Institución:
                                        </td>
                                        <td>Carrera:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlNivelE" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlInstitucion" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div class="container_12">
                                DATOS LABORALES
                                            <div style="width: auto; border: 2px Solid #4a1414;">
                                            </div>
                            </div>
                            <div class="contenPanel">
                                <div runat="server" id="divPorcentaje" style="display: none;">
                                    <table>
                                        <tr>
                                            <td>Porcentaje Nomina:
                                            </td>
                                            <td>Sueldo:
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtNomina" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSueldo" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Porcentaje Asimilados:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtAsimilados" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Porcentaje Honorarios:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtHonorarios" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Porcentaje TN:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtTN" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Porcentaje EZ Wallet:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtEZWallet" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div runat="server" id="divFijo" style="display: none;">
                                    <table>
                                        <tr>
                                            <td>Sueldo Bruto:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoBruto" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Sueldo Honorarios
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoHonorarios" runat="server" Text="0" CssClass="textbox"
                                                    onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sueldo TN:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoTN" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sueldo EZ Wallet:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoEZWallet" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table>
                                    <tr>
                                        <td>Bono
                                        </td>
                                        <td>Comision
                                        </td>
                                        <td>Otros Ingresos
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtBono" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtComisionEmp" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOtrosIngresos" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="container_12 container">
                            PAGOS
                <div style="width: auto; border: 2px Solid #4a1414;">
                </div>
                            <div class="contenPanel">
                                <table>
                                    <tr>
                                        <td>Razon Social Pagadora:
                                        </td>
                                        <td>Tipo Pago:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlPagadora" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlTipoPago" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Esquema Actual:
                                        </td>
                                        <td>Fecha Ultimo Pago:
                                        </td>
                                        <td></td>
                                        <td>
                                            <div class="panelAntiguedad" style="display: none">
                                                <asp:Label Text="Antiguedad (Años)" ID="lblAntiguedad" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlEsquemaActual" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtUltimoPago" runat="server" Text="" CssClass="datepicker" required="required"
                                                placeholder="dd/mm/yyyy"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkAntiguedad" CssClass="chkBox" CausesValidation="True" Text="Presenta Antiguedad" OnCheckedChanged="chkAntiguedad_CheckedChanged"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <div class="panelAntiguedad" style="display: none">
                                                <asp:TextBox runat="server" value="0" ID="txtAntiguedad" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <div class="container_12 container">
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                            <div class="contenPanel ">
                                <table>
                                    <tr>
                                        <td>Importe Fonacot:
                                        </td>
                                        <td>Infonavit:
                                        </td>
                                        <td>Importe Infonavit:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtImporteFonacot" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlInfonavit" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtImporteInfonavit" runat="server" Text="0" CssClass="textbox"
                                                onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Pension Alimenticia:
                                        </td>
                                        <td>Importe Pension:
                                        </td>
                                        <td>Prima de Riesgo:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlPension" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtImportePension" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlPrimaRiesgo" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
             <div class="card">
                <div class="card-header" id="heading2">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse2" aria-expanded="false" aria-controls="collapse2">
                            DATOS DE OBRA
                        </button>
                    </h2>
                </div>
                <div id="collapse2" class="collapse" aria-labelledby="heading2" data-parent="#accordionExample">
                    <div class="card-body">
                        <div class="container_12 container">
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                            <div class="contenPanel">
                                <table>
                                    <tr>
                                        <td class="td">
                                            <asp:RadioButtonList runat="server" ID="RadioButtonList1" CssClass="chkBox" onchange="javascript:showContent()"
                                                CausesValidation="True">
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btnG" OnClick="btnGuardar_Click" />
            </div>


        </div>
    </div>
    <script type="text/javascript">
        function AgregarBanco() {

            //Agregamos datos de pago
            var idb = $("#<%=ddlBanco.ClientID%>").find('option:selected').val();   
            var banco = $("#<%=ddlBanco.ClientID%>").find('option:selected').text();
            var cuenta = $("#<%=txtCuenta.ClientID%>").val();
            var clabe = $("#<%=txtClabe.ClientID%>").val();
            var tarjeta = $("#<%=txtTarjeta.ClientID%>").val();
            //Agregamos pago
            var numbanco = numbanco + 1;
            //Agregamos el banco a la tabla
            $("#<%=tblBancos.ClientID%>").append(
                "<tr>" +
                "<td>" +
                banco +
                "</td>" +
                "<td>" +
                cuenta +
                "</td>" +
                "<td>" +
                clabe +
                "</td>" +
                "<td>" +
                tarjeta +
                "</td>" +
                "<td style='visibility: hidden'>" +
                idb +
                "</td>" +
                "</tr>"
            );
        }

        function Limpiar() {
            var cuenta = $("#<%=txtCuenta.ClientID%>").val(' ');
            var clabe = $("#<%=txtClabe.ClientID%>").val(' ');
            var tarjeta = $("#<%=txtTarjeta.ClientID%>").val(' ');
        }
    </script>
</asp:Content>
<%--<td>Nacionalidad:
</td>
<td class="td">
    <asp:TextBox ID="txtNacionalidad" runat="server" Text="Mexicana" CssClass="textbox"> </asp:TextBox>
</td>
--%>
