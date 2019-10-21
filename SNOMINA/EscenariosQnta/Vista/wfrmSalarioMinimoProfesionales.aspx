<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmSalarioMinimoProfesionales.aspx.cs" Inherits="EscenariosQnta.wfrmSalarioMinimoProfesionales" %>

<%--<%@ Register Src="../ControlUsuario/CuadroMensajeControlUsu.ascx" TagName="CuadroMensajeControlUsu" TagPrefix="uc1" %>--%>
<%@ MasterType VirtualPath="~/Vista/Site.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-datepicker.css" rel="stylesheet" type="text/css" />
    <!-- JS -->
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        
        //**** Formato Fecha ****//
        $(document).ready(function () {
            $(".datepicker").datepicker({ format: 'dd/mm/yyyy', autoclose: true, todayBtn: 'linked' })
        });
        
        //**** Valida Letras *****//
        function isAlphabetKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97 || charCode == 32) || (charCode == 241) || (charCode == 44)) {

                return true;
            }
            return false;
        }

        //**** Valida Numeros Telefonicos ****//
        function isPhoneNumnberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 57 && charCode >= 48) || (charCode == 47)) {

                return true;
            }
            return false;

        }

        // **** Valida Numeros ****//
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

        //**** Valida Fecha ****//
        function isDateKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 57 && charCode >= 48) || (charCode == 47)) {

                return true;
            }
            return false;

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
            <%--<div id="stars2">
            </div>--%>
            <div id="srars3">
            </div>
            <div class="containerTitlePage ">
                <div class="titlePage">
                    SALARIO MINIMO PROFESIONALES
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
                                Fecha:
                            </td>
                            <td>
                                Categoria:
                            </td>
                            <td>
                                Salario Minimo:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtFecha" runat="server" Text="" CssClass="datepicker" required
                                    placeholder="dd/mm/yyyy" onkeypress="return isPhoneNumnberKey(event, this);"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtProfesionales" runat="server" Text="" CssClass="textbox" onkeypress="return isAlphabetKey(event, this)"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtValor" runat="server" Text="" onkeypress="return isDecimalKey(event, this);"
                                    CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>
                <div style="height: 50px;">
                </div>
                <div style="overflow: auto; height: 260px;">
                    <asp:GridView ID="gvSalarioMinimoProf" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                        OnRowEditing="gvSalarioMinimoProf_RowEditing" OnRowUpdating="gvSalarioMinimoProf_RowUpdating"
                        OnRowCancelingEdit="gvSalarioMinimoProf_RowCancelingEdit" AllowPaging="true"
                        PageSize="50">
                        <Columns>
                            <asp:TemplateField HeaderText="Id_SalMinProf" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId_SalMinProf" runat="server" Text='<%# Eval("Id_SalMinProf")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFecha" runat="server" Text='<%# Eval("Fecha")%>' onkeypress="return isDateKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Categoria">
                                <ItemTemplate>
                                    <asp:Label ID="lblProfesionales" runat="server" Text='<%# Eval("Profesionales")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtProfesionales" runat="server" Text='<%# Eval("Profesionales")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salario Minimo">
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server" Text='<%# Eval("Valor")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("Valor")%>' onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
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
