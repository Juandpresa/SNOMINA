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
            $('#<%=chkObra.ClientID%>').click(function () {
                $("#cdobra").show();
                if (this.checked == false) {
                    $("#cdobra").hide();
                }
            });


            $('#<%=txtFFObra.ClientID%>').on('change', function () {

                var f1 = $('#<%=txtFIObra.ClientID%>').val();
                var f2 = $('#<%=txtFFObra.ClientID%>').val();
                var s1 = formato(f1);
                var s2 = formato(f2);
                var res = restaFechas(s1, s2);
                if (f1 != "") {
                    $('#<%=txtDiasTobra.ClientID%>').val(res);
                }

            });

            $('#<%=txtFIObra.ClientID%>').on('change', function () {

                var f1 = $('#<%=txtFIObra.ClientID%>').val();
                var f2 = $('#<%=txtFFObra.ClientID%>').val();
                var s1 = formato(f1);
                var s2 = formato(f2);
                var res = restaFechas(s1, s2);
                if (f2 != "") {
                    $('#<%=txtDiasTobra.ClientID%>').val(res);
                }

            });

        });

        function formato(texto) {
            return texto.replace(/^(\d{4})[/](\d{2})[/](\d{2})$/g, '$3/$2/$1');
        }

        restaFechas = function (f1, f2) {
            var aFecha1 = f1.split('/');
            var aFecha2 = f2.split('/');
            var fFecha1 = Date.UTC(aFecha1[2], aFecha1[1] - 1, aFecha1[0]);
            var fFecha2 = Date.UTC(aFecha2[2], aFecha2[1] - 1, aFecha2[0]);
            var dif = fFecha2 - fFecha1;
            var dias = Math.floor(dif / (1000 * 60 * 60 * 24));
            return dias;
        }

        function showimagepreview(input) {

            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {

                    document.getElementsByTagName("img")[0].setAttribute("src", e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

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
                //document.getElementById('ContentPlaceHolder1_txtSueldoBruto').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtSueldoNeto').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtSueldoHonorarios').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtSueldoTN').value = 0;
                //document.getElementById('ContentPlaceHolder1_txtSueldoEZWallet').value = 0;
                //Card´s
                $(".panelSueldo").show();
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
                $(".panelSueldo").hide();
            }
        }
        function MostrarEsq() {
            var txtpors = document.getElementById('ContentPlaceHolder1_divPorSueldo');
            var listaEsq = $('#<%=ddlEsquemas.ClientID%>').find('option:selected').val();
            if (listaEsq == 1) {
                txtpors.style.display = 'block';
                document.getElementById('ContentPlaceHolder1_txtPorcentaje').readOnly = true;
                $('.mix').hide();
            }
            if (listaEsq == 2) {
                $('.mix').show();
                txtpors.style.display = 'none';
            }
            if (listaEsq >= 3) {
                txtpors.style.display = 'block';
                document.getElementById('ContentPlaceHolder1_txtPorcentaje').readOnly = true;
                $('.mix').hide();
            }
        }
        function MostrarEsqM() {
            var txtpors = document.getElementById('ContentPlaceHolder1_divPorSueldo');
            var listaEsq = $('#<%=ddlEMixto.ClientID%>').find('option:selected').val();
            if (listaEsq == 1) {
                txtpors.style.display = 'block';
                document.getElementById('ContentPlaceHolder1_txtPorcentaje').readOnly = false;
            }

            if (listaEsq >= 3) {
                txtpors.style.display = 'block';
                document.getElementById('ContentPlaceHolder1_txtPorcentaje').readOnly = false;
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

                            <div class="contenPanel row">
                                <table>
                                    <tr>
                                        <td></td>
                                        <td valign="top" align="center">
                                            <img id="img" alt="" style="width: 135px; height: 160px" src="../image/usuario.png" />

                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td align="center">
                                            <br />
                                            <asp:FileUpload ID="fileFoto" runat="server" onchange="showimagepreview(this)" CssClass="btn btn-info" />
                                            <br />
                                            <br />
                                        </td>
                                        <td></td>
                                    </tr>

                                    <tr>
                                        <td>Empleadora:
                                        </td>
                                        <td>Cliente:
                                        </td>
                                        <td>Clave Empleado:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlEmpleadora" runat="server" CssClass="cssDropdown" OnSelectedIndexChanged="ddlEmpleadora_SelectedIndexChanged"
                                                AutoPostBack="true" required="required">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCliente" runat="server" CssClass="cssDropdown" OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged"
                                                AutoPostBack="true" required="required">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
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
                                            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="cssDropdown" required="required">
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
                                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td class="td">
                                                    <asp:RadioButtonList runat="server" ID="rbtTipoEsquema" CssClass="chkBox" onchange="javascript:showContent()" CausesValidation="True" AutoPostBack="True" OnSelectedIndexChanged="rbtTipoEsquema_SelectedIndexChanged" required="required">
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                            DATOS DE RESIDENCIA</button>
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
                                            <asp:TextBox ID="txtCalle" runat="server" Text="" CssClass="textbox" required="required"></asp:TextBox>
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
                                            <asp:DropDownList ID="ddlEntidad" runat="server" CssClass="cssDropdown" required="required">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtCiudadDel" runat="server" Text="" CssClass="textbox" required="required"></asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtCP" runat="server" CssClass="textbox" onkeypress="return isDecimalKey(event, this);" required="required"></asp:TextBox>
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
                                        <td>Departamento:
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
                                            <asp:TextBox ID="txtDepto" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Fecha Ingreso:
                                        </td>
                                        <td>Tipo de Contrato:
                                        </td>
                                        <td>Dias de Contrato:
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox ID="txtFechaIngreso" runat="server" Text="" CssClass="datepicker" required="required"
                                                placeholder="dd/mm/yyyy"></asp:TextBox>
                                        </td>

                                        <td class="td">
                                            <asp:DropDownList ID="ddlContrato" runat="server" CssClass="cssDropdown" required="required">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtDiasC" runat="server" Text="" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Empleado de Construccion:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkObra" runat="server" CssClass="chkBox" />
                                        </td>

                                    </tr>
                                </table>
                                <br />

                                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td>Turno:
                                                </td>
                                                <td>Jornada:
                                                </td>
                                                <td>Horario:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td">
                                                    <asp:DropDownList ID="ddlTurno" runat="server" CssClass="cssDropdown">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td">
                                                    <asp:DropDownList ID="ddlJornada" runat="server" CssClass="cssDropdown">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="td">
                                                    <asp:DropDownList ID="ddlHorario" runat="server" CssClass="cssDropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                        </table>
                                        <br />
                                        <asp:GridView ID="grdHorario2" runat="server" CssClass="table table-responsive-sm" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Dia">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGDia" runat="server" Text='<%# Eval("Dia") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Entrada">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGEntrada" runat="server" Text='<%# Eval("Entrada") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Salida">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGSalida" runat="server" Text='<%# Eval("Salida") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                            DATOS DE PAGO</button>
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
                                        <td>Periodo de Pago:
                                        </td>
                                        <td>
                                            <div class="panelSueldo" style="display: none">
                                                <asp:Label Text="Sueldo Neto:" ID="SNeto" runat="server" />
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:TextBox ID="txtTarjeta" runat="server" MaxLength="16" CssClass="textbox" onkeypress="return isDecimalKey(event, this);">
                                            </asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlPeriodoPago" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>

                                        <td class="td">
                                            <div class="panelSueldo" style="display: none">
                                                <asp:TextBox ID="txtSueldoNeto" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSBruto" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div>
                                            <asp:Button ID="btbAddTarjeta" runat="server" Text="Agregar" UseSubmitBehavior="False" OnClick="btbAddTarjeta_Click" CssClass="btn btn-success" />
                                            <br />
                                            <br />
                                        </div>
                                        <asp:GridView ID="grd" runat="server" CssClass="table table-responsive-sm" DataKeyNames="Banco" AutoGenerateColumns="false" AutoGenerateDeleteButton="True" OnRowDeleting="grd_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Banco">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGBanco" runat="server" Text='<%# Eval("Banco") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Cuenta">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGCuenta" runat="server" Text='<%# Eval("Cuenta") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Clabe">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGClabe" runat="server" Text='<%# Eval("Clabe") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Tarjeta">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGTarjeta" runat="server" Text='<%# Eval("Tarjeta") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Prioridad">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkGPrioridad" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="IdBanco" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGIdBanco" runat="server" Text='<%# Eval("IdBanco") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

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
                                    <tr>
                                        <td>Razon Social Pagadora:
                                        </td>
                                        <td>Tipo Pago:
                                        </td>
                                        <td>Prestaciones:
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
                                        <td class="td">
                                            <asp:DropDownList ID="ddlPrestacion" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <div class="panelAntiguedad" style="display: none">
                                                <asp:Label Text="Antiguedad" ID="lblAntiguedad" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkAntiguedad" CssClass="chkBox" CausesValidation="True" Text="Presenta Antiguedad" OnCheckedChanged="chkAntiguedad_CheckedChanged"></asp:CheckBox>
                                        </td>
                                        <td>
                                            <div class="panelAntiguedad" style="display: none">
                                                <asp:TextBox ID="txtAntiguedad" runat="server" Text="" CssClass="datepicker" placeholder="dd/mm/yyyy"></asp:TextBox>
                                            </div>

                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td align="center">Esquema:
                                        </td>
                                        <td align="center">
                                            <div class="mix" style="display: none">
                                                <asp:Label Text="Mixto:" ID="Mixto" runat="server" />
                                            </div>
                                        </td>
                                        <td>
                                            <div class="mix" style="display: none">
                                                <asp:Label Text="Sueldo Total:" ID="SMixto" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="td" align="center">
                                            <asp:DropDownList ID="ddlEsquemas" runat="server" CssClass="cssDropdown" onchange="javascript:MostrarEsq()">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <div class="mix" style="display: none">
                                                <asp:DropDownList ID="ddlEMixto" runat="server" CssClass="cssDropdown" onchange="javascript:MostrarEsqM()">
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="mix" style="display: none">
                                                <asp:TextBox ID="txtST" runat="server" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div runat="server" id="divPorSueldo" style="display: none">
                                    <table>
                                        <tr>
                                            <td>Porcentaje(%):
                                            </td>
                                            <td>Sueldo Neto:
                                            </td>
                                            <td>Sueldo Bruto:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:TextBox ID="txtPorcentaje" runat="server" Text="100" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldo" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);" AutoPostBack="False"></asp:TextBox>
                                            </td>
                                            <td class="td">
                                                <asp:TextBox ID="txtSueldoBruto" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <br />
                                                        <asp:Button ID="btnEsquema" runat="server" Text="Agregar" UseSubmitBehavior="False" CssClass="btn btn-info" OnClick="btnEsquema_Click" />
                                                    </td>
                                                    <td class="td" style="text-align: right">
                                                        <br />
                                                        <br />
                                                        Total (%):
                                                    </td>
                                                    <td></td>
                                                    <td align="center">
                                                        <br />
                                                        <br />
                                                        &nbsp;&nbsp;<asp:TextBox ID="txtTotalE" ReadOnly="true" runat="server" Text="0" CssClass="textbox" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:GridView ID="grdEsquemas" runat="server" CssClass="table table-responsive-sm" DataKeyNames="Esquema" AutoGenerateColumns="false" AutoGenerateDeleteButton="True" OnRowDeleting="grdEsquemas_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Esquema">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtEsquema" runat="server" Text='<%# Eval("Esquema") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Porcentaje">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPorcentaje" runat="server" Text='<%# Eval("Porcentaje") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sueldo Bruto">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSueldoB" runat="server" Text='<%# Eval("SueldoBruto") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sueldo Neto">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSueldoN" runat="server" Text='<%# Eval("SueldoNeto") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sueldo Diario">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSueldoD" runat="server" Text='<%# Eval("SueldoDiario") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Sueldo Diario Integrado">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSueldoDI" runat="server" Text='<%# Eval("SueldoDiarioI") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="IdEsquema" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtGIEsquema" runat="server" Text='<%# Eval("IdEsquema") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                                        <td>Nacionalidad:
                                        </td>
                                        <td>Estado Civil:
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlPrimaRiesgo" runat="server" CssClass="cssDropdown">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="td">
                                            <asp:TextBox ID="txtNacionalidad" runat="server" Text="Mexicana" CssClass="textbox"> </asp:TextBox>
                                        </td>
                                        <td class="td">
                                            <asp:DropDownList ID="ddlECivil" runat="server" CssClass="cssDropdown">
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
            <div class="card cdobra" id="cdobra" style="display: block">
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
                                <asp:UpdatePanel runat="server" ID="UpdatePanelO" UpdateMode="Conditional">
                                    <ContentTemplate>
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
                                                    <asp:TextBox ID="txtCentroCostos" runat="server" CssClass="cssDropdown">
                                                    </asp:TextBox>
                                                </td>

                                                <td class="td">
                                                    <asp:TextBox ID="txtFIObra" runat="server" Text="" CssClass="datepicker"
                                                        placeholder="dd/mm/yyyy" required="required"></asp:TextBox>
                                                </td>
                                                <td class="td">
                                                    <asp:TextBox ID="txtFFObra" runat="server" Text="" CssClass="datepicker"
                                                        placeholder="dd/mm/yyyy" required="required" AutoPostBack="False"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtDiasTobra" runat="server" Text="" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td class="td">
                                                    <asp:TextBox ID="txtTObra" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                                                </td>
                                                <td class="td">
                                                    <asp:TextBox ID="txtUbicacionO" TextMode="MultiLine" MaxLength="100" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
        </div>
    </div>
    <script type="text/javascript" runat="server">

    </script>
</asp:Content>

