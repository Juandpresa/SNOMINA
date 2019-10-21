<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmEscenario.aspx.cs" Inherits="EscenariosQnta.wfrmEscenario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
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
                    CONFIGURACION EMPLEADO
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
                                Escenario:
                            </td>
                            <td>
                                Cliente
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtEscenario" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="cssDropdown">
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Nombre:
                            </td>
                            <td>
                                Apellido Paterno:
                            </td>
                            <td>
                                Apellido Materno:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtNombre" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtPaterno" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtMaterno" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Puesto:
                            </td>
                            <td>
                                Descripcion Puesto:
                            </td>
                            <td>
                                Prima Riesgo:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtPuesto" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtDescripcion" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlPrimaRiesgo" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha Ingreso:
                            </td>
                            <td>
                                Fecha Nacimiento:
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtFechaIngreso" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtFechaNacimiento" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sueldo Bruto:
                            </td>
                            <td>
                                Sueldo Neto:
                            </td>
                            <td>
                                Sueldo Integrado:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtSueldoBruto" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtSueldoNeto" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtSueldoIntegrado" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Ubicacion Laboral:
                            </td>
                            <td>
                                Infonavit:
                            </td>
                            <td>
                                Importe Fonacot:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtUbicacionLaboral" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlInfonavit" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtImporteFonacot" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Prestacion:
                            </td>
                            <td>
                                Pension Alimenticia:
                            </td>
                            <td>
                                Importe Pension:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:DropDownList ID="ddlPrestacion" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlPension" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtImportePension" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Esquema Actual:
                            </td>
                            <td>
                                Clasificacion Empleado:
                            </td>
                            <td>
                                Nacionalidad:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:DropDownList ID="ddlEsquemaActual" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlClasificacionEmpleado" runat="server" CssClass="cssDropdown"
                                    required>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtNacionalidad" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>
                <div>
                    <asp:GridView runat="server" ID="gvEmpleado" CssClass="mGrid" AutoGenerateColumns="False"
                        OnRowCancelingEdit="gvEmpleado_RowCancelingEdit" OnRowEditing="gvEmpleado_RowEditing"
                        OnRowUpdating="gvEmpleado_RowUpdating" OnRowDataBound="gvEmpleado_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Id_Empleado" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbId_Empleado" runat="server" Text='<%# Bind("Id_Empleado") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Escenario" SortExpression="Escenario">
                                <ItemTemplate>
                                    <asp:Label ID="lbId_Escenario" runat="server" Text='<%# Bind("Id_Escenario") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtExcenario" runat="server" Text='<%# Eval("Id_Escenario")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
                                <ItemTemplate>
                                    <asp:Label ID="lbId_Cliente" runat="server" Text='<%# Bind("Nombre_RazonSocial") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlClientegv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre" SortExpression="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lbNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("Nombre")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Paterno" SortExpression="Paterno">
                                <ItemTemplate>
                                    <asp:Label ID="lbPaterno" runat="server" Text='<%# Bind("Paterno") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPaterno" runat="server" Text='<%# Eval("Paterno")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Materno" SortExpression="Materno">
                                <ItemTemplate>
                                    <asp:Label ID="lbMaterno" runat="server" Text='<%# Bind("Materno") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMaterno" runat="server" Text='<%# Eval("Materno")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Puesto" SortExpression="Puesto">
                                <ItemTemplate>
                                    <asp:Label ID="lbPuesto" runat="server" Text='<%# Bind("Puesto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPuesto" runat="server" Text='<%# Eval("Puesto")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripcion Puesto" SortExpression="Descripcion Puesto">
                                <ItemTemplate>
                                    <asp:Label ID="lbDescriPto" runat="server" Text='<%# Bind("DescriPto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescriPto" runat="server" Text='<%# Eval("DescriPto")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prima Riesgo" SortExpression="Prima Riesgo">
                                <ItemTemplate>
                                    <asp:Label ID="lbPrimaRiesgo" runat="server" Text='<%# Bind("PrimaRiesgo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlPrimaRiesgogv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Ingreso" SortExpression="Fecha Ingreso">
                                <ItemTemplate>
                                    <asp:Label ID="lbFechaIngreso" runat="server" Text='<%# Bind("FechaIngreso") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFechaIngreso" runat="server" Text='<%# Eval("FechaIngreso")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha Nacimiento" SortExpression="Fecha Nacimiento">
                                <ItemTemplate>
                                    <asp:Label ID="lbFechaNac" runat="server" Text='<%# Bind("FechaNac") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFechaNac" runat="server" Text='<%# Eval("FechaNac")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sueldo Bruto" SortExpression="Sueldo Bruto">
                                <ItemTemplate>
                                    <asp:Label ID="lbSueldoBruto" runat="server" Text='<%# Bind("SueldoBruto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSueldoBruto" runat="server" Text='<%# Eval("SueldoBruto")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sueldo Neto" SortExpression="Sueldo Neto">
                                <ItemTemplate>
                                    <asp:Label ID="lbSueldoNeto" runat="server" Text='<%# Bind("SueldoNeto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSueldoNeto" runat="server" Text='<%# Eval("SueldoNeto")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sueldo Integrado" SortExpression="Sueldo Integrado">
                                <ItemTemplate>
                                    <asp:Label ID="lbSueldoIntegrado" runat="server" Text='<%# Bind("SueldoIntegrado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSueldoIntegrado" runat="server" Text='<%# Eval("SueldoIntegrado")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ubicacion Labora" SortExpression="Ubicacion Laboral">
                                <ItemTemplate>
                                    <asp:Label ID="lbUbicaLabora" runat="server" Text='<%# Bind("UbicaLabora") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUbicaLabora" runat="server" Text='<%# Eval("UbicaLabora")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Infonavit" SortExpression="Infonavit">
                                <ItemTemplate>
                                    <asp:Label ID="lbInfonavit" runat="server" Text='<%# Bind("Infonavit") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlInfonavitgv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Importe Fonacot" SortExpression="Importe Fonacot">
                                <ItemTemplate>
                                    <asp:Label ID="lbImpFonacot" runat="server" Text='<%# Bind("ImpFonacot") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtImpFonacot" runat="server" Text='<%# Eval("ImpFonacot")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prestacion" SortExpression="Prestacion">
                                <ItemTemplate>
                                    <asp:Label ID="lbPrestacion" runat="server" Text='<%# Bind("Prestacion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlPrestaciongv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pension" SortExpression="Pension">
                                <ItemTemplate>
                                    <asp:Label ID="lbPension" runat="server" Text='<%# Bind("Pension") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlPensiongv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Importe Pension" SortExpression="Importe Pension">
                                <ItemTemplate>
                                    <asp:Label ID="lbImportePension" runat="server" Text='<%# Bind("ImportePension") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtImportePension" runat="server" Text='<%# Eval("ImportePension")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Esquema" SortExpression="Esquema">
                                <ItemTemplate>
                                    <asp:Label ID="lbEsquema" runat="server" Text='<%# Bind("Esquema") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlEsquemagv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clasificacion" SortExpression="Clasificacion">
                                <ItemTemplate>
                                    <asp:Label ID="lbClasificacion" runat="server" Text='<%# Bind("Clasificacion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlClasificaciongv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nacionalidad" SortExpression="Nacionalidad">
                                <ItemTemplate>
                                    <asp:Label ID="lbNacionalidad" runat="server" Text='<%# Bind("Nacionalidad") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNacionalidad" runat="server" Text='<%# Eval("Nacionalidad")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True">
                                <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
                            </asp:CommandField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#EDECEC" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
