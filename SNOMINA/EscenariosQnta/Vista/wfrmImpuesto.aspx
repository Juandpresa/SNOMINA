<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmImpuesto.aspx.cs" Inherits="EscenariosQnta.wfrmImpuesto" %>

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
        <div id="html">
            <!--===================== Content ======================-->
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
            <div class="containerTitlePage">
                <div class="titlePage">
                    IMPUESTO</div>
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
                                Periodo:
                            </td>
                            <td class="td">
                                <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="cssDropdown" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                </div>
                <div style="height: 50px;">
                </div>
                <div style="overflow:auto; height:260px;">
                    <asp:GridView ID="gvImpuesto" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                        AllowPaging="true" PageSize="50" ShowFooter="true" OnRowCancelingEdit="gvImpuesto_RowCancelingEdit"
                        OnRowEditing="gvImpuesto_RowEditing" OnRowUpdating="gvImpuesto_RowUpdating">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Limite Inferior">
                                <ItemTemplate>
                                    <asp:Label ID="lblLimInf" runat="server" Text='<%# Eval("LimInfImpuesto")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLimInf" runat="server" Text='<%# Eval("LimInfImpuesto")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtLimInf" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Limite Superior">
                                <ItemTemplate>
                                    <asp:Label ID="lblLimSup" runat="server" Text='<%# Eval("LimSupImpuesto")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLimSup" runat="server" Text='<%# Eval("LimSupImpuesto")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtLimSup" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cuota Fija">
                                <ItemTemplate>
                                    <asp:Label ID="lblCuotaFija" runat="server" Text='<%# Eval("CuotaFija")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCuotaFija" runat="server" Text='<%# Eval("CuotaFija")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCuotaFija" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Porcentaje">
                                <ItemTemplate>
                                    <asp:Label ID="lblPorcentaje" runat="server" Text='<%# Eval("Porcentaje")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPorcentaje" runat="server" Text='<%# Eval("Porcentaje")%>' onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPorcentaje" runat="server" onkeypress="return isDecimalKey(event, this);"></asp:TextBox>
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
