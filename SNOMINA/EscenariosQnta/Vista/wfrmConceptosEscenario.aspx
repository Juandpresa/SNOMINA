<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="wfrmConceptosEscenario.aspx.cs" Inherits="EscenariosQnta.Vista.wfrmConceptosEscenario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/grid.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery-2.1.1.min.js"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            /*Code to copy the gridview header with style*/
            var gridHeader = $('#<%=gvConceptoEscenario.ClientID%>').clone(true);
            /*Code to remove first ror which is header row*/
            $(gridHeader).find("tr:gt(0)").remove();
            $('#<%=gvConceptoEscenario.ClientID%> tr th').each(function (i) {
                /* Here Set Width of each th from gridview to new table th */
                $("th:nth-child(" + (i + 1) + ")", gridHeader).css('width', ($(this).width()).toString() + "px");
            });
            $("#controlHead").append(gridHeader);
            $('#controlHead').css('position', 'absolute');
            $('#controlHead').css('top', $('#<%=gvConceptoEscenario.ClientID%>').offset().top - 10);

        });
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
                    CONCEPTOS POR ESCENARIO
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
                <asp:UpdatePanel runat="server" ID="UpdatePanel">
                    <ContentTemplate>
                        <%--    <div style=" border: 1px solid #084B8A; color: #ffffff; font-weight: bold;">
                            <table bgcolor="#3090C7" rules="all">
                                <tr>
                                <td style="width: 71px;">
                                        
                                    </td>
                                    <td style="width: 71px;">
                                        Concepto
                                    </td>
                                    <td style="width: 180px;">
                                        Calculo
                                    </td>
                                </tr>
                            </table>
                        </div>--%>
                         <%--<div style="height: 130px; overflow: auto;">--%>
                           <div>
                                <div id="controlHead">
                                </div>
                                <div style="height: 250px; overflow: auto">
                                    <asp:GridView ID="gvConceptoEscenario" runat="server" CssClass="mGrid" AutoGenerateColumns="false"
                                        OnRowCancelingEdit="gvConceptoEscenario_RowCancelingEdit" OnRowEditing="gvConceptoEscenario_RowEditing"
                                        OnRowUpdating="gvConceptoEscenario_RowUpdating" OnRowDataBound="gvConceptoEscenario_RowDataBound"
                                        Width="100%" >
                                        <Columns>
                                            <asp:CommandField ShowEditButton="True">
                                                <ControlStyle Font-Bold="True" ForeColor="#0066CC" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Id_Conceptos" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbId_Conceptos" runat="server" Text='<%# Bind("Id_Concepto") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbConcepto" runat="server" Text='<%# Bind("Concepto") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Calculo" SortExpression="Calculo">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbCalculo" runat="server" Text='<%# Bind("Calculo") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCalculo" runat="server" Text='<%# Eval("Calculo")%>' Width="100%"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#EDECEC" />
                                    </asp:GridView>
                               <%-- </div>
                            </div>--%>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
