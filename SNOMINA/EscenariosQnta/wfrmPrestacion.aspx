<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmPrestacion.aspx.cs" Inherits="EscenariosQnta.wfrmPrestacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
    <script language="Javascript" type="text/javascript">
        function isAlphabetKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97 || charCode == 32)) {

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
                regexp = /.[0-9]{4}$/
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
                    FACTOR INTEGRACION
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
                                Factor de Integracion
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlPrestaciones" runat="server" CssClass="cssDropdown" OnSelectedIndexChanged="ddlPrestaciones_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnAgregarEditar" runat="server" Text="Agregar / Editar" CssClass="btn"
                                    OnClick="btnAgregarEditar_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <div runat="server" id="divFactor" visible="false">
                        <table>
                            <tr>
                                <td>
                                    Prestaciones:
                                </td>
                                <td>
                                    Descripcion:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtNombre" runat="server" Text="" onkeypress="return isAlphabetKey(event)"
                                        CssClass="textbox"></asp:TextBox>
                                </td>
                                  <td class="td">
                                    <asp:TextBox ID="txtDescripcion" runat="server" Text="" onkeypress="return isAlphabetKey(event)"
                                        CssClass="textbox"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" OnClick="btnGuardar_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelar" Text="Cancelar" runat="server" OnClick="btnCancelar_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvPrestaciones" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                        AllowPaging="true" ShowFooter="true" OnRowCancelingEdit="gvPrestaciones_RowCancelingEdit"
                                        OnRowEditing="gvPrestaciones_RowEditing" OnRowUpdating="gvPrestaciones_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_Prest" runat="server" Text='<%# Eval("Id_Prest")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("Nombre")%>' onkeypress="return isAlphabetKey(event)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Descripcion">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("Descripcion")%>'
                                                        onkeypress="return isAlphabetKey(event)"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField  ShowEditButton="True" >
    <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
    </asp:CommandField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#EDECEC" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                </div>
                <div style="height: 50px;">
                </div>
                <div>
                    <asp:GridView ID="gvPrestacionesDetalle" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                        AllowPaging="true" PageSize="50" ShowFooter="true" OnRowCancelingEdit="gvPrestacionesDetalle_RowCancelingEdit"
                        OnRowEditing="gvPrestacionesDetalle_RowEditing" OnRowUpdating="gvPrestacionesDetalle_RowUpdating">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo Valor">
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoValor" runat="server" Text='<%# Eval("TipoValor")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTipoValor" runat="server" Text='<%# Eval("TipoValor")%>' onkeypress="return isAlphabetKey(event)"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtTipoValor" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Valor">
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server" Text='<%# Eval("Valor")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValor" runat="server" Text='<%# Eval("Valor")%>' onkeypress="return isDecimalKey(event)"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtValor" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <FooterTemplate>
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click">
                                    </asp:Button>
                                </FooterTemplate>
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
