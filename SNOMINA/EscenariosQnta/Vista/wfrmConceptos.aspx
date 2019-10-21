<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmConceptos.aspx.cs" Inherits="EscenariosQnta.wfrmConceptos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
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


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            var valor = document.getElementById('<%= txtTopeExc.ClientID %>').value

            if ((charCode <= 57 && charCode >= 48)) {


                if (valor == '0')
                { document.getElementById('ContentPlaceHolder1_ddlTipoExc').disabled = true; }
                else { document.getElementById('ContentPlaceHolder1_ddlTipoExc').disabled = false; }

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

        function minmax(value, min, max) {
            if (parseInt(value) < min || isNaN(parseInt(value)))
                return 0;
            else if (parseInt(value) > max)
                return 99;
            else return value;
        }

        function validaTipoExc() {
            document.getElementById('ddlTipoExc').disabled = true;
        }
       

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="theForm" action="" method="GET">
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
                    CONCEPTOS
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
                                Nombre Corto
                            </td>
                            <td>
                                Descripcion:
                            </td>
                            <td>
                                Tipo de Concepto
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtNomCorto" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtDescripcion" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <%--<asp:TextBox ID="txtTipoConcepto" runat="server" Text="" CssClass="textbox" required></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlTipoConcepto" runat="server" CssClass="cssDropdown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                AppTNom:
                            </td>
                            <td>
                                Prioridad:
                            </td>
                            <td>
                                Calculo:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtAppTNom" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtPrioridad" runat="server" Text="" CssClass="textbox" required
                                    TextMode="Number" onkeypress="return isNumberKey(event)" min="0" max="99" size="1"
                                    MaxLength="2" onkeyup="this.value = minmax(this.value, 0, 99)"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtCalculo" runat="server" Text="" CssClass="textbox" required></asp:TextBox>
                            </td>
                            <td>
                                <%-- <asp:Button ID="btnAddFuncion" Text="Agregar Operador" runat="server" CssClass="btn" />--%>
                                <asp:LinkButton ID="btnAddFuncion" runat="server" OnClick="btnAddFuncion_Click">Agregar Operador</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                IntegraSS:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <%--<asp:TextBox ID="txtIntegraSS" runat="server" Text="" CssClass="textbox" required></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlIntegra" runat="server" CssClass="cssDropdown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Dato Sup:
                            </td>
                            <td>
                                Dato Info:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtDatoSup" runat="server" Text="0" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtDatoInfo" runat="server" Text="0" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Imp Sup:
                            </td>
                            <td>
                                Imp Inf:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtImpSup" runat="server" Text="0" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtImpInf" runat="server" Text="0" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Porc Sup:
                            </td>
                            <td>
                                Porc Inf:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtPorcSup" runat="server" Text="0" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtPorcInf" runat="server" Text="0" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tope Exc:
                            </td>
                            <td>
                                Tipo Exc:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtTopeExc" runat="server" Text="0" CssClass="textbox" TextMode="Number"
                                    onkeypress="return isNumberKey(event); validaTipoExc();" min="0" max="99" size="1"
                                    MaxLength="2" onkeyup="this.value = minmax(this.value, 0, 99)" OnTextChanged="txtTopeExc_TextChanged"></asp:TextBox>
                            </td>
                            <td class="td">
                                <%--<asp:TextBox ID="txtTipoexc" runat="server" Text="" CssClass="textbox" required Enabled="false"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlTipoExc" runat="server" CssClass="cssDropdown" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                esPrevisionSocial:
                            </td>
                            <td>
                                AfectaNeto:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <%--<asp:TextBox ID="txtesPrevisionSocial" runat="server" Text="" CssClass="textbox"
                                    required></asp:TextBox>--%>
                                <asp:CheckBox ID="chkPrevision" runat="server" />
                            </td>
                            <td class="td">
                                <%--<asp:TextBox ID="txtAfectaNeto" runat="server" Text="" CssClass="textbox" required></asp:TextBox>--%>
                                <asp:CheckBox ID="chkAfectaNeto" runat="server" CssClass="chkBox" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>
                <div style="height: 50px">
                </div>
                <div style="overflow: auto; height: 260px;">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel">
                        <ContentTemplate>
                            <asp:GridView runat="server" ID="gvConceptos" CssClass="mGrid" AutoGenerateColumns="False"
                                OnRowCancelingEdit="gvConceptos_RowCancelingEdit" OnRowEditing="gvConceptos_RowEditing"
                                OnRowUpdating="gvConceptos_RowUpdating" OnRowDataBound="gvConceptos_RowDataBound"
                                Width="100%">
                                <Columns>
                                    <asp:CommandField ShowEditButton="True">
                                        <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
                                    </asp:CommandField>
                                    <asp:TemplateField HeaderText="Id_Conceptos" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lbId_Conceptos" runat="server" Text='<%# Bind("Id_Conceptos") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nombre Corto" SortExpression="Cliente">
                                        <ItemTemplate>
                                            <asp:Label ID="lbNomCorto" runat="server" Text='<%# Bind("NomCorto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNomCorto" runat="server" Text='<%# Eval("NomCorto")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                                        <ItemTemplate>
                                            <asp:Label ID="lbDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("Descripcion")%>'
                                                Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo Concepto" SortExpression="TipoConcepto">
                                        <ItemTemplate>
                                            <asp:Label ID="lbTipoConcepto" runat="server" Text='<%# Bind("TipoConcepto") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlTipoConceptogv" runat="server" >
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AppTNom" SortExpression="AppTNom">
                                        <ItemTemplate>
                                            <asp:Label ID="lbAppTNom" runat="server" Text='<%# Bind("AppTNom") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtAppTNom" runat="server" Text='<%# Eval("AppTNom")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prioridad" SortExpression="Prioridad">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPrioridad" runat="server" Text='<%# Bind("Prioridad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPrioridad" runat="server" Text='<%# Eval("Prioridad")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Calculo" SortExpression="Calculo">
                                        <ItemTemplate>
                                            <asp:Label ID="lbCalculo" runat="server" Text='<%# Bind("Calculo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtCalculo" runat="server" Text='<%# Eval("Calculo")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IntegraSS" SortExpression="IntegraSS">
                                        <ItemTemplate>
                                            <asp:Label ID="lbIntegraSS" runat="server" Text='<%# Bind("IntegraSS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlIntegraSSgv" runat="server" >
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DatoSup" SortExpression="DatoSup">
                                        <ItemTemplate>
                                            <asp:Label ID="lbDatoSup" runat="server" Text='<%# Bind("DatoSup") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDatoSup" runat="server" Text='<%# Eval("DatoSup")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DatoInf" SortExpression="DatoInf">
                                        <ItemTemplate>
                                            <asp:Label ID="lbDatoInf" runat="server" Text='<%# Bind("DatoInf") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDatoInf" runat="server" Text='<%# Eval("DatoInf")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ImpSup" SortExpression="ImpSup">
                                        <ItemTemplate>
                                            <asp:Label ID="lbImpSup" runat="server" Text='<%# Bind("ImpSup") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtImpSup" runat="server" Text='<%# Eval("ImpSup")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ImpInf" SortExpression="ImpInf">
                                        <ItemTemplate>
                                            <asp:Label ID="lbImpInf" runat="server" Text='<%# Bind("ImpInf") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtImpInf" runat="server" Text='<%# Eval("ImpInf")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PorcSup" SortExpression="PorcSup">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPorcSup" runat="server" Text='<%# Bind("PorcSup") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPorcSup" runat="server" Text='<%# Eval("PorcSup")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PorInf" SortExpression="PorInf">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPorInf" runat="server" Text='<%# Bind("PorInf") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPorInf" runat="server" Text='<%# Eval("PorInf")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="esPrevisionSocial" SortExpression="esPrevisionSocial">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkesPrevisionSocial" runat="server" Checked='<%# Bind("esPrevisionSocial") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkesPrevisionSocial" runat="server" Checked='<%# Eval("esPrevisionSocial")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipoexc" SortExpression="Tipoexc">
                                        <ItemTemplate>
                                            <asp:Label ID="lbTipoexc" runat="server" Text='<%# Bind("Tipoexc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlTipoExcgv" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TopeExc" SortExpression="TopeExc">
                                        <ItemTemplate>
                                            <asp:Label ID="lbTopeExc" runat="server" Text='<%# Bind("TopeExc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtTopeExc" runat="server" Text='<%# Eval("TopeExc")%>' Width="100%"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AfectaNeto" SortExpression="AfectaNeto">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAfectaNeto" runat="server" Checked='<%# Bind("AfectaNeto")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:CheckBox ID="chkAfectaNeto" runat="server" Checked='<%# Eval("AfectaNeto")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#EDECEC" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
