<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmCalculo.aspx.cs" Inherits="EscenariosQnta.wfrmCalculo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function openpopup() {
            document.getElementById('light').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }


        function closepopup() {
            document.getElementById('light').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
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
                    CALCULO
                </div>
            </div>
        </div>
        <div id="skills">
            <div class="container_12">
                CALCULOS
                <div style="width: auto; border: 2px Solid #4a1414;">
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                Cliente
                            </td>
                            <td>
                                Escenario
                            </td>
                            <%-- <td>
                                Propuesta
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlCliente" AutoPostBack="true" CssClass="cssDropdown"
                                    OnSelectedIndexChanged="ddlCliente_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlEscenario" AutoPostBack="true" CssClass="cssDropdown"
                                    OnSelectedIndexChanged="ddlEscenario_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                            <%--   <td>
                                <asp:DropDownList runat="server" ID="ddlPropuesta" AutoPostBack="true" 
                                    CssClass="cssDropdown" 
                                    onselectedindexchanged="ddlPropuesta_SelectedIndexChanged"></asp:DropDownList>
                            </td>--%>
                            <td>
                                 <asp:Button ID="btnCalcular" runat="server" Text="Re-Calcular" CssClass="btn" 
                                     Visible="false" onclick="btnCalcular_Click" OnClientClick="openpopup()"/>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <div style="overflow: auto; height: 260px;">
                        <asp:GridView runat="server" ID="gvCalculo" CssClass="mGrid" Width="100%" OnRowDataBound="gvCalculo_RowDataBound">
                            <AlternatingRowStyle BackColor="#EDECEC" />
                        </asp:GridView>
                    </div>
                    <div id="tbEstaus" runat="server" visible="false">
                        <table>
                            <tr>
                                <td>
                                    Estatus
                                </td>
                                <td>
                                    Notas
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlEstatus" AutoPostBack="true" CssClass="cssDropdown">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNotas" runat="server" Text="" CssClass="textbox" TextMode="multiline"
                                        Columns="50" Rows="5"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnGuardar" Text="Guardar" CssClass="btn" OnClick="btnGuardar_Click" />
                                </td>
                                <td class="td">
                                </td>
                                <td>
                                    <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btn" Visible="false"
                                        OnClick="btnExportar_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
         <div id="light" class="bright_content image">      
            <div class="titlePage"> Procesando... </div>
    </div>
    <div id="fade" class="dark_overlay">
    </div>
    </div>

   
</asp:Content>
