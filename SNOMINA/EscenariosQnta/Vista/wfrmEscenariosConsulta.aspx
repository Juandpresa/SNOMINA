<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmEscenariosConsulta.aspx.cs" Inherits="EscenariosQnta.wfrmEscenarioConsulta" %>

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
                    ESCENARIO
                </div>
            </div>
        </div>
        <div id="skills">
            <div class="container_12">
                CONSULTA ESCENARIO
                <div style="width: auto; border: 2px Solid #4a1414;">
                </div>
                <div class="contenPanel">
                    Noombre / Razon Social
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn" OnClick="btnBuscar_Click">
                    </asp:Button>
                </div>
                <div>
                    <div style="overflow: auto; height: 260px;">
                        <asp:GridView runat="server" ID="gvEscenario" CssClass="mGrid" AutoGenerateColumns="False"
                            OnRowCancelingEdit="gvEscenario_RowCancelingEdit" ShowFooter="false" OnRowEditing="gvEscenario_RowEditing"
                            OnRowUpdating="gvEscenario_RowUpdating" OnRowDataBound="gvEscenario_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
                                    <ItemTemplate>
                                        <asp:Label ID="lbId_Cliente" runat="server" Text='<%# Bind("Cliente") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlClientegv" runat="server">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Escenario">
                                    <ItemTemplate>
                                        <asp:Label ID="lbId_Escenario" runat="server" Text='<%# Bind("Id_Escenario") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Propuesta" SortExpression="Propuesta" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbId_EscDetalle" runat="server" Text='<%# Bind("Id_EscDetalle") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prestacion" SortExpression="Prestacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPrestacion" runat="server" Text='<%# Bind("Prestacion") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlPrestaciongv" runat="server">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nomina" SortExpression="PorcNomina">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPorcNomina" runat="server" Text='<%# Eval("PorcNomina") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPorcNomina" runat="server" Text='<%# Eval("PorcNomina")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Asimilados" SortExpression="PorcAsimilados">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPorcAsimilados" runat="server" Text='<%# Eval("PorcAsimilados") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPorcAsimilados" runat="server" Text='<%# Eval("PorcAsimilados")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Honorarios" SortExpression="PorcHonorarios">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPorcHonorarios" runat="server" Text='<%# Eval("PorcHonorarios") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPorcHonorarios" runat="server" Text='<%# Eval("PorcHonorarios")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TN" SortExpression="PorcTN">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPorcTN" runat="server" Text='<%# Eval("PorcTN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPorcTN" runat="server" Text='<%# Eval("PorcTN")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EZ Wallet" SortExpression="PorcEZWallet">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPorcEZWallet" runat="server" Text='<%# Eval("PorcEZWallet") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPorcEZWallet" runat="server" Text='<%# Eval("PorcEZWallet")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Clasificacion" SortExpression="Clasificacion">
                                    <ItemTemplate>
                                        <asp:Label ID="lbClasificacion" runat="server" Text='<%# Eval("Clasificacion") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlClasificaciongv" runat="server" required>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TipoComision" SortExpression="TipoComision">
                                    <ItemTemplate>
                                        <asp:Label ID="lbTipoComision" runat="server" Text='<%# Eval("TipoComision") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlTipoComisiongv" runat="server" required>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ImporteComision" SortExpression="ImporteComision">
                                    <ItemTemplate>
                                        <asp:Label ID="lbImporteComision" runat="server" Text='<%# Eval("ImporteComision") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtImporteComision" runat="server" Text='<%# Eval("ImporteComision")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="RangoSueldoIni" SortExpression="RangoSueldoIni">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRangoSueldoIni" runat="server" Text='<%# Bind("RangoSueldoIni") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRangoSueldoIni" runat="server" Text='<%# Eval("RangoSueldoIni")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RangoSueldoFin" SortExpression="RangoSueldoFin">
                                    <ItemTemplate>
                                        <asp:Label ID="lbRangoSueldoFin" runat="server" Text='<%# Bind("RangoSueldoFin") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRangoSueldoFin" runat="server" Text='<%# Eval("RangoSueldoFin")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Nota" SortExpression="Nota">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNota" runat="server" Text='<%# Eval("Nota") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNota" runat="server" Text='<%# Eval("Nota")%>'></asp:TextBox>
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
    </div>
</asp:Content>
