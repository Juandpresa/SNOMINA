<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmEscenarioConcepto.aspx.cs" Inherits="EscenariosQnta.wfrmEscenarioConcepto" %>

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

        function ValidateUsPhoneNumber() {
            document.getElementById('txtTelefono').addEventListener('input', function (e) {
                var x = e.target.value.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
                e.target.value = !x[2] ? x[1] : '(' + x[1] + ') ' + x[2] + (x[3] ? '-' + x[3] : '');
            });
        }


        function SelectAllP(id) {
            var gridP = document.getElementById("<%= gvEscenarioConceptoP.ClientID %>"); ;
            var chkP = document.getElementById(id);
            var cellP;

            if (chkP.checked == true) {
                if (gridP.rows.length > 0) {
                    for (i = 1; i < gridP.rows.length; i++) {
                        for (var k = 0; k < gridP.rows[i].cells.length; k++) {
                            cellP = gridP.rows[i].cells[k];
                            for (j = 0; j < cellP.childNodes.length; j++) {
                                if (cellP.childNodes[j].type == "checkbox") {
                                    cellP.childNodes[j].checked = true;
                                }
                            }
                        }
                    }
                }
            }
            else {
                if (gridP.rows.length > 0) {
                    for (i = 1; i < gridP.rows.length; i++) {
                        for (var k = 0; k < gridP.rows[i].cells.length; k++) {
                            cellP = gridP.rows[i].cells[k];
                            for (j = 0; j < cellP.childNodes.length; j++) {
                                if (cellP.childNodes[j].type == "checkbox") {
                                    cellP.childNodes[j].checked = false;
                                }
                            }
                        }
                    }
                }
            }

        }

        function SelectAllH(id) {

            var gridH = document.getElementById("<%= gvEscenarioConceptoH.ClientID %>"); ;
            var chkH = document.getElementById(id);
            var cellH;

            if (chkH.checked == true) {
                if (gridH.rows.length > 0) {
                    for (i = 1; i < gridH.rows.length; i++) {
                        for (var k = 0; k < gridH.rows[i].cells.length; k++) {
                            cellH = gridH.rows[i].cells[k];
                            for (j = 0; j < cellH.childNodes.length; j++) {
                                if (cellH.childNodes[j].type == "checkbox") {
                                    cellH.childNodes[j].checked = true;
                                }
                            }
                        }
                    }
                }
            }
            else {
                if (gridH.rows.length > 0) {
                    for (i = 1; i < gridH.rows.length; i++) {
                        for (var k = 0; k < gridH.rows[i].cells.length; k++) {
                            cellH = gridH.rows[i].cells[k];
                            for (j = 0; j < cellH.childNodes.length; j++) {
                                if (cellH.childNodes[j].type == "checkbox") {
                                    cellH.childNodes[j].checked = false;
                                }
                            }
                        }
                    }
                }
            }


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
                    ESCENARIO CONCEPTO
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
                                Cliente:
                            </td>
                            <td>
                                Escenario:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:DropDownList ID="ddlCliente" runat="server" AutoPostBack="true" CssClass="cssDropdown"
                                    OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlEscenario" runat="server" AutoPostBack="true" CssClass="cssDropdown"
                                    OnSelectedIndexChanged="ddlEscenario_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="height: 50px;">
                </div>
                <div id="contenedorconceptos" runat="server" style="text-align: center; width: 95%;
                    border: 2px groove #4a1414; height: auto; border-radius: 12px;" visible="false">
                    <div style="float: left; width: 40%; margin: 2px; overflow: auto;">
                        <asp:GridView ID="gvEscenarioConceptoP" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                            OnRowDataBound="gvEscenarioConceptoP_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="30px" HeaderText="" Visible="true">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAllP" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionP" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Conceptos" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbId_Conceptos" runat="server" Text='<%# Bind("Id_Conceptos") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Corto" SortExpression="Cliente">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNomCorto" runat="server" Text='<%# Bind("NomCorto") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculo" SortExpression="Calculo">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCalculo" runat="server" Text='<%# Bind("Calculo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#EDECEC" />
                        </asp:GridView>
                    </div>
                    <div style="float: left; width: 13%; text-align: center; margin: 5px;">
                        <div>
                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn" Text=">>" OnClick="btnAgregar_Click" />
                        </div>
                        <div>
                            <asp:Button ID="btnQuitar" runat="server" CssClass="btn" Text="<<" OnClick="btnQuitar_Click" />
                        </div>
                        <div style="margin: 50px 10px 10px 10px;">
                            <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                        </div>
                    </div>
                    <div style="float: left; width: 40%; margin: 2px; overflow: auto;">
                        <asp:GridView ID="gvEscenarioConceptoH" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                            OnRowDataBound="gvEscenarioConceptoH_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="30px" HeaderText="" Visible="true">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAllH" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionP" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Id_Conceptos" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbId_Conceptos" runat="server" Text='<%# Bind("Id_Conceptos") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre Corto" SortExpression="Cliente">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNomCorto" runat="server" Text='<%# Bind("NomCorto") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Calculo" SortExpression="Calculo">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCalculo" runat="server" Text='<%# Bind("Calculo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <AlternatingRowStyle BackColor="#EDECEC" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
