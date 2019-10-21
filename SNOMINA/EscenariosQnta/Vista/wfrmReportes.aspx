<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmReportes.aspx.cs" Inherits="EscenariosQnta.Vista.wfrmReportes" EnableEventValidation ="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function myFunction() {
            var myWindow = window.open("viewReportes.aspx", "myWindow", "width=700,height=800");
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
                    REPORTES
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
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlCliente" CssClass="cssDropdown" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>                                     
                    </table>
                    <table>
                        <tr>
                            <td class="td">
                                Reporte
                            </td>
                            <td class="td">
                                Formato Reporte
                            </td>
                            <td class="td">
                                Escenario:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="chkReporte" runat="server" CssClass="chkBox" RepeatDirection="Horizontal">
                                    <asp:ListItem>Resumen</asp:ListItem>
                                    <asp:ListItem>Simplificado</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="chkFormatoReporte" runat="server" CssClass="chkBox" RepeatDirection="Horizontal">
                                    <asp:ListItem>PDF</asp:ListItem>
                                    <asp:ListItem>Excel</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkEscenarios" runat="server">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Button ID="btnBuscar" Text="Buscar" runat="server" CssClass="btn" OnClick="btnBuscar_Click" />
                </div>               
            </div>
        </div>
    </div>
</asp:Content>
