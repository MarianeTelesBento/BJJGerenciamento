<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BJJGerenciamento.UI.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.5/font/bootstrap-icons.css" rel="stylesheet">
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
       .logo {
        max-width: 180px;  /* aumente o tamanho da imagem */
        height: auto;
        border-radius: 50%;
        display: block;
        margin-left: auto;
        margin-right: auto;
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    
                   
                       <div class="form-container">
                       
                        <img src='<%= ResolveUrl("~/Images/Logo_BJJ.png") %>' alt="" class="logo mb-4" />
                       
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
