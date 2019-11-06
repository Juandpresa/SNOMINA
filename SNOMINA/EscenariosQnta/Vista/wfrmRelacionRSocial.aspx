<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Vista/Site.Master" CodeBehind="wfrmRelacionRSocial.aspx.cs" Inherits="EscenariosQnta.Vista.wfrmRelacionRSocial" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-datepicker.css" rel="stylesheet" type="text/css" />
    <!-- JS -->
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="../js/bootstrap.js" type="text/javascript"></script>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div>
        <div id="html">
            <div id="portfolio">
                <div class="container_13">
                </div>
            </div>

            <div class="containerTitlePage ">
                <div class="titlePage">
                    RELACIONES DE RAZONES SOCIALES
                </div>
            </div>
        </div>
        <div id="skills">
            <%--ACORDION PARA LAS SECCIONES--%>
            <div class="accordion container" id="accordionExample">
                <div class="card">
                    <div class="card-header" id="headingOne">
                        <h2 class="mb-0">
                            <button class="btn btn-link" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                DATOS GENERALES
                            </button>
                        </h2>
                    </div>

                    <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordionExample">
                        <div class="card-body">
                            <div class="container_12 container">
                                <div style="width: auto; border: 2px Solid #4a1414;">
                                </div>
                                <div class="contenPanel">
                                    <table>
                                        <tr>
                                            <td>Contratista:
                                            </td>
                                            <td>Empleadora:
                                            </td>
                                            <td>Pagadora:
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="td">
                                                <asp:DropDownList ID="ddlContratista" runat="server" CssClass="cssDropdown"></asp:DropDownList>
                                            </td>
                                            <td class="td">
                                                <asp:DropDownList ID="ddlEmpleadora" runat="server" CssClass="cssDropdown"></asp:DropDownList>
                                            </td>
                                            <td class="td">
                                                <asp:DropDownList ID="ddlPagadora" runat="server" CssClass="cssDropdown"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Cliente:
                                            </td>
                                            <td>Facturista:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td">
                                                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="cssDropdown"></asp:DropDownList>
                                            </td>
                                            <td class="td">
                                                <asp:DropDownList ID="ddlFacturista" runat="server" CssClass="cssDropdown"></asp:DropDownList>
                                            </td>
                                        </tr>

                                    </table>
                                </div>
                                <div style="width: auto; border: 2px Solid #4a1414;"></div>

                            </div>
                            <div>
                                        <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                                    </div>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>
    
</asp:content>
