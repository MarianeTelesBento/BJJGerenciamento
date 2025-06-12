<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BJJGerenciamento.UI.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" /> <!-- MUITO IMPORTANTE -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background-color: #f0f2f5;
            font-family: 'Segoe UI', sans-serif;
        }

        .form-container {
            padding: 30px;
            background-color: #f9f9f9;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            width: 100%;
        }

        .btn-custom {
            background-color: #007bff;
            color: white;
        }

        .logo {
            max-width: 180px;
            height: auto;
            border-radius: 50%;
            display: block;
            margin: 0 auto 20px auto;
        }

        @media (max-width: 576px) {
            .form-container {
                padding: 20px;
            }

            .logo {
                max-width: 130px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-12 col-sm-10 col-md-8 col-lg-6 col-xl-4">
                    <div class="form-container mt-5">
                        <img src='<%= ResolveUrl("~/Images/Logo_BJJ.png") %>' alt="Logo" class="logo" />

                        <asp:Label ID="lblMensagem" runat="server" CssClass="text-danger" Visible="false" />

                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control mb-3" placeholder="Usuário" />

                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control" placeholder="Senha" />
                            <div class="input-group-append">
                                <button type="button" class="btn btn-outline-secondary" onclick="togglePassword()">
                                    <i id="eyeIcon" class="bi bi-eye"></i>
                                </button>
                            </div>
                        </div>

                        <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn btn-custom btn-block" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        function togglePassword() {
            const senha = document.getElementById('<%= txtSenha.ClientID %>');
            const eyeIcon = document.getElementById('eyeIcon');

            if (senha.type === "password") {
                senha.type = "text";
                eyeIcon.classList.remove('bi-eye');
                eyeIcon.classList.add('bi-eye-slash');
            } else {
                senha.type = "password";
                eyeIcon.classList.remove('bi-eye-slash');
                eyeIcon.classList.add('bi-eye');
            }
        }
    </script>
</body>
</html>

