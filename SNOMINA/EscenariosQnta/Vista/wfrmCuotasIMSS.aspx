<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmCuotasIMSS.aspx.cs" Inherits="EscenariosQnta.wfrmCuotasIMSS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- CSS -->
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-datepicker.css" rel="stylesheet" type="text/css" />
    <!-- JS -->
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".datepicker").datepicker({ format: 'dd/mm/yyyy', autoclose: true, todayBtn: 'linked' })
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

    </script>
    <style type="text/css">
        .textRight
        {
            text-align: right;
            width: 70px;
        }
    </style>
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
                    CUOTA IMSS
                </div>
            </div>
        </div>
        <div id="skills">
            <div class="container_12">
                <div class="contenPanel">
                    DATOS GENERALES
                    <div style="width: auto; border: 2px Solid #4a1414;">
                    </div>
                    <div class="contenPanel">
                        <table>
                            <tr>
                                <td>
                                    Fecha:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtFecha" runat="server" Text="" CssClass="datepicker" required
                                        placeholder="dd/mm/yyyy"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    DATOS PATRON
                    <div style="width: auto; border: 2px Solid #4a1414;">
                    </div>
                    <div class="contenPanel">
                        <table>
                            <tr>
                                <td>
                                    Cuota Fija:
                                </td>
                                <td>
                                    Excedente
                                </td>
                                <td>
                                    Prestaciones en Dinero:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtPatCuotaFija" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatExcedente" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatDinero" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Invalidez Y Vida:
                                </td>
                                <td>
                                    Retiro:
                                </td>
                                <td>
                                    Cesantía Y Vejez:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtPatIV" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatRetiro" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatCV" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Gastos Medicos:
                                </td>
                                <td>
                                    Guardería Patron:
                                </td>
                                <td>
                                    Infonavit:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtPatGastosMedicos" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatGuarderia" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatInfonavit" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                    DATOS OBRERO
                    <div style="width: auto; border: 2px Solid #4a1414;">
                    </div>
                    <div class="contenPanel">
                        <table>
                            <tr>
                                <td>
                                    Enfermedades y Cuota fija:
                                </td>
                                <td>
                                    Enfermedad y Maternidad Prestaciones en Dinero:
                                </td>
                                <td>
                                    Enfermedades y Maternidad:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtObrCuotaFija" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtObrDinero" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtObrMedicos" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Invalidez Y Vida:
                                </td>
                                <td>
                                    Cesantía Y Vejez:
                                </td>
                                <td>
                                    Guarderia Obrero:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtObrIV" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtObrCV" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtObrGuarderia" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Excedente De 3 SMG:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtObrExcedente" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>
                <div style="height: 50px;">
                </div>
                <div style="overflow: auto; height: 260px;">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel">
                        <ContentTemplate>
                            <asp:GridView runat="server" ID="grvCuotaIMSS" CssClass="mGrid" AutoGenerateColumns="False"
                                OnRowCancelingEdit="grvCuotaIMSS_RowCancelingEdit" OnRowEditing="grvCuotaIMSS_RowEditing"
                                OnRowUpdating="grvCuotaIMSS_RowUpdating" Width="100%" OnRowDataBound="grvCuotaIMSS_RowDataBound">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id_Cuota" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId_Cuota" runat="server" Text='<%# Eval("Id_Cuota")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fecha">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtFecha" runat="server" Text='<%# Eval("Fecha")%>' onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cuota Fija Patron">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatCuotaFija" runat="server" Text='<%# Eval("PatCuotaFija")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPatCuotaFija" runat="server" Text='<%# Eval("PatCuotaFija")%>'
                                                onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excedente">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatExcedente" runat="server" Text='<%# Eval("PatExcedente")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPatExcedente" runat="server" Text='<%# Eval("PatExcedente")%>'
                                                onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prestaciones en Dinero">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatDinero" runat="server" Text='<%# Eval("PatDinero")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPatDinero" runat="server" Text='<%# Eval("PatDinero")%>' onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invalidez y Vejez Patron">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatIV" runat="server" Text='<%# Eval("PatIV")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPatIV" runat="server" Text='<%# Eval("PatIV")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retiro">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatRetiro" runat="server" Text='<%# Eval("PatRetiro")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPatRetiro" runat="server" Text='<%# Eval("PatRetiro")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cesantía y Vejez Patron">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatCV" runat="server" Text='<%# Eval("PatCV")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPatCV" runat="server" Text='<%# Eval("PatCV")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gastos Medicos Patron">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatGastosMedicos" runat="server" Text='<%# Eval("PatGastosMedicos")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPatGastosMedicos" runat="server" Text='<%# Eval("PatGastosMedicos")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Guarderia Patron">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatGuarderia" runat="server" Text='<%# Eval("PatGuarderia")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPatGuarderia" runat="server" Text='<%# Eval("PatGuarderia")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Infonavit Patron">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPatInfonavit" runat="server" Text='<%# Eval("PatInfonavit")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPatInfonavit" runat="server" Text='<%# Eval("PatInfonavit")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cuota Fija Obrero">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObrCuotaFija" runat="server" Text='<%# Eval("ObrCuotaFija")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtObrCuotaFija" runat="server" Text='<%# Eval("ObrCuotaFija")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prestaciones En Dinero Obrero">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDinero" runat="server" Text='<%# Eval("ObrDinero")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtObrDinero" runat="server" Text='<%# Eval("ObrDinero")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gastos Médicos Obrero">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObrMedicos" runat="server" Text='<%# Eval("ObrMedicos")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtObrMedicos" runat="server" Text='<%# Eval("ObrMedicos")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invalidez y Vida Obrero">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObrIV" runat="server" Text='<%# Eval("ObrIV")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtObrIV" runat="server" Text='<%# Eval("ObrIV")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cesantia Y Vejez Obrero">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObrCV" runat="server" Text='<%# Eval("ObrCV")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtObrCV" runat="server" Text='<%# Eval("ObrCV")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Guarderia Obrero">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObrGuarderia" runat="server" Text='<%# Eval("ObrGuarderia")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtObrGuarderia" runat="server" Text='<%# Eval("ObrGuarderia")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excedente Obrero">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObrExcedente" runat="server" Text='<%# Eval("ObrExcedente")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtObrExcedente" runat="server" Text='<%# Eval("ObrExcedente")%>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True">
                                        <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
                                    </asp:CommandField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#EDECEC" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
