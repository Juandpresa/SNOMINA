<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmCuotasIMSS.aspx.cs" Inherits="EscenariosQnta.wfrmCuotasIMSS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
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

        //**** Valida Numeros ****//
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
                    <div>
                        <table>
                            <tr>
                                <td>
                                    Fecha:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtFecha" runat="server" Text="" CssClass="textbox" required TextMode="Date"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    DATOS PATRON
                    <div style="width: auto; border: 2px Solid #4a1414;">
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    Pat Cuota Fija:
                                </td>
                                <td>
                                    Pat Exced:
                                </td>
                                <td>
                                    Pat Dinero:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtPatCuotaFija" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatExced" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatDinero" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pat Expecie:
                                </td>
                                <td>
                                    Pat IV:
                                </td>
                                <td>
                                    Pat Retiro:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtPatEspecie" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatIV" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtPatRetiro" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Pat CV:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtPatCV" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    DATOS OBRERO
                    <div style="width: auto; border: 2px Solid #4a1414;">
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    Obr Exc:
                                </td>
                                <td>
                                    Obr Dinero:
                                </td>
                                <td>
                                    Obr Especie:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtObrExc" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtObrDinero" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtObrEspecie" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Obr IV:
                                </td>
                                <td>
                                    Obr CV:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtObrIV" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtObrCV" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Infonavit:
                                </td>
                                <td>
                                    Excedente:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtInfonavit" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                                <td class="td">
                                    <asp:TextBox ID="txtExcedente" runat="server" Text="" CssClass="textbox" required
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>
                <div>
                    <asp:GridView runat="server" ID="grvCuotaIMSS" CssClass="mGrid" AutoGenerateColumns="false"
                        OnRowCancelingEdit="grvCuotaIMSS_RowCancelingEdit" OnRowEditing="grvCuotaIMSS_RowEditing"
                        OnRowUpdating="grvCuotaIMSS_RowUpdating">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id_Cuota" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId_Cuota" runat="server" Text='<%# Eval("Id_Cuota")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFecha" runat="server" Text='<%# Eval("Fecha")%>' onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pat Cuota Fija">
                                <ItemTemplate>
                                    <asp:Label ID="lblPatCuotaFija" runat="server" Text='<%# Eval("PatCuotaFija")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPatCuotaFija" runat="server" CssClass="textRight" Text='<%# Eval("PatCuotaFija")%>'
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pat Exced">
                                <ItemTemplate>
                                    <asp:Label ID="lblPatExced" runat="server" Text='<%# Eval("PatExced")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPatExced" runat="server" CssClass="textRight" Text='<%# Eval("PatExced")%>'
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pat Dinero">
                                <ItemTemplate>
                                    <asp:Label ID="lblPatDinero" runat="server" Text='<%# Eval("PatDinero")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPatDinero" runat="server" CssClass="textRight" Text='<%# Eval("PatDinero")%>'
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pat Especie">
                                <ItemTemplate>
                                    <asp:Label ID="lblPatEspecie" runat="server" Text='<%# Eval("PatEspecie")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPatEspecie" runat="server" CssClass="textRight" Text='<%# Eval("PatEspecie")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pat IV">
                                <ItemTemplate>
                                    <asp:Label ID="lblPatIV" runat="server" Text='<%# Eval("PatIV")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPatIV" runat="server" CssClass="textRight" Text='<%# Eval("PatIV")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pat Retiro">
                                <ItemTemplate>
                                    <asp:Label ID="lblPatRetiro" runat="server" Text='<%# Eval("PatRetiro")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPatRetiro" runat="server" CssClass="textRight" Text='<%# Eval("PatRetiro")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pat CV">
                                <ItemTemplate>
                                    <asp:Label ID="lblPatCV" runat="server" Text='<%# Eval("PatCV")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPatCV" runat="server" CssClass="textRight" Text='<%# Eval("PatCV")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Obr Exc">
                                <ItemTemplate>
                                    <asp:Label ID="lblObrExc" runat="server" Text='<%# Eval("ObrExc")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtObrExc" runat="server" CssClass="textRight" Text='<%# Eval("ObrExc")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Obr Dinero">
                                <ItemTemplate>
                                    <asp:Label ID="lblDinero" runat="server" Text='<%# Eval("ObrDinero")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtObrDinero" runat="server" CssClass="textRight" Text='<%# Eval("ObrDinero")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Obr Especie">
                                <ItemTemplate>
                                    <asp:Label ID="lblObrEspecie" runat="server" Text='<%# Eval("ObrEspecie")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtObrEspecie" runat="server" CssClass="textRight" Text='<%# Eval("ObrEspecie")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Obr IV">
                                <ItemTemplate>
                                    <asp:Label ID="lblObrIV" runat="server" Text='<%# Eval("ObrIV")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtObrIV" runat="server" CssClass="textRight" Text='<%# Eval("ObrIV")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Obr CV">
                                <ItemTemplate>
                                    <asp:Label ID="lblObrCV" runat="server" Text='<%# Eval("ObrCV")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtObrCV" runat="server" CssClass="textRight" Text='<%# Eval("ObrCV")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Infonavit">
                                <ItemTemplate>
                                    <asp:Label ID="lblInfonavit" runat="server" Text='<%# Eval("Infonavit")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtInfonavit" runat="server" CssClass="textRight" Text='<%# Eval("Infonavit")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Excedente">
                                <ItemTemplate>
                                    <asp:Label ID="lblExcedente" runat="server" Text='<%# Eval("Excedente")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtExcedente" runat="server" CssClass="textRight" Text='<%# Eval("Excedente")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                           <asp:CommandField  ShowEditButton="True" >
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
