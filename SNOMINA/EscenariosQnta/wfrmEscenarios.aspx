<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmEscenarios.aspx.cs" Inherits="EscenariosQnta.wfrmEscenarios" %>

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
                        ESCENARIO
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
                                Folio Escenario:
                            </td>
                            <td>
                                Cliente:
                            </td>
                            <td>
                                Tabla Prestaciones:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtFolio" runat="server" Text="0" CssClass="textbox" Enabled="false"
                                    required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlPrestacion" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Porcentaje Nomina:
                            </td>
                            <td>
                                Porcentaje Asimilados:
                            </td>
                            <td>
                                Porcentaje Honorarios:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtNomina" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtAsimilados" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtHonorarios" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Porcentaje Otros Productos:
                            </td>
                            <td>
                                Aplica para Clasificacion:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtOtrosProductos" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlClasificacion" runat="server" CssClass="cssDropdown" required>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Rango de Sueldo de:
                            </td>
                            <td>
                                a:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtRangoSueldoIni" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtRangoSueldoFin" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Notas:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtNotas" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                            </td>
                            <td class="td">
                                <asp:Button ID="btnNueva" Text="Nueva" runat="server" CssClass="btn" OnClick="btnNueva_Click"
                                    Visible="false" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:GridView runat="server" ID="gvEscenario" CssClass="mGrid" AutoGenerateColumns="False"
                        OnRowCancelingEdit="gvEscenario_RowCancelingEdit" ShowFooter="true" OnRowEditing="gvEscenario_RowEditing"
                        OnRowUpdating="gvEscenario_RowUpdating" OnRowDataBound="gvEscenario_RowDataBound"
                        OnDataBound="gvEscenario_DataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Escenario">
                                <ItemTemplate>
                                    <asp:Label ID="lbId_Escenario" runat="server" Text='<%# Bind("Id_Escenario") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Propuesta" SortExpression="Propuesta">
                                <ItemTemplate>
                                    <asp:Label ID="lbId_Propuesta" runat="server" Text='<%# Bind("Id_Propuesta") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
                                <ItemTemplate>
                                    <asp:Label ID="lbId_Cliente" runat="server" Text='<%# Bind("Cliente") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlClientegv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlClientegv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prestacion" SortExpression="Prestacion">
                                <ItemTemplate>
                                    <asp:Label ID="lbPrestacion" runat="server" Text='<%# Bind("Prestacion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlPrestaciongv" runat="server" CssClass="cssDropdown" required>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlPrestaciongv" runat="server" CssClass="cssDropdown" required>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PorcNomina" SortExpression="PorcNomina">
                                <ItemTemplate>
                                    <asp:Label ID="lbPorcNomina" runat="server" Text='<%# Bind("PorcNomina") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPorcNomina" runat="server" Text='<%# Eval("PorcNomina")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPorcNomina" runat="server" Text=""></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PorcAsimilados" SortExpression="PorcAsimilados">
                                <ItemTemplate>
                                    <asp:Label ID="lbPorcAsimilados" runat="server" Text='<%# Bind("PorcAsimilados") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPorcAsimilados" runat="server" Text='<%# Eval("PorcAsimilados")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPorcAsimilados" runat="server" Text=""></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PorcHonorarios" SortExpression="PorcHonorarios">
                                <ItemTemplate>
                                    <asp:Label ID="lbPorcHonorarios" runat="server" Text='<%# Bind("PorcHonorarios") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPorcHonorarios" runat="server" Text='<%# Eval("PorcHonorarios")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPorcHonorarios" runat="server" Text=""></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PorOtrosProductos" SortExpression="PorOtrosProductos">
                                <ItemTemplate>
                                    <asp:Label ID="lbPorOtrosProductos" runat="server" Text='<%# Bind("PorOtrosProductos") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txPorOtrosProductos" runat="server" Text='<%# Eval("PorOtrosProductos")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPorOtrosProductoss" runat="server" Text=""></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clasificacion" SortExpression="Clasificacion">
                                <ItemTemplate>
                                    <asp:Label ID="lbClasificacion" runat="server" Text='<%# Bind("Clasificacion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                       <asp:DropDownList ID="ddlClasificaciongv" runat="server" CssClass="cssDropdown" required>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddlClasificaciongv" runat="server" CssClass="cssDropdown" required>
                                    </asp:DropDownList>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RangoSueldoIni" SortExpression="RangoSueldoIni">
                                <ItemTemplate>
                                    <asp:Label ID="lbRangoSueldoIni" runat="server" Text='<%# Bind("RangoSueldoIni") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRangoSueldoIni" runat="server" Text='<%# Eval("RangoSueldoIni")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtRangoSueldoIni" runat="server" Text=""></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RangoSueldoFin" SortExpression="RangoSueldoFin">
                                <ItemTemplate>
                                    <asp:Label ID="lbRangoSueldoFin" runat="server" Text='<%# Bind("RangoSueldoFin") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRangoSueldoFin" runat="server" Text='<%# Eval("RangoSueldoFin")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtRangoSueldoFin" runat="server" Text=""></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nota" SortExpression="Nota">
                                <ItemTemplate>
                                    <asp:Label ID="lbNota" runat="server" Text='<%# Bind("Nota") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNota" runat="server" Text='<%# Eval("Nota")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNota" runat="server" Text=""></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <FooterTemplate>
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click">
                                    </asp:Button>
                                </FooterTemplate>
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
