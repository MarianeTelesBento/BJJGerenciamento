<%@ Page Title="Editar Usuário" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarUsuario.aspx.cs" Inherits="BJJGerenciamento.UI.EditarUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">

    <style>
        /* Seu CSS permanece o mesmo */
        .card-body .form-control,
        .card-body .input-group {
            max-width: 300px;
            margin-left: auto;
            margin-right: auto;
        }
        .form-control {
            height: calc(2.25rem + 2px);
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
        }
        .input-group > .form-control {
            height: calc(2.25rem + 2px);
            border-right: 0;
        }
        .input-group-text {
            height: calc(2.25rem + 2px);
            border-left: 0;
            background-color: #e9ecef;
            border-color: #ced4da;
            padding: 0.375rem 0.75rem;
            cursor: pointer;
            display: flex;
            align-items: center;
        }
        .input-group-text .btn {
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
        }
        .card-body .btn-custom {
            max-width: 200px;
            margin-left: auto;
            margin-right: auto;
            display: block;
            height: calc(2.25rem + 2px);
        }
        .input-group-text i {
            font-size: 1rem;
        }
    </style>

    <body>
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card shadow rounded-4">
                        <div class="card-header bg-primary text-white text-center">
                            <h4>Editar Usuário</h4>
                        </div>
                        <div class="card-body">

                            <div class="mb-3"> 
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Nome de Usuário" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <div class="input-group">
                                    <asp:TextBox ID="txtSenha" runat="server" TextMode="Password" CssClass="form-control" placeholder="Senha"></asp:TextBox>
                                    <div class="input-group-text">
                                        <button type="button" class="btn btn-link p-0 border-0" onclick="togglePasswordVisibility('<%= txtSenha.ClientID %>', 'eyeIconSenha')">
                                            <i id="eyeIconSenha" class="bi bi-eye"></i>
                                        </button>
                                    </div>
                                </div>
                            </div>

                            <div class="d-grid d-flex justify-content-center">
                                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-custom btn-primary" OnClick="btnSalvar_Click" />
                            </div>
                            <br />
                            <div class="d-grid d-flex justify-content-center">
                            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn btn-custom btn-primary" Style="background-color: #6c757d; color: white; border-color: #6c757d;" OnClick="btnVoltar_Click" />
                                </div>
                            <div class="mt-3 text-center">
                                <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript">
    function togglePasswordVisibility(inputElementId, iconElementId) {
        const inputField = document.getElementById(inputElementId);
        const eyeIcon = document.getElementById(iconElementId);

        if (inputField.type === "password") {
            inputField.type = "text";
            eyeIcon.classList.remove('bi-eye');
            eyeIcon.classList.add('bi-eye-slash');
        } else {
            inputField.type = "password";
            eyeIcon.classList.remove('bi-eye-slash');
            eyeIcon.classList.add('bi-eye');
        }
    }

    $(document).ready(function () {
        $("#<%= btnSalvar.ClientID %>").click(function (e) {
            let usuario = $("#<%= txtUsuario.ClientID %>").val().trim();
            let email = $("#<%= txtEmail.ClientID %>").val().trim();
            let senha = $("#<%= txtSenha.ClientID %>").val();

            if (usuario === "" || email === "" || senha === "") {
                alert("Preencha todos os campos.");
                e.preventDefault();
                return;
            }

            let emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!emailRegex.test(email)) {
                alert("Digite um e-mail válido.");
                e.preventDefault();
                return;
            }           

            if (senha.length < 6) {
                alert("A senha deve ter pelo menos 6 caracteres.");
                e.preventDefault();
                return;
            }
        });
    });
        </script>
    </body>
</asp:Content>
