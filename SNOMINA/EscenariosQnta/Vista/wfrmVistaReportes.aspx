﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wfrmVistaReportes.aspx.cs"
    Inherits="EscenariosQnta.Vista.wfrmVistaReportes" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="ViewReports" runat="server" AutoDataBind="true" GroupTreeStyle-ShowLines="False" />
    </div>
    </form>
</body>
</html>