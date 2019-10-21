<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmFunciones.aspx.cs" Inherits="EscenariosQnta.wfrmFunciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <script language="Javascript" type="text/javascript">
        function isAlphabetKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97 || charCode == 32)) {

                return true;
            }
            return false;

        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 57 && charCode >= 48)) {

                return true;
            }
            return false;
        }

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
                    FUNCIONES
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
                                Nombre Corto:
                            </td>
                            <td>
                                Descripcion:
                            </td>
                            <td>
                                Operacion:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtNomCorto" runat="server" Text="" required CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtDescripcion" runat="server" Text="" required CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtOperacion" runat="server" Text="" CssClass="textbox"></asp:TextBox>
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
                    <asp:GridView ID="gvFunciones" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                        OnRowCancelingEdit="gvFunciones_RowCancelingEdit" OnRowEditing="gvFunciones_RowEditing"
                        OnRowUpdating="gvFunciones_RowUpdating">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId_Func" runat="server" Text='<%# Eval("Id_Func")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre Corto">
                                <ItemTemplate>
                                    <asp:Label ID="lblNomCorto" runat="server" Text='<%# Eval("NomCorto")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNomCorto" runat="server" Text='<%# Eval("NomCorto")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripcion">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("Descripcion")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operacion">
                                <ItemTemplate>
                                    <asp:Label ID="lblOperacion" runat="server" Text='<%# Eval("Operacion")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOperacion" runat="server" Text='<%# Eval("Operacion")%>'></asp:TextBox>
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
