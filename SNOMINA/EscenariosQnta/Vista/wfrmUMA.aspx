<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmUMA.aspx.cs" Inherits="EscenariosQnta.wfrmUMA" %>

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


        // **** Valida Numeros *****//
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode <= 57 && charCode >= 48)) {

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
            <div id="stars2">
            </div>
            <div id="srars3">
            </div>
            <div class="containerTitlePage ">
                <div class="titlePage">
                    UMA
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
                                UMA:
                            </td>
                            <%-- <td>
                                SMG:
                            </td>--%>
                            <td>
                                Factor Infonavit:
                            </td>
                        </tr>
                        <tr>
                            <td class="td">
                                <asp:TextBox ID="txtFecha" runat="server" Text="" CssClass="datepicker" required
                                    placeholder="dd/mm/yyyy"></asp:TextBox>
                            </td>
                            <td class="td">
                                <asp:TextBox ID="txtUMA" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                            </td>
                            <%-- <td class="td">
                                <asp:TextBox ID="txtSMG" runat="server" Text="" CssClass="textbox" required onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                            </td>--%>
                            <td class="td">
                                <asp:TextBox ID="txtFactorInfonavit" runat="server" Text="" CssClass="textbox" required
                                    onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
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
                    <asp:GridView ID="gvUMA" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                        OnRowEditing="gvUMA_RowEditing" OnRowUpdating="gvUMA_RowUpdating" OnRowCancelingEdit="gvUMA_RowCancelingEdit"
                        AllowPaging="true" PageSize="50">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fecha">
                                <ItemTemplate>
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("Fecha")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFecha" runat="server" Text='<%# Eval("Fecha")%>' onkeypress="return isDecimalKey(event)"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UMA">
                                <ItemTemplate>
                                    <asp:Label ID="lblUMA" runat="server" Text='<%# Eval("UMA")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUMA" runat="server" Text='<%# Eval("UMA")%>' onkeypress="return isDecimalKey(event)"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="SMG">
                                <ItemTemplate>
                                    <asp:Label ID="lblSMG" runat="server" Text='<%# Eval("SMG")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSMG" runat="server" Text='<%# Eval("SMG")%>' onkeypress="return isDecimalKey(event)"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Factor Infonavit">
                                <ItemTemplate>
                                    <asp:Label ID="lblFcInfonavit" runat="server" Text='<%# Eval("FcInfonavit")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFcInfonavit" runat="server" Text='<%# Eval("FcInfonavit")%>'
                                        onkeypress="return isDecimalKey(event)"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True">
                                <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
                            </asp:CommandField>
                        </Columns>
                        <AlternatingRowStyle BackColor="#EDECEC" />
                        <RowStyle HorizontalAlign="Right" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
