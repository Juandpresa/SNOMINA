﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmNominaActual.aspx.cs" Inherits="EscenariosQnta.wfrmNominaActual" %>

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
                    NOMINA ACTUAL
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
                                Nombre:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtNombre" runat="server" Text="" onkeypress="return isAlphabetKey(event)"
                                    required CssClass="textbox"></asp:TextBox>
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
                    <asp:GridView ID="gvTipoNomina" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                        OnRowCancelingEdit="gvTipoNomina_RowCancelingEdit" OnRowEditing="gvTipoNomina_RowEditing"
                        OnRowUpdating="gvTipoNomina_RowUpdating">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id_EjecComer" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId_TipoNom" runat="server" Text='<%# Eval("Id_TipoNom")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNombre" runat="server" onkeypress="return isAlphabetKey(event)"
                                        Text='<%# Eval("Nombre")%>'></asp:TextBox>
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
