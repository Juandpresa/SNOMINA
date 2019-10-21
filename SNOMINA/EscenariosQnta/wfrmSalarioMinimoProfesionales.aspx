<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmSalarioMinimoProfesionales.aspx.cs" Inherits="EscenariosQnta.wfrmSalarioMinimoProfesionales" %>

<%@ Register Src="CuadroMensajeControlUsu.ascx" TagName="CuadroMensajeControlUsu" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
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
                    regexp = /.[0-9]{4}$/
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
                                Profesionales:
                            </td>
                            <td>
                                Valor:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtFecha" runat="server" Text="" onkeypress="return isDecimalKey(event, this);"
                                    required CssClass="textbox" TextMode="Date"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtProfesionales" runat="server" Text="" 
                                    CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtValor" runat="server" Text="" onkeypress="return isDecimalKey(event, this);"
                                    CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <uc1:CuadroMensajeControlUsu ID="CuadroMensajeControlUsu1" runat="server" />
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                    <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
                </div>
                <div style="height: 50px;">
                </div>
                <div>
                  
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
                            <asp:TemplateField HeaderText="Profesionales">
                                <ItemTemplate>
                                    <asp:Label ID="lblProfesionales" runat="server" Text='<%# Eval("Profesionales")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtProfesionales" runat="server" Text='<%# Eval("Profesionales")%>'
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor">
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server" Text='<%# Eval("Valor")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("Valor")%>' onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
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
