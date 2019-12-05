<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmEsquemaPago.aspx.cs" Inherits="EscenariosQnta.wfrmEsquemaPago" %>

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
                    ESQUEMA PAGO
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
                               Tipo de Esquema de Pago:
                            </td>
                            <td>
                                SP a Ejecutar:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtNEsquema" runat="server" Text="" onkeypress="return isAlphabetKey(event)"
                                    required="required" CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtSPutilizado" runat="server" Text="" 
                                    required="required" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>
                <div style="height: 50px;">
                </div>
                <div style="overflow:auto; height:260px;">
                    <asp:GridView ID="gvEsquemaPago" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                        OnRowCancelingEdit="gvEsquemaPago_RowCancelingEdit" OnRowEditing="gvEsquemaPago_RowEditing"
                        OnRowUpdating="gvEsquemaPago_RowUpdating">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id_TpoEsq" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId_TpoEsq" runat="server" Text='<%# Eval("IdCatEsquemas")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre Esquema">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreEsquema" runat="server" Text='<%# Eval("NombreEsquema")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNombreEsquema" runat="server" onkeypress="return isAlphabetKey(event)"
                                        Text='<%# Eval("NombreEsquema")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SP_Utilizado">
                                <ItemTemplate>
                                    <asp:Label ID="lblId_SPNombre" runat="server" Text='<%# Eval("SP_Nombre")%>'></asp:Label>
                                </ItemTemplate>
                                 <EditItemTemplate>
                                    <asp:TextBox ID="txtSPNombre" runat="server" onkeypress="return isAlphabetKey(event)"
                                        Text='<%# Eval("SP_Nombre")%>'></asp:TextBox>
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
</asp:Content>
