<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUpFunciones.aspx.cs"
    Inherits="EscenariosQnta.js.PopUpFunciones" %>

 <%@ Register Src="../Vista/ControlUsuario/CuadroMensajeControlUsu.ascx" TagName="CuadroMensajeControlUsu" TagPrefix="uc1" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tabla de Funciones</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <script language="Javascript" type="text/javascript">

        function validatextbox() {

            var calculo = document.getElementById('<%= txtCalculcoPopUp.ClientID %>').value;            
            var doc = window.opener.document;
          
          if(calculo == '' || calculo == null)
          {              
              document.getElementById("lblMsg").innerHTML = '*Obligatorio';
                return false;
            }
            else {
                lblMsg.value = '';
                theForm = doc.getElementById('form1');            
                theField = doc.getElementById('ContentPlaceHolder1_txtCalculo');
                theField.value = calculo;
                window.close();
                theForm.submit();
                return true;                
            }

        }


    </script>
</head>
<body>
    <form id="form1" runat="server">

    <div style="margin:10px 10px 10px 10px;">
        <div>
            Calculo:
            <asp:TextBox ID="txtCalculcoPopUp" runat="server" Text="" CssClass="textbox"></asp:TextBox>
            <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
        </div>
        <div>
            <asp:GridView ID="gvFunciones" runat="server" CssClass="mGrid" AutoGenerateColumns="False"
                OnSelectedIndexChanged="gvFunciones_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblId_Func" runat="server" Text='<%# Eval("Id_Func")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nombre Corto">
                        <ItemTemplate>
                            <asp:Label ID="lblNomCorto" runat="server" Text='<%# Eval("NomCorto")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNomCorto" runat="server" onkeypress="return isAlphabetKey(event, this);"
                                Text='<%# Eval("NomCorto")%>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descripcion">
                        <ItemTemplate>
                            <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("Descripcion")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescripcion" runat="server" onkeypress="return isAlphabetKey(event)"
                                Text='<%# Eval("Descripcion")%>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="Operacion">
                        <ItemTemplate>
                            <asp:Label ID="lblOperacion" runat="server" Text='<%# Eval("Operacion")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtOperacion" runat="server" onkeypress="return isNumberKey(event)"
                                Text='<%# Eval("Operacion")%>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton Text="Seleccionar" ID="lnkSelect" runat="server" CommandName="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle BackColor="#EDECEC" />
            </asp:GridView>
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnAceptar" Text="Aceptar" runat="server" CssClass="btn" OnClientClick="return validatextbox()" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancelar" Text="Cancelar" runat="server" CssClass="btn" OnClick="btnCancelar_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Literal ID="ltrMensaje" runat="server"></asp:Literal>
            <uc1:CuadroMensajeControlUsu ID="CuadroMensajeControlUsu1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
