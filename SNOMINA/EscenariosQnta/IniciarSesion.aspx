﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs"
    Inherits="EscenariosQnta.Login" %>

<!DOCTYPE html />
<html>
<head>
    <meta charset="UTF-8">
    <link href="css/login.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form class="login-form">
    <p class="login-text">
        <span class="fa-stack fa-lg">
            <i class="fa fa-circle fa-stack-2x"></i>
            <i class="fa fa-lock fa-stack-1x"></i>
        </span>
    </p>
    <input type="email" class="login-username" autofocus="true" required="true" placeholder="Email" />
    <input type="password" class="login-password" required="true" placeholder="Password" />
    <input type="submit" name="Login" value="Login" class="login-submit" />
    </form>
    <a href="#" class="login-forgot-pass">forgot password?</a>
    <div class="underlay-photo">
    </div>
    <div class="underlay-black">
    </div>
</body>
</html>
