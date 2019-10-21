<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wfrmSalarioMinimo.aspx.cs" Inherits="EscenariosQnta.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/grid.css" rel="stylesheet" type="text/css" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <!--===================== Content ======================--> 
   <div id="html">
        <div id="portfolio" >
            <div class="container_13"></div>  
        </div>       
        <div id="stars"></div>
        <div id="stars2"></div>
        <div id="srars3"></div>
            <div class="containerTitlePage ">
                <div class="titlePage">
                    SALARIO MINIMO
                </div>
            </div>
        </div>
        <div id="skills" >
  <div class="container_12">
  
  DATOS GENERALES
  <div style="width:auto; border:2px Solid #4a1414;"></div>
  <div class="contenPanel">
      <table>
        <tr>           
            <td>Fecha:</td>                               
        </tr>
        <tr>
            <td class="td"><asp:TextBox ID="txtFecha" runat="server" Text="" onkeypress="return isDecimalKey(event, this);"  required CssClass="textbox" TextMode="Date"></asp:TextBox> </td>                        
        </tr>
        <tr>
            <td>Val Zona A:</td>               
            <td>Val Zona B:</td>                                            
            <td>Val Zona C:</td>            
        </tr>
        <tr>
            <td class="td"><asp:TextBox ID="txtValZonaA" runat="server" Text="" onkeypress="return isDecimalKey(event, this);" CssClass="textbox"></asp:TextBox> </td>
            <td class="td"><asp:TextBox ID="txtValZonaB" runat="server" Text="" onkeypress="return isDecimalKey(event, this);" CssClass="textbox"></asp:TextBox> </td>              
            <td class="td"><asp:TextBox ID="txtValZonaC" runat="server" Text="" onkeypress="return isDecimalKey(event, this);" CssClass="textbox"></asp:TextBox> </td>            
        </tr>    
        </table>
    </div>
        <div>
            <asp:Button ID="btnGuardar" Text="Guardar" runat="server"  CssClass="btn" 
                onclick="btnGuardar_Click"/>
        </div>
        <div style="height:50px;"></div>
        <div>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>--%>
<asp:GridView ID="gvSalarioMinimo" runat="server" CssClass="mGrid" AutoGenerateColumns = "false"  
onrowediting="gvSalarioMinimo_RowEditing" onrowupdating="gvSalarioMinimo_RowUpdating"  
        onrowcancelingedit="gvSalarioMinimo_RowCancelingEdit" AllowPaging="true" 
                 PageSize="50" onpageindexchanging="gvSalarioMinimo_PageIndexChanging">
<Columns>
<asp:TemplateField HeaderText = "Id" Visible="false">
    <ItemTemplate>
        <asp:Label ID="lblId" runat="server"
        Text='<%# Eval("Id")%>'></asp:Label>
    </ItemTemplate>   
</asp:TemplateField>
<asp:TemplateField HeaderText = "Fecha" >
    <ItemTemplate>
        <asp:Label ID="lblFecha" runat="server"
        Text='<%# Eval("Fecha")%>'></asp:Label>
    </ItemTemplate>   
    <EditItemTemplate>
        <asp:TextBox ID="txtFecha" runat="server"
            Text='<%# Eval("Fecha")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
    </EditItemTemplate>  
</asp:TemplateField>
<asp:TemplateField  HeaderText = "ValZonaA">
    <ItemTemplate>
        <asp:Label ID="lblValZonaA" runat="server"
                Text='<%# Eval("ValZonaA")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtValZonaA" runat="server"
            Text='<%# Eval("ValZonaA")%>' onkeypress="return isDecimalKey(event, this)"></asp:TextBox>
    </EditItemTemplate>    
</asp:TemplateField>
<asp:TemplateField   HeaderText = "ValZonaB">
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
</asp:TemplateField>
<asp:CommandField  ShowEditButton="True" >
    <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
    </asp:CommandField>
</Columns>
<AlternatingRowStyle BackColor="#EDECEC"  />
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
