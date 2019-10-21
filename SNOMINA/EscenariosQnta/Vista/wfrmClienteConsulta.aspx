<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmClienteConsulta.aspx.cs" Inherits="EscenariosQnta.wfrmClienteConsulta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
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
                    CLIENTE
                </div>
            </div>
        </div>
        <div id="skills">
            <div class="container_12">
                CONSULTA CLIENTES
                <div style="width: auto; border: 2px Solid #4a1414;">
                </div>
                <div class="contenPanel">
                    Nombre / Razon Social
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn" OnClick="btnBuscar_Click">
                    </asp:Button>
                </div>
                <div>
                    <div style="overflow: auto; height: 260px;">
                        <asp:GridView runat="server" ID="grClientes" CssClass="mGrid" AutoGenerateColumns="False"
                            Width="100%" DataKeyNames="Id_Cliente" AllowPaging="True" EnableViewState="False"
                            OnPageIndexChanging="grClientes_PageIndexChanging" OnRowDataBound="grClientes_RowDataBound">
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFormatString="~/Vista/wfrmClienteActulizar.aspx?Id_Cliente={0}"
                                    DataNavigateUrlFields="Id_Cliente" HeaderText="" Text="Editar" ItemStyle-Width="30px">
                                    <FooterStyle Font-Bold="True" Font-Names="0066cc" />
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:HyperLinkField>
                                <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCliente" runat="server" Text='<%# Bind("Id_Cliente") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre / Razon Social" SortExpression="NombreRazonSocial">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNombreRazonSocial" runat="server" Text='<%# Bind("Nombre_RazonSocial") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Denominacion" SortExpression="Denominacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDenominacion" runat="server" Text='<%# Bind("Denominacion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Giro" SortExpression="Giro">
                                    <ItemTemplate>
                                        <asp:Label ID="lbGiro" runat="server" Text='<%# Bind("Giro") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calle" SortExpression="CalleNum">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCalleNum" runat="server" Text='<%# Bind("CalleNum") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Colonia" SortExpression="Colonia">
                                    <ItemTemplate>
                                        <asp:Label ID="lbColonia" runat="server" Text='<%# Bind("Colonia") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delegacion" SortExpression="Delegacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDelegacion" runat="server" Text='<%# Bind("Delegacion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Entidad" SortExpression="Entidad">
                                    <ItemTemplate>
                                        <asp:Label ID="lbEntidad" runat="server" Text='<%# Bind("Entidad") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CP" SortExpression="CP">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCP" runat="server" Text='<%# Bind("CP") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pais" SortExpression="Pais">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPais" runat="server" Text='<%# Bind("Pais") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Telefono" SortExpression="Telefono">
                                    <ItemTemplate>
                                        <asp:Label ID="lbTelefono" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contacto" SortExpression="Contacto">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNomContacto" runat="server" Text='<%# Bind("NomContacto") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ejecutivo" SortExpression="Ejecutivo">
                                    <ItemTemplate>
                                        <asp:Label ID="lbEjecutivo" runat="server" Text='<%# Bind("Ejecutivo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Socio" SortExpression="Socio">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSocio" runat="server" Text='<%# Bind("Socio") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asociados" SortExpression="Asociados">
                                    <ItemTemplate>
                                        <asp:Label ID="lbAsociados" runat="server" Text='<%# Bind("Asociados") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Notas" SortExpression="Notas">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNotas" runat="server" Text='<%# Bind("Notas") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#EDECEC" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
