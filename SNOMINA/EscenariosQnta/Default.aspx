<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="EscenariosQnta.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" type="text/css" href="../css/normalize.css" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container home">
            <%--<div class="content"> --%>
            <div id="large-header" class="large-header">
                <%--<canvas id=""></canvas>--%>
                <div>
                    <h1 class="main-title">
                        <span class="thin"></span>
                        SISTEMA DE NOMINAS
                        </h1>
                </div>
                <div>
                    <%--<img class="logoInicio" src="../image/gif.gif" alt="" />--%>
                </div>
            </div>
            <%--</div>--%>
        </div>
    </div>
    <!-- /container -->
    <script src="../js/TweenLite.min.js"></script>
    <script src="../js/EasePack.min.js"></script>
    <script src="../js/rAF.js"></script>
    <script src="../js/home.js"></script>
</asp:Content>
