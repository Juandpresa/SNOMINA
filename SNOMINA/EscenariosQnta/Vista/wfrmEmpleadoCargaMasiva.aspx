<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmEmpleadoCargaMasiva.aspx.cs" Inherits="EscenariosQnta.wfrmEmpleadoCargaMasiva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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

        function isDateKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 57 && charCode >= 48) || (charCode == 47) || (charCode == 58)) {

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
            <div id="stars2">
            </div>
            <div id="srars3">
            </div>
            <div class="containerTitlePage ">
                <div class="titlePage">
                    EMPLEADOS CARGA MASIVA
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
                            <td class="td">
                                Archivo Empleado:
                            </td>
                        </tr>
                        
                    </table>
                    <div style="width:50%; float:left;">
                        <asp:FileUpload runat="server" ID="fulEmpleado" accept="xls|xlsx" CssClass="btn"
                                    Height="30px" Width="564px" data-buttonText="Select a File"/>
                    </div>

                    <div style="width:25%; float:right;">
                        <asp:Button ID="btnDescargar" runat="server" Text="Descargar Layout" 
                                    CssClass="btn" onclick="btnDescargar_Click"/>
                    </div>
                                    <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>

                </div>

                <div style="height: 50px;">
                </div>
                <div style="overflow:auto; height:260px;">
                    <asp:GridView ID="gvEmpleados" runat="server" AutoGenerateColumns="true" 
                        CssClass="mGrid" onrowdatabound="gvEmpleados_RowDataBound">
                        <AlternatingRowStyle BackColor="#EDECEC" />
                    </asp:GridView>
                    <asp:Button ID="btnExportar" Text="Exportar a Excel" runat="server" CssClass="btn"
                        OnClick="btnExportar_Click"  Visible="false"/>
                </div>
                <asp:Label ID="lbmsg" Text="" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
