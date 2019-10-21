<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmDatosAreaComercial.aspx.cs" Inherits="EscenariosQnta.wrfmDatosAreaComercial" %>

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
            //            key = e.keyCode ? e.keyCode : e.which
            //            // backspace
            //            if (key == 8) return true
            //            // 0-9
            //            if (key > 47 && key < 58) {
            //                if (field.value == "") return true
            //                regexp = /.[0-9]{}$/
            //                return !(regexp.test(field.value))
            //            }

            //            // .
            //            if (key == 46) {
            //                if (field.value == "") return false
            //                regexp = /^[0-9]+$/
            //                return regexp.test(field.value)
            //            }
            //            // other key
            //            return false


            document.getElementById('txtTelefono').addEventListener('input', function (e) {
                var x = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
                e.target.value = !x[2] ? x[1] : '(' + x[1] + ') ' + x[2] + (x[3] ? '-' + x[3] : '');
            });
        }



        function ValidateUsPhoneNumber() {
            document.getElementById('txtTelefono').addEventListener('input', function (e) {
                var x = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
                e.target.value = !x[2] ? x[1] : '(' + x[1] + ') ' + x[2] + (x[3] ? '-' + x[3] : '');
            });
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
                    AREA COMERCIAL
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
                                Nombre Contacto:
                            </td>
                            <td>
                                Apellido Paterno:
                            </td>
                            <td>
                                Apellido Materno:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtNombre" runat="server" Text="" onkeypress="return isAlphabetKey(event)"
                                    required CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtApellidoPaterno" runat="server" Text="" onkeypress="return isAlphabetKey(event)"
                                    required CssClass="textbox"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtApellidoMaterno" runat="server" Text="" onkeypress="return isAlphabetKey(event)"
                                    required CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Telefono:
                            </td>
                            <td>
                                Correo:
                            </td>
                            <td>
                                Notas:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtTelefono" runat="server" Text="" onkeypress="return isDecimalKey(event)"
                                    CssClass="textbox" placeholder="(___) ___-____"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtCorreo" runat="server" Text="" TextMode="Email" CssClass="textbox"
                                    placeholder="example@qnta.com"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtNotas" runat="server" Text="" CssClass="textbox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tipo
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlTipoArea" CssClass="cssDropdown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>
                <div style="height: 50px;">
                </div>
                <div>
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
                    <asp:GridView ID="gvEjecutivos" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                        OnRowEditing="gvEjecutivos_RowEditing" OnRowUpdating="gvEjecutivos_RowUpdating"
                        OnRowCancelingEdit="gvEjecutivos_RowCancelingEdit" AllowPaging="true" PageSize="50"
                        OnPageIndexChanging="gvEjecutivos_PageIndexChanging" 
                        onrowdatabound="gvEjecutivos_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id_EjecComer" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId_EjecComer" runat="server" Text='<%# Eval("Id_EjecComer")%>'></asp:Label>
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
                            <asp:TemplateField HeaderText="Apellido Paterno">
                                <ItemTemplate>
                                    <asp:Label ID="lblApellidoPaterno" runat="server" Text='<%# Eval("ApellidoPaterno")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtApellidoPaterno" runat="server" Text='<%# Eval("ApellidoPaterno")%>'
                                        onkeypress="return isAlphabetKey(event)"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Apellido Materno">
                                <ItemTemplate>
                                    <asp:Label ID="lblApellidoMaterno" runat="server" Text='<%# Eval("ApellidoMaterno")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtApellidoMaterno" runat="server" Text='<%# Eval("ApellidoMaterno")%>'
                                        onkeypress="return isAlphabetKey(event)"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Telefono">
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefono" runat="server" Text='<%# Eval("Telefono")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTelefono" runat="server" Text='<%# Eval("Telefono")%>' onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Correo">
                                <ItemTemplate>
                                    <asp:Label ID="lblCorreo" runat="server" Text='<%# Eval("Correo")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCorreo" runat="server" Text='<%# Eval("Correo")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Notas">
                                <ItemTemplate>
                                    <asp:Label ID="lblNotas" runat="server" Text='<%# Eval("Notas")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNotas" runat="server" Text='<%# Eval("Notas")%>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activo">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Eval("Activo")%>' Enabled="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Eval("Activo")%>' Enabled="true" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tipo de Area" SortExpression="Tipo de Area">
                                <ItemTemplate>
                                    <asp:Label ID="lbTipoArea" runat="server" Text='<%# Bind("TipoArea") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTipoAreagv" runat="server" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField  ShowEditButton="True" >
    <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
    </asp:CommandField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#EDECEC" />
                    </asp:GridView>
                    <%--</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID = "gvEjecutivos" />
</Triggers>
</asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
