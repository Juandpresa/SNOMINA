<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
  CodeBehind="wfrmEmpleadoConsulta.aspx.cs" Inherits="EscenariosQnta.wfrmEmpleadoConsulta"
  MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="../css/style.css" rel="stylesheet" type="text/css" />
  <link href="../css/grid.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
        
    </style>
  <script type="text/javascript">
    var accordionItems = new Array();

    function init() {

      // Grab the accordion items from the page
      var divs = document.getElementsByTagName('div');
      for (var i = 0; i < divs.length; i++) {
        if (divs[i].className == 'accordionItem') accordionItems.push(divs[i]);
      }

      // Assign onclick events to the accordion item headings
      for (var i = 0; i < accordionItems.length; i++) {
        var h2 = getFirstChildWithTagName(accordionItems[i], 'H2');
        h2.onclick = toggleItem;
      }

      // Hide all accordion item bodies except the first
      for (var i = 1; i < accordionItems.length; i++) {
        accordionItems[i].className = 'accordionItem hide';
      }
    }


    function toggleItem() {
      var itemClass = this.parentNode.className;

      // Hide all items
      for (var i = 0; i < accordionItems.length; i++) {
        accordionItems[i].className = 'accordionItem hide';
      }

      // Show this item if it was previously hidden
      if (itemClass == 'accordionItem hide') {
        this.parentNode.className = 'accordionItem';

      }
    }

    function getFirstChildWithTagName(element, tagName) {
      for (var i = 0; i < element.childNodes.length; i++) {
        if (element.childNodes[i].nodeName == tagName) return element.childNodes[i];
      }
    }

    // **** Valida Letras *****//
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

    function SelectAll(id) {
      var grid = document.getElementById("<%= gvEmpleado.ClientID %>");;
      var chk = document.getElementById(id);
      var cell;

      if (chk.checked == true) {
        if (grid.rows.length > 0) {
          for (i = 1; i < grid.rows.length; i++) {
            for (var k = 0; k < grid.rows[i].cells.length; k++) {
              cell = grid.rows[i].cells[k];
              for (j = 0; j < cell.childNodes.length; j++) {
                if (cell.childNodes[j].type == "checkbox") {
                  cell.childNodes[j].checked = true;
                }
              }
            }
          }
        }
      }
      else {
        if (grid.rows.length > 0) {
          for (i = 1; i < grid.rows.length; i++) {
            for (var k = 0; k < grid.rows[i].cells.length; k++) {
              cell = grid.rows[i].cells[k];
              for (j = 0; j < cell.childNodes.length; j++) {
                if (cell.childNodes[j].type == "checkbox") {
                  cell.childNodes[j].checked = false;
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
     <%-- <div id="stars">
      </div>
      <div id="stars2">
      </div>
      <div id="srars3">
      </div>--%>
      <div class="containerTitlePage ">
        <div class="titlePage">
          EMPLEADO
        </div>
      </div>
    </div>
    <div id="skills">
      <div class="container_12">
        CONSULTA EMPLEADO
                <div style="width: auto; border: 2px Solid #4a1414;">
                </div>
        <div class="contenPanel">
          <table>
            <tr>
              <td>Nombre
              </td>
              <td>Apellido Paterno
              </td>
              <td>Apellido Materno
              </td>
            </tr>
            <tr>
              <td>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox"></asp:TextBox>
              </td>
              <td>
                <asp:TextBox ID="txtPaterno" runat="server" CssClass="textbox"></asp:TextBox>
              </td>
              <td>
                <asp:TextBox ID="txtMaterno" runat="server" CssClass="textbox"></asp:TextBox>
              </td>
            </tr>
          </table>
          <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn" OnClick="btnBuscar_Click"></asp:Button>
        </div>
        <div>
          <div style="overflow: auto; height: 260px;">
            <asp:UpdatePanel runat="server" ID="UpdatePanel">
              <ContentTemplate>
                <asp:GridView runat="server" ID="gvEmpleado" CssClass="mGrid" AutoGenerateColumns="False"
                  OnRowCancelingEdit="gvEmpleado_RowCancelingEdit" OnRowEditing="gvEmpleado_RowEditing"
                  OnRowUpdating="gvEmpleado_RowUpdating" OnRowDataBound="gvEmpleado_RowDataBound"
                  OnPageIndexChanging="gvEmpleado_PageIndexChanging" PageSize="100" AllowPaging="true">
                  <Columns>
                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="" Visible="true">
                      <HeaderTemplate>
                        <asp:CheckBox ID="checkAll" runat="server" />
                      </HeaderTemplate>
                      <ItemTemplate>
                        <asp:CheckBox ID="chkSeleccion" runat="server" />
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id_Empleado" Visible="false">
                      <ItemTemplate>
                        <asp:Label ID="lbId_Empleado" runat="server" Text='<%# Bind("Id_Empleado") %>'></asp:Label>
                      </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Clave" SortExpression="Clave">
                      <ItemTemplate>
                        <asp:Label ID="lbClave" runat="server" Text='<%# Bind("Clave") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:Label ID="txtClave" runat="server" AutoPostBack="true"
                          OnSelectedIndexChanged="ddlClientegv_SelectedIndexChanged">
                        </asp:Label>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cliente" SortExpression="Cliente">
                      <ItemTemplate>
                        <asp:Label ID="lbId_Cliente" runat="server" Text='<%# Bind("Nombre_RazonSocial") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:DropDownList ID="ddlClientegv" runat="server" AutoPostBack="true"
                          OnSelectedIndexChanged="ddlClientegv_SelectedIndexChanged">
                        </asp:DropDownList>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre" SortExpression="Nombre">
                      <ItemTemplate>
                        <asp:Label ID="lbNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtNombre" runat="server" Text='<%# Eval("Nombre")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paterno" SortExpression="Paterno">
                      <ItemTemplate>
                        <asp:Label ID="lbPaterno" runat="server" Text='<%# Bind("Paterno") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtPaterno" runat="server" Text='<%# Eval("Paterno")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Materno" SortExpression="Materno">
                      <ItemTemplate>
                        <asp:Label ID="lbMaterno" runat="server" Text='<%# Bind("Materno") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtMaterno" runat="server" Text='<%# Eval("Materno")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Puesto" SortExpression="Puesto">
                      <ItemTemplate>
                        <asp:Label ID="lbPuesto" runat="server" Text='<%# Bind("Puesto") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtPuesto" runat="server" Text='<%# Eval("Puesto")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripcion Puesto" SortExpression="Descripcion Puesto">
                      <ItemTemplate>
                        <asp:Label ID="lbDescriPto" runat="server" Text='<%# Bind("DescriPto") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtDescriPto" runat="server" Text='<%# Eval("DescriPto")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prima Riesgo" SortExpression="Prima Riesgo">
                      <ItemTemplate>
                        <asp:Label ID="lbPrimaRiesgo" runat="server" Text='<%# Bind("PrimaRiesgo") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:DropDownList ID="ddlPrimaRiesgogv" runat="server">
                        </asp:DropDownList>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Ingreso" SortExpression="Fecha Ingreso">
                      <ItemTemplate>
                        <asp:Label ID="lbFechaIngreso" runat="server" Text='<%# Bind("FechaIngreso") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtFechaIngreso" runat="server" Text='<%# Eval("FechaIngreso")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Nacimiento" SortExpression="Fecha Nacimiento">
                      <ItemTemplate>
                        <asp:Label ID="lbFechaNac" runat="server" Text='<%# Bind("FechaNac") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtFechaNac" runat="server" Text='<%# Eval("FechaNac")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo Esquema" SortExpression="Tipo Esquema">
                      <ItemTemplate>
                        <asp:Label ID="lbTipoEsquema" runat="server" Text='<%# Bind("TipoEsquema") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:RadioButtonList ID="rbtTipoEsquema" runat="server" OnSelectedIndexChanged="rbtTipoEsquema_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nomina %" SortExpression="PorcNomina">
                      <ItemTemplate>
                        <asp:Label ID="lbPorcNomina" runat="server" Text='<%# Bind("PorcNomina") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtPorcNomina" runat="server" Text='<%# Eval("PorcNomina")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Asimilados %" SortExpression="PorcAsimilados">
                      <ItemTemplate>
                        <asp:Label ID="lbPorcAsimilados" runat="server" Text='<%# Bind("PorcAsimilados") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtPorcAsimilados" runat="server" Text='<%# Eval("PorcAsimilados")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Honorarios %" SortExpression="PorcHonorarios">
                      <ItemTemplate>
                        <asp:Label ID="lbPorcHonorarios" runat="server" Text='<%# Bind("PorcHonorarios") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtPorcHonorarios" runat="server" Text='<%# Eval("PorcHonorarios")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sueldo" SortExpression="Sueldo">
                      <ItemTemplate>
                        <asp:Label ID="lbSueldo" runat="server" Text='<%# Bind("Sueldo") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtSueldo" runat="server" Text='<%# Eval("Sueldo")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sueldo Bruto" SortExpression="Sueldo Bruto">
                      <ItemTemplate>
                        <asp:Label ID="lbSueldoBruto" runat="server" Text='<%# Bind("SueldoBruto") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtSueldoBruto" runat="server" Text='<%# Eval("SueldoBruto")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sueldo Neto" SortExpression="Sueldo Neto">
                      <ItemTemplate>
                        <asp:Label ID="lbSueldoNeto" runat="server" Text='<%# Bind("SueldoNeto") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtSueldoNeto" runat="server" Text='<%# Eval("SueldoNeto")%>'></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Clasificacion" SortExpression="Clasificacion">
                      <ItemTemplate>
                        <asp:Label ID="lbClasificacion" runat="server" Text='<%# Bind("Clasificacion") %>'></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:DropDownList ID="ddlClasificaciongv" runat="server">
                        </asp:DropDownList>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="SueldoMensual" SortExpression="SueldoMensual">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSueldoMensual" runat="server" Text='<%# Bind("SueldoMensual") %>'
                                            Enabled="false"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSueldoMensual" runat="server" Text='<%# Eval("SueldoMensual")%>'
                                            Enabled="false"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="SueldoDiario" SortExpression="SueldoDiario">
                      <ItemTemplate>
                        <asp:Label ID="lbSueldoDiario" runat="server" Text='<%# Bind("SueldoDiario") %>'
                          Enabled="false"></asp:Label>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txtSueldoDiario" runat="server" Text='<%# Eval("SueldoDiario")%>'
                          Enabled="false"></asp:TextBox>
                      </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True">
                      <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
                    </asp:CommandField>
                  </Columns>
                  <AlternatingRowStyle BackColor="#EDECEC" />
                </asp:GridView>
              </ContentTemplate>
            </asp:UpdatePanel>
          </div>
            <br />
            <br />
          <asp:Button ID="btnBorrar" runat="server" Text="Borrar Empleado" CssClass="btn" OnClick="btnBorrar_Click" />
        </div>
      </div>
    </div>
  </div>
</asp:Content>
