﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
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
            $('#<%=chkObra.ClientID%>').click(function () {
                $("#cdobra").show();
                if (this.checked == false) {
                    $("#cdobra").hide();
                }
            });

            $("#<%=tblBancos.ClientID%>").on('click', '.delete', function () {
                $(this).closest('tr').remove();
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
            //var porcentaje = document.getElementById('ContentPlaceHolder1_divPorcentaje');
            //var fijo = document.getElementById('ContentPlaceHolder1_divFijo');
            //var reside = document.getElementById('ContentPlaceHolder1_cresidencia');
            var esimss = document.getElementById('ContentPlaceHolder1_divimss');
            var esasam = document.getElementById('ContentPlaceHolder1_divasam');
            var opesq = $('#<%=ddlEsquemas.ClientID%>').value();
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
            if (opesq == 1) {
                esimss.style.display = 'block';
            }
            if (selected.value == "1") {
                //alert(selected.value);
                document.getElementById('ContentPlaceHolder1_txtSueldoBruto').value = 0;
                document.getElementById('ContentPlaceHolder1_txtSueldoNeto').value = 0;
                document.getElementById('ContentPlaceHolder1_txtSueldoHonorarios').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtSueldoTN').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtSueldoEZWallet').value = 0;
                //Card´s
                cresidencia.style.display = 'block';
                cdlaborales.style.display = 'block';
                cpago.style.display = 'block';
                cdcomplemento.style.display = 'none';
            }
            else {
                //alert('Please select an option');
                //porcentaje.style.display = 'block';
                //fijo.style.display = 'block';
                //document.getElementById('ContentPlaceHolder1_txtNomina').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtAsimilados').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtHonorarios').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtTN').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtEZWallet').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtSueldo').value = 0;
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
                                            <asp:TextBox ID="txtClabe" runat="server" CssClass="textbox" MaxLength="18" FilterType="Numbers" onkeypress="return isDecimalKey(event, this);">
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
                                            <asp:TextBox ID="txtTarjeta" runat="server" MaxLength="16" CssClass="textbox" onkeypress="return isDecimalKey(event, this);">
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
                                <%--<asp:Table ID="tblBancos"
                                    Font-Size="Large"
                                    Width="550"
                                    ForeColor="#000"
                                    CellPadding="5"
                                    CellSpacing="5"
                                    GridLines="Both"
                                    runat="server">
                                    <asp:TableHeaderRow
                                        runat="server"
                                        ForeColor="#000"
                                        Font-Bold="true"
                                        BorderStyle="Solid"
                                        BorderWidth="2"
                                        GridLines="Both">
                                        <asp:TableHeaderCell>Banco</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Cuenta</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>CLABE</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>No. Tarjeta</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Prioridad</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Borrar</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>IdBanco</asp:TableHeaderCell>
                                    </asp:TableHeaderRow>
                                    <asp:TableRow>
                                        <asp:TableCell>1</asp:TableCell>
                                        <asp:TableCell>1</asp:TableCell>
                                        <asp:TableCell>1</asp:TableCell>
                                        <asp:TableCell>1</asp:TableCell>
                                        <asp:TableCell>1</asp:TableCell>
                                        <asp:TableCell>1</asp:TableCell>
                                        <asp:TableCell>1</asp:TableCell>

                                    </asp:TableRow>
                                </asp:Table>--%>
                                <table id="tblBancos" class="table table-bordered" runat="server">
                                    <thead>
                                        <tr>
                                            <th>Banco</th>
                                            <th>Cuenta</th>
                                            <th>CLABE</th>
                                            <th>No. Tarjeta</th>
                                            <th>Prioridad</th>
                                            <th>Borrar</th>
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
            <div class="card" id="cdcomplemento" style="display: block">
                <div class="card-header" id="heading6">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse6" aria-expanded="false" aria-controls="collapse6">
                            DATOS COMPLEMENTARIOS</button>
                    </h2>
                </div>
                <div id="collapse6" class="collapse" aria-labelledby="heading6" data-parent="#accordionExample">
                    <div class="card-body">
                        <div class="container_12 container">
                            Datos Escolares
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
                                <div style="width: auto; border: 2px Solid #4a1414;">
                                </div>
                                DATOS LABORALES
                                            <div style="width: auto; border: 2px Solid #4a1414;">
                                            </div>
                            </div>
                            <div class="contenPanel">
                                <table>
                                    <tr class="td">
                                        <td colspan="2" align="center">Esquema:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td" colspan="2" align="center">
                                            <asp:DropDownList ID="ddlEsquemas" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div runat="server" id="divimss" style="display: none">
                                    <table>
                                        <tr>
                                            <td>Porcentaje(%):</td>
                                        </tr>
                                        <tr>
                                        <td><asp:TextBox ID="txtPorcentajeIMSS" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Sueldo Bruto:
                                            </td>
                                            <td>Sueldo Neto:
                                            </td>
                                            <td>Sueldo Diario:
                                            </td>
                                            <td>Sueldo Diario Integrado:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoBruto" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                            <td class="td">
                                                <asp:TextBox ID="SueldoNetoC" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoDiario" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoDI" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div runat="server" id="divasam" style="display: none">
                                    <table>
                                        <tr>
                                            <td>Porcentaje (%):
                                            </td>
                                            <td>Sueldo Neto:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoNetoI" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                            <td class="td">
                                                <asp:TextBox ID="txtPorcentajeASAM" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div runat="server" id="divhonorarios" style="display: none">
                                    <table>
                                        <tr>
                                            <td>Porcentaje (%):
                                            </td>
                                            <td>Sueldo Honorarios:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtParocentajeHonorarios" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoHonorarios" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div runat="server" id="divpublicidad" style="display: none">
                                    <table>
                                        <tr>
                                            <td>Porcentaje (%):
                                            </td>
                                            <td>Sueldo Publicidad:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtPorcentajePubli" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoPubli" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                        </div>
                        <div class="container_12 container">
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
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
                                        <td>
                                            <div class="panelAntiguedad" style="display: none">
                                                <asp:Label Text="Antiguedad (Años)" ID="lblAntiguedad" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
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
                            DATOS ADICIONALES 
                             <div style="width: auto; border: 2px Solid #4a1414;">
                             </div>
                            <div class="contenPanel ">
                                <table>
                                    <tr>
                                        <td>Prima de Riesgo:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlPrimaRiesgo" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
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
            <div class="card cdobra" id="cdobra" style="display: none">
                <div class="card-header" id="heading7">
                    <h2 class="mb-0">
                        <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapse7" aria-expanded="false" aria-controls="collapse7">
                            DATOS DE OBRA
                        </button>
                    </h2>
                </div>
                <div id="collapse7" class="collapse" aria-labelledby="heading7" data-parent="#accordionExample">
                    <div class="card-body">
                        <div class="container_12 container">
                            <div style="width: auto; border: 2px Solid #4a1414;">
                            </div>
                            <div class="contenPanel">
                                <table>
                                    <tr>
                                        <td>Centro de Costos:
                                        </td>
                                        <td>Fecha Inicio de Obra:
                                        </td>
                                        <td>Fecha Fin de Obra:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlCCostos" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>

                                        <td class="td">
                                            <asp:TextBox ID="txtFIObra" runat="server" Text="" CssClass="datepicker"
                                                placeholder="dd/mm/yyyy" required="required"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtFFObra" runat="server" Text="" CssClass="datepicker"
                                                placeholder="dd/mm/yyyy" required="required"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>Dias Totales de Obra:
                                        </td>
                                        <td>Tipo de Obra:
                                        </td>
                                        <td>Ubicación:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox ID="txtDiasTobra" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtTObra" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtUbicacionO" TextMode="MultiLine" runat="server" Text="" CssClass="textbox"></asp:TextBox>
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
                <br />
                <br />
                <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btnG" OnClick="btnGuardar_Click" />
            </div>

            <input type="text" name="numbanco" id="numbanco" value="0" />
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
            var numb = parseInt($("#numbanco").val()) + 1;
            //Agregamos el banco a la tabla
            $("#<%=tblBancos.ClientID%>").append(

                "<tr>" +
                "<td><input type='text' style='border:0;background:transparent;' name='bancotxt'" + numb + " id='bancotxt'" + numb + " readonly value='" + banco + "'/>" +
              "</td>" +
              "<td>" +
              "<input type='text' style='border:0;background:transparent;'" +
              " name='cuentaid" + numb + "'" +
                " id='cuentaid" + numb + "'" +
                " value='" + cuenta + "'/>" +
              "</td>" + "<td>" +
              "<input type='text' style='border:0;background:transparent;'" +
              " name='clabeid" + numb + "'" +
              " id='clabeid" + numb + "'" +
                " readonly " +
                " value='" + clabe + "'/>" +
              "</td>" +"<td>" +
              "<input type='text' style='border:0;background:transparent;'" +
              " name='clabeid" + numb + "'" +
              " id='clabeid" + numb + "'" +
                " readonly " +
                " value='" + tarjeta + "'/>" +
              "</td>" + "<td align='center'>" +
              "<input type='checkbox' ID='Prioridad' class='checkbox'>" +
              "</td>" +"<td align='center'>" +
              "<button type='button' ID='delete' class='delete btn btn-danger' href='#'>X</i></button>" +
              "</td>" +
              "<td style='visibility: hidden'>" +
              "<input type='text' style='border:0;background:transparent;'" +
              " name='idTipoPago" + numb + "'" +
              " id='idTipoPago" + numb + "'" +
                " readonly " +
                " value='" + idb + "'/>" +
              "</td>" +
              "</tr>"
            
                //"<tr>" +
                //"<td>" +
                //banco +
                //"</td>" +
                //"<td>" +
                //cuenta +
                //"</td>" +
                //"<td>" +
                //clabe +
                //"</td>" +
                //"<td>" +
                //tarjeta +
                //"</td>" +
                //"<td align='center'>" +
                //"<input type='checkbox' ID='Prioridad' class='checkbox'>" +
                //"</td>" +
                //"<td align='center'>" +
                //"<button type='button' ID='delete' class='delete btn btn-danger' href='#'>X</i></button>" +
                //"</td>" +
                //"<td style='visibility: hidden'>" +
                //idb +
                //"</td>" +
                //"</tr>"
            );
             $('#numbanco').val(numb);
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
