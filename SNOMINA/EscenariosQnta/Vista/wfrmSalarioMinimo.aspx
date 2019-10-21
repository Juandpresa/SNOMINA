<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmSalarioMinimo.aspx.cs" Inherits="EscenariosQnta.wfrmSalarioMinimo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- CSS -->
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap-datepicker.css" rel="stylesheet" type="text/css" />
    <!-- JS -->
    <script src="../js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".datepicker").datepicker({ format: 'dd/mm/yyyy', autoclose: true, todayBtn: 'linked' })
        });

        // **** Valida Letras *****//
        function isAlphabetKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 93 && charCode >= 65) || (charCode <= 122 && charCode >= 97 || charCode == 32)) {

                return true;
            }
            return false;

        }

        //**** Valida Fecha ****//
        function isPhoneNumnberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 57 && charCode >= 48) || (charCode == 47)) {

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
                    regexp = /.[0-9]{5}$/
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
            <%--<div id="stars2">
            </div>--%>
            <div id="srars3">
            </div>
            <div class="containerTitlePage ">
                <div class="titlePage">
                    SALARIO MINIMO GENERAL
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
                                Salario Minimo General:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtFecha" runat="server" Text="" required CssClass="datepicker" onkeypress="return isPhoneNumnberKey(event, this);"
                                        placeholder="dd/mm/yyyy"></asp:TextBox>
                               
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtValZonaA" runat="server" Text="" onkeypress="isDecimalKey(event, this); return false;"
                                     CssClass="textbox" required></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <%--<td>Val Zona B:</td>                                            
                                <td>Val Zona C:</td> --%>
                        </tr>
                        <tr>
                            <%-- <td class="td"><asp:TextBox ID="txtValZonaB" runat="server" Text="" onkeypress="return isDecimalKey(event, this);" CssClass="textbox"></asp:TextBox> </td>              
                                 <td class="td"><asp:TextBox ID="txtValZonaC" runat="server" Text="" onkeypress="return isDecimalKey(event, this);" CssClass="textbox"></asp:TextBox> </td>  --%>
                        </tr>
                    </table>
                </div>
                <div>
                    <asp:Button ID="btnGuardar" Text="Guardar" runat="server" CssClass="btn" OnClick="btnGuardar_Click" />
                </div>
                <div style="height: 50px;">
                </div>
                <div style="overflow: auto; height: 260px;">
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                       <ContentTemplate>--%>
                    <asp:GridView ID="gvSalarioMinimo" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                        OnRowEditing="gvSalarioMinimo_RowEditing" OnRowUpdating="gvSalarioMinimo_RowUpdating"
                        OnRowCancelingEdit="gvSalarioMinimo_RowCancelingEdit" AllowPaging="true" PageSize="50"
                        OnPageIndexChanging="gvSalarioMinimo_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFecha" runat="server" Text='<%# Eval("Fecha")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salario Minimo">
                                <ItemTemplate>
                                    <asp:Label ID="lblValZonaA" runat="server" Text='<%# Eval("ValZonaA")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValZonaA" runat="server" Text='<%# Eval("ValZonaA")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField   HeaderText = "ValZonaB">
                                <ItemTemplate>
                                    <asp:Label ID="lblValZonaB" runat="server"
                                            Text='<%# Eval("ValZonaB")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValZonaB" runat="server"
                                        Text='<%# Eval("ValZonaB")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>    
                            </asp:TemplateField>
                            <asp:TemplateField   HeaderText = "ValZonaC">
                                <ItemTemplate>
                                    <asp:Label ID="lblValZonaC" runat="server"
                                            Text='<%# Eval("ValZonaC")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValZonaC" runat="server"
                                        Text='<%# Eval("ValZonaC")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>    
                            </asp:TemplateField>--%>
                            <asp:CommandField ShowEditButton="True">
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
