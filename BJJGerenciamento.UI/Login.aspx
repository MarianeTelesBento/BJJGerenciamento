<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BJJGerenciamento.UI.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .form-container {
            margin-top: 100px;
            padding: 30px;
            background-color: #f9f9f9;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
        }
        .btn-custom {
            background-color: #007bff;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="form-container">
                        <h2 class="text-center">Login</h2>
                        <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger" Visible="false" />
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control mb-3" placeholder="Usuário" />
                        <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control mb-3" placeholder="Senha" />
                        <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-custom btn-block" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
