<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmFactorIntegracion.aspx.cs" Inherits="EscenariosQnta.WebForm3" %>

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
                regexp = /.[0-9]{5}$/
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
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:DropDownList ID="ddlFactor" runat="server" CssClass="cssDropdown" OnSelectedIndexChanged="ddlFactor_SelectedIndexChanged"
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
                                    Tabla Prestaciones:
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <asp:TextBox ID="txtNombre" runat="server" Text="" CssClass="textbox"></asp:TextBox>
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
                                    <asp:GridView ID="gvFactor" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                        AllowPaging="true" ShowFooter="true" OnRowCancelingEdit="gvFactor_RowCancelingEdit"
                                        OnRowEditing="gvFactor_RowEditing" OnRowUpdating="gvFactor_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblId_Factor" runat="server" Text='<%# Eval("Id_Factor")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("Nombre")%>' ></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowEditButton="True">
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
                    <asp:GridView ID="gvDetalleFactor" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                        AllowPaging="true" PageSize="50" ShowFooter="true" OnRowCancelingEdit="gvDetalleFactor_RowCancelingEdit"
                        OnRowEditing="gvDetalleFactor_RowEditing" OnRowUpdating="gvDetalleFactor_RowUpdating">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Antiguedad">
                                <ItemTemplate>
                                    <asp:Label ID="lblAntiguedad" runat="server" Text='<%# Eval("Antiguedad")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAntiguedad" runat="server" Text='<%# Eval("Antiguedad")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAntiguedad" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dias Aguinaldo">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiasAguin" runat="server" Text='<%# Eval("DiasAguin")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDiasAguin" runat="server" Text='<%# Eval("DiasAguin")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txDiasAguin" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dias Vacaciones">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiasVac" runat="server" Text='<%# Eval("DiasVac")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDiasVac" runat="server" Text='<%# Eval("DiasVac")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txDiasVac" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="% Prima">
                                <ItemTemplate>
                                    <asp:Label ID="lblPorcPrima" runat="server" Text='<%# Eval("PorcPrima")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPorcPrima" runat="server" Text='<%# Eval("PorcPrima")%>' onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPorcPrima" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Otra Prestacion">
                                <ItemTemplate>
                                    <asp:Label ID="lblOtraPrestacion" runat="server" Text='<%# Eval("OtraPrestacion")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtOtraPrestacion" runat="server" Text='<%# Eval("OtraPrestacion")%>'
                                        onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOtraPrestacion" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Integracion">
                                <ItemTemplate>
                                    <asp:Label ID="lblIntegracion" runat="server" Text='<%# Eval("Integracion")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtIntegracion" runat="server" Text='<%# Eval("Integracion")%>'
                                        onkeypress="return isDecimalKey(event, this);" Enabled="false"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIntegracion" runat="server" onkeypress="return isDecimalKey(event, this);" Enabled="false"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <FooterTemplate>
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click">
                                    </asp:Button>
                                </FooterTemplate>
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
